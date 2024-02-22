using DemoUserManagement.BusinessLayer;
using DemoUserManagement.UtilityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace DemoUserManagement.web
{
    public partial class NoteUserControlV2 : System.Web.UI.UserControl
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
            set { ObjectType = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               int objectID = this.ObjectID;
               int objectType = this.ObjectType;

                if (objectID != 0)
                {
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ToggleNoteControls", "toggleNoteControls(true);", true);
                    ViewState["SortDirection"] = "ASC";
                    ViewState["SortExpression"] = "NoteID";                 
                    BindGridView();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ToggleNoteControls", "toggleNoteControls(false);", true);
                    BindGridView();
                }
            }
        }



        private void BindGridView()
        {
            string sortExpression = ViewState["SortExpression"]?.ToString() ?? "NoteID";
            string sortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";

            int currentPageIndex = GridViewDocuments.PageIndex;
            int pageSize = GridViewDocuments.PageSize;

            int objectID = ObjectID;
            int objectType = ObjectType;

            if (objectID>0)
            {
                // Conversion successful, proceed with the code
                GridViewDocuments.VirtualItemCount = NoteUserControlBusiness.GetTotalNotesCount(objectID);

                DataTable dt = NoteUserControlBusiness.GetAllNotesData(sortExpression, sortDirection, currentPageIndex, pageSize, objectID);
                GridViewDocuments.DataSource = dt;
                GridViewDocuments.DataBind();
            }
            else
            {
                // Handle the case where conversion fails, perhaps log an error or display a message
                // Example: Log.Error("Invalid ObjectID: " + objectIDString);
            }
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

       }
}
