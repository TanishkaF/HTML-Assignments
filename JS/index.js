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

        formData.forEach(function (element) {
            
            var value = element.value.trim();
            if (value.length === 0) {
                alert("Please fill out all the fields.");
                data = null;
                return;
            }
            data[element.getAttribute('data-store')] = value;
        });

        return data;
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

            // Start a new row after every 3 columns
            if ((index + 1) % 3 === 0) {
                tableHTML += '</tr><tr>';
            }
        }
    });

    tableHTML += '</tr></table>';
    displayDiv.innerHTML = tableHTML;

    // Show the custom alert
    document.getElementById("customAlert").style.display = "block";
}

function closeAlert() {
    // Close the custom alert
    document.getElementById("customAlert").style.display = "none";
}

function clearForm() {
    // Clear the content within the customAlert
    document.getElementById("displayData").innerHTML = "";
}

function resetForm() {
    // Clear specific input fields
    document.getElementById("dataForm").reset();
}
