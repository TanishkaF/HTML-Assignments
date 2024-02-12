using DemoUserManagement.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void AddNote(string studentID, string noteData)
        {
            string noteType = "UserDetails";
            NoteUserControlDataAcess.InsertNote(studentID, noteType, noteData);
        }
    }
}
