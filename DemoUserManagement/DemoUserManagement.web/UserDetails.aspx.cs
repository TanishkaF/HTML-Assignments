using DemoUserManagement.BusinessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement.web
{
    public partial class UserDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            resetButton.Click += ResetButton_Click;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["StudentID"]))
                {
                    int studentID;
                    if (int.TryParse(Request.QueryString["StudentID"], out studentID))
                    {
                        PopulateStudentTableDetails(studentID);
                      
                        PopulatedAddressDetails(studentID);
                        PopulatedEducationDetails(studentID);
                    }
                }
                PopulateCountries(cCountry);
                PopulateCountries(pCountry);
            }
        }

        protected void SubmitClick(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["StudentID"]))
            {
                int userID = UserBusiness.GetLastInsertedUserID();
                
                StudentDetailsTableViewModel studentDetailsTable = GetStudentTableDetails();
                UserBusiness.UpdateStudentDetailsTable(userID, studentDetailsTable);

                BtnUpload_Click(sender, e, studentDetailsTable);

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
            }
            else
            {
                StudentDetailsTableViewModel studentDetails = GetStudentTableDetails();
                BtnUpload_Click(sender, e, studentDetails);
                UserBusiness.InsertStudentTableDetails(studentDetails);

                int userID = UserBusiness.GetLastInsertedUserID();


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
                    AddressDetailViewModel addressDetailsPermanent = GetAddressDetails(userID, 2);
                    UserBusiness.InsertAddressDetails(addressDetailsPermanent);
                }


                EducationDetailViewModel educationDetailViewModel10 = GetEducationDetails(userID, EducationType.MatriculationEducation);
                UserBusiness.InsertEducationDetails(educationDetailViewModel10);
                EducationDetailViewModel educationDetailViewModel12 = GetEducationDetails(userID, EducationType.IntermediateEducation);
                UserBusiness.InsertEducationDetails(educationDetailViewModel12);
                EducationDetailViewModel educationDetailViewModelGraduate = GetEducationDetails(userID, EducationType.GraduateEducation);
                UserBusiness.InsertEducationDetails(educationDetailViewModelGraduate);                

            }
                UpdateUserDetails();
        }

        protected void BtnUpload_Click(object sender, EventArgs e, StudentDetailsTableViewModel studentDetailsTable)
        {
            if (fileUpload.HasFile)
            {
                try
                {
                    string uploadFolderPath = ConfigurationManager.AppSettings["UploadFolderPath"];

                    string fileName = Path.GetFileName(fileUpload.FileName);
                    string fileExtension = Path.GetExtension(fileName);

                    if (fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        string physicalUploadFolderPath = uploadFolderPath;

                        string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                        string filePath = Path.Combine(physicalUploadFolderPath, uniqueFileName);

                        fileUpload.SaveAs(filePath);

                        DocumentViewModel document = new DocumentViewModel
                        {
                            StudentID = UserBusiness.GetLastInsertedUserID(),
                            DiskDocumentName = uniqueFileName,
                            OriginalDocumentName = fileName
                        };
                       // UserBusiness.InsertDocument(document);

                        studentDetailsTable.DiskDocumentName = uniqueFileName;
                        studentDetailsTable.OriginalDocumentName = fileName;
                    }
                }
                catch (Exception ex)
                {
                    Logger.AddData(ex);
                }
            }
            else
            {
                // TODO: Handle case when no file is selected
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
                // Add other control types as needed (e.g., CheckBox, RadioButton, etc.)
            }
        }       

        protected void UpdateUserDetails()
        {
            Response.Redirect($"UserList.aspx");
        }
          
        private StudentDetailsTableViewModel GetStudentTableDetails()
        {
            StudentDetailsTableViewModel studentDetailsTable = new StudentDetailsTableViewModel();
            studentDetailsTable.FirstName = GetValueFromTextBox(firstName);
            studentDetailsTable.MiddleName = GetValueFromTextBox(middleName);
            studentDetailsTable.LastName = GetValueFromTextBox(lastName);
            studentDetailsTable.Email = GetValueFromTextBox(email);
            studentDetailsTable.Phone = GetValueFromTextBox(phone);
            studentDetailsTable.AadharNumber = GetValueFromTextBox(aadhar);
            studentDetailsTable.DateOfBirth = (DateTime)GetDateTimeValueFromTextBox(birthday);
            studentDetailsTable.Gender = male.Checked ? "Male" : "Female";
            studentDetailsTable.Hobbies = HobbySelected();
            studentDetailsTable.DiskDocumentName = "";
            studentDetailsTable.OriginalDocumentName = "";

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

            // Assuming you have controls for education details on your page
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
            StudentDetailsTableViewModel studentDetailsTable = UserBusiness.GetStudentDetailsTable(studentID);
            // Populate student details
            firstName.Text = studentDetailsTable.FirstName;
            middleName.Text = studentDetailsTable.MiddleName;
            lastName.Text = studentDetailsTable.LastName;
            email.Text = studentDetailsTable.Email;
            birthday.Text = studentDetailsTable.DateOfBirth.ToString("yyyy-MM-dd");
            phone.Text = studentDetailsTable.Phone;
            aadhar.Text = studentDetailsTable.AadharNumber;
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
        }

        protected void PopulatedAddressDetails(int studentID)
        {
            // Retrieve current address details
            AddressDetailViewModel currentAddress = UserBusiness.GetCurrentAddress(studentID);
            c1Address.Text = currentAddress.AddressLine1;
            c2Address.Text = currentAddress.AddressLine2;
            cPinCode.Text = currentAddress.Pincode;
            cState.Text = GetStateName(currentAddress.StateID); // Assuming you have a method to get the state name
            cCountry.Text = GetCountryName(currentAddress.CountryID); // Assuming you have a method to get the country name

            // Retrieve permanent address details
            AddressDetailViewModel permanentAddress = UserBusiness.GetPermanentAddress(studentID);
            if (permanentAddress != null)
            {
                p1Address.Text = permanentAddress.AddressLine1;
                p2Address.Text = permanentAddress.AddressLine2;
                pPinCode.Text = permanentAddress.Pincode;
                pState.Text = GetStateName(permanentAddress.StateID); // Assuming you have a method to get the state name
                pCountry.Text = GetCountryName(permanentAddress.CountryID); // Assuming you have a method to get the country name
            }
            else
            {
                // Handle the case where permanent address is not found
                // For example, you can clear the fields or display a message
                p1Address.Text = "";
                p2Address.Text = "";
                pPinCode.Text = "";
                pState.Text = "";
                pCountry.Text = "";
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

        private string GetStateName(int? stateID)
        {
            if (stateID.HasValue)
            {
                string stateName = "BBSR";
                return stateName ?? "";
            }
            else
            {
                return "";
            }
        }

        private string GetCountryName(int? countryID)
        {
            if (countryID.HasValue)
            {
                string countryName = "ANGUL";
                return countryName ?? "";
            }
            else
            {
                return "";
            }
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

    }
}
