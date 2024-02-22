using DemoUserManagement.DataAccessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using System.Collections.Generic;
using System.Data;


namespace DemoUserManagement.BusinessLayer
{
    public class NoteUserControlBusiness
    {
        public static DataTable GetAllNotesData(string sortExpression, string sortDirection, int startRowIndex, int pageSize,int studentID)
        {
            return NoteUserControlDataAcess.GetAllNotesData(sortExpression, sortDirection, startRowIndex, pageSize,studentID);
        }
     
        public static int GetTotalNotesCount(int studentID)
        {
            return NoteUserControlDataAcess.GetTotalNotesCount(studentID);
        }

        public static void InsertNote(NoteViewModel note)
        {
            NoteUserControlDataAcess.InsertNote(note);
        } 
        
        public static DataTable GetAllDocumentData(string sortExpression, string sortDirection, int startRowIndex, int pageSize,int studentID)
        {
            return NoteUserControlDataAcess.GetAllDocumentData(sortExpression, sortDirection, startRowIndex, pageSize,studentID);
        }
     
        public static int GetTotalDocumentCount(int studentID)
        {
            return NoteUserControlDataAcess.GetTotalDocumentCount(studentID);
        }

        public static void InsertDocument(DocumentViewModel document)
        {
            NoteUserControlDataAcess.InsertDocument(document);
        }

        //public static List<DocumentClass> PopulateDocumentDropList(List<DocumentClass> studentDocumentList)
        //{
        //    return studentDocumentList;
        //}

        public static List<DocumentClass> PopulateDocument()
        {
            return StudentDocumentType.studentDocument;
        }

        public static string GetDocumentUniqueNameById(int documentID)
        {
            return NoteUserControlDataAcess.GetDocumentUniqueNameById(documentID);
        }

        public static List<int> GetDocumentIDsByObjectID(int objectID)
        {
            return NoteUserControlDataAcess.GetDocumentIDsByObjectID(objectID);
        }




    }
}
