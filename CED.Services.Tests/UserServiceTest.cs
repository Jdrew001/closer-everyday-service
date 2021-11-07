using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models;
using CED.Services.Core;
using Moq;
using Xunit;

namespace CED.Services.Tests
{
    public class UserServiceTest
    {
        [Fact]
        public async void GetNoUsersFromSearch()
        {
            var repo = new Mock<IUserRepository>();
            UserService userService = new UserService(repo.Object);
            var users = new List<User>();
            repo.Setup(o => o.SearchForUser("Dt")).Returns(Task.FromResult(users));
            var result = await userService.SearchForUser("Dt");

            Assert.Empty(result);
        }

        [Fact]
        public async void GetOneUserFromSearch()
        {
            var repo = new Mock<IUserRepository>();
            UserService userService = new UserService(repo.Object);
            var users = new List<User>()
            {
                new User()
                {
                    Id = Guid.Parse("09f43914-3aca-11ec-856e-e86a64f2b202"),
                    FirstName = "Drew",
                    LastName = "Atkison",
                    Email = "dtatkison@gmail.com"
                }
            };
            repo.Setup(o => o.SearchForUser("Dt")).Returns(Task.FromResult(users));
            var result = await userService.SearchForUser("Dt");

            Assert.NotEmpty(result);
        }
    }
}
