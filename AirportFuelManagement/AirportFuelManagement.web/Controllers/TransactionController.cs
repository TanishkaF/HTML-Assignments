//using AirportFuelManagement.UtilityLayer;
//using AirportFuelManagement.ViewModel;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace AirportFuelManagement.Controllers
//{
//    public class TransactionController : Controller
//    {
//        // GET: Transaction
//        public ActionResult TransactionView()
//        {
//            return View();
//        }
//        public ActionResult AddReverseTransactionView()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult AddTransaction(AllViewModel.FuelTransaction fuelTransaction)
//        {
//            try
//            {
//                if (AirportBL.AddTransaction(fuelTransaction))
//                {
//                    return Json(new { success = true });
//                }
//                else
//                {
//                    return Json(new { success = false, message = "Failed to add Transaction. Please try again." });
//                }
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, message = ex.Message });
//            }
//        }

//        public ActionResult GetTransactionForm(int transactionType)
//        {
//            var model = new AllViewModel.FuelTransaction
//            {
//                TransactionType = transactionType
//            };

//            return PartialView("_TransactionView", model);
//        }

//        [HttpGet]
//        public ActionResult GetAllTransactions(int pageIndex = TransactionConstants.PageIndex, int pageSize = TransactionConstants.PageSize, string sortExpression = TransactionConstants.SortExpression,
//            string sortDirection = TransactionConstants.SortDirection)
//        {

//            string actualSortDirection = sortDirection.ToUpper() == "DESC" ? "DESC" : "ASC";

//            int totalTransactionCount = AirportBL.GetTotalTransactionCount();

//            int totalPages = (int)Math.Ceiling((double)totalTransactionCount / pageSize);

//            pageIndex = Math.Max(0, Math.Min(pageIndex, totalPages - 1));
//            List<AllViewModel.FuelTransaction> tansactions = AirportBL.GetAllTransactions(sortExpression, actualSortDirection, pageIndex, pageSize);

//            var result = new
//            {
//                FuelTransaction = tansactions,
//                PageIndex = pageIndex,
//                TotalPages = totalPages
//            };

//            return Json(result, JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult GetReverseTransactionForm(int transactionID)
//        {
//            var transactionDetails = AirportBL.GetTransactionDetails(transactionID);
//            var model = new AllViewModel.FuelTransaction
//            {
//                TransactionID = transactionDetails.TransactionID,
//                TransactionType = transactionDetails.TransactionType,
//                AirportID = transactionDetails.AirportID,
//                AircraftID = transactionDetails.AircraftID,
//                Quantity = transactionDetails.Quantity,
//                TransactionIDParent = transactionDetails.TransactionIDParent
//            };


//            return PartialView("_AddReverseTransactionView", model);
//        }

//        [HttpGet]
//        public string GetAirportsJson()
//        {
//            List<AllViewModel.Airport> airports = AirportBL.GetAirportList();
//            return JsonConvert.SerializeObject(airports);
//        }

//        [HttpGet]
//        public string GetAircraftJson()
//        {
//            List<AllViewModel.Aircraft> aircrafts = AirportBL.GetAircraftList();
//            return JsonConvert.SerializeObject(aircrafts);
//        }

//        [HttpPost]
//        public ActionResult RemoveAllTransactions()
//        {
//            bool success = AirportBL.RemoveAllTransactions();

//            if (success)
//            {
//                return Json(new { success = true });
//            }
//            else
//            {
//                return Json(new { success = false });
//            }
//        }

//    }
//}