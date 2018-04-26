using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Data.Interfaces;

namespace Dapper.Data.Abstracts
{
    public abstract class BaseAuditEntity : IEntity<int>, IAuditEntity
    {
        [Key]
        public int Id { get; set; }
        public long CreateBy { get; set; }
        public DateTimeOffset TimeCreatedOffset { get; set; }
        public long? ModifyBy { get; set; }
        public DateTimeOffset? TimeModifyOffset { get; set; }
    }
}
