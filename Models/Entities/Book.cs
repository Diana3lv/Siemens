using System.ComponentModel.DataAnnotations;

namespace API_LibraryManagement.Models;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title field is not allowed to be empty.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Author field is not allowed to be empty.")]
    public string Author { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be zero or a positive number.")]
    public int Quantity { get; set; }
}