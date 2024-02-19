<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="DemoUserManagement.web.SignUp1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="Scripts/CheckEmail.js"></script>
    <link href="Content/SignUpStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container">
        <h2>LOG IN</h2>
        <p>
            <label for="txtEmail">Email ID:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="lblEmailError" runat="server" CssClass="error-message"></asp:Label>
        </p>
        <p>
            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </p>
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        <div class="button-container">
            <asp:Button ID="btnSignUp" runat="server" Text="SignUp" OnClick="BtnSignup_Click"/>
            <asp:Button ID="btnLogin" runat="server" Text="LogIn" OnClick="BtnLogin_Click" />
        </div>
        
    </form>
</body>
</html>
