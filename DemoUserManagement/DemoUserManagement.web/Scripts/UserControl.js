$(document).ready(function () {
    getDocumentList();
});

function addNote() {
    var noteData = document.getElementById('txtNote').value;

    var objectID = document.getElementById('hiddenObjectID').value;
    var objectType = document.getElementById('hiddenObjectType').value;
   
    var note = {
        ObjectID: objectID,
        ObjectType: objectType,
        NoteText: noteData
    };
    
    $.ajax({
        type: 'POST',
        url: 'LogIn.aspx/InsertNote', 
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ note: note }),
        dataType: 'json',
        success: function (response) {
            document.getElementById('txtNote').value = '';          
        },
        error: function (xhr, status, error) {
            console.error('Error adding note:', error);
           
        }
    });
    return false;
}

//function toggleNoteControls() {

//    $('#txtNote').toggle();
//    $('#btnAddNote').toggle();
//    $('ddlOptions').toggle();
//    $('fileInput').toggle();
//    $('btnAddDocument').toggle();

//}

function getDocumentList() {
    $.ajax({
        url: 'LogIn.aspx/PopulateDocumentDropDownList',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        success: function (response) {
            if (response && response.d) {
                var documents = response.d; // No need to parse JSON here
                PopulateDocumentDropDownList('ddlOptions', documents);
            } else {
                console.error("Invalid response from server");
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching document list:", error); // Provide a meaningful error message
        }
    });
}

function PopulateDocumentDropDownList(dropDownId, documents) {
    var dropDown = $('#' + dropDownId);
    dropDown.empty();
    dropDown.append($('<option>').val('').text('Select Document').prop('disabled', true).prop('selected', true)); // Update default text

    $.each(documents, function (index, document) {
        var option = $('<option>').val(document.documentType).text(document.documentName);
        dropDown.append(option);
    });
}

$('#btnAddDocument').click(function () {
        var formData = new FormData();
        formData.append('file', $('#fileInput')[0].files[0]);
        var documentType = $('#ddlOptions').val();
        $.ajax({
            url: 'UploadFile.ashx',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                
                    var uploadedFileName = response.DiskDocumentName;
                    var originalFileName = response.OriginalFileName;

                    // Assuming you have these variables available
                    var objectID = document.getElementById('hiddenObjectID').value;
                    var objectType = document.getElementById('hiddenObjectType').value;
                    //var documentType = document.getElementById('hiddenDocumentType').value;

                    var documentObject = {
                        ObjectID: objectID,
                        ObjectType: objectType,
                        DocumentType: documentType,
                        DiskDocumentName: uploadedFileName,
                        OriginalDocumentName: originalFileName
                    };

                    insertDocument(documentObject);
                
            },
            error: function (xhr, status, error) {
                console.error('Error uploading file:', error);
            }
        });
        return false;
});


function insertDocument(document) {
    
    $.ajax({
        url: 'LogIn.aspx/InsertDocument', 
        type: 'POST',
        data: JSON.stringify({ document: document }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            console.log('Document inserted successfully:', response);
           // BindGridView();
        },
        error: function (xhr, status, error) {
            console.error('Error inserting document:', error);
        }
    });
}