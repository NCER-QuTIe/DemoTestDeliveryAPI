using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class TestQTI
{
    [Key]
    public Guid TestQTIId { get; set; }

    [Required(ErrorMessage = "Title for a test is a required field")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Test must have a qti body")]
    public string? Body { get; set; }

    public DateTime CreationDate { get; set; }

    public bool IsActive { get; set; }
}