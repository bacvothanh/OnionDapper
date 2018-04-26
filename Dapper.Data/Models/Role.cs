using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Data.Abstracts;
using Dapper.Data.Interfaces;

namespace Dapper.Data.Models
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
