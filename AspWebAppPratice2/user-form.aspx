<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user-form.aspx.cs" Inherits="AspWebAppPratice2.user_form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            margin-left: 0px;
        }

        .auto-style3 {
            width: 121px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="labelId" runat="server">User Name</asp:Label>
            <asp:TextBox ID="UserName" runat="server" ToolTip="Enter User Name"></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="Label2" runat="server"> Name</asp:Label>
            <asp:TextBox ID="LastName" runat="server" ToolTip="Enter LastName"></asp:TextBox>
        </div>
        <div>
            <asp:RadioButton ID="RadioButton1" runat="server" Text="Male" GroupName="gender"></asp:RadioButton>
            <asp:RadioButton ID="RadioButton2" runat="server" Text="FeMale" GroupName="gender"></asp:RadioButton>
        </div>
        <div>
            <asp:CheckBox ID="CheckBox1" runat="server" Text="J2SE" />
            <asp:CheckBox ID="CheckBox2" runat="server" Text="J2EE" />
            <asp:CheckBox ID="CheckBox3" runat="server" Text="Spring" />
        </div>
        <div>
            <p>Select a City of Your Choice</p>
            <div>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="">Please Select</asp:ListItem>
                    <asp:ListItem>New Delhi </asp:ListItem>
                    <asp:ListItem>Greater Noida</asp:ListItem>
                    <asp:ListItem>NewYork</asp:ListItem>
                    <asp:ListItem>Paris</asp:ListItem>
                    <asp:ListItem>London</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div>
            
        </div>
        <p>
            <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
        </p>
        <br />

    </form>

    <asp:Label ID="userInput" runat="server"></asp:Label>
    <asp:Label ID="lastInput" runat="server"></asp:Label>
    <asp:Label ID="genderInput" runat="server"></asp:Label>
    <asp:Label ID="checkBoxInput" runat="server"></asp:Label>
    <asp:Label ID="DropDownInput" runat="server"></asp:Label>

</body>

</html>
