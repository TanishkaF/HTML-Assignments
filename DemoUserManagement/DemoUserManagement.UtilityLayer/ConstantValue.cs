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
        public const int ObjectType = 1;
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


    //public static int 

    //public  LogInSessionModel GetSessionDetails()
    //{
    // return    HttpContext.Current.Session["LoggedInUserID"] as LogInSessionModel;

    //}





}
