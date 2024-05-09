using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_shop.Models
{
    public class UserBook
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "App User")]
        [Required(ErrorMessage = "App User is required")]
        [StringLength(450, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string AppUser { get; set; }
        //Relationships
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        
    }
}
