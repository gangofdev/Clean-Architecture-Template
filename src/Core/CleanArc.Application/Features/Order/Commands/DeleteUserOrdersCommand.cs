namespace CleanArc.Application.Features.Order.Commands;

public record DeleteUserOrdersCommand(int UserId):IRequest<OperationResult<bool>>;