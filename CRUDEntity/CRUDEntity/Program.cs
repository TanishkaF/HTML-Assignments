using CRUDEntity;
using System;
using System.Collections.Generic;
using System.Linq;
namespace EFDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("display data");           
            using (EF_Demo_DBEntities DBEntities = new EF_Demo_DBEntities())
            {
                List<Department> listDepartments = DBEntities.Departments.ToList();
                Console.WriteLine("printing:");

                foreach (Department dept in listDepartments)
                {
                    Console.WriteLine("DepartmentId = {0}, DepartmentName = {1}", dept.DepartmentID, dept.DepartmentName);               
                }

                List<Employee> listEmployee = DBEntities.Employees.ToList();

                foreach (Employee emp in listEmployee)
                {
                    Console.WriteLine("EmployeeID = {0}, EmployeeName = {1}",
                        emp.EmployeeID, emp.EmployeeName);
                }
               // Console.ReadKey();
            }

            Console.WriteLine("insert data");
            /* using (EF_Demo_DBEntities context = new EF_Demo_DBEntities())
             {

                 //Create a new student which you want to add to the database
                 var newStudent = new Employee()
                 {
                     EmployeeID = 80,
                     EmployeeName = "Rout",
                     DepartmentID = 1
                 };
                 //Add Student Entity into Students DBset by calling the Add Method
                 context.Employees.Add(newStudent);
                 //Now the Entity State will be in Added State
                 Console.WriteLine($"Before SaveChanges Entity State: {context.Entry(newStudent).State}");

                 //If you want to see what SQL Statement it generates for Inserting the data, 
                 //please use the following statement, it will log the SQL Statement in the console window
                 context.Database.Log = Console.Write;

                 //Call SaveChanges method to save student into database
                 context.SaveChanges();
                 //Now the Entity State will change from Added State to Unchanged State
                 Console.WriteLine($"After SaveChanges Entity State: {context.Entry(newStudent).State}");
             }
            */

            Console.WriteLine("update data");
            using (EF_Demo_DBEntities context = new EF_Demo_DBEntities())
            {

                var student = context.Employees.Find(80);
               
                Console.WriteLine($"Before Updating Entity State: {context.Entry(student).State}");
                //Update the first name and last name
                student.EmployeeName = "Sanju";
           
                //Now the Entity State will be in Added State
                Console.WriteLine($"Before SaveChanges Entity State: {context.Entry(student).State}");

                //If you want to see what SQL Statement it generates for Inserting the data, 
                //please use the following statement, it will log the SQL Statement in the console window
                context.Database.Log = Console.Write;

                //Call SaveChanges method to save student into database
                context.SaveChanges();
                //Now the Entity State will change from Added State to Unchanged State
                Console.WriteLine($"After SaveChanges Entity State: {context.Entry(student).State}");
            }

            Console.WriteLine("delete data");
            using (EF_Demo_DBEntities context = new EF_Demo_DBEntities())
            {
                var student = context.Employees.Find(80);

                Console.WriteLine($"Before Updating Entity State: {context.Entry(student).State}");
                //Update the first name and last name
                //student.EmployeeName = "Sanju";

                context.Employees.Remove(student);
                //Now the Entity State will be in Added State
                Console.WriteLine($"Before SaveChanges Entity State: {context.Entry(student).State}");

                //If you want to see what SQL Statement it generates for Inserting the data, 
                //please use the following statement, it will log the SQL Statement in the console window
                context.Database.Log = Console.Write;

                //Call SaveChanges method to save student into database
                context.SaveChanges();
                //Now the Entity State will change from Added State to Unchanged State
                Console.WriteLine($"After SaveChanges Entity State: {context.Entry(student).State}");
            }

            Console.ReadKey();
        }
    }
}