<%@ Page Language="C#" Title="User's List" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="DemoUserManagement.web.UserList" MasterPageFile="~/Site.Master"%>


<asp:Content ID="BodyContent1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/UserListGridViewStyleSheet.css" rel="stylesheet" />
    <main  id ="GridViewUsers2">
    <h1>Users List</h1>
    <asp:GridView 
        Class="GridViewClass"
        ID="GridViewUsers"
        runat="server"
        AutoGenerateColumns="False"
        AllowPaging="True"
        AllowSorting="True"
        AllowCustomPaging="True"
        PageSize="5"
        OnPageIndexChanging="GridViewUsers_PageIndexChanging"
        OnSorting="GridViewUsers_Sorting"
        OnRowCommand="GridViewUsers_RowCommand"
        DataKeyNames="StudentID"
        ClientIDMode="Static">
        <Columns>
            <asp:BoundField DataField="StudentID" HeaderText="Student ID" SortExpression="StudentID" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
            <asp:BoundField DataField="AadharNumber" HeaderText="Aadhar" SortExpression="AadharNumber" />

            <%--<asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="downloadLink" runat="server" Text="Download" OnClick="Button_Click" ClientIDMode="Static" CommandArgument='<%# Eval("DiskDocumentName") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditUser" ClientIDMode="Static" CommandArgument='<%# Eval("StudentID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button ID="btnAddStudent" runat="server" Text="Add More Student" ClientIDMode="Static" OnClick="BtnAddStudent_Click" />
        </main>
</asp:Content>