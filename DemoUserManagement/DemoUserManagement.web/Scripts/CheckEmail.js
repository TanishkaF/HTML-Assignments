$(document).ready(function () {
    $('#email').blur(function () {
        var email = $('#email').val();
        $.ajax({
            type: "POST",
            url: "SignUp.aspx/CheckEmail",
            data: JSON.stringify({ email: email }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response && response.d) {
                    console.log("Email exists.");
                    $('#lblEmailError').text("Email exists.");
                } else {
                    console.log("Email does not exist.");
                    $('#lblEmailError').text("Email does not exist.");
                }
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
                $('#lblEmailError').text("Error checking email.");
            }
        });
    });

});