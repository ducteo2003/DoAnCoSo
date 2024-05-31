    using static System.Net.Mime.MediaTypeNames;

    namespace DACS.Models
    {
        public class BenhNhan
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string GioiTinh { get; set; }
            public DateTime NgaySinh { get; set; }
            public string DiaChi { get; set; }
            public string SoDienThoai { get; set; }
            public string? ImageUrl { get; set; }
            public string TinhTrang { get; set; }
            public string Gmail { get; set; }
            public string? ChuanDoan { get; set; }
            public string? DieuTri {  get; set; }
            public virtual ICollection<Image> Images { get; set; } = new List<Image>();
            public virtual ICollection<DatLichKham> LichKhamBenhs { get; set; }
        }
    }
