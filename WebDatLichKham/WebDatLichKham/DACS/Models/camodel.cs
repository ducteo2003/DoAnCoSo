using DACS.Models;

namespace WebDatLichKham.Models
{
    public class camodel
    {
        public List<LichLamViec> Schedules { get; set; }

        // Danh sách các ca đã được đặt
        public List<TimeOnly> BookedTimes { get; set; }
    }
}
