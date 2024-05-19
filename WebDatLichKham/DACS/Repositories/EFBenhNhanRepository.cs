using DACS.Data;
using DACS.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS.Repositories
{
    public class EFBenhNhanRepository : IBenhNhanRepository
    {
        public readonly ApplicationDbContext _context;
        public EFBenhNhanRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(BenhNhan benhNhan)
        {
            _context.BenhNhans.Add(benhNhan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var benhnhan = await _context.BenhNhans.FirstOrDefaultAsync(p => p.Id == id);
            _context.BenhNhans.Remove(benhnhan);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BenhNhan>> GetAllAsync()
        {
            return await _context.BenhNhans.ToListAsync();
        }

        public async Task<BenhNhan> GetByIdAsync(int? id)
        {
            return await _context.BenhNhans.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(BenhNhan benhNhan)
        {
            _context.BenhNhans.Update(benhNhan);
            await _context.SaveChangesAsync();
        }
    }
}
