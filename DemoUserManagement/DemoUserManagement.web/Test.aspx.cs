using DemoUserManagement.BusinessLayer;
using DemoUserManagement.UtilityLayer;
using System;

namespace DemoUserManagement.web
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int lastInsertedUserId = UserBusiness.GetLastInsertedUserID();
            DocumentUserControl.ObjectId = lastInsertedUserId;
            DocumentUserControl.ObjectType = StudentDocumentType.ObjectType;
            DocumentUserControl.DropDownList = StudentDocumentType.studentDocument;
        }       
    }
}
