namespace CleanArc.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<OperationResult<List<GetUsersQueryResponse>>>;