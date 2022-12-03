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
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var cars = await _context.Cars.ToListAsync();

            if (cars.Count == 0)
            {
                return NotFound("No cars found");
            }

            return Ok(cars);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

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
        public async Task<IActionResult> PutCar(int id, CarDto car)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var foundCar = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if(foundCar == null)
            {
                return NotFound("The car does not exist");
            }

            if (authUser.Role != "admin" && authUser.UserId != foundCar.User_Id)
            {
                return StatusCode(403);
            }

            foundCar.Make = car.Make;
            foundCar.Model = car.Model;
            foundCar.ManufactureDate = car.ManufactureDate;
            foundCar.Engine = car.Engine;
            foundCar.Color = car.Color;
            foundCar.Gearbox = car.Gearbox;
            foundCar.GasType = car.GasType;
            foundCar.Advert_Id = car.Advert_Id;
            foundCar.Milage = car.Milage;

            _context.Cars.Update(foundCar);
            await _context.SaveChangesAsync();

            return Ok(car);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Car>> PostCar(Car car)
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
            var authUser = _jwtTokenService.ParseUser(Request.Headers.Authorization, false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            if (authUser.Role != "admin" && authUser.UserId != car.User_Id)
            {
                return StatusCode(403);
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
