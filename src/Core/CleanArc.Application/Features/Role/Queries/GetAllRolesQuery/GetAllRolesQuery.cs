namespace CleanArc.Application.Features.Role.Queries.GetAllRolesQuery;

public record GetAllRolesQuery():IRequest<OperationResult<List<GetAllRolesQueryResponse>>>;