using System;
using System.Data;
using System.Web.UI.WebControls;
using DemoUserManagement.BusinessLayer;

namespace DemoUserManagement.web
{
    public partial class UserList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize ViewState["SortDirection"] and ViewState["SortExpression"] if it's the first time loading the page
                ViewState["SortDirection"] = "ASC";
                ViewState["SortExpression"] = "StudentID";

                // Load GridView data without sorting if no query parameter is present
                if (string.IsNullOrEmpty(Request.QueryString["userID"]))
                {
                    BindAllUsersGridView();
                }
                else
                {
                    string userIDString = Request.QueryString["userID"];
                    int userID;

                    if (int.TryParse(userIDString, out userID))
                    {
                        DisplayStudent(userID);
                    }
                }
                if (!string.IsNullOrEmpty(Request.QueryString["userID"]))
                {
                    string userIDValue = Request.QueryString["userID"];
                    string userIDString = Request.QueryString["userID"];
                    int userID;

                    if (int.TryParse(userIDString, out userID))
                    {
                        DisplayStudent(userID);
                    }
                }

                else
                {
                    // If no query parameter is present, bind the GridView without any filters
                    BindAllUsersGridView();
                }
            }
        }

        private void DisplayStudent(int userIDValue)
        {
            int studentID = userIDValue; // Change this to the desired student ID
            DataTable dt = UserListBusiness.GetStudentRecord(studentID);
            BindGridView(dt);
        }


        private void BindAllUsersGridView()
        {
            string sortExpression = ViewState["SortExpression"]?.ToString() ?? "StudentID"; // Default sorting by StudentID
            string sortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";
            int currentPageIndex = GridViewUsers.PageIndex;
            int pageSize = GridViewUsers.PageSize;

            GridViewUsers.VirtualItemCount = UserListBusiness.GetTotalCount(); // Update this method to get the total count of users

            int startRowIndex = currentPageIndex * pageSize + 1;
            int endRowIndex = startRowIndex + pageSize - 1;

            DataTable dt = UserListBusiness.GetAllUsersData(sortExpression, sortDirection, startRowIndex, pageSize);
            BindGridView(dt);
        }
      
        private void BindGridView(DataTable dt)
        {
            GridViewUsers.DataSource = dt;
            GridViewUsers.DataBind();
        }

        private void BindGridView()
        {
            string sortExpression = ViewState["SortExpression"]?.ToString() ?? "StudentID"; // Default sorting by StudentID
            string sortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";
            int currentPageIndex = GridViewUsers.PageIndex;
            int pageSize = GridViewUsers.PageSize;

            int userID = UserBusiness.GetLastInsertedUserID() + 1;

            // Call the business layer method with the additional parameter
            DataTable dt = UserListBusiness.GetFilteredUsersData(sortExpression, sortDirection, currentPageIndex, pageSize);
            BindGridView(dt);
        }



        protected void BtnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("userDetails.aspx");
        }

        protected void GridViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditUser")
            {
                int studentID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"UserDetails.aspx?StudentID={studentID}");
            }
            else if (e.CommandName == "RefreshGrid")
            {
                BindGridView();
            }
        }

        protected void GridViewUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUsers.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void GridViewUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = ViewState["SortDirection"].ToString();

            if (sortExpression == ViewState["SortExpression"].ToString())
            {
                sortDirection = sortDirection == "ASC" ? "DESC" : "ASC";
            }
            else
            {
                sortDirection = "ASC";
            }

            ViewState["SortExpression"] = sortExpression;
            ViewState["SortDirection"] = sortDirection;

            BindGridView();
        }
    }
}
