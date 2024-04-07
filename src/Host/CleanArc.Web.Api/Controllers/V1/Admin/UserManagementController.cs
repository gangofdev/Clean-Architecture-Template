using CleanArc.Application.Features.Users.Queries.GetUsers;
using CleanArc.Infrastructure.Identity.Identity.PermissionManager;

namespace CleanArc.Web.Api.Controllers.V1.Admin
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/UserManagement")]
    [Display(Description = "Managing API Users")]
    [Authorize(ConstantPolicies.DynamicPermission)]
    public class UserManagementController(ISender sender) : BaseController
    {
        private readonly ISender _sender = sender;

        [HttpGet("CurrentUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var queryResult = await _sender.Send(new GetUsersQuery());

            return base.OperationResult(queryResult);
        }
    }
}
