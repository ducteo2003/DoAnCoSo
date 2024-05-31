using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using DACS.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using WebDatLichKham.Models;

namespace DACS.Models
{
    public class BacSi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [StringLength(6)]
        public string GioiTinh { get; set; }
        public int? ChucVuId { get; set; }
        public int? KhoaId { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string? GioiThieu { get; set; }
        public string? KinhNghiem { get; set; }
        public string? HocVan { get; set; }

        public string? ImageUrl { get; set; }

        public  ChucVu? ChucVu { get; set; }
        public  Khoa? Khoa { get; set; }

        public  ICollection<Image> Images { get; set; }
        public  ICollection<ChiTietDatLichKham> ChiTietLichKhamBenhs { get; set; }
        public  ICollection<LichLamViec> LichLamViecs { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

    }
}
