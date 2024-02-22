$(document).ready(function () {
    $('#btnLogin').click(function () {
        var email = $('#txtEmail').val().trim();
        var password = $('#txtPassword').val().trim();

        $.ajax({
            type: 'POST',
            url: 'LogIn.aspx/ValidateUser',
            data: JSON.stringify({ email: email, password: password }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {
                if (response.d) {
                    // Check if user is admin from session
                    var isAdmin = response.d.IsAdmin;
                    console.log(isAdmin);
                    if (isAdmin) {
                        window.location.href = 'UserList.aspx';
                    } else {
                        var userID = response.d.UserID;
                        window.location.href = 'UserDetailsV2.aspx?StudentID=' + userID;
                    }
                } else {
                    $('#lblError').text('Invalid combination of Email ID with Password.');
                }
            },
            error: function (error) {
                console.log('Error validating user:', error);
            }
        });
    });

    $('#btnSignUp').click(function () {
        //window.location.href = 'SignUp.aspx';
        window.location.href = 'UserDetailsV2.aspx';
    });
});