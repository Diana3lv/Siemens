namespace API_LibraryManagement.Persistence.Repositories;
// namespace API_LibraryManagement.Application.Interfaces.IBookRepository;

using API_LibraryManagement.Models;
using API_LibraryManagement.Persistence.Data;
using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync() =>
        await _context.Books.ToListAsync();

    public async Task<Book?> GetByIdAsync(int id) =>
        await _context.Books.FindAsync(id);

    public async Task<Book> AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> UpdateAsync(Book book)
    {
        // Găsește cartea existentă după ID
        var existing = await _context.Books.FindAsync(book.Id);
        if (existing == null) return null; // Dacă nu există cartea, returnează null
        
        Console.WriteLine(book.Title);
        
        // Actualizează doar câmpurile care sunt transmise în request
        if (!string.IsNullOrEmpty(book.Title))
            existing.Title = book.Title;

        if (!string.IsNullOrEmpty(book.Author))
            existing.Author = book.Author;

        if (book.Quantity >= 0)  // Verifică dacă cantitatea este validă
            existing.Quantity = book.Quantity;

        Console.WriteLine(existing.Title);
        // Salvează schimbările în baza de date
        await _context.SaveChangesAsync();
    
        return existing; // Returnează cartea actualizată
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
}
