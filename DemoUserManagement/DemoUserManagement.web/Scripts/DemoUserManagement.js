function resetForm() {
    document.getElementById("dataForm").reset();
}


function validateForm() {

    if (firstName === '') {
        var firstName = document.getElementById('firstName').value.trim();
        var errorMessageContainer = document.getElementById('errorMessageFirstNameDiv');
        errorMessageContainer.innerText = "Please enter First Name";
        errorMessageContainer.style.display = "block";
        return false;
    }     

    else {
        errorMessageContainer.innerText = ""; 
        errorMessageContainer.style.display = "none"; 
        return true; 
    }
}
