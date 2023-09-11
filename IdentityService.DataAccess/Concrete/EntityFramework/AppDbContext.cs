using IdentityService.Core.Configurations;
using IdentityService.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Entities.Entities;

namespace IdentityService.DataAccess.Concrete.EntityFramework
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQL5111.site4now.net;Initial Catalog=db_a9ea96_identity;User Id=db_a9ea96_identity_admin;Password=id4516ru");
        }


        public DbSet<AppUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
