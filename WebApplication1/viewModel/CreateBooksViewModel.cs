using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using e_shop.Models;

namespace WebApplication1.viewModel
{
    public class CreateBooksViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public int ? YearPublished { get; set; }
        public int ? NumPages { get; set; }
        public string ? Description { get; set; }
        public string ? Publisher { get; set; }
        public IFormFile FrontPage { get; set; }
        public string ? DownloadUrl { get; set; }
        //Relations
        [Required(ErrorMessage = "Author is required")]
        public int AuthorId { get; set; }
        public IEnumerable<Author> ? Author { get; set; }
        public IEnumerable<int> GenreIds { get; set; }
        public IEnumerable<Genre>? Genres { get; set; }
        
    }
}

