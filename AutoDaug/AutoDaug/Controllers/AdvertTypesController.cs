using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoDaug.DataContext;
using AutoDaug.Models;
using AutoDaug.Auth;
using AutoDaug.Requests;

namespace AutoDaug.Controllers
{
    [Route("api/advertTypes")]
    [ApiController]
    public class AdvertTypesController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public AdvertTypesController(ApiDbContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<AdvertType>>> GetAdvertType()
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            return _context.AdvertTypes.Any() ? Ok(await _context.AdvertTypes.ToListAsync()) : NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertType>> GetAdvertType(int id)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var advertType = await _context.AdvertTypes.FindAsync(id);

            if (advertType == null)
            {
                return NotFound();
            }

            return advertType;
        }

        [HttpGet("{id}/adverts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Advert>>> GetAdvertsByAdvertType(int id)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            if (id == null)
            {
                return NotFound();
            }
            var adverts = await _context.Adverts.Where(advert => advert.AdvertType_Id == id).ToListAsync();
            if (adverts.Count == 0)
            {
                return NotFound();
            }

            return Ok(adverts);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutAdvertType(int id, AdvertTypeDto advertType)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, true);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var foundType = await _context.AdvertTypes.FirstOrDefaultAsync(x => x.Id == id);
            if(foundType == null)
            {
                return NotFound("Advert type is not existant");
            }

            foundType.Name = advertType.Name;
            foundType.Description = advertType.Description;

            _context.AdvertTypes.Update(foundType);
            await _context.SaveChangesAsync();

            return Ok(advertType);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AdvertType>> PostAdvertType(AdvertType advertType)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, true);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAdvertType(int id)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, true);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var advertType = await _context.AdvertTypes.FindAsync(id);
            if (advertType == null)
            {
                return NotFound();
            }

            if(_context.Adverts.Any(advert => advert.Id == advertType.Id))
            {
                return Conflict("The advert type you are trying to delete still has adverts");
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
