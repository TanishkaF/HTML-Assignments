using DemoUserManagement.DataAccessLayer;
using System.Data;

namespace DemoUserManagement.BusinessLayer
{
    public static class UserListBusiness
    {
        public static DataTable GetAllUsersData(string sortExpression, string sortDirection, int currentPageIndex, int pageSize)
        {
            return UserListDataAccess.GetAllUsersData(sortExpression, sortDirection, currentPageIndex, pageSize);
        }

        public static DataTable GetFilteredUsersData(string sortExpression, string sortDirection, int currentPageIndex, int pageSize)
        {
            return UserListDataAccess.GetFilteredUsersData(sortExpression, sortDirection, currentPageIndex, pageSize);
        }

        public static int GetTotalCount()
        {
            return UserListDataAccess.GetTotalCount();
        }

    }
}
