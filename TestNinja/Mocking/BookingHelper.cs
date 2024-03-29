﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository bookingRepository)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = bookingRepository.GetActiveBookings(booking.Id);

            if (bookings == null) return String.Empty;
            {
                var overlappingBooking =
                    bookings.FirstOrDefault(
                        b => booking.ArrivalDate < b.DepartureDate &&
                             b.ArrivalDate < booking.DepartureDate);

                return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
            }
        }
    }

    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; } //start
        public DateTime DepartureDate { get; set; } //end
        public string Reference { get; set; }
    }
}