using System.ComponentModel.DataAnnotations;

namespace e_shop.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Genre is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string GenreName{ get; set; }

        //Relationships
        public List<BookGenre> ? Books_Genres { get; set; }
    }
}
