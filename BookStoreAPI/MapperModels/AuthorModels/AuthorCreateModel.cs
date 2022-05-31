using System.ComponentModel.DataAnnotations;

namespace BookStore.API.MapperModels.AuthorModels;

public class AuthorCreateModel
{
    [Required] [StringLength(20)] public string FirstName { get; set; }

    [Required] [StringLength(20)] public string LastName { get; set; }

    [StringLength(400)] public string Bio { get; set; }
}