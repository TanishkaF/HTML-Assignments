﻿<%@ Page Language="C#" Title="User Detail Form" AutoEventWireup="true" CodeBehind="UserDetailsV2.aspx.cs" Inherits="DemoUserManagement.web.UserDetailsV2" %>


<!DOCTYPE html>
<html>

<head>
    <title>Admission Form Bootstrap</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link href="Content/index.css" rel="stylesheet" />
</head>

<body>

    <div class="border">

        <h1><u>Admission Form</u></h1>

        <form name='registration' id="dataForm">


            <!-- PERSONAL INFO -->
            <div>
                <fieldset>

                    <legend>
                        <h2>PERSONAL DETAILS:</h2>
                    </legend>

                    <div class="container-fluid">
                        <!-- Single Row with Three Columns -->
                        <div class="row">
                            <!-- Column 1 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <span id="starSign" class="text-danger font-weight-bold">*</span>
                                        <label for="fname">First Name:</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#129489;</span>
                                        </div>
                                        <input type="text" id="firstName" name="name" placeholder="First Name"
                                            class="form-control" data-store="FirstName">
                                    </div>
                                    <div class="error-display-box" id="errorMessageFirstNameDiv"></div>
                                </div>
                            </div>

                            <!-- Column 2 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <label for="mname">Middle Name:</label>
                                    </div>
                                    <div>
                                        <input type="text" id="middleName" name="name" placeholder="Middle Name"
                                            data-store="SecondName" class="form-control">
                                    </div>
                                </div>
                            </div>

                            <!-- Column 3 -->
                            <div class="col-sm-4">
                                <div class="p-3   ">
                                    <!-- Two Divs in Column 3 -->
                                    <div>
                                        <label for="lname">Last Name:</label>
                                    </div>
                                    <div>
                                        <input type="text" id="lastName" name="name" placeholder="Last Name"
                                            data-store="LastName" class="form-control">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="container-fluid">
                        <!-- Single Row with Three Columns -->
                        <div class="row">
                            <!-- Column 1 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <span id="starSign" class="text-danger font-weight-bold">*</span>
                                        <label for="email">Email:</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#9993;</span>
                                        </div>
                                        <input type="text" id="email" name="email" placeholder="Email"
                                            class="form-control" data-store="Email">
                                    </div>
                                    <div id="errorMessageDisplayBox" class="error-display-box"></div>

                                </div>
                            </div>

                            <!-- Column 2 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <span id="starSign" class="text-danger font-weight-bold">*</span>
                                        <label for="password">Password:</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#128274;</span>
                                        </div>
                                        <input type="password" id="password" name="password" placeholder="Password"
                                            data-store="Password" class="form-control">
                                    </div>
                                    <div id="errorMessagePasswordDiv" class="error-display-box"></div>

                                </div>
                            </div>

                            <!-- Column 3 -->
                            <div class="col-sm-4">
                                <div class="p-3   ">
                                    <!-- Two Divs in Column 3 -->
                                    <div>
                                        <span id="starSign" class="text-danger font-weight-bold"
                                            class="text-danger font-weight-bold">*</span>
                                        <label for="password">Confirm Password:</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#128274;</span>
                                        </div>
                                        <input type="password" id="cpassword" name="cpassword"
                                            placeholder="Confirm Password" data-store="ConfirmPassword"
                                            class="form-control">
                                    </div>
                                    <div id="errorMessageConfirmPasswordDiv" class="error-display-box"></div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="container-fluid">
                        <!-- Single Row with Three Columns -->
                        <div class="row">
                            <!-- Column 1 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <label for="birthday">Birthday:</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#127856;</span>
                                        </div>
                                        <input type="date" id="birthday" name="birthday" data-store="Birthday"
                                            class="form-control">
                                    </div>
                                </div>
                            </div>

                           
                            <div class="col-sm-4">
                                <div class="p-3   ">
                                    <!-- Two Divs in Column 3 -->
                                    <div>
                                        <label for="gender">Gender:</label>
                                    </div>
                                    <div class="radio-button">
                                        <div class="radio-container form-control">
                                            <input type="radio" id="male" name="gender" value="male"
                                                data-store="Gender">
                                            <label for="male">Male</label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <input type="radio" id="female" name="gender" value="female"
                                                data-store="Gender">
                                            <label for="female">Female</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="container-fluid">
                        <!-- Single Row with Three Columns -->
                        <div class="row">
                            <!-- Column 1 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <span id="starSign" class="text-danger font-weight-bold">*</span>
                                        <label for="phone">Contact No:</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#x260E;</span>
                                        </div>
                                        <input type="numbers" name="phone" id="phone"
                                            placeholder="Please Enter 10 Digits" data-store="Phone"
                                            class="form-control">
                                    </div>
                                    <div id="errorMessageContactDiv" class="error-display-box"></div>
                                </div>
                            </div>

                            <!-- Column 2 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <span id="starSign" class="text-danger font-weight-bold">*</span>
                                        <label for="aadhar">Aadhar No:</label>
                                    </div>
                                    <div>
                                        <input type="numbers" name="aadhar" id="aadhar"
                                            placeholder="Please Enter 12 Digits" data-store="Aadhar"
                                            class="form-control">
                                    </div>
                                    <div id="errorMessageAadharDiv" class="error-display-box"></div>
                                </div>
                            </div>

                            <!-- Column 3 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <label for="fileUpload">Upload Aadhar Card PDF:</label>
                                    <input type="file" id="fileUpload" class="form-control" />

                                    <div class="form-group">
                                        <label id="aadharCardUploadLabel"></label>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>

                </fieldset>
            </div>

            <div class="extra-space"></div>

            <!-- ADDRESS -->
            <div>
                <fieldset>

                    <legend>
                        <h2>ADDRESS:</h2>
                    </legend>


                    <h3>Current Address:</h3>
                    <div class="container-fluid">
                        <!-- Single Row with Two Columns -->
                        <div class="row">
                            <!-- Column 1 -->
                            <div class="col-sm-6">
                                <div class="p-3">
                                    <div>
                                        <label for="country">Country:</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#127758;</span>
                                        </div>
                                        <select id="cCountry" name="country" data-store="CurrentCountry"
                                            class="form-control" onchange="getStates(this.value, 'cState')">
                                            <option value="" disabled selected>Select Country</option>
                                        </select>
                                    </div>
                                </div>
                            </div>

                            <!-- Column 2 -->
                            <div class="col-sm-6">
                                <div class="p-3">
                                    <!-- Two Divs in Column 2 -->
                                    <div>
                                        <label for="state">State:</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#x1F5FA;</span>
                                        </div>
                                        <select id="cState" name="state" class="form-control" data-store="CurrentState">
                                            <option value="" disabled selected>Select State</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="container-fluid">
                        <!-- Single Row with Two Columns -->
                        <div class="row">
                            <!-- Column 1 -->
                            <div class="col-sm-6">
                                <div class="p-3">
                                    <div>
                                        <label for="c1address">Address Line 1:</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#127968;</span>
                                        </div>
                                        <input type="text" id="c1Address" name="address" placeholder="Address Line 1"
                                            class="form-control" data-store="CurrentAddressLine1">
                                    </div>
                                </div>
                            </div>

                            <!-- Column 2 -->
                            <div class="col-sm-6">
                                <div class="p-3  ">
                                    <!-- Two Divs in Column 2 -->
                                    <div>
                                        <label for="c2address">Address Line 2:</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#127968;</span>
                                        </div>
                                        <input type="text" id="c2Address" name="address" placeholder="Address Line 2"
                                            class="form-control" data-store="CurrentAddressLine2">
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="container-fluid">
                        <!-- Single Row with Two Columns -->
                        <div class="row">
                            <!-- Column 1 -->
                            <div class="col-sm-6">
                                <div class="p-3">
                                    <div>
                                        <span id="starSign" class="text-danger font-weight-bold">*</span>
                                        <label for="pinCode">Pincode:</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#x1F4CD;</span>
                                        </div>
                                        <input type="numbers" id="cPinCode" name="pinCode" placeholder="Pincode"
                                            class="form-control" data-store="CurrentPinCode">
                                    </div>
                                    <div id="errorMessageCurrentPinCodeDiv" class="error-display-box"></div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="same-curr">
                        <input type="checkbox" id="sameAsCurrent" name="sameAsCurrent" value="sameAsCurrent"
                            onclick="copyAddress()">
                        <label for="sameAsCurrent">Permanent Address Same as Current Address.</label>
                    </div>

                    <h3>Permanent Address:</h3>

                    <div>
                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <label for="country">Country:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127758;</span>
                                            </div>
                                            <select id="pCountry" name="country" data-store="PermanentCountry"
                                                class="form-control" onchange="getStates(this.value, 'pState')">
                                                <option value="" disabled selected>Select Country</option>                                              
                                            </select>
                                        </div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3   ">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <label for="state">State:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#x1F5FA;</span>
                                            </div>
                                            <select id="pState" name="state" class="form-control"
                                                data-store="PermanentState">
                                                <option value="" disabled selected>Select State</option>
                                            </select>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <label for="p1address">Address Line 1:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127968;</span>
                                            </div>
                                            <input type="text" id="p1Address" name="p1address"
                                                placeholder="Address Line 1" class="form-control"
                                                data-store="PermanentAddressLine1">
                                        </div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3   ">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <label for="p2address">Address Line 2:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127968;</span>
                                            </div>
                                            <input type="text" id="p2Address" name="address"
                                                placeholder="Address Line 2" class="form-control"
                                                data-store="PermanentAddressLine2">
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label for="pinCode">Pincode:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#x1F4CD;</span>
                                            </div>
                                            <input type="numbers" id="pPinCode" name="pPinCode" placeholder="PinCode"
                                                data-store="PermanentPinCode" class="form-control">
                                        </div>
                                        <div id="errorMessagePermanentPinCodeDiv" class="error-display-box"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>





                </fieldset>
            </div>

            <div class="extra-space"></div>

            <!-- EDUCATION -->
            <div>
                <fieldset>

                    <legend>
                        <h2>EDUCATION:</h2>
                    </legend>

                    <h3>MATRICULATION(10)</h3>
                    <div>
                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label for="instname">Institute Name:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127979;</span>
                                            </div>
                                            <input type="text" id="instName10" class="form-control" name="instname"
                                                placeholder="Institute Name" data-store="InstituteName10">
                                        </div>
                                        <div class="mandotory error-display-box" id="errorMessageInstitute10Div"></div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3  ">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <label for="board">Board:</label>
                                        </div>
                                        <div>
                                            <select id="board10" class="form-control" name="board" data-store="Board10">
                                                <option value="" disabled selected>Select Board</option>
                                                <option value="icse">ICSE</option>
                                                <option value="cbse">CBSE</option>
                                                <option value="satetBoard">State Board</option>
                                            </select>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <label>Marks:</label>
                                        </div>
                                        <div class="radio-button">
                                            <div class="radio-container form-control">
                                                <input type="radio" id="cgpa10" name="marks" value="cgpa"
                                                    data-store="Marks10">
                                                <label for="cgpa10">CGPA</label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <input type="radio" id="percentage10" name="marks" value="percentage"
                                                    data-store="Marks10">
                                                <label for="percentage">Percentage</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3  ">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label>Enter Aggregate:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#128203;</span>
                                            </div>
                                            <input type="text" id="grade10Value" class="form-control"
                                                name="grade10Value" placeholder="Enter Aggregate"
                                                data-store="Aggregate10">
                                        </div>
                                        <div id='errorMessageAggregate10Div' class="error-display-box"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label for="yop">Year of Passing:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127891;</span>
                                            </div>
                                            <input type="text" id="yop10" name="yop10" placeholder="YOP"
                                                data-store="YOP10" class="form-control">
                                        </div>
                                        <div id="errorMessageYOP10Div" class="error-display-box"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <h3>Intermediate(12)</h3>
                    <div>
                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label for="instname">Institute Name:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127979;</span>
                                            </div>
                                            <input type="text" id="instName12" class="form-control" name="instname"
                                                placeholder="Institute Name" data-store="InstituteName12">
                                        </div>
                                        <div class="mandotory error-display-box" id="errorMessageInstitute12Div"></div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3  ">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <label for="board">Board:</label>
                                        </div>
                                        <div>
                                            <select id="board12" class="form-control" name="board" data-store="Board12">
                                                <option value="" disabled selected>Select Board</option>
                                                <option value="icse">ICSE</option>
                                                <option value="cbse">CBSE</option>
                                                <option value="satetBoard">State Board</option>
                                            </select>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <label>Marks:</label>
                                        </div>
                                        <div>
                                            <div class="radio-button">
                                                <div class="radio-container form-control">
                                                    <input type="radio" id="cgpa12" name="marks" value="cgpa"
                                                        data-store="Marks12">
                                                    <label for="cgpa">CGPA</label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input type="radio" id="percentage12" name="marks" value="percentage"
                                                        data-store="Marks12">
                                                    <label for="percentage">Percentage</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3  ">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label>Enter Aggregate:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#128203;</span>
                                            </div>
                                            <input type="text" id="grade12Value" class="form-control"
                                                name="grade12Value" placeholder="Enter Aggregate"
                                                data-store="Aggregate12">
                                        </div>
                                        <div id='errorMessageAggregate12Div' class="error-display-box"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label for="yop">Year of Passing:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127891;</span>
                                            </div>
                                            <input type="text" id="yop12" name="yop12" placeholder="YOP"
                                                data-store="YOP12" class="form-control">
                                        </div>
                                        <div id="errorMessageYOP12Div" class="error-display-box"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <h3>Graduate:</h3>
                    <div>
                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label for="instname">Institute Name:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127979;</span>
                                            </div>
                                            <input type="text" id="instNameG" class="form-control" name="instname"
                                                placeholder="Institute Name" data-store="InstituteNameG">
                                        </div>
                                        <div class="mandotory error-display-box" id="errorMessageInstituteDiv"></div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3  ">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <label for="board">Board:</label>
                                        </div>
                                        <div>
                                            <select id="boardG" class="form-control" name="board" data-store="BoardG">
                                                <option value="" disabled selected>Select Board</option>
                                                <option value="icse">ICSE</option>
                                                <option value="cbse">CBSE</option>
                                                <option value="satetBoard">State Board</option>
                                            </select>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <label>Marks:</label>
                                        </div>
                                        <div>
                                            <div class="radio-button">
                                                <div class="radio-container form-control">
                                                    <input type="radio" id="cgpaG" name="marksg" value="cgpa"
                                                        data-store="MarksG">
                                                    <label for="cgpa">CGPA</label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input type="radio" id="percentageG" name="marksg"
                                                        value="percentage" data-store="Marks12">
                                                    <label for="percentage">Percentage</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3  ">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label>Enter Aggregate:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#128203;</span>
                                            </div>
                                            <input type="text" id="gradeGValue" class="form-control" name="gradegValue"
                                                placeholder="Enter Aggregate" data-store="AggregateG">
                                        </div>
                                        <div id='errorMessageAggregateGDiv' class="error-display-box"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container-fluid">
                            <!-- Single Row with Two Columns -->
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label for="yop">Year of Passing:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127891;</span>
                                            </div>
                                            <input type="text" id="yopG" name="yopg" placeholder="YOP" data-store="YOPG"
                                                class="form-control">
                                        </div>
                                        <div id="errorMessageYOPGDiv" class="error-display-box"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                </fieldset>
            </div>

            <div class="extra-space"></div>

            <!-- HOBBY -->
            <div>
                <fieldset>

                    <legend>
                        <h2>HOBBY:</h2>
                    </legend>


                    <div class="container mt-4">
                        <div class="row mb-3">
                            <!-- First Row -->
                            <div class="col">
                                <div class="form-check">
                                    <input class="form-check-input  hobby-checkbox" type="checkbox" value="Dancing" id="checkbox1">
                                    <label class="form-check-label" for="checkbox1">Dancing</label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <input class="form-check-input  hobby-checkbox" type="checkbox" value="Singing" id="checkbox2">
                                    <label class="form-check-label" for="checkbox2">Singing</label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <input class="form-check-input  hobby-checkbox" type="checkbox" value="Coding" id="checkbox3">
                                    <label class="form-check-label" for="checkbox3">Coding</label>
                                </div>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <!-- Second Row -->
                            <div class="col">
                                <div class="form-check">
                                    <input class="form-check-input  hobby-checkbox" type="checkbox" value="Web Designing"
                                        id="checkbox4">
                                    <label class="form-check-label" for="checkbox4">Web Designing</label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <input class="form-check-input  hobby-checkbox" type="checkbox" value="Board Games" id="checkbox5">
                                    <label class="form-check-label" for="checkbox5">Board Games</label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <input class="form-check-input  hobby-checkbox" type="checkbox" value="Camping" id="checkbox6">
                                    <label class="form-check-label" for="checkbox6">Camping</label>
                                </div>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <!-- Third Row -->
                            <div class="col">
                                <div class="form-check">
                                    <input class="form-check-input hobby-checkbox" type="checkbox" value="Running" id="checkbox7">
                                    <label class="form-check-label" for="checkbox7">Running</label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <input class="form-check-input  hobby-checkbox" type="checkbox" value="Sleeping" id="checkbox8">
                                    <label class="form-check-label" for="checkbox8">Sleeping</label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <input class="form-check-input  hobby-checkbox" type="checkbox" value="Reading" id="checkbox9">
                                    <label class="form-check-label" for="checkbox9">Reading</label>
                                </div>
                            </div>
                        </div>


                    </div>

                    <div class="container mt-4">
                        <div class="message-feedback-container">
                            <h3>Message and Feedback:</h3>

                            <div class="form-group">
                                <label for="message">Message:</label>
                                <textarea class="form-control text-message-feedback" id="message" name="message"
                                    rows="3" placeholder="Enter message you want to provide."
                                    data-store="Message"></textarea>
                            </div>

                            <div class="form-group">
                                <label for="feedback">Feedback:</label>
                                <textarea class="form-control text-message-feedback" id="feedback" name="feedback"
                                    rows="4" placeholder="Please feel free to give any feedback to us."
                                    data-store="Feedback"></textarea>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>

            <!-- BUTTONS -->
            <div class="fixed-bottom p-3 bg-transparent d-flex justify-content-end footer-buttons">
                <button type="button" class="btn btn-success SubmitButton" id="submitButton" onclick="return submitUserDetails()">Submit</button>
                <button type="button" class="btn btn-danger ResetButton ml-2" onclick="resetForm()">Reset</button>
            </div>

        </form>
    </div>

    <!-- Add Bootstrap JS and Popper.js -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="Scripts/UserDetailsV2.js"></script>
    <script src="Scripts/CheckEmail.js"></script>
</body>

</html>
