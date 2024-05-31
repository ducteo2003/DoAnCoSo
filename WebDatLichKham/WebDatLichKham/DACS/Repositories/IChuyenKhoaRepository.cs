using DACS.Models;

namespace DACS.Repositories
{
    public interface IChuyenKhoaRepository
    {
        Task<IEnumerable<Khoa>> GetAllAsync();
        Task<Khoa> GetByIdAsync(int id);
        Task AddAsync(Khoa chuyenKhoa);
        Task UpdateAsync(Khoa chuyenKhoa);
        Task DeleteAsync(int id);
    }
}
