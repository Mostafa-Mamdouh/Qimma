using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;

using PRJ.Application.DTOs.Common;
using PRJ.DataAccess.MSSQL;
using rep = PRJ.Repository;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business
{
    public class NotificationManager
    {

        private readonly MailSettings _mailSettings;
        private readonly IConfiguration _env;

        public NotificationManager(IOptions<MailSettings> mailSettings, IConfiguration configuration)
        {
            _mailSettings = mailSettings.Value;
            _env = configuration;
        }


        public async Task<wap.Response<bool>> SendEmailAsync(MailRequest mailRequest)
        {
            
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
                email.Sender.Name = _mailSettings.DisplayName;
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                if (mailRequest.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in mailRequest.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, MimeKit.ContentType.Parse(file.ContentType));
                        }
                    }
                }
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.CheckCertificateRevocation = false;
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, true);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return new wap.Response<bool>() { Data = true };
           

        }




    }
}
