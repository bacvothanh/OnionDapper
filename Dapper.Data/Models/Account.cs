using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Dapper.Data.Abstracts;
using Dapper.Data.Enums;
using Dapper.Data.Interfaces;

namespace Dapper.Data.Models
{
    [Table("Accounts")]
    public class Account : BaseAuditDeleteEntity, IUser<int>
    {
        public Account()
        {
            Status = AccountStatus.Unactive;
        }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string HashPassword { get; set; }
        public string Email { get; set; }
        public string TokenResetPassword { get; set; }
        public string TokenConfrimEmail { get; set; }
        public DateTimeOffset? TimeLastLogin { get; set; }
        public AccountStatus Status { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
