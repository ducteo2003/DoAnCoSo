using DACS.Data;
using Microsoft.EntityFrameworkCore;
using WebDatLichKham.Models;

namespace WebDatLichKham.Repositories
{
    public class EFDieutriRepository: IDieutriRepository
    {
        public readonly ApplicationDbContext _context;
        public EFDieutriRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(DieuTri dieuTri)
        {

            _context.dieuTris.Add(dieuTri);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dieuTri = await _context.dieuTris.FirstOrDefaultAsync(p => p.Id == id);
            _context.dieuTris.Remove(dieuTri);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DieuTri>> GetAllAsync()
        {
            return await _context.dieuTris.ToListAsync();
        }

        public async Task<DieuTri> GetByIdAsync(int id)
        {
            return await _context.dieuTris.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(DieuTri dieuTri)
        {
            _context.dieuTris.Update(dieuTri);
            await _context.SaveChangesAsync();
        }
    }
}
