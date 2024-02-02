using SchoolCRUD.BusinessLayer;
using SchoolCRUD.ViewModel;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using Enum = SchoolCRUD.UtilityLayer.Enum;

namespace SchoolCRUD
{
    internal class MainMethod
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Helper.DisplayMenu();

                Console.Write("Enter your choice: ");

                if (System.Enum.TryParse(Console.ReadLine(), out Enum.MenuOption choice))
                {
                    //Console.WriteLine();
                    switch (choice)
                    {
                        case Enum.MenuOption.CreateStudent:
                            Helper.CreateStudent();
                            break;

                        case Enum.MenuOption.CreateClass:
                            Helper.CreateClass();
                            break;

                        case Enum.MenuOption.CreateCourse:
                            Helper.CreateCourse();
                            break;
                        
                        case Enum.MenuOption.CreateEnrollment:
                            Helper.CreateEnrollment();
                            break;

                        case Enum.MenuOption.UpdateStudent:
                            Helper.UpdateStudent();
                            break;

                        case Enum.MenuOption.UpdateClass:
                            Helper.UpdateClass();
                            break;

                        case Enum.MenuOption.UpdateCourse:
                            Helper.UpdateCourse();
                            break; 
                        
                        case Enum.MenuOption.UpdateEnrollment:
                            Helper.UpdateEnrollment();
                            break;

                        case Enum.MenuOption.DeleteStudent:
                            Helper.DeleteStudent();
                            break;

                        case Enum.MenuOption.DeleteClass:
                            Helper.DeleteClass();
                            break;

                        case Enum.MenuOption.DeleteCourse:
                            Helper.DeleteCourse();
                            break; 
                        
                        case Enum.MenuOption.DeleteEnrollment:
                            Helper.DeleteEnrollment();
                            break;

                        case Enum.MenuOption.DisplayStudent:
                            Helper.DisplayStudent();
                            break;

                        case Enum.MenuOption.DisplayClass:
                            Helper.DisplayClasses();
                            break;

                        case Enum.MenuOption.DisplayCourse:
                            Helper.DisplayCourses();
                            break;
                        
                        case Enum.MenuOption.DisplayEnrollment:
                            Helper.DisplayEnrollments();
                            break;

                        case Enum.MenuOption.SearchStudentByID:
                            Helper.SearchStudentByID();
                            break;

                        case Enum.MenuOption.SearchClassByID:
                            Helper.SearchClassByID();
                            break;

                        case Enum.MenuOption.SearchCourseByID:
                            Helper.SearchCourseByID();
                            break; 
                        
                        case Enum.MenuOption.SearchEnrollmentByID:
                            Helper.SearchEnrollmentByID();
                            break;

                        case Enum.MenuOption.GetStudentsWithClassesAndCourses:
                            Helper.GetStudentWithCourses();
                            break;

                        case Enum.MenuOption.ExpoterDecider:
                            Helper.ExpoterDecider();
                            break;
                        
                        case Enum.MenuOption.ALLJOINHELPER:
                            Helper.ALLJOINHELPER();
                            break;


                        case Enum.MenuOption.Exit:
                            Console.WriteLine("Exiting the pogram");
                            Console.ReadKey();
                            return;


                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                }

            }

        }
    }
}