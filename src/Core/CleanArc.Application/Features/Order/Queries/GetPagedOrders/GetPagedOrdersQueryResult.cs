using CleanArc.Domain.Entities.Order;
using CleanArc.Domain.Models;

namespace CleanArc.Application.Features.Order.Queries.GetPagedOrders;

public class GetPagedOrdersQueryResult : PagedResult<OrderInfo>
{
    public GetPagedOrdersQueryResult()
    {
        
    }
    public GetPagedOrdersQueryResult(PagedResult<OrderInfo> result)
    {
        this.Page=result.Page;
        this.PageCount=result.PageCount;
        this.PageIndex=result.PageIndex;
        this.PageSize=result.PageSize;
        this.TotalCount=result.TotalCount;
        
    }
}