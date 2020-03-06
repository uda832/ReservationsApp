using ReservationsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReservationsApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ReservationContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Reservations.Any())
            {
                return;   // DB has been seeded
            }

            DateTime reservationTime = new DateTime(2020, 4, 1, 10, 0, 0);

            context.Reservations.AddRange(new List<Reservation>()
            {
                new Reservation() {
                    Email = "John@Smith.com",
                    Phone = "7777777777",
                    StartTime = reservationTime,
                    EndTime = reservationTime.AddHours(1),
                    SubmittedDate = DateTime.Now, 
                },
                new Reservation() {
                    Email = "Jane@Doe.com",
                    Phone = "8888888888",
                    StartTime = reservationTime.AddDays(1),
                    EndTime = reservationTime.AddDays(1).AddHours(1),
                    SubmittedDate = DateTime.Now,
                },
            });

            context.SaveChanges();
        }
    }
}