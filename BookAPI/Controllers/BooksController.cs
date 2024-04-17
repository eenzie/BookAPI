using BookAPI.Data;
using BookAPI.DTOs;
using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookAPIContext _db;

        public BooksController(BookAPIContext context)
        {
            _db = context;
        }

        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<Book, BookDTO>> AsBookDto =
            x => new BookDTO
            {
                Title = x.Title,
                Author = x.Author.Name,
                Genre = x.Genre
            };

        // GET: api/Books
        [HttpGet]
        public IQueryable<BookDTO> GetBooks()
        {
            return _db.Books
                .Include(b => b.Author)
                .Select(AsBookDto);
        }

        // GET: api/Books/5
        //      [HttpGet that uses route ID after the base controller route
        //      It's the same as using [Route("api/books/id")] above it
        //      So it is essentially combined in one line now
        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<ActionResult<BookDTO>> GetBookById(int id)
        {
            try
            {
                var book = await _db.Books
                                .Include(b => b.Author)
                                .Where(b => b.BookId == id)
                                .Select(AsBookDto)
                                .FirstOrDefaultAsync();

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get: api/Books/5/details
        [HttpGet("{id}/details", Name = "GetBookDetails")]
        public async Task<ActionResult<BookDetailDTO>> GetBookDetails(int id)
        {
            try
            {
                var book = await (from b in _db.Books.Include(b => b.Author)
                                  where b.BookId == id
                                  select new BookDetailDTO
                                  {
                                      Title = b.Title,
                                      Genre = b.Genre,
                                      PublishDate = b.PublishDate,
                                      Description = b.Description,
                                      Price = b.Price,
                                      Author = b.Author.Name
                                  })
                                  .FirstOrDefaultAsync();

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Books/genre/{genre}
        [HttpGet("genre/{genre}", Name = "GetBooksByGenre")]
        public IQueryable<BookDTO> GetBooksByGenre(string genre)
        {
            return _db.Books
                        .Include(b => b.Author)
                        .Where(b => EF.Functions
                        .Like(b.Genre.ToLower(), genre.ToLower()))
                        .Select(AsBookDto);
        }

        // GET: api/Authors/5/books
        [HttpGet("~/api/authors/{authorId:int}/books", Name = "GetBooksByAuthor")]
        public IQueryable<BookDTO> GetBooksByAuthor(int authorId)
        {
            return _db.Books
                .Include(b => b.Author)
                .Where(b => b.AuthorId == authorId)
                .Select(AsBookDto);
        }

        // GET: api/Books/date/{pubdate:datetime}
        [HttpGet("date/{year:int}/{month:int}/{day:int}", Name = "GetBooksSlashByDate")]
        public IQueryable<BookDTO> GetBooksBySlashDate(int year, int month, int day)
        {
            DateTime pubdate = new DateTime(year, month, day);

            return _db.Books.Include(b => b.Author)
                .Where(b => EF.Property<DateTime>(b, "PublishDate").Date == pubdate.Date)
                .Select(AsBookDto);
        }

        // GET: api/Books/date/{pubdate:datetime}
        [HttpGet("date-{year:int}-{month:int}-{day:int}", Name = "GetBooksByDashDate")]
        public IQueryable<BookDTO> GetBooksByDashDate(int year, int month, int day)
        {
            DateTime pubdate = new DateTime(year, month, day);

            return _db.Books.Include(b => b.Author)
                .Where(b => EF.Property<DateTime>(b, "PublishDate").Date == pubdate.Date)
                .Select(AsBookDto);
        }

        #region METODER DER IKKE VIRKER

        // Virker ikke med regex
        //GET: api/Books/date/{pubdate:datetime}
        //[HttpGet("date/{{*pubdate:datetime:regex(\\d{{4}}/\\d{{2}}/\\d{{2}})}}", Name = "GetBooksByPubDate")]
        //public IQueryable<BookDTO> GetBooksByPubDate(DateTime pubdate)
        //{
        //    DateTime startDate = pubdate.Date;
        //    DateTime endDate = startDate.AddDays(1); // Next day to include whole day

        //    return _db.Books.Include(b => b.Author)
        //                    .Where(b => b.PublishDate >= startDate && b.PublishDate < endDate)
        //                    .Select(AsBookDto);
        //}

        // Gives error 500 for the StringComparison.OrdinalIgnoreCase use in the Equals() method
        //[HttpGet("genre/{genre}", Name = "GetBooksByGenre")]
        //public IQueryable<BookDTO> GetBooksByGenre(string genre)
        //{
        //    return _context.Books
        //        .Include(b => b.Author)
        //        .Where(b => b.Genre
        //        .Equals(genre, StringComparison.OrdinalIgnoreCase))
        //        .Select(AsBookDto);
        //}
        #endregion  

        #region Unused methods generated during scaffolding

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        //{
        //    return await _context.Books.ToListAsync();
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Books>> GetBook(int id)
        //{
        //    var book = await _context.Books.FindAsync(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBook(int id, Book book)
        //{
        //    if (id != book.BookId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(book).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Books
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Book>> PostBook(Book book)
        //{
        //    _context.Books.Add(book);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        //}

        //// DELETE: api/Books/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBook(int id)
        //{
        //    var book = await _context.Books.FindAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Books.Remove(book);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool BookExists(int id)
        //{
        //    return _context.Books.Any(e => e.BookId == id);
        //}
        #endregion
    }
}
