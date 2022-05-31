using System.ComponentModel.DataAnnotations;

namespace BookStore.API.MapperModels.UserModels;

public class UserLoginDTO
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }
}