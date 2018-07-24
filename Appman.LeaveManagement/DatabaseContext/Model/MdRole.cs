using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.DatabaseContext.Model
{
    public class MdRole
    {
        [Key]
        public string RoleCode { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Abbreviation { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsInProbation { get; set; }
    }
}
