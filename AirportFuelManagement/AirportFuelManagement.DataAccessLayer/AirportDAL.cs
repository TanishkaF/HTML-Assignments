using AirportFuelManagement.UtilityLayer;
using AirportFuelManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using static AirportFuelManagement.ViewModel.AllViewModel;
using System.Data.Common;

namespace AirportFuelManagement.DataAccessLayer
{
    public class AirportDAL
    {
        public static List<AllViewModel.Airport> GetAirportList()
        {
            try
            {
                using (var context = new AirportDBEntities()) // Assuming AirportDBEntities is your DbContext
                {
                    return context.Airports
                        .Select(a => new AllViewModel.Airport { AirportID = a.AirportID, AirportName = a.AirportName })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return null;
            }
        }

        public static bool AddAirport(AllViewModel.Airport viewModelAirport)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    Airport dbAirport = new Airport
                    {
                        // AirportID = viewModelAirport.AirportID,
                        AirportID = viewModelAirport.AirportID,
                        AirportName = viewModelAirport.AirportName,
                        FuelCapacity = viewModelAirport.FuelCapacity,
                        FuelAvailable = viewModelAirport.FuelAvailable
                    };

                    context.Airports.Add(dbAirport);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return false;
            }
        }

        public static bool UpdateAirport(AllViewModel.Airport viewModelAirport)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    var dbAirport = context.Airports.FirstOrDefault(a => a.AirportID == viewModelAirport.AirportID);

                    if (dbAirport != null)
                    {
                        dbAirport.AirportName = viewModelAirport.AirportName;
                        dbAirport.FuelCapacity = viewModelAirport.FuelCapacity;
                        var fuelAvailable = (context.FuelTransactions
                  .Where(ft => ft.AirportID == viewModelAirport.AirportID && ft.TransactionType == 1)
                  .Sum(ft => (decimal?)ft.Quantity) ?? 0)
                  - (context.FuelTransactions
                      .Where(ft => ft.AirportID == viewModelAirport.AirportID && ft.TransactionType == 2)
                      .Sum(ft => (decimal?)ft.Quantity) ?? 0);

                        if(fuelAvailable >= dbAirport.FuelCapacity)
                        {
                            dbAirport.FuelAvailable = 0;
                        }
                        else
                        {
                            dbAirport.FuelAvailable = fuelAvailable;
                        }

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return false;
            }
        }

        public static DataTable GetAllAirports(string sortExpression, string sortDirection, int currentPageIndex, int pageSize)
        {
            DataTable dt = new DataTable();

            string query = $@"SELECT 
                                AirportUID, AirportID, AirportName, FuelCapacity, FuelAvailable
                            FROM 
                                Airport
                            ORDER BY {sortExpression} {sortDirection}
                            OFFSET @StartRowIndex ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AirportManagementConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    int startRowIndex = currentPageIndex * pageSize;
                    cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    try
                    {
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddLogException(ex);
                    }
                }
            }

            return dt;
        }

        public static int GetTotalAirportCount()
        {
            int totalCount = -1;

            try
            {
                using (var context = new AirportDBEntities())
                {
                    totalCount = context.Airports.Count();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
            }

            return totalCount;
        }

        public static AllViewModel.Airport GetAirportById(string airportId)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    // Retrieve the aircraft entity from the database
                    var dbAirport = context.Airports.FirstOrDefault(a => a.AirportID == airportId);

                    if (dbAirport != null)
                    {

                        var viewModelAircraft = new AllViewModel.Airport
                        {
                            AirportID = dbAirport.AirportID,
                            AirportName = dbAirport.AirportName,
                            FuelCapacity = dbAirport.FuelCapacity,
                            FuelAvailable = dbAirport.FuelAvailable
                        };

                        return viewModelAircraft;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return null;
            }
        }

        public static List<AllViewModel.Aircraft> GetAircraftList()
        {
            try
            {
                using (var context = new AirportDBEntities()) // Assuming AirportDBEntities is your DbContext
                {
                    return context.Aircraft
                        .Select(a => new AllViewModel.Aircraft { AircraftID = a.AircraftID })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return null;
            }
        }

        public static bool AddAircraft(AllViewModel.Aircraft viewModelAircraft)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    Aircraft dbAircraft = new Aircraft
                    {
                        AircraftID = viewModelAircraft.AircraftID,
                        AircraftNumber = viewModelAircraft.AircraftNumber,
                        AirLine = viewModelAircraft.AirLine,
                        Source = viewModelAircraft.Source,
                        Destination = viewModelAircraft.Destination
                    };

                    context.Aircraft.Add(dbAircraft);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return false;
            }
        }

        public static bool UpdateAircraft(AllViewModel.Aircraft viewModelAircraft)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    var dbAircraft = context.Aircraft.FirstOrDefault(a => a.AircraftID == viewModelAircraft.AircraftID);

                    if (dbAircraft != null)
                    {
                        dbAircraft.AircraftNumber = viewModelAircraft.AircraftNumber;
                        dbAircraft.AirLine = viewModelAircraft.AirLine;
                        dbAircraft.Source = viewModelAircraft.Source;
                        dbAircraft.Destination = viewModelAircraft.Destination;

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return false;
            }
        }

        public static DataTable GetAllAircraft(string sortExpression, string sortDirection, int currentPageIndex, int pageSize)
        {
            DataTable dt = new DataTable();

            string query = $@"SELECT 
                        AircraftUID, AircraftID, AircraftNumber, AirLine, Source, Destination
                    FROM 
                        Aircraft
                    ORDER BY {sortExpression} {sortDirection}
                    OFFSET @StartRowIndex ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AirportManagementConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    int startRowIndex = currentPageIndex * pageSize;
                    cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    try
                    {
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddLogException(ex);
                    }
                }
            }

            return dt;
        }

        public static int GetTotalAircraftCount()
        {
            int totalCount = -1;
            try
            {
                using (var context = new AirportDBEntities())
                {
                    totalCount = context.Aircraft.Count();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
            }

            return totalCount;
        }

        public static AllViewModel.Aircraft GetAircraftById(string aircraftId)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    // Retrieve the aircraft entity from the database
                    var dbAircraft = context.Aircraft.FirstOrDefault(a => a.AircraftID == aircraftId);

                    if (dbAircraft != null)
                    {
                        // Fetch the airport names for Source and Destination based on AirportID
                        var sourceAirport = context.Airports.FirstOrDefault(a => a.AirportID == dbAircraft.Source);
                        var destinationAirport = context.Airports.FirstOrDefault(a => a.AirportID == dbAircraft.Destination);

                        var viewModelAircraft = new AllViewModel.Aircraft
                        {
                            AircraftID = dbAircraft.AircraftID,
                            AircraftNumber = dbAircraft.AircraftNumber,
                            AirLine = dbAircraft.AirLine,
                            // Set the Source and Destination properties to airport names
                            Source = sourceAirport != null ? sourceAirport.AirportName : string.Empty,
                            Destination = destinationAirport != null ? destinationAirport.AirportName : string.Empty
                        };

                        return viewModelAircraft;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return null;
            }
        }


        public static bool AddTransaction(AllViewModel.FuelTransaction viewModelTransaction)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    var airport = context.Airports.FirstOrDefault(a => a.AirportID == viewModelTransaction.AirportID);
                    if (airport == null)
                    {
                        return false;
                    }

                    decimal totalFuelAvailable = (context.FuelTransactions
                        .Where(ft => ft.AirportID == viewModelTransaction.AirportID && ft.TransactionType == 1)
                        .Sum(ft => (decimal?)ft.Quantity) ?? 0)
                        - (context.FuelTransactions
                            .Where(ft => ft.AirportID == viewModelTransaction.AirportID && ft.TransactionType == 2)
                            .Sum(ft => (decimal?)ft.Quantity) ?? 0)
                        + viewModelTransaction.Quantity;

                    FuelTransaction dbTransaction = new FuelTransaction
                    {
                        TransactionID = viewModelTransaction.TransactionID,
                        TimeStamp = DateTime.Now,
                        TransactionType = viewModelTransaction.TransactionType,
                        AirportID = viewModelTransaction.AirportID,
                        AircraftID = viewModelTransaction.AircraftID,
                        Quantity = viewModelTransaction.Quantity,
                        TransactionIDParent = viewModelTransaction.TransactionIDParent
                    };

                    context.FuelTransactions.Add(dbTransaction);
                    context.SaveChanges();

                    var fuelAvailable = (context.FuelTransactions
                        .Where(ft => ft.AirportID == viewModelTransaction.AirportID && ft.TransactionType == 1)
                        .Sum(ft => (decimal?)ft.Quantity) ?? 0)
                        - (context.FuelTransactions
                            .Where(ft => ft.AirportID == viewModelTransaction.AirportID && ft.TransactionType == 2)
                            .Sum(ft => (decimal?)ft.Quantity) ?? 0);


                    if (fuelAvailable < 0 || fuelAvailable > airport.FuelCapacity)
                    {
                        context.FuelTransactions.Remove(dbTransaction);
                        context.SaveChanges();
                        return false;
                    }

                    context.SaveChanges();

                    airport.FuelAvailable = fuelAvailable;
                    context.SaveChanges();

                    return true;
                }

            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return false;
            }
        }

        public static DataTable GetAllTransactions(string sortExpression, string sortDirection, int currentPageIndex, int pageSize)
        {
            DataTable dt = new DataTable();

            //string query = $@"SELECT 
            //             TransactionID, TimeStamp, TransactionType, AirportID, AircraftID, Quantity, TransactionIDParent
            //         FROM 
            //             FuelTransaction
            //         ORDER BY {sortExpression} {sortDirection}
            //         OFFSET @StartRowIndex ROWS FETCH NEXT @PageSize ROWS ONLY";

            string query = $@"SELECT 
                      ft.TransactionID, 
                      ft.TimeStamp, 
                      ft.TransactionType, 
                      a.AirportName AS AirportName, 
                      ft.AircraftID, 
                      ft.Quantity, 
                      ft.TransactionIDParent
                  FROM 
                      FuelTransaction ft
                  INNER JOIN 
                      Airport a ON ft.AirportID = a.AirportID
                  ORDER BY 
                      {sortExpression} {sortDirection}
                  OFFSET @StartRowIndex ROWS FETCH NEXT @PageSize ROWS ONLY";


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AirportManagementConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    int startRowIndex = currentPageIndex * pageSize;
                    cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    try
                    {
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddLogException(ex);
                    }
                }
            }

            return dt;
        }

        public static int GetTotalTransactionCount()
        {
            int totalCount = 0;
            try
            {
                using (var context = new AirportDBEntities())
                {
                    totalCount = context.FuelTransactions.Count();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
            }

            return totalCount;
        }

        public static bool RemoveAllTransactions()
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    var transactions = context.FuelTransactions.ToList();
                    context.FuelTransactions.RemoveRange(transactions);

                    //foreach (var transaction in transactions)
                    //{
                    //    context.FuelTransactions.Remove(transaction);
                    //}
                    context.SaveChanges();

                    var airports = context.Airports.ToList();

                    foreach (var airport in airports)
                    {
                        airport.FuelAvailable = 0;
                    }
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return false;
            }
        }

        public static AllViewModel.FuelTransaction GetTransactionDetails(int transactionID)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    var dbTransaction = context.FuelTransactions.FirstOrDefault(t => t.TransactionID == transactionID);
                    if (dbTransaction != null)
                    {
                        return new AllViewModel.FuelTransaction
                        {
                            TransactionID = dbTransaction.TransactionID,
                            TimeStamp = dbTransaction.TimeStamp,
                            TransactionType = dbTransaction.TransactionType,
                            AirportID = dbTransaction.AirportID,
                            AircraftID = dbTransaction.AircraftID,
                            Quantity = dbTransaction.Quantity,
                            TransactionIDParent = dbTransaction.TransactionIDParent
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return null;
            }
        }

        public static List<AirportFuelSummary> GetAirportFuelSummary(string sortExpression, string sortDirection, int currentPageIndex, int pageSize)
        {
            using (var context = new AirportDBEntities())
            {
                var query = context.Airports
                    .Select(airport => new AirportFuelSummary
                    {
                        AirportName = airport.AirportName,
                        FuelAvailable = (decimal)airport.FuelAvailable
                        //FuelAvailable = (context.FuelTransactions
                        //        .Where(ft => ft.AirportID == airport.AirportID && ft.TransactionType == 1)
                        //        .Sum(ft => (decimal?)ft.Quantity) ?? 0)
                        //    - (context.FuelTransactions
                        //        .Where(ft => ft.AirportID == airport.AirportID && ft.TransactionType == 2)
                        //        .Sum(ft => (decimal?)ft.Quantity) ?? 0)
                    });

                switch (sortExpression)
                {
                    case "AirportName":
                        query = sortDirection == "ASC" ? query.OrderBy(a => a.AirportName) : query.OrderByDescending(a => a.AirportName);
                        break;
                    case "FuelAvailable":
                        query = sortDirection == "ASC" ? query.OrderBy(a => a.FuelAvailable) : query.OrderByDescending(a => a.FuelAvailable);
                        break;
                    default:
                        query = query.OrderBy(a => a.AirportName);
                        break;
                }
                int startRowIndex = currentPageIndex * pageSize;

                query = query.Skip(startRowIndex).Take(pageSize);

                var airportFuelSummaries = query.ToList();

                // Update FuelAvailable column in Airports table
                foreach (var summary in airportFuelSummaries)
                {
                    var airport = context.Airports.FirstOrDefault(a => a.AirportName == summary.AirportName);
                    if (airport != null)
                    {
                        airport.FuelAvailable = summary.FuelAvailable;
                    }
                }

                context.SaveChanges();

                return airportFuelSummaries;
            }
        }

        public static List<FuelConsumptionReportItem> GetFuelConsumptionReport()
        {
            using (var context = new AirportDBEntities())
            {
                var fuelConsumptionReport = context.Airports
                                 .OrderBy(a => a.AirportID)
                                 .Select(airport => new FuelConsumptionReportItem
                                 {
                                     AirportName = airport.AirportName,
                                     Transactions = airport.FuelTransactions
                                         .OrderBy(ft => ft.TimeStamp)
                                         .Select(ft => new FuelTransactionItem
                                         {
                                             CreatedDateTime = ft.TimeStamp,
                                             Type = ft.TransactionType == 1 ? "In" : "Out",
                                             Fuel = ft.Quantity,
                                             Aircraft = ft.AircraftID ?? ""
                                         }).ToList(),
                                     FuelAvailable = airport.FuelAvailable != null ? (decimal)airport.FuelAvailable : 0 // Handle null value
                                 })
                                 .ToList();


                return fuelConsumptionReport;

            }
        }
    }
}