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
using System.Net;
using AutoDaug.Requests;

namespace AutoDaug.Controllers
{
    [Route("api/adverts")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public AdvertsController(ApiDbContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Advert>>> GetAdvert()
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var adverts = authUser.Role == "admin" ? await _context.Adverts.ToListAsync() :
                await _context.Adverts.Where(x => x.User_Id == authUser.UserId).ToListAsync();

            if (adverts.Count == 0)
            {
                return NotFound("No adverts found");
            }

            return Ok(adverts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Advert>> GetAdvert(int id)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var advert = await _context.Adverts.FindAsync(id);

            if (advert == null)
            {
                return NotFound();
            }

            return advert;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutAdvert(int id, AdvertDto advert)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var foundAdvert = await _context.Adverts.FirstOrDefaultAsync(x => x.Id == id);
            if (foundAdvert == null)
            {
                return NotFound("The advert does not exist");
            }

            if (authUser.Role != "admin" && authUser.UserId != foundAdvert.User_Id)
            {
                return StatusCode(403);
            }

            foundAdvert.Price = advert.Price;
            foundAdvert.Name = advert.Name;
            foundAdvert.Description = advert.Description;
            foundAdvert.AdvertType_Id = advert.AdvertType_Id;

            _context.Adverts.Update(foundAdvert);
            await _context.SaveChangesAsync();

            return Ok(advert);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Advert>> PostAdvert(Advert advert)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(!_context.AdvertTypes.Any(x => x.Id == advert.AdvertType_Id) || !_context.Users.Any(x => x.Id == advert.User_Id))
            {
                return BadRequest("Given user id or advert type id is not existant");
            }

            _context.Adverts.Add(advert);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdvert", new { id = advert.Id }, advert);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAdvert(int id)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var advert = await _context.Adverts.FindAsync(id);
            if (advert == null)
            {
                return NotFound();
            }

            if (authUser.Role != "admin" && authUser.UserId != advert.User_Id)
            {
                return StatusCode(403);
            }

            _context.Adverts.Remove(advert);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdvertExists(int id)
        {
            return _context.Adverts.Any(e => e.Id == id);
        }
    }
}
