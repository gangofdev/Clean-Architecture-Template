using CleanArc.Domain.Models.Email;

namespace CleanArc.Domain.Contracts.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message);
        Task SendEmailAsync(EmailMessage message, List<EmailAttachment> attachments);
    }
}
