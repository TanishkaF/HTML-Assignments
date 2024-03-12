using DemoUserManagement.BusinessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement.web
{
    public partial class UserDetails : Page
    
    {
        private bool authorizationChecked = false;
        public int test = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
           CheckAuthorizationAndLoadUserDetails();

            if (!IsPostBack)
            {
                test = 5;
                if (!string.IsNullOrEmpty(Request.QueryString["StudentID"]))
                {
                    int studentID;
                    if (int.TryParse(Request.QueryString["StudentID"], out studentID))
                    {
                        NoteUserControl.ObjectID = studentID;
                        NoteUserControl.ObjectType = NoteType.ObjectType;
                        DocumentUserControl.ObjectId = studentID;
                        DocumentUserControl.ObjectType = StudentDocumentType.ObjectType;
                        DocumentUserControl.DropDownList = StudentDocumentType.studentDocument;

                        PopulateStudentTableDetails(studentID);
                        PopulatedAddressDetails(studentID);
                        PopulatedEducationDetails(studentID);
                    }
                }
                else
                {
                    PopulateCountries(cCountry);
                    PopulateCountries(pCountry);
                }
            }
            else
            {
                var test2 = test;
            }
        }


        protected void SubmitClick(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["StudentID"]))
            {
                int userID = Convert.ToInt32(Request.QueryString["StudentID"]);

                UserDetailsViewModel studentDetailsTable = GetStudentTableDetails();

                BtnUpload_Click(sender, e, studentDetailsTable);
                UserBusiness.UpdateUserDetails(userID, studentDetailsTable);

                AddressDetailViewModel addressDetailsCurrent = GetAddressDetails(userID, AddressType.CurrentAddress);
                UserBusiness.UpdateAddressDetails(userID, AddressType.CurrentAddress, addressDetailsCurrent);

                AddressDetailViewModel addressDetailsPermanent = GetAddressDetails(userID, AddressType.PermanentAddress);
                UserBusiness.UpdateAddressDetails(userID, AddressType.PermanentAddress, addressDetailsPermanent);
                EducationDetailViewModel educationDetails10 = GetEducationDetails(userID, EducationType.MatriculationEducation);
                UserBusiness.UpdateEducationDetails(userID, EducationType.MatriculationEducation, educationDetails10);
                EducationDetailViewModel educationDetails12 = GetEducationDetails(userID, EducationType.IntermediateEducation);
                UserBusiness.UpdateEducationDetails(userID, EducationType.IntermediateEducation, educationDetails12);
                EducationDetailViewModel educationDetailsGraduate = GetEducationDetails(userID, EducationType.GraduateEducation);
                UserBusiness.UpdateEducationDetails(userID, EducationType.GraduateEducation, educationDetailsGraduate);

                string email = UserBusiness.GetEmailByUserID(userID);

                if (AuthenticationServiceBusiness.IsAdmin(email))
                {
                    Response.Redirect("UserList.aspx");
                }
                else
                {
                    Response.Redirect($"UserDetails.aspx?StudentID={userID}");
                }

              //  UpdateUserDetails(userID);
            }
            else
            {
                UserDetailsViewModel studentDetails = GetStudentTableDetails();
                BtnUpload_Click(sender, e, studentDetails);
                UserBusiness.InsertUserDetails(studentDetails);

                int userID = UserBusiness.GetLastInsertedUserID();
                UserBusiness.InsertUserRoll(userID);

                AddressDetailViewModel addressDetails1 = GetAddressDetails(userID, AddressType.CurrentAddress);
                addressDetails1.AddressType = AddressType.CurrentAddress;
                UserBusiness.InsertAddressDetails(addressDetails1);

                if (sameAsCurrent.Checked)
                {
                    AddressDetailViewModel addressDetails2 = GetAddressDetails(userID, AddressType.PermanentAddress);
                    addressDetails2.UserID = userID;
                    addressDetails2.AddressType = AddressType.PermanentAddress;
                    UserBusiness.InsertAddressDetails(addressDetails2);
                }
                else
                {
                    AddressDetailViewModel addressDetailsPermanent = GetAddressDetails(userID, AddressType.PermanentAddress);
                    UserBusiness.InsertAddressDetails(addressDetailsPermanent);
                }


                EducationDetailViewModel educationDetailViewModel10 = GetEducationDetails(userID, EducationType.MatriculationEducation);
                UserBusiness.InsertEducationDetails(educationDetailViewModel10);
                EducationDetailViewModel educationDetailViewModel12 = GetEducationDetails(userID, EducationType.IntermediateEducation);
                UserBusiness.InsertEducationDetails(educationDetailViewModel12);
                EducationDetailViewModel educationDetailViewModelGraduate = GetEducationDetails(userID, EducationType.GraduateEducation);
                UserBusiness.InsertEducationDetails(educationDetailViewModelGraduate);                

                UpdateUserDetails(userID);
            }
        }        
        
        protected void ResetButton_Click(object sender, EventArgs e)
        {
            foreach (Control control in Page.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
                else if (control is DropDownList)
                {
                    ((DropDownList)control).ClearSelection();
                }
            }
        }       

        protected void UpdateUserDetails(int userID)
        {
            Response.Redirect($"UserDetails.aspx?StudentID={userID}");
            // Response.Redirect($"UserList.aspx");
        }

        protected void BtnUpload_Click(object sender, EventArgs e, UserDetailsViewModel studentDetailsTable)
        {
            UploadFile uploadFileHandler = new UploadFile();

            if (fileUpload.HasFile)
            {
                string uploadedFileName = uploadFileHandler.UploadFileToServer(fileUpload.PostedFile);

                if (!string.IsNullOrEmpty(uploadedFileName))
                {
                    studentDetailsTable.DiskDocumentName = uploadedFileName;
                    studentDetailsTable.OriginalDocumentName = fileUpload.FileName;                    
                }
                else
                {
                    
                }
            }
            else
            {
                
            }
        }

        //protected void BtnUpload_Click(object sender, EventArgs e, UserDetailsViewModel studentDetailsTable)
        //{
        //    if (fileUpload.HasFile)
        //    {
        //        try
        //        {
        //            string uploadFolderPath = ConfigurationManager.AppSettings["UploadFolderPath"];

        //            string fileName = Path.GetFileName(fileUpload.FileName);
        //            string fileExtension = Path.GetExtension(fileName);

        //            if (fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
        //            {
        //                string physicalUploadFolderPath = uploadFolderPath;

        //                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

        //                string filePath = Path.Combine(physicalUploadFolderPath, uniqueFileName);

        //                fileUpload.SaveAs(filePath);

        //                studentDetailsTable.DiskDocumentName = uniqueFileName;
        //                studentDetailsTable.OriginalDocumentName = fileName;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.AddData(ex);
        //        }
        //    }
        //    else
        //    {
        //        // TODO: Handle case when no file is selected
        //    }
        //}

        private UserDetailsViewModel GetStudentTableDetails()
        {
            UserDetailsViewModel studentDetailsTable = new UserDetailsViewModel();
            studentDetailsTable.FirstName = GetValueFromTextBox(firstName);
            studentDetailsTable.MiddleName = GetValueFromTextBox(middleName);
            studentDetailsTable.LastName = GetValueFromTextBox(lastName);
            studentDetailsTable.Email = GetValueFromTextBox(email);
            studentDetailsTable.Phone = GetValueFromTextBox(phone);
            studentDetailsTable.AadharNumber = GetValueFromTextBox(aadhar);
            studentDetailsTable.DateOfBirth = GetValueFromTextBox(birthday);
            studentDetailsTable.Gender = male.Checked ? "Male" : "Female";
            studentDetailsTable.Hobbies = HobbySelected();
            studentDetailsTable.DiskDocumentName = "";
            studentDetailsTable.OriginalDocumentName = "";
            studentDetailsTable.Password= GetValueFromTextBox(password);

            return studentDetailsTable;
        }      

        private AddressDetailViewModel GetAddressDetails(int userID, int addressType)
        {
            AddressDetailViewModel addressDetails = new AddressDetailViewModel();

            // Assuming you have controls for address details on your page
            addressDetails.UserID = userID;
            addressDetails.AddressType = addressType;

            if (addressType == AddressType.CurrentAddress)
            {
                addressDetails.CountryID = GetSelectedCountryID(cCountry); // Assuming you have a method to get the selected country ID
                addressDetails.StateID = GetSelectedStateID(cState); // Assuming you have a method to get the selected state ID
                addressDetails.AddressLine1 = GetValueFromTextBox(c1Address);
                addressDetails.AddressLine2 = GetValueFromTextBox(c2Address);
                addressDetails.Pincode = GetValueFromTextBox(cPinCode);
            }
            else if (addressType == AddressType.PermanentAddress)
            {
                addressDetails.CountryID = GetSelectedCountryID(pCountry); // Assuming you have a method to get the selected country ID
                addressDetails.StateID = GetSelectedStateID(pState); // Assuming you have a method to get the selected state ID
                addressDetails.AddressLine1 = GetValueFromTextBox(p1Address);
                addressDetails.AddressLine2 = GetValueFromTextBox(p2Address);
                addressDetails.Pincode = GetValueFromTextBox(pPinCode);
            }



            return addressDetails;
        }

        private EducationDetailViewModel GetEducationDetails(int userID, int educationType)
        {
            EducationDetailViewModel educationDetails = new EducationDetailViewModel();

            educationDetails.StudentID = userID;
            educationDetails.EducationType = educationType;

            if (educationType == EducationType.MatriculationEducation)
            {
                // Assuming controls for 10th education on your page
                educationDetails.InstituteName = GetValueFromTextBox(instName10);
                educationDetails.Board = board10.SelectedValue;
                if (cgpa10.Checked)
                {
                    educationDetails.Marks = "CGPA";
                }
                else if (percentage.Checked)
                {
                    educationDetails.Marks = "Percentage";
                }
                string grade10TextBox = GetValueFromTextBox(grade10Value);
                if (decimal.TryParse(grade10TextBox, out decimal aggregate))
                {
                    educationDetails.Aggregate = aggregate;
                }
                string yop10Value = GetValueFromTextBox(yop10);
                if (!string.IsNullOrEmpty(yop10Value))
                {
                    educationDetails.YearOfCompletion = int.Parse(yop10Value);
                }
            }
            else if (educationType == EducationType.IntermediateEducation)
            {
                // Assuming controls for 12th education on your page
                educationDetails.InstituteName = GetValueFromTextBox(instName12);
                educationDetails.Board = board12.SelectedValue;
                if (cgpa12.Checked)
                {
                    educationDetails.Marks = "CGPA";
                }
                else if (percentage12.Checked)
                {
                    educationDetails.Marks = "Percentage";
                }
                string grade12TextBox = GetValueFromTextBox(grade12Value);
                if (decimal.TryParse(grade12TextBox, out decimal aggregate))
                {
                    educationDetails.Aggregate = aggregate;
                }
                string yop12Value = GetValueFromTextBox(yop12);
                if (!string.IsNullOrEmpty(yop12Value))
                {
                    educationDetails.YearOfCompletion = int.Parse(yop12Value);
                }
            }
            else if (educationType == EducationType.GraduateEducation)
            {
                // Assuming controls for graduate education on your page
                educationDetails.InstituteName = GetValueFromTextBox(instNameG);
                educationDetails.Board = boardG.SelectedValue;
                if (cgpaG.Checked)
                {
                    educationDetails.Marks = "CGPA";
                }
                else if (percentageG.Checked)
                {
                    educationDetails.Marks = "Percentage";
                }
                string gradeGTextBox = GetValueFromTextBox(gradeGValue);
                if (decimal.TryParse(gradeGTextBox, out decimal aggregate))
                {
                    educationDetails.Aggregate = aggregate;
                }
                string yopGValue = GetValueFromTextBox(yopG);
                if (!string.IsNullOrEmpty(yopGValue))
                {
                    educationDetails.YearOfCompletion = int.Parse(yopGValue);
                }
            }

            return educationDetails;
        }      
                
        protected void PopulateStudentTableDetails(int studentID)
        {
            UserDetailsViewModel studentDetailsTable = UserBusiness.GetUserDetails(studentID);
            firstName.Text = studentDetailsTable.FirstName;
            middleName.Text = studentDetailsTable.MiddleName;
            lastName.Text = studentDetailsTable.LastName;
            email.Text = studentDetailsTable.Email;
            birthday.Text = studentDetailsTable.DateOfBirth;
            phone.Text = studentDetailsTable.Phone;
            aadhar.Text = studentDetailsTable.AadharNumber;
            password.Text = studentDetailsTable.Password;


            string gender = studentDetailsTable.Gender;
            if (gender == "Male")
            {
                male.Checked = true;
            }
            else if (gender == "Female")
            {
                female.Checked = true;
            }
            string hobbies = studentDetailsTable.Hobbies;
            string[] hobbyArray = hobbies.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string hobby in hobbyArray)
            {
                switch (hobby)
                {
                    case "Dancing":
                        checkbox1.Checked = true;
                        break;
                    case "Singing":
                        checkbox2.Checked = true;
                        break;
                    case "Coding":
                        checkbox3.Checked = true;
                        break;
                    case "Web Designing":
                        checkbox4.Checked = true;
                        break;
                    case "Board Games":
                        checkbox5.Checked = true;
                        break;
                    case "Camping":
                        checkbox6.Checked = true;
                        break;
                    case "Running":
                        checkbox7.Checked = true;
                        break;
                    case "Sleeping":
                        checkbox8.Checked = true;
                        break;
                    case "Reading":
                        checkbox9.Checked = true;
                        break;

                }
            }
            
            
            string uploadedFileName = studentDetailsTable.OriginalDocumentName;
            if (!string.IsNullOrEmpty(uploadedFileName))
            {
                aadharCardUploadLabel.Text = uploadedFileName;
            }

        }

        protected void PopulatedAddressDetails(int studentID)
        {
            AddressDetailViewModel currentAddress = UserBusiness.GetCurrentAddress(studentID);
            if (currentAddress != null && currentAddress.CountryID.HasValue)
            {
                c1Address.Text = currentAddress.AddressLine1;
                c2Address.Text = currentAddress.AddressLine2;
                cPinCode.Text = currentAddress.Pincode;

                int countryID = currentAddress.CountryID.Value; // Access the value of CountryID
                string currentCountry = UserBusiness.GetCountryName(countryID);
                PopulateDropdownCountry(currentCountry, cCountry);

                string currentState = UserBusiness.GetStateName((int)currentAddress.StateID);
                PopulateDropdownState(currentState, cState, (int)currentAddress.CountryID);
            }
            else
            {
                c1Address.Text = "";
                c2Address.Text = "";
                cPinCode.Text = "";
                PopulateCountries(cCountry);
                
            }

            AddressDetailViewModel permanentAddress = UserBusiness.GetPermanentAddress(studentID);
            if (permanentAddress != null && permanentAddress.CountryID.HasValue)
            {
                p1Address.Text = permanentAddress.AddressLine1;
                p2Address.Text = permanentAddress.AddressLine2;
                pPinCode.Text = permanentAddress.Pincode;

                int countryID = permanentAddress.CountryID.Value; // Access the value of CountryID
                string permanentCountry = UserBusiness.GetCountryName(countryID);
                PopulateDropdownCountry(permanentCountry, pCountry);

                string permanentState = UserBusiness.GetStateName((int)permanentAddress.StateID);
                PopulateDropdownState(permanentState, pState, (int)permanentAddress.CountryID);
            }
            else
            {
                p1Address.Text = "";
                p2Address.Text = "";
                pPinCode.Text = "";
                PopulateCountries(pCountry);
            }
        }

        private void PopulateDropdownState(string selectedItem, DropDownList dropdown, int countryID)
        {
            dropdown.Items.Clear();
            List<StateViewModel> states = UserBusiness.GetStates(countryID);

            foreach (StateViewModel state in states)
            {
                ListItem item = new ListItem(state.StateName, state.StateID.ToString());
                dropdown.Items.Add(item);

                // Check if the state name matches the selectedItem
                if (state.StateName == selectedItem)
                {
                    dropdown.SelectedValue = state.StateID.ToString();
                }
            }
        }

        private void PopulateDropdownCountry(string selectedItem, DropDownList dropdown)
        {
            dropdown.Items.Clear();
            List<CountryViewModel> countries = UserBusiness.GetCountries();
            foreach (CountryViewModel country in countries)
            {
                dropdown.Items.Add(new ListItem(country.CountryName, country.CountryID.ToString()));
                if(country.CountryName == selectedItem)
                {
                    dropdown.SelectedValue = country.CountryID.ToString();
                   
                }
            }
        }

        protected void PopulatedEducationDetails(int studentID)
        {
            EducationDetailViewModel education10 = UserBusiness.GetEducation10(studentID);
            instName10.Text = education10.InstituteName;
            board10.Text = education10.Board;
            if (education10.Marks == "CGPA")
            {
                cgpa10.Checked = true;
            }
            else if (education10.Marks == "Percentage")
            {
                percentage.Checked = true;
            }
            grade10Value.Text = education10.Aggregate.ToString();
            
            yop10.Text = education10.YearOfCompletion.ToString(); 
            EducationDetailViewModel education12 = UserBusiness.GetEducation12(studentID);
            instName12.Text = education12.InstituteName;
            board12.Text = education12.Board;
            if (education12.Marks == "CGPA")
            {
                cgpa12.Checked = true;
            }
            else if (education12.Marks == "Percentage")
            {
                percentage12.Checked = true;
            }
            grade12Value.Text = education12.Aggregate.ToString();
            yop12.Text = education12.YearOfCompletion.ToString(); 

            EducationDetailViewModel educationGraduate = UserBusiness.GetEducationGraduate(studentID);
            instNameG.Text = educationGraduate.InstituteName;
            boardG.Text = educationGraduate.Board;
            if (educationGraduate.Marks == "CGPA")
            {
                cgpaG.Checked = true;
            }
            else if (educationGraduate.Marks == "Percentage")
            {
                percentageG.Checked = true;
            }
            gradeGValue.Text = educationGraduate.Aggregate.ToString();  
            yopG.Text = educationGraduate.YearOfCompletion.ToString(); 

        }

        public string HobbySelected()
        {
            StringBuilder selectedHobbies = new StringBuilder();

            if (checkbox1.Checked)
            {
                selectedHobbies.Append("Dancing, ");
            }
            if (checkbox2.Checked)
            {
                selectedHobbies.Append("Singing, ");
            }
            if (checkbox3.Checked)
            {
                selectedHobbies.Append("Coding, ");
            }
            if (checkbox4.Checked)
            {
                selectedHobbies.Append("Web Designing, ");
            }
            if (checkbox5.Checked)
            {
                selectedHobbies.Append("Board Games, ");
            }
            if (checkbox6.Checked)
            {
                selectedHobbies.Append("Camping, ");
            }
            if (checkbox7.Checked)
            {
                selectedHobbies.Append("Running, ");
            }
            if (checkbox8.Checked)
            {
                selectedHobbies.Append("Sleeping, ");
            }
            if (checkbox9.Checked)
            {
                selectedHobbies.Append("Reading, ");
            }

            return selectedHobbies.ToString();
        }

        protected string GetValueFromTextBox(TextBox textBox)
        {
            return textBox?.Text?.Trim() ?? string.Empty;
        }

        protected DateTime? GetDateTimeValueFromTextBox(TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox?.Text))
            {
                if (DateTime.TryParse(textBox.Text, out DateTime parsedDate))
                {
                    return parsedDate;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        private int? GetSelectedCountryID(DropDownList countryDropDown)
        {
            if (countryDropDown.SelectedItem != null)
            {
                int countryID;
                if (int.TryParse(countryDropDown.SelectedValue, out countryID))
                {
                    return countryID;
                }
            }
            return null;
        }

        private int? GetSelectedStateID(DropDownList stateDropDown)
        {
            if (stateDropDown.SelectedItem != null)
            {
                int stateID;
                if (int.TryParse(stateDropDown.SelectedValue, out stateID))
                {
                    return stateID;
                }
            }
            return null;
        }

        protected void PopulateCountries(DropDownList countryDropDown)
        {
            List<CountryViewModel> countries = UserBusiness.GetCountries();

            foreach (CountryViewModel country in countries)
            {
                ListItem item = new ListItem(country.CountryName, country.CountryID.ToString());
                countryDropDown.Items.Add(item);
            }
        }

        protected void PopulateStates(int countryID, DropDownList stateDropDown)
        {
            stateDropDown.Items.Clear();
            List<StateViewModel> states = UserBusiness.GetStates(countryID);

            foreach (StateViewModel state in states)
            {
                ListItem item = new ListItem(state.StateName, state.StateID.ToString());
                stateDropDown.Items.Add(item);
            }
        }

        protected void cCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateStates(Convert.ToInt32(cCountry.SelectedValue), cState);
        }

        protected void pCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateStates(Convert.ToInt32(pCountry.SelectedValue), pState);
        }

        protected void CopyAddress(object sender, EventArgs e)
        {
            int userID = UserBusiness.GetLastInsertedUserID();

            if (sameAsCurrent.Checked)
            {
                ViewState["PrevPCountry"] = pCountry.SelectedValue;
                ViewState["PrevPState"] = pState.SelectedValue;
                ViewState["PrevP1Address"] = p1Address.Text;
                ViewState["PrevP2Address"] = p2Address.Text;
                ViewState["PrevPPinCode"] = pPinCode.Text;

                pCountry.SelectedValue = cCountry.SelectedValue ?? "Choose Country";
                PopulateStates(Convert.ToInt32(cCountry.SelectedValue ?? "Choose Sate"), pState);


                pState.SelectedValue = cState.SelectedValue;
                p1Address.Text = c1Address.Text;
                p2Address.Text = c2Address.Text;
                pPinCode.Text = cPinCode.Text;
            }
            else
            {
                pCountry.SelectedValue = (string)ViewState["PrevPCountry"];
                string prevPState = (string)ViewState["PrevPState"];

                if (pState.Items.FindByValue(prevPState) != null)
                {
                    pState.SelectedValue = prevPState;
                }
                else
                {
                    if (pState.Items.Count > 0)
                    {
                        pState.SelectedValue = pState.Items[0].Value;
                    }
                }

                p1Address.Text = (string)ViewState["PrevP1Address"];
                p2Address.Text = (string)ViewState["PrevP2Address"];
                pPinCode.Text = (string)ViewState["PrevPPinCode"];
            }
        }

        private void CheckAuthorizationAndLoadUserDetails()
        {
            LogInSessionModel userSessionInfo = ConstantValues.GetUserSessionInfo();
            if (!authorizationChecked)
            {
                if (userSessionInfo != null)
                {
                    int authenticatedUserID = userSessionInfo.UserID;
                    bool isAdmin = userSessionInfo.IsAdmin;

                    bool urlParsedSuccessfully = int.TryParse(Request.QueryString["StudentID"], out int urlUpdatedStudentID);

                    if (isAdmin || (urlParsedSuccessfully && urlUpdatedStudentID == authenticatedUserID))
                    {
                        // Allow admin or the authenticated user to access the requested data
                        // No redirection necessary here
                    }
                    else
                    {
                        Response.Redirect($"UserDetails.aspx?StudentID={authenticatedUserID}");
                    }
                }
                else
                {
                    Response.Redirect("UserDetails.aspx");
                }
                authorizationChecked = true;
            }
        }







    }
}