<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoteUserControl.ascx.cs" Inherits="DemoUserManagement.web.NoteUserControl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <link rel="stylesheet" href="index.css" />
    <style>
        .centered-textbox {
            margin: 10px;
        }
    </style>
</head>
<body>
    <asp:TextBox ID="txtNote" runat="server" CssClass="centered-textbox larger-textbox"></asp:TextBox>
    <asp:Button ID="btnAddNote" runat="server" Text="Add Note" OnClick="BtnAddNote_Click" />

    <div>
       <asp:GridView
    ID="GridViewNotes"
    runat="server"
    AutoGenerateColumns="False"
    AllowSorting="True"
    AllowPaging="True"
    AllowCustomPaging="True"
    OnPageIndexChanging="GridViewNotes_PageIndexChanging"
    OnSorting="GridViewNotes_Sorting"
    PageSize="5"
    DataKeyNames="NoteID">    
            <Columns>
                <asp:BoundField DataField="NoteID" HeaderText="NoteID" SortExpression="NoteID" />
                <asp:BoundField DataField="ObjectID" HeaderText="Object ID" SortExpression="ObjectID" />
                <asp:BoundField DataField="ObjectType" HeaderText="Object Type" SortExpression="ObjectType" />
                <asp:BoundField DataField="NoteText" HeaderText="Note Text" SortExpression="NoteText" />
                <asp:BoundField DataField="TimeStamp" HeaderText="TimeStamp" SortExpression="TimeStamp" />
            </Columns>
        </asp:GridView>
    </div>
</body>
</html>
