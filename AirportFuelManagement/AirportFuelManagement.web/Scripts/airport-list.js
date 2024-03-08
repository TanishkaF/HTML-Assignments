$(document).ready(function () {

    $("#submitAirport").click(function () {
        if (validateAirportAdd()) {
            var airportData = {
                AirportID: $("#AirportID").val(),
                AirportName: $("#AirportName").val(),
                FuelCapacity: $("#FuelCapacity").val()
            };

            $.ajax({
                url: '/Airport/AddAirport',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(airportData),
                success: function (response) {
                    if (response && response.hasOwnProperty('success')) {
                        if (response.success) {
                            // Success message or action
                            $("#AirportID").val("");
                            $("#AirportName").val("");
                            $("#FuelCapacity").val("");
                            loadAirportList();
                            //alert('Airport added successfully.');
                        } else {
                           // alert('Failed to add airport: ' + response.message);
                        }
                    } else {
                       // alert('Invalid response received from the server.');
                    }
                },
                error: function (xhr, status, error) {
                    // alert('Error occurred while adding airport: ' + error);
                }
            });
        }
        // Return false to prevent default form submission
        return false;
    });


    loadAirportList();
});

$("#addAirportButton").click(function () {
    $.ajax({
        url: '/Airport/AddAirportForm',
        type: 'GET',
        success: function (response) {
            $('#airportFormContainer').html(response);
        },
        error: function (xhr, status, error) {
            alert('Error occurred while loading airport form: ' + error);
        }
    });
});

function loadAirportList(pageIndex = $("#pageIndexAirportList").val(), pageSize = $("#pageSizeAirportList").val(), sortExpression = $("#sortExpressionAirportList").val(), sortDirection = $("#sortDirectionAirportList").val()) {
    $.ajax({
        type: "GET",
        url: "/Airport/GetAllAirports",
        data: { pageIndex: pageIndex, pageSize: pageSize, sortExpression: sortExpression, sortDirection: sortDirection },
        success: function (data) {
            $("#airportTable tbody").empty();
            $.each(data.Airports, function (index, item) {
                $('#airportTable tbody').append(
                    '<tr>' +
                    //'<td>' + item.AirportUID + '</td>' +
                    '<td>' + item.AirportID + '</td>' +
                    '<td>' + item.AirportName + '</td>' +
                    '<td>' + (item.FuelCapacity ?? 'N/A') + '</td>' +
                    '<td>' + (item.FuelAvailable ?? 'N/A') + '</td>' +
                    '</tr>'
                );
            });
            renderPagination(data.PageIndex, data.TotalPages);
        },
        error: function () {
            alert("Error occurred while retrieving airports.");
        }
    });
}

function renderPagination(currentPageIndex, totalPages) {
    $("#paginationAirportList").empty();
    if (totalPages > 1) {
        for (var i = 0; i < totalPages; i++) {
            var pageNumber = i + 1;
            var pageLink = $("<a>").addClass("page-numbers").attr("href", "#").data("page-index", i).text(pageNumber);
            if (i === currentPageIndex) {
                pageLink.addClass("active");
            }
            //console.log("Generated pagination link:", pageLink); // Log the generated pagination link
            $("#paginationAirportList").append(pageLink);
        }
    }
}

$(document).on("click", ".page-numbers", function () {
    var pageIndex = $(this).data("page-index");
    loadAirportList(pageIndex);
    return false;
});

$(document).on("click", ".sortable-header-airports", function () {
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

    loadAirportList($("#pageIndexAirportList").val(), $("#pageSizeAirportList").val(), sortExpression, sortDirection);

    return false;
});


function validateAirportAdd() {
    var airportID = $('#AirportID').val();
    var airportName = $('#AirportName').val();
    var fuelCapacity = $('#FuelCapacity').val();


    $('.error-message').remove();

    var isValid = true;

    if (!airportID) {
        $('#AirportID').after('<span class="error-message">Please enter Airport ID.</span>');
        isValid = false;
    }

    if (!airportName) {
        $('#AirportName').after('<span class="error-message">Please enter Airport Name.</span>');
        isValid = false;
    }

    if (!fuelCapacity || fuelCapacity < 0) {
        $('#FuelCapacity').after('<span class="error-message">Please enter Fuel Capacity.</span>');
        isValid = false;
    }

    return isValid;
}