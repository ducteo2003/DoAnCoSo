using System.ComponentModel.DataAnnotations;
using WebDatLichKham.Models;

namespace DACS.Models
{
    public class Khoa

    {
        
        public int Id { get; set; }
     
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Image> Images { get; set; }

        public ICollection<BacSi> BacSis { get; set; }
        public ICollection<ChanDoan> ChanDoan { get; set;}

    }

}
