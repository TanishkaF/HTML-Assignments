﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="DemoUserManagement.web.UserDetails" %>
<%@ Register Src="~/NoteUserControl.ascx" TagPrefix="uc" TagName="NoteUserControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Admission Form ASP.NET</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link href="Content/UserDetailsStyleSheet.css" rel="stylesheet" />
    <script src="Scripts/DemoUserManagement.js"></script>    
</head>

<body>
    <form id="dataForm" runat="server">

        <div class="box-main-main">

            <h1><u>Admission Form</u></h1>

            <div>
                <fieldset>
                    <legend>
                        <h2>PERSONAL DETAILS:</h2>
                    </legend>

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <span id="starSign" class="text-danger font-weight-bold">*</span>
                                        <asp:Label runat="server" AssociatedControlID="firstName" Text="First Name:" />
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#129489;</span>
                                        </div>
                                        <asp:TextBox ID="firstName" runat="server" CssClass="form-control" Placeholder="First Name" ClientIDMode="Static" />
                                    </div>
                                    <div id="errorMessageFirstNameDiv" class="error-display-box error-message"></div>

                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <asp:Label runat="server" AssociatedControlID="middleName" Text="Middle Name:" />
                                    </div>
                                    <div>
                                        <asp:TextBox ID="middleName" runat="server" CssClass="form-control" Placeholder="Middle Name" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <asp:Label runat="server" AssociatedControlID="lastName" Text="Last Name:" />
                                    </div>
                                    <div>
                                        <asp:TextBox ID="lastName" runat="server" CssClass="form-control" Placeholder="Last Name" ClientIDMode="Static" />
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
                                        <asp:Label runat="server" AssociatedControlID="email" Text="Email:" />
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#9993;</span>
                                        </div>
                                        <asp:TextBox ID="email" runat="server" CssClass="form-control" Placeholder="Email" data-store="Email" ClientIDMode="Static" />
                                    </div>
                                    <div id="errorMessageEmailDiv" class="error-display-box"></div>
                                </div>
                            </div>

                            <!-- Column 2 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <span id="starSign" class="text-danger font-weight-bold">*</span>
                                        <asp:Label runat="server" AssociatedControlID="password" Text="Password:" />
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#128274;</span>
                                        </div>
                                        <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Password" data-store="Password" ClientIDMode="Static" />
                                    </div>
                                    <div id="errorMessagePasswordDiv" class="error-display-box"></div>
                                </div>
                            </div>

                            <!-- Column 3 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <!-- Two Divs in Column 3 -->
                                    <div>
                                        <span id="starSign" class="text-danger font-weight-bold">*</span>
                                        <asp:Label runat="server" AssociatedControlID="cpassword" Text="Confirm Password:" />
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#128274;</span>
                                        </div>
                                        <asp:TextBox ID="cpassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Confirm Password" data-store="ConfirmPassword" ClientIDMode="Static" />
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
                                        <asp:Label runat="server" AssociatedControlID="birthday" Text="Birthday:" />
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#127856;</span>
                                        </div>
                                        <asp:TextBox ID="birthday" runat="server" CssClass="form-control" Placeholder="Birthday" data-store="Birthday" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>

                            <!-- Column 2 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <!-- Two Divs in Column 3 -->
                                    <div>
                                        <asp:Label runat="server" Text="Gender:" />
                                    </div>
                                    <div class="radio-button">
                                        <div class="radio-container form-control">
                                            <asp:RadioButton ID="male" runat="server" Text="Male" GroupName="genderGroup" value="male" data-store="Gender" ClientIDMode="Static" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="female" runat="server" Text="Female" GroupName="genderGroup" value="female" data-store="Gender" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <!-- Column 3 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <div>
                                        <span id="starSign" class="text-danger font-weight-bold">*</span>
                                        <asp:Label runat="server" AssociatedControlID="phone" Text="Contact No:" />
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#x260E;</span>
                                        </div>
                                        <asp:TextBox ID="phone" runat="server" CssClass="form-control" Placeholder="Please Enter 10 Digits" data-store="Phone" ClientIDMode="Static" />
                                    </div>
                                    <div id="errorMessageContactDiv" class="error-display-box"></div>
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
                                        <asp:Label runat="server" AssociatedControlID="aadhar" Text="Aadhar No:" />
                                    </div>
                                    <div>
                                        <asp:TextBox ID="aadhar" runat="server" CssClass="form-control" Placeholder="Please Enter 12 Digits" data-store="Aadhar" ClientIDMode="Static" />
                                    </div>
                                    <div id="errorMessageAadharDiv" class="error-display-box"></div>
                                </div>
                            </div>

                            <div class="col-sm-4">
    <div class="p-3">
        <div>
            <asp:Label runat="server" Text="Uploaded Document:" />
        </div>
        <div class="input-group">
            <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" ClientIDMode="Static" />
        </div>
        <div id="errorMessageFileUploadDiv" class="error-display-box"></div>
    </div>
</div>

                          <%--  <!-- Column 3 -->
                            <div class="col-sm-4">
                                <div class="p-3">
                                    <!-- Two Divs in Column 3 -->
                                    <div>
                                        <asp:Label runat="server" AssociatedControlID="pan" Text="Pan No:" />
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1">&#x1F4B3;</span>
                                        </div>
                                        <asp:TextBox ID="pan" runat="server" CssClass="form-control" Placeholder="Pan Card" data-store="Pan" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>--%>

                        </div>
                    </div>
                </fieldset>
            </div>

            <div class="extra-space"></div>

            <!-- ADDRESS -->
            <div>
                <asp:Panel runat="server" ID="addressPanel">
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
                                            <asp:DropDownList ID="cCountry" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cCountry_SelectedIndexChanged" ClientIDMode="Static">
                                                <asp:ListItem Text="Select Country" Value="" Disabled="true" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
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
                                            <asp:DropDownList ID="cState" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                <asp:ListItem Text="Select State" Value="" Disabled="true" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="container-fluid">
                            <div class="row">
                                <!-- Column 1 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <asp:Label runat="server" AssociatedControlID="c1Address" Text="Address Line 1:" />
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127968;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="c1Address" CssClass="form-control" Placeholder="Address Line 1" data-store="CurrentAddressLine1" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3  ">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <asp:Label runat="server" AssociatedControlID="c2Address" Text="Address Line 2:" />
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127968;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="c2Address" CssClass="form-control" Placeholder="Address Line 2" data-store="CurrentAddressLine2" ClientIDMode="Static" />
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
                                            <asp:Label runat="server" AssociatedControlID="cPinCode" Text="Pincode:" />
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#x1F4CD;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="cPinCode" CssClass="form-control" Placeholder="Pincode" data-store="CurrentPinCode" ClientIDMode="Static" />
                                        </div>
                                        <div id="errorMessageCurrentPinCodeDiv" class="error-display-box"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="same-curr">
                            <asp:CheckBox runat="server" ID="sameAsCurrent" Text="Permanent Address Same as Current Address." Value="sameAsCurrent" OnCheckedChanged="CopyAddress" AutoPostBack="true" ClientIDMode="Static" />
                        </div>

                        <h3>Permanent Address:</h3>

                        <div>

                            <!-- Single Row with Two Columns -->
                            <div class="container-fluid">
                                <!-- Single Row with Two Columns -->
                                <div class="row">
                                    <!-- Column 1 - Country -->
                                    <div class="col-sm-6">
                                        <div class="p-3">
                                            <div>
                                                <label for="pCountry">Country:</label>
                                            </div>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" id="basic-addon1">&#127758;</span>
                                                </div>
                                                <asp:DropDownList ID="pCountry" runat="server" CssClass="form-control" DataStore="PermanentCountry" AutoPostBack="True" OnSelectedIndexChanged="pCountry_SelectedIndexChanged" ClientIDMode="Static">
                                                    <asp:ListItem Text="Select Country" Value="" Disabled="true" Selected="true" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- Column 2 - State -->
                                    <div class="col-sm-6">
                                        <div class="p-3">
                                            <div>
                                                <label for="pState">State:</label>
                                            </div>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" id="basic-addon1">&#x1F5FA;</span>
                                                </div>
                                                <asp:DropDownList ID="pState" runat="server" CssClass="form-control" DataStore="PermanentState" ClientIDMode="Static">
                                                    <asp:ListItem Text="Select State" Value="" Disabled="true" Selected="true" />
                                                </asp:DropDownList>
                                            </div>
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
                                            <asp:TextBox runat="server" ID="p1Address" name="p1address" Placeholder="Address Line 1" class="form-control" data-store="PermanentAddressLine1" ClientIDMode="Static" />
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
                                            <asp:TextBox runat="server" ID="p2Address" name="address" Placeholder="Address Line 2" class="form-control" data-store="PermanentAddressLine2" ClientIDMode="Static" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <asp:Label runat="server" AssociatedControlID="pPinCode" Text="Pincode:" />
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#x1F4CD;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="pPinCode" CssClass="form-control" Placeholder="PinCode" data-store="PermanentPinCode" ClientIDMode="Static" />
                                        </div>
                                        <div id="errorMessagePermanentPinCodeDiv" class="error-display-box"></div>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </fieldset>
                </asp:Panel>
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
                                            <asp:Label runat="server" AssociatedControlID="instName10" Text="Institute Name:" />
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127979;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="instName10" CssClass="form-control" placeholder="Institute Name" data-store="InstituteName10" ClientIDMode="Static" />
                                        </div>
                                        <div class="mandotory error-display-box" id="errorMessageInstitute10Div"></div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <asp:Label runat="server" AssociatedControlID="board10" Text="Board:" />
                                        </div>
                                        <div>
                                            <asp:DropDownList runat="server" ID="board10" CssClass="form-control" data-store="Board10">
                                                <asp:ListItem Text="Select Board" Value="" Disabled="true" Selected="true" />
                                                <asp:ListItem Text="ICSE" Value="icse" />
                                                <asp:ListItem Text="CBSE" Value="cbse" />
                                                <asp:ListItem Text="State Board" Value="satetBoard" />
                                            </asp:DropDownList>
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
                                            <asp:Label runat="server" Text="Marks:" />
                                        </div>
                                        <div class="radio-button">
                                            <div class="radio-container form-control">
                                                <asp:RadioButton runat="server" ID="cgpa10" Text="CGPA" GroupName="marks" value="cgpa" data-store="Marks10" ClientIDMode="Static" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton runat="server" ID="percentage" Text="Percentage" GroupName="marks" value="percentage" data-store="Marks10" ClientIDMode="Static" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <asp:Label runat="server" Text="Enter Aggregate:" />
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#128203;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="grade10Value" CssClass="form-control" Placeholder="Enter Aggregate" data-store="Aggregate10" ClientIDMode="Static" />
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
                                            <asp:Label runat="server" AssociatedControlID="yop10" Text="Year of Passing:" />
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127891;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="yop10" CssClass="form-control" Placeholder="YOP" data-store="YOP10" ClientIDMode="Static" />
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
                                            <asp:Label runat="server" AssociatedControlID="instName12" Text="Institute Name:" />
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127979;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="instName12" CssClass="form-control" Placeholder="Institute Name" data-store="InstituteName12" ClientIDMode="Static" />
                                        </div>
                                        <div class="mandotory error-display-box" id="errorMessageInstitute12Div"></div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <asp:Label runat="server" AssociatedControlID="board12" Text="Board:" />
                                        </div>
                                        <div>
                                            <asp:DropDownList runat="server" ID="board12" CssClass="form-control" DataStore="Board12">
                                                <asp:ListItem Text="Select Board" Value="" Disabled Selected></asp:ListItem>
                                                <asp:ListItem Text="ICSE" Value="icse"></asp:ListItem>
                                                <asp:ListItem Text="CBSE" Value="cbse"></asp:ListItem>
                                                <asp:ListItem Text="State Board" Value="stateBoard"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <label>Marks:</label>
                                        </div>
                                        <div>
                                            <div class="radio-button">
                                                <div class="radio-container form-control">
                                                    <asp:RadioButton runat="server" ID="cgpa12" Text="CGPA" GroupName="marks12" Value="cgpa" data-store="Marks12" ClientIDMode="Static" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                     <asp:RadioButton runat="server" ID="percentage12" Text="Percentage" GroupName="marks12" Value="percentage" data-store="Marks12" ClientIDMode="Static" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label>Enter Aggregate:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#128203;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="grade12Value" CssClass="form-control" Name="grade12Value" Placeholder="Enter Aggregate" data-store="Aggregate12" ClientIDMode="Static" />
                                        </div>
                                        <div id="errorMessageAggregate12Div" class="error-display-box"></div>
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
                                            <asp:TextBox runat="server" ID="yop12" Name="yop12" Placeholder="YOP" data-store="YOP12" CssClass="form-control" ClientIDMode="Static" />
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
                                            <asp:Label runat="server" AssociatedControlID="instNameG" Text="Institute Name:" />
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127979;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="instNameG" CssClass="form-control" Placeholder="Institute Name" data-store="InstituteNameG" ClientIDMode="Static" />
                                        </div>
                                        <div class="mandotory error-display-box" id="errorMessageInstituteDiv"></div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <asp:Label runat="server" AssociatedControlID="boardG" Text="Board:" />
                                        </div>
                                        <div>
                                            <asp:DropDownList runat="server" ID="boardG" CssClass="form-control" data-store="BoardG">
                                                <asp:ListItem Text="Select Board" Value="" Disabled="true" Selected="true" />
                                                <asp:ListItem Text="ICSE" Value="icse" />
                                                <asp:ListItem Text="CBSE" Value="cbse" />
                                                <asp:ListItem Text="State Board" Value="stateBoard" />
                                            </asp:DropDownList>
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
                                            <label for="marks">Marks:</label>
                                        </div>
                                        <div>
                                            <div class="radio-button">
                                                <div class="radio-container form-control">
                                                    <asp:RadioButton ID="cgpaG" runat="server" Text="CGPA" GroupName="marksg" Value="cgpa" data-store="MarksG" ClientIDMode="Static" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="percentageG" runat="server" Text="Percentage" GroupName="marksg" Value="percentage" data-store="MarksG" ClientIDMode="Static" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Column 2 -->
                                <div class="col-sm-6">
                                    <div class="p-3">
                                        <!-- Two Divs in Column 2 -->
                                        <div>
                                            <span id="starSign" class="text-danger font-weight-bold">*</span>
                                            <label for="aggregate">Enter Aggregate:</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#128203;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="gradeGValue" CssClass="form-control" Name="gradegValue" Placeholder="Enter Aggregate" data-store="AggregateG" ClientIDMode="Static" />
                                        </div>
                                        <div id="errorMessageAggregateGDiv" class="error-display-box"></div>
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
                                            <asp:Label runat="server" AssociatedControlID="yopG" Text="Year of Passing:" />
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon1">&#127891;</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="yopG" CssClass="form-control" Placeholder="YOP" data-store="YOPG" ClientIDMode="Static" />
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

            <%--Hobby--%>
            <div>
                <fieldset>
                    <legend>
                        <h2>HOBBY:</h2>
                    </legend>

                    <div class="container mt-4">
                        <div class="row mb-3">
                            <!-- First Row -->
                            <div class="col">
                                <div class="form-check hobby_check">
                                    <asp:CheckBox ID="checkbox1" runat="server" CssClass="form-check-input" Text="Dancing" ClientIDMode="Static" />
                                    <label class="form-check-label" for="checkbox1"></label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="checkbox2" runat="server" CssClass="form-check-input" Text="Singing" ClientIDMode="Static" />
                                    <label class="form-check-label" for="checkbox2"></label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="checkbox3" runat="server" CssClass="form-check-input" Text="Coding" ClientIDMode="Static" />
                                    <label class="form-check-label" for="checkbox3"></label>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <!-- Second Row -->
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="checkbox4" runat="server" CssClass="form-check-input" Text="Web Designing" Value="Web Designing" ClientIDMode="Static" />
                                    <label class="form-check-label" for="checkbox4"></label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="checkbox5" runat="server" CssClass="form-check-input" Text="Board Games" Value="Board Games" ClientIDMode="Static" />
                                    <label class="form-check-label" for="checkbox5"></label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="checkbox6" runat="server" CssClass="form-check-input" Text="Camping" Value="Camping" ClientIDMode="Static" />
                                    <label class="form-check-label" for="checkbox6"></label>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <!-- Third Row -->
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="checkbox7" runat="server" CssClass="form-check-input" Text="Running" Value="Running" ClientIDMode="Static" />
                                    <label class="form-check-label" for="checkbox7"></label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="checkbox8" runat="server" CssClass="form-check-input" Text="Sleeping" Value="Sleeping" ClientIDMode="Static" />
                                    <label class="form-check-label" for="checkbox8"></label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="checkbox9" runat="server" CssClass="form-check-input" Text="Reading" Value="Reading" ClientIDMode="Static" />
                                    <label class="form-check-label" for="checkbox9"></label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="container mt-4">
                        <div class="message-feedback-container">
                            <h3>Message and Feedback:</h3>

                            <div class="form-group">
                                <label for="message">Message:</label>
                                <asp:TextBox runat="server" ID="message" CssClass="form-control text-message-feedback" Rows="3" Placeholder="Enter message you want to provide." data-store="Message" />
                            </div>

                            <div class="form-group">
                                <label for="feedback">Feedback:</label>
                                <asp:TextBox runat="server" ID="feedback" CssClass="form-control text-message-feedback" Rows="4" Placeholder="Please feel free to give any feedback to us." data-store="Feedback" />
                            </div>
                        </div>
                    </div>

                </fieldset>
            </div>

            <uc:NoteUserControl ID="NoteUserControl" runat="server" />
            <div class="fixed-bottom p-3 bg-transparent d-flex justify-content-end footer-buttons">
                <asp:Button ID="submitButton" runat="server" CssClass="btn btn-success SubmitButton" Text="Submit" OnClientClick="return validateForm();" OnClick="SubmitClick" ClientIDMode="Static" />
                <%--                <asp:Button ID="submitButton" runat="server" CssClass="btn btn-success SubmitButton" Text="Submit"  OnClick="SubmitClick" ClientIDMode="Static" />--%>
                <asp:Button ID="resetButton" runat="server" CssClass="btn btn-danger ResetButton ml-2" Text="Reset" OnClientClick="return resetForm();" ClientIDMode="Static" />
            </div>
        </div>

    </form>

</body>

</html>

