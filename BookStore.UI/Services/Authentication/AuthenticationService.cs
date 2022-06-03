using Blazored.LocalStorage;
using BookStore.UI.Providers;
using BookStore.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStore.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient client;
        private readonly ILocalStorageService storage;
        private readonly AuthenticationStateProvider provider;

        public AuthenticationService(IClient client,ILocalStorageService storage,AuthenticationStateProvider provider)
        {
            this.client = client;
            this.storage = storage;
            this.provider = provider;
        }
        public async Task<bool> AuthenticateAsync(UserLoginDTO loginModel)
        {
            var response =  await client.LoginAsync(loginModel);

            //store token
            await storage.SetItemAsync("accessToken", response.Token);


            //Change auth state of app
            await ((AuthenticationProvider)provider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            await ((AuthenticationProvider)provider).LoggedOut();
        }
    }
}
