<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="DemoUserManagement.web.LogIn" %>

<!DOCTYPE html>
<html>
<head>
    <title>Sign Up</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="Scripts/CheckEmail.js"></script>
    <link href="Content/SignUpStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <h2>LOG IN</h2>
        <p>
            <label for="txtEmail">Email ID:</label>
            <input type="email" id="txtEmail" class="form-control" />
            <span id="lblEmailError" class="error-message"></span>
        </p>
        <p>
            <label for="txtPassword">Password:</label>
            <input type="password" id="txtPassword" class="form-control" />
        </p>
        <span id="lblError" style="color: red;"></span>
        <div class="button-container">
            <button id="btnSignUp">SignUp</button>
            <button id="btnLogin">LogIn</button>
        </div>
    </div>
    <script src="Scripts/LogIn.js"></script>
</body>
</html>
