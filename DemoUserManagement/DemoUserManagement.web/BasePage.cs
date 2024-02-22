using DemoUserManagement.BusinessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DemoUserManagement.web
{
    public class BasePage : System.Web.UI.Page
    {
        private bool authorizationChecked = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            LogInSessionModel logInSessionModel = ConstantValues.GetUserSessionInfo();
            string currentPageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath).ToLower();

            if (logInSessionModel == null)
            {

                if (!string.IsNullOrWhiteSpace(Request.QueryString["StudentID"]))
                {
                    //do nothing
                    Response.Redirect("UserDetailsV2.aspx");
                }
                else if (currentPageName == "userdetailsv2.aspx")
                {

                }
                else if (currentPageName != "login.aspx")
                {
                    Response.Redirect("login.aspx");
                }
                
            }           
            else if (currentPageName == "login.aspx")
            {
                if (logInSessionModel.IsAdmin)
                {
                    Response.Redirect("userlist.aspx");
                }
                else
                {
                    Response.Redirect($"userdetailsv2.aspx?studentid={logInSessionModel.UserID}");
                }
            }
            else if (currentPageName == "userlist.aspx" && !logInSessionModel.IsAdmin)
            {
                Response.Redirect("login.aspx");
            }
            else if ((string.IsNullOrWhiteSpace(Request.QueryString["UserID"]) || Convert.ToInt32(Request.QueryString["UserID"]) > 0 && logInSessionModel == null))
            {
                CheckAuthorizationAndLoadUserDetails();
            }
            else if (currentPageName == "userdetailsv2.aspx" && !logInSessionModel.IsAdmin)
            {
                CheckAuthorizationAndLoadUserDetails();
                // Response.Redirect($"userdetailsv2.aspx?StudentID={logInSessionModel.UserID}");
            }
        }


        [WebMethod]
        public static void ResetButton_Click()
        {

        }

        public static bool CheckAuthentication(int userID)
        {
            LogInSessionModel logInSessionModel = ConstantValues.GetUserSessionInfo();
            if (userID == logInSessionModel.UserID || logInSessionModel.IsAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [WebMethod]
        public static void InsertNote(NoteViewModel note)
        {

            NoteUserControlBusiness.InsertNote(note);

        }

        [WebMethod]
        public static void InsertDocument(DocumentViewModel document)
        {
            NoteUserControlBusiness.InsertDocument(document);
        }

        [WebMethod]
        public static List<DocumentClass> PopulateDocumentDropDownList()
        {
            return NoteUserControlBusiness.PopulateDocument();
        }

        [WebMethod]
        public static string GetDocumentUniqueNameById(int documentID)
        {
            return NoteUserControlBusiness.GetDocumentUniqueNameById(documentID);
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
                        Response.Redirect($"userdetailsv2.aspx?studentid={authenticatedUserID}");
                    }
                }
                else
                {
                    Response.Redirect("userdetailsv2.aspx");
                }
                authorizationChecked = true;
            }
        }

    }
}