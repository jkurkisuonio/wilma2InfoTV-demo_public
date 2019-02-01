using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing_Primus_Json.Models.Careeria;

namespace Testing_Primus_Json.Utils
{
    class CareeriaSpesific
    {
        internal static CareeriaRoom ParseRoom(int i, string nimi)
        {
            // PreGuard
            if (String.IsNullOrEmpty(nimi)) return new CareeriaRoom { PrimusCardNumber = -1 };
            var careeriaRoom = new CareeriaRoom();
            var nameParts = nimi.Split('-');
            // PreGuard
            if (nameParts.Count() < 4)
            { // Do something

                Console.WriteLine("Puutteelliset tiedot: " + nimi);
                return new CareeriaRoom { Caption = nimi, PrimusCardNumber = i };
            }

            switch (nameParts[0])
            {
                case "AMT": careeriaRoom.Place = CareeriaRoom.CareeriaPlace.AMT; break;
                case "PMT": careeriaRoom.Place = CareeriaRoom.CareeriaPlace.PMT; break;
                case "LOV": careeriaRoom.Place = CareeriaRoom.CareeriaPlace.LOV; break;
                case "KER": careeriaRoom.Place = CareeriaRoom.CareeriaPlace.KER; break;
                case "POM": careeriaRoom.Place = CareeriaRoom.CareeriaPlace.POM; break;
                case "LUN": careeriaRoom.Place = CareeriaRoom.CareeriaPlace.LUN; break;
                case "HER": careeriaRoom.Place = CareeriaRoom.CareeriaPlace.HER; break;
                case "ASK": careeriaRoom.Place = CareeriaRoom.CareeriaPlace.ASK; break;
                case "HKK": careeriaRoom.Place = CareeriaRoom.CareeriaPlace.HKK; break;
                case "VAN": careeriaRoom.Place = CareeriaRoom.CareeriaPlace.VAN; break;
                default: careeriaRoom.Place = CareeriaRoom.CareeriaPlace.UNKNOWN; break;
            }
            switch (nameParts[1])
            {
                case "LUE": careeriaRoom.Type = CareeriaRoom.CareeriaType.LUE; break;
                case "ATK": careeriaRoom.Type = CareeriaRoom.CareeriaType.ATK; break;
                case "ERI": careeriaRoom.Type = CareeriaRoom.CareeriaType.ERI; break;
                case "NEU": careeriaRoom.Type = CareeriaRoom.CareeriaType.NEU; break;
                case "TEO": careeriaRoom.Type = CareeriaRoom.CareeriaType.TEO; break;
                case "KIE": careeriaRoom.Type = CareeriaRoom.CareeriaType.KIE; break;
                case "LII": careeriaRoom.Type = CareeriaRoom.CareeriaType.LII; break;
                case "MUU": careeriaRoom.Type = CareeriaRoom.CareeriaType.MUU; break;                
                case "YKS": careeriaRoom.Type = CareeriaRoom.CareeriaType.YKS; break;
                default: careeriaRoom.Type = CareeriaRoom.CareeriaType.UNKNOWN; break;

                 

            }
            Int32.TryParse(nameParts[2], out int floor);
            int numberOfSeats = 0;
            if (nameParts.Count() == 5)  Int32.TryParse(nameParts[4], out numberOfSeats);
            careeriaRoom.Floor = floor;
            careeriaRoom.NumberOfSeats = numberOfSeats;
            careeriaRoom.PrimusCardNumber = i;
            careeriaRoom.Name = nameParts[3];
            careeriaRoom.Caption = nimi;
            return careeriaRoom;
        }
    }
}
