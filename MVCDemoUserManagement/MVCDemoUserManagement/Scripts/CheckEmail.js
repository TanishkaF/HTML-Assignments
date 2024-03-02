$(document).ready(function () {
    $('#email').blur(function () {
        var email = $('#email').val();
       // var userID = getParameterByName('StudentID');
        var userID = getParameterByName('StudentID'); // Adjust parameter name here

        if (userID == null) {
            checkEmailOnly(email);
        } else {
            checkCombination(email, userID);
        }
    });
});

function checkCombination(email, userID) {
    var checkEmailUrl = $('#checkEmailUrl').val(); // Get the URL from the hidden input field
    $.ajax({
        type: "POST",
        url: checkEmailUrl,
        data: JSON.stringify({ email: email, userID: userID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response) {
                console.log("Error: URL StudentID and stored UserID do not match.");
                $('#lblEmailError').text("Email exists. Please use a different email address.");
                $('#submitButton').prop('disabled', true);
            } else {
                $('#lblEmailError').text("");
                $('#submitButton').prop('disabled', false);
            }
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
            $('#lblEmailError').text("Error checking email.");
            $('#submitButton').prop('disabled', true);
        }
    });
}

function checkEmailOnly(email) {
    var checkEmailUrl = $('#checkEmailUrl').val(); // Get the URL from the hidden input field
    $.ajax({
        type: "POST",
        url: checkEmailUrl,
        data: JSON.stringify({ email: email, userID: 0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response) {
                console.log("Email exists.");
                $('#lblEmailError').text("Email exists. Please use a different email address.");
                $('#submitButton').prop('disabled', true);
            } else {
                $('#lblEmailError').text("");
                $('#submitButton').prop('disabled', false);
            }
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
            $('#lblEmailError').text("Error checking email.");
            $('#submitButton').prop('disabled', true);
        }
    });
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}