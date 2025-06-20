using Microsoft.EntityFrameworkCore;
using MiMangaBot.Data; // Usa tu contexto real
using MiMangaBot.Models;

namespace MiMangaBot.Features.Prestamos
{
    public class PrestamoService
    {
        private readonly ApplicationDbContext _context;

        public PrestamoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Prestamo>> GetAllAsync()
        {
            return await _context.Prestamos.ToListAsync();
        }
        public async Task<List<Prestamo>> GetPaginatedAsync(int page, int pageSize)
        {
            return await _context.Prestamos
                .OrderBy(p => p.IdPrestamo) // Asegúrate que el nombre sea correcto
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<Prestamo?> GetByIdAsync(int id)
        {
            return await _context.Prestamos.FindAsync(id);
        }

        public async Task<bool> AddAsync(Prestamo prestamo)
        {
            var exists = await _context.Mangas.AnyAsync(m => m.id == prestamo.IdManga);
            if (!exists) return false;

            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Prestamo prestamo)
        {
            var exists = await _context.Prestamos.AnyAsync(p => p.IdPrestamo == prestamo.IdPrestamo);
            if (!exists) return false;

            _context.Prestamos.Update(prestamo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null) return false;

            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
