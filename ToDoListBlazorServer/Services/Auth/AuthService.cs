using ToDoListBlazorServer.Data;
using SecureIdentity.Password;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Client;
using ToDoListBlazorServer.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace ToDoListBlazorServer.Services.Auth
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly CustomAuthStateProvider _authStateProvider;

        public AuthService(AppDbContext context, AuthenticationStateProvider authenticationStateProvider)
        {
            _context = context;
            _authStateProvider = (CustomAuthStateProvider)authenticationStateProvider;
        }
            

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                return false;

            if (user.PasswordHash != HashPasswordService.HashPassword(password))
                return false;


            

            _authStateProvider.MarkUserAsAuthenticated(user.Name, user.Email, user.Id);

            return true;

        }

        public void LogoutAsync()
        {
             _authStateProvider.MarkUserAsLoggedOut();
        }

     
    }
}
