using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FallGuyStats.Data;
using FallGuyStats.Objects.Models;

namespace FallGuyStats
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        private readonly FallGuysContext _context;

        public UserSettingsController(FallGuysContext context)
        {
            _context = context;
        }

        // GET: api/UserSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSettingsModel>>> GetUserSettings()
        {
            return await _context.UserSettings.ToListAsync();
        }

        // GET: api/UserSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSettingsModel>> GetUserSettingsModel(int id)
        {
            var userSettingsModel = await _context.UserSettings.FindAsync(id);

            if (userSettingsModel == null)
            {
                return NotFound();
            }

            return userSettingsModel;
        }

        // PUT: api/UserSettings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSettingsModel(int id, UserSettingsModel userSettingsModel)
        {
            if (id != userSettingsModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(userSettingsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSettingsModelExists(id))
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

        // POST: api/UserSettings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserSettingsModel>> PostUserSettingsModel(UserSettingsModel userSettingsModel)
        {
            _context.UserSettings.Add(userSettingsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSettingsModel", new { id = userSettingsModel.Id }, userSettingsModel);
        }

        // DELETE: api/UserSettings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserSettingsModel>> DeleteUserSettingsModel(int id)
        {
            var userSettingsModel = await _context.UserSettings.FindAsync(id);
            if (userSettingsModel == null)
            {
                return NotFound();
            }

            _context.UserSettings.Remove(userSettingsModel);
            await _context.SaveChangesAsync();

            return userSettingsModel;
        }

        private bool UserSettingsModelExists(int id)
        {
            return _context.UserSettings.Any(e => e.Id == id);
        }
    }
}
