using System.Collections.Generic;
using Dapper.Data.Enums;
using Dapper.Data.Logic;
using Dapper.Data.Models;

namespace Dapper.Data.Migrations
{
    using Infrastructure;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Dapper.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Dapper.Data.ApplicationDbContext context)
        {
            var roleAdmin = context.Roles.FirstOrDefault(x => x.Name == ApplicationConstant.Role.Admin);
            if (roleAdmin == null)
            {
                roleAdmin = new Role { Name = ApplicationConstant.Role.Admin };
                context.Roles.Add(roleAdmin);
            }

            var accountAdmin = context.Accounts.FirstOrDefault(x => x.UserName == "admin@gmail.com");
            if (accountAdmin == null)
            {
                accountAdmin = new Account
                {
                    UserName = "admin@gmail.com",
                    DisplayName = "Administrator"
                };
                accountAdmin.SetPassword("thanhbac123!@#");
                accountAdmin.AddRole(roleAdmin);
                context.Accounts.Add(accountAdmin);
                context.SaveChanges();
            }
            

            context.SaveChanges();
        }

        private void InitSampleData(ApplicationDbContext context, Role rolePatient, Role roleDoctor, Role roleHospitalManager)
        {
            var member = new Account
            {
                DisplayName = "Bac Vo",
                Email = "bacvothanh1988@gmail.com",
                UserName = "bacvothanh1988@gmail.com",
                Roles = new List<Role>
                {
                    rolePatient
                },
                Status = AccountStatus.Active,
                TimeCreatedOffset = DateTimeOffset.Now,
                CreateBy = 0
            };

            member.SetPassword("123456");
            context.Accounts.Add(member);
            context.SaveChanges();
            
            
        }

       

    }
}
