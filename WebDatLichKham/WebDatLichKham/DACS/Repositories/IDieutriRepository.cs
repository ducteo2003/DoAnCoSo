using DACS.Models;
using WebDatLichKham.Models;

namespace WebDatLichKham.Repositories
{
    public interface IDieutriRepository
    {
        Task<IEnumerable<DieuTri>> GetAllAsync();
        Task<DieuTri> GetByIdAsync(int id);
        Task AddAsync(DieuTri chuyenKhoa);
        Task UpdateAsync(DieuTri chuyenKhoa);
        Task DeleteAsync(int id);
    }
}
