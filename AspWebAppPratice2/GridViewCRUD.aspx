<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridViewCRUD.aspx.cs" Inherits="AspWebAppPratice2.GridViewCRUD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridViewCRUD</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>STUDENT TABLE</h1>
            <asp:GridView ID="GridView1" runat="server" 
                AutoGenerateColumns="False" 
                DataSourceID="SqlDataSource1"
                AllowSorting="True" 
                OnSorting="SortingGridView" 
                DataKeyNames="StudentID">
                <Columns>
                    <asp:BoundField DataField="StudentID" HeaderText="ID" SortExpression="StudentID" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                            <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GridViewConnectionString %>"
                SelectCommand="SELECT * FROM [Student]"
                UpdateCommand="UPDATE [Student] SET [FirstName]=@FirstName, [LastName]=@LastName WHERE [StudentID]=@StudentID"
                DeleteCommand="DELETE FROM [Student] WHERE [StudentID]=@StudentID"></asp:SqlDataSource>
            <br />
            <div>
                <label>First Name</label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </div>
            <div>
                <label>Last Name</label>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="Button1" runat="server" Text="Insert" OnClick="InsertButton" />
                <asp:Button ID="Button2" runat="server" Text="Reset" OnClick="ResetButton" />
            </div>
        </div>
    </form>
</body>
</html>