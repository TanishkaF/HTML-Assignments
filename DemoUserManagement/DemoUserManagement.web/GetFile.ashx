<%@ WebHandler Language="C#" Class="DemoUserManagement.web.GetFile" %>
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Collections.Generic;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System.Net;


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
                    string url = $"GetFile.ashx?documentID={documentId}";
                    if (File.Exists(filePath))
                    {
                        //context.Response.ContentType = "application/octet-stream";
                        //context.Response.AppendHeader("Content-Disposition", "inline; filename=\"" + HttpUtility.UrlPathEncode(fileName) + "\"");
                        //context.Response.TransmitFile(filePath);
                        //context.Response.Flush();
                        //context.Response.End();

                        context.Response.ContentType = "application/octet-stream";
                        context.Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlPathEncode(fileName) + "\"");
                        context.Response.TransmitFile(url);
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
                    string redirectURL = "UserDetailsV2.aspx?StudentID=" + loggedInUserID;
                    //  context.Response.Write("Access denied");
                    context.Response.Redirect(redirectURL);
                    // return;
                }
            }
            else
            {
                string redirectUrl = "LogIn.aspx";
                context.Response.Redirect(redirectUrl);
            }
        }



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
<%@ WebHandler Language="C#" Class="DemoUserManagement.web.GetFile" %>
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Collections.Generic;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System.Net;


namespace DemoUserManagement.web
{
    public class GetFile : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //public void ProcessRequest(HttpContext context)
        //{
        //    LogInSessionModel logInSessionModel = ConstantValues.GetUserSessionInfo();
        //    int loggedInUserID = logInSessionModel.UserID;

        //    if (BasePage.CheckAuthentication(loggedInUserID))
        //    {
        //        string documentIdString = context.Request.QueryString["documentID"];
        //        if (string.IsNullOrEmpty(documentIdString) || !int.TryParse(documentIdString, out int documentId))
        //        {
        //            context.Response.StatusCode = 400;
        //            context.Response.Write("Invalid document ID");
        //            return;
        //        }

        //        List<int> userDocumentIDs = NoteUserControlBusiness.GetDocumentIDsByObjectID(loggedInUserID);

        //        if (userDocumentIDs.Contains(documentId) || logInSessionModel.IsAdmin)
        //        {
        //            string fileName = NoteUserControlBusiness.GetDocumentUniqueNameById(documentId);
        //            string folderPath = ConfigurationManager.AppSettings["UploadFolderPath"];
        //            string filePath = Path.Combine(folderPath, fileName);

        //            if (File.Exists(filePath))
        //            {
        //                string getFileUrl = $"https://localhost:44398/GetFile.ashx?documentID={documentId}";

        //                try
        //                {
        //                    using (var client = new WebClient())
        //                    {
        //                        // Read the file content
        //                        byte[] fileContent = File.ReadAllBytes(filePath);

        //                        // Upload the file to the specified URL
        //                        client.UploadData(getFileUrl, fileContent);
        //                    }

        //                    // File uploaded successfully
        //                    context.Response.Write("File uploaded successfully");
        //                }
        //                catch (Exception ex)
        //                {
        //                    // Handle any exceptions that may occur during the file upload process
        //                    context.Response.StatusCode = 500;
        //                    context.Response.Write("An error occurred while uploading the file: " + ex.Message);
        //                }
        //            }
        //            else
        //            {
        //                context.Response.StatusCode = 404;
        //                context.Response.Write("File not found");
        //            }
        //        }
        //        else
        //        {
        //            context.Response.StatusCode = 403; // Forbidden
        //            string redirectURL = "UserDetailsV2.aspx?StudentID=" + loggedInUserID;
        //            context.Response.Redirect(redirectURL);
        //        }
        //    }
        //    else
        //    {
        //        string redirectUrl = "LogIn.aspx";
        //        context.Response.Redirect(redirectUrl);
        //    }
        //}


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
                    string url = $"GetFile.ashx?documentID={documentId}";
                    if (File.Exists(filePath))
                    {
                        //context.Response.ContentType = "application/octet-stream";
                        //context.Response.AppendHeader("Content-Disposition", "inline; filename=\"" + HttpUtility.UrlPathEncode(fileName) + "\"");
                        //context.Response.TransmitFile(filePath);
                        //context.Response.Flush();
                        //context.Response.End();

                        context.Response.ContentType = "application/octet-stream";
                        context.Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlPathEncode(fileName) + "\"");
                        context.Response.TransmitFile(url);
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
                    string redirectURL = "UserDetailsV2.aspx?StudentID=" + loggedInUserID;
                    //  context.Response.Write("Access denied");
                    context.Response.Redirect(redirectURL);
                    // return;
                }
            }
            else
            {
                string redirectUrl = "LogIn.aspx";
                context.Response.Redirect(redirectUrl);
            }
        }



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
