using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System;
using DemoUserManagement.UtilityLayer;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections.Generic;
using System.Web;


namespace DemoUserManagement.web
{
    public partial class DocumentUserControl : System.Web.UI.UserControl
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
                ViewState["ObjectID"] = this.ObjectID;
                ViewState["ObjectType"] = this.ObjectType;

                PopulateDropdown(DropDownList);
                BindGridView();
                if (ObjectID != 0)
                {
                    ddlOptions.Visible = true;
                    fileUpload.Visible = true;
                    lblFileUpload.Visible = true;
                    lblDdlOptions.Visible = true;
                    //btnAddDocument.Visible = true;

                    ViewState["SortDirection"] = "ASC";
                    ViewState["SortExpression"] = "DocumentID";
                }
                else
                {
                    lblFileUpload.Visible = false;
                    lblDdlOptions.Visible = false;
                    ddlOptions.Visible = false;
                    fileUpload.Visible = false;
                    btnAddDocument.Visible = false;
                }
            }
        }

        public void PopulateDropdown(List<DocumentClass> documentList)
        {
            if (ddlOptions != null && documentList != null)
            {
                ddlOptions.Items.Clear();
                foreach (DocumentClass docClass in documentList)
                {
                    ddlOptions.Items.Add(new ListItem(docClass.documentName, docClass.documentType.ToString()));
                }
            }
            DropDownList = documentList;
        }


        public int GetDocumentType(string documentName)
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

        protected void BtnAddDocument_Click(object sender, EventArgs e)
        {
            UploadFile uploadFileHandler = new UploadFile();
            if (fileUpload.HasFile)
            {
                try
                {
                    string uploadedFileName = uploadFileHandler.UploadFileToServer(fileUpload.PostedFile);

                    if (!string.IsNullOrEmpty(uploadedFileName))
                    {
                        int objectID = ViewState["ObjectID"] != null ? (int)ViewState["ObjectID"] : 0;
                        int objectType = ViewState["ObjectType"] != null ? (int)ViewState["ObjectType"] : 0;
                        int documentType = Convert.ToInt32(ddlOptions.SelectedValue);

                        DocumentViewModel document = new DocumentViewModel
                        {
                            ObjectID = objectID,
                            ObjectType = objectType,
                            DocumentType = documentType,
                            DiskDocumentName = uploadedFileName,
                            OriginalDocumentName = fileUpload.FileName
                        };

                        NoteUserControlBusiness.InsertDocument(document);
                        BindGridView();
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
        }

        //protected void BtnAddDocument_Click(object sender, EventArgs e)
        //{
        //    if (fileUpload.HasFile)
        //    {
        //        try
        //        {
        //            string uploadFolderPath = ConfigurationManager.AppSettings["UploadFolderPath"];

        //            string fileName = Path.GetFileName(fileUpload.FileName);

        //            string physicalUploadFolderPath = uploadFolderPath;

        //            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

        //            string filePath = Path.Combine(physicalUploadFolderPath, uniqueFileName);

        //            fileUpload.SaveAs(filePath);

        //            int objectID = ViewState["ObjectID"] != null ? (int)ViewState["ObjectID"] : 0;
        //            int objectType = ViewState["ObjectType"] != null ? (int)ViewState["ObjectType"] : 0;

        //            int documentType = Convert.ToInt32(ddlOptions.SelectedValue);

        //            DocumentViewModel document = new DocumentViewModel
        //            {
        //                ObjectID = objectID,
        //                ObjectType = objectType,
        //                DocumentType = documentType,
        //                DiskDocumentName = uniqueFileName,
        //                OriginalDocumentName = fileName
        //            };

        //            NoteUserControlBusiness.InsertDocument(document);
        //            BindGridView();
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.AddData(ex);

        //        }
        //    }
        //}


        private void BindGridView()
        {
            string sortExpression = ViewState["SortExpression"]?.ToString() ?? "DocumentID";
            string sortDirection = ViewState["SortDirection"]?.ToString() ?? "ASC";

            int currentPageIndex = GridViewDocuments.PageIndex;
            int pageSize = GridViewDocuments.PageSize;

            int objectID = ViewState["ObjectID"] != null ? (int)ViewState["ObjectID"] : 0;
            int objectType = ViewState["ObjectType"] != null ? (int)ViewState["ObjectType"] : 0;

            GridViewDocuments.VirtualItemCount = NoteUserControlBusiness.GetTotalDocumentCount(objectID, objectType);

            DataTable dt = NoteUserControlBusiness.GetAllDocumentData(sortExpression, sortDirection, currentPageIndex, pageSize, objectID, objectType);
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

        protected void Button_Click(object sender, EventArgs e)
        {
            LinkButton downloadLink = (LinkButton)sender;
            string fileName = downloadLink.CommandArgument;

            string getFileUrl = $"{Request.Url.Scheme}://{Request.Url.Authority}/GetFile.ashx?fileName={HttpUtility.UrlEncode(fileName)}";

            string script = $@"<script type='text/javascript'>
                         window.location.href = '{getFileUrl}';
                     </script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DownloadScript", script);

            // UpdatePanelGridViewDocuments.Update();

        }



    }
}
