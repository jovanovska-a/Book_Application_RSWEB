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
        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        } 
    }
}
