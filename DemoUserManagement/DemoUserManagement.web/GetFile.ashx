<%@ WebHandler Language="C#" Class="DemoUserManagement.web.GetFile" %>
using System;
using System.Configuration;
using System.IO;
using System.Web;
using DemoUserManagement.UtilityLayer;


namespace DemoUserManagement.web
{
    public class GetFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string fileName = context.Request.QueryString["fileName"];
                string folderPath = ConfigurationManager.AppSettings["UploadFolderPath"];
                string filePath = Path.Combine(folderPath, fileName);

                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    context.Response.Clear();
                   context.Response.ContentType = "application/pdf"; 

                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    context.Response.AddHeader("Content-Length", file.Length.ToString());
           //       context.Response.ContentType = GetMimeType(file.Extension);
                    context.Response.TransmitFile(file.FullName);
                    context.Response.Flush();
                }
                else
                {
                    context.Response.StatusCode = 404;
                    context.Response.Write("File not found");
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                context.Response.StatusCode = 500;
                context.Response.Write("An error occurred: " + ex.Message);
            }
            finally
            {
                context.Response.End();
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
