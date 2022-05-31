using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStore.UI.Providers;

public class AuthProvider : AuthenticationStateProvider
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    private readonly ILocalStorageService _localStorage;

    public AuthProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        var savedToken = await _localStorage.GetItemAsync<string>("AccessToken");
        if (savedToken == null) return new AuthenticationState(user);

        var token = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        if (token.ValidTo < DateTime.Now) return new AuthenticationState(user);
        var claims = token.Claims;
        user = new ClaimsPrincipal(new ClaimsIdentity(claims, "JWT"));

        return new AuthenticationState(user);
    }

    public async Task LoggedIn()
    {
        var savedToken = await _localStorage.GetItemAsync<string>("AuthToken");
        var token = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        var claims = token.Claims;
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "JWT"));
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        _localStorage.RemoveItemAsync("AuthToken");
        var anyone = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anyone));
        NotifyAuthenticationStateChanged(authState);
    }
}