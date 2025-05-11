namespace API_LibraryManagement.Application.Interfaces;

using API_LibraryManagement.Models;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<Book> AddAsync(Book book);
    Task<Book?> UpdateAsync(Book book);
    Task<bool> DeleteAsync(int id);
}
