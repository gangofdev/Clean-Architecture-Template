using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CleanArc.Domain.Contracts.Persistence;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);
    Task<List<Order>> GetAllUserOrdersAsync(int userId);
    Task<List<Order>> GetAllOrdersWithRelatedUserAsync();
    Task<Order> GetUserOrderByIdAndUserIdAsync(int userId,int orderId,bool trackEntity);
    Task DeleteUserOrdersAsync(int userId);
    Task<PagedResult<Order>> GetPagedOrdersAsync(Expression<Func<Order, bool>> filter = null,
        Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include = null,
        Expression<Func<Order, object>> orderBy = null,
        int pageIndex = 0, int pageSize = 50);


}