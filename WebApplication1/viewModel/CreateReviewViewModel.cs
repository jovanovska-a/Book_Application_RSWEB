using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using e_shop.Models;

namespace e_shop.viewModel
{
    public class CreateReviewViewModel
    {

        [Display(Name = "App User")]
        [Required(ErrorMessage = "App User is required")]
        [StringLength(450, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string AppUser { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Comment is required")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string Comment { get; set; }

        public int? Rating { get; set; }
        
        public IEnumerable<Book> Books { get; set; }
        public int BookId { get; set; }
    }
}

