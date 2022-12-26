using Google.OrTools.Sat;
using PM.Scheduler.GTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Scheduler.GTools
{

    internal record GSchedule
    {
        public static GSchedule New(CpModel model, int minTime, int maxTime, int minD, int maxD, string name)//, int duration, string name)
        {
            var r = new GSchedule()
            {
                Start = model.NewIntVar(minTime, maxTime, $"start_{name}"),
                End = model.NewIntVar(minTime, maxTime, $"end_{name}")
            };
            var d = model.NewIntVar(minD, maxD, $"duration_{name}");
            r.Interval = model.NewIntervalVar(r.Start, d, r.End, $"interval_{name}");
            return r;
        }
        public static GSchedule NewOptional(CpModel model, GSchedule main, int minTime, int maxTime, int duration, string name)
        {
            var r = new GSchedule()
            {
                Start = model.NewIntVar(minTime, maxTime, $"start_{name}"),
                End = model.NewIntVar(minTime, maxTime, $"end_{name}"),
                Presence = model.NewBoolVar($"presence_{name}")
            };
            r.Interval = model.NewOptionalIntervalVar(r.Start, duration, r.End, r.Presence, $"interval_O_{name}");

            model.Add(main.Start == r.Start).OnlyEnforceIf(r.Presence);
            model.Add(main.End == r.End).OnlyEnforceIf(r.Presence);

            return r;
        }
        // public string ResourceName { get; set; }
        public IntVar Start { get; set; }
        public IntVar End { get; set; }
        public BoolVar Presence { get; set; }
        public IntervalVar Interval { get; set; }
        public AllowedResource AllowedResource { get; set; }
        public Process Process { get; set; }
    }
}
