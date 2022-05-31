using System.ComponentModel.DataAnnotations;

namespace BookStore.API.MapperModels.UserModels;

public class UserDTO : UserLoginDTO
{
    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required] public string Role { get; set; }
}