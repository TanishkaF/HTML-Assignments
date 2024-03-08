using System;
using System.Collections.Generic;
using System.Data;
using AirportFuelManagement.DataAccessLayer;
using AirportFuelManagement.UtilityLayer;
using AirportFuelManagement.ViewModel;
using static AirportFuelManagement.ViewModel.AllViewModel;

public class AirportBL
{
    public static List<AllViewModel.Airport> GetAirportList()
    {
        try
        {
            if (AirportDAL.GetAirportList().Count > 0)
            {
                return AirportDAL.GetAirportList();
            }
            return null;
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return null;
        }
    }

    public static bool AddAirport(AllViewModel.Airport airport)
    {
        try
        {
            if (AirportDAL.AddAirport(airport))
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return false;
        }
    }

    public static List<AllViewModel.Airport> GetAllAirports(string sortExpression, string sortDirection, int startRowIndex, int pageSize)
    {
        try
        {
            DataTable dt = AirportDAL.GetAllAirports(sortExpression, sortDirection, startRowIndex, pageSize);
            List<AllViewModel.Airport> convertedListAirports = dt.ToListFromDataTable<AllViewModel.Airport>();
            return convertedListAirports;
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return null;
        }
    } 

    public static int GetTotalAirportCount()
    {
        try
        {
            return AirportDAL.GetTotalAirportCount();
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return -1;
        }
    }

    public static List<AllViewModel.Aircraft> GetAircraftList()
    {
        try
        {
            if (AirportDAL.GetAircraftList().Count > 0)
            {
                return AirportDAL.GetAircraftList();
            }
            return null;
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return null;
        }
    }

    public static bool AddAircraft(AllViewModel.Aircraft aircraft)
    {
        try
        {

            if (AirportDAL.AddAircraft(aircraft))
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return false;
        }
    }

    public static List<AllViewModel.Aircraft> GetAllAircrafts(string sortExpression, string sortDirection, int startRowIndex, int pageSize)
    {
        try
        {
            DataTable dt = AirportDAL.GetAllAircraft(sortExpression, sortDirection, startRowIndex, pageSize);
            List<AllViewModel.Aircraft> convertedListAircrafts = dt.ToListFromDataTable<AllViewModel.Aircraft>();
            return convertedListAircrafts;
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return null;
        }
    }

    public static int GetTotalAircraftCount()
    {
        try
        {
            return AirportDAL.GetTotalAircraftCount();
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return -1;
        }
    }

    public static List<AllViewModel.FuelTransaction> GetAllTransactions(string sortExpression, string sortDirection, int startRowIndex, int pageSize)
    {
        try
        {
            DataTable dt = AirportDAL.GetAllTransactions(sortExpression, sortDirection, startRowIndex, pageSize);
            List<AllViewModel.FuelTransaction> convertedListTransactions = dt.ToListFromDataTable<AllViewModel.FuelTransaction>();
            return convertedListTransactions;
            //return ConvertDataTableToTransactionList(dt);
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return new List<AllViewModel.FuelTransaction>();
        }
    }

    public static bool AddTransaction(AllViewModel.FuelTransaction viewModelTransaction)
    {
        try
        {
            if (AirportDAL.AddTransaction(viewModelTransaction))
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return false;
        }
    }

    public static AllViewModel.FuelTransaction GetTransactionDetails(int transactionID)
    {
        return AirportDAL.GetTransactionDetails(transactionID);
    }

    public static int GetTotalTransactionCount()
    {
        try
        {
            return AirportDAL.GetTotalTransactionCount();
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return -1;
        }
    }

    public static bool RemoveAllTransactions()
    {
        try
        {
            return AirportDAL.RemoveAllTransactions();
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            return false;
        }
    }

    public static List<AirportFuelSummary> GetAllFuelSummary(string sortExpression, string sortDirection, int startRowIndex, int pageSize)
    {
        try
        {
            return AirportDAL.GetAirportFuelSummary(sortExpression, sortDirection, startRowIndex, pageSize);
        }
        catch (Exception ex)
        {
            Logger.AddData(ex);
            throw;
        }
    }

    public static List<FuelConsumptionReportItem> GetFuelConsumptionReport()
    {
        return AirportDAL.GetFuelConsumptionReport();
    }
}

internal static class DataTableExtensions
{
    public static List<T> ToListFromDataTable<T>(this DataTable dataTable) where T : new()
    {
        List<T> list = new List<T>();

        foreach (DataRow row in dataTable.Rows)
        {
            T obj = new T();
            foreach (DataColumn column in dataTable.Columns)
            {
                var property = typeof(T).GetProperty(column.ColumnName);

                if (property != null && row[column] != DBNull.Value)
                {
                    Type propertyType = property.PropertyType;

                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        propertyType = Nullable.GetUnderlyingType(propertyType);
                    }

                    try
                    {
                        property.SetValue(obj, Convert.ChangeType(row[column], propertyType));
                    }
                    catch (Exception ex)
                    {
                        Logger.AddData(ex);
                    }
                }
            }

            list.Add(obj);
        }

        return list;
    }
}

//private static List<AllViewModel.Airport> ConvertDataTableToAirportList(DataTable dt)
//{
//    List<AllViewModel.Airport> airportList = new List<AllViewModel.Airport>();

//    foreach (DataRow row in dt.Rows)
//    {
//        var address = new AllViewModel.Airport
//        {
//            AirportUID = Convert.ToInt32(row["AirportUID"]),
//            AirportID = row["AirportID"].ToString(),
//            AirportName = row["AirportName"].ToString(),
//            FuelCapacity = row["FuelCapacity"] != DBNull.Value ? Convert.ToDecimal(row["FuelCapacity"]) : (decimal?)null,
//            FuelAvailable = row["FuelAvailable"] != DBNull.Value ? Convert.ToDecimal(row["FuelAvailable"]) : (decimal?)null
//        };
//        airportList.Add(address);
//    }

//    return airportList;
//}

//private static List<AllViewModel.FuelTransaction> ConvertDataTableToTransactionList(DataTable dt)
//{
//    List<AllViewModel.FuelTransaction> list = new List<AllViewModel.FuelTransaction>();

//    foreach (DataRow row in dt.Rows)
//    {
//        try
//        {
//            var transaction = new AllViewModel.FuelTransaction
//            {
//                TransactionID = Convert.ToInt32(row["TransactionID"]),
//                TimeStamp = Convert.ToDateTime(row["TimeStamp"]),
//                TransactionType = Convert.ToInt32(row["TransactionType"]),
//                //AirportID = row["AirportID"].ToString(),
//                AirportName = row["AirportName"].ToString(),
//                AircraftID = row["AircraftID"].ToString(),
//                Quantity = Convert.ToDecimal(row["Quantity"]),
//                TransactionIDParent = row.IsNull("TransactionIDParent") ? null : (int?)Convert.ToInt32(row["TransactionIDParent"])
//            };
//            list.Add(transaction);
//        }
//        catch (Exception ex)
//        {
//            Logger.AddData(ex);
//        }
//    }

//    return list;
//}

//private static List<AllViewModel.Aircraft> ConvertDataTableToAircraftList(DataTable dt)
//{
//    List<AllViewModel.Aircraft> list = new List<AllViewModel.Aircraft>();

//    foreach (DataRow row in dt.Rows)
//    {
//        var aircraft = new AllViewModel.Aircraft
//        {
//            AircraftUID = Convert.ToInt32(row["AircraftUID"]),
//            AircraftID = row["AircraftID"].ToString(),
//            AircraftNumber = row["AircraftNumber"].ToString(),
//            AirLine = row["AirLine"].ToString(),
//            Source = row["Source"].ToString(),
//            Destination = row["Destination"].ToString()
//        };
//        list.Add(aircraft);
//    }

//    return list;
//}