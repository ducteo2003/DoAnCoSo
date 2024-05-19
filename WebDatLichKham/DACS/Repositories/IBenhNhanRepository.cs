using DACS.Models;

namespace DACS.Repositories
{
    public interface IBenhNhanRepository
    {
        Task<IEnumerable<BenhNhan>> GetAllAsync();
        Task<BenhNhan> GetByIdAsync(int? id);
        Task AddAsync(BenhNhan benhNhan);
        Task UpdateAsync(BenhNhan benhNhan);
        Task DeleteAsync(int id);
    }
}
