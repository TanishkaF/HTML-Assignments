<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="DemoUserManagement.web.UserList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>This is the list of all students register till date.</h1>
        <asp:GridView ID="GridViewUsers"
            runat="server"
            AutoGenerateColumns="False"
            AllowPaging="True"
            AllowSorting="True"
            AllowCustomPaging="True"
            PageSize="2"
            OnPageIndexChanging="GridViewUsers_PageIndexChanging"
            OnSorting="GridViewUsers_Sorting"
            OnRowCommand="GridViewUsers_RowCommand"
            DataKeyNames="StudentID">
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                <asp:BoundField DataField="AadharNumber" HeaderText="Aadhar" SortExpression="AadharNumber" />
                <asp:BoundField DataField="Country" HeaderText="Country of Current Address" SortExpression="Country" />
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditUser" CommandArgument='<%# Eval("StudentID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Button ID="btnAddStudent" runat="server" Text="Add More Student" OnClick="BtnAddStudent_Click" />

    </form>
</body>
</html>
