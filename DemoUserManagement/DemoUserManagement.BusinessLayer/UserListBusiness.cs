using DemoUserManagement.DataAccessLayer;
using System.Data;

namespace DemoUserManagement.BusinessLayer
{
    public static class UserListBusiness
    {

        public static DataTable GetAllUserListData(string sortExpression, string sortDirection, int startRowIndex, int pageSize)
        {
            return UserListDataAccess.GetAllUserListData(sortExpression, sortDirection, startRowIndex, pageSize);
        }

        public static int GetTotalUserCount()
        {
            return UserListDataAccess.GetTotalUserCount();
        }

    }
}
