using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using AirportFuelManagement.Attribute;
using AirportFuelManagement.UtilityLayer;
using AirportFuelManagement.ViewModel;


namespace AirportFuelManagement.Controllers
{
    public class AirportFuelController : Controller
    {
        // GET: AirportFuel
        [SessionCheck]
        public ActionResult AirportFuelView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllFuelSummary(int pageIndex = AirportFuelConstants.PageIndex, int pageSize = AirportFuelConstants.PageSize, string sortExpression = AirportFuelConstants.SortExpression,
       string sortDirection = AirportFuelConstants.SortDirection)
        {
            string actualSortDirection = sortDirection.ToUpper() == "DESC" ? "DESC" : "ASC";

            int totalAirportFuelCount = AirportBL.GetTotalAirportCount();

            int totalPages = (int)Math.Ceiling((double)totalAirportFuelCount / pageSize);

            pageIndex = Math.Max(0, Math.Min(pageIndex, totalPages - 1));
            List<AllViewModel.AirportFuelSummary> airports = AirportBL.GetAllFuelSummary(sortExpression, actualSortDirection, pageIndex, pageSize);

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