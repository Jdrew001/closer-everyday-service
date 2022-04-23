using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using CED.Models.DTO;
using CED.Services.Core;
using CED.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CED.Services.Tests
{
    public class AuthenticationServiceTest
    {
        // [Fact]
        // public async void Login()
        // {
        //     // Setup
        //     var logger = new Mock<ILogger<AuthenticationService>>();
        //     var jwtToken = Options.Create(new JwtToken());
        //     var connectionStrings = Options.Create(new ConnectionStrings());
        //     var deviceService = new Mock<IDeviceService>();
        //     var userRepo = new Mock<IUserRepository>();
        //     var refreshTokenRepo = new Mock<IRefreshTokenRepository>();
        //     var testUser = new User()
        //     {
        //         Id = Guid.NewGuid(),
        //         Email = "dtatkison@gmail.com",
        //         Username = "dtatkison",
        //         FirstName = "Drew",
        //         LastName = "Atkison",
        //         LastLogin = new DateTime(),
        //         Locked = false,
        //         DateLocked = null,
        //         PasswordSalt = "1234",
        //         Password = "Test"
        //     };
            

        //     LoginRequestDTO dto = new LoginRequestDTO()
        //     {
        //         Email = "dtatkison@gmail.com",
        //         Password = "Test",
        //         IpAddress = "123.123.123.123"
        //     };

        //     AuthenticationService a = new AuthenticationService(
        //         logger.Object, jwtToken, connectionStrings, deviceService.Object, userRepo.Object, refreshTokenRepo.Object);
        //     userRepo.Setup(x => x.GetUserByEmail(dto.Email)).Returns(Task.FromResult(testUser));


        //     await a.Login(dto, "1234");
        // }
    }
}
