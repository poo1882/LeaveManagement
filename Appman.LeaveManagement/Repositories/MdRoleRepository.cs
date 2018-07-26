using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class MdRoleRepository
    {
        LeaveManagementDbContext _dbContext;
        public MdRoleRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(MdRole mdRole)
        {
            _dbContext.MdRoles.Add(mdRole);
            _dbContext.SaveChanges();
        }

        public MdRole GetRole (string roleCode)
        {
            return _dbContext.MdRoles.FirstOrDefault(x => x.RoleCode == roleCode);
        }

        public bool InitRoles(string password)
        {
            if (password != "init")
                return false;
            var lines = File.ReadAllLines("C:\\Users\\poo1882\\Desktop\\mdRoles.csv").Select(a => a.Split(','));
            foreach (var item in lines)
            {

                MdRole role = new MdRole
                {
                    RoleCode = item[0].Replace(" -", ","),
                    Position = item[1].Replace(" -", ","),
                    Department = item[2].Replace(" -", ","),
                    Abbreviation = item[3].Replace(" -", ","),
                };

                if (item[4].ToLower() == "true")
                    role.IsAdmin = true;
                else
                    role.IsAdmin = false;
                if (item[5].ToLower() == "true")
                    role.IsInProbation = true;
                else
                    role.IsInProbation = false;
                _dbContext.MdRoles.Add(role);
            }
            
            _dbContext.SaveChanges();
            return true;
        }

        public List<MdRole> GetRoles()
        {
            return _dbContext.MdRoles.ToList();
        }

        public void ClearRoles()
        {
            var roles = _dbContext.MdRoles;
            foreach (var item in roles)
            {
                roles.Remove(item);
            }
            _dbContext.SaveChanges();
        }
    }
}
