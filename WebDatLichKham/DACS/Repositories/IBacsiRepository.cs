using DACS.Models;

namespace DACS.Repositories
{
    public interface IBacsiRepository
    {
        Task<IEnumerable<BacSi>> GetAllAsync();
        Task<BacSi> GetByIdAsync(int id);
        Task AddAsync(BacSi bacSi);
        Task UpdateAsync(BacSi bacSi);
        Task DeleteAsync(int id);
    }
}
