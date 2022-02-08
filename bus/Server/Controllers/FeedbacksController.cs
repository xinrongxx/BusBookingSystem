using bus.Server.IRepository;
using bus.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace bus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public FeedbacksController(ApplicationDbContext context)
        public FeedbacksController(IUnitOfWork unitofWork)
        {
            //_context = context;
            _unitOfWork = unitofWork;
        }

        // GET: api/Feedbacks
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Model>>> GetFeedbacks()
        public async Task<ActionResult> GetFeedbacks()
        {
            //return await _context.Feedbacks.ToListAsync();
            var Feedbacks = await _unitOfWork.Feedbacks.GetAll();
            return Ok(Feedbacks);
        }

        // GET: api/Feedbacks/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Model>> GetFeedback(int id)
        public async Task<ActionResult> GetFeedbacks(int id)
        {
            //var Model = await _context.Feedbacks.FindAsync(id);
            var Feedback = await _unitOfWork.Feedbacks.Get(q => q.Id == id);

            if (Feedback == null)
            {
                return NotFound();
            }

            //return Model;
            return Ok(Feedback);
        }

        // PUT: api/Feedbacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(int id, Feedback Feedback)
        {
            if (id != Feedback.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Model).State = EntityState.Modified;
            _unitOfWork.Feedbacks.Update(Feedback);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);

            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!FeedbackExists(id))
                if (!await FeedbackExists(id))
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

        // POST: api/Feedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Seat>> PostFeedback(Feedback Feedback)
        {
            //_context.Feedbacks.Add(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Feedbacks.Insert(Feedback);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetFeedback", new { id = Feedback.Id }, Feedback);
        }

        // DELETE: api/Feedbacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            //var Model = await _context.Feedbacks.FindAsync(id);
            var Feedback = await _unitOfWork.Feedbacks.Get(q => q.Id == id);
            if (Feedback == null)
            {
                return NotFound();
            }

            //_context.Feedbacks.Remove(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Feedbacks.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> FeedbackExists(int id)
        {
            //return _context.Feedbacks.Any(e => e.Id == id);
            var Feedback = await _unitOfWork.Feedbacks.Get(q => q.Id == id);
            return Feedback != null;
        }
    }
}