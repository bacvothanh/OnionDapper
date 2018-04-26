using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Data.Models;
using Dapper.ViewModel;

namespace Dapper.Converter
{
    public class AutoMapperConfig
    {
        public static void Run()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                
            });
        }
    }
}
