using DemoUserManagement.ViewModel;
using System.Data.SqlClient;
using System.Configuration;

using System;
using System.Net.NetworkInformation;
using System.Net;
using System.Data;
using System.Collections.Generic;

namespace DemoUserManagement.DataAccessLayer
{
    public static class UserDetailsDataAccess
    {
        public static int GetLastInsertedUserID()
        {
            int userID = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = "SELECT IDENT_CURRENT('StudentDetails') AS LastUserID";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    userID = Convert.ToInt32(result);
                }
            }

            return userID;
        }

            public static List<CountryViewModel> GetCountries()
            {
                List<CountryViewModel> countries = new List<CountryViewModel>();
                string query = "SELECT CountryID, CountryName FROM Countries";

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CountryViewModel country = new CountryViewModel
                        {
                            CountryID = Convert.ToInt32(reader["CountryID"]),
                            CountryName = reader["CountryName"].ToString()
                        };
                        countries.Add(country);
                    }
                    reader.Close();
                }
                return countries;
            }

            public static List<StateViewModel> GetStates(int countryID)
            {
                List<StateViewModel> states = new List<StateViewModel>();
                string query = "SELECT StateID, StateName FROM States WHERE CountryID = @CountryID";

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CountryID", countryID);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                    StateViewModel state = new StateViewModel
                    {
                            StateID = Convert.ToInt32(reader["StateID"]),
                            StateName = reader["StateName"].ToString()
                        };
                        states.Add(state);
                    }
                    reader.Close();
                }
                return states;
            }
        



        public static void InsertStudentDetails(StudentDetailViewModel studentDetails)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = @"INSERT INTO StudentDetails (FirstName, MiddleName, LastName, Email, DateOfBirth, Gender, Phone, AadharNumber) 
                   VALUES (@FirstName, @MiddleName, @LastName, @Email, @DateOfBirth, @Gender, @Phone, @AadharNumber)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", studentDetails.FirstName);
                command.Parameters.AddWithValue("@MiddleName", studentDetails.MiddleName);
                command.Parameters.AddWithValue("@LastName", studentDetails.LastName);
                command.Parameters.AddWithValue("@Email", studentDetails.Email);
                command.Parameters.AddWithValue("@DateOfBirth", studentDetails.DateOfBirth.HasValue ? (object)studentDetails.DateOfBirth.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Gender", studentDetails.Gender);
                command.Parameters.AddWithValue("@Phone", studentDetails.Phone);
                command.Parameters.AddWithValue("@AadharNumber", studentDetails.AadharNumber);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void InsertAddressDetails(AddressDetailViewModel addressDetails)
        {
            string query = @"INSERT INTO AddressDetails (UserID, AddressType, Country, State, AddressLine1, AddressLine2, Pincode) 
                     VALUES (@UserID, @AddressType, @Country, @State, @AddressLine1, @AddressLine2, @Pincode)";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", addressDetails.UserID);
                command.Parameters.AddWithValue("@AddressType", addressDetails.AddressType);
                command.Parameters.AddWithValue("@Country", string.IsNullOrEmpty(addressDetails.Country) ? (object)DBNull.Value : (object)addressDetails.Country);
                command.Parameters.AddWithValue("@State", string.IsNullOrEmpty(addressDetails.State) ? (object)DBNull.Value : (object)addressDetails.State);
                command.Parameters.AddWithValue("@AddressLine1", string.IsNullOrEmpty(addressDetails.AddressLine1) ? (object)DBNull.Value : (object)addressDetails.AddressLine1);
                command.Parameters.AddWithValue("@AddressLine2", string.IsNullOrEmpty(addressDetails.AddressLine2) ? (object)DBNull.Value : (object)addressDetails.AddressLine2);
                command.Parameters.AddWithValue("@Pincode", string.IsNullOrEmpty(addressDetails.Pincode) ? (object)DBNull.Value : (object)addressDetails.Pincode);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public static void InsertEducationDetails(EducationDetailViewModel educationDetails)
        {
            string query = @"INSERT INTO EducationDetails (StudentID, EducationType, InstituteName, Board, Marks, Aggregate, YearOfCompletion) 
                             VALUES (@StudentID, @EducationType, @InstituteName, @Board, @Marks, @Aggregate, @YearOfCompletion)";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", educationDetails.StudentID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EducationType", educationDetails.EducationType ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@InstituteName", string.IsNullOrEmpty(educationDetails.InstituteName) ? DBNull.Value : (object)educationDetails.InstituteName);
                command.Parameters.AddWithValue("@Board", string.IsNullOrEmpty(educationDetails.Board) ? DBNull.Value : (object)educationDetails.Board);
                command.Parameters.AddWithValue("@Marks", string.IsNullOrEmpty(educationDetails.Marks) ? DBNull.Value : (object)educationDetails.Marks);
                command.Parameters.AddWithValue("@Aggregate", educationDetails.Aggregate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@YearOfCompletion", educationDetails.YearOfCompletion ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public static void UpdateStudentDetails(int studentID, StudentDetailViewModel studentDetails)
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
                command.Parameters.AddWithValue("@FirstName", studentDetails.FirstName);
                command.Parameters.AddWithValue("@MiddleName", studentDetails.MiddleName);
                command.Parameters.AddWithValue("@LastName", studentDetails.LastName);
                command.Parameters.AddWithValue("@Email", studentDetails.Email);
                command.Parameters.AddWithValue("@DateOfBirth", studentDetails.DateOfBirth.HasValue ? (object)studentDetails.DateOfBirth.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Phone", studentDetails.Phone);
                command.Parameters.AddWithValue("@AadharNumber", studentDetails.AadharNumber);
                command.Parameters.AddWithValue("@Gender", studentDetails.Gender);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                // Handle the update result as needed
            }
        }

        public static void UpdateAddressDetails(int userID, int addressType, AddressDetailViewModel addressDetails)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string updateAddressQuery = @"UPDATE AddressDetails 
                             SET Country = @Country, State = @State, 
                                 AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, 
                                 Pincode = @Pincode
                             WHERE UserID = @UserID AND AddressType = @AddressType";

                SqlCommand command = new SqlCommand(updateAddressQuery, connection);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@AddressType", addressType);
                command.Parameters.AddWithValue("@Country", addressDetails.Country);
                command.Parameters.AddWithValue("@State", addressDetails.State);
                command.Parameters.AddWithValue("@AddressLine1", addressDetails.AddressLine1);
                command.Parameters.AddWithValue("@AddressLine2", addressDetails.AddressLine2);
                command.Parameters.AddWithValue("@Pincode", addressDetails.Pincode);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle the exception
                }
            }
        }

        public static void UpdateEducationDetails(int studentID, int educationType, EducationDetailViewModel educationDetails)
        {
            string query = @"UPDATE EducationDetails 
                     SET InstituteName = @InstituteName, 
                         Board = @Board, 
                         Marks = @Marks, 
                         Aggregate = @Aggregate, 
                         YearOfCompletion = @YearOfCompletion
                     WHERE StudentID = @StudentID AND EducationType = @EducationType";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);
                command.Parameters.AddWithValue("@EducationType", educationType);
                command.Parameters.AddWithValue("@InstituteName", string.IsNullOrEmpty(educationDetails.InstituteName) ? DBNull.Value : (object)educationDetails.InstituteName);
                command.Parameters.AddWithValue("@Board", string.IsNullOrEmpty(educationDetails.Board) ? DBNull.Value : (object)educationDetails.Board);
                command.Parameters.AddWithValue("@Marks", string.IsNullOrEmpty(educationDetails.Marks) ? DBNull.Value : (object)educationDetails.Marks);
                command.Parameters.AddWithValue("@Aggregate", educationDetails.Aggregate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@YearOfCompletion", educationDetails.YearOfCompletion ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public static string ValidateAndTrimInput(string input)
        {
            return input?.Trim() ?? string.Empty;
        }

        public static DateTime? ParseDateTime(string dateTimeString)
        {
            if (!string.IsNullOrEmpty(dateTimeString))
            {
                if (DateTime.TryParse(dateTimeString, out DateTime parsedDate))
                {
                    return parsedDate;
                }
                else
                {
                    // Handle invalid date format
                    throw new ArgumentException("Invalid Date of Birth format");
                }
            }
            return null;
        }

        public static AddressDetailViewModel GetCurrentAddress(int studentID)
        {
            AddressDetailViewModel currentAddress = new AddressDetailViewModel();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string currentAddressQuery = "SELECT * FROM AddressDetails WHERE UserID = @StudentID AND AddressType = 1";
                SqlCommand command = new SqlCommand(currentAddressQuery, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        currentAddress.AddressLine1 = reader["AddressLine1"].ToString();
                        currentAddress.AddressLine2 = reader["AddressLine2"].ToString();
                        currentAddress.Pincode = reader["Pincode"].ToString();
                        currentAddress.State = reader["State"].ToString();
                        currentAddress.Country = reader["Country"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle the exception
                }
            }

            return currentAddress;
        }

        public static StudentDetailViewModel GetStudentDetails(int studentID)
            {
                StudentDetailViewModel studentDetails = new StudentDetailViewModel();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                    string studentQuery = "SELECT * FROM StudentDetails WHERE StudentID = @StudentID";
                    SqlCommand command = new SqlCommand(studentQuery, connection);
                    command.Parameters.AddWithValue("@StudentID", studentID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            studentDetails.FirstName = reader["FirstName"].ToString();
                            studentDetails.MiddleName = reader["MiddleName"].ToString();
                            studentDetails.LastName = reader["LastName"].ToString();
                            studentDetails.Email = reader["Email"].ToString();
                            studentDetails.DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]) : (DateTime?)null;
                            studentDetails.Phone = reader["Phone"].ToString();
                            studentDetails.AadharNumber = reader["AadharNumber"].ToString();
                            studentDetails.Gender = reader["Gender"].ToString();
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception
                    }
                }

                return studentDetails;
            }

        public static AddressDetailViewModel GetPermanentAddress(int studentID)
        {
            AddressDetailViewModel permanentAddress = new AddressDetailViewModel();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string permanentAddressQuery = "SELECT * FROM AddressDetails WHERE UserID = @StudentID AND AddressType = 2";
                SqlCommand command = new SqlCommand(permanentAddressQuery, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        permanentAddress.AddressLine1 = reader["AddressLine1"].ToString();
                        permanentAddress.AddressLine2 = reader["AddressLine2"].ToString();
                        permanentAddress.Pincode = reader["Pincode"].ToString();
                        permanentAddress.State = reader["State"].ToString(); // Update State field
                        permanentAddress.Country = reader["Country"].ToString(); // Update Country field
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle the exception
                }
            }

            return permanentAddress;
        }

        private static string connectionString = ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString;

            public static EducationDetailViewModel GetEducationDetails(int studentID, int educationType)
            {
                EducationDetailViewModel educationDetails = new EducationDetailViewModel();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string educationQuery = "SELECT * FROM EducationDetails WHERE StudentID = @StudentID AND EducationType = @EducationType";
                    SqlCommand command = new SqlCommand(educationQuery, connection);
                    command.Parameters.AddWithValue("@StudentID", studentID);
                    command.Parameters.AddWithValue("@EducationType", educationType);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            educationDetails.InstituteName = reader["InstituteName"].ToString();
                            educationDetails.Board = reader["Board"].ToString();
                            educationDetails.Marks = reader["Marks"].ToString();
                            educationDetails.Aggregate = reader["Aggregate"] != DBNull.Value ? Convert.ToDecimal(reader["Aggregate"]) : (decimal?)null;
                            educationDetails.YearOfCompletion = reader["YearOfCompletion"] != DBNull.Value ? Convert.ToInt32(reader["YearOfCompletion"]) : (int?)null;
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception
                    }
                }

                return educationDetails;
            }

            public static EducationDetailViewModel GetEducation10(int studentID)
            {
                return GetEducationDetails(studentID, 1);
            }

            public static EducationDetailViewModel GetEducation12(int studentID)
            {
                return GetEducationDetails(studentID, 2);
            }

            public static EducationDetailViewModel GetEducationGraduate(int studentID)
            {
                return GetEducationDetails(studentID, 3);
            }
        }
    }
