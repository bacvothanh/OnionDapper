using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Data.Interfaces;

namespace Dapper.Data.Abstracts
{
    public abstract class BaseDeleteEntity : IEntity<int>, IDelete
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset? TimeDeleteOffset { get; set; }
        public long? DeleteBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
