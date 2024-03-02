
function submitUserDetails() {
    $.ajax({
        url: 'UserDetailsV2.aspx/CheckAdmin',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        success: function (response) {
            if (response && response.d === true) {
                // If user is admin, redirect to UserList.aspx
                window.location.href = 'UserList.aspx';
            } else if (response === null) {
                // If response is null, redirect to LogIn.aspx
                window.location.href = 'LogIn.aspx';
            } else {
                // If user is not admin, proceed with other actions
                var urlParams = new URLSearchParams(window.location.search);
                var studentID = urlParams.get('StudentID');
                if (!studentID) {
                    alert("New user");
                    window.location.href = 'LogIn.aspx';
                } else {
                    // Upload file and perform other actions
                    var formData = new FormData();
                    var studentDetailsTable = getUserDetails();
                    for (var key in studentDetailsTable) {
                        formData.append(key, studentDetailsTable[key]);
                    }
                    var fileInput = document.getElementById('fileUpload');
                    if (fileInput.files.length > 0) {
                        formData.append('file', fileInput.files[0]);
                        studentDetailsTable.OriginalDocumentName = fileInput.files[0].name;
                    }
                    uploadFile(formData, studentDetailsTable, studentID);

                    var addressDetailViewCurrent = getAddressDetails('c');
                    var addressDetailViewPermanent = getAddressDetails('p');
                    updateAddressDetails(studentID, 1, addressDetailViewCurrent);
                    updateAddressDetails(studentID, 2, addressDetailViewPermanent);

                    var educationDetails10 = getEducationDetails('10');
                    var educationDetails12 = getEducationDetails('12');
                    var educationDetailsG = getEducationDetails('G');
                    updateEducationDetails(studentID, 1, educationDetails10);
                    updateEducationDetails(studentID, 2, educationDetails12);
                    updateEducationDetails(studentID, 3, educationDetailsG);

                    alert('Data Updated successfully.');
                }
            }
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}

function resetForm() {
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/ResetButton_Click",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
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
            console.error(xhr.responseText);
            alert("An error occurred while resetting the form. Please try again later.");
        }
    });
}

function uploadFile(formData, studentDetailsTable, studentID) {
    $.ajax({
        url: 'UploadFile.ashx',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            studentDetailsTable.DiskDocumentName = response.DiskDocumentName;
            studentDetailsTable.OriginalFileName = response.OriginalFileName;
            if (studentID != 0) {
                updateUserDetails(studentID, studentDetailsTable);
            } else {
                insertDetails(studentDetailsTable, function () {
                    var sameAsCurrent = $('#sameAsCurrent').prop('checked');
                    insertAddress(sameAsCurrent);
                });
                insertEducation('10');
                insertEducation('12');
                insertEducation('G');
            }
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
        data: JSON.stringify({ userDetails: studentDetailsTable }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("Details submitted successfully.");
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error("Error occurred while submitting details:", errorThrown);
        }
    });
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

function updateUserDetails(userID, studentDetailsTable) {
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/UpdateUserDetails",
        data: JSON.stringify({ userID: userID, userDetails: studentDetailsTable }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("User details updated successfully");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error("Error updating user details:", thrownError);
        }
    });
}

function updateAddressDetails(studentID, addressType, addressDetails) {
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/UpdateAddressDetails", // Replace with your actual endpoint
        data: JSON.stringify({ userID: studentID, addressType: addressType, addressDetailView: addressDetails }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("Address details updated successfully");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error("Error updating address details:", thrownError);
        }
    });
}

function updateEducationDetails(studentID, educationType, educationDetails) {
    $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/UpdateEducationDetails", // Replace with your actual endpoint
        data: JSON.stringify({ userID: studentID, educationType: educationType, educationDetails: educationDetails }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("Education details updated successfully");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error("Error updating Education details:", thrownError);
        }
    });
}

function getSelectedHobbies() {
    var selectedHobbies = [];
    var selectedHobbyString = "";

    $('.hobby-checkbox').each(function () {
        var checkbox = $(this);
        if (checkbox.prop('checked')) {
            selectedHobbies.push(checkbox.val());
        }
    });

    selectedHobbyString = selectedHobbies.join(", ");
    return selectedHobbyString;
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

            var dateString = studentDetails.DateOfBirth;
            var parts = dateString.split('-');
            var formattedDate = parts[2] + '-' + parts[1] + '-' + parts[0];
            $("#birthday").val(formattedDate);

            $("#phone").val(studentDetails.Phone);
            $("#aadhar").val(studentDetails.AadharNumber);
            $("#password").val(studentDetails.Password);

            var gender = studentDetails.Gender;
            if (gender === "male") {
                $("#male").prop("checked", true);
            } else if (gender === "female") {
                $("#female").prop("checked", true);
            }

            var hobbies = studentDetails.Hobbies;
            var hobbyArray = hobbies.split(', ');

            hobbyArray.forEach(function (hobby) {
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
            console.error("Error fetching student details:", thrownError);
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
    cgpa.prop("checked", educationDetail.Marks === "cgpa");
    percentage.prop("checked", educationDetail.Marks === "Percentage");
    gradeValue.val(educationDetail.Aggregate);
    yop.val(educationDetail.YearOfCompletion);
}

function getUserDetails() {
    console.log(getSelectedHobbies());
    console.log($('#birthday').val());
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
                //getStates(1, pStates);
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

function checkEmail(email) {
    return $.ajax({
        type: "POST",
        url: "UserDetailsV2.aspx/CheckEmail",
        data: JSON.stringify({ email: email }),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
}

function getQueryStringParameter(name) {
    var urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(name);
}

function isEmpty(obj) {
    for (var key in obj) {
        if (obj.hasOwnProperty(key) && (obj[key] === "" || obj[key] === null)) {
            return true;
        }
    }
    return false;
}



function populateAddressFields(addressDetail, prefix) {
    $("#" + prefix + "1Address").val(addressDetail.AddressLine1);
    $("#" + prefix + "2Address").val(addressDetail.AddressLine2);
    $("#" + prefix + "PinCode").val(addressDetail.Pincode);

    populateSelectedCountryDropdown(addressDetail.CountryID, prefix);


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

function populateCurrentAddressFields(addressDetails) {
    if (addressDetails) {
        document.getElementById("txtCurrentAddressLine1").value = addressDetails.AddressLine1;
        document.getElementById("txtCurrentAddressLine2").value = addressDetails.AddressLine2;
        document.getElementById("ddlCurrentCountry").value = addressDetails.CountryID.toString();
        PopulateStates(addressDetails.CountryID, true, function () {
            document.getElementById("ddlCurrentState").value = addressDetails.StateID.toString();
        });
        document.getElementById("txtCurrentPincode").value = addressDetails.Pincode;
    }
}

function populatePermanentAddressFields(addressDetails) {
    if (addressDetails) {
        document.getElementById("txtPermanentAddressLine1").value = addressDetails.AddressLine1;
        document.getElementById("txtPermanentAddressLine2").value = addressDetails.AddressLine2;
        document.getElementById("ddlPermanentCountry").value = addressDetails.CountryID.toString();
        PopulateStates(addressDetails.CountryID, false, function () {
            document.getElementById("ddlPermanentState").value = addressDetails.StateID.toString();
        });
        document.getElementById("txtPermanentPincode").value = addressDetails.Pincode;
    }
}

CurrentRotation - 2
cycleReapate - 1
offerCycleID - 176