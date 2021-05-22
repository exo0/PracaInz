using Moq;
using System;
using Xunit;
using PracaInz.Services;
using PracaInz.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using PracaInz.BLL;
using PracaInz.DAL.EF;

namespace PracaInz.UnitTest
{
    public class UserRolesTest
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;
        public Mock<UserRoleIdentityServices> mock = new Mock<UserRoleIdentityServices>();
        
        [Fact]
        public async void GetUser()
        {
            mock.Setup(p => p.GetUserById(1));
            UserRoleIdentityServices service = new UserRoleIdentityServices(userManager, signInManager, roleManager, context);
            var result = service.GetUserById(1);
            Assert.Equal("Local", result.FirstName);
            

        }
    }
}
