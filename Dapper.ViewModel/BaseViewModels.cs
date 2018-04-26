using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.ViewModel
{
    public abstract class BaseEntityViewModel
    {
        public int Id { get; set; }
    }

    public abstract class BaseEntityDeleteViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset? TimeDeleteOffset { get; set; }
        public long? DeleteBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public abstract class BaseAuditDeleteViewModel
    {
        public int Id { get; set; }
        public long CreateBy { get; set; }
        public DateTimeOffset TimeCreatedOffset { get; set; }
        public long? ModifyBy { get; set; }
        public DateTimeOffset? TimeModifyOffset { get; set; }
        public DateTimeOffset? TimeDeleteOffset { get; set; }
        public long? DeleteBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public abstract class BaseAuditViewModel
    {
        public int Id { get; set; }
        public long CreateBy { get; set; }
        public DateTimeOffset TimeCreatedOffset { get; set; }
        public long? ModifyBy { get; set; }
        public DateTimeOffset? TimeModifyOffset { get; set; }
    }
}
