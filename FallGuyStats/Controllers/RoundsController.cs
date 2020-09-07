// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FallGuyStats.Data;
using FallGuyStats.Models;

namespace FallGuyStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundsController : ControllerBase
    {
        private readonly FallGuysContext _context;

        public RoundsController(FallGuysContext context)
        {
            _context = context;
        }

        // GET: api/Rounds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoundModel>>> GetRounds()
        {
            return await _context.Rounds.ToListAsync();
        }

        // GET: api/Rounds/5
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

        // PUT: api/Rounds/5
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

        // POST: api/Rounds
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RoundModel>> PostRoundModel(RoundModel roundModel)
        {
            _context.Rounds.Add(roundModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoundModel", new { id = roundModel.Id }, roundModel);
        }

        // DELETE: api/Rounds/5
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
