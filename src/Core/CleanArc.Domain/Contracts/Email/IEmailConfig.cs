namespace CleanArc.Domain.Contracts.Email
{
    public interface IEmailConfig
    {
        string DefaultFromAddress { get; }
        string DefaultFromDisplayName { get; }
    }
}
