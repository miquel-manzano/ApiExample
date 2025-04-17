using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T1_APIREST.Context;
using T1_APIREST.DTO;
using T1_APIREST.Models;

namespace T1_APIREST.Controllers
{
    [Route("/api/Films")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FilmsController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("hi")]
        public IActionResult helloClient()
        {
           return Ok("Helo client");
        }
        // GET: api/Films
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmGetDTO>>> GetFilms()
        {
            var films = await _context.Films
                .Include(f => f.Director)
                .Select(f => new FilmGetDTO
                {
                    Id = f.ID,
                    Name = f.Name,
                    Description = f.Description,
                    DirectorName = $"{f.Director.Name} {f.Director.Surname}"
                })
                .ToListAsync();
            return Ok(films);
        }

        // GET: api/Films/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            return Ok(film);
        }
        // POST: api/Films
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(FilmInsertDTO filmDTO)
        {
            // Verificar si el director existeix
            var director = await _context.Directors.FindAsync(filmDTO.DirectorId);
            if (director == null)
            {
                return NotFound("Director not found.");
            }

            var film = new Film{
                Name = filmDTO.Name,
                Description = filmDTO.Description,
                DirectorID = filmDTO.DirectorId
            };

            try
            {
                await _context.Films.AddAsync(film);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex);
            }


            // return CreatedAtAction(nameof(GetFilm), new { id = film.ID }, film);
            return Ok(film);
        }

        // DELETE: api/Films/5
        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            try
            {
                _context.Films.Remove(film);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex);
            }
            return NoContent();
        }
        // PUT: api/Films/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("put/{id}")]
        public async Task<IActionResult> PutFilm(int id, Film film)
        {
            if (id != film.ID)
            {
                return BadRequest();
            }

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
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

     

        private bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.ID == id);
        }
    }
}
