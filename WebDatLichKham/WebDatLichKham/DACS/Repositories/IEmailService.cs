using WebDatLichKham.Models;

namespace WebDatLichKham.Repositories
{
    public interface IEmailService
    {
        Task<bool> send(EmailSender emailsetting);
    }
}