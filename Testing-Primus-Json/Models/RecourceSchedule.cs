using System;
using System.Collections;
using System.Collections.Generic;

namespace Testing_Primus_Json.Models
{
    public class RecourceSchedule
    {
        public string PrimusType { get; set; }
        public int PrimusID { get; set; }
        public string PrimusName { get; set; }
        public List<Schedule> Schedule { get; set; }

        public IEnumerator<DateTime> GetEnumerator()
        {
            throw new NotImplementedException();
        }

       
    }
}