$(document).ready(function () {
    loadNotePartialView();
});

function loadNotePartialView(pageIndex = 0, pageSize = 10, sortExpression = "NoteID", sortDirection = "ASC") {
    var objectId = 5; // Specify your objectId here
    var objectType = 1; // Specify your objectType here

    $.ajax({
        type: "GET",
        url: "/NoteUserV2/GetNotes",
        data: { objectId: objectId, objectType: objectType, pageIndex: pageIndex, pageSize: pageSize, sortExpression: sortExpression, sortDirection: sortDirection },
        dataType: "json",
        success: function (data) {
            renderTable(data); // Render the table with the received JSON data
        },
        error: function () {
            console.log("Error");
        }
    });
}

function renderTable(data) {
    var tbody = $('#noteTable tbody');
    tbody.empty(); // Clear existing table rows

    // Populate table rows with data
    $.each(data, function (index, item) {
        var row = $('<tr>');
        row.append('<td>' + item.NoteID + '</td>');
        row.append('<td>' + item.ObjectID + '</td>');
        row.append('<td>' + item.ObjectType + '</td>');
        row.append('<td>' + item.NoteText + '</td>');
        row.append('<td>' + item.TimeStamp + '</td>');
        tbody.append(row);
    });
}

$(document).on("click", ".page-numbers", function () {
    var url = $(this).attr("href");
    $.get(url, function (data) {
        renderTable(data); // Render the table with the received JSON data
    });
    return false;
});

$(document).on("click", ".sortable-header", function () {
    var sortExpression = $(this).data("sort-expression");
    var sortDirection = $(this).data("sort-direction");

    loadNotePartialView(0, 10, sortExpression, sortDirection);
    return false;
});