using Newtonsoft.Json;
using SharedLibraries.ClientUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Testing_Primus_Json.Models;

namespace Testing_Primus_Json
{
    class Program
    {

        public static void Main(string[] args)
        {
            // Luodaan WilmaJson luokasta olio ja alustetaan se wilmapalvelimen Urlilla, salasanalla, käyttäjätunnuksella ja Wilma-avaimella.
            WilmaJson wilma = new WilmaJson(    wilmaUrl: Properties.Settings.Default.wilmaUrl, 
                                                  passwd: Properties.Settings.Default.passwd,  
                                                username: Properties.Settings.Default.username,
                                                companySpesificKey: Properties.Settings.Default.companySpesificKey);

            // Luodaan sessio.
            var firstContact = wilma.Login(string.Empty);
            // Kirjaudutaan
            string loginWCookiesResult = wilma.LoginWCookies(Properties.Settings.Default.wilmaUrl + "login");

            // Haetaan dataa parametrein.
            // Huone 149 = Ruokalakabinetti           
            var result2 = wilma.Login("schedule/index_json?p=31.10.2018&f=31.12.2018&rooms=149");
            // Saatu tulos de-serialisoidaan poco-luokilla käsiteltäväksi tietorakenteeksi.
            WilmaClassResourse ruokalaKabinettiVaraukset = JsonConvert.DeserializeObject<WilmaClassResourse>(result2);

            // Tulostetaan ruudulle testimielessä.
         


            foreach (RecourceSchedule varaukset in ruokalaKabinettiVaraukset.RecourceSchedules)
            {
                Console.WriteLine(varaukset.PrimusName);               
                foreach (Schedule varaus in varaukset.Schedule.OrderBy(x => x.DateTimes.FirstOrDefault()))
                {
                    
                    foreach (var pvm in varaus.Dates)
                    {
                    Console.Write("Pvm: " + pvm + " " + varaus.Start + " - " + varaus.End);   
                    Console.WriteLine(" " + varaus.Groups.FirstOrDefault().Caption);                        
                    }
                  
                }
            }

            Console.ReadLine();

        }

    }
}
    

