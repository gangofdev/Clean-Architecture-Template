using CleanArc.Domain.Models.Email;
using CleanArc.SharedKernel.Common;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CleanArc.Infrastructure.Email.MailKit
{
    public class MailKitMailSender : IMailKitMailSender
    {
        private readonly EmailSettings _config;
        public MailKitMailSender(IOptions<EmailSettings> mailKitConfig) 
        {
            ArgumentNullException.ThrowIfNull(mailKitConfig, nameof(IOptions<EmailSettings>));
            ArgumentNullException.ThrowIfNull(mailKitConfig.Value, nameof(EmailSettings));
            _config = mailKitConfig.Value;
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(_config.Host, _config.Port, _config.UseSSL);
            MimeMessage mimeMessage = new();
            mimeMessage.From.Add(new MailboxAddress(_config.DefaultFromDisplayName, _config.DefaultFromAddress));
            mimeMessage.To.Add(new MailboxAddress(message.To, message.To));
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(message.Body);
            await client.AuthenticateAsync(_config.Username, _config.Password);
            var result = await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }

        public async Task SendEmailAsync(EmailMessage message, List<EmailAttachment> emailAttachments)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_config.Host, _config.Port, _config.UseSSL);
                MimeMessage mimeMessage = new();
                mimeMessage.From.Add(new MailboxAddress(_config.DefaultFromDisplayName, _config.DefaultFromAddress));
                mimeMessage.To.Add(new MailboxAddress(message.To, message.To));
                mimeMessage.Subject = message.Subject;
                var body = new TextPart(message.Body);
                var multipart = new Multipart("mixed")
                {
                    body
                };
                foreach (var emailAttachment in emailAttachments)
                {
                    var attachment = new MimePart(emailAttachment.MediaType, emailAttachment.MediaSubType)
                    {
                        FileName = emailAttachment.Name,
                        Content = new MimeContent(File.OpenRead(emailAttachment.FilePath)),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                    };
                    multipart.Add(attachment);
                }
                client.Authenticate(_config.Username, _config.Password);

                var result = await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
