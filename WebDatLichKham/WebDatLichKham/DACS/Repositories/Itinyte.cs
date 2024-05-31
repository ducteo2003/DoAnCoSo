using DACS.Models;

namespace WebDatLichKham.Repositories
{
    public interface  Itinyte
    {
        Task<IEnumerable<TinYTe>> GetAllAsync();
        Task<TinYTe> GetByIdAsync(int id);
        Task AddAsync(TinYTe lichKhamBenh);
        Task UpdateAsync(TinYTe lichKhamBenh);
        Task DeleteAsync(int id);
    }
}
