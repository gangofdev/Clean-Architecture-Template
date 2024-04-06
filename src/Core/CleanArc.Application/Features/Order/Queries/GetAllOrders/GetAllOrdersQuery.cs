namespace CleanArc.Application.Features.Order.Queries.GetAllOrders;

public record GetAllOrdersQuery():IRequest<OperationResult<List<GetAllOrdersQueryResult>>>;