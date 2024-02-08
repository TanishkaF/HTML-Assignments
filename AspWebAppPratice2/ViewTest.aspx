<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewTest.aspx.cs" Inherits="AspWebAppPratice2.ViewTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
        <p>
            UserName: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br /><br />
            Password: <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Submit" />
            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Restore" />
        </p>
    </form>
</body>
</html>
