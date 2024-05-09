using e_shop.Data;
using e_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Services
{
    public class BooksGenresService : IBooksGenresService
    {        private readonly AppDbContext _context;
        public BooksGenresService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(BookGenre bookGenre)
        {
            _context.Books_Genres.Add(bookGenre);
            _context.SaveChanges();
        }

        public Task<BookGenre> UpdateAsync(BookGenre bookGenre)
        {
            throw new NotImplementedException();
        }
    }
}
