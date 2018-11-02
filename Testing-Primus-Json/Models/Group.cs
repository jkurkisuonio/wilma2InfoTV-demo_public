using System.Collections.Generic;

namespace Testing_Primus_Json.Models
{
    public class Group
    {
        public int KurreId { get; set; }
        public string ShortCaption { get; set; }
        public string Caption { get; set; }
        public string FullCaption { get; set; }
        public List<ActivityType> ActivityType { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}