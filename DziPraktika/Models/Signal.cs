using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DziPraktika.Models;
public class Signal
{
    public int Id { get; set; }

    [Required]
    public SignalType Type { get; set; }

    [Required, StringLength(1000, MinimumLength = 10)]
    public string Content { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public string ReferenceNumber => $"SIG-{Id:D6}"; // вх. номер, не е колона
}
