$(document).ready(function () {
    $.ajax({
        url: '/DocumentUserV2/PopulateDropdown', 
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var options = $('#ddlOptions');
            $.each(data, function (index, item) {
                options.append($('<option>').val(item.documentType).text(item.documentName));
            });
        },
        error: function () {
            console.log('Error occurred while fetching document options.');
        }
    });
    loadDocumentPartialView();
});


function loadDocumentPartialView(pageIndex = $("#pageIndexDocument").val(), pageSize = $("#pageSizeDocument").val(),
    sortExpression = $("#sortExpressionDocument").val(), sortDirection = $("#sortDirectionDocument").val()) {
    var objectId = $("#objectId").val();
    var objectType = $("#objectType").val();

    $.ajax({
        type: "GET",
        url: "/DocumentUserV2/GetDocuments",
        data: { objectId: objectId, objectType: objectType, pageIndex: pageIndex, pageSize: pageSize, sortExpression: sortExpression, sortDirection: sortDirection },
        dataType: "json",
        success: function (data) {
            $("#documentTable tbody").empty();
            $.each(data, function (index, item) {
                var row = "<tr><td>" + item.DocumentID + "</td><td>" + item.ObjectID + "</td><td>" + item.DocumentType + "</td><td>" + item.OriginalDocumentName + "</td><td>" + formatDate(item.Timestamp) + "</td>";
                row += "<td><a href='/File/DownloadFile?documentId=" + item.DocumentID + "' class='download-link' data-document-id='" + item.DocumentID + "'>Download</a></td></tr>"; 
                //row += "<td><a href='/File/DownloadFile?documentId = ',+ item.DocumentId, class='download-link' data-document-id='" + item.DocumentID + "'>Download</a></td></tr>"; 
                $("#documentTable tbody").append(row);
            });
            renderPagination(data[0].PageIndex, data[0].TotalPages);
        },
        error: function () {
            console.log("Error");
        }
    });
}

function formatDate(timestamp) {
    var date = new Date(parseInt(timestamp.substr(6)));
    return date.toLocaleString(); // Adjust the date format as needed
}

function renderPagination(currentPageIndex, totalPages) {
    $("#paginationDocument").empty();
    for (var i = 0; i < totalPages; i++) {
        var pageNumber = i + 1;
        var pageLink = $("<a>").addClass("page-numbers").attr("href", "#").data("page-index", i).text(pageNumber);
        if (i === currentPageIndex) {
            pageLink.addClass("active");
        }
      //  console.log("Generated pagination link:", pageLink); 
        $("#paginationDocument").append(pageLink);
    }
}

$(document).on("click", ".page-numbers", function () {
    var pageIndex = $(this).data("page-index");
    loadDocumentPartialView(pageIndex);
    return false;
});

$(document).on("click", ".sortable-header", function () {
    var sortExpression = $(this).data("sort-expression");

    var sortDirection = "ASC";
    if ($(this).hasClass("sorted-asc")) {
        sortDirection = "DESC";
        $(this).removeClass("sorted-asc").addClass("sorted-desc");
    } else if ($(this).hasClass("sorted-desc")) {
        sortDirection = "ASC";
        $(this).removeClass("sorted-desc").addClass("sorted-asc");
    } else {
        $(this).addClass("sorted-asc");
    }

    loadDocumentPartialView($("#pageIndexDocument").val(), $("#pageSizeDocument").val(), sortExpression, sortDirection);

    return false;
});