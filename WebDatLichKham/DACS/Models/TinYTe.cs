using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class TinYTe
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string TieuDe { get; set; }
        public string? ImageUrl { get; set; }

        public List<Image>? Images { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayDang { get; set; }
    }
}
