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

    form.addEventListener("submit", function (e) {
        if (button.disabled) { e.preventDefault(); return; } // втори клик
        // jQuery unobtrusive validation спира невалидна форма; бутонът се заключва само при валидна.
        const valid = window.jQuery && jQuery.fn.valid ? jQuery(form).valid() : form.checkValidity();
        if (!valid) return;
        button.disabled = true;
        button.textContent = "Изпращане…";
    });
})();
