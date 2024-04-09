
namespace CleanArc.SharedKernel.Common
{
    public class EmailSettings
    {
        public string DefaultFromAddress { get; set; }
        public string DefaultFromDisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
