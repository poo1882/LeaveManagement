using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Controllers
{
    [Route("/api/[Controller]")]
    public class MdGenderController : Controller
    {
        private readonly LeaveManagementDbContext _dbContext;
        private readonly MdGenderRepository _mdGenderRepo;

        public MdGenderController(LeaveManagementDbContext leaveManagementDbContext)
        {
            _dbContext = leaveManagementDbContext;
            _mdGenderRepo = new MdGenderRepository(_dbContext);
        }

        [Route("MdGender")]
        [HttpPost]
        public IActionResult AddMdGender (MdGender mdGender)
        {
            _mdGenderRepo.Add(mdGender);
            return Ok();
        }
    }
}
