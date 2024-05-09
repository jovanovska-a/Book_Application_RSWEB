using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using e_shop.Models;

namespace WebApplication1.viewModel
{
    public class CreateBooksViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Publication year")]
        public int? YearPublished { get; set; }

        [Display(Name = "Number of Pages")]
        public int? NumPages { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Publisher")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Invalid Length")]
        public string? Publisher { get; set; }

        [Display(Name = "Front Page")]
        public string? FrontPage { get; set; }

        [Display(Name = "Download here")]
        public string? DownloadUrl { get; set; }
        public IEnumerable<Author>? Authors { get; set; }
        public int AuthorId { get; set; }
        public IEnumerable<Genre>? Genres { get; set; }
        public IEnumerable<int> GenreIds { get; set; }
    }
}

