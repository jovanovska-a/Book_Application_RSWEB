using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_shop.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Comment is required")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string Comment { get; set; }

        public int ? Rating { get; set; }
        //Relations
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book ? Book { get; set; }

        [Display(Name = "App User")]
        [Required(ErrorMessage = "App User is required")]
        [StringLength(450, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string ? AppUser { get; set; }
        [ForeignKey("AppUser")]
        public AppUser ? AppUserInfo { get; set; }

    }
    
    

}
