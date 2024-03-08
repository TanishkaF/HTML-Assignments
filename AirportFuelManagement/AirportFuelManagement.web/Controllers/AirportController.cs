using AirportFuelManagement.Attribute;
using AirportFuelManagement.BusinessLayer;
using AirportFuelManagement.UtilityLayer;
using AirportFuelManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirportFuelManagement.Controllers
{
    public class AirportController : Controller
    {
        // GET: Airport
        [SessionCheck]
        public ActionResult AirportView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddAirportForm()
        {
            return PartialView("_AddAirportFormView");
        }

        [HttpPost]
        public ActionResult AddAirport(AllViewModel.Airport airport)
        {
            try
            {
                if (AirportBL.AddAirport(airport))
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to add airport. Please try again." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetAllAirports(int pageIndex = AirportConstants.PageIndex, int pageSize = AirportConstants.PageSize, string sortExpression = AirportConstants.SortExpression, 
            string sortDirection = AirportConstants.SortDirection){

            string actualSortDirection = sortDirection.ToUpper() == "DESC" ? "DESC" : "ASC";

            int totalAirportCount = AirportBL.GetTotalAirportCount();

            int totalPages = (int)Math.Ceiling((double)totalAirportCount / pageSize);

            pageIndex = Math.Max(0, Math.Min(pageIndex, totalPages - 1));
            List<AllViewModel.Airport> airports = AirportBL.GetAllAirports(sortExpression,actualSortDirection,pageIndex,pageSize);

            var result = new
            {
                Airports = airports,
                PageIndex = pageIndex,
                TotalPages = totalPages
            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}