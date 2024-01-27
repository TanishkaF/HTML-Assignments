using CRUDEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Entity
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            using (EF_Demo_DBEntities DBEntities = new EF_Demo_DBEntities())
            {
                while (true)
                {
                    Console.WriteLine("Menu:");
                    Console.WriteLine("1. Create a new department");
                    Console.WriteLine("2. Read all departments");
                    Console.WriteLine("3. Update the name of the first department");
                    Console.WriteLine("4. Delete the last department");
                    Console.WriteLine("5. Exit");
                    Console.Write("Enter your choice: ");

                    
                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                CreateNewDepartment(DBEntities);
                                CreateNewEmployee(DBEntities);
                                break;
                            case 2:
                                ReadAllDepartments(DBEntities);
                                ReadAllEmployee(DBEntities);
                                break;
                            case 3:
                                UpdateDepartmentById(DBEntities);
                                UpdateEmployeeById(DBEntities);
                                break;
                            case 4:
                                DeleteDepartmentById(DBEntities);
                                DeleteEmployeeById(DBEntities);
                                break;
                            case 5:
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Please enter a valid option.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }

                    Console.WriteLine();
                }
            }
        }
       
        static void CreateNewDepartment(EF_Demo_DBEntities DBEntities)
        {
            Console.Write("Enter the number of departments to create: ");

            if (int.TryParse(Console.ReadLine(), out int numberOfDepartments) && numberOfDepartments > 0)
            {
                // Get the current maximum DepartmentID
                int currentMaxDepartmentID = DBEntities.Departments.Max(d => (int?)d.DepartmentID) ?? 0;

                for (int i = 0; i < numberOfDepartments; i++)
                {
                    Console.Write($"Enter the name of department #{i + 1}: ");
                    string departmentName = Console.ReadLine();

                    Department newDepartment = new Department
                    {
                        DepartmentID = currentMaxDepartmentID + 1,
                        DepartmentName = departmentName
                    };

                    DBEntities.Departments.Add(newDepartment);
                    DBEntities.SaveChanges();
                    Console.WriteLine("New department created. DepartmentId: {0}", newDepartment.DepartmentID);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number greater than 0.");
            }
        }

        static void CreateNewEmployee(EF_Demo_DBEntities DBEntities)
        {
            Console.Write("Enter the name of the new employee: ");
            string employeeName = Console.ReadLine();

            // Get the current maximum EmployeeID
            int currentMaxEmployeeID = DBEntities.Employees.Max(e => (int?)e.EmployeeID) ?? 0;

            // Assuming you also want to associate the employee with a department
            Console.Write("Enter the DepartmentID for the employee: ");
            if (int.TryParse(Console.ReadLine(), out int departmentId))
            {
                Employee newEmployee = new Employee
                {
                    EmployeeID = currentMaxEmployeeID + 1,
                    EmployeeName = employeeName,
                    DepartmentID = departmentId
                };

                DBEntities.Employees.Add(newEmployee);
                DBEntities.SaveChanges();
                Console.WriteLine("New employee created. EmployeeID: {0}", newEmployee.EmployeeID);
            }
            else
            {
                Console.WriteLine("Invalid DepartmentID. Unable to create the employee.");
            }
        }

        static void ReadAllDepartments(EF_Demo_DBEntities DBEntities)
        {
            // Assuming you have a DataTable with data from the Employees table
            DataTable departmentDataTable = DBEntities.Departments.ToDataTable<Department>();

            // Convert DataTable back to List<Employee>
            List<Department> convertedListEmployee = departmentDataTable.ToListFromDataTable<Department>();

            Console.WriteLine("printing department");

            // Now you can iterate over the list and print the data
            foreach (Department department in convertedListEmployee)
            {
                Console.WriteLine($"DepartmentID: {department.DepartmentID}, DepartmentName: {department.DepartmentName}");
            }

        }

        static void ReadAllEmployee(EF_Demo_DBEntities DBEntities)
        {
            // Assuming you have a DataTable with data from the Employees table
            DataTable employeeDataTable = DBEntities.Employees.ToDataTable<Employee>();

            // Convert DataTable back to List<Employee>
            List<Employee> convertedListEmployee = employeeDataTable.ToListFromDataTable<Employee>();

            Console.WriteLine("printing employee");

            // Now you can iterate over the list and print the data
            foreach (Employee employee in convertedListEmployee)
            {
                Console.WriteLine($"EmployeeID: {employee.EmployeeID}, EmployeeName: {employee.EmployeeName}, DepartmentID: {employee.DepartmentID}");
            }

        }

        static void UpdateDepartmentById(EF_Demo_DBEntities DBEntities)
        {
            Console.Write("Enter the DepartmentID to update: ");
            if (int.TryParse(Console.ReadLine(), out int departmentIdToUpdate))
            {
                // Check if the department exists
                var departmentToUpdate = DBEntities.Departments.Find(departmentIdToUpdate);
                if (departmentToUpdate != null)
                {
                    Console.Write("Enter the updated name for the department: ");
                    string updatedName = Console.ReadLine();

                    departmentToUpdate.DepartmentName = updatedName;
                    DBEntities.SaveChanges();
                    Console.WriteLine("Department with DepartmentID {0} updated.", departmentIdToUpdate);
                }
                else
                {
                    Console.WriteLine("Department with DepartmentID {0} does not exist.", departmentIdToUpdate);
                }
            }
            else
            {
                Console.WriteLine("Invalid DepartmentID. Unable to update the department.");
            }
        }

        static void UpdateEmployeeById(EF_Demo_DBEntities DBEntities)
        {
            Console.Write("Enter the EmployeeID to update: ");
            if (int.TryParse(Console.ReadLine(), out int EmployeeIdToUpdate))
            {
                // Check if the department exists
                var EmployeeToUpdate = DBEntities.Employees.Find(EmployeeIdToUpdate);
                if (EmployeeToUpdate != null)
                {
                    Console.Write("Enter the updated name for the Employee: ");
                    string updatedName = Console.ReadLine();

                    EmployeeToUpdate.EmployeeName = updatedName;
                    DBEntities.SaveChanges();
                    Console.WriteLine("Employee with EmplpoyeeID {0} updated.", EmployeeIdToUpdate);
                }
                else
                {
                    Console.WriteLine("Employee with EmplpoyeeID {0} does not exist.", EmployeeIdToUpdate);
                }
            }
            else
            {
                Console.WriteLine("Invalid EmplpoyeeID. Unable to update the Emplpoyee.");
            }
        }

        static void DeleteDepartmentById(EF_Demo_DBEntities DBEntities)
        {
            Console.Write("Enter the DepartmentID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int departmentIdToDelete))
            {
                // Check if the department exists
                var departmentToDelete = DBEntities.Departments.Find(departmentIdToDelete);
                if (departmentToDelete != null)
                {
                    // Manually delete associated employees
                    var associatedEmployees = DBEntities.Employees.Where(e => e.DepartmentID == departmentIdToDelete);
                    DBEntities.Employees.RemoveRange(associatedEmployees);

                    // Now, delete the department
                    DBEntities.Departments.Remove(departmentToDelete);
                    DBEntities.SaveChanges();
                    Console.WriteLine("Department with DepartmentID {0} and associated employees deleted.", departmentIdToDelete);
                }
                else
                {
                    Console.WriteLine("Department with DepartmentID {0} does not exist.", departmentIdToDelete);
                }
            }
            else
            {
                Console.WriteLine("Invalid DepartmentID. Unable to delete the department.");
            }
        }

        static void DeleteEmployeeById(EF_Demo_DBEntities DBEntities)
        {
            Console.Write("Enter the EmployeeID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int employeeIdToDelete))
            {
                // Check if the employee exists
                var employeeToDelete = DBEntities.Employees.Find(employeeIdToDelete);
                if (employeeToDelete != null)
                {
                    // Now, delete the employee
                    DBEntities.Employees.Remove(employeeToDelete);
                    DBEntities.SaveChanges();
                    Console.WriteLine("Employee with EmployeeID {0} deleted.", employeeIdToDelete);
                }
                else
                {
                    Console.WriteLine("Employee with EmployeeID {0} does not exist.", employeeIdToDelete);
                }
            }
            else
            {
                Console.WriteLine("Invalid EmployeeID. Unable to delete the employee.");
            }
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

                        // Check if the property is Nullable
                        if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            propertyType = Nullable.GetUnderlyingType(propertyType);
                        }

                        try
                        {
                            // Attempt to convert the value
                            property.SetValue(obj, Convert.ChangeType(row[column], propertyType));
                        }
                        catch (InvalidCastException)
                        {
                            // Handle the conversion failure (optional)
                           // Console.WriteLine($"Conversion failed for column {column.ColumnName}. Skipping.");
                        }
                    }
                }

                list.Add(obj);
            }

            return list;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
{
    DataTable dataTable = new DataTable();

    if (items.Any())
    {
        foreach (var prop in typeof(T).GetProperties())
        {
            dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }

        foreach (var item in items)
        {
            DataRow row = dataTable.NewRow();
            foreach (var prop in typeof(T).GetProperties())
            {
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            }
            dataTable.Rows.Add(row);
        }
    }

    return dataTable;
}

        }
    }
