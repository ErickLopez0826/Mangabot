using MiMangaBot.Data;
using MiMangaBot.Models;
using Microsoft.EntityFrameworkCore;

namespace MiMangaBot.Services
{
    public class MangaService
    {
        private readonly ApplicationDbContext _context;

        public MangaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manga>> GetAllAsync()
        {
            return await _context.Mangas.ToListAsync();

        }
        public async Task<List<Manga>> GetPaginatedAsync(int page, int pageSize)
        {
            return await _context.Mangas
                .OrderBy(m => m.id) // asegúrate que estás usando la propiedad correcta
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<Manga?> GetByIdAsync(int id)
        {
            return await _context.Mangas.FindAsync(id);
        }

        public async Task<Manga> AddAsync(Manga manga)
        {
            _context.Mangas.Add(manga);
            await _context.SaveChangesAsync();
            return manga;
        }

        public async Task<bool> UpdateAsync(Manga manga)
        {
            var exists = await _context.Mangas.AnyAsync(m => m.id == manga.id);
            if (!exists) return false;

            _context.Mangas.Update(manga);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var manga = await _context.Mangas.FindAsync(id);
            if (manga == null) return false;

            _context.Mangas.Remove(manga);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
