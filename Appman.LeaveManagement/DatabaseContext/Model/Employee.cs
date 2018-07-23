using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.DatabaseContext.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeNumber { get; set; }
        public string StaffId { get; set; }
        public string FirstNameTH { get; set; }
        public string LastNameTH { get; set; }
        public string FirstNameEN { get; set; }
        public string LastNameEN { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string GenderCode { get; set; }
        public string RoleCode { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsActive { get; set; }
    }

    
}
