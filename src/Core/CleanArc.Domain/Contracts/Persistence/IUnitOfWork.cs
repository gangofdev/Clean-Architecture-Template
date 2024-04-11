using CleanArc.Domain.Common;

namespace CleanArc.Domain.Contracts.Persistence;

public interface IUnitOfWork
{
    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    public IOrderRepository OrderRepository { get; }
    IGenericRepository<T> GetGenericRepository<T>() where T : BaseEntity;
    Task CommitAsync();
    ValueTask RollBackAsync();
}