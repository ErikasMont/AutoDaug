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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AdvertType>>> GetAdvertType()
        {
            return await _context.AdvertTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertType>> GetAdvertType(int id)
        {
            var advertType = await _context.AdvertTypes.FindAsync(id);

            if (advertType == null)
            {
                return NotFound();
            }

            return advertType;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            return Ok(advertType);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AdvertType>> PostAdvertType(AdvertType advertType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.AdvertTypes.Add(advertType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdvertType", new { id = advertType.Id }, advertType);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
