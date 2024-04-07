using CleanArc.Application.Features.Order.Queries.GetAllOrders;
using CleanArc.Infrastructure.Identity.Identity.PermissionManager;


namespace CleanArc.Web.Api.Controllers.V1.Admin
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/OrderManagement")]
    [Display(Description = "Managing Users related Orders")]
    [Authorize(ConstantPolicies.DynamicPermission)]
    public class OrderManagementController(ISender sender) : BaseController
    {
        private readonly ISender _sender = sender;

        [HttpGet("OrderList")]
        public async Task<IActionResult> GetOrders()
        {
            var queryResult = await _sender.Send(new GetAllOrdersQuery());

            return base.OperationResult(queryResult);
        }
    }
}
