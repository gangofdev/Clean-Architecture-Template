using CleanArc.Application.Features.Admin.Commands.AddAdminCommand;
namespace CleanArc.Web.Api.Controllers.V1.Admin
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/AdminManager")]
    public class AdminManagerController(ISender sender) : BaseController
    {
       

        [Authorize(Roles = "admin")]
        [HttpPost("NewAdmin")]
        public async Task<IActionResult> AddNewAdmin(AddAdminCommand model)
        {
            var commandResult = await sender.Send(model);

            return base.OperationResult(commandResult);
        }
    }
}
