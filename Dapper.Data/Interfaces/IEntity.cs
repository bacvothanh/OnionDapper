using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Data.Interfaces
{
    public interface IEntity <T>
    {
        T Id { get; set; }
    }
}
