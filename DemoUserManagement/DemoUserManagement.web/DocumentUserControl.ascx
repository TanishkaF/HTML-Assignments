<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentUserControl.ascx.cs" Inherits="DemoUserManagement.web.DocumentUserControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" type="text/css" href="Content/UserListGridViewStyleSheet.css" />
</head>
<body>
    <div class="row">
        <div class="col-sm-4">
            <div class="p-3">
                <asp:label id="lblDdlOptions" for="ddlOptions" runat="server">Select Option:</asp:label>
                <asp:DropDownList ID="ddlOptions" runat="server" CssClass="form-control"></asp:DropDownList>
             </div>
        </div>
        <div class="col-sm-4">
            <div class="p-3">
                <asp:label for="fileUpload" id="lblFileUpload" runat="server">Upload PDF:</asp:label>
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" ClientIDMode="Static" />
            </div>
        </div>
        <div class="col-sm-4">
            <div class="p-3">
                <label></label>
                <asp:Button ID="btnAddDocument" runat="server" Text="Add Document" OnClick="BtnAddDocument_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>



    <asp:GridView
        class="GridViewUsers"
        ID="GridViewDocuments"
        runat="server"
        AutoGenerateColumns="False"
        AllowSorting="True"
        AllowPaging="True"
        AllowCustomPaging="True"
        OnPageIndexChanging="GridViewDocuments_PageIndexChanging"
        OnSorting="GridViewDocuments_Sorting"
        PageSize="5"
        DataKeyNames="DocumentID"
        ClientIDMode="Static">
        <Columns>
            <asp:BoundField DataField="DocumentID" HeaderText="Document ID" SortExpression="DocumentID" />
            <asp:BoundField DataField="ObjectID" HeaderText="Object ID" SortExpression="ObjectID" />
            <asp:BoundField DataField="ObjectType" HeaderText="Object Type" SortExpression="ObjectType" />
            <asp:BoundField DataField="DocumentType" HeaderText="Document Type" SortExpression="DocumentType" />
            <asp:BoundField DataField="OriginalDocumentName" HeaderText="Original Name" SortExpression="OriginalDocumentName" />
            <asp:TemplateField HeaderText="Time Stamp" SortExpression="TimeStamp">
                <ItemTemplate>
                    <%# Eval("TimeStampFormatted") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <asp:LinkButton ID="downloadLink" runat="server" Text="Download" OnClick="Button_Click" ClientIDMode="Static" CommandArgument='<%# Eval("DiskDocumentName") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>
    </asp:GridView>

</body>
</html>
