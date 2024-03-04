$(document).ready(function () {
    loadUserListPartialView();
});

$("#btnAddUser").click(function () {
    $.ajax({
        url: "/UserListV2/Add",
        type: 'GET',
        success: function () {
            window.location.href = "/UserDetails/UserDetails?studentid=0";
        },
        error: function () {
            console.log("Error occurred while redirecting to UserDetails view.");
        }
    });
});


function loadUserListPartialView(pageIndex = $("#pageIndexUserList").val(), pageSize = $("#pageSizeUserList").val(), sortExpression = $("#sortExpressionUserList").val(), sortDirection = $("#sortDirectionUserList").val()) {
    var objectId = $("#objectId").val();
    var objectType = $("#objectType").val();

    $.ajax({
        type: "GET",
        url: "/UserListV2/GetUsers",
        data: { pageIndex: pageIndex, pageSize: pageSize, sortExpression: sortExpression, sortDirection: sortDirection },
        dataType: "json",
        success: function (data) {
            $("#userListTable tbody").empty();
            $.each(data, function (index, item) {
                var editButton = "<button class='editUserButton' data-student-id='" + item.StudentID + "'>Edit</button>"; // Create edit button
                $("#userListTable tbody").append("<tr><td>" + item.StudentID + "</td><td>" + item.FirstName + "</td><td>" + item.LastName + "</td><td>" + item.Phone + "</td><td>"
                    + item.AadharNumber + "</td><td>" + editButton + "</td></tr>"); // Append edit button to table row
                row += "<td><a href='/UserDetailsV2/UserDetailsV2?StudentID=" + item.StudentID + "' class='edit-link' data-edit-id='" + item.StudentID + "'>Edit</a></td></tr>";
              //  $("#userListTable tbody").append(row);
            });
            renderPaginationUserList(data[0].PageIndex, data[0].TotalPages);
          
        },

        error: function () {
            console.log("Error");
        }
    });
}

function renderPaginationUserList(currentPageIndex, totalPages) {
    $("#paginationUserList").empty();
    for (var i = 0; i < totalPages; i++) {
        var pageNumber = i + 1;
        var pageLink = $("<a>").addClass("page-numbers").attr("href", "#").data("page-index", i).text(pageNumber);
        if (i === currentPageIndex) {
            pageLink.addClass("active");
        }
        console.log("Generated pagination link:", pageLink); // Log the generated pagination link
        $("#paginationUserList").append(pageLink);
    }
}

$(document).on("click", ".page-numbers", function () {
    var pageIndex = $(this).data("page-index");
    loadUserListPartialView(pageIndex);
    return false;
});

$(document).on("click", ".sortable-header-users", function () {
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

    loadUserListPartialView($("#pageIndexUserList").val(), $("#pageSizeUserList").val(), sortExpression, sortDirection);

    return false;
});