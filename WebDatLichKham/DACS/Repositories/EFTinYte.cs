using DACS.Data;
using DACS.Models;
using Microsoft.EntityFrameworkCore;

namespace WebDatLichKham.Repositories
{
    public class EFTinYte: Itinyte
    {
        public readonly ApplicationDbContext _context;
        public EFTinYte(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TinYTe tinYTe)
        {
            _context.TinYTes.Add(tinYTe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tinYTe = await _context.TinYTes.FirstOrDefaultAsync(p => p.Id == id);
            _context.TinYTes.Remove(tinYTe);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TinYTe>> GetAllAsync()
        {
            return await _context.TinYTes.ToListAsync();
        }

        public async Task<TinYTe> GetByIdAsync(int id)
        {
            return await _context.TinYTes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(TinYTe tinYTe)
        {
            _context.TinYTes.Update(tinYTe);
            await _context.SaveChangesAsync();
        }
    }
}

