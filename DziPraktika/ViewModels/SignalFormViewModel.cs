using System.ComponentModel.DataAnnotations;
using DziPraktika.Models;

namespace DziPraktika.ViewModels;

public class SignalFormViewModel
{
    public const int MinLength = 10;
    public const int MaxLength = 1000;

    [Required(ErrorMessage = "Избери „Проблем“ или „Предложение“.")]
    [Display(Name = "Вид")]
    public SignalType? Type { get; set; }

    [Required(ErrorMessage = "Напиши текст на сигнала.")]
    [StringLength(MaxLength, MinimumLength = MinLength,
        ErrorMessage = "Текстът трябва да е между {2} и {1} символа.")]
    [Display(Name = "Текст")]
    public string Content { get; set; } = "";
}
