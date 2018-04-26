using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Data.Models;

namespace Dapper.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Account> Accounts { get; set; }
     
        public DbSet<Role> Roles { get; set; }
      
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(x => x.Roles)
                .WithMany(y => y.Accounts)
                .Map(m =>
                {
                    m.ToTable("AccountRoles");
                    m.MapLeftKey("AccountId");
                    m.MapRightKey("RoleId");
                });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
