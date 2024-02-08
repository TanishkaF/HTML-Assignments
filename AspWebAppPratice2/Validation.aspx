<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <div>
        </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">User Name</td>
                <td>
                    <asp:TextBox ID="username" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="user" runat="server" ControlToValidate="username"
                        ErrorMessage="Please enter a user name" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Password</td>
                <td>
                    <asp:TextBox ID="password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="pass" runat="server" ControlToValidate="password"
                        ErrorMessage="Please enter a password" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="login" />
                </td>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                    <br />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
