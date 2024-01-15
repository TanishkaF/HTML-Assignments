function validateAndStore() {
    var data = validateData();
    if (data) {
        storeData(data);
        displayUserData();
    }
}

function validateData() {

    var formData = document.querySelectorAll('#dataForm [data-store]');
    var data = {};
    let flag = false;

    for (var i = 0; i < formData.length; i++) {

        var element = formData[i];
        var value = element.value.trim();

        var errorMessage = document.createElement('span');
        errorMessage.className = 'error-message';
        errorMessage.id = 'errorContainer';
        

       
        switch (element.getAttribute('data-store')) {

            case 'FirstName':
                if (element.value.length <= 0) {
                    element.focus();
                    const error = document.getElementById('errorMessageFirstNameDiv');
                    error.style.display = "block";
                    error.innerText = "";
                    errorMessage.innerText = "Please Enter First Name";
                    document.getElementById('errorMessageFirstNameDiv').appendChild(errorMessage);
                } else {
                    document.getElementById('errorMessageFirstNameDiv').style.display = "";
                }
                break;
            
                case 'InstituteName10':
                    if (element.value.length <= 0) {
                        element.focus();
                        const error = document.getElementById('errorMessageInstitute10Div');
                        error.style.display = "block";
                        error.innerText = "";
                        errorMessage.innerText = "Please Enter Institute Name of Class 10";
                        document.getElementById('errorMessageInstitute10Div').appendChild(errorMessage);
                    } else {
                        document.getElementById('errorMessageInstitute10Div').style.display = "";
                    }
                break;

                case 'InstituteName12':
                    if (element.value.length <= 0) {
                        element.focus();
                        const error = document.getElementById('errorMessageInstitute12Div');
                        error.style.display = "block";
                        error.innerText = "";
                        errorMessage.innerText = "Please Enter Institute Name of Class 12";
                        document.getElementById('errorMessageInstitute12Div').appendChild(errorMessage);
                    } else {
                        document.getElementById('errorMessageInstitute12Div').style.display = "";
                    }
                break;

                case 'InstituteNameG':
                    if (element.value.length <= 0) {
                        element.focus();
                        const error = document.getElementById('errorMessageInstituteDiv');
                        error.style.display = "block";
                        error.innerText = "";
                        errorMessage.innerText = "Please Enter Institute Name of Class 12";
                        document.getElementById('errorMessageInstituteDiv').appendChild(errorMessage);
                    } else {
                        document.getElementById('errorMessageInstituteDiv').style.display = "";
                    }
                break;
            
              case 'Email':
                if (!validateEmail(element)) {
                    element.focus();
                    const error=document.getElementById('errorMessageDisplayBox');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid Email Address.';
                    document.getElementById('errorMessageDisplayBox').appendChild(errorMessage);
                }else{
                    flag = true;
                    document.getElementById('errorMessageDisplayBox').style.display="";
                }
                break;

            case 'Password':
                if (!validatePassword(element)) {
                    element.focus();
                    const error=document.getElementById('errorMessagePasswordDiv');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid Password.';
                    document.getElementById('errorMessagePasswordDiv').appendChild(errorMessage);
                }
                else{
                    flag = true;
                    document.getElementById('errorMessagePasswordDiv').style.display="";

                }
                break;

            case 'ConfirmPassword':
                if (!validatePassword(element)) {
                    element.focus();
                    const error=document.getElementById('errorMessageConfirmPasswordDiv');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid cPassword.';
                    document.getElementById('errorMessageConfirmPasswordDiv').appendChild(errorMessage);
                }
                else{
                    flag = true;
                    document.getElementById('errorMessageConfirmPasswordDiv').style.display="";
                }
                break;

            case 'Age':
                if (!validateNumbers(element) || (element.value.length !== 2)) {
                    console.log('Value:', value);
                    console.log('Length:', value.length);
                    element.focus();
                    const error=document.getElementById('errorMessageAgeDiv');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid Age of 2 digit';
                    document.getElementById('errorMessageAgeDiv').appendChild(errorMessage);
                }
                else{
                    flag = true;
                    document.getElementById('errorMessageAgeDiv').style.display="";
                }
                break;

            case 'Phone':
                if (!validateNumbers(element) || (element.value.length !== 10)) {
                    element.focus();
                    const error=document.getElementById('errorMessageContactDiv');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid Phone Number of 10 digits.';
                    document.getElementById('errorMessageContactDiv').appendChild(errorMessage);
                }
                else{
                    flag = true;
                    document.getElementById('errorMessageContactDiv').style.display="";
                }
                break;

            case 'Aadhar':
                if (!validateNumbers(element) || !(element.value.length == 12)) {
                    element.focus();
                    const error=document.getElementById('errorMessageAadharDiv');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid Aadhar Number of 12 digits.';
                    document.getElementById('errorMessageAadharDiv').appendChild(errorMessage);
                }
                else{
                    flag = true;
                    document.getElementById('errorMessageAadharDiv').style.display="";
                }
                break;

            case 'CurrentPinCode':
                if (!validateNumbers(element) || !(element.value.length == 6)) {
                    element.focus();
                    const error=document.getElementById('errorMessageCurrentPinCodeDiv');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid PinCode of 6 digit';
                    document.getElementById('errorMessageCurrentPinCodeDiv').appendChild(errorMessage);
                }
                else{
                    flag = true;
                    document.getElementById('errorMessageCurrentPinCodeDiv').style.display="";
                }
                break;

            case 'PermanentPinCode':
                if (!validateNumbers(element) || !(element.value.length == 6)) {
                    element.focus();
                    const error=document.getElementById('errorMessagePermanentPinCodeDiv');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid PinCode of 6 digit';
                    document.getElementById('errorMessagePermanentPinCodeDiv').appendChild(errorMessage);                 
                }else{
                    flag = true;
                    document.getElementById('errorMessagePermanentPinCodeDiv').style.display="";
                }
                break;

            case 'YOP10':
                if (!validateNumbers(element) && !(element.value.length == 4)) {
                    element.focus();
                    const error=document.getElementById('errorMessageYOP10Div');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid YOP of 4 digit';
                    document.getElementById('errorMessageYOP10Div').appendChild(errorMessage);
                }else{
                    flag = true;
                    document.getElementById('errorMessageYOP10Div').style.display="";
                }
                break;

            case 'Aggregate10':
                if (!validateAggregate(element)) {
                    element.focus();
                    const error=document.getElementById('errorMessageAggregate10Div');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid Aggregrate';
                    document.getElementById('errorMessageAggregate10Div').appendChild(errorMessage);
                }else{
                    flag = true;
                    document.getElementById('errorMessageAggregate10Div').style.display="";
                }
                break;

            case 'YOP12':
                if (!validateNumbers(element) && !(element.value.length == 4)) {
                    element.focus();
                    const error=document.getElementById('errorMessageYOP12Div');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid YOP of 4 digit';
                    document.getElementById('errorMessageYOP12Div').appendChild(errorMessage);
                }else{
                    flag = true;
                    document.getElementById('errorMessageYOP12Div').style.display="";
                }
                break;

            case 'Aggregate12':
                if (!validateAggregate(element)) {
                    element.focus();
                    errorMessage.innerText = 'Please enter a valid Aggregrate';
                    document.getElementById('errorMessageAggregate12Div').appendChild(errorMessage);
                }else{
                    flag = true;
                    document.getElementById('errorMessageAggregate12Div').innerText="";
                }
                break;

            case 'AggregateG':
                if (!validateAggregate(element)) {
                    element.focus();
                    const error=document.getElementById('errorMessageAggregateGDiv');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a Aggregrate';
                    document.getElementById('errorMessageAggregateGDiv').appendChild(errorMessage);
                }else{
                    flag = true;
                    document.getElementById('errorMessageAggregateGDiv').style.display="";
                }
            break;

            case 'YOPG':
                if (!validateNumbers(element) && !(element.value.length == 4)) {
                    element.focus();
                    const error=document.getElementById('errorMessageYOPGDiv');
                    error.style.display="block";
                    error.innerText="";
                    errorMessage.innerText = 'Please enter a valid YOP of 4 digit';
                    document.getElementById('errorMessageYOPGDiv').appendChild(errorMessage);
                }else{
                    flag = true;
                    document.getElementById('errorMessageYOPGDiv').style.display="";
                }
            break;

            default:
                break;
        }

        data[element.getAttribute('data-store')] = value;
    }


  if(flag){
        alert("Data is good");
        return data;
  }else{
    alert('data is bad');
    return null;
  }

}


function clearErrorMessage(containerId) {
    // Clear error message by removing elements with class 'error-message'
    var errorContainer = document.getElementById(containerId);
    var errorMessages = errorContainer.getElementsByClassName('error-message');
    while (errorMessages.length > 0) {
        errorMessages[0].parentNode.removeChild(errorMessages[0]);
    }
}

function getErrorMessageContainerId(element) {
    // Provide the appropriate error message container id based on the data-store attribute
    switch (element.getAttribute('data-store')) {
        case 'Email':
            return 'errorMessageDisplayBox';
        case 'Password':
            return 'errorMessagePasswordDiv';
        // Add cases for other fields...
        default:
            return 'errorMessageDisplayBox'; // Default to a common container if not specified
    }
}


function storeData(data) {
    Object.keys(data).forEach(function (key) {
        localStorage.setItem(key, data[key]);
    });
}

function displayUserData() {
    var displayDiv = document.getElementById("displayData");
    displayDiv.innerHTML = "";

    var formData = document.querySelectorAll('#dataForm [data-store]');
    var tableHTML = '<table border="1" style="border-collapse: collapse; margin-bottom: 10px;"><tr>';

    formData.forEach(function (element, index) {
        var value = localStorage.getItem(element.getAttribute('data-store'));
        if (value) {
            tableHTML += `<td style="padding: 5px;">${element.getAttribute('data-store').charAt(0).toUpperCase() + element.getAttribute('data-store').slice(1)}: ${value}</td>`;
    
            // Check if this is the last element or the third element in a row
            if (index === formData.length - 1 || (index + 1) % 3 === 0) {
                tableHTML += '</tr>';
                // Start a new row if there are more elements
                if (index !== formData.length - 1) {
                    tableHTML += '<tr>';
                }
            }
        }
    });
    

    tableHTML += '</tr></table>';
    displayDiv.innerHTML = tableHTML;

    // Show the custom alert
    document.getElementById("customAlert").style.display = "block";
}

function closeAlert() {
    document.getElementById("customAlert").style.display = "none";
}

function clearForm() {
    document.getElementById("displayData").innerHTML = "";
}

function resetForm() {
    document.getElementById("dataForm").reset();
    removeErrorMessages();
}

function removeErrorMessages() {
    var errorMessages = document.querySelectorAll('.error-message');
    errorMessages.forEach(function (errorMessage) {
        errorMessage.parentNode.removeChild(errorMessage);
    });
}


function validateEmail(verifyEmail) {

    var mail = /^[a-z]*[A-Z]*[@][a-z]*[.][a-x]{3}/;

    if (verifyEmail.value.match(mail)) {
        return true;
    }
    else {
        return false;
    }
}

function validatePassword(parameter) {

    let verifyPassword = parameter;

    var vpass = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%])[A-Za-z\d@#$%]{8,15}$/;

    if (verifyPassword.value.match(vpass) && verifyPassword.value.length >= 8 && verifyPassword.value.length <= 15) {
        return true;
    } else {
        return false;
    }
}

function validateNumbers(parameter) {

    let verifyParameter = parameter;

    var numbers = /^[0-9]+$/;

    if (verifyParameter.value.match(numbers)) {
        return true;
    } else {
        return false;
    }
}

function validateAlphaNumeric(paramater) {

    var letters = /^[0-9a-zA-Z]+$/;
    let verifyParameter = paramater;

    if (verifyParameter.value.match(letters)) {
        return true;
    }
    else {
        return false;
    }
}

function validateAggregate(parameter) {

    var compare = /[0-9]+[.]*[0-9]*/;
    let verifyPara = parameter;

    if (verifyPara.value.match(compare)) {
        return true;
    } else {
        return false;
    }
}


function validateAggregate(parameter) {

    var compare = /[0-9]+[.]*[0-9]*/;
    let verifyParameter = parameter;

    if (verifyParameter.value.match(compare)) {
        return true;
    } else {
        return false;
    }
}

function copyCurrentToPermanent() {
    var sameAsCurrentCheckbox = document.getElementById('sameAsCurrent');
    var permanentAddressContainer = document.getElementById('permanentAddressContainer');
    var currentAddressFields = document.querySelectorAll('.container-AG [data-store^="Current"]');

    sameAsCurrentCheckbox.addEventListener('change', function() {
        if (this.checked) {
            // Copy current address fields to permanent address fields
            currentAddressFields.forEach(function(currentField) {
                var permanentField = document.querySelector('[data-store="Permanent' + currentField.dataset.store.substr(7) + '"]');
                if (permanentField) {
                    permanentField.value = currentField.value;
                }
            });
        } else {
            // Clear permanent address fields
            currentAddressFields.forEach(function(currentField) {
                var permanentField = document.querySelector('[data-store="Permanent' + currentField.dataset.store.substr(7) + '"]');
                if (permanentField) {
                    permanentField.value = '';
                }
            });
        }
    });
}

function updateStates(countryDropdownId, stateDropdownId) {
    var countryDropdown = document.getElementById(countryDropdownId);
    var stateDropdown = document.getElementById(stateDropdownId);

    // Get the selected country value
    var selectedCountry = countryDropdown.value;

    // Clear existing options in the state dropdown
    stateDropdown.innerHTML = '<option value="" disabled selected>Select State</option>';

    // Define states based on the selected country
    var states = {
        india: ['Delhi', 'Kolkata', 'Mumbai', 'Odisha'],
        canada: ['Ontario', 'British Columbia', 'Alberta', 'Quebec'],
        unitedKingdom: ['England', 'Scotland', 'Wales', 'Northern Ireland'],
        australia: ['New South Wales', 'Victoria', 'Queensland', 'Western Australia']
    };

    // Populate the state dropdown with options based on the selected country
    if (selectedCountry in states) {
        states[selectedCountry].forEach(function (state) {
            var option = document.createElement('option');
            option.value = state.toLowerCase();
            option.textContent = state;
            stateDropdown.appendChild(option);
        });
    }
}
