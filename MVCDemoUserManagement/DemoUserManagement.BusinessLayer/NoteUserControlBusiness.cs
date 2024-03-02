using DemoUserManagement.DataAccessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;


namespace DemoUserManagement.BusinessLayer
{
    public class NoteUserControlBusiness
    {
        public static DataTable GetAllNotesData(string sortExpression, string sortDirection, int startRowIndex, int pageSize,int objectID, int objectType)
        {
            return NoteUserControlDataAcess.GetAllNotesData(sortExpression, sortDirection, startRowIndex, pageSize, objectID, objectType);
        }
     
        public static int GetTotalNotesCount(int objectID,int objectType)
        {
            return NoteUserControlDataAcess.GetTotalNotesCount(objectID, objectType);
        }

        public static void InsertNote(NoteViewModel note)
        {
            NoteUserControlDataAcess.InsertNote(note);
        } 
        
        public static DataTable GetAllDocumentData(string sortExpression, string sortDirection, int startRowIndex, int pageSize,int objectID,int objectType)
        {
            return NoteUserControlDataAcess.GetAllDocumentData(sortExpression, sortDirection, startRowIndex, pageSize, objectID, objectType);
        }
     
        public static int GetTotalDocumentCount(int objectID,int objectType)
        {
            return NoteUserControlDataAcess.GetTotalDocumentCount(objectID,objectType);
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

        public static List<DocumentClass> GetDocumentType()
        {
            return StudentDocumentType.studentDocument;
        }

        public static  List<DocumentViewModel> ConvertDataTableToList(DataTable dt)
        {
            var list = new List<DocumentViewModel>();

            foreach (DataRow row in dt.Rows)
            {
                var document = new DocumentViewModel
                {
                    DocumentID = Convert.ToInt32(row["DocumentID"]),
                    ObjectID = Convert.ToInt32(row["ObjectID"]),
                    ObjectType = Convert.ToInt32(row["ObjectType"]),
                    DocumentType = Convert.ToInt32(row["DocumentType"]),
                    DiskDocumentName = row["DiskDocumentName"].ToString(),
                    OriginalDocumentName = row["OriginalDocumentName"].ToString(),
                    Timestamp = Convert.ToDateTime(row["TimeStamp"])
                };
                list.Add(document);
            }

            return list;
        }

    }
}