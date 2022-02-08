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
    public class SeatsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public SeatsController(ApplicationDbContext context)
        public SeatsController(IUnitOfWork unitofWork)
        {
            //_context = context;
            _unitOfWork = unitofWork;
        }

        // GET: api/Seats
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Model>>> GetSeats()
        public async Task<ActionResult> GetSeats()
        {
            //return await _context.Seats.ToListAsync();
            var Seats = await _unitOfWork.Seats.GetAll();
            return Ok(Seats);
        }

        // GET: api/Seats/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Model>> GetSeat(int id)
        public async Task<ActionResult> GetSeats(int id)
        {
            //var Model = await _context.Seats.FindAsync(id);
            var Seat = await _unitOfWork.Seats.Get(q => q.Id == id);

            if (Seat == null)
            {
                return NotFound();
            }

            //return Model;
            return Ok(Seat);
        }

        // PUT: api/Seats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeat(int id, Seat Seat)
        {
            if (id != Seat.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Model).State = EntityState.Modified;
            _unitOfWork.Seats.Update(Seat);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);

            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!SeatExists(id))
                if (!await SeatExists(id))
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

        // POST: api/Seats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Seat>> PostSeat(Seat Seat)
        {
            //_context.Seats.Add(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Seats.Insert(Seat);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetSeat", new { id = Seat.Id }, Seat);
        }

        // DELETE: api/Seats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            //var Model = await _context.Seats.FindAsync(id);
            var Seat = await _unitOfWork.Seats.Get(q => q.Id == id);
            if (Seat == null)
            {
                return NotFound();
            }

            //_context.Seats.Remove(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Seats.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> SeatExists(int id)
        {
            //return _context.Seats.Any(e => e.Id == id);
            var Seat = await _unitOfWork.Seats.Get(q => q.Id == id);
            return Seat != null;
        }
    }
}