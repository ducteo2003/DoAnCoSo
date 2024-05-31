using System.ComponentModel.DataAnnotations;

namespace WebDatLichKham.Models
{
    public class DieuTri
    {
        public int Id { get; set; }
        public int ChanDoanId { get; set; }
        public string MoTaDieuTri { get; set; }
        public ChanDoan ChanDoan { get; set; }
    }
}
