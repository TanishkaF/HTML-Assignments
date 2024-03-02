using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using DemoUserManagement.BusinessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;


namespace MVCDemoUserManagement
{
    namespace DemoUserManagement.web
    {
        /// <summary>
        /// Summary description for DownloadFile
        /// </summary>
        public class DownloadFile : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                        string filePath = ConfigurationManager.AppSettings["UploadFolderPath"] + fileName;
                        if (File.Exists(filePath))
                        {
                            string extension = Path.GetExtension(filePath);
                            string mimeType = GetMimeType(extension);

                            context.Response.ContentType = mimeType;
                            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                            context.Response.TransmitFile(filePath);
                            context.Response.End();
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
                        context.Response.Redirect(redirectURL);
                    }
                }
                else
                {
                    string redirectUrl = "LogIn.aspx";
                    context.Response.Redirect(redirectUrl);
                }
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
                    case ".jpg":
                    case ".jpeg":
                        return "image/jpeg";
                    case ".png":
                        return "image/png";
                    case ".gif":
                        return "image/gif";
                    default:
                        return "application/octet-stream";
                }
            }

            public bool IsReusable
            {
                get
                {
                    return false;
                }
            }
        }
    }
}