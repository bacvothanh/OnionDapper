using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Data.Interfaces
{
    public interface IDelete
    {
        DateTimeOffset? TimeDeleteOffset { get; set; }
        long? DeleteBy { get; set; }
        bool IsDeleted { get; set; }
    }
}
