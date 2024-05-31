using DACS.Models;
using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class LichLamViec
    {
        
        public int Id { get; set; }
        public int? BacSiId { get; set; }
        public DateTime Lich { get; set; }
        public TimeOnly Ca { get; set; }
        //public string CaType { get; set; }
        public BacSi? BacSi { get; set; }
        public ICollection<DatLichKham> DatLichKhamBenhs { get; set; }
    }
}
