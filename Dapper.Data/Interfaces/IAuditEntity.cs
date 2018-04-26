using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Data.Interfaces
{
    public interface IAuditEntity
    {
        long CreateBy { get; set; }
        DateTimeOffset TimeCreatedOffset { get; set; }
        long? ModifyBy { get; set; }
        DateTimeOffset? TimeModifyOffset { get; set; }
    }
}
