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
                if (Request.QueryString["Refresh"] == "1")
                {
                    BindGridView();
                }
                else
                {
                    // If no query parameter is present, bind the GridView without any filters
                    BindAllUsersGridView();
                }
            }
        }

        private void BindAllUsersGridView()
        {
            string sortExpression = ViewState["SortExpression"]?.ToString() ?? "StudentID"; // Default sorting by StudentID
            string sortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";
            int currentPageIndex = GridViewUsers.PageIndex;
            int pageSize = GridViewUsers.PageSize;

            // Get total count of users
            int totalCount = UserListBusiness.GetTotalCount();

            // Calculate the total number of pages
            int pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

            // Ensure currentPageIndex doesn't exceed the actual number of pages
            if (currentPageIndex >= pageCount)
            {
                // If currentPageIndex exceeds the number of pages, reset to the last page
                currentPageIndex = pageCount - 1;
                GridViewUsers.PageIndex = currentPageIndex;
            }

            // Set the total count for proper pagination
            GridViewUsers.VirtualItemCount = totalCount;

            // Calculate startRowIndex
            int startRowIndex = currentPageIndex * pageSize;

            // Retrieve data based on current page index and page size
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
                BindGridView(); // Rebind the GridView to reflect updated data
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
