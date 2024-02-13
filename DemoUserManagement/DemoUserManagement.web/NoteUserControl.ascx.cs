using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace DemoUserManagement.web
{
    public partial class NoteUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string studentID = Request.QueryString["StudentID"];

                if (!string.IsNullOrEmpty(studentID))
                {
                    txtNote.Visible = true;
                    btnAddNote.Visible = true;
                    ViewState["SortDirection"] = "ASC";
                    ViewState["SortExpression"] = "NoteID";
                    BindGridView();
                }
                else
                {
                    txtNote.Visible = false;
                    btnAddNote.Visible = false;
                }
            }
        }

        private void BindGridView()
        {
            string sortExpression = ViewState["SortExpression"]?.ToString() ?? "NoteID";
            string sortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";

            int currentPageIndex = GridViewNotes.PageIndex;
            int pageSize = GridViewNotes.PageSize;

            string studentIDString = Request.QueryString["StudentID"];
            int studentID = Convert.ToInt32(studentIDString);

            GridViewNotes.VirtualItemCount = NoteUserControlBusiness.GetTotalCount(studentID); 

            DataTable dt = NoteUserControlBusiness.GetAllNotesData(sortExpression, sortDirection, currentPageIndex, pageSize, studentID);
            GridViewNotes.DataSource = dt;
            GridViewNotes.DataBind();
        }

        protected void GridViewNotes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewNotes.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void GridViewNotes_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void BtnAddNote_Click(object sender, EventArgs e)
        {
            string studentID = Request.QueryString["StudentID"];
            string noteData = txtNote.Text;

            NoteViewModel note = new NoteViewModel
            {
                ObjectID = int.Parse(studentID),
                NoteText = noteData
            };

            NoteUserControlBusiness.InsertNote(note);
            txtNote.Text = "";
            BindGridView();
        }

    }
}