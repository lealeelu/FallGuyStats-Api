using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FallGuyStats.Data;
using FallGuyStats.Models;

namespace FallGuyStats
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundController : ControllerBase
    {
        private readonly EpisodeContext _context;

        public RoundController(EpisodeContext context)
        {
            _context = context;
        }

        // GET: api/Round
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoundModel>>> GetRounds()
        {
            return await _context.Rounds.ToListAsync();
        }

        // GET: api/Round/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoundModel>> GetRoundModel(int id)
        {
            var roundModel = await _context.Rounds.FindAsync(id);

            if (roundModel == null)
            {
                return NotFound();
            }

            return roundModel;
        }

        // PUT: api/Round/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoundModel(int id, RoundModel roundModel)
        {
            if (id != roundModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(roundModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoundModelExists(id))
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

        // POST: api/Round
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RoundModel>> PostRoundModel(RoundModel roundModel)
        {
            _context.Rounds.Add(roundModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoundModel", new { id = roundModel.Id }, roundModel);
        }

        // DELETE: api/Round/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RoundModel>> DeleteRoundModel(int id)
        {
            var roundModel = await _context.Rounds.FindAsync(id);
            if (roundModel == null)
            {
                return NotFound();
            }

            _context.Rounds.Remove(roundModel);
            await _context.SaveChangesAsync();

            return roundModel;
        }

        private bool RoundModelExists(int id)
        {
            return _context.Rounds.Any(e => e.Id == id);
        }
    }
}
