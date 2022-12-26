using Google.Protobuf.WellKnownTypes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Scheduler.GTools.Models
{
    public class ProcessSchedule : IComparable
    {
        public Process Process { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int Duration => End - Start;
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            ProcessSchedule otherTask = obj as ProcessSchedule;
            if (otherTask != null)
            {
                if (Start != otherTask.Start)
                    return Start.CompareTo(otherTask.Start);
                else
                    return 0;
            }
            else
                throw new ArgumentException("Object is not a ProcessScheduling");
        }
       
    }
}
