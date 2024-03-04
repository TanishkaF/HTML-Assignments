$(document).ready(function () {
    loadNotePartialView();
});

function loadNotePartialView(pageIndex = $("#pageIndex").val(), pageSize = $("#pageSize").val(), sortExpression = $("#sortExpression").val(), sortDirection = $("#sortDirection").val()) {
    //var objectId = $("#objectId").val();
    //var objectType = $("#objectType").val();

    $.ajax({
        type: "GET",
      //  url: "/NoteUserV2/GetNotes",
        url: "/NoteUserV2/GetNotes?objectId=" + objectID + "&objectType=" + objectType + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&sortExpression=" + sortExpression + "&sortDirection=" + sortDirection,

     //   data: { objectId: objectId, objectType: objectType, pageIndex: pageIndex, pageSize: pageSize, sortExpression: sortExpression, sortDirection: sortDirection },
        dataType: "json",
        success: function (data) {
            $("#noteTable tbody").empty();
            $.each(data, function (index, item) {
                $("#noteTable tbody").append("<tr><td>" + item.NoteID + "</td><td>" + item.ObjectID + "</td><td>" + item.ObjectType + "</td><td>" + item.NoteText + "</td><td>" + formatDate(item.TimeStamp) + "</td></tr>");
            });
            renderPaginationNote(data[0].PageIndex, data[0].TotalPages);
            // renderPagination(pageIndex, 4);
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


function renderPaginationNote(currentPageIndex, totalPages) {
    $("#paginationNote").empty();
    for (var i = 0; i < totalPages; i++) {
        var pageNumber = i + 1;
        var pageLink = $("<a>").addClass("page-numbers").attr("href", "#").data("page-index", i).text(pageNumber);
        if (i === currentPageIndex) {
            pageLink.addClass("active");
        }
        console.log("Generated pagination link:", pageLink); // Log the generated pagination link
        $("#paginationNote").append(pageLink);
    }
}

$(document).on("click", ".page-numbers", function () {
    var pageIndex = $(this).data("page-index");
    loadNotePartialView(pageIndex);
    return false;
});

$(document).on("click", ".sortable-header1", function () {
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

    loadNotePartialView($("#pageIndex").val(), $("#pageSize").val(), sortExpression, sortDirection);

    return false;
});



$("#addButton").click(function () {
    var noteText = $("#noteText").val();

    var data = {
        objectId: $("#objectId").val(),
        objectType: $("#objectType").val(),
        noteText: noteText
    };

    $.ajax({
        url: "/NoteUserV2/Add",
        type: "POST",
        data: data,
        success: function (response) {
            loadNotePartialView();
        },
        error: function () {
            console.log("Error occurred while adding a note.");
        }
    });
});
