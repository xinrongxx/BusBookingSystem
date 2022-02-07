using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bus.Server.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bus.Server.Data;
using bus.Shared.Domain;


namespace bus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public BusesController(ApplicationDbContext context)
        public BusesController(IUnitOfWork unitofWork)
        {
            //_context = context;
            _unitOfWork = unitofWork;
        }

        // GET: api/Buses
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Model>>> GetBuses()
        public async Task<ActionResult> GetBuses()
        {
            //return await _context.Buses.ToListAsync();
            var Buses = await _unitOfWork.Buses.GetAll();
            return Ok(Buses);
        }

        // GET: api/Buses/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Model>> GetBuse(int id)
        public async Task<ActionResult> GetBuses(int id)
        {
            //var Model = await _context.Buses.FindAsync(id);
            var Bus = await _unitOfWork.Buses.Get(q => q.Id == id);

            if (Bus == null)
            {
                return NotFound();
            }

            //return Model;
            return Ok(Bus);
        }

        // PUT: api/Buses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBus(int id, Bus Bus)
        {
            if (id != Bus.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Model).State = EntityState.Modified;
            _unitOfWork.Buses.Update(Bus);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);

            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!BusExists(id))
                if (!await BusExists(id))
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

        // POST: api/Buses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bus>> PostBus(Bus Bus)
        {
            //_context.Buses.Add(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Buses.Insert(Bus);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetBus", new { id = Bus.Id }, Bus);
        }

        // DELETE: api/Buses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            //var Model = await _context.Buses.FindAsync(id);
            var Bus = await _unitOfWork.Buses.Get(q => q.Id == id);
            if (Bus == null)
            {
                return NotFound();
            }

            //_context.Buses.Remove(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Buses.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> BusExists(int id)
        {
            //return _context.Buses.Any(e => e.Id == id);
            var Bus = await _unitOfWork.Buses.Get(q => q.Id == id);
            return Bus != null;
        }
    }
}
