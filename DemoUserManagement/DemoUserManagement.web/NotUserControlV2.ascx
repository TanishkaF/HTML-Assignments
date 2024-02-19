<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NotUserControlV2.ascx.cs" Inherits="DemoUserManagement.web.NotUserControlV2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Note User Control</title>
    <link href="Content/UserListGridViewStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <input type="text" id="txtNote" class="centered-textbox larger-textbox" style="margin:10px">
    <button id="btnAddNote">Add Note</button>
    
    <table id="GridViewDocuments">
        <thead>
            <tr>
                <th>NoteID</th>
                <th>Object ID</th>
                <th>Object Type</th>
                <th>Note Text</th>
                <th>TimeStamp</th>
            </tr>
        </thead>
        <tbody></tbody>
    </table>

    <script src="noteUserControl.js"></script>
</body>
</html>
