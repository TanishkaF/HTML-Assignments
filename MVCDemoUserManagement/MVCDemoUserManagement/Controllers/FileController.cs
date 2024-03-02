using System.Net;
using System.IO;
using System.Web.Mvc;
using DemoUserManagement.BusinessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System;

namespace MVCDemoUserManagement.Controllers
{
    public class FileController : Controller
    {
        [HttpGet]
        [Route("api/download/{documentId}")]
        public ActionResult DownloadFile(int documentId)
        {
            //LogInSessionModel logInSessionModel = ConstantValues.GetUserSessionInfo();
            //int loggedInUserID = logInSessionModel.UserID;

            //if (BasePage.CheckAuthentication(loggedInUserID))
            //{
                //List<int> userDocumentIDs = NoteUserControlBusiness.GetDocumentIDsByObjectID(loggedInUserID);

                //if (userDocumentIDs.Contains(documentId) || logInSessionModel.IsAdmin)
                //{
                    string fileName = NoteUserControlBusiness.GetDocumentUniqueNameById(documentId);
                    string filePath = ConfigurationManager.AppSettings["UploadFolderPath"] + fileName;

                    if (System.IO.File.Exists(filePath))
                    {
                        return File(filePath, "application/octet-stream", fileName);

                    }
                    else
                    {
                        return HttpNotFound("File not found");
                    }
                //}
                //else
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Access denied");
                //}
            //}
            //else
            //{
            //    return new HttpUnauthorizedResult("User not authenticated");
            //}
        }
             

       

    }
}
