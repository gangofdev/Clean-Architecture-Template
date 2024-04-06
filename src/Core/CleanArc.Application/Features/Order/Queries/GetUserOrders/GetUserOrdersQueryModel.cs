namespace CleanArc.Application.Features.Order.Queries.GetUserOrders;

public record GetUserOrdersQueryModel(int UserId) : IRequest<OperationResult<List<GetUsersQueryResultModel>>>;