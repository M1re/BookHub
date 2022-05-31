#nullable disable
using AutoMapper;
using BookStore.API.MapperModels.BookModels;
using BookStore.DataAccess.DataBase;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly BookStoreDatabase _context;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public BooksController(BookStoreDatabase context, IMapper mapper, ILogger<BooksController> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookGetModel>>> GetBooks()
    {
        try
        {
            var books = _mapper.Map<IEnumerable<BookGetModel>>(
                await _context.Books.Include(a => a.Author).ToListAsync());
            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Performing Get in {nameof(GetBooks)}");
            return StatusCode(500, "There was an error completeing your request.");
        }
    }

    // GET: api/Books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookGetModel>> GetBook(int id)
    {
        try
        {
            var book = _mapper.Map<BookGetModel>(await _context.Books.FindAsync(id));

            if (book == null) return NotFound();

            return book;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Performing Get in {nameof(GetBook)}");
            return StatusCode(500, "There was an error completeing your request.");
        }
    }

    // PUT: api/Books/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutBook(int id, BookUpdateModel model)
    {
        if (id != model.Id) return BadRequest();
        var book = await _context.Books.FindAsync(id);

        if (book == null) return NotFound();
        _mapper.Map<BookUpdateModel>(model);
        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await BookExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Books
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BookCreateModel>> PostBook(BookCreateModel model)
    {
        try
        {
            var book = _mapper.Map<Book>(model);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostBook), new { id = book.Id }, book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Performing Post in {nameof(PostBook)}");
            return StatusCode(500, "There was an error completeing your request.");
        }
    }

    // DELETE: api/Books/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Performing Delete in {nameof(DeleteBook)}");
            return StatusCode(500, "There was an error completeing your request.");
        }
    }

    private async Task<bool> BookExists(int id)
    {
        return await _context.Books.AnyAsync(e => e.Id == id);
    }
}