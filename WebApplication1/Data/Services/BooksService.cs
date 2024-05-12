using e_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace e_shop.Data.Services
{
    public class BooksService : IBooksService
    {
        private readonly AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Book book)
        {
            _context.Books.Add(book);
             _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Books.FirstOrDefaultAsync(n => n.Id == id);
            _context.Books.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var result=await _context.Books.Include(a => a.Author).Include(r => r.Reviews).Include(bg => bg.Books_Genres).ThenInclude(g => g.Genre).ToListAsync();
            return result;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var result = await _context.Books.Include(a => a.Author).Include(r => r.Reviews).Include(bg => bg.Books_Genres).ThenInclude(g => g.Genre).FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public Book Update(int id, Book book)
        {
            _context.Update(book);
            _context.SaveChanges();
            return book;
        }
        public async Task<Book> GetLastBook()
        {
            return await _context.Books.AsNoTracking().FirstOrDefaultAsync(i => i.Id == _context.Books.Max(i => i.Id));
        }
        public async Task<Book> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Books.Include(a => a.Author).Include(r => r.Reviews).Include(bg => bg.Books_Genres).ThenInclude(g => g.Genre).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksByAuthorId(int id)
        {
            return await _context.Books.Include(a => a.Author).Include(r => r.Reviews).Include(bg => bg.Books_Genres).ThenInclude(g => g.Genre).Where(a => a.AuthorId.Equals(id)).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreId(int id)
        {
            List<BookGenre> bookGenres = await _context.Books_Genres.Where(g => g.GenreId.Equals(id)).ToListAsync();
            List<Book> books = new List<Book>();
            foreach (var bg in bookGenres)
            {
                Book book = await GetByIdAsync(bg.BookId);
                books.Add(book);
            }
            return books;
        }
    }
}
