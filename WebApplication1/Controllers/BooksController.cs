﻿using e_shop.Data;
using e_shop.Data.Services;
using e_shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Services;
using WebApplication1.viewModel;

namespace e_shop.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService _service;
        private readonly IAuthorsService _authorsService;
        private readonly IGenresService _genresService;
        private readonly IBooksGenresService _bookGenresService;
        private readonly IPhotosService _photoService;

        public BooksController(IBooksService service,IAuthorsService authorsService,IGenresService genresService,IBooksGenresService bookGenresService,IPhotosService photoService)
        {
            _service = service;
            _authorsService = authorsService;
            _genresService = genresService;
            _bookGenresService = bookGenresService;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index(string searchString1, string searchString2, string searchString3)
        {
            var allBooks = await _service.GetAllAsync();
            if(!String.IsNullOrEmpty(searchString1))
            {
                allBooks=allBooks.Where(n => n.Title.Contains(searchString1)).ToList();
            }
            if (!String.IsNullOrEmpty(searchString2))
            {
                allBooks = allBooks.Where(n => n.Books_Genres.Any(
                        bg => bg.Genre.GenreName.Contains(searchString2))
                );
            }
            if (!String.IsNullOrEmpty(searchString3))
            {
                allBooks = allBooks.Where(n => n.Author.FirstName.Contains(searchString3) || n.Author.LastName.Contains(searchString3)).ToList();
            }
            return View(allBooks);
        }
        //Get: Books/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<Author> authors = await _authorsService.GetAll();
            IEnumerable<Genre> genres = await _genresService.GetAll();
            CreateBooksViewModel newVM = new CreateBooksViewModel()
            {
                Author = authors,
                Genres = genres,
            };
            return View(newVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBooksViewModel bookVM)
        {
            if(!ModelState.IsValid)
            {
                IEnumerable<Author> author = await _service.GetAllAuthors();
                IEnumerable<Genre> genres = await _service.GetAllGenres();
                bookVM.Author = author;
                bookVM.Genres = genres;
                ModelState.AddModelError("", "Failed to add book");
                return View(bookVM);
            }
            var result = await _photoService.AddPhotoAsync(bookVM.FrontPage);
            var newBook = new Book()
            {
                Title = bookVM.Title,
                YearPublished = bookVM.YearPublished,
                NumPages = bookVM.NumPages,
                Description = bookVM.Description,
                Publisher = bookVM.Publisher,
                FrontPage = result.Url.ToString(),
                AuthorId = bookVM.AuthorId,
                DownloadUrl = bookVM.DownloadUrl,
            };
            _service.Add(newBook);

            Book newLastBook = await _service.GetLastBook();
            foreach (var genreId in bookVM.GenreIds)
            {
                BookGenre bookGenre = new BookGenre()
                {
                    BookId = newLastBook.Id,
                    GenreId = genreId,
                };
                _bookGenresService.Add(bookGenre);
            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> SearchByAuthorId(int id)
        {
            IEnumerable<Book> books = await _service.GetBooksByAuthorId(id);
            return View(books);
        }
        //Get: Book/Delete
        public async Task<IActionResult> Delete(int id)
        {
            Book book = await _service.GetByIdAsync(id);
            if(book==null)
            {
                return View("NotFound");
            }
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteBookConfirmed(int id)
        {
            Book book=await _service.GetByIdAsync(id);
            if(book==null)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        //Get: Books/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Book book = await _service.GetByIdAsyncNoTracking(id);
            if (book == null)
            {
                return View("NotFound");
            }
            IEnumerable<Author> authors = await _service.GetAllAuthors();
            IEnumerable<Genre> genres = await _service.GetAllGenres();
            List<int> genreIds = new List<int>();
            foreach (var genre in book.Books_Genres)
            {
                genreIds.Add(genre.GenreId);
            }
            EditBooksViewModel bookVM = new EditBooksViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                YearPublished = book.YearPublished,
                NumPages = book.NumPages,
                Publisher = book.Publisher,
                FrontPage = book.FrontPage,
                DownloadUrl = book.DownloadUrl,
                Authors = authors,
                Genres = genres,
                AuthorId = book.AuthorId,
                GenreIds = genreIds,
            };
            return View(bookVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBooksViewModel bookVM)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Author> authors = await _service.GetAllAuthors();
                bookVM.Authors = authors;
                IEnumerable<Genre> genres = await _service.GetAllGenres();
                bookVM.Genres = genres;
                ModelState.AddModelError("", "Failed to edit book");
                return View(bookVM);
            }
            if (bookVM != null)
            {
                Book newBook = new Book()
                {
                    Id = bookVM.Id,
                    Title = bookVM.Title,
                    Description = bookVM.Description,
                    YearPublished = bookVM.YearPublished,
                    NumPages = bookVM.NumPages,
                    Publisher = bookVM.Publisher,
                    FrontPage = bookVM.FrontPage,
                    DownloadUrl = bookVM.DownloadUrl,
                    AuthorId = bookVM.AuthorId,
                };
                _service.Update(id,newBook);
                IEnumerable<BookGenre> bookGenres = await _bookGenresService.GetAll();
                foreach (var bg in bookGenres)
                {
                    if (bg.BookId == id)
                    {
                        _bookGenresService.Delete(bg);
                    }
                }
                foreach (var genreId in bookVM.GenreIds)
                {
                    BookGenre bookGenre = new BookGenre()
                    {
                        BookId = bookVM.Id,
                        GenreId = genreId,
                    };
                    _bookGenresService.Add(bookGenre);
                }

                return Redirect("/Books/Details/" + id);
            }
            else
            {
                IEnumerable<Author> authors = await _service.GetAllAuthors();
                bookVM.Authors = authors;
                IEnumerable<Genre> genres = await _service.GetAllGenres();
                bookVM.Genres = genres;
                return View(bookVM);
            }
        }
    }
    
}





