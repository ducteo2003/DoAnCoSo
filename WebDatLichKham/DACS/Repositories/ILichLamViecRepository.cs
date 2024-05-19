using DACS.Models;
using System.ComponentModel;

namespace DACS.Repositories
{
    public interface ILichLamViecRepository
    {
        Task<IEnumerable<LichLamViec>> GetAllAsync();
        Task<LichLamViec> GetByIdAsync(int? Id);
        Task AddAsync(LichLamViec Lichlamviec);
        Task UpdateAsync(LichLamViec lichLamViec);
        Task DeleteAsync(int Id);
    }
}
