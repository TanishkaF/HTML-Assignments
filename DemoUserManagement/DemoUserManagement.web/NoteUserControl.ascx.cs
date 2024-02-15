using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace DemoUserManagement.web
{
    public partial class NoteUserControl : System.Web.UI.UserControl
    {

        public int ObjectID;
        public int ObjectType;
        public int ObjectId
        {
            get { return ObjectID; }
            set { ObjectID = value; }
        }

        public int ObjectTypes
        {
            get { return ObjectType; }
            set {  ObjectType = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int objectID = this.ObjectID;
                int objectType = this.ObjectType;

                ViewState["ObjectID"] = objectID;
                ViewState["ObjectType"] = objectType;

                if (objectID != 0)
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

            int currentPageIndex = GridViewDocuments.PageIndex;
            int pageSize = GridViewDocuments.PageSize;

            //string studentIDString = Request.QueryString["StudentID"];
            int objectID = ViewState["ObjectID"] != null ? (int)ViewState["ObjectID"] : 0;
            int studentID = Convert.ToInt32(objectID);

            GridViewDocuments.VirtualItemCount = NoteUserControlBusiness.GetTotalNotesCount(studentID); 

            DataTable dt = NoteUserControlBusiness.GetAllNotesData(sortExpression, sortDirection, currentPageIndex, pageSize, studentID);
            GridViewDocuments.DataSource = dt;
            GridViewDocuments.DataBind();
        }

        protected void GridViewDocuments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewDocuments.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void GridViewDocuments_Sorting(object sender, GridViewSortEventArgs e)
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
            string noteData = txtNote.Text;

            int objectID = ViewState["ObjectID"] != null ? (int)ViewState["ObjectID"] : 0;
            int objectType = ViewState["ObjectType"] != null ? (int)ViewState["ObjectType"] : 0;

            NoteViewModel note = new NoteViewModel
            {
                ObjectID = objectID,
                ObjectType = objectType,
                NoteText = noteData
            };

            NoteUserControlBusiness.InsertNote(note);

            txtNote.Text = "";
            BindGridView();
        }

    }
}