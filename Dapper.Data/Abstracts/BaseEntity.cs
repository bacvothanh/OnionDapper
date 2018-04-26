using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Data.Interfaces;

namespace Dapper.Data.Abstracts
{
    public abstract class BaseEntity : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
    }
}
