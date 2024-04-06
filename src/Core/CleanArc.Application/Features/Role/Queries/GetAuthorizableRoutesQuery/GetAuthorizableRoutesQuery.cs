namespace CleanArc.Application.Features.Role.Queries.GetAuthorizableRoutesQuery;

public record GetAuthorizableRoutesQuery():IRequest<OperationResult<List<GetAuthorizableRoutesQueryResponse>>>;