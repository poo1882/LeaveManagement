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
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Position { get; set; }
        public DateTime StartWorkingDate { get; set; }
        public bool IsActive { get; set; }
        

    }


    //public class EmployeeInput
    //{
        
    //    internal Guid Id { get; set; }
    //    public string Email { get; set; }
    //    public string FirstName { get; set; }
    //    public string Lastname { get; set; }
    //    public string Role { get; set; }
    //    public DateTime StartDate { get; set; }
    //    public byte[] ProfilePicture { get; set; }


    //}
}
