<%@ WebHandler Language="C#" Class="DemoUserManagement.web.GetFile" %>
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Collections.Generic;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;



namespace DemoUserManagement.web
{
    public class GetFile : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            LogInSessionModel logInSessionModel = ConstantValues.GetUserSessionInfo();
            int loggedInUserID = logInSessionModel.UserID;

            if (BasePage.CheckAuthentication(loggedInUserID))
            {
                string documentIdString = context.Request.QueryString["documentID"];
                if (string.IsNullOrEmpty(documentIdString) || !int.TryParse(documentIdString, out int documentId))
                {
                    context.Response.StatusCode = 400;
                    context.Response.Write("Invalid document ID");
                    return;
                }

                List<int> userDocumentIDs = NoteUserControlBusiness.GetDocumentIDsByObjectID(loggedInUserID);

                if (userDocumentIDs.Contains(documentId) || logInSessionModel.IsAdmin)
                {
                    string fileName = NoteUserControlBusiness.GetDocumentUniqueNameById(documentId);
                    string folderPath = ConfigurationManager.AppSettings["UploadFolderPath"];
                    string filePath = Path.Combine(folderPath, fileName);

                    if (File.Exists(filePath))
                    {
                        context.Response.ContentType = "application/octet-stream";
                        context.Response.AppendHeader("Content-Disposition", "inline; filename=\"" + HttpUtility.UrlPathEncode(fileName) + "\"");
                        context.Response.TransmitFile(filePath);
                        return;
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                        context.Response.Write("File not found");
                        return;
                    }
                }
                else
                {
                    context.Response.StatusCode = 403; // Forbidden
                    context.Response.Write("Access denied");
                    return;
                }
            }
            else
            {
                string redirectUrl = "UserDetailsV2.aspx?StudentID=" + loggedInUserID;
                context.Response.Redirect(redirectUrl);
            }
        }


        //public void ProcessRequest(HttpContext context)
        //{
        //    LogInSessionModel logInSessionModel = ConstantValues.GetUserSessionInfo();
        //    int loggedInUserID = logInSessionModel.UserID;

        //    if (BasePage.CheckAuthentication(loggedInUserID))
        //    {
        //        string documentId = context.Request.QueryString["documentID"];
        //        List<int> list = NoteUserControlBusiness.GetDocumentIDsByObjectID(loggedInUserID);

        //        if (!string.IsNullOrEmpty(documentId) && int.TryParse(documentId, out int id))
        //        {
        //            string fileName = NoteUserControlBusiness.GetDocumentUniqueNameById(id);
        //            string folderPath = ConfigurationManager.AppSettings["UploadFolderPath"];
        //            string filePath = Path.Combine(folderPath, fileName);

        //            if (File.Exists(filePath))
        //            {
        //                context.Response.ContentType = "application/octet-stream";
        //                context.Response.AppendHeader("Content-Disposition", "inline; filename=\"" + HttpUtility.UrlPathEncode(fileName) + "\"");
        //                context.Response.TransmitFile(filePath);
        //            }
        //            else
        //            {
        //                context.Response.StatusCode = 404;
        //                context.Response.Write("File not found");
        //            }
        //        }
        //        else
        //        {
        //            context.Response.StatusCode = 400;
        //            context.Response.Write("Invalid document ID");
        //        }
        //    }
        //    else
        //    {
        //        string redirectUrl = "UserDetailsV2.aspx?StudentID=" + loggedInUserID;
        //        context.Response.Redirect(redirectUrl);
        //        // HttpResponse.Redirect("UserDetailsV2.aspx?StudentID=");
        //    }
        //    // context.Response.End();
        //}

        public bool IsReusable
        {
            get { return false; }
        }

        private string GetMimeType(string extension)
        {
            switch (extension.ToLower())
            {
                case ".pdf":
                    return "application/pdf";
                case ".doc":
                case ".docx":
                    return "application/msword";
                case ".xls":
                case ".xlsx":
                    return "application/vnd.ms-excel";
                case ".txt":
                    return "text/plain";
                case ".zip":
                    return "application/zip";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
