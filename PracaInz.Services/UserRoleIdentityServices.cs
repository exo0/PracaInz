using Microsoft.AspNetCore.Identity;
using PracaInz.BLL;
using PracaInz.DAL.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracaInz.Services
{
    public class UserRoleIdentityServices
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserRoleIdentityServices(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            ApplicationDbContext _context)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            context = _context;
        }
    }
}
