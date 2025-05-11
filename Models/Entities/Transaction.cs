namespace API_LibraryManagement.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum TransactionType { Borrow, Return }

public class Transaction
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "BookId field is not allowed to be empty.")]
    public int BookId { get; set; }

    [Required(ErrorMessage = "TransactionType field is only allowed to be 'Borrow' or 'Return'.")]
    public TransactionType TransactionType { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(BookId))]
    public Book Book { get; set; }
}
