using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApplicationDemo.Models;
using MVCBusinessLayer;
using MVCUtilityLayer;

namespace MVCApplicationDemo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Get()
        {
            return View();
        }

        [HttpPost]
        [Route("Employee/Add")]

        public ActionResult Add(EmployeeViewModel newEmployee)
        {
            if (ModelState.IsValid)
            {
                MVCBusinessLayer.MVCBusinessLayer.AddEmployee(newEmployee);

                int newEmployeeID = newEmployee.EmployeeID;

                return RedirectToAction("GetSpecificEmployee", new { employeeID = newEmployeeID });
            }
            return View(newEmployee);
        }

        [HttpPost]
        [Route("Employee/Edit")]

        public ActionResult Edit(EmployeeViewModel newEmployee)
        {
            if (ModelState.IsValid)
            {
                MVCBusinessLayer.MVCBusinessLayer.UpdateEmployee(newEmployee);

                int newEmployeeID = newEmployee.EmployeeID;

                return RedirectToAction("GetSpecificEmployee", new { employeeID = newEmployeeID });
            }
            return View(newEmployee);
        }

        [HttpPost]
        [Route("Employee/Delete")]
        public ActionResult Delete(int employeeID)
        {
            if (ModelState.IsValid)
            {
                MVCBusinessLayer.MVCBusinessLayer.DeleteEmployee(employeeID);
            }
            return View("Details");
        }

        [HttpGet]
        [Route("Employee/GetAll")]
        public ActionResult GetAll()
        {
            List<EmployeeViewModel> employees = MVCBusinessLayer.MVCBusinessLayer.GetAllEmployees();
            return View("Get", employees);
        }



        public ActionResult GetSpecificEmployee(int employeeID)
        {
            EmployeeViewModel employee = MVCBusinessLayer.MVCBusinessLayer.ReadEmployee(employeeID);

            if (employee == null || employeeID == 0)
            {
                return HttpNotFound();
            }

            return View("Details", employee);
        }
    }
}
