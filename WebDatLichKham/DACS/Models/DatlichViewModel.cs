using DACS.Models;

namespace WebDatLichKham.Models
{
    public class DatlichViewModel
    {
        public IEnumerable<BacSi> BacSis { get; set; }
        public IEnumerable<TinYTe> TinYtes { get; set; }
    }
}
