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
                ViewState["SortDirection"] = "ASC";
                ViewState["SortExpression"] = "StudentID";
                BindGridView();
            }
        }

        private void BindGridView()
        {
            string sortExpression = ViewState["SortExpression"]?.ToString() ?? "StudentID";
            string sortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";

            int currentPageIndex = GridViewUsers.PageIndex;
            int pageSize = GridViewUsers.PageSize;          

            GridViewUsers.VirtualItemCount = UserListBusiness.GetTotalUserCount();              

            DataTable dt = UserListBusiness.GetAllUserListData(sortExpression, sortDirection, currentPageIndex, pageSize);
            GridViewUsers.DataSource = dt;
            GridViewUsers.DataBind();
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
      
    }
}
