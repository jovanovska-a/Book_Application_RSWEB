using System.ComponentModel.DataAnnotations;

namespace e_shop.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        public DateOnly ? BirthDate { get; set; }

        [Display(Name = "Nationality")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string ? Nationality { get; set; }

        [Display(Name = "Gender")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string ? Gender { get; set; }

        //Relationships
        public List<Book> ? Books { get; set; }

    }
}
