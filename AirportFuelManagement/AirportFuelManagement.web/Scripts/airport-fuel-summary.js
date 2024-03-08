$(document).ready(function () {
    loadAirportFuelList();
})

function loadAirportFuelList(pageIndex = $("#pageIndexAirportFuelList").val(), pageSize = $("#pageSizeAirportFuelList").val(), sortExpression = $("#sortExpressionAirportFuelList").val(), sortDirection = $("#sortDirectionAirportFuelList").val()) {
    $.ajax({
        type: "GET",
        url: "/AirportFuel/GetAllFuelSummary",
        data: { pageIndex: pageIndex, pageSize: pageSize, sortExpression: sortExpression, sortDirection: sortDirection },
        success: function (data) {
            $("#airportFuelTable tbody").empty();
            $.each(data.Airports, function (index, item) {
                $('#airportFuelTable tbody').append(
                    '<tr>' +
                    '<td>' + item.AirportName + '</td>' +
                    '<td>' + (item.FuelAvailable ?? 'N/A') + '</td>' +
                    '</tr>'
                );
            });
            renderAirportFuelPagination(data.PageIndex, data.TotalPages, 'loadAirportFuelList');
        },
        error: function () {
            alert("Error occurred while retrieving airport fuel summary.");
        }
    });
}



function renderAirportFuelPagination(currentPageIndex, totalPages) {
    $("#paginationAirportFuelList").empty();
    if (totalPages > 1) {
        for (var i = 0; i < totalPages; i++) {
            var pageNumber = i + 1;
            var pageLink = $("<a>").addClass("page-numbers").attr("href", "#").data("page-index", i).text(pageNumber);
            if (i === currentPageIndex) {
                pageLink.addClass("active");
            }
            console.log("Generated pagination link:", pageLink); // Log the generated pagination link
            $("#paginationAirportFuelList").append(pageLink);
        }
    }
}

$(document).on("click", ".page-numbers", function () {
    var pageIndex = $(this).data("page-index");
    loadAirportFuelList(pageIndex);
    return false;
});

$(document).on("click", ".sortable-header-airportFuels", function () {
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

    loadAirportFuelList($("#pageIndexAirportList").val(), $("#pageSizeAirportList").val(), sortExpression, sortDirection);

    return false;
});