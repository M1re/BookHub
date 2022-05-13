using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Mapper.BookModels
{
    public class BookUpdateModel : BaseModel
    {
        [Required, StringLength(50)]
        public string Title { get; set; }

        [Required, Range(1000, int.MaxValue)]
        public int Year { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required, StringLength(250)]
        public string Summary { get; set; }

        public string Image { get; set; }

        [Required, Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }
}
