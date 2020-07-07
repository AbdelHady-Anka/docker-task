using Moq;
using RawRabbit;
using Xunit;
using Actio.Api.Controllers;
using Actio.Api.Repositories;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Actio.Common.Commands;
using System.Threading.Tasks;
using FluentAssertions;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class ActivitiesControllerTests
    {
        [Fact]
        public async Task post_should_return_accepted()
        {
            var busClientMock = new Mock<IBusClient>();
            var activityRepositoryMock = new Mock<IActivityRepository>();

            var controller = new ActivitiesController(busClientMock.Object,
                activityRepositoryMock.Object);

            var userId = Guid.NewGuid();

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] {
                            new Claim(ClaimTypes.Name, userId.ToString())
                        }
                    , "test"))
                }
            };

            var command = new CreateActivity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = "test"
            };

            var result = await controller.Post(command);

            var acceptedResult = result as AcceptedResult;
            acceptedResult.Should().NotBeNull();
            acceptedResult.Location.Should().BeEquivalentTo($"Activities/{command.Id}");
        }
    }
}