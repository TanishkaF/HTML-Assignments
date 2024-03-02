using DemoUserManagement.BusinessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoUserManagement.Controllers
{
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index(int objectId, int objectType)
        {
            var model = new NoteViewModel
            {
                ObjectID = objectId,
                ObjectType = objectType
            };

            return View(model);
        }

      
        [HttpPost]
        public ActionResult Add(NoteViewModel note)
        {
            if (SessionCheckAttribute.CheckAuthentication(note.ObjectID))
            {
                if (ModelState.IsValid)
                {
                    NoteUserControlBusiness.InsertNote(note);
                }

            }
             //   return RedirectToAction("UserDetails", "UserDetails", new { studentid = note.ObjectID });
            return RedirectToAction("GetNotes", new { sortExpression = "NoteID", sortDirection = "DESC", objectId = note.ObjectID, objectType = note.ObjectType });
        }

        public ActionResult GetNotes(int objectId, int objectType, int pageIndex=NoteType.PageIndex, int pageSize=NoteType.PageSize,string sortExpression=NoteType.SortExpression, string sortDirection =NoteType.SortDirection)

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
            ///]return RedirectToAction("UserDetails", "UserDetails", new { studentid = objectId });
           // return PartialView("~/Views/Note/_NoteGridView.cshtml", model);

             return PartialView("_NoteGridView", model);
        }


        private List<NoteViewModel> ConvertDataTableToList(DataTable dt)
        {
            List<NoteViewModel> list = new List<NoteViewModel>();

            foreach (DataRow row in dt.Rows)
            {
                var note = new NoteViewModel
                {
                    NoteID = Convert.ToInt32(row["NoteID"]),
                    ObjectID = Convert.ToInt32(row["ObjectID"]),
                    ObjectType = Convert.ToInt32(row["ObjectType"]),
                    NoteText = row["NoteText"].ToString(),
                    TimeStamp = Convert.ToDateTime(row["TimeStamp"])
                };
                list.Add(note);
            }

            return list;
        }
    }
}

