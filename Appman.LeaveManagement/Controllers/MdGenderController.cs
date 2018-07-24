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
        public IActionResult AddMdGender(MdGender mdGender)
        {
            _mdGenderRepo.Add(mdGender);
            return Ok();
        }

        [Route("Title")]
        [HttpGet]
        public IActionResult GetTitle(string staffId, string language)
        {
            if (language.ToLower() == "th")
            {
                string result = _mdGenderRepo.GetTitleTH(staffId);
                return Content(JsonConvert.SerializeObject(result), "application/json");
            }

            else if (language.ToLower() == "en")
            {
                string result = _mdGenderRepo.GetTitleEN(staffId);
                return Content(JsonConvert.SerializeObject(result), "application/json");
            }

            return NotFound();
        }

        [Route("MdGenders")]
        [HttpPut]
        public IActionResult InitMdGenders(string password)
        {
            if (!_mdGenderRepo.InitMdGenders(password))
                return NotFound();
            return Ok();
        }

        [Route("MdGenders")]
        [HttpGet]
        public IActionResult GetMdGenders()
        {
            var result = _mdGenderRepo.GetMdGenders();
            if (result == null)
                return NotFound();
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [Route("MdGenders")]
        [HttpDelete]
        public IActionResult ClearMdGenders()
        {
            _mdGenderRepo.ClearMdGenders();
            return Ok();
        }
    }
}
