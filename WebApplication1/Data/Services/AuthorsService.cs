using e_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace e_shop.Data.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var result=await _context.Authors.FirstOrDefaultAsync(n => n.Id==id); 
             _context.Authors.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var result = await _context.Authors.ToListAsync();
            return result;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var result = await _context.Authors.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<Author> UpdateAsync(Author newAuthor)
        {
            _context.Update(newAuthor);
            await _context.SaveChangesAsync();
            return newAuthor;
        }

    }
}
