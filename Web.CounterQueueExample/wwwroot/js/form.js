const form = document.getElementById("operation-form");
const operationNameValueInput = form.querySelector("#operation-name-value");
const operationButtons = document.querySelectorAll("button[data-operation]");

[...operationButtons].forEach(btnEl => {
    btnEl.addEventListener("click", () => {
        operationNameValueInput.value = btnEl.dataset.operation;
        form.submit();
    });
});
