$(document).ready(function () {
    $("#btnSignUp").click(function () {
        window.location.href = "/User/UserView";
    });

    $("#btnLogin").click(function () {
        var email = $("#txtEmail").val();
        var password = $("#txtPassword").val();

        $.ajax({
            url: "/LogIn/ValidateUser",
            type: "POST",
            data: { email: email, password: password },
            success: function (response) {
                if (response.success) {
                    window.location.href = "/Airport/AirportView";
                } else {
                    $("#lblError").text(response.errorMessage);
                }
            },
            error: function (xhr, status, error) {
                $("#lblError").text(response.errorMessage);
            }
        });
    });


    var currentPageUrl = window.location.pathname;

    if (currentPageUrl.includes("AirportView")) {
        $("#airport-link").addClass("active-link");
    }
    if (currentPageUrl.includes("AircraftView")) {
        $("#aircraft-link").addClass("active-link");
    }
    if (currentPageUrl.includes("TransactionView")) {
        $("#transaction-link").addClass("active-link");
    }
    if (currentPageUrl.includes("AirportFuelView")) {
        $("#airport-fuel-link").addClass("active-link");
    }
    if (currentPageUrl.includes("/GetFuelConsumptionReport/GetFuelConsumptionReportView")) {
        $("#fuel-consumption-report-link").addClass("active-link");
    }

    else  {
        $("#log-in-link").addClass("active-link");
    }


});
