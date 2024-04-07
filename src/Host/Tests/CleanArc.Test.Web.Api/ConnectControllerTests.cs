using CleanArc.Application.Features.Connect.Commands.Create;
using CleanArc.Application.Features.Connect.Queries.GetToken;
using CleanArc.Domain.Models.Jwt;
using CleanArc.Web.Api.Controllers.V1.Connect;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace CleanArc.Test.Web.Api
{
    public class ConnectControllerTests
    {

        [Fact]
        public async Task CreateUser_Returns_OkResult()
        {

            //Arrange
            var mediator = new Mock<IMediator>();
            var controller = new ConnectController(mediator.Object);

            var model = new SignupCommand("user@cleanarc.com", "User", "Development", "Test@123", "+0123456789");

            var userCreateResult = new SignupCommandResult();
            mediator.Setup(x => x.Send(model, new CancellationToken())).ReturnsAsync(userCreateResult);

            //Act
            var result = await controller.CreateUser(model);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TokenRequest_Returns_OkResult()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            var controller = new ConnectController(mediator.Object);

            var model = new GetTokenQuery("user@cleanarc.com", "Test@123");
            //complete the test 
            var userTokenRequestResult = new AccessTokenResponse(null);
            mediator.Setup(x => x.Send(model, new CancellationToken())).ReturnsAsync(userTokenRequestResult);
            //Act
            var result = await controller.TokenRequest(model);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }





    }
}