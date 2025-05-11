using System.Reflection;

namespace API_LibraryManagement.Controllers;

using Microsoft.AspNetCore.Mvc;
using API_LibraryManagement.Models;
using API_LibraryManagement.Application.Interfaces;

[Route("api/books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    // GET api/books
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _bookService.GetAllAsync();
        return Ok(books);
    }

    // GET api/books/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }
    
    // POST api/books
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _bookService.AddAsync(book);
        return Ok(new { Message = "Book created", Book = created });
    }
    
       [HttpPatch("{id}")]
    public async Task<IActionResult> PatchBook(int id, [FromBody] Dictionary<string, object> updates)
    {
        // Validate input
        if (updates == null || updates.Count == 0)
            return BadRequest(new { Message = "No updates provided." });

        // Fetch the book
        var book = await _bookService.GetByIdAsync(id);
        if (book == null)
            return NotFound(new { Message = $"Book with ID {id} not found." });

        // Track changes for Entity Framework
        var bookType = typeof(Book);
        foreach (var entry in updates)
        {
            var prop = bookType.GetProperty(entry.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null || !prop.CanWrite)
                return BadRequest(new { Message = $"Property '{entry.Key}' does not exist or is read-only." });

            try
            {
                // Handle type conversion
                object convertedValue;
                if (prop.PropertyType == typeof(string))
                {
                    // Directly convert to string, handling null or non-string inputs
                    convertedValue = entry.Value?.ToString();
                }
                else
                {
                    // Handle other types with Convert.ChangeType
                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    if (entry.Value == null && prop.PropertyType.IsClass)
                        convertedValue = null;
                    else if (entry.Value == null)
                        return BadRequest(new { Message = $"Value for property '{entry.Key}' cannot be null." });
                    else
                        convertedValue = Convert.ChangeType(entry.Value, targetType);
                }

                prop.SetValue(book, convertedValue);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Invalid value for property '{entry.Key}': {ex.Message}" });
            }
        }

        // Validate the updated model
        if (!TryValidateModel(book))
            return BadRequest(ModelState);

        try
        {
            // Update and save changes
            await _bookService.UpdateAsync(book);
            return Ok(new { Message = "Book partially updated", Book = book });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"An error occurred while updating the book: {ex.Message}" });
        }
    }
    
    // DELETE api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var deleted = await _bookService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
