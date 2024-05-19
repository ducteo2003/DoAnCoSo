using DACS.Data;
using DACS.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS.Repositories
{
    public class EFChucvuRepository : IChucVurepository
    {
        public readonly ApplicationDbContext _context;
        public EFChucvuRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ChucVu chucVu)
        {
            _context.ChucVus.Add(chucVu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chucVu = await _context.ChucVus.FirstOrDefaultAsync(p => p.Id == id);
            _context.ChucVus.Remove(chucVu);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChucVu>> GetAllAsync()
        {
            return await _context.ChucVus.ToListAsync();
        }

        public async Task<ChucVu> GetByIdAsync(int id)
        {
            return await _context.ChucVus.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(ChucVu chucVu)
        {
            _context.ChucVus.Update(chucVu);
            await _context.SaveChangesAsync();
        }
    }
}
