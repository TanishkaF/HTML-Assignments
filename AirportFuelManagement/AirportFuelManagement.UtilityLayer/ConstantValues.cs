using AirportFuelManagement.ViewModel;
using System.Web;

namespace AirportFuelManagement.UtilityLayer
{
    public class ConstantValues
    {
        public static AllViewModel.LogInSessionModel GetUserSessionInfo()
        {
            return HttpContext.Current.Session["UserSessionInfo"] as AllViewModel.LogInSessionModel;
        }

        public static void SetUserSessionInfo(AllViewModel.LogInSessionModel userSessionInfo)
        {
           HttpContext.Current.Session["UserSessionInfo"] = userSessionInfo;
        }
    }

    public struct TransactionType
    {
        public const int In = 1;
        public const int Out = 2;
    }

    public struct AirportConstants
    {
        public const int PageIndex = 0;
        public const int PageSize = 5;
        public const string SortExpression = "AirportName";
        public const string SortDirection = "ASC";
    } 

    public struct AircraftConstants
    {
        public const int PageIndex = 0;
        public const int PageSize = 15;
        public const string SortExpression = "AircraftNumber";
        public const string SortDirection = "ASC";
    }

    public struct TransactionConstants
    {
        public const int PageIndex = 0;
        public const int PageSize = 5;
        public const string SortExpression = "TimeStamp";
        public const string SortDirection = "DESC";
    }    
    
    public struct AirportFuelConstants
    {
        public const int PageIndex = 0;
        public const int PageSize = 3;
        public const string SortExpression = "FuelAvailable";
        public const string SortDirection = "DESC";
    }

}
