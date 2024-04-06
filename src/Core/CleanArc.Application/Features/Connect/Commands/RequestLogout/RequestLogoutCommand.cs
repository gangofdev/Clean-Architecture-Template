namespace CleanArc.Application.Features.Connect.Commands.RequestLogout;

public record RequestLogoutCommand(int UserId):IRequest<OperationResult<bool>>;