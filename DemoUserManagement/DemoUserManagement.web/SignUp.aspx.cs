using DemoUserManagement.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement.web
{
    public partial class SignUp1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void BtnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserDetails.aspx");
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            bool isValidUser = ValidateUser(email, password);

            if (isValidUser)
            {

                if (IsAdmin(email))
                {
                    Response.Redirect("~/UserList.aspx");
                }
                else
                {
                    int userID = GetUserID(email);
                    Session["AuthenticatedUserID"] = userID;
                    Response.Redirect($"~/UserDetails.aspx?StudentID={userID}");


                }                
            }else{
                lblError.Text = "Invalid combination of EmailID with Password.";
            }
        }
        

        [WebMethod]
        public static bool ValidateUser(string email, string password)
        {
            return AuthenticationServiceBusiness.ValidateUser(email, password);
        }


        [WebMethod]
        public static bool CheckEmail(string email)
       {
            return AuthenticationServiceBusiness.CheckEmailExists(email);
             
        }

        private bool IsAdmin(string email)
        {
            return AuthenticationServiceBusiness.IsAdmin(email);
        }

        private int GetUserID(string email)
        {
            return AuthenticationServiceBusiness.GetUserID(email);
        }

        private int GetLoggedInUserID()
        {
            string email = txtEmail.Text.Trim(); // Get the email from the textbox
            return AuthenticationServiceBusiness.GetUserID(email);
        }

    }
}