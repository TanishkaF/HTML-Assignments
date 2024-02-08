<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo_LifeCycle1.aspx.cs" Inherits="AspWebAppPratice2.Demo_LifeCycle1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Label runat="server" ID="lblName" />
        </div>
    </form>
</body>
</html>
