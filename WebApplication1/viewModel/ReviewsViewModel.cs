using e_shop.Models;

namespace WebApplication1.viewModel
{
    public class ReviewsViewModel
    {

        public int BookId { get; set; }
        public IEnumerable<Review> ? Reviews { get; set; }
    }
}
