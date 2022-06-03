using Blazored.LocalStorage;
using BookStore.UI.Providers;
using BookStore.UI.Services.Base;

namespace BookStore.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient client;
        private readonly ILocalStorageService storage;
        private readonly AuthenticationProvider provider;

        public AuthenticationService(IClient client,ILocalStorageService storage,AuthenticationProvider provider)
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
            await provider.LoggedIn();

            return true;
        }
    }
}
