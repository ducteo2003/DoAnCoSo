using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DACS.Models
{
    public class ChiTietDatLichKham
    {
        public int Id  { get; set; }
        public int? DatLichKhamId { get; set; }
        public int? BacSiId { get; set; }
        public string GhiChu { get; set; }
        public DatLichKham? DatLichKham { get; set; }
        public BacSi? BacSi { get; set; }
        
    }
}
