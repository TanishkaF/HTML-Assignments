<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sum.aspx.cs" Inherits="AspWebAppPratice2.Sum" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="label1" runat="server" Text="Enter First Number:"></asp:Label>
            <asp:TextBox ID="input1" runat="server" required="True"></asp:TextBox>
        </div> 
                <br />
        <div>
            <asp:Label ID="label2" runat="server" Text="Enter Second Number:"></asp:Label>
            <asp:TextBox ID="input2" runat="server" required="True"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="AddNumber" Text ="Submit"/>
        </div>
        <div>
            <asp:Label ID ="showSum" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
