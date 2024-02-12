using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Text;
using System.Web.UI;
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
                UpdateUserDetails(userID);
            }
            else
            {
                StudentDetailViewModel studentDetails = GetStudentDetails();
                UserBusiness.InsertStudentDetails(studentDetails);

                int userID = UserBusiness.GetLastInsertedUserID();

                AddressDetailViewModel addressDetails1 = GetAddressDetails(userID, 1);
                addressDetails1.AddressType = 1;
                UserBusiness.InsertAddressDetails(addressDetails1);

                // Insert second address if not same as current
                if (sameAsCurrent.Checked)
                {
                   // CopyAddress(sender,e);
                    AddressDetailViewModel addressDetails2 = GetAddressDetails(userID, 2);
                    addressDetails2.UserID = userID;
                    addressDetails2.AddressType = 2;
                    // Insert the second address
                    UserBusiness.InsertAddressDetails(addressDetails2);
                }
                else
                {
                    AddressDetailViewModel addressDetailsPermanent = GetAddressDetails(userID, 2);
                    UserBusiness.InsertAddressDetails(addressDetailsPermanent);

                }


                EducationDetailViewModel educationDetailViewModel10 = GetEducationDetails(userID,1);
                UserBusiness.InsertEducationDetails(educationDetailViewModel10);
                EducationDetailViewModel educationDetailViewModel12 = GetEducationDetails(userID,2);
                UserBusiness.InsertEducationDetails(educationDetailViewModel12);
                EducationDetailViewModel educationDetailViewModelGraduate = GetEducationDetails(userID,3);
                UserBusiness.InsertEducationDetails(educationDetailViewModelGraduate);

                string hobbiesString = HobbySelected();
                string messageText = message.Text;
                string feedbackText = feedback.Text;

                UserBusiness.InsertHobbyDetails(userID,hobbiesString, messageText, feedbackText);
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
                // Add other control types as needed (e.g., CheckBox, RadioButton, etc.)
            }
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

        protected void UpdateUserDetails(int userId)
        {
            // Add userId along with other parameters to the URL
            Response.Redirect($"UserList.aspx");
        }

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
                        PopulateStudentDetails(studentID);
                        PopulateCountries(cCountry);
                        PopulateCountries(pCountry);
                        PopulatedAddressDetails(studentID);
                        PopulatedEducationDetails(studentID);
                       // PopulateHobbyDetails(studentID);
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

        //protected void PopulateHobbyDetails(int studentID)
        //{
        //   HobbyDetailViewModel hobbyDetails = UserBusiness.GetHobbyDetails(studentID);        

        //    string hobbies = hobbyDetails.Hobbies;
        //    string[] hobbyArray = hobbies.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

        //    foreach (string hobby in hobbyArray)
        //    {
        //        switch (hobby)
        //        {
        //            case "Dancing":
        //                checkbox1.Checked = true;
        //                break;
        //            case "Singing":
        //                checkbox2.Checked = true;
        //                break;
        //            case "Coding":
        //                checkbox3.Checked = true;
        //                break;
        //                // Add cases for all hobbies...
        //        }
        //    }

        //    // Populate message and feedback
        //    message.Text = hobbyDetails.Message;
        //    feedback.Text = hobbyDetails.Feedback;
        //}

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

    }
}
