using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.DatabaseContext.Model
{
    public class Approbation
    {
        [Key]
        public Guid ApprobationGuid { get; set; }
        public int LeaveId { get; set; }
        public string ApproverId { get; set; }
        public string Status { get; set; }
    }
}
