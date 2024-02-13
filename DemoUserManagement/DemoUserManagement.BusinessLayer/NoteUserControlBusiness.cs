using DemoUserManagement.DataAccessLayer;
using DemoUserManagement.ViewModel;
using System.Data;


namespace DemoUserManagement.BusinessLayer
{
    public class NoteUserControlBusiness
    {
        public static DataTable GetAllNotesData(string sortExpression, string sortDirection, int startRowIndex, int pageSize,int studentID)
        {
            return NoteUserControlDataAcess.GetAllNotesData(sortExpression, sortDirection, startRowIndex, pageSize,studentID);
        }
     
        public static int GetTotalCount(int studentID)
        {
            return NoteUserControlDataAcess.GetTotalNotesCount(studentID);
        }

        public static void InsertNote(NoteViewModel note)
        {
            NoteUserControlDataAcess.InsertNote(note);
        }
    }
}
