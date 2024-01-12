function validateData() {

    var firstname = document.getElementById("fname").value;
    var middlename = document.getElementById("mname").value;
    var lastname = document.getElementById("lname").value;
    var vEmail = document.getElementById("email").value;
    var vPass = document.getElementById("password").value;
    var vCPass = document.getElementById("cpassword").value;
    var vAge = document.getElementById("age").value;
    var vContact = document.getElementById("phone").value;
    var vAadhar = document.getElementById("aadhar").value;
    var birthday = document.getElementById("birthday").value;
    var pan = document.getElementById("pan").value;
    var vgender = document.querySelector('input[name="gender"]:checked');

    var ccountry = document.getElementById("ccountry").value;
    var cstate = document.getElementById("cstate").value;
    var c1address = document.getElementById("c1address").value;
    var c2address = document.getElementById("c2address").value;
    var vCPinCode = document.getElementById("cpinCode").value;

    var pcountry = document.getElementById("pcountry").value;
    var pstate = document.getElementById("pstate").value;
    var p1address = document.getElementById("p1address").value;
    var p2address = document.getElementById("p2address").value;
    var vPPinCode = document.getElementById("ppinCode").value;

    var instname10 = document.getElementById("instname10").value;
    var board10 = document.getElementById("board10").value;
    var vMarks = document.querySelector('input[name="marks"]:checked');
    var vGrade10Value = document.getElementById("grade10Value").value;
    var vYop10 = document.getElementById("yop10").value;

    var instname12 = document.getElementById("instname12").value;
    var board12 = document.getElementById("board12").value;
    var vMarks12 = document.querySelector('input[name="marks12"]:checked');
    var vGrade12Value = document.getElementById("grade12Value").value;
    var vYop12 = document.getElementById("yop12").value;

    var instnameg = document.getElementById("instnameg").value;
    var boardg = document.getElementById("boardg").value;
    var vMarksG =  document.querySelector('input[name="marksg"]:checked');
    var vGradegValue = document.getElementById("gradegValue").value;
    var vYopg = document.getElementById("yopg").value;

    var message = document.getElementById("message").value;
    var feedback = document.getElementById("feedback").value;

    const totalValidationCount = 5;
    var globalCount = 5;


    if (globalCount == totalValidationCount) {
        return {

            firstName: firstname,
            middleName: middlename,
            lastName: lastname,
            email: vEmail,
            password: vPass,
            confirmPassword: vCPass,
            birthday: birthday,
            age: vAge,
            contactNumber: vContact,
            aadhar: vAadhar,
            pan: pan,
            gender: vgender.value,

            CurrentCountry: ccountry,
            CurrentState: cstate,
            CurrentAddressLine1: c1address,
            CurrentAddressLine2: c2address,
            CurrentPinCode: vCPinCode,

            PermanentCountry: pcountry,
            PermanentState: pstate,
            PermanentPinCode: vPPinCode,
            PermanentAddressLine1: p1address,
            PermanentAddressLin2: p2address,

            InstituteName10: instname10,
            InstituteBoard10: board10,
            Marks10: vMarks.value,
            Aggregate10: vGrade10Value,
            YOP10: vYop10,

            InstituteName12: instname12,
            InstituteBoard12: board12,
            Marks12: vMarks12.value,
            Aggregate12: vGrade12Value,
            YOP12: vYop12,

            InstituteNameGraduation: instnameg,
            InstituteBoardGraduation: boardg,
            MarksGraduation: vMarksG.value,
            AggregateGraduation: vGradegValue,
            YOPGraduation: vYopg,

            messageEntered: message,
            feedbackEntered: feedback

        };
    } else {
        alert("Somethis is getting wrong");
        return null;
    }

}


function storeData(data) {


    localStorage.setItem('firstName', data.firstName);
    localStorage.setItem('middleName', data.middleName);
    localStorage.setItem('lastName', data.lastName);
    localStorage.setItem('email', data.email);
    localStorage.setItem('password', data.password);
    localStorage.setItem('confirmPassword', data.confirmPassword);
    localStorage.setItem('birthday', data.birthday);
    localStorage.setItem('age', data.age);
    localStorage.setItem("Gender", data.gender);
    localStorage.setItem('contactNumber', data.contactNumber);
    localStorage.setItem('aadhar', data.aadhar);
    localStorage.setItem('pan', data.pan);

    localStorage.setItem('currentCountry', data.CurrentCountry);
    localStorage.setItem('currentState', data.CurrentState);
    localStorage.setItem('currentAddressLine1', data.CurrentAddressLine1);
    localStorage.setItem('currentAddressLine2', data.CurrentAddressLine2);
    localStorage.setItem('currentPinCode', data.CurrentPinCode);

    localStorage.setItem('permanentCountry', data.PermanentCountry);
    localStorage.setItem('permanentState', data.PermanentState);
    localStorage.setItem('permanentAddressLine1', data.PermanentAddressLine1);
    localStorage.setItem('permanentAddressLine2', data.PermanentAddressLin2);
    localStorage.setItem('permanentPinCode', data.PermanentPinCode);

    localStorage.setItem("instituteName10", data.InstituteName10);
    localStorage.setItem("board10", data.InstituteBoard10);
    localStorage.setItem("marks10",data.Marks10);
    localStorage.setItem("aggregrate10", data.Aggregate10);
    localStorage.setItem("yOP10", data.YOP10);

    localStorage.setItem("instituteName12", data.InstituteName12);
    localStorage.setItem("board12", data.InstituteBoard12);
    localStorage.setItem("marks12",data.Marks12);
    localStorage.setItem("aggregrate12", data.Aggregate12);
    localStorage.setItem("yOP12", data.YOP12);

    localStorage.setItem("instituteNameGraduation", data.InstituteNameGraduation);
    localStorage.setItem("boardGraduation", data.InstituteBoardGraduation);
    localStorage.setItem("MarksGraduation",data.MarksGraduation);
    localStorage.setItem("aggregrateGraduation", data.AggregateGraduation);
    localStorage.setItem("yOPGraduation", data.YOPGraduation);



    localStorage.setItem('MessageEntered', data.messageEntered);
    localStorage.setItem('FeedBackEntered', data.feedbackEntered);
}

function displayUserData() {

    var firstname = localStorage.getItem("firstName");
    var middlename = localStorage.getItem("middleName");
    var lastname = localStorage.getItem("lastName");
    var email = localStorage.getItem("email");
    var password = localStorage.getItem("password");
    var cpassword = localStorage.getItem("confirmPassword");
    var birthday = localStorage.getItem("birthday");
    var age = localStorage.getItem("age");
    var contact = localStorage.getItem("contactNumber");
    var aadhar = localStorage.getItem("aadhar");
    var pan = localStorage.getItem("pan");
    var gender = localStorage.getItem("gender");

    var ccountry = localStorage.getItem("currentCountry");
    var cstate = localStorage.getItem("currentState");
    var cpinCode = localStorage.getItem("currentpinCode");
    var c1address = localStorage.getItem("currentAddressLine1");
    var c2address = localStorage.getItem("currentAddressLine2");

    var pcountry = localStorage.getItem("permanentCountry");
    var pstate = localStorage.getItem("permanentState");
    var ppinCode = localStorage.getItem("permanentPinCode");
    var p1address = localStorage.getItem("permanentAddressLine1");
    var p2address = localStorage.getItem("permanentAddressLine2");

    var instname10 = localStorage.getItem("instituteName10");
    var board10 = localStorage.getItem("board10");
    var marks10 = localStorage.getItem("marks10");
    var grade10Value = localStorage.getItem("aggregrate10");
    var yop10 = localStorage.getItem("yOP10");

    var instname12 = localStorage.getItem("instituteName12");
    var board12 = localStorage.getItem("board12");
    var marks12 = localStorage.getItem("marks12");
    var grade12Value = localStorage.getItem("aggregrate12");
    var yop12 = localStorage.getItem("yOP12");

    var instnameg = localStorage.getItem("instituteNameGraduation");
    var boardg = localStorage.getItem("boardGraduation");
    var marksg = localStorage.getItem("MarksGraduation");
    var gradegValue = localStorage.getItem("aggregrateGraduation");
    var yopg = localStorage.getItem("yOPGraduation");

    var message = localStorage.getItem("MessageEntered");
    var feedback = localStorage.getItem("FeedBackEntered");


    var displayDiv = document.getElementById("displayData");

    displayDiv.innerHTML = `

            <div class="column">
  <p>FirstName: ${firstname}</p>
  <p>MiddleName: ${middlename}</p>
  <p>LastName: ${lastname}</p>
  <p>Email: ${email}</p>
  <p>Password: ${password}</p>
  <p>ConfirmPassword: ${cpassword}</p>
</div>

<div class="column">
  <p>Birthday: ${birthday}</p>
  <p>Age: ${age}</p>
  <p>Gender: ${gender}</p>
  <p>ContactNumber: ${contact}</p>
  <p>Aadhar: ${aadhar}</p>
  <p>Pan: ${pan}</p>
</div>

<div class="column">
  <p>CurrentCountry: ${ccountry}</p>
  <p>CurrentSate: ${cstate}</p>
  <p>CurrentPinCode: ${cpinCode}</p>
  <p>CurrentAddressLine1: ${c1address}</p>
  <p>CurrentAddressLine2: ${c2address}</p>
</div>


    
<div class="column">

<p>PermanentCountry: ${pcountry}</p>
<p>PermanentSate: ${pstate}</p>
<p>PermanentPinCode: ${ppinCode}</p>
 
  <p>PermanentAddressLine1: ${p1address}</p>
  <p>PermanentAddressLine2: ${p2address}</p>
    </div>

    <div class="column">
    <p>InstituteName10: ${instname10}</p>
    <p>Board10: ${board10}</p>
    <p>Marks10: ${marks10}</p>
    <p>Aggregrate10: ${grade10Value}</p>
    <p>YOP10: ${yop10}</p>
    </div>

    <div class="column">
    <p>InstituteName12: ${instname12}</p>
    <p>Board12: ${board12}</p>
    <p>Marks12: ${marks12}</p>
    <p>Aggregrate12: ${grade12Value}</p>
    <p>YOP12: ${yop12}</p>
    </div>

    <div class="column">
    <p>InstituteNameGraduate: ${instnameg}</p>
    <p>BoardGraduate: ${boardg}</p>
    <p>MarksGraduatie: ${marksg}</p>
    <p>AggregrateGraduate: ${gradegValue}</p>
    <p>YOPGraduate: ${yopg}</p>
    </div>

    <div class="column">
    <p>Message: ${message}</p>
    <p>FeedBack: ${feedback}</p>
    </div>

    </div>
    

 
`;
    document.getElementById("customAlert").style.display = "block";
}

function validateAndStore() {
    var data = validateData();
    if (data) {
        storeData(data);
        displayUserData();
    }
}

function closeAlert() {
    // Close the custom alert
    document.getElementById("customAlert").style.display = "none";
}


function clearForm() {
    // Clear the content within the popup
    document.getElementById("displayData").innerHTML = "";
}



function resetForm() {
    // Clear specific input fields
    var inputFieldsToClear = [
        "fname", "mname", "lname", "email", "password", "cpassword",
        "age", "phone", "aadhar", "birthday", "pan",
        "ccountry", "cstate", "c1address", "c2address", "cpinCode",
        "pcountry", "pstate", "p1address", "p2address", "ppinCode",
        "instname10", "board10", "grade10Value", "yop10",
        "instname12", "board12", "grade12Value", "yop12",
        "instnameg", "boardg", "gradegValue", "yopg",
        "message", "feedback"
    ];

    inputFieldsToClear.forEach(fieldId => {
        document.getElementById(fieldId).value = "";
    });

    // Clear radio buttons
    var genderRadios = document.querySelectorAll('input[name="gender"]');
    genderRadios.forEach(radio => {
        radio.checked = false;
    });

    var marks10Radios = document.querySelectorAll('input[name="marks"]');
    marks10Radios.forEach(radio => {
        radio.checked = false;
    });

    var marks12Radios = document.querySelectorAll('input[name="marks12"]');
    marks12Radios.forEach(radio => {
        radio.checked = false;
    });

    var marksgRadios = document.querySelectorAll('input[name="marksg"]');
    marksgRadios.forEach(radio => {
        radio.checked = false;
    });

    clearLocalStorage();

    closeAlert();
}



function clearLocalStorage() {
    localStorage.clear();
}

