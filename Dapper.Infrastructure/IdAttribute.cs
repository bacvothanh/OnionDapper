using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Infrastructure
{
    /// <summary>
    /// Id in database
    /// </summary>
    public class IdAttribute : Attribute
    {
        public IdAttribute(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}
