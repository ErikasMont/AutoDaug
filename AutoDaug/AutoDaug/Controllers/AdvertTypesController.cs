using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoDaug.DataContext;
using AutoDaug.Models;

namespace AutoDaug.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertTypesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public AdvertTypesController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/AdvertTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdvertType>>> GetAdvertType()
        {
            return await _context.AdvertTypes.ToListAsync();
        }

        // GET: api/AdvertTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdvertType>> GetAdvertType(int id)
        {
            var advertType = await _context.AdvertTypes.FindAsync(id);

            if (advertType == null)
            {
                return NotFound();
            }

            return advertType;
        }

        // PUT: api/AdvertTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvertType(int id, AdvertType advertType)
        {
            if (id != advertType.Id)
            {
                return BadRequest();
            }

            _context.Entry(advertType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertTypeExists(id))
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

        // POST: api/AdvertTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdvertType>> PostAdvertType(AdvertType advertType)
        {
            _context.AdvertTypes.Add(advertType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdvertType", new { id = advertType.Id }, advertType);
        }

        // DELETE: api/AdvertTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvertType(int id)
        {
            var advertType = await _context.AdvertTypes.FindAsync(id);
            if (advertType == null)
            {
                return NotFound();
            }

            _context.AdvertTypes.Remove(advertType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdvertTypeExists(int id)
        {
            return _context.AdvertTypes.Any(e => e.Id == id);
        }
    }
}
