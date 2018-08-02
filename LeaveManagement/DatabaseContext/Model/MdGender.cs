using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.DatabaseContext.Model
{
    public class MdGender
    {
        [Key]
        public string GenderCode { get; set; }
        public string GenderTH { get; set; }
        public string GenderEN { get; set; }
        public string TitleTH { get; set; }
        public string TitleEN { get; set; }
    }
}
