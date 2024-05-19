using DACS.Data;
using DACS.Models;
using DACS.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Repositories
{
    public class EFChuyenKhoaRepository: IChuyenKhoaRepository
    {
        public readonly ApplicationDbContext _context;
        public EFChuyenKhoaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Khoa chuyenKhoa)
        {
            _context.Khoas.Add(chuyenKhoa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chuyenKhoa = await _context.Khoas.FirstOrDefaultAsync(p => p.Id == id);
            _context.Khoas.Remove(chuyenKhoa);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Khoa>> GetAllAsync()
        {
            return await _context.Khoas.ToListAsync();
        }

        public async Task<Khoa> GetByIdAsync(int id)
        {
            return await _context.Khoas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Khoa chuyenKhoa)
        {
            _context.Khoas.Update(chuyenKhoa);
            await _context.SaveChangesAsync();
        }
    }
}
