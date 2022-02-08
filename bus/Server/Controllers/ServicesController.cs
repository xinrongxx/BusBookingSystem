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
    public class ServicesController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public ServicesController(ApplicationDbContext context)
        public ServicesController(IUnitOfWork unitofWork)
        {
            //_context = context;
            _unitOfWork = unitofWork;
        }

        // GET: api/Services
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Model>>> GetServices()
        public async Task<ActionResult> GetServices()
        {
            //return await _context.Services.ToListAsync();
            var Services = await _unitOfWork.Services.GetAll();
            return Ok(Services);
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Model>> GetService(int id)
        public async Task<ActionResult> GetServices(int id)
        {
            //var Model = await _context.Services.FindAsync(id);
            var Service = await _unitOfWork.Services.Get(q => q.Id == id);

            if (Service == null)
            {
                return NotFound();
            }

            //return Model;
            return Ok(Service);
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service Service)
        {
            if (id != Service.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Model).State = EntityState.Modified;
            _unitOfWork.Services.Update(Service);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);

            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ServiceExists(id))
                if (!await ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Seat>> PostService(Service Service)
        {
            //_context.Services.Add(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Services.Insert(Service);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetService", new { id = Service.Id }, Service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            //var Model = await _context.Services.FindAsync(id);
            var Service = await _unitOfWork.Services.Get(q => q.Id == id);
            if (Service == null)
            {
                return NotFound();
            }

            //_context.Services.Remove(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Services.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> ServiceExists(int id)
        {
            //return _context.Services.Any(e => e.Id == id);
            var Service = await _unitOfWork.Services.Get(q => q.Id == id);
            return Service != null;
        }
    }
}