using System.Linq;

namespace TestNinja.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public BookingRepository()
        {
            _unitOfWork = new UnitOfWork();
        }

        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var bookings = _unitOfWork.Query<Booking>()
                .Where(
                    b => b.Status != "Cancelled");

            if (excludedBookingId.HasValue)
                bookings.Where(x => x.Id != excludedBookingId.Value);

            return bookings;
        }
    }
}