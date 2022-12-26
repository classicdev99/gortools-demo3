using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Scheduler.GTools.Models
{
    public record AllowedResource
    {
        public Resource Resource { get; set; }
        public int Duration { get; set; }
        public AllowedResource(Resource resource, int duration)
        {
            Resource = resource;
            Duration = duration;
        }
        public AllowedResource() { }
    }
}
