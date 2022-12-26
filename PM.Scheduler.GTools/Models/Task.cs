using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Scheduler.GTools.Models
{
    public class Task
    {
        public string Resource { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsAllDay { get; set; } = false;
    }
}
