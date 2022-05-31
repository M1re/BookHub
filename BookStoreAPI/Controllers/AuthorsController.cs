#nullable disable
using AutoMapper;
using BookStore.API.MapperModels.AuthorModels;
using BookStore.DataAccess.DataBase;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthorsController : ControllerBase
{
    private readonly BookStoreDatabase _context;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public AuthorsController(BookStoreDatabase context, IMapper mapper, ILogger<AuthorsController> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    // GET: api/Authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorGetModel>>> GetAuthors()
    {
        try
        {
            var authors = _mapper.Map<IEnumerable<AuthorGetModel>>(await _context.Authors.ToListAsync());
            return Ok(authors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Performing Get in {nameof(GetAuthors)}");
            return StatusCode(500, "There was an error completeing your request.");
        }
    }

    // GET: api/Authors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorGetModel>> GetAuthor(int id)
    {
        try
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null) return NotFound();
            var model = _mapper.Map<AuthorGetModel>(author);

            return Ok(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Performing Get in {nameof(GetAuthor)}");
            return StatusCode(500, "There was an error completeing your request.");
        }
    }

    // PUT: api/Authors/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutAuthor(int id, AuthorUpdateModel model)
    {
        if (id != model.Id) return BadRequest();

        var author = await _context.Authors.FindAsync(id);

        if (author == null) return NotFound();

        _mapper.Map<AuthorUpdateModel>(author);
        _context.Entry(author).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await AuthorExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Authors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Author>> PostAuthor(AuthorCreateModel model)
    {
        try
        {
            var author = _mapper.Map<Author>(model);
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Performing Post in {nameof(PostAuthor)}");
            return StatusCode(500, "There was an error completeing your request.");
        }
    }

    // DELETE: api/Authors/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        try
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Performing Delete in {nameof(DeleteAuthor)}");
            return StatusCode(500, "There was an error completeing your request.");
        }
    }

    private async Task<bool> AuthorExists(int id)
    {
        return await _context.Authors.AnyAsync(e => e.Id == id);
    }
}