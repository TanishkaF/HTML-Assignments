using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System;
using DemoUserManagement.UtilityLayer;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace DemoUserManagement.web
{
    public partial class DocumentUserControlV2 : System.Web.UI.UserControl
    {
        public int ObjectID;
        public int ObjectType;
        public List<DocumentClass> DropDownList { get; set; }

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

                if (ObjectID != 0)
                {
                    ViewState["SortDirection"] = "ASC";
                    ViewState["SortExpression"] = "DocumentID";
                    BindGridView();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ToggleNoteControls", "toggleNoteControls(false);", true);
                    BindGridView();
                }
            }
        }


        public int GetDocumentType(string documentName)
        {
            try
            {
                foreach (DocumentClass doc in DropDownList)
                {
                    if (doc.documentName.Equals(documentName, StringComparison.OrdinalIgnoreCase))
                    {
                        return doc.documentType;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return -1; 
            }
        }

        protected void GridViewDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DownloadFile")
                {
                    int documentId = Convert.ToInt32(e.CommandArgument);
                    int userId = ObjectID;
                    string url = $"GetFile.ashx?documentID={documentId}";
                    Response.Redirect(url);
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
            }
        }



        private void BindGridView()
        {
            try
            {
                string sortExpression = ViewState["SortExpression"]?.ToString() ?? "DocumentID";
                string sortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";

                int currentPageIndex = GridViewDocuments.PageIndex;
                int pageSize = GridViewDocuments.PageSize;

                int objectID = ObjectID;

                if (objectID > 0)
                {
                    GridViewDocuments.VirtualItemCount = NoteUserControlBusiness.GetTotalDocumentCount(objectID);

                    DataTable dt = NoteUserControlBusiness.GetAllDocumentData(sortExpression, sortDirection, currentPageIndex, pageSize, objectID);
                    GridViewDocuments.DataSource = dt;
                    GridViewDocuments.DataBind();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
            }
        }

        protected void GridViewDocuments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridViewDocuments.PageIndex = e.NewPageIndex;
                BindGridView();
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
            }
        }

        protected void GridViewDocuments_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Logger.AddData(ex);
            }
        }
        
    }
}