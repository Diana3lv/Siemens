using Microsoft.AspNetCore.Mvc;
using API_LibraryManagement.Application.Interfaces;
using API_LibraryManagement.Models;

namespace API_LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/books/{bookId}/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        
        // POST api/books/{bookId}/transactions
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(int bookId, [FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            transaction.BookId = bookId;

            try
            {
                var created = await _transactionService.CreateTransactionAsync(transaction);
                return Ok(new { Message = "Transaction created", Transaction = created });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}