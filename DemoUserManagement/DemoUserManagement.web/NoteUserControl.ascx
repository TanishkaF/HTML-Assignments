t<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoteUserControl.ascx.cs" Inherits="DemoUserManagement.web.NoteUserControl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<body>
        <link href="Content/UserListGridViewStyleSheet.css" rel="stylesheet" />

    <asp:TextBox ID="txtNote" runat="server" CssClass="centered-textbox larger-textbox" style="margin:10px"></asp:TextBox>

    
      <asp:UpdatePanel ID="UpdatePanelGridViewDocuments" runat="server">
    <ContentTemplate>
            <asp:Button ID="btnAddNote" runat="server" Text="Add Note" OnClick="BtnAddNote_Click" ClientIDMode="static" />

        <asp:GridView
            ID="GridViewDocuments"
            runat="server"
            AutoGenerateColumns="False"
            AllowSorting="True"
            AllowPaging="True"
            AllowCustomPaging="True"
            OnPageIndexChanging="GridViewDocuments_PageIndexChanging"
            OnSorting="GridViewDocuments_Sorting"
            PageSize="5"
            DataKeyNames="NoteID">
            <Columns>
                <asp:BoundField DataField="NoteID" HeaderText="NoteID" SortExpression="NoteID" />
                <asp:BoundField DataField="ObjectID" HeaderText="Object ID" SortExpression="ObjectID" />
                <asp:BoundField DataField="ObjectType" HeaderText="Object Type" SortExpression="ObjectType" />
                <asp:BoundField DataField="NoteText" HeaderText="Note Text" SortExpression="NoteText" />
                <asp:TemplateField HeaderText="TimeStamp" SortExpression="TimeStamp">
                    <ItemTemplate>
                        <%# Eval("TimeStampFormatted") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="GridViewDocuments" EventName="PageIndexChanging" />
        <asp:AsyncPostBackTrigger ControlID="GridViewDocuments" EventName="Sorting" />
    </Triggers>
</asp:UpdatePanel>

    
</body>
</html>
