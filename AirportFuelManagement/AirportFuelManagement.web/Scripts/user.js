$(document).ready(function () {

    $('#EmailID').blur(function () {
        var email = $('#EmailID').val();
        checkEmailOnly(email);
    });

    $("#addUser").click(function () {

        var name = $("#Name").val();
        var email = $("#EmailID").val();
        var password = $("#Password").val();

        var user = {
            Name: name,
            EmailID: email,
            Password: password
        };

        $.ajax({
            url: '/User/AddUser',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(user),
            success: function (response) {
                if (response.success) {
                    alert('User added successfully.');
                    $("#userForm")[0].reset();

                } else {
                    alert('Failed to add user: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                alert('Error occurred while adding user: ' + error);
            }
        });
    });
    
    $("#resetForm").click(function () {
        $("#userForm")[0].reset();
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
        url: "/User/CheckEmail",
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