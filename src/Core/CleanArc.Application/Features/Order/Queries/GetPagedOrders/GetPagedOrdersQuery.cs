namespace CleanArc.Application.Features.Order.Queries.GetPagedOrders;

public record GetPagedOrdersQuery() : PagedRequest, IRequest<OperationResult<GetPagedOrdersQueryResult>>
{

}
