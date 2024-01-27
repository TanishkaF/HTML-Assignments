using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DataTableExtensions
{
    public static class DataTableExtensions
    {
        public static List<T> ToListExtension<T>(this DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();
                foreach (DataColumn column in dataTable.Columns)
                {
                    // Use reflection to set property or field values on the object
                    typeof(T).GetProperty(column.ColumnName)?.SetValue(obj, row[column]);
                }
                list.Add(obj);
            }

            return list;
        }

        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dataTable = new DataTable();

            if (list != null && list.Any())
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    dataTable.Columns.Add(new DataColumn(property.Name, property.PropertyType));
                }

                foreach (T item in list)
                {
                    DataRow row = dataTable.NewRow();

                    foreach (PropertyInfo property in properties)
                    {
                        object value = property.GetValue(item);
                        row[property.Name] = value ?? DBNull.Value;
                    }

                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
    }

    class Program
    {
        static void Main()
        {
            // Example usage:

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));

            dataTable.Rows.Add(1, "John");
            dataTable.Rows.Add(2, "Jane");

            // Convert DataTable to List<Employee>
            List<Employee> employees = dataTable.ToListExtension<Employee>();

            Console.WriteLine("Printing Employees:");

            foreach (Employee emp in employees)
            {
                Console.WriteLine("ID = {0}, Name = {1}", emp.ID, emp.Name);
            }

            // Convert List<Employee> to DataTable
            DataTable newDataTable = DataTableExtensions.ListToDataTable(employees);

            Console.WriteLine("\nPrinting DataTable:");

            // Print DataTable
            foreach (DataRow row in newDataTable.Rows)
            {
                Console.WriteLine("ID = {0}, Name = {1}", row["ID"], row["Name"]);
            }
        }
    }

    class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
