using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using DemoUserManagement.UtilityLayer;

namespace MVCDemoUserManagement.Controllers
{
    public class NoteUserV2Controller : Controller
    {
        [HttpPost]
        public ActionResult Add(NoteViewModel note)
        {
            if (ModelState.IsValid)
                {
                    NoteUserControlBusiness.InsertNote(note);
                    return Json(new { success = true });
                }
            
            return Json(new { success = false });
        }

        public ActionResult GetNotes(int objectId, int objectType, int pageIndex, int pageSize, string sortExpression, string sortDirection)
        {
            string actualSortDirection = sortDirection.ToUpper() == "DESC" ? "DESC" : "ASC";

            int totalNotesCount = NoteUserControlBusiness.GetTotalNotesCount(objectId, objectType);
            int totalPages = (int)Math.Ceiling((double)totalNotesCount / pageSize);

            pageIndex = Math.Max(0, Math.Min(pageIndex, totalPages - 1));

            DataTable dt = NoteUserControlBusiness.GetAllNotesData(sortExpression, actualSortDirection, pageIndex, pageSize, objectId, objectType);
            List<NoteViewModel> model = ConvertDataTableToList(dt);

            foreach (var note in model)
            {
                note.PageIndex = pageIndex;
                note.TotalPages = totalPages;
            }

            ViewBag.SortExpression = sortExpression;
            ViewBag.SortDirection = sortDirection;

            //var jsonData = new
            //{
            //    Notes = model,
            //    PageIndex = pageIndex,
            //    TotalPages = totalPages
            //};

            return Json(model, JsonRequestBehavior.AllowGet);

           // return PartialView("NoteViewV2", model);
        }

        private List<NoteViewModel> ConvertDataTableToList(DataTable dt)
        {
            return dt.AsEnumerable().Select(row => new NoteViewModel
            {
                NoteID = Convert.ToInt32(row["NoteID"]),
                ObjectID = Convert.ToInt32(row["ObjectID"]),
                ObjectType = Convert.ToInt32(row["ObjectType"]),
                NoteText = row["NoteText"].ToString(),
                TimeStamp = Convert.ToDateTime(row["TimeStamp"])
            }).ToList();
        }
    }
}
