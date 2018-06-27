using Appman.LeaveManagement.DatabaseContext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.DatabaseContext
{
    public class LeaveManagementDbContext : DbContext
    {


        public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RemainingHour>().HasKey(table => new {
                table.EmployeeId,
                table.Year
            });
            builder.Entity<Reporting>().HasKey(table => new {
                table.EmployeeId,
                table.ReportingTo
            });
        }

        public DbSet<LeaveInfo> LeaveInfos { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RemainingHour> RemainingHours { get; set; }
        public DbSet<MdGender> MdGenders { get; set; }
        public DbSet<Reporting> Reportings { get; set; }

    }
}
