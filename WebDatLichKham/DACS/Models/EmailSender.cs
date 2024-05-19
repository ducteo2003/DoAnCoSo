namespace WebDatLichKham.Models
{
    public class EmailSender
    {
        public string To {  get; set; } 
        public string name { get; set; }
        public string subject { get; set; }
        public string conTent {  get; set; }
        public List<string>  CC { get; set; } = new List<string>();

        public List<string> AttachmentFiles { get; set; } = new List<string>();
    }
}
