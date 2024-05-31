using DACS.Data;
using DACS.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS.Repositories
{
    public class EFBacsiRepository : IBacsiRepository
    {
        public readonly ApplicationDbContext _context;
        public EFBacsiRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(BacSi bacsi)
        {
            _context.BacSis.Add(bacsi);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bacsi = await _context.BacSis.FirstOrDefaultAsync(p=>p.Id == id);
            _context.BacSis.Remove(bacsi);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BacSi>> GetAllAsync()
        {
            return await _context.BacSis.Include(p => p.Khoa).ToListAsync();
        }

        public async Task<BacSi> GetByIdAsync(int id)
        {
            return await _context.BacSis.Include(p => p.Khoa).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(BacSi bacsi)
        {
            _context.BacSis.Update(bacsi);
            await _context.SaveChangesAsync();
        }
    }
}
