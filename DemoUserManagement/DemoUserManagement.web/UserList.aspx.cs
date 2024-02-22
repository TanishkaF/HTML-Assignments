using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DemoUserManagement.BusinessLayer;

namespace DemoUserManagement.web
{
    public partial class UserList : BasePage
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

        protected void Button_Click(object sender, EventArgs e)
        {
            LinkButton downloadLink = (LinkButton)sender;
            string fileName = downloadLink.CommandArgument;

            string getFileUrl = $"{Request.Url.Scheme}://{Request.Url.Authority}/GetFile.ashx?fileName={HttpUtility.UrlEncode(fileName)}";

            string script = $@"<script type='text/javascript'>
                         window.open('{getFileUrl}', '_blank');
                     </script>";
            HttpContext.Current.Response.Write(script);
        }





        //protected void Button_Click(object sender, EventArgs e)
        //{
        //    LinkButton downloadLink = (LinkButton)sender;
        //    string fileName = downloadLink.CommandArgument; 
        //    string folderPath = ConfigurationManager.AppSettings["UploadFolderPath"];
        //    string filePath = Path.Combine(folderPath, fileName);

        //    FileInfo file = new FileInfo(filePath);
        //    if (file.Exists)
        //    {
        //        Response.Clear();
        //        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
        //        Response.AddHeader("Content-Length", file.Length.ToString());
        //        Response.ContentType = GetMimeType(file.Extension);
        //        Response.Flush();
        //        Response.TransmitFile(file.FullName);
        //        Response.End();
        //    }
        //    else
        //    {
        //        string script = $@"<script type='text/javascript'>
        //                    window.open('{filePath}', '_blank');
        //                  </script>";
        //        ClientScript.RegisterStartupScript(this.GetType(), "OpenFileInNewTab", script);
        //    }

        //}

        private string GetMimeType(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".txt":
                    return "text/plain";
                case ".pdf":
                    return "application/pdf";
                case ".doc":
                    return "application/msword";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                default:
                    return "application/octet-stream";
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

        protected void BtnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("userDetails.aspx");
        }

        protected void GridViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditUser")
            {
                int studentID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"UserDetailsV2.aspx?StudentID={studentID}");
            }
            else if (e.CommandName == "RefreshGrid")
            {
                BindGridView();
            }
        }
      
    }
}
