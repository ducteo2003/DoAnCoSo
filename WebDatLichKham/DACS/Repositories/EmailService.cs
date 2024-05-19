using Microsoft.EntityFrameworkCore.Metadata.Internal;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using WebDatLichKham.Helper;
using WebDatLichKham.Models;

namespace WebDatLichKham.Repositories
{
    public class EmailService : IEmailService
    {
        private readonly string _senderEmail;
        private readonly string _senderName;
        private readonly string _key;
        public EmailService(IConfiguration configuration)
        {
            var config = configuration.GetSection("EmailBrevo");
            _senderEmail = config["Sender:Email"] ?? string.Empty;
            _senderName = config["Sender:Name"] ?? string.Empty;
            _key = config["key"] ?? string.Empty;
            if (!sib_api_v3_sdk.Client.Configuration.Default.ApiKey.ContainsKey("api-key"))
            {
                sib_api_v3_sdk.Client.Configuration.Default.ApiKey.Add("api-key", _key);
            }

           
        }
        public async Task<bool> send(EmailSender emailsetting)
        {
            try
            {
                var transaction = new TransactionalEmailsApi();
                var sender = new SendSmtpEmailSender(_senderName,_senderEmail);
                var to = new List<SendSmtpEmailTo>{
              new  (emailsetting.To, emailsetting.name)
        };
                string body = emailsetting.conTent;
                List<SendSmtpEmailCc> lsCC = null;
                if (emailsetting.CC.Any())
                {
                    lsCC = new List<SendSmtpEmailCc>();
                    foreach (var cc in emailsetting.CC)
                    {
                        lsCC.Add(new SendSmtpEmailCc { Email = cc });
                    }
                }
                List<SendSmtpEmailAttachment> LsAttachments = null;
                if (emailsetting.AttachmentFiles.Any())
                {
                    foreach (var parthFile in emailsetting.AttachmentFiles)
                    {
                        LsAttachments.Add(new SendSmtpEmailAttachment { Url = parthFile });
                    }
                }
                var sendEmail = new SendSmtpEmail(sender, to, null, lsCC, body, null, emailsetting.subject, null, LsAttachments);
                CreateSmtpEmail result = await transaction.SendTransacEmailAsync(sendEmail);
                return result is not null ? true : false;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}
