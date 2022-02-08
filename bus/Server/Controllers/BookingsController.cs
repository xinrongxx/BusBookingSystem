using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bus.Server.Data;
using bus.Shared.Domain;
using bus.Server.IRepository;

namespace bus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public BookingsController(ApplicationDbContext context)
        public BookingsController(IUnitOfWork unitofWork)
        {
            //_context = context;
            _unitOfWork = unitofWork;
        }

        // GET: api/Bookings
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Model>>> GetBookings()
        public async Task<ActionResult> GetBookings()
        {
            //return await _context.Bookings.ToListAsync();
            var Bookings = await _unitOfWork.Bookings.GetAll(includes: q => q.Include(x => x.Bus).Include(x => x.Customer).Include(x => x.Service));
            return Ok(Bookings);
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Model>> GetBooking(int id)
        public async Task<ActionResult> GetBookings(int id)
        {
            //var Model = await _context.Bookings.FindAsync(id);
            var Booking = await _unitOfWork.Bookings.Get(q => q.Id == id);

            if (Booking == null)
            {
                return NotFound();
            }

            //return Model;
            return Ok(Booking);
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking Booking)
        {
            if (id != Booking.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Model).State = EntityState.Modified;
            _unitOfWork.Bookings.Update(Booking);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);

            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!BookingExists(id))
                if (!await BookingExists(id))
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

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Seat>> PostBooking(Booking Booking)
        {
            //_context.Bookings.Add(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Bookings.Insert(Booking);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetBooking", new { id = Booking.Id }, Booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            //var Model = await _context.Bookings.FindAsync(id);
            var Booking = await _unitOfWork.Bookings.Get(q => q.Id == id);
            if (Booking == null)
            {
                return NotFound();
            }

            //_context.Bookings.Remove(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Bookings.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> BookingExists(int id)
        {
            //return _context.Bookings.Any(e => e.Id == id);
            var Booking = await _unitOfWork.Bookings.Get(q => q.Id == id);
            return Booking != null;
        }
    }
}