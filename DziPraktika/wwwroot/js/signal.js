(function () {
    const form = document.getElementById("signal-form");
    if (!form) return;

    const textarea = form.querySelector("textarea[name='Content']");
    const counter = document.getElementById("signal-counter");
    const button = document.getElementById("signal-submit");
    const min = 10, max = 1000;

    function updateCounter() {
        const len = textarea.value.trim().length;
        counter.textContent = len + " / " + max;
        counter.classList.toggle("signal-counter-bad", (len > 0 && len < min) || len > max);
    }
    textarea.addEventListener("input", updateCounter);
    updateCounter();

    const buttonText = button.textContent;
    // Back след успешно изпращане връща страницата от bfcache със заключен бутон — отключи го.
    window.addEventListener("pageshow", function (e) {
        if (e.persisted) { button.disabled = false; button.textContent = buttonText; }
    });

    form.addEventListener("submit", function (e) {
        if (button.disabled) { e.preventDefault(); return; } // втори клик
        textarea.value = textarea.value.trim(); // същото, което брояча и сървърът мерят
        // jQuery unobtrusive validation спира невалидна форма; бутонът се заключва само при валидна.
        const valid = window.jQuery && jQuery.fn.valid ? jQuery(form).valid() : form.checkValidity();
        if (!valid) return;
        button.disabled = true;
        button.textContent = "Изпращане…";
    });
})();
