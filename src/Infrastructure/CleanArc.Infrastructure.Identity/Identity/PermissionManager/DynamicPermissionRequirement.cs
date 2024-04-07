using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace CleanArc.Infrastructure.Identity.Identity.PermissionManager;

public class DynamicPermissionRequirement : IAuthorizationRequirement
{
}

public class DynamicPermissionHandler(
    IDynamicPermissionService dynamicPermissionService,
    IHttpContextAccessor contextAccessor
    ) : AuthorizationHandler<DynamicPermissionRequirement>
{
    private readonly IDynamicPermissionService _dynamicPermissionService = dynamicPermissionService;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DynamicPermissionRequirement requirement)
    {

        var user = _contextAccessor.HttpContext.User;

        var routeData = _contextAccessor.HttpContext.GetRouteData().Values;

        var controller = routeData["controller"].ToString();

        var action = routeData["action"].ToString();

        var area = routeData["area"]?.ToString();

        if (_dynamicPermissionService.CanAccess(user, area, controller, action))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}