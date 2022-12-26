using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Scheduler.GTools.Models
{
    public record Process
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string OpCode { get; set; }
        public List<AllowedResource> AllowedResources { get; set; } = new();
        public int ProcessType = 0;//Para matriz de cambio
        public int? MaxWaitTime = null;

        public Process(string code, string description)
        {
            Code = code; Description = description;
        }

        public void AddAllowedResource(Resource resource, int duration)
        {
            AllowedResources.Add(new AllowedResource(resource, duration));
        }
        public override string ToString()
        {
            return Code + "_" + Description;
        }


    }
}
