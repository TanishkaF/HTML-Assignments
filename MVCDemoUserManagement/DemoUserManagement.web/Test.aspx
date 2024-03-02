<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="DemoUserManagement.web.Test" %>
<%@ Register Src="~/DocumentUserControl.ascx" TagPrefix="uc1" TagName="DocumentUserControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Bootstrap CSS link -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/UserListGridViewStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <uc1:DocumentUserControl runat="server" id="DocumentUserControl" ClientIDMode="Static"/>
        </div>
    </form>
</body>
</html>
