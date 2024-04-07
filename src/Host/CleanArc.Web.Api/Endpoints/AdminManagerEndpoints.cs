using CleanArc.Application.Features.Admin.Commands.AddAdminCommand;

namespace CleanArc.Web.Api.Endpoints;

public class AdminManagerEndpoints : ICarterModule
{
    private readonly string _routePrefix = "/api/v{version:apiVersion}/Admin/";
    private readonly double _version = 1.1;
    private readonly string _tag = "AdminManager";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapEndpoint(
      builder => builder.MapPost($"{_routePrefix}NewAdmin"
          , async (AddAdminCommand model, ISender sender) => (await sender.Send(model)).ToEndpointResult())
      , _version
      , "AddAdmin"
      , _tag)
      .RequireAuthorization(builder => builder.RequireRole("admin"));
    }
}