using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PM.Scheduler.GTools.Models.Process;

namespace PM.Scheduler.GTools.Models
{
    public record Resource
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public Resource(string code, string description)
        {
            Code = code; Description = description;
        }
        public Resource(string code)
        {
            Code = code; Description = code;
        }
    }
}
