using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DemoUserManagement.UtilityLayer;

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

            GridViewUsers.VirtualItemCount = GetTotalCount(); // Update this method to get the total count of users

            int startRowIndex = currentPageIndex * pageSize + 1;
            int endRowIndex = startRowIndex + pageSize - 1;

            string query = $@"SELECT 
                        StudentDetails.StudentID,
                        StudentDetails.FirstName,
                        StudentDetails.LastName,
                        StudentDetails.Phone,
                        StudentDetails.AadharNumber,
                        AddressDetails.Country
                  FROM 
                        StudentDetails 
                  INNER JOIN 
                        AddressDetails ON StudentDetails.StudentID = AddressDetails.UserID
                  ORDER BY 
                        {sortExpression} {sortDirection}
                  OFFSET 
                        (@StartRowIndex - 1) ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    try
                    {
                        con.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            GridViewUsers.DataSource = dt;
                            GridViewUsers.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {

                        Logger.AddData(ex);
                    }
                }
            }
        }



        private void BindGridView()
        {
            string sortExpression = ViewState["SortExpression"]?.ToString() ?? "StudentID"; // Default sorting by StudentID
            string sortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";
            int currentPageIndex = GridViewUsers.PageIndex;
            int pageSize = GridViewUsers.PageSize;
            int totalCount = GetTotalCount(); // Update this method to get the total count of users

            GridViewUsers.VirtualItemCount = totalCount;

            int startRowIndex = currentPageIndex * pageSize + 1;
            int endRowIndex = startRowIndex + pageSize - 1;

            string query = $@"SELECT 
                            StudentDetails.StudentID,
                            StudentDetails.FirstName,
                            StudentDetails.LastName,
                            StudentDetails.Phone,
                            StudentDetails.AadharNumber,
                            AddressDetails.Country
                      FROM 
                            StudentDetails 
                      INNER JOIN 
                            AddressDetails ON StudentDetails.StudentID = AddressDetails.UserID
                      WHERE 
                            AddressDetails.AddressType = 1 -- Assuming 1 represents the current address
                      ORDER BY 
                            {sortExpression} {sortDirection}
                      OFFSET 
                            (@StartRowIndex - 1) ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    try
                    {
                        con.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            GridViewUsers.DataSource = dt;
                            GridViewUsers.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.AddData(ex);
                    }
                }
            }
        }


        private int GetTotalCount()
        {
            int totalCount = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM StudentDetails " +
                               "INNER JOIN AddressDetails ON StudentDetails.StudentID = AddressDetails.UserID " +
                               "WHERE AddressDetails.AddressType = 1"; // Assuming 1 represents the current address

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        totalCount = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Logger.AddData(ex);
                    }
                }
            }

            return totalCount;
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