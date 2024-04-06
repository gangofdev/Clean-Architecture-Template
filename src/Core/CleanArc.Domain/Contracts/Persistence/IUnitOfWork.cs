namespace CleanArc.Domain.Contracts.Persistence;

public interface IUnitOfWork
{
    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    public IOrderRepository OrderRepository { get; }
    Task CommitAsync();
    ValueTask RollBackAsync();
}