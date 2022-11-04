﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoDaug.DataContext;
using AutoDaug.Models;
using AutoDaug.Responses;
using AutoDaug.Requests;
using AutoDaug.Auth;
using BCryptNet = BCrypt.Net.BCrypt;

namespace AutoDaug.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public UsersController(ApiDbContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var authUser = _jwtTokenService.ParseUser(Request.Cookies["jwt"], true);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            return _context.Users.Any() ? Ok(await _context.Users.ToListAsync()) : NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Cookies["jwt"], false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Cookies["jwt"], false);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Register(RegisterRequest request)
        {
            var sameName = _context.Users.FirstOrDefault(e => e.Username.Equals(request.Username));
            if (sameName != null)
                return Conflict();

            var user = new User
            {
                Username = request.Username,
                Password = BCryptNet.HashPassword(request.Password),
                IsAdmin = false,
                AccountState = "Not confirmed",
                PhoneNumber = request.PhoneNumber
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPost("confirm/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Confirm(int id)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Cookies["jwt"], true);

            if(authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            if (!UserExists(id))
            {
                return BadRequest();
            }

            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            user.AccountState = "Confirmed";
            user.IsAdmin = false;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponse>> Token(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var foundUser = await _context.Users.FirstOrDefaultAsync(user => user.Username == request.Username);
            // Check if password and username matches and if state is confirmed
            if (foundUser == null || !BCryptNet.Verify(request.Password, foundUser.Password))
            {
                return BadRequest("Invalid credentials!");
            }

            if(foundUser.AccountState == "Not confirmed")
            {
                return BadRequest("Account has not yet been confirmed");
            }

            var jwt = _jwtTokenService.Generate(foundUser.Id, foundUser.IsAdmin);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });
            var response = new LoginResponse { Username = request.Username, Password = request.Password, Token = jwt, IsAdmin = false };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var authUser = _jwtTokenService.ParseUser(Request.Cookies["jwt"], true);

            if (authUser.Error != null)
            {
                return Unauthorized(authUser.Error);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if(_context.Cars.Any(car => car.User_Id == user.Id))
            {
                return Conflict("User has cars, can't delete");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok("Logged out");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
