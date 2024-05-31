using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? BacSiId { get; set; }
        public int? KhoaId { get; set; }
        public string ImageUrl { get; set; }
        public int? BenhNhanId { get; set; }
        public BacSi? BacSi { get; set; }
        public BenhNhan? BenhNhan { get; set; }
        public Khoa? khoa { get; set; }
    }
}
