$(document).ready(function () {

    loadAircraftList();
    getAirports('Source');
    getAirports('Destination');

    $("#submitAircraft").click(function () {
        if (validateAircraftAdd()) {
            var aircraftData = {
                AircraftID: $("#AircraftID").val(),
                AircraftNumber: $("#AircraftNumber").val(),
                AirLine: $("#AirLine").val(),
                Source: $("#Source").val(),
                Destination: $("#Destination").val()
            };

            $.ajax({
                url: '/Aircraft/AddAircraft',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(aircraftData),
                success: function (response) {
                    if (response && response.hasOwnProperty('success')) {
                        if (response.success) {
                            $("#AircraftID").val("");
                            $("#AircraftNumber").val("");
                            $("#AirLine").val("");
                            $("#Source").val("");
                            $("#Destination").val("");
                            loadAircraftList();
                            alert('Successfully added aircraft.');
                        } else {
                            alert('Failed to add aircraft: ' + response.message);
                        }
                    } else {
                        alert('Invalid response received from the server.');
                    }
                },
                error: function (xhr, status, error) {
                    alert('Error occurred while adding aircraft: ' + error);
                }
            });
        }

    });

});

function getAirports(airportDropdownId) {
    $.ajax({
        url: '/Transaction/GetAirportsJson',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#' + airportDropdownId).empty();
            $('#' + airportDropdownId).append($('<option>', {
                value: '',
                text: 'Choose Airport name'
            }));
            $.each(data, function (index, airport) {
                $('#' + airportDropdownId).append($('<option>', {
                    value: airport.AirportID,
                    text: airport.AirportName
                }));
            });
            $('#' + airportDropdownId).trigger('change');
        },
        error: function (xhr, status, error) {
         //   console.error('Error fetching airports:', error);
        }
    });
}

function loadAircraftList(pageIndex = $("#pageIndexAircraftList").val(), pageSize = $("#pageSizeAircraftList").val(), sortExpression = $("#sortExpressionAircraftList").val(), sortDirection = $("#sortDirectionAircraftList").val()) {
    $.ajax({
        type: "GET",
        url: "/Aircraft/GetAllAircrafts",
        data: { pageIndex: pageIndex, pageSize: pageSize, sortExpression: sortExpression, sortDirection: sortDirection },
        success: function (data) {
            $("#aircraftTable tbody").empty();
            $.each(data.Aircrafts, function (index, item) {
                $('#aircraftTable tbody').append(
                    '<tr>' +
                    //'<td>' + item.AircraftUID + '</td>' + 
                    '<td>' + item.AircraftID + '</td>' +
                    '<td>' + item.AircraftNumber + '</td>' +
                    '<td>' + item.AirLine + '</td>' +
                    '<td>' + item.Source + '</td>' +
                    '<td>' + item.Destination + '</td>' +
                    '</tr>'
                );
            });
            renderPagination(data.PageIndex, data.TotalPages);
        },
        error: function () {
            alert("Error occurred while retrieving aircrafts.");
        }
    });
}

function renderPagination(currentPageIndex, totalPages) {
    $("#paginationAircraftList").empty();
    if (totalPages > 1) { // Only display pagination if there's more than one page
        for (var i = 0; i < totalPages; i++) {
            var pageNumber = i + 1;
            var pageLink = $("<a>").addClass("page-numbers").attr("href", "#").data("page-index", i).text(pageNumber);
            if (i === currentPageIndex) {
                pageLink.addClass("active");
            }
            $("#paginationAircraftList").append(pageLink);
        }
    }
}

$(document).on("click", ".page-numbers", function () {
    var pageIndex = $(this).data("page-index");
    loadAircraftList(pageIndex);
    return false;
});

$(document).on("click", ".sortable-header-aircrafts", function () {
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

    loadAircraftList($("#pageIndexAircraftList").val(), $("#pageSizeAircraftList").val(), sortExpression, sortDirection);

    return false;
});

$("#addAircraftButton").click(function () {

    $.ajax({
        url: '/Aircraft/AddAircraftForm',
        type: 'GET',
        success: function (response) {
            $('#aircraftFormContainer').html(response);
        },
        error: function (xhr, status, error) {
            alert('Error occurred while loading aircraft form: ' + error);
        }
    });

});

function validateAircraftAdd() {
    var aircraftID = $('#AircraftID').val();
    var aircraftNumber = $('#AircraftNumber').val();
    var airline = $('#AirLine').val();
    var source = $('#Source').val();
    var destination = $('#Destination').val();

    $('.error-message').remove();

    var isValid = true;

    if (!aircraftID) {
        $('#AircraftID').after('<span class="error-message">Please enter Aircraft ID.</span>');
        isValid = false;
    }

    if (!aircraftNumber) {
        $('#AircraftNumber').after('<span class="error-message">Please enter Aircraft Number.</span>');
        isValid = false;
    }

    if (!airline) {
        $('#AirLine').after('<span class="error-message">Please enter Airline.</span>');
        isValid = false;
    }

    if (!source) {
        $('#Source').after('<span class="error-message">Please select Source.</span>');
        isValid = false;
    }
    if (!destination) {
        $('#Destination').after('<span class="error-message">Please select Destination.</span>');
        isValid = false;
    }

    return isValid;
}
