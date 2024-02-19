function populateUserHobbies(userID) {
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/GetUserHobbies",
        data: JSON.stringify({ userID: userID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var hobbies = response.d; // Assuming the response is an array of hobbies

            hobbies.forEach(function (hobby) {
                switch (hobby) {
                    case "Dancing":
                        $("#checkbox1").prop("checked", true);
                        break;
                    case "Singing":
                        $("#checkbox2").prop("checked", true);
                        break;
                    case "Coding":
                        $("#checkbox3").prop("checked", true);
                        break;
                    case "Web Designing":
                        $("#checkbox4").prop("checked", true);
                        break;
                    case "Board Games":
                        $("#checkbox5").prop("checked", true);
                        break;
                    case "Camping":
                        $("#checkbox6").prop("checked", true);
                        break;
                    case "Running":
                        $("#checkbox7").prop("checked", true);
                        break;
                    case "Sleeping":
                        $("#checkbox8").prop("checked", true);
                        break;
                    case "Reading":
                        $("#checkbox9").prop("checked", true);
                        break;
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error("Error fetching user hobbies:", thrownError);
        }
    });
}


function copyAddress(sameAsCurrent) {
    $.ajax({
        type: "POST",
        url: "UserDetails.aspx/CopyAddress",
        data: JSON.stringify({ sameAsCurrent: sameAsCurrent }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            // Handle success if needed
            console.log("Address copied successfully.");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error("Error copying address:", thrownError);
        }
    });
}


//hi from here working

function checkEmail(email) {
    return $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/CheckEmail",
        data: JSON.stringify({ email: email }),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
}

function resetForm() {
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/ResetButton_Click",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            // Reset successful, update UI as needed
            $("[data-store]").each(function () {
                var elementType = this.tagName.toLowerCase();
                if (elementType === "input" || elementType === "textarea") {
                    $(this).val("");
                } else if (elementType === "select") {
                    $(this).prop("selectedIndex", 0);
                }
            });
            alert("Form reset successfully!");
        },
        error: function (xhr, status, error) {
            // Handle errors if any
            console.error(xhr.responseText);
            alert("An error occurred while resetting the form. Please try again later.");
        }
    });
}

function getSelectedHobbies() {
    var checkboxes = [];

    // Loop through each checkbox and push its checked state to the checkboxes array
    $('.hobby-checkbox').each(function () {
        checkboxes.push($(this).prop('checked'));
    });

    // Make an Ajax request to the server-side method
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/GetSelectedHobbies",
        data: JSON.stringify({ checkboxes: checkboxes }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            // Handle the response from the server
            console.log("Selected hobbies:", response.d);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            // Handle any errors
            console.error("Error getting selected hobbies:", thrownError);
        }
    });
}

function getAndPopulateAddressDetails(studentID, addressType) {
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/GetAddressDetails",
        data: JSON.stringify({ studentID: studentID, addressType: addressType }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            // Handle the response from the server and populate the address details
            var addressDetail = response.d;
            if (addressDetail != null) {
                if (addressType == 1) {
                    populateAddressFields(addressDetail, "c");
                } else if (addressType == 2) {
                    populateAddressFields(addressDetail, "p");
                }
            } else {
                console.error("Address details not found for address type: " + addressType);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            // Handle any errors
            console.error("Error getting address details:", thrownError);
        }
    });
}

function populateAddressFields(addressDetail, prefix) {
    $("#" + prefix + "1Address").val(addressDetail.AddressLine1);
    $("#" + prefix + "2Address").val(addressDetail.AddressLine2);
    $("#" + prefix + "PinCode").val(addressDetail.Pincode);

    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/GetCountryName",
        data: JSON.stringify({ countryID: addressDetail.CountryID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#" + prefix + "Country").val(response.d);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error("Error getting country name:", thrownError);
        }
    });

    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/GetStateName",
        data: JSON.stringify({ stateID: addressDetail.StateID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#" + prefix + "State").val(response.d);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error("Error getting state name:", thrownError);
        }
    });
}

function getAndPopulateEducationDetails(studentID) {
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/GetEducationDetail",
        data: JSON.stringify({ studentID: studentID, educationType: "10" }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            populateEducationFields(response.d, "10");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error("Error getting education details:", thrownError);
        }
    });

    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/GetEducationDetail",
        data: JSON.stringify({ studentID: studentID, educationType: "12" }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            populateEducationFields(response.d, "12");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error("Error getting education details:", thrownError);
        }
    });

    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/GetEducationDetail",
        data: JSON.stringify({ studentID: studentID, educationType: "graduate" }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            populateEducationFields(response.d, "graduate");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error("Error getting education details:", thrownError);
        }
    });
}

function populateEducationFields(educationDetail, educationType) {
    var instName, board, cgpa, percentage, gradeValue, yop;

    switch (educationType) {
        case "10":
            instName = $("#instName10");
            board = $("#board10");
            cgpa = $("#cgpa10");
            percentage = $("#percentage10");
            gradeValue = $("#grade10Value");
            yop = $("#yop10");
            break;
        case "12":
            instName = $("#instName12");
            board = $("#board12");
            cgpa = $("#cgpa12");
            percentage = $("#percentage12");
            gradeValue = $("#grade12Value");
            yop = $("#yop12");
            break;
        case "graduate":
            instName = $("#instNameG");
            board = $("#boardG");
            cgpa = $("#cgpaG");
            percentage = $("#percentageG");
            gradeValue = $("#gradeGValue");
            yop = $("#yopG");
            break;
        default:
            break;
    }

    instName.val(educationDetail.InstituteName);
    board.val(educationDetail.Board);
    cgpa.prop("checked", educationDetail.Marks === "CGPA");
    percentage.prop("checked", educationDetail.Marks === "Percentage");
    gradeValue.val(educationDetail.Aggregate);
    yop.val(educationDetail.YearOfCompletion);
}
function populateStudentDetails(studentID) {
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/GetStudentDetails",
        data: JSON.stringify({ studentID: studentID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var studentDetails = response.d;
            $("#firstName").val(studentDetails.FirstName);
            $("#middleName").val(studentDetails.MiddleName);
            $("#lastName").val(studentDetails.LastName);
            $("#email").val(studentDetails.Email);
            $("#birthday").val(studentDetails.DateOfBirth);
            $("#phone").val(studentDetails.Phone);
            $("#aadhar").val(studentDetails.AadharNumber);
            $("#password").val(studentDetails.Password);

            var gender = studentDetails.Gender;
            if (gender === "Male") {
                $("#male").prop("checked", true);
            } else if (gender === "Female") {
                $("#female").prop("checked", true);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error("Error fetching student details:", thrownError);
        }
    });
}

function getQueryStringParameter(name) {
    var urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(name);
}

function getUserDetails() {
    return {
        FirstName: $('#firstName').val(),
        MiddleName: $('#middleName').val(),
        LastName: $('#lastName').val(),
        Email: $('#email').val(),
        Phone: $('#phone').val(),
        AadharNumber: $('#aadhar').val(),
        DateOfBirth: $('#birthday').val(),
        Gender: $('input[name=gender]:checked').val(),
        Hobbies: getSelectedHobbies(),
        Password: $('#password').val()
    };
}

function getEducationDetails(type) {
    return {

        InstituteName: $('#' + 'instName' + type).val(),
        Board: $('#' + 'board' + type).val(),
        Marks: $('input[name=marks]:checked').val(),
        Aggregate: $('#' + 'grade' + type + 'Value').val(),
        YearOfCompletion: $('#' + 'yop' + type).val()
    };
}


function getAddressDetails(type) {
    return {
        CountryID: $('#' + type + 'Country').val(),
        StateID: $('#' + type + 'State').val(),
        AddressLine1: $('#' + type + '1Address').val(),
        AddressLine2: $('#' + type + '2Address').val(),
        Pincode: $('#' + type + 'PinCode').val()
    };
}


function getSelectedHobbies() {
    var hobbies = [];
    $('input[name=hobby]:checked').each(function () {
        hobbies.push($(this).val());
    });
    return hobbies.join(', ');
}



/*$(document).ready(function () {*/
//  //  submitUserDetails();
//    var studentID = getQueryStringParameter("StudentID");

//    if (studentID) {
//        populateStudentDetails(studentID);
//        populateAddressDetails(studentID);
//        populateEducationDetails(studentID);
//    } else {
//       // PopulateCountries(cCountry);
//        //PopulateCountries(pCountry);
//    }

//    $.ajax({
//        type: "POST",
//        url: "UserDetailsV2.aspx/GetAddressTypes",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            window.addressTypes = JSON.parse(response.d);
//        },
//        error: function (xhr, status, error) {
//            console.error('Error fetching address types - Status:', status, 'Error:', error);
//        }
//    });

//    $.ajax({
//        type: "POST",
//        url: "UserDetailsV2.aspx/GetEducationTypes",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            window.educationTypes = JSON.parse(response.d);
//        },
//        error: function (xhr, status, error) {
//            console.error('Error fetching education types - Status:', status, 'Error:', error);
//        }
//    });
//});



function getCountry() {
    $.ajax({
        url: 'UserDetailsV2.aspx/GetCountries',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        success: function (response) {
            var countries = JSON.parse(response.d);
            populateCountries('pCountry', countries);
            populateCountries('cCountry', countries);
        },
        error: function (xhr, status, error) {
            console.error("f");
        }
    });
}

function populateCountries(dropDownId, countries) {
    var dropDown = $('#' + dropDownId);
    dropDown.empty();
    dropDown.append($('<option>').val('').text('Select Country').prop('disabled', true).prop('selected', true));

    $.each(countries, function (index, country) {
        var option = $('<option>').val(country.CountryID).text(country.CountryName);
        dropDown.append(option);
    });
}

function getStates(countryID, stateDropDown) {
    $.ajax({
        url: 'UserDetailsV2.aspx/GetStates',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ countryID: parseInt(countryID) }),
        success: function (response) {
            var states = JSON.parse(response.d);
            populateStates(states, stateDropDown);

        },
        error: function (xhr, status, error) {
            console.error("Error fetching states:", error);
        }
    });
}

function populateStates(states, stateDropDown) {
    var dropDown = $('#' + stateDropDown);
    dropDown.empty();
    dropDown.append($('<option>').val('').text('Select State').prop('disabled', true).prop('selected', true));

    $.each(states, function (index, state) {
        var option = $('<option>').val(state.StateID).text(state.StateName);
        dropDown.append(option);
    });
}



//$('#submitButton').click(function (e) {
//    e.preventDefault();
//    var userID = 33;
//    populateStudentDetails(userID);
//    getAndPopulateAddressDetails(userID, 1); 
//    getAndPopulateAddressDetails(userID, 2);
//    getAndPopulateEducationDetails(userID);
//    getSelectedHobbies();
//    populateUserHobbies(userID)
//});



$(document).ready(function () {
    var urlParams = new URLSearchParams(window.location.search);
    var userID = urlParams.get('studentID');
    getCountry();
    if (userID) {
        e.preventDefault();
        populateStudentDetails(userID);
        getAndPopulateAddressDetails(userID, 1);
        getAndPopulateAddressDetails(userID, 2);
        getAndPopulateEducationDetails(userID);
        getSelectedHobbies();
        populateUserHobbies(userID)
       // updateFunction(userID); // Call the update function
    } else {
        submitUserDetails(); // Call the insert function
    }
});

function submitUserDetails() {
    var formData = new FormData();
    var studentDetailsTable = getUserDetails();

    for (var key in studentDetailsTable) {
        formData.append(key, studentDetailsTable[key]);
    }
    var fileInput = document.getElementById('fileUpload');

    if (fileInput.files.length > 0) {
        formData.append('file', fileInput.files[0]);
        studentDetailsTable.OriginalDocumentName = fileInput.files[0].name; // Get original file name and add it to studentDetailsTable
    }

    uploadFile(formData, studentDetailsTable);

    var sameAsCurrent = $('#sameAsCurrent').prop('checked');
    insertAddress(sameAsCurrent);

    insertEducation('10');
    insertEducation('12');
    insertEducation('G');

    return false;
}

function uploadFile(formData, studentDetailsTable) {
    $.ajax({
        url: 'UploadFile.ashx',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false, // Let jQuery handle the content type
        success: function (response) {
            studentDetailsTable.DiskDocumentName = response.DiskDocumentName; // Assuming the server returns the disk document name
            studentDetailsTable.OriginalFileName = response.OriginalFileName; // Assuming the server returns the original file name
            insertDetails(studentDetailsTable); // Call the function to insert details after successful upload
        },
        error: function (xhr, status, error) {
            console.error('Error uploading file:', error);
        }
    });
}

function insertDetails(studentDetailsTable) {
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/InsertDetails",
        data: JSON.stringify({ userDetails: studentDetailsTable }), // Convert studentDetailsTable to JSON string
        contentType: "application/json; charset=utf-8", // Set content type
        dataType: "json", // Expected data type of the response
        success: function (response) {
            console.log("Details submitted successfully.");
            // Redirect or perform other actions
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error("Error occurred while submitting details:", errorThrown);
        }
    });
}

function insertEducation(type) {
    var educationTypeMapping = {
        '10': 1,
        '12': 2,
        'G': 3
    };

    var educationDetails = getEducationDetails(type);
    if (!isEmpty(educationDetails)) {
        var numericEducationType = educationTypeMapping[type];
        if (numericEducationType) {
            $.ajax({
                type: "POST",
                url: "UserDetailsV2.aspx/InsertEducationDetails",
                data: JSON.stringify({ educationDetails: educationDetails, educationType: numericEducationType }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(type + " education details inserted successfully.");
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Error occurred while inserting " + type + " education details:", errorThrown);
                }
            });
        } else {
            console.log("Error: Education type mapping not found for " + type + ".");
        }
    } else {
        console.log("Error: Please fill in all education details for " + type + ".");
    }
}



function isEmpty(obj) {
    for (var key in obj) {
        if (obj.hasOwnProperty(key) && (obj[key] === "" || obj[key] === null)) {
            return true;
        }
    }
    return false;
}


function getLastInsertedUserID() {
    var lastInsertedUserID;
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/GetLastInsertedUserID",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false, 
        success: function (response) {
            lastInsertedUserID = parseInt(response.d);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error("Error getting last inserted user ID:", errorThrown);
        }
    });
    return lastInsertedUserID;
}

function copyAddress() {
    var checkBox = $('#sameAsCurrent');
    if (checkBox.is(':checked')) {
        var data = {
            cCountry: $('#cCountry').val(),
            cState: $('#cState').val(),
            c1Address: $('#c1Address').val(),
            c2Address: $('#c2Address').val(),
            cPinCode: $('#cPinCode').val()
        };

        $.ajax({
            type: 'POST',
            url: 'UserDetailsV2.aspx/CopyAddress',
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {
                $('#pCountry').val(data.cCountry);
                $('#pState').val(data.cState);
                $('#p1Address').val(data.c1Address);
                $('#p2Address').val(data.c2Address);
                $('#pPinCode').val(data.cPinCode);
            },
            error: function (xhr, status, error) {
                console.error('Error copying address:', error);
            }
        });
    }
}

function insertAddress(sameAsCurrent) {
    var currentAddressDetails = getAddressDetails('c');
    currentAddressDetails.AddressType = 1;

    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/InsertAddress",
        data: JSON.stringify({ addressDetailView: currentAddressDetails, addressType: 1 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("Current address details inserted successfully.");

            if (sameAsCurrent) {
                $.ajax({
                    type: "POST",
                    url: "UserDetailsV2.aspx/InsertAddress",
                    data: JSON.stringify({ addressDetailView: currentAddressDetails, addressType: 2 }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log("Permanent address details inserted successfully.");
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.error("Error occurred while inserting permanent address details:", errorThrown);
                    }
                });
            } else {
                var permanentAddressDetails = getAddressDetails('p');
                permanentAddressDetails.AddressType = 2; 

                $.ajax({
                    type: "POST",
                    url: "UserDetailsV2.aspx/InsertAddress",
                    data: JSON.stringify({ addressDetailView: permanentAddressDetails, addressType: 2 }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log("Permanent address details inserted successfully.");
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.error("Error occurred while inserting permanent address details:", errorThrown);
                    }
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error("Error occurred while inserting current address details:", errorThrown);
        }
    });
}
