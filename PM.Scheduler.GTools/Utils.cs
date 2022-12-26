using PM.Scheduler.GTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PM.Scheduler.GTools
{
    public static class Utils
    {
        public static List<Models.Task> GetTasks(WorkingCalendar calendar, Dictionary<Resource, List<Models.ProcessSchedule>> plan)
        {
            List<Models.Task> lineas = new();
            foreach (var entry in plan)
            {
                lineas.AddRange(GetTasks(calendar, entry.Key.Code, entry.Value));
            }
            return lineas;
        }
        private static List<Models.Task> GetTasks(WorkingCalendar calendar, string resource, List<Models.ProcessSchedule> processes)
        {
            List<Models.Task> lineas = new();
            foreach (var step in processes)
            {
                lineas.Add(new Models.Task()
                {
                    Start = calendar.GetDateTime(step.Start),
                    End = calendar.GetDateTime(step.End),
                    Category = step.Process.OpCode,
                    Title = step.Process.ToString(),
                    Resource = resource,
                });
            }
            return lineas;
        }
        public static void Print(string resource, List<Models.ProcessSchedule> processes)
        {
            List<string> lineas = new();
            foreach (var step in processes)
            {
                lineas.Add(new string('*', step.Start) + FixLength(step.Process.Code, step.Duration));
            }
            string print = Fusion(lineas);
            Console.WriteLine($"Resource {resource}:: {print}");
        }
        private static string FixLength(string word, int lenght)
        {
            bool d = true;
            while (word.Length < lenght - 2)
            {
                if (d) word = word + " ";
                else word = " " + word;
                d = !d;
            }
            return "|" + word + "|";
        }
        private static string Fusion(List<string> lineas)
        {
            string result = "";
            for (int i = 0; i < lineas.Max(l => l.Length); i++)
            {
                char add = '*';
                foreach (string linea in lineas)
                {
                    if (linea.Length > i && linea[i] != '*')
                    {
                        add = linea[i];
                        break;
                    }
                }
                result += add;
            }
            return result;
        }
    }
}
