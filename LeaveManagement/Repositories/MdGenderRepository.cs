using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Repositories
{
    public class MdGenderRepository
    {
        LeaveManagementDbContext _dbContext;
        public MdGenderRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(MdGender mdGender)
        {
            _dbContext.MdGenders.Add(mdGender);
            _dbContext.SaveChanges();
        }

        public string GetTitleTH(string staffId)
        {
            Employee emp = _dbContext.Employees.FirstOrDefault(x => x.StaffId == staffId);
            return GetTitle(emp.GenderCode,"TH");

        }

        public string GetTitleEN(string staffId)
        {
            Employee emp = _dbContext.Employees.FirstOrDefault(x => x.StaffId == staffId);
            return GetTitle(emp.GenderCode, "EN");
        }

        private string GetTitle(string genderCode,string language)
        {
            var gender = _dbContext.MdGenders.FirstOrDefault(x => x.GenderCode == genderCode);
            if (language.ToLower() == "th")
                return gender.TitleTH;
            else if (language.ToLower() == "en")
                return gender.TitleEN;
            return null;
        }

        public bool InitMdGenders(string password)
        {
            if (password != "init")
                return false;
            var lines = File.ReadAllLines("C:\\Users\\poo1882\\Desktop\\mdGenders.csv").Select(a => a.Split(','));
            foreach (var item in lines)
            {

                MdGender mdGender = new MdGender
                {
                   GenderCode = item[0],
                   GenderTH = item[1],
                   GenderEN = item[2],
                   TitleTH = item[3],
                   TitleEN = item[4]
                };
                _dbContext.MdGenders.Add(mdGender);
            }

            _dbContext.SaveChanges();
            return true;
        }

        public List<MdGender> GetMdGenders()
        {
            return _dbContext.MdGenders.ToList();
        }

        public void ClearMdGenders()
        {
            var mdGenders = _dbContext.MdGenders;
            foreach (var item in mdGenders)
            {
                mdGenders.Remove(item);
            }
            _dbContext.SaveChanges();
        }
    }
}
