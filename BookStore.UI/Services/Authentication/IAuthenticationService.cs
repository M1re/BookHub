using BookStore.UI.Services.Base;

namespace BookStore.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(UserLoginDTO loginModel);
    }

    public class AuthenticationService : IAuthenticationService
    {
        public Task<bool> AuthenticateAsync(UserLoginDTO loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
