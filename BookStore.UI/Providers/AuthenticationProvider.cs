using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookStore.UI.Providers
{
    public class AuthenticationProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService storage;
        private readonly JwtSecurityTokenHandler tokenHandler;

        public AuthenticationProvider(ILocalStorageService storage)
        {
            this.storage = storage;
            tokenHandler = new JwtSecurityTokenHandler();
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var savedToken = await storage.GetItemAsync<string>("accessToken");
            if(savedToken == null)
            {
                return new AuthenticationState(user);
            }

            var tokenContent = tokenHandler.ReadToken(savedToken);

            if (tokenContent.ValidTo < DateTime.Now)
            {
                return new AuthenticationState(user);
            }
            var claims = await GetClaims();
            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            return new AuthenticationState(user);
        }

        public async Task LoggedIn()
        {
            var claims = await GetClaims();
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task LoggedOut()
        {
            await storage.RemoveItemAsync("accessToken");
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var savedToken = await storage.GetItemAsync<string>("accessToken");
            var tokenContent = tokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
