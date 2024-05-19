using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class ChucVu
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public virtual ICollection<BacSi> BacSis { get; set; }
    }
}
