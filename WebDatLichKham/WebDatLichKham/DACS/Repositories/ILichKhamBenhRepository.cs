using DACS.Models;

namespace DACS.Repositories
{
    public interface ILichKhamBenhRepository
    {
        Task<IEnumerable<DatLichKham>> GetAllAsync();
        Task<DatLichKham> GetByIdAsync(int id);
        Task AddAsync(DatLichKham lichKhamBenh);
        Task UpdateAsync(DatLichKham lichKhamBenh);
        Task DeleteAsync(int id);
    }
}
