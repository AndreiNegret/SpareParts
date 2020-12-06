using Microsoft.AspNetCore.Identity;
using SpareParts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Services
{
    public class AuthService : IAuthService

    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public User CreateUser(string username, string email, string phonenumber, string firstname, string lastname, string address, string city)
        {
            return new User { UserName = username, Email = email, PhoneNumber = phonenumber, FirstName = firstname, LastName = lastname, Address = address, City = city};
        }
        public async Task<IdentityResult> Register(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> Login(string username, string password, bool rememberMe, bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure: false);
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }


    }
}
