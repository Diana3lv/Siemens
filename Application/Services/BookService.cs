namespace API_LibraryManagement.Application.Services;

using API_LibraryManagement.Models;
using API_LibraryManagement.Persistence.Repositories;
using API_LibraryManagement.Application.Interfaces;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Book>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Book?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public Task<Book> AddAsync(Book book)
    {
        return _repository.AddAsync(book);
    }

    public Task<Book?> UpdateAsync(Book book) => _repository.UpdateAsync(book);

    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}
