using DACS.Models;
using System.ComponentModel.DataAnnotations;
namespace WebDatLichKham.Models
{
    public class ChanDoan
    {
        public int Id { get; set; }

        public int KhoaId { get; set; }
        public string MoTaChanDoan { get; set; }

        public Khoa Khoa { get; set; }
        public DieuTri DieuTri { get; set; }
    }
}
