using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.MapperModels.UserModels
{
    public class UserDTO:UserLoginDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Role { get; set; }
    }
}