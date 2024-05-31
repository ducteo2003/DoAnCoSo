using DACS.Models;

namespace DACS.Models
{
    public class DatLichKham
    {
        public int Id { get; set; }
        public int? BenhNhanId { get; set; }
        public int? LichLamViecId { get; set; }
        public DateTime NgayDatLich { get; set; }
        public string TrangThai { get; set; }
        public BenhNhan? BenhNhan { get; set; }
        public LichLamViec? LichLamViec { get; set; }
        public ICollection<ChiTietDatLichKham>? ChiTietDatLichKhams { get; set; }
    }
}
