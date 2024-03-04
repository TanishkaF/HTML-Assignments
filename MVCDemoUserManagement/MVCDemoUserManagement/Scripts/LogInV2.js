$(document).ready(function () {
    $("#btnSignUp").click(function () {
        // Redirect to SignUp action
        window.location.href = "/UserDetailsV2/UserDetailsV2?StudentID=0";
    });

   

    $("#btnLogin").click(function () {
        var email = $("#txtEmail").val();
        var password = $("#txtPassword").val();

        $.ajax({
            url: "/LogInV2/ValidateUserV2", 
            type: "POST",
            data: { email: email, password: password },
            success: function (response) {
                if (response.success) { // Check for success field
                    window.location.href = response.isAdmin ? "/UserListV2/UserListV2" : "/UserDetailsV2/UserDetailsV2?StudentID=" + response.userID; // Updated redirection based on isAdmin flag
                } else {
                    $("#lblError").text(response.errorMessage); // Display error message
                }
            },
            error: function (xhr, status, error) {
                $("#lblError").text("Invalid combination of Email ID with Password.");
            }
        });
    });


});
