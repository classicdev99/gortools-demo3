using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PM.Scheduler.GTools.Models
{
    public record Operation
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public List<Process> Processes { get; private set; } = new();

        public Operation(string code, string description)
        {
            Code = code; Description = description;
        }
        public void AddProcess(params Process[] processes)
        {
            foreach (Process process in processes)
            {
                process.Description = Description + "_" + process.Description;
                //process.SortNumber = Processes.Count;
                process.OpCode = Code;
                Processes.Add(process);
            }
        }
    }
}
