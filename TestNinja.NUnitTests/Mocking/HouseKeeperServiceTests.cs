using Moq;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking;

[TestFixture]
public class HouseKeeperServiceTests
{
    private Mock<IEmailSender> _mailSender;
    private HouseKeeperService _service;
    private Mock<IXtraMessageBox> _xtraMessageBox;
    private Mock<IStatementGenerator> _statementGenerator;
    private Mock<IUnitOfWork> _unitOfWork;

    private static Housekeeper Housekeeper
    {
        get
        {
            var housekeeper = new Housekeeper()
            {
                Email = "a",
                FullName = "b",
                Oid = 1,
                StatementEmailBody = "c"
            };
            return housekeeper;
        }
        set => Housekeeper = value;
    }

    [SetUp]
    public void SetUp()
    {
        _mailSender = new Mock<IEmailSender>();
        _statementGenerator = new Mock<IStatementGenerator>();
        _xtraMessageBox = new Mock<IXtraMessageBox>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _service = new HouseKeeperService(_unitOfWork.Object, _statementGenerator.Object, _mailSender.Object,
            _xtraMessageBox.Object);
    }

    private void SetUpUnitOfWork(Housekeeper housekeeper)
    {
        _unitOfWork.Setup(x => x.Query<Housekeeper>()).Returns(new List<Housekeeper>()
        {
            housekeeper
        }.AsQueryable());
    }

    [Test]
    public void SendStatementEmails_WhenCalled_GenerateStatement()
    {
        var housekeeper = Housekeeper;
        SetUpUnitOfWork(housekeeper);
        _service.SendStatementEmails(SampleDate());
        _statementGenerator.Verify(s => s.SaveStatement(housekeeper.Oid, housekeeper.FullName, SampleDate()));
    }

    [Test]
    public void SendStatementEmails_WhenCalled_EmailStatements()
    {
        var housekeeper = Housekeeper;
        SetUpUnitOfWork(housekeeper);
        _statementGenerator.Setup(s => s.SaveStatement(housekeeper.Oid, housekeeper.FullName, SampleDate()))
            .Returns(housekeeper.FullName);
        _service.SendStatementEmails(SampleDate());
        _mailSender.Verify(
            x => x.EmailFile(housekeeper.Email, housekeeper.StatementEmailBody, housekeeper.FullName,
                It.IsAny<string>()));
    }

    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("")]
    public void SendStatementEmails_StatementFileIsNullEmptyWhite_ShouldNotEmailState(string returnFullname)
    {
        var housekeeper = Housekeeper;
        SetUpUnitOfWork(housekeeper);
        _statementGenerator.Setup(s => s.SaveStatement(housekeeper.Oid, housekeeper.FullName, SampleDate()))
            .Returns(() => returnFullname);
        _service.SendStatementEmails(SampleDate());
        _mailSender.Verify(
            x => x.EmailFile(housekeeper.Email, housekeeper.StatementEmailBody, housekeeper.FullName,
                It.IsAny<string>()), Times.Never);
    }

    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("")]
    public void SendStatementEmails_DifferentEmail_ShouldNotGenerateStatement(string email)
    {
        var housekeeper = Housekeeper;
        housekeeper.Email = email;
        SetUpUnitOfWork(housekeeper);
        _service.SendStatementEmails(SampleDate());
        _statementGenerator.Verify(s => s.SaveStatement(housekeeper.Oid, housekeeper.FullName, SampleDate()),
            Times.Never);
    }

    [Test]
    public void SendStatementEmails_EmailSendingFailed_DisplayMessageBox()
    {
        var housekeeper = Housekeeper;
        _statementGenerator.Setup(s => s.SaveStatement(housekeeper.Oid, housekeeper.FullName, SampleDate()))
            .Returns(() => housekeeper.FullName);
        _mailSender.Setup(x => x.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<string>())).Throws<Exception>();
        SetUpUnitOfWork(housekeeper);
        _service.SendStatementEmails(SampleDate());
        _xtraMessageBox.Verify(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
    }


    private DateTime SampleDate()
    {
        return new DateTime(2017, 1, 1);
    }
}