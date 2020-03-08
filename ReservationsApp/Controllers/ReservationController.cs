using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservationsApp.Data;
using ReservationsApp.Models;

namespace ReservationsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationContext _context;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ReservationContext context, ILogger<ReservationController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            _logger.LogInformation(nameof(GetReservations) + "(1) - Invoked...");

            return await _context.Reservations.ToListAsync();
        }

        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservationById(int id)
        {
            _logger.LogInformation(nameof(GetReservationById) + "(1) - Invoked...");

            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }


        // PUT: api/Reservation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            _logger.LogInformation(nameof(PutReservation) + "(1) - Invoked...");


            if (id != reservation.ReservationID)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Reservation
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _logger.LogInformation(nameof(PostReservation) + "(1) - Invoked...");

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.ReservationID }, reservation);
        }


        // DELETE: api/Reservation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            _logger.LogInformation(nameof(DeleteReservation) + "(1) - Invoked...");


            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }


        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationID == id);
        }
    }
}
