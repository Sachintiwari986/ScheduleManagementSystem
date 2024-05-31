
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using ShiftManagementSystem.Models;
using MailKit.Net.Smtp;

namespace ShiftManagementSystem.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IOptions<MailSetting> _appSetting;
        public EmailServices(IOptions<MailSetting> appSetting)
        {
            _appSetting = appSetting;

        }
        public bool SendEmail(string to, string subject, string html)
        {
            try
            {

                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_appSetting.Value.Mail));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = html };

                // send email
                using var smtp = new SmtpClient();

                smtp.Connect(_appSetting.Value.Host, _appSetting.Value.Port, SecureSocketOptions.StartTlsWhenAvailable);
                smtp.Authenticate(_appSetting.Value.Mail, _appSetting.Value.Password);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
