using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DemoUserManagement.ViewModel;

namespace DemoUserManagement.UtilityLayer
{
    public class ConstantValues
    {
        public static LogInSessionModel GetUserSessionInfo()
        {
            return HttpContext.Current.Session["UserSessionInfo"] as LogInSessionModel;
        }

        public static void SetUserSessionInfo(LogInSessionModel userSessionInfo)
        {
            HttpContext.Current.Session["UserSessionInfo"] = userSessionInfo;
        }
    }


    public struct AddressType
    {
        public const int CurrentAddress = 1;
        public const int PermanentAddress = 2;
    }

    public struct EducationType
    {
        public const int MatriculationEducation = 1;
        public const int IntermediateEducation = 2;
        public const int GraduateEducation = 3;
    }

    //editing student=1
    public struct NoteType
    {
        public const int  PageSize = 10;
        public const int PageIndex = 0;
        public const string SortExpression = "NoteID";
        public const string SortDirection = "ASC";
        public const int ObjectType = 1;
    } 
    
    //editing student=1
    public struct DocumentType
    {
        public const int  PageSize = 5;
        public const int PageIndex = 0;
        public const string SortExpression = "DocumentID";
        public const string SortDirection = "ASC";
        public const int ObjectType = 1;
    }

    public struct UserListType
    {
        public const int PageSize = 10;
        public const int PageIndex = 0;
        public const string SortExpression = "StudentID";
        public const string SortDirection = "ASC";
    }

    public class StudentDocumentType
    {
        public const int ObjectType = 1;
       

        public static List<DocumentClass> studentDocument = new List<DocumentClass>
        {
            new DocumentClass { documentType = 1, documentName = "AdmitCard" }, 
            new DocumentClass { documentType = 2, documentName = "PanCard" },
            new DocumentClass { documentType = 3, documentName = "AadharCard" }
        };
    }  
}