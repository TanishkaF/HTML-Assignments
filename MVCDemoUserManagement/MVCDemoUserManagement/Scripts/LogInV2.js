$(document).ready(function () {
    $("#btnSignUp").click(function () {
        // Redirect to SignUp action
        window.location.href = "/UserDetailsV2/UserDetailsV2?StudentID=0";
    });

    $("#btnLogin").click(function () {
        var email = $("#txtEmail").val();
        var password = $("#txtPassword").val();

        $.ajax({
            url: "LogInV2/ValidateUserV2",
            type: "POST",
            data: { email: email, password: password }, // Pass email and password as data
            success: function (response) {
                if (response.success) {
                    window.location.href = "/UserListV2/UserListV2";
                } else {
                    window.location.href = "/UserDetailsV2/UserDetailsV2?StudentID=5";
                }
            },
            error: function (xhr, status, error) {
                $("#lblError").text("Invalid combination of Email ID with Password.");
            }
        });
    });

});
