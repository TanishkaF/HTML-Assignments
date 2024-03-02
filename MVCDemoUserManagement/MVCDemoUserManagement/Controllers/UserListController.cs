using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MVCDemoUserManagement.Controllers
{
    public class UserListController : Controller
    {
        // GET: UserList
        public ActionResult Index()
        {
            return View();
        }

        [SessionCheck]
        public ActionResult GetUsers(int pageIndex = 0, int pageSize = 10, string sortExpression = "StudentID", string sortDirection = "DESC")
        {
            string actualSortDirection = sortDirection.ToUpper() == "DESC" ? "DESC" : "ASC";

            int totalUsersCount = UserListBusiness.GetTotalUserCount();
            int totalPages = (int)Math.Ceiling((double)totalUsersCount / pageSize);

            pageIndex = Math.Max(0, Math.Min(pageIndex, totalPages - 1));

            DataTable dt = UserListBusiness.GetAllUserListData(sortExpression, actualSortDirection, pageIndex, pageSize);

            List<UserDetailsViewModel> model = ConvertDataTableToList(dt);

            foreach (var user in model)
            {
                user.PageIndex = pageIndex;
                user.TotalPages = totalPages;
            }

            ViewBag.SortExpression = sortExpression;
            ViewBag.SortDirection = sortDirection;

            return View("UserList", model);
        }

        private List<UserDetailsViewModel> ConvertDataTableToList(DataTable dt)
        {
            List<UserDetailsViewModel> list = new List<UserDetailsViewModel>();

            foreach (DataRow row in dt.Rows)
            {
                var user = new UserDetailsViewModel
                {
                    StudentID = Convert.ToInt32(row["StudentID"]),
                    FirstName = row["FirstName"].ToString(),                 
                    LastName = row["LastName"].ToString(),
                    Phone = row["Phone"].ToString(),
                    AadharNumber = row["AadharNumber"].ToString()
                };
                list.Add(user);
            }

            return list;
        }

        public ActionResult Add()
        {
            return RedirectToAction("UserDetails", "UserDetails", new { studentid = 0 });
        }
    }
}
