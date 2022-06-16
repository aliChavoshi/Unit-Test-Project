using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            //Arrange 
            var reservation = new Reservation();
            //Act
            var result = reservation.CanBeCancelledBy(new User() {IsAdmin = true});
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_UserIsOwner_ReturnTrue()
        {
            //Arrange
            var reservation = new Reservation
            {
                //Act
                MadeBy = new User()
            };
            //Act
            var result = reservation.CanBeCancelledBy(reservation.MadeBy);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_OtherUser_ReturnFalse()
        {
            //Arrange
            var reservation = new Reservation() {MadeBy = new User()};
            //Act
            var result = reservation.CanBeCancelledBy(new User());
            //Assert
            Assert.IsFalse(result);
        }
    }
}