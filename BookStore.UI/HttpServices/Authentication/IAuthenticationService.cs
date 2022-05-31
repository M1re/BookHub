namespace BookStore.UI.HttpServices.Authentication;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(UserLoginDTO loginModel);
    public Task Logout();
}