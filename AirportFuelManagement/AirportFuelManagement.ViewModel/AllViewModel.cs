using System;
using System.Collections.Generic;

namespace AirportFuelManagement.ViewModel
{
    public class AllViewModel
    {
        public class AirportFuelSummary
        {
            public string AirportName { get; set; }
            public decimal FuelAvailable { get; set; }

            public decimal FuelTotal { get; set;}
        }

        public class User
        {
            public int UserID { get; set; }
            public string Name { get; set; }
            public string EmailID { get; set; }
            public string Password { get; set; }
        }

        public class Airport
        {
            public int AirportUID { get; set; }
            public string AirportID { get; set; }
            public string AirportName { get; set; }
            public decimal? FuelCapacity { get; set; }
            public decimal? FuelAvailable { get; set; }
        }

        public class FuelConsumptionReportItem
        {
            public string AirportName { get; set; }
            public string  AirportID { get; set; }
            public List<FuelTransactionItem> Transactions { get; set; }
            public decimal FuelAvailable { get; set; }
        }

        public class FuelTransactionItem
        {
            public DateTime DateTime { get; set; }
            public string Type { get; set; }
            public decimal Fuel { get; set; }
            public string Aircraft { get; set; }
        }


        public class Aircraft
        {
            public int AircraftUID { get; set; }
            public string AircraftID { get; set; }
            public string AircraftNumber { get; set; }
            public string AirLine { get; set; }
            public string Source { get; set; }
            public string Destination { get; set; }
        }

        public class FuelTransaction
        {
            public string AirportName { get; set; }
            public int TransactionID { get; set; }
            public DateTime TimeStamp { get; set; }
            public int TransactionType { get; set; }
            public string AirportID { get; set; }
            public string AircraftID { get; set; }
            public decimal Quantity { get; set; }
            public int? TransactionIDParent { get; set; }
        }

        public class LogInSessionModel
        {
            public int UserID { get; set; }
            public bool IsAdmin { get; set; }
        }

        public struct TransactionType
        {
            public const int In = 1;
            public const int Out = 2;
        }  
    }
}