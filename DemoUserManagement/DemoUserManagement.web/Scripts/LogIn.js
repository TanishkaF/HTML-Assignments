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
                    // Set session variables
                    $.ajax({
                        type: 'POST',
                        url: 'LogIn.aspx/SetSessionVariables',
                        data: JSON.stringify({ email: email }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function () {
                            // After setting session variables, determine if user is admin or not
                            $.ajax({
                                type: 'POST',
                                url: 'LogIn.aspx/IsAdmin',
                                data: JSON.stringify({ email: email }),
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                success: function (isAdminResponse) {
                                    if (isAdminResponse.d) {
                                        console.log('User is an admin');
                                        window.location.href = 'UserList.aspx';
                                    } else {
                                        // Get the user ID and redirect to UserDetails.aspx
                                        $.ajax({
                                            type: 'POST',
                                            url: 'LogIn.aspx/GetUserID',
                                            data: JSON.stringify({ email: email }),
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json',
                                            success: function (userIDResponse) {
                                                console.log('User ID:', userIDResponse.d);
                                                window.location.href = 'UserDetails.aspx?StudentID=' + userIDResponse.d;
                                            },
                                            error: function (error) {
                                                console.log('Error getting user ID:', error);
                                            }
                                        });
                                    }
                                },
                                error: function (error) {
                                    console.log('Error checking if user is admin:', error);
                                }
                            });
                        },
                        error: function (error) {
                            console.log('Error setting session variables:', error);
                        }
                    });
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
        window.location.href = 'UserDetails.aspx';
    });
});
