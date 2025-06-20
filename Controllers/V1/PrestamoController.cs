using Microsoft.AspNetCore.Mvc;
using MiMangaBot.Features.Prestamos;
using MiMangaBot.Models;

namespace MiMangaBot.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PrestamosController : ControllerBase
    {
        private readonly PrestamoService _service;

        public PrestamosController(PrestamoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetAll([FromQuery] int page = 1)
        {
            const int pageSize = 100;

            if (page < 1)
                return BadRequest(new { Message = "El número de página debe ser mayor o igual a 1." });

            var result = await _service.GetPaginatedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamo>> GetById(int id)
        {
            var prestamo = await _service.GetByIdAsync(id);
            if (prestamo == null) return NotFound();
            return Ok(prestamo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Prestamo prestamo)
        {
            var success = await _service.AddAsync(prestamo);
            if (!success)
                return BadRequest("No se pudo crear el préstamo. ¿El id del manga existe?");
            return Ok(prestamo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Prestamo prestamo)
        {
            if (id != prestamo.IdPrestamo)
                return BadRequest("ID no coincide");

            var success = await _service.UpdateAsync(prestamo);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
