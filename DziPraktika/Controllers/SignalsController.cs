using DziPraktika.Data;
using DziPraktika.Models;
using DziPraktika.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DziPraktika.Controllers;

[AllowAnonymous]
public class SignalsController(ApplicationDbContext db, ILogger<SignalsController> logger) : Controller
{
    public const string SuccessKey = "SignalSuccess";
    public const string ErrorKey = "SignalError";

    [HttpGet]
    public IActionResult Create() => View(new SignalFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SignalFormViewModel vm)
    {
        // Атрибутите на ViewModel-а вече са проверени при model binding.
        // Проверяваме и текста без интервалите в краищата: "         x" минава атрибута, но не е 10 символа текст.
        var content = (vm.Content ?? "").Trim();
        if (ModelState.IsValid &&
            (content.Length < SignalFormViewModel.MinLength || content.Length > SignalFormViewModel.MaxLength))
        {
            ModelState.AddModelError(nameof(vm.Content),
                $"Текстът трябва да е между {SignalFormViewModel.MinLength} и {SignalFormViewModel.MaxLength} символа.");
        }
        if (vm.Type is { } t && !Enum.IsDefined(t))
        {
            ModelState.AddModelError(nameof(vm.Type), "Избери „Проблем“ или „Предложение“.");
        }

        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var signal = new Signal { Type = vm.Type!.Value, Content = content, CreatedAt = DateTime.UtcNow };
        try
        {
            db.Signals.Add(signal);
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Неуспешен запис на сигнал");
            ViewData[ErrorKey] = "Сигналът не беше записан поради грешка. Опитай отново.";
            return View(vm);
        }

        TempData[SuccessKey] = $"Благодарим! Сигналът е приет с вх. номер {signal.ReferenceNumber}.";
        return RedirectToAction(nameof(Create)); // PRG: F5 не изпраща сигнала повторно
    }
}
