using Microsoft.AspNetCore.Identity;
using SpareParts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Services
{
    public interface IAuthService
    {
        public User CreateUser(string username, string email, string phonenumber, string firstname, string lastname, string city, string country);
        public Task<SignInResult> Login(string username, string password, bool rememberMe, bool lockoutOnFailure);
        public Task<IdentityResult> Register(User user, string password);
        public Task Logout();
    }
}
