<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comapre.aspx.cs" Inherits="AspWebAppPratice2.Comapre" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="input1" runat="server" Text="enter num1"></asp:Label>
            <asp:TextBox ID="firstval" runat="server"></asp:TextBox>

            <asp:Label ID="Lab1" runat="server" Text="enter number2"></asp:Label>
            <asp:TextBox ID="secondval" runat="server"></asp:TextBox>

            <asp:CompareValidator ID="CompareValidator1" runat="server"
                ControlToValidate="firstval"
                ControlToCompare="secondval"
                ErrorMessage="Enter valid value as first one should be less than second one"
                ForeColor="Red"
                Operator="LessThan"
                Type="Integer">
            </asp:CompareValidator>

            <asp:Button ID="Button1" runat="server" Text="Compare" OnClick="clickCompare" />
            <asp:Label ID="showSum" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
