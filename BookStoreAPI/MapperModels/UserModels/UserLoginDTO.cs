using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.MapperModels.UserModels
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
