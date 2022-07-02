using Moq;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking;

[TestFixture]
public class BookingHelperTests_OverlappingBookingsExist
{
    private Booking _existingBooking;
    private Mock<IBookingRepository> _repository;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<IBookingRepository>();
        _existingBooking = new Booking
        {
            Id = 2,
            ArrivalDate = ArriveOn(2017, 1, 15),
            DepartureDate = DepartOn(2017, 1, 20),
            //15<x<20
            Reference = "a"
        };
        _repository.Setup(x => x.GetActiveBookings(1)).Returns(new List<Booking>()
        {
            //active bookings
            _existingBooking
        }.AsQueryable());
    }

    [Test]
    public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
    {
        //bookings
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = Before(_existingBooking.ArrivalDate, 2), //13
            DepartureDate = Before(_existingBooking.ArrivalDate), //14
            //10<x<14
        }, _repository.Object);

        Assert.IsEmpty(result);
    }

    [Test]
    public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBooking()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = Before(_existingBooking.ArrivalDate),
            DepartureDate = After(_existingBooking.ArrivalDate),
        }, _repository.Object);
        Assert.IsNotEmpty(result);
        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    [Test]
    public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBooking()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = Before(_existingBooking.ArrivalDate),
            DepartureDate = Before(_existingBooking.DepartureDate),
        }, _repository.Object);
        Assert.IsNotEmpty(result);
        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    [Test]
    public void BookingStartsInTheMiddleOfAnExistingBookingButFinishesAfter_ReturnExistingBooking()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = Before(_existingBooking.ArrivalDate),
            DepartureDate = After(_existingBooking.DepartureDate),
        }, _repository.Object);
        Assert.IsNotEmpty(result);
        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    [Test]
    public void BookingStartsAndFinishesAfterAnExistingBookings_ReturnEmptyString()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = After(_existingBooking.DepartureDate),
            DepartureDate = After(_existingBooking.DepartureDate, 2),
        }, _repository.Object);
        Assert.IsEmpty(result);
    }

    [Test]
    public void BookingIsCancelled_ReturnEmptyString()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = After(_existingBooking.DepartureDate),
            DepartureDate = After(_existingBooking.DepartureDate, 2),
            Status = "Cancelled"
        }, _repository.Object);
        Assert.IsEmpty(result);
    }

    [Test]
    public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBooking()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = Before(_existingBooking.ArrivalDate),
            DepartureDate = After(_existingBooking.DepartureDate),
        }, _repository.Object);
        Assert.IsNotEmpty(result);
        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    private static DateTime Before(DateTime dateTime, int days = 1)
    {
        return dateTime.AddDays(-days);
    }

    private static DateTime After(DateTime dateTime, int days = 1)
    {
        return dateTime.AddDays(days);
    }

    private static DateTime ArriveOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 14, 0, 0);
    }

    private static DateTime DepartOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 10, 0, 0);
    }
}