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

namespace AutoDaug.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public CarsController(ApiDbContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            var authUser = _jwtTokenService.ParseUser(Request.Cookies["jwt"], false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            return _context.Cars.Any() ? Ok(await _context.Cars.ToListAsync()) : NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Cookies["jwt"], false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Cookies["jwt"], false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(car);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Cookies["jwt"], false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(!_context.Users.Any(x => x.Id == car.User_Id))
            {
                return BadRequest("Given user id is not existant");
            }

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Cookies["jwt"], false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
