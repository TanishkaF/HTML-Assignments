$(document).ready(function () {

    getAirports('AirportID');
    getAircrafts('AircraftID');   

    loadFuelTransactionList();
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
            console.error('Error fetching airports:', error);
        }
    });
}

function getAircrafts(aircraftDropdownId) {
    $.ajax({
        url: '/Transaction/GetAircraftJson', // Corrected the URL
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#' + aircraftDropdownId).empty();
            // Add default option
            $('#' + aircraftDropdownId).append($('<option>', {
                value: '',
                text: 'Choose Aircraft name'
            }));
            // Populate dropdown with fetched data
            $.each(data, function (index, aircraft) {
                $('#' + aircraftDropdownId).append($('<option>', {
                    value: aircraft.AircraftID,
                    text: aircraft.AircraftID // This can be changed to the appropriate property if there's a name property for aircraft
                }));
            });
            $('#' + aircraftDropdownId).trigger('change');
        },
        error: function (xhr, status, error) {
            console.error('Error fetching aircrafts:', error);
        }
    });
}

$("#addTransactionButton").click(function () {
    toggleTransactionFormDisplay(false);
    loadTransactionForm(1);
});

$(document).on("click", ".reverse-button", function () {
    var transactionID = $(this).closest("tr").find("td:first").text();
    toggleTransactionFormDisplay(true);
    loadReverseTransactionForm(transactionID);
});

$("#removeAllTransactionsButton").click(function () {
    $.ajax({
        url: '/Transaction/RemoveAllTransactions', // Replace with your actual controller and action method
        type: 'POST',
        success: function (response) {
            if (response.success) {
                alert('All transactions removed successfully.');
                loadFuelTransactionList();
                // Optionally, you can update the UI or reload data after successful removal
            } else {
                alert('Failed to remove transactions. Please try again later.');
            }
        },
        error: function (xhr, status, error) {
            alert('Error occurred while removing transactions: ' + error);
        }
    });
});


function toggleTransactionFormDisplay(reverseFormVisible) {
    var addReverseTransactionFormContainer = document.getElementById("addReverseTransactionFormContainer");
    var transactionFormContainer = document.getElementById("transactionFormContainer");

    if (reverseFormVisible) {
        addReverseTransactionFormContainer.style.display = "block";
        transactionFormContainer.style.display = "none";
    } else {
        addReverseTransactionFormContainer.style.display = "none";
        transactionFormContainer.style.display = "block";
    }
}

$("#submitTransactionButton").click(function (event) {

    if (validateTransactionAdd) {

        var transactionDirection = $("input[name='TransactionDirection']:checked").val();

        var transactionType;
        if (transactionDirection === "IN") {
            transactionType = 1;
        } else if (transactionDirection === "OUT") {
            transactionType = 2;
        }

        var transactionData = {
            TransactionType: transactionType,
            AirportID: $("#AirportID").val(),
            AircraftID: $("#AircraftID").val(),
            Quantity: $("#Quantity").val(),
        };

        $.ajax({
            url: '/Transaction/AddTransaction',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(transactionData),
            success: function (response) {
                if (response && response.hasOwnProperty('success')) {
                    if (response.success) {
                        $('#transactionForm')[0].reset();
                        loadTransactionForm(1);
                    } else {
                        alert('Failed to add transaction: ' + response.message);
                    }
                } else {
                    alert('Invalid response received from the server.');
                }
            },
            error: function (xhr, status, error) {
                alert('Error occurred while adding transaction: ' + error);
            }
        });
    }
        return false;

});

$('#submitReverseTransactionButton').click(function (event) {

    if (validateTransactionAdd) {

        var transactionIDParent = $('#TransactionID').val();
        var transactionDirection = $("input[name='TransactionDirection']:checked").val();

        var transactionType;
        if (transactionDirection === "IN") {
            transactionType = 1;
        } else if (transactionDirection === "OUT") {
            transactionType = 2;
        }

        var transactionData = {
            TransactionType: transactionType,
            AirportID: $('#AirportID').val(),
            AircraftID: $('#AircraftID').val(),
            Quantity: $('#Quantity').val(),
            TransactionIDParent: transactionIDParent
        };

        $.ajax({
            url: '/Transaction/AddTransaction',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(transactionData),
            success: function (response) {
                if (response && response.hasOwnProperty('success')) {
                    if (response.success) {
                        ///  $("#reverseTransactionForm")[0].reset();
                        loadReverseTransactionForm(1);
                    } else {
                        alert('Failed to add transaction: ' + response.message);
                    }
                } else {
                    alert('Invalid response received from the server.');
                }
            },
            error: function (xhr, status, error) {
                alert('Error occurred while adding transaction: ' + error);
            }
        });
    }
    return false;
});

$(document).on("click", ".page-numbers", function () {
    var pageIndex = $(this).data("page-index");
    loadFuelTransactionList(pageIndex);
    return false;
});

$(document).on("click", ".sortable-header-fuel-transaction", function () {
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

    loadFuelTransactionList($("#pageIndexFuelTransactionList").val(), $("#pageSizeFuelTransactionList").val(), sortExpression, sortDirection);

    return false;
});

function loadTransactionForm(transactionType) {
    $.ajax({
        type: "GET",
        url: "/Transaction/GetTransactionForm",
        data: { transactionType: transactionType },
        success: function (partialView) {            
            $("#transactionFormContainer").html(partialView);
        },
        error: function () {
            alert("Error occurred while loading transaction form.");
        }
    });
}

function loadReverseTransactionForm(transactionID) {
    $.ajax({
        type: "GET",
        url: "/Transaction/GetReverseTransactionForm",
        data: { transactionID: transactionID },
        success: function (partialView) {
            $("#addReverseTransactionFormContainer").html(partialView);          
        },
        error: function () {
            alert("Error occurred while loading reverse transaction form.");
        }
    });
}

function loadFuelTransactionList(pageIndex = $("#pageIndexTransactionList").val(), pageSize = $("#pageSizeTransactionList").val(), sortExpression = $("#sortExpressionTransactionList").val(), sortDirection = $("#sortDirectionTransactionList").val()) {
    $.ajax({
        type: "GET",
        url: "/Transaction/GetAllTransactions",
        data: { pageIndex: pageIndex, pageSize: pageSize, sortExpression: sortExpression, sortDirection: sortDirection },
        success: function (data) {
            $("#TransactionTable tbody").empty();
            $.each(data.FuelTransaction, function (index, item) {
                var transactionType = item.TransactionType === 1 ? 'IN' : 'OUT';
                $('#TransactionTable tbody').append(
                    '<tr>' +
                    '<td>' + item.TransactionID + '</td>' +
                    '<td>' + formatDate(item.TimeStamp) + '</td>' +
                    '<td>' + transactionType + '</td>' +
                    '<td>' + item.AirportName + '</td>' +
                    '<td>' + (item.AircraftID ?? 'N/A') + '</td>' +
                    '<td>' + item.Quantity + '</td>' +
                    '<td>' + (item.TransactionIDParent ?? 'N/A') + '</td>' +
                    '<td><button class="reverse-button">Reverse</button></td>' + // Adding the Reverse button column
                    '</tr>'
                );
            });
            renderPagination(data.PageIndex, data.TotalPages);
        },
        error: function () {
            alert("Error occurred while retrieving fuel transactions.");
        }
    });
}

function formatDate(timestamp) {
    var date = new Date(parseInt(timestamp.substr(6)));
    return date.toLocaleString(); // Adjust the date format as needed
}

function renderPagination(currentPageIndex, totalPages) {
    $("#paginationFuelTransactionList").empty();
    if (totalPages > 1) {
        for (var i = 0; i < totalPages; i++) {
            var pageNumber = i + 1;
            var pageLink = $("<a>").addClass("page-numbers").attr("href", "#").data("page-index", i).text(pageNumber);
            if (i === currentPageIndex) {
                pageLink.addClass("active");
            }
            console.log("Generated pagination link:", pageLink); // Log the generated pagination link
            $("#paginationFuelTransactionList").append(pageLink);
        }
    }
}

function validateTransactionAdd() {
    var airportName = $('#AirportID').val();
    var aircraftName = $('#AircraftID').val();
    var quantity = $('#Quantity').val();
    var isValid = true;

    $('.error-message').hide();

    if (!airportName) {
        $('#AirportID').next('.error-message').show().text('Please enter Airport Name.');
        isValid = false;
    }

    if (!aircraftName) {
        $('#AircraftID').next('.error-message').show().text('Please enter Aircraft Name.');
        isValid = false;
    }

    if (!quantity && quantity <= 0) {
        $('#Quantity').next('.error-message').show().text('Please enter valid Quantity.');
        isValid = false;
    }

    return isValid;
}
