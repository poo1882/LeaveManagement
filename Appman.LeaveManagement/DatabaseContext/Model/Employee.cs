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
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Position { get; set; }
        public DateTime StartWorkingDate { get; set; }
        public bool IsActive { get; set; }
        public string Section { get; set; }
        public bool IsInProbation { get; set; }
        public string GenderCode { get; set; }
        public bool IsSuperHr { get; set; }
    }

    
}
