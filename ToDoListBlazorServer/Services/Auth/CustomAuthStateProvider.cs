using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ToDoListBlazorServer.Services.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _user = new ClaimsPrincipal(new ClaimsIdentity());


        public override Task<AuthenticationState> GetAuthenticationStateAsync()
             => Task.FromResult(new AuthenticationState(_user));

        public void MarkUserAsAuthenticated(string name, string email, int userId)
        {
                       var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
               
            };
            var identity = new ClaimsIdentity(claims, "apiauth_type");
            _user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsLoggedOut()
        {
            _user = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }


    }
}
