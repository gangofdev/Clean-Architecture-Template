namespace CleanArc.Domain.Models.Email
{
    public class EmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public EmailMessage(string from, string to, string message, string body)
        {
            From = from;
            To = to;
            Subject = message;
            Body = body;
        }
    }
}
