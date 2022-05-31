using Blazored.LocalStorage;
using BookStore.UI.Providers;

namespace BookStore.UI.HttpServices.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthProvider _provider;

    public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthProvider provider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _provider = provider;
    }

    public async Task<bool> AuthenticateAsync(UserLoginDTO loginModel)
    {
        var response = await _httpClient.LoginAsync(loginModel);

        //Store token
        await _localStorage.SetItemAsync("Access Token", response.Token);

        // Change auth state
        await ((AuthProvider)_provider).LoggedIn();

        return true;
    }

    public async Task Logout()
    {
        await ((AuthProvider)_provider).LoggedOut();
    }
}