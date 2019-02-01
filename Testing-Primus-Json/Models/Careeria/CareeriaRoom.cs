using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Primus_Json.Models.Careeria
{
    public class CareeriaRoom
    {
        public CareeriaRoom()
        {
            this.Name = String.Empty;
            this.NumberOfSeats = 0;
            this.Caption = String.Empty;
            this.Floor = 0;
            
        }

        public int PrimusCardNumber { get; set; }
        public string Name { get; set; }

        public string Caption { get; set; }
        public enum CareeriaPlace { UNKNOWN, ASK, HER, KER, LOV, AMT, PMT, POM, VAN, HKK, LUN }

        public CareeriaPlace Place { get; set; }

        public enum CareeriaType { UNKNOWN, ATK, ERI, KIE, LII, LUE, MUU, NEU, TEO, YKS }

        public CareeriaType Type { get; set; }

        public int NumberOfSeats { get; set; }

        public int Floor { get; set; }
    }
}
