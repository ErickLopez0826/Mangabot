using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiMangaBot.Data;
using MiMangaBot.Models;
using Microsoft.AspNetCore.Authorization;


namespace MiMangaBot.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MangaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MangaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/manga
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manga>>> GetAll([FromQuery] int page = 1)
        {
            const int pageSize = 100;

            if (page < 1)
                return BadRequest(new { Message = "El número de página debe ser mayor o igual a 1." });

            var mangas = await _context.Mangas
                .OrderBy(m => m.id) // Asegúrate que 'Id' coincide con tu modelo
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(mangas);
        }


        // GET: api/v1/manga/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manga>> GetById(int id)
        {
            var manga = await _context.Mangas.FindAsync(id);

            if (manga == null)
                return NotFound(new { Message = "Manga no encontrado." });

            return manga;
        }

        // POST: api/v1/manga
        [HttpPost]
        public async Task<ActionResult<Manga>> Create(Manga manga)
        {
            _context.Mangas.Add(manga);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { Id = manga.id }, manga);
        }

        // PUT: api/v1/manga/5
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, Manga manga)
        {
            if (Id != manga.id)
                return BadRequest(new { Message = "El ID no coincide." });

            _context.Entry(manga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Mangas.Any(m => m.id == Id))
                    return NotFound(new { Message = "Manga no encontrado para actualizar." });

                throw;
            }

            return NoContent();
        }

        // DELETE: api/v1/manga/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var manga = await _context.Mangas.FindAsync(Id);

            if (manga == null)
                return NotFound(new { Message = "Manga no encontrado para eliminar." });

            _context.Mangas.Remove(manga);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
