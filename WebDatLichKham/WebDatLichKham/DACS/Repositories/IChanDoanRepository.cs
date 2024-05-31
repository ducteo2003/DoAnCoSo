using DACS.Models;
using WebDatLichKham.Models;

namespace WebDatLichKham.Repositories
{
    public interface IChanDoanRepository
    {
        Task<IEnumerable<ChanDoan>> GetAllAsync();
        Task<ChanDoan> GetByIdAsync(int id);
        Task AddAsync(ChanDoan chanDoan);
        Task UpdateAsync(ChanDoan chanDoan);
        Task DeleteAsync(int id);
    }
}
