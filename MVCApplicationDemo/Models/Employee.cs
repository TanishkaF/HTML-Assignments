using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCApplicationDemo.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int EmployeeID {  get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
    }
}