using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing_Primus_Json.Models;
using Testing_Primus_Json.Models.Careeria;
using Testing_Primus_Json.Utils;

namespace Testing_Primus_Json
{
  
    public class WilmaQuery
    {
        private WilmaJson wilmajson;

        public WilmaQuery(WilmaJson wilmajson)
        {
            this.wilmajson = wilmajson ?? throw new ArgumentNullException(nameof(wilmajson));
        }

        /// <summary>
        /// Selvitetään kaikki tilat Wilmasta ja muodostetaan niistä oliojoukko josta ilmenee tilan ominaisuudet.
        /// </summary>
        /// <returns></returns>
        public List<CareeriaRoom> GetARoom()
        {
            var roomlist = new Dictionary<int, string>();
            var careeriaroomlist = new List<CareeriaRoom>();
            // Koko tämä vuosi
            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year, 12, 31);

            // TODO: Hardcoded - max. numero Primus korttien on nyt 700.
            for (int i = 0; i < 700; i++)
            {
                var result2 = wilmajson.Login("schedule/index_json?p=" + firstDay.Day + "." + firstDay.Month + "." + firstDay.Year + "&f=" + lastDay.Day + "." + lastDay.Month + "." + lastDay.Year + "&rooms=" + i);
                WilmaClassResourse huoneenVarauksetTaltaVuodelta = JsonConvert.DeserializeObject<WilmaClassResourse>(result2);
                foreach (RecourceSchedule varaukset in huoneenVarauksetTaltaVuodelta.RecourceSchedules)
                {
                    // Tilan ominaisuudet on koodattu tähän Caption kenttään. Formaatti: 
                    /*                     
                Esimerkkejä AMT-ATK-1-C03-22
                AMT-NEU-2-Neuvotteluhuone B-12
                ASK-LII-1-Liikuntasali-0
                HER-ERI-3-Hoiva-20
                LOV-LUE-1-110-0
                PMT-TEO-1-C11-18
                POM-MUU-1-Iris-0
                Paikkakunta ASK Askola
                HER Helsinki
                KER Kerava
                LOV Loviisa 
                AMT Porvoo, Ammattitie
                PMT Porvoo, Perämiehentie
                POM Porvoo, POMO-talo
                VAN Vantaa
                Tilatyyppi ATK Atk-luokka
                ERI Käytännön opetustilat, erikoisluokka
                KIE Kielistudio
                LII Liikuntatila
                LUE Luentotila
                MUU Muu tila
                NEU Neuvotteluhuone
                TEO Teorialuokka
                YKS Yksilöohjaustila
                Kerros 1 1. kerros
                2
                3
                4
                5
                Tilan nimi Virna Tilan nimi
                C06
                Mega
                Kapasiteetti 36 Tilaan mahtuma henkilömäärä*/
                    var nimi = varaukset.Schedule.FirstOrDefault()?.Groups.FirstOrDefault()?.Rooms.FirstOrDefault()?.Caption;
                    roomlist.Add(i, nimi);
                    CareeriaRoom careeriaRoom = CareeriaSpesific.ParseRoom(i, nimi);
                    if (careeriaRoom.PrimusCardNumber != -1) careeriaroomlist.Add(careeriaRoom);
                }

            }

            return careeriaroomlist;
        }



    }
}



