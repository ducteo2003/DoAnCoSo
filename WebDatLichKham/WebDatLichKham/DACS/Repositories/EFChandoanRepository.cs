using DACS.Data;
using DACS.Models;
using Microsoft.EntityFrameworkCore;
using WebDatLichKham.Models;

namespace WebDatLichKham.Repositories
{
    public class EFChandoanRepository : IChanDoanRepository
    {
        public readonly ApplicationDbContext _context;
        public EFChandoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ChanDoan chanDoan)
        {
            
            _context.chanDoans.Add(chanDoan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chanDoan = await _context.chanDoans.FirstOrDefaultAsync(p => p.Id == id);
            _context.chanDoans.Remove(chanDoan);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChanDoan>> GetAllAsync()
        {
            return await _context.chanDoans.ToListAsync();
        }

        public async Task<ChanDoan> GetByIdAsync(int id)
        {
            return await _context.chanDoans.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(ChanDoan chanDoan)
        {
            _context.chanDoans.Update(chanDoan);
            await _context.SaveChangesAsync();
        }
    }
}
