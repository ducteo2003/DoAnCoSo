using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class Khoa

    {
        
        public int Id { get; set; }
     
        public string Name { get; set; }
    
       
        public ICollection<BacSi> BacSis { get; set; }

    }

}
