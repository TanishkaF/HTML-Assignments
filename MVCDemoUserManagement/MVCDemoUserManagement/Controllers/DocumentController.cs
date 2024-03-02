using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using DemoUserManagement.UtilityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;


namespace MVCDemoUserManagement.Controllers
{
    public class DocumentController : Controller
    {
        public ActionResult Index(int objectId, int objectType)
        {
            var model = new DocumentViewModel
            {
                ObjectID = objectId,
                ObjectType = objectType
            };

            return View(model);
        }

        public ActionResult PopulateDropdown()
        {
            List<DocumentClass> documentList = NoteUserControlBusiness.GetDocumentType();
            ViewBag.DocumentList = documentList;
            return PartialView("_Dropdown");
        }

        [SessionCheck]
        public ActionResult GetDocuments(int objectId, int objectType, int pageIndex = 0, int pageSize = 3, string sortExpression = "DocumentID", string sortDirection = "ASC")
        {
            string actualSortDirection = sortDirection.ToUpper() == "DESC" ? "DESC" : "ASC";

            int totalDocumentCount = NoteUserControlBusiness.GetTotalDocumentCount(objectId, objectType);
            int totalPages = (int)Math.Ceiling((double)totalDocumentCount / pageSize);
            pageIndex = Math.Max(0, Math.Min(pageIndex, totalPages - 1));

            DataTable dt = NoteUserControlBusiness.GetAllDocumentData(sortExpression, actualSortDirection, pageIndex, pageSize, objectId, objectType);
            List<DocumentViewModel> model = ConvertDataTableToList(dt);


            foreach (var doc in model)
            {
                doc.PageIndex = pageIndex;
                doc.TotalPages = totalPages;
            }

            ViewBag.SortExpression = sortExpression;
            ViewBag.SortDirection = sortDirection;
            PopulateDropdown();
            return PartialView("_DocumentGridView", model);
        }

        private List<DocumentViewModel> ConvertDataTableToList(DataTable dt)
        {
            var list = new List<DocumentViewModel>();

            foreach (DataRow row in dt.Rows)
            {
                var document = new DocumentViewModel
                {
                    DocumentID = Convert.ToInt32(row["DocumentID"]),
                    ObjectID = Convert.ToInt32(row["ObjectID"]),
                    ObjectType = Convert.ToInt32(row["ObjectType"]),
                    DocumentType = Convert.ToInt32(row["DocumentType"]),
                    DiskDocumentName = row["DiskDocumentName"].ToString(),
                    OriginalDocumentName = row["OriginalDocumentName"].ToString(),
                    Timestamp = Convert.ToDateTime(row["TimeStamp"])
                };
                list.Add(document);
            }

            return list;
        }

        [HttpPost]
        public void InsertDocument(DocumentViewModel1 document)
        {
            if (SessionCheckAttribute.CheckAuthentication(document.ObjectID))
            {
                NoteUserControlBusiness.InsertDocument(null);
            }
        }
    }

}
