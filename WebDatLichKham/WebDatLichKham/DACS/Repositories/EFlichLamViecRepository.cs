using DACS.Data;
using DACS.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace DACS.Repositories
{
    public class EFlichLamViecRepository :ILichLamViecRepository
    {
        private readonly ApplicationDbContext _context;
        public EFlichLamViecRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LichLamViec>> GetAllAsync()
        {
            return await _context.LichLamViecs.Include(p=>p.BacSi).ToListAsync();
        }
        public async Task<LichLamViec> GetByIdAsync(int? id)
        {
            return await _context.LichLamViecs.Include(p=>p.BacSi).FirstOrDefaultAsync(p=>p.Id == id);
        }
        public async Task AddAsync(LichLamViec lichLamViec)
        {
            _context.LichLamViecs.Add(lichLamViec);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lichLam = await _context.LichLamViecs.FirstOrDefaultAsync(p => p.Id == id);
            _context.LichLamViecs.Remove(lichLam);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(LichLamViec lichLamViec)
        {
            _context.LichLamViecs.Update(lichLamViec);
            await _context.SaveChangesAsync();
        }
    }
}
