using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


namespace DemoUserManagement.web
{
    public partial class UserDetails : System.Web.UI.Page
    {
        protected void SubmitClick(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["StudentID"]))
            {
                int userID = UserBusiness.GetLastInsertedUserID();
                StudentDetailViewModel studentDetails = GetStudentDetails();
                UserBusiness.UpdateStudentDetails(userID, studentDetails);
                AddressDetailViewModel addressDetailsCurrent = GetAddressDetails(userID,1);
                UserBusiness.UpdateAddressDetails(userID,1,addressDetailsCurrent);
                AddressDetailViewModel addressDetailsPermanent = GetAddressDetails(userID,2);
                UserBusiness.UpdateAddressDetails(userID,2, addressDetailsPermanent);
                EducationDetailViewModel educationDetails10 = GetEducationDetails(userID,1);
                UserBusiness.UpdateEducationDetails(userID,1, educationDetails10); 
                EducationDetailViewModel educationDetails12 = GetEducationDetails(userID,2);
                UserBusiness.UpdateEducationDetails(userID,2, educationDetails12);  
                EducationDetailViewModel educationDetailsGraduate = GetEducationDetails(userID,3);
                UserBusiness.UpdateEducationDetails(userID,3, educationDetailsGraduate);
            }
            else
            {
                StudentDetailViewModel studentDetails = GetStudentDetails();
                UserBusiness.InsertStudentDetails(studentDetails);
            
                int userID = UserBusiness.GetLastInsertedUserID();                           
               
                AddressDetailViewModel addressDetails1 = GetAddressDetails(userID, 1);
                UserBusiness.InsertAddressDetails(addressDetails1);

                if (!sameAsCurrent.Checked)
                {
                    AddressDetailViewModel addressDetails2 = GetAddressDetails(userID, 2);
                    UserBusiness.InsertAddressDetails(addressDetails2);
                }


                EducationDetailViewModel educationDetailViewModel10 = GetEducationDetails(userID,1);
                UserBusiness.InsertEducationDetails(educationDetailViewModel10);
                EducationDetailViewModel educationDetailViewModel12 = GetEducationDetails(userID,2);
                UserBusiness.InsertEducationDetails(educationDetailViewModel12);
                EducationDetailViewModel educationDetailViewModelGraduate = GetEducationDetails(userID,3);
                UserBusiness.InsertEducationDetails(educationDetailViewModelGraduate);

            }

            UpdateUserDetails();
        }

        protected void CopyAddress(object sender, EventArgs e)
        {
            int userID = UserBusiness.GetLastInsertedUserID();
            AddressDetailViewModel currentAddress = new AddressDetailViewModel();
            AddressDetailViewModel permanentAddress = new AddressDetailViewModel();
            

            // Assuming currentAddress and permanentAddress are defined elsewhere in your code.
            // Make sure to replace 'currentAddress' and 'permanentAddress' with the actual instances.

            if (sameAsCurrent.Checked)
            {
                // Set the UserID for the current address
                currentAddress.UserID = userID;

                // Insert current address
                UserBusiness.InsertAddressDetails(currentAddress);

                ViewState["PrevPCountry"] = pCountry.SelectedValue;
                ViewState["PrevPState"] = pState.SelectedValue;
                ViewState["PrevP1Address"] = p1Address.Text;
                ViewState["PrevP2Address"] = p2Address.Text;
                ViewState["PrevPPinCode"] = pPinCode.Text;

                pCountry.SelectedValue = cCountry.SelectedValue;
                PopulateStates(Convert.ToInt32(cCountry.SelectedValue), pState);

                pState.SelectedValue = cState.SelectedValue;
                p1Address.Text = c1Address.Text;
                p2Address.Text = c2Address.Text;
                pPinCode.Text = cPinCode.Text;
            }
            else
            {
                // Set the UserID for the permanent address
                permanentAddress.UserID = userID;

                // Insert permanent address
                UserBusiness.InsertAddressDetails(permanentAddress);

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

        protected void UpdateUserDetails()
        {
            Response.Redirect("UserList.aspx?Refresh=1");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["StudentID"]))
                {
                    int studentID;
                    if (int.TryParse(Request.QueryString["StudentID"], out studentID))
                    {
                        PopulateStudentDetails(studentID);
                        PopulatedAddressDetails(studentID);
                        PopulatedEducationDetails(studentID);
                    }
                }
                PopulateCountries(cCountry);
                PopulateCountries(pCountry);
            }
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



        private StudentDetailViewModel GetStudentDetails()
        {
            StudentDetailViewModel studentDetails = new StudentDetailViewModel();

            studentDetails.FirstName = GetValueFromTextBox(firstName);
            studentDetails.MiddleName = GetValueFromTextBox(middleName);
            studentDetails.LastName = GetValueFromTextBox(lastName);
            studentDetails.Email = GetValueFromTextBox(email);
            studentDetails.Phone = GetValueFromTextBox(phone);
            studentDetails.AadharNumber = GetValueFromTextBox(aadhar);
            studentDetails.DateOfBirth = GetDateTimeValueFromTextBox(birthday);
            studentDetails.Gender = male.Checked ? "Male" : "Female";

            return studentDetails;
        }

        private AddressDetailViewModel GetAddressDetails(int userID, int addressType)
        {
            AddressDetailViewModel addressDetails = new AddressDetailViewModel();

            // Assuming you have controls for address details on your page
            addressDetails.UserID = userID;
            addressDetails.AddressType = addressType;

            if (addressType == 1)
            {
                addressDetails.Country = cCountry.SelectedItem.Text; // Getting the selected country
                addressDetails.State = cState.SelectedItem.Text; // Getting the selected state
                addressDetails.AddressLine1 = GetValueFromTextBox(c1Address);
                addressDetails.AddressLine2 = GetValueFromTextBox(c2Address);
                addressDetails.Pincode = GetValueFromTextBox(cPinCode);
            }
            else if (addressType == 2)
            {
                // Assuming controls for permanent address on your page
                addressDetails.Country = pCountry.SelectedItem.Text; // Getting the selected country
                addressDetails.State = pState.SelectedItem.Text; // Getting the selected state
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

            if (educationType == 1)
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
            else if (educationType == 2)
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
            else if (educationType == 3)
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

        protected void PopulateStudentDetails(int studentID)
        {
            StudentDetailViewModel studentDetails = UserBusiness.GetStudentDetails(studentID);

            // Populate student details
            firstName.Text = studentDetails.FirstName;
            middleName.Text = studentDetails.MiddleName;
            lastName.Text = studentDetails.LastName;
            email.Text = studentDetails.Email;
            birthday.Text = studentDetails.DateOfBirth?.ToString("yyyy-MM-dd");
            phone.Text = studentDetails.Phone;
            aadhar.Text = studentDetails.AadharNumber;

            string gender = studentDetails.Gender;
            if (gender == "Male")
            {
                male.Checked = true;
            }
            else if (gender == "Female")
            {
                female.Checked = true;
            }

          

            //// Populate education details
            //EducationDetailViewModel education10 = EducationManager.GetEducationDetails(studentID, 1);
            //instName10.Text = education10.InstituteName;
            //board10.Text = education10.Board;
            //// Populate other fields similarly for education 10th, 12th, and Graduate
        }

        protected void PopulatedAddressDetails(int studentID)
        {
            // Retrieve current address details
            AddressDetailViewModel currentAddress = UserBusiness.GetCurrentAddress(studentID);
            c1Address.Text = currentAddress.AddressLine1;
            c2Address.Text = currentAddress.AddressLine2;
            cPinCode.Text = currentAddress.Pincode;
            cState.Text = currentAddress.State;
            cCountry.Text = currentAddress.Country;

            // Retrieve permanent address details
            AddressDetailViewModel permanentAddress = UserBusiness.GetPermanentAddress(studentID);
            if (permanentAddress != null)
            {
                p1Address.Text = permanentAddress.AddressLine1;
                p2Address.Text = permanentAddress.AddressLine2;
                pPinCode.Text = permanentAddress.Pincode;
                pState.Text = permanentAddress.State;
                pCountry.Text = permanentAddress.Country;
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
            // Populate education details for 10th standard
            EducationDetailViewModel education10 = UserBusiness.GetEducation10(studentID);
            instName10.Text = education10.InstituteName;
            board10.Text = education10.Board;
            // Assuming cgpa10 is the checkbox for CGPA and percentage10 is the checkbox for percentage
            if (education10.Marks == "cgpa10")
            {
                cgpa10.Checked = true;
            }
            else if (education10.Marks == "percentage")
            {
                percentage.Checked = true;
            }
            grade10Value.Text = education10.Aggregate.ToString(); // Assuming this is a TextBox for displaying the grade
            yop10.Text = education10.YearOfCompletion.ToString(); // Assuming this is a TextBox for displaying the year of completion

            // Populate education details for 12th standard
            EducationDetailViewModel education12 = UserBusiness.GetEducation12(studentID);
            instName12.Text = education12.InstituteName;
            board12.Text = education12.Board;
            // Assuming cgpa12 is the checkbox for CGPA and percentage12 is the checkbox for percentage
            if (education12.Marks == "cgpa12")
            {
                cgpa12.Checked = true;
            }
            else if (education12.Marks == "percentage")
            {
                percentage12.Checked = true;
            }
            grade12Value.Text = education12.Aggregate.ToString(); // Assuming this is a TextBox for displaying the grade
            yop12.Text = education12.YearOfCompletion.ToString(); // Assuming this is a TextBox for displaying the year of completion

            // Populate education details for graduation
            EducationDetailViewModel educationGraduate = UserBusiness.GetEducationGraduate(studentID);
            instNameG.Text = educationGraduate.InstituteName;
            boardG.Text = educationGraduate.Board;
            // Assuming cgpaG is the checkbox for CGPA and percentageG is the checkbox for percentage
            if (educationGraduate.Marks == "cgpa")
            {
                cgpaG.Checked = true;
            }
            else if (educationGraduate.Marks == "percentage")
            {
                percentageG.Checked = true;
            }
            gradeGValue.Text = educationGraduate.Aggregate.ToString(); // Assuming this is a TextBox for displaying the grade
            yopG.Text = educationGraduate.YearOfCompletion.ToString(); // Assuming this is a TextBox for displaying the year of completion

        }

    }
}
