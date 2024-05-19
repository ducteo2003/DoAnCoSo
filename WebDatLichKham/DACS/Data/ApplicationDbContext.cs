using DACS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebDatLichKham.Models;

namespace DACS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BacSi> BacSis { get; set; }
        public DbSet<BenhNhan> BenhNhans { get; set; }
        public DbSet<ChiTietDatLichKham> ChiTietDatLichKhams { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<DatLichKham> DatLichKhams { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<LichLamViec> LichLamViecs { get; set; }
        public DbSet<TinYTe> TinYTes { get; set; }
    }
}
