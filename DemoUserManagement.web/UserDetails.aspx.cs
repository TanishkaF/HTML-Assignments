using System;
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
                int userID = GetUserID();
                UpdateUserDetails(userID);
                UpdateAddressDetails(userID);
                UpdateEducationDetails(userID);
            }
            else
            {
                ValidateAndStore(sender,e);
                int userID = GetUserID(); 
                InsertAddressTable(userID, 1, cCountry.SelectedValue, cState.SelectedValue, c1Address.Text, c2Address.Text, cPinCode.Text);
                if (!sameAsCurrent.Checked)
                {
                    InsertAddressTable(userID, 2, pCountry.SelectedValue, pState.SelectedValue, p1Address.Text, p2Address.Text, pPinCode.Text);
                }
                InsertEducationDetailsForType(userID, 1, instName10, board10, cgpa10, grade10Value, yop10); // Insert 10th education details
                InsertEducationDetailsForType(userID, 2, instName12, board12, cgpa12, grade12Value, yop12); // Insert 12th education details
                InsertEducationDetailsForType(userID, 3, instNameG, boardG, cgpaG, gradeGValue, yopG); // Insert Graduate education details
            }
            
            UpdateUserDetails();
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
                    }
                }
                PopulateCountries(cCountry);
                PopulateCountries(pCountry);
            }
        }  

        protected void ValidateAndStore(object sender, EventArgs e)
        {
            try
            {
             
              

                string firstNameValue = GetValueFromTextBox(firstName);
                string middleNameValue = GetValueFromTextBox(middleName);
                string lastNameValue = GetValueFromTextBox(lastName);
                string emailValue = GetValueFromTextBox(email);
                string phoneValue = GetValueFromTextBox(phone);
                string aadharValue = GetValueFromTextBox(aadhar);
                DateTime? dateOfBirthValue = GetDateTimeValueFromTextBox(birthday);
                string genderValue = male.Checked ? "Male" : "Female";

                InsertStudentDetails(firstNameValue, middleNameValue, lastNameValue, emailValue, dateOfBirthValue, genderValue, phoneValue, aadharValue);

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Insertion Operation Was Successful');", true);
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
            }
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

        protected void PopulateCountries(DropDownList countryDropDown)
        {
            string query = "SELECT CountryID, CountryName FROM Countries";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListItem item = new ListItem(reader["CountryName"].ToString(), reader["CountryID"].ToString());
                    countryDropDown.Items.Add(item);
                }
                reader.Close();
            }
        }

        protected void PopulateStates(int countryID, DropDownList stateDropDown)
        {
            stateDropDown.Items.Clear();
                                        

            string query = "SELECT StateID, StateName FROM States WHERE CountryID = @CountryID";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CountryID", countryID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListItem item = new ListItem(reader["StateName"].ToString(), reader["StateID"].ToString());
                    stateDropDown.Items.Add(item);
                }
                reader.Close();
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

        protected int GetUserID()
        {
            int userID = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = "SELECT IDENT_CURRENT('StudentDetails') AS LastUserID";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    userID = Convert.ToInt32(reader["LastUserID"]);
                }
                reader.Close();
            }

            return userID;
        }

        protected void CopyAddress(object sender, EventArgs e)
        {
            int userID = GetUserID();

            if (sameAsCurrent.Checked)
            {
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

        protected void InsertStudentDetails(string firstName, string middleName, string lastName, string email, DateTime? dateOfBirth, string gender, string phone, string aadhar)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = @"INSERT INTO StudentDetails (FirstName, MiddleName, LastName, Email, DateOfBirth, Gender, Phone, AadharNumber) 
                   VALUES (@FirstName, @MiddleName, @LastName, @Email, @DateOfBirth, @Gender, @Phone, @AadharNumber)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@MiddleName", middleName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth.HasValue ? (object)dateOfBirth.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@AadharNumber", aadhar);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        protected void InsertAddressTable(int userID, int addressType, string country, string state, string addressLine1, string addressLine2, string pincode)
        {
            string query = @"INSERT INTO AddressDetails (UserID, AddressType, Country, State, AddressLine1, AddressLine2, Pincode) 
                             VALUES (@UserID, @AddressType, @Country, @State, @AddressLine1, @AddressLine2, @Pincode)";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@AddressType", addressType);
                command.Parameters.AddWithValue("@Country", string.IsNullOrEmpty(country) ? DBNull.Value : (object)country);
                command.Parameters.AddWithValue("@State", string.IsNullOrEmpty(state) ? DBNull.Value : (object)state);
                command.Parameters.AddWithValue("@AddressLine1", string.IsNullOrEmpty(addressLine1) ? DBNull.Value : (object)addressLine1);
                command.Parameters.AddWithValue("@AddressLine2", string.IsNullOrEmpty(addressLine2) ? DBNull.Value : (object)addressLine2);
                command.Parameters.AddWithValue("@Pincode", string.IsNullOrEmpty(pincode) ? DBNull.Value : (object)pincode);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        protected void InsertEducationDetailsForType(int userID, int educationType, TextBox instNameTextBox, DropDownList boardDropDown, RadioButton cgpaRadioButton, TextBox gradeTextBox, TextBox yopTextBox)
        {
            string instituteName = instNameTextBox.Text;
            string board = boardDropDown.SelectedValue;
            string marks = cgpaRadioButton.Checked ? "CGPA" : "Percentage";
            decimal aggregate = 0;
            int yearOfCompletion = 0;

            if (int.TryParse(yopTextBox.Text, out int year))
            {
                yearOfCompletion = year;
            }

            if (decimal.TryParse(gradeTextBox.Text, out decimal aggregateValue))
            {
                aggregate = aggregateValue;
            }
            InsertEducationTable(userID, educationType, instituteName, board, marks, aggregate, yearOfCompletion);
        }

        protected void InsertEducationTable(int userID, int educationType, string instituteName, string board, string marks, decimal aggregate, int yearOfCompletion)
        {
            string query = @"INSERT INTO EducationDetails (StudentID, EducationType, InstituteName, Board, Marks, Aggregate, YearOfCompletion) 
                     VALUES (@UserID, @EducationType, @InstituteName, @Board, @Marks, @Aggregate, @YearOfCompletion)";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@EducationType", educationType);
                command.Parameters.AddWithValue("@InstituteName", string.IsNullOrEmpty(instituteName) ? DBNull.Value : (object)instituteName);
                command.Parameters.AddWithValue("@Board", string.IsNullOrEmpty(board) ? DBNull.Value : (object)board);
                command.Parameters.AddWithValue("@Marks", string.IsNullOrEmpty(marks) ? DBNull.Value : (object)marks);
                command.Parameters.AddWithValue("@Aggregate", aggregate);
                command.Parameters.AddWithValue("@YearOfCompletion", yearOfCompletion);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        //ALL THREE POPULATED
        protected void PopulateStudentDetails(int studentID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string studentQuery = "SELECT * FROM StudentDetails WHERE StudentID = @StudentID";
                SqlCommand command = new SqlCommand(studentQuery, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    firstName.Text = reader["FirstName"].ToString();
                    middleName.Text = reader["MiddleName"].ToString();
                    lastName.Text = reader["LastName"].ToString();
                    email.Text = reader["Email"].ToString();
                    birthday.Text = reader["DateOfBirth"].ToString();
                    phone.Text = reader["Phone"].ToString();
                    aadhar.Text = reader["AadharNumber"].ToString();

                    string gender = reader["Gender"].ToString();
                    if (gender == "Male")
                    {
                        male.Checked = true;
                    }
                    else if (gender == "Female")
                    {
                        female.Checked = true;
                    }
                }
                reader.Close();
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                connection.Open();

                string currentAddressQuery = "SELECT * FROM AddressDetails WHERE UserID = @StudentID AND AddressType = 1";
                SqlCommand currentAddressCommand = new SqlCommand(currentAddressQuery, connection);
                currentAddressCommand.Parameters.AddWithValue("@StudentID", studentID);
                SqlDataReader currentAddressReader = currentAddressCommand.ExecuteReader();
                if (currentAddressReader.Read())
                {
                    c1Address.Text = currentAddressReader["AddressLine1"].ToString();
                    c2Address.Text = currentAddressReader["AddressLine2"].ToString();
                    cPinCode.Text = currentAddressReader["Pincode"].ToString();
                    cState.Text = currentAddressReader["State"].ToString();
                    cCountry.Text = currentAddressReader["Country"].ToString();                
                }
                currentAddressReader.Close();

                string permanentAddressQuery = "SELECT * FROM AddressDetails WHERE UserID = @StudentID AND AddressType = 2";

                SqlCommand permanentAddressCommand = new SqlCommand(permanentAddressQuery, connection);
                permanentAddressCommand.Parameters.AddWithValue("@StudentID", studentID);

                SqlDataReader permanentAddressReader = permanentAddressCommand.ExecuteReader();
                if (permanentAddressReader.Read())
                {
                    p1Address.Text = permanentAddressReader["AddressLine1"].ToString();
                    p2Address.Text = permanentAddressReader["AddressLine2"].ToString();
                    pPinCode.Text = permanentAddressReader["Pincode"].ToString();

                    pState.Text = permanentAddressReader["State"].ToString();
                    pCountry.Text = permanentAddressReader["Country"].ToString();
                
                }
                permanentAddressReader.Close();

                connection.Close();
            }

       

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                connection.Open();

                string educationQuery10 = "SELECT * FROM EducationDetails WHERE StudentID = @StudentID AND EducationType = 1";
                SqlCommand educationCommand10 = new SqlCommand(educationQuery10, connection);
                educationCommand10.Parameters.AddWithValue("@StudentID", studentID);

                SqlDataReader educationReader10 = educationCommand10.ExecuteReader();
                if (educationReader10.Read())
                {
                    instName10.Text = educationReader10["InstituteName"].ToString();
                    board10.Text = educationReader10["Board"].ToString();

                    string marksType10 = educationReader10["Marks"].ToString();
                    if (marksType10 == "cgpa10")
                    {
                        cgpa10.Checked = true;
                    }
                    else if (marksType10 == "percentage")
                    {
                        percentage.Checked = true;
                    }

                    grade10Value.Text = educationReader10["Aggregate"].ToString();
                    yop10.Text = educationReader10["YearOfCompletion"].ToString();
                }
                educationReader10.Close();

                string educationQuery12 = "SELECT * FROM EducationDetails WHERE StudentID = @StudentID AND EducationType = 2";
                SqlCommand educationCommand12 = new SqlCommand(educationQuery12, connection);
                educationCommand12.Parameters.AddWithValue("@StudentID", studentID);

                SqlDataReader educationReader12 = educationCommand12.ExecuteReader();
                if (educationReader12.Read())
                {
                    instName12.Text = educationReader12["InstituteName"].ToString();
                    board12.Text = educationReader12["Board"].ToString();
                    string marksType12 = educationReader12["Marks"].ToString();
                    if (marksType12 == "cgpa12")
                    {
                        cgpa12.Checked = true;
                    }
                    else if (marksType12 == "percentage")
                    {
                        percentage12.Checked = true;
                    }
                    grade12Value.Text = educationReader12["Aggregate"].ToString();
                    yop12.Text = educationReader12["YearOfCompletion"].ToString();
                }
                educationReader12.Close();

                string educationQueryGraduate = "SELECT * FROM EducationDetails WHERE StudentID = @StudentID AND EducationType = 3";
                SqlCommand educationCommandGraduate = new SqlCommand(educationQueryGraduate, connection);
                educationCommandGraduate.Parameters.AddWithValue("@StudentID", studentID);

                SqlDataReader educationReaderGraduate = educationCommandGraduate.ExecuteReader();
                if (educationReaderGraduate.Read())
                {
                    instNameG.Text = educationReaderGraduate["InstituteName"].ToString();
                    boardG.Text = educationReaderGraduate["Board"].ToString();
                    string marksTypeGraduate = educationReaderGraduate["Marks"].ToString();
                    if (marksTypeGraduate == "cgpa")
                    {
                        cgpaG.Checked = true;
                    }
                    else if (marksTypeGraduate == "percentage")
                    {
                        percentageG.Checked = true;
                    }
                    gradeGValue.Text = educationReaderGraduate["Aggregate"].ToString();
                    yopG.Text = educationReaderGraduate["YearOfCompletion"].ToString();
                }
                educationReaderGraduate.Close();
            }


        }

        private void UpdateUserDetails(int studentID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string updateQuery = @"UPDATE StudentDetails 
                               SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, 
                                   Email = @Email, DateOfBirth = @DateOfBirth, Phone = @Phone, AadharNumber = @AadharNumber,
                                   Gender = @Gender
                               WHERE StudentID = @StudentID";

                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);
                command.Parameters.AddWithValue("@FirstName", firstName.Text);
                command.Parameters.AddWithValue("@MiddleName", middleName.Text);
                command.Parameters.AddWithValue("@LastName", lastName.Text);
                command.Parameters.AddWithValue("@Email", email.Text);
                command.Parameters.AddWithValue("@Phone", phone.Text);
                command.Parameters.AddWithValue("@AadharNumber", aadhar.Text);
                command.Parameters.AddWithValue("@Gender", male.Checked ? "Male" : "Female");
                command.Parameters.AddWithValue("@DateOfBirth", DateTime.Parse(birthday.Text)); // Assuming birthday is always valid

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();                   
                }
                catch (Exception ex)
                {
                    Logger.AddData(ex);
                }
            }
        }

        private void UpdateAddressDetails(int studentID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string updateCurrentAddressQuery = @"UPDATE AddressDetails 
                                             SET Country = @Country, State = @State, 
                                                 AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, 
                                                 Pincode = @Pincode
                                             WHERE UserID = @StudentID AND AddressType = 1";

                SqlCommand command = new SqlCommand(updateCurrentAddressQuery, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);
                command.Parameters.AddWithValue("@Country", cCountry.SelectedValue);
                command.Parameters.AddWithValue("@State", cState.SelectedValue);
                command.Parameters.AddWithValue("@AddressLine1", c1Address.Text);
                command.Parameters.AddWithValue("@AddressLine2", c2Address.Text);
                command.Parameters.AddWithValue("@Pincode", cPinCode.Text);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger.AddData(ex);
                }
            }
        }

        private void UpdateEducationDetails(int studentID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string updateEducationQuery = @"UPDATE EducationDetails 
                                        SET InstituteName = @InstName, Board = @Board, 
                                            Marks = @MarksType, Aggregate = @Grade, 
                                            YearOfCompletion = @YOP
                                        WHERE StudentID = @StudentID";

                SqlCommand command = new SqlCommand(updateEducationQuery, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);

                SetEducationParameters(command, instName10, board10, cgpa10, grade10Value, yop10, "cgpa10");
                SetEducationParameters(command, instName12, board12, cgpa12, grade12Value, yop12, "cgpa12");
                SetEducationParameters(command, instNameG, boardG, cgpaG, gradeGValue, yopG, "cgpa");

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger.AddData(ex);
                }
            }
        }

        private void SetEducationParameters(SqlCommand command, TextBox instName, DropDownList board, RadioButton cgpa, TextBox gradeValue, TextBox yop, string marksType)
        {
            command.Parameters.AddWithValue("@InstName", instName.Text);
            command.Parameters.AddWithValue("@Board", board.SelectedValue); // Use SelectedValue to get the selected item from DropDownList
            command.Parameters.AddWithValue("@MarksType", cgpa.Checked ? marksType : "percentage");
            command.Parameters.AddWithValue("@Grade", Convert.ToDecimal(gradeValue.Text));
            command.Parameters.AddWithValue("@YOP", Convert.ToInt32(yop.Text));
        }

    }
}