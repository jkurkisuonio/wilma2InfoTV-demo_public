using System;
using System.Collections.Generic;


namespace Testing_Primus_Json.Models
{
    public class Schedule : IComparable<Schedule>
    {
        public string UID { get; set; }
        public int Day { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Class { get; set; }
        private DateTime datetime;
        private List<String> dates;
        private List<DateTime> dateTimes;

        public Schedule()
        {
            dateTimes = new List<DateTime>();
           
        }

        public List<Group> Groups { get; set; }
        public List<string> Dates
        { get
            {
                return dates;
            }
         set {
                dates = value;
                foreach (var d in dates)
                {
                    bool convert = DateTime.TryParse(d, out datetime);
                    if (convert) DateTimes.Add(datetime);
                }
            }
        }

        public List<DateTime> DateTimes { get => dateTimes; }

        public int CompareTo(Schedule other)
        {
            DateTime first = this.DateTimes[0];
            DateTime second = other.DateTimes[0];
            if (first == second) return 0;
            else return 1;
        }
    }

}