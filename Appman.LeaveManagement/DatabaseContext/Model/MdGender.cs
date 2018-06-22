using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.DatabaseContext.Model
{
    public class MdGender
    {
        [Key]
        public string GenderCode { get; set; }
        public string TH { get; set; }
        public string EN { get; set; }
    }
}
