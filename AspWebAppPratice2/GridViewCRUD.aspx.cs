using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace AspWebAppPratice2
{
    public partial class GridViewCRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void InsertButton(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GridViewConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Student (FirstName, LastName) VALUES (@FirstName, @LastName)";
                cmd.Parameters.AddWithValue("@FirstName", TextBox1.Text);
                cmd.Parameters.AddWithValue("@LastName", TextBox2.Text);
                cmd.ExecuteNonQuery();
                Response.Write("data inserted successfully");
                Response.Redirect("GridView.aspx");
            }
        }

        protected void ResetButton(object sender, EventArgs e)
        {

            TextBox1.Text = "";
            TextBox2.Text = "";
        }

        protected void SortingGridView(object sender, GridViewSortEventArgs e)
        {
            string sortDirection = ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : string.Empty;
            
            if (string.IsNullOrEmpty(sortDirection))
            {
                sortDirection = "ASC";
            }

            sortDirection = (sortDirection == "ASC") ? "DESC" : "ASC";
            ViewState["SortDirection"] = sortDirection;

            SqlDataSource1.SelectCommand = "SELECT * FROM [Student]";
            if (!string.IsNullOrEmpty(e.SortExpression))
            {
                SqlDataSource1.SelectCommand += " ORDER BY " + e.SortExpression + " " + sortDirection;
            }

            GridView1.DataBind();
        }

    }
}
