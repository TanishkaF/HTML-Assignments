using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoUserManagement.Controllers
{
    public class DocumentUserV2Controller : Controller
    {
        public ActionResult PopulateDropdown()
        {
            List<DocumentClass> documentList = NoteUserControlBusiness.GetDocumentType();
            return Json(documentList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetDocuments(int objectId, int objectType, int pageIndex,int pageSize, string sortExpression, string sortDirection)
        {
            string actualSortDirection = sortDirection.ToUpper() == "DESC" ? "DESC" : "ASC";

            int totalDocumentCount = NoteUserControlBusiness.GetTotalDocumentCount(objectId, objectType);
            int totalPages = (int)Math.Ceiling((double)totalDocumentCount / pageSize);
            pageIndex = Math.Max(0, Math.Min(pageIndex, totalPages - 1));

            DataTable dt = NoteUserControlBusiness.GetAllDocumentData(sortExpression, actualSortDirection, pageIndex, pageSize, objectId, objectType);
            List<DocumentViewModel> model = NoteUserControlBusiness.ConvertDataTableToList(dt);


            foreach (var doc in model)
            {
                doc.PageIndex = pageIndex;
                doc.TotalPages = totalPages;
            }

            ViewBag.SortExpression = sortExpression;
            ViewBag.SortDirection = sortDirection;
            return Json(model, JsonRequestBehavior.AllowGet);
        }  
        
    }
}