<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoteUserControlV2.ascx.cs" Inherits="DemoUserManagement.web.NoteUserControlV2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="Content/UserListGridViewStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <input type="hidden" id="hiddenObjectID" value="<%= ObjectID %>" name="hiddenObjectID" />
    <input type="hidden" id="hiddenObjectType" value="<%= ObjectType %>" name="hiddenObjectType"/>
    <%--<input type="hidden" id="hiddenObjectID"  value="<%= ObjectID %>" runat="server" />
    <input type="hidden" id="hiddenObjectType" value="<%= ObjectType %>" runat="server"  />--%>

    <input type="text" id="txtNote" class="centered-textbox larger-textbox" style="margin:10px" />
    <input type="button" id="btnAddNote" value="Add Note" onclick="return addNote();" />
    
    <asp:UpdatePanel ID="UpdatePanelGridViewDocuments" runat="server">
        <ContentTemplate>
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
