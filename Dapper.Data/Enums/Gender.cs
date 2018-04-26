using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Data.Enums
{
    public enum Gender
    {
        [Description("Nam")]
        Male =1,
        [Description("Nữ")]
        Female =0
    }
}
