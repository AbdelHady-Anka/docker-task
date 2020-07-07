using System;
using System.Threading.Tasks;
using Actio.Common.Auth;
using Actio.Domain.Entities;
using Actio.DomainServices.Repositories;
using Actio.DomainServices.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Actio.Services.Identity.Tests.Unit.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_should_be_succeed()
        {
            var auser = new { Email = "user4@email.com", Password = "password", Name = "user4", Salt = "salt", Hash = "hash", Token = "token" };
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            encrypterMock.Setup(x => x.GetSalt(auser.Password)).Returns(auser.Salt);
            encrypterMock.Setup(x => x.GetHash(auser.Password, auser.Salt)).Returns(auser.Hash);
            jwtHandlerMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(new JsonWebToken { Token = auser.Token });


            var user = new User(auser.Name, auser.Email, auser.Password, auser.Salt);

            userRepositoryMock.Setup(x => x.GetByEmailAsync(auser.Email)).ReturnsAsync(user);

            var userService = new UserService(userRepositoryMock.Object,
                encrypterMock.Object, jwtHandlerMock.Object);

            // await userService.RegisterAsync(user.Email, user.Password, user.Name);
            var jwt = await userService.LoginAsync(auser.Email, auser.Password);

            userRepositoryMock.Verify(x => x.GetByEmailAsync(auser.Email), Times.Once);
            jwtHandlerMock.Verify(x => x.Create(It.IsAny<Guid>()), Times.Once);

            jwt.Should().NotBeNull();

            jwt.Token.Should().BeEquivalentTo(auser.Token);
        }
    }
}