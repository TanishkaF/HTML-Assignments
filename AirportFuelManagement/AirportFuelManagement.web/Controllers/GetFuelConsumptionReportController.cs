using AirportFuelManagement.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirportFuelManagement.Controllers
{
    public class GetFuelConsumptionReportController : Controller
    {
        // GET: GetFuelConsumptionReport
        [SessionCheck]
        public ActionResult GetFuelConsumptionReportView()
        {
            var fuelConsumptionReport = AirportBL.GetFuelConsumptionReport();
            return View(fuelConsumptionReport);
        }   
    }
}