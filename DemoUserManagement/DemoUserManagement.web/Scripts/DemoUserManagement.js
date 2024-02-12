function resetForm() {
    document.getElementById("dataForm").reset();
    removeErrorMessages();
}

function validateData() {
    var formData = document.querySelectorAll('#dataForm [data-store]');
    var isValid = true;

    for (var i = 0; i < formData.length; i++) {
        var element = formData[i];
        var errorMessageContainer = document.getElementById('errorMessage' + element.getAttribute('data-store') + 'Div');
        var value = element.value.trim();

        clearErrorMessage(errorMessageContainer.id);

        switch (element.getAttribute('data-store')) {
            case 'FirstName':
                if (value.length <= 0) {
                    displayErrorMessage(errorMessageContainer, "Please Enter First Name");
                    isValid = false;
                }
                break;
            case 'Email':
                if (!validateEmail(element)) {
                    displayErrorMessage(errorMessageContainer, 'Please enter a valid Email Address.');
                    isValid = false;
                }
                break;
            case 'Password':
                if (!validatePassword(element)) {
                    displayErrorMessage(errorMessageContainer, 'Please enter a valid Password.');
                    isValid = false;
                }
                break;

        }

        if (!isValid) {
            return false;
        }
    }

    return true;
}

function displayErrorMessage(container, message) {
    container.style.display = "block";
    container.innerText = message;
}

function clearErrorMessage(containerId) {
    var errorContainer = document.getElementById(containerId);
    errorContainer.style.display = "none";
}
