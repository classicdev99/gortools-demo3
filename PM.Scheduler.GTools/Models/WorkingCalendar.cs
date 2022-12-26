using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Scheduler.GTools.Models
{
    public class WorkingCalendar
    {

        public DateTime GetDateTime(int hour)
        {
            return DateTime.Now.Date
                .AddHours(8)
                .AddHours(hour);
        }
    }
}
