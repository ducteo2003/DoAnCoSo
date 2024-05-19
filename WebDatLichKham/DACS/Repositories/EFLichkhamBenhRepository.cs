using DACS.Data;
using DACS.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS.Repositories
{
    public class EFLichkhamBenhRepository : ILichKhamBenhRepository
    {
        public readonly ApplicationDbContext _context;
        public EFLichkhamBenhRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(DatLichKham lichKhamBenh)
        {
            _context.DatLichKhams.Add(lichKhamBenh);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lichKhamBenh = await _context.DatLichKhams.FirstOrDefaultAsync(p => p.Id == id);
            _context.DatLichKhams.Remove(lichKhamBenh);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DatLichKham>> GetAllAsync()
        {
            return await _context.DatLichKhams.ToListAsync();
        }

        public async Task<DatLichKham> GetByIdAsync(int id)
        {
            return await _context.DatLichKhams.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(DatLichKham lichKhamBenh)
        {
            _context.DatLichKhams.Update(lichKhamBenh);
            await _context.SaveChangesAsync();
        }
    }
}
