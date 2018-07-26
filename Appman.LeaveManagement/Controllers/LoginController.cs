//using Appman.LeaveManagement.DatabaseContext;
//using Appman.LeaveManagement.DatabaseContext.Model;
//using Appman.LeaveManagement.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Appman.LeaveManagement.Controllers
//{
//    [Route("/api/[Controller]")]
//    public class LoginController : Controller
//    {
//        private readonly LeaveManagementDbContext _dbContext;
//        private readonly EmployeeRepository _empRepo;
//        private string clientSecret = "";
//        private string staffId;
//        public LoginController(LeaveManagementDbContext leaveManagementDbContext)
//        {
//            _dbContext = leaveManagementDbContext;
//            _empRepo = new EmployeeRepository(_dbContext);
//        }
//        [Route("Login")]
//        [HttpPost]
//        public IActionResult Login(Employee employee, string clientSecret)
//        {
//            //SetClientSecret(clientSecret);
//            //if (_empRepo.GetProfile(employee.StaffId) == null)
//            //    _empRepo.Add(employee);
//            return Ok();
//        }

//        [Route("GetClientSecret")]
//        [HttpGet]
//        public string GetClientSecret()
//        {
//            return clientSecret;
//        }
        

//        private void SetClientSecret(string clientSecret)
//        {
//            this.clientSecret = clientSecret;
//        }


//        private void SetStaffId(string staffId)
//        {
//            this.staffId = staffId;
//        }
//    }
//}
