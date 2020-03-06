using Microsoft.EntityFrameworkCore;
using ReservationsApp.Models;

namespace ReservationsApp.Data
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options)
            : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().ToTable("Reservations");
        }
    }
}




