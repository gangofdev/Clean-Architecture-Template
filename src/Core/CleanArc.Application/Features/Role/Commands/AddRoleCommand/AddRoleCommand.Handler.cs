using CleanArc.Domain.Contracts.Identity;
using CleanArc.Application.Models.Identity;
using Mediator;
using CleanArc.Domain.Models.Role;

namespace CleanArc.Application.Features.Role.Commands.AddRoleCommand
{
    internal class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, OperationResult<bool>>
    {
        private readonly IRoleManagerService _roleManagerService;

        public AddRoleCommandHandler(IRoleManagerService roleManagerService)
        {
            _roleManagerService = roleManagerService;
        }

        public async ValueTask<OperationResult<bool>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var addRoleResult =
                await _roleManagerService.CreateRoleAsync(new NewRole() { RoleName = request.RoleName });

            if (addRoleResult.Succeeded)
                return OperationResult<bool>.SuccessResult(true);

            var errors = string.Join("\n", addRoleResult.Errors.Select(c => c.Description));

            return OperationResult<bool>.FailureResult(errors);
        }
    }
}
