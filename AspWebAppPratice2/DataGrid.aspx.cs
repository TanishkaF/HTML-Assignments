using System;
using System.Data.SqlClient;
using System.Data;


namespace AspWebAppPratice2
{
    public partial class DataGrid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("data source=.; database=student; integrated security=SSPI"))
            {
                // Create data adapter to fetch data from database
                SqlDataAdapter sde = new SqlDataAdapter("Select * from employee", con);

                // Create DataSet to hold the retrieved data
                DataSet ds = new DataSet();

                // Fill the DataSet with data from the database
                sde.Fill(ds);

                // Set the DataSource of the DataGrid to the filled DataSet
                DataGrid1.DataSource = ds;

                // Bind the data to the DataGrid
                DataGrid1.DataBind();
            }       
        }
    }
}