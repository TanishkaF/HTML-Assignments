<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentUserControlV2.ascx.cs" Inherits="DemoUserManagement.web.DocumentUserControlV2" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" type="text/css" href="Content/UserListGridViewStyleSheet.css" />
</head>
<body>

    <div class="row">
        <div class="col-sm-4">
            <div class="p-3">
                <label for="ddlOptions" class="toggleClass">Select Option:</label>
                <select id="ddlOptions" class="form-control"></select>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="p-3">
                <label for="fileInput" class="toggleClass">Upload PDF:</label>
                <input type="file" id="fileInput" />
            </div>
        </div>
        <div class="col-sm-4">
            <div class="p-3">
                <label></label>
                <button id="btnAddDocument" class="btn btn-primary">Add Document</button>
            </div>
        </div>
    </div>

    <%--<asp:GridView
        class="GridViewUsers"
        ID="GridViewDocuments"
        runat="server"
        AutoGenerateColumns="False"
        AllowSorting="True"
        AllowPaging="True"
        AllowCustomPaging="True"
        OnPageIndexChanging="GridViewDocuments_PageIndexChanging"
        OnSorting="GridViewDocuments_Sorting"
        OnRowCommand="GridViewDocuments_RowCommand"
        PageSize="2"
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
            <asp:Button ID="btnDownload" runat="server" Text="Download" CommandName="DownloadFile" CommandArgument='<%# Eval("DocumentID") %>' />
        </ItemTemplate>
    </asp:TemplateField>

        </Columns>
    </asp:GridView>--%>

    <asp:UpdatePanel ID="UpdatePanelGridViewDocuments" runat="server">
    <ContentTemplate>
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
            OnRowCommand="GridViewDocuments_RowCommand"
            PageSize="2"
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
                        <asp:Button ID="btnDownload" runat="server" Text="Download" CommandName="DownloadFile" CommandArgument='<%# Eval("DocumentID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>




    <input type="hidden" id="hiddenObjectID" value="<%= ObjectID %>" runat="server"/>
    <input type="hidden" id="hiddenObjectType" value="<%= ObjectType %>" runat="server" />
    <input type="hidden" id="hiddenDocumentType" runat="server"/>

</body>
</html>





<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" type="text/css" href="Content/UserListGridViewStyleSheet.css" />

</head>
<body>

    <div class="row">
        <div class="col-sm-4">
            <div class="p-3">
                <asp:Label ID="lblDdlOptions" for="ddlOptions" runat="server">Select Option:</asp:Label>
                <asp:DropDownList ID="ddlOptions" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="p-3">
                <asp:Label for="fileUpload" ID="lblFileUpload" runat="server">Upload PDF:</asp:Label>
                <%--                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" ClientIDMode="Static" />--%>
<%--<input type="file" id="fileInput" />

            </div>
        </div>
        <div class="col-sm-4">
            <div class="p-3">
                <label></label>
                <button id="btnAddDocument" class="btn btn-primary">Add Document</button>
           </div>
        </div>
    </div>
    <input type="hidden" id="hiddenObjectID" value="<%= ObjectID %>" />
    <input type="hidden" id="hiddenObjectType" value="<%= ObjectType %>" />
    <input type="hidden" id="hiddenDocumentType" />


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
</html>--%>
