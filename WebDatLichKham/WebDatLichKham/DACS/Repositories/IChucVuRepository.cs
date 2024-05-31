using DACS.Models;

namespace DACS.Repositories
{
    public interface IChucVurepository
    {
        Task<IEnumerable<ChucVu>> GetAllAsync();
        Task<ChucVu> GetByIdAsync(int id);
        Task AddAsync(ChucVu chucVu);
        Task UpdateAsync(ChucVu chucVu);
        Task DeleteAsync(int id);
    }
}
