using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Controllers
{
    [Route("/api/[Controller]")]
    public class MdRoleController : Controller
    {
        private readonly LeaveManagementDbContext _dbContext;
        private readonly MdRoleRepository _mdRoleRepo;

        public MdRoleController(LeaveManagementDbContext leaveManagementDbContext)
        {
            _dbContext = leaveManagementDbContext;
            _mdRoleRepo = new MdRoleRepository(_dbContext);
        }

        [Route("MdRoles")]
        [HttpPut]
        public IActionResult InitRoles(string password)
        {
            if (!_mdRoleRepo.InitRoles(password))
                return NotFound();
            return Ok();
        }

        [Route("MdRoles")]
        [HttpGet]
        public IActionResult GetRoles()
        {
            var result = _mdRoleRepo.GetRoles();
            if (result == null)
                return NotFound();
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [Route("MdRoles")]
        [HttpDelete]
        public IActionResult ClearRoles()
        {
            _mdRoleRepo.ClearRoles();
            return Ok();
        }
    }
}
