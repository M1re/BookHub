namespace BookStore.API.MapperModels.UserModels;

public class UserAuthResponse
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
}