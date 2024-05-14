using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_shop.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        [Display(Name ="Title")]
        public string Title { get; set; }

        [Display(Name = "Year")]
        public int ? YearPublished { get; set; }
        
        [Display(Name = "Number of Pages")]
        public int ? NumPages { get; set; }
       
        [Display(Name = "Description")]
        public string ? Description { get; set; }
        
        [Display(Name="Publisher")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string ? Publisher { get; set; }
        
        [Display(Name = "Front Page")]
        public string ? FrontPage { get; set; }
        
        [Display(Name = "Download here")]
        public string ? DownloadUrl { get; set; }

        //Relationships
        public List<Review> ? Reviews { get; set; }
        public List<UserBook> ? User_Books { get; set; }
        public List<BookGenre> ? Books_Genres { get; set; }
        //Author
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author ? Author { get; set; }
    }
    
}
