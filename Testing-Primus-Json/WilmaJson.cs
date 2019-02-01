using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using SharedLibraries.ClientUtilities;
using SharedLibraries.ShareTypes;
using Testing_Primus_Json.Models.Careeria;

namespace Testing_Primus_Json
{
    public class WilmaJson
    {

        private readonly string wilmaUrl;
        private readonly string passwd;
        private readonly string username;
        private readonly string companySpesificKey;
        private string hash; 
        private IndexJson values;
        private HttpWebRequest request;
        private HttpWebResponse response;
        private LoginResultJson loginResultJson;

        private static readonly HttpClient client = new HttpClient();


        public WilmaJson(string wilmaUrl, string passwd, string username, string companySpesificKey)
        {
            this.wilmaUrl = wilmaUrl;
            this.passwd = passwd;
            this.username = username;
            this.companySpesificKey = companySpesificKey;
        }
        
        /// <summary>
        /// Suorittaa kyselyn Wilman Json rajapintaan ja palauttaa merkkijonon.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public string Login(string service)
        {
            // Initiate the HttpWebRequest with session support with CookiedFactory
            if (String.IsNullOrEmpty(service)) request = CookiedRequestFactory.CreateHttpWebRequest(wilmaUrl + "index_json");
            else request = CookiedRequestFactory.CreateHttpWebRequest(wilmaUrl + service);

            // Serialize the response to json

            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            
            // Get the login status from the service
            response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            string jsonResponseString = reader.ReadToEnd();
            values = JsonConvert.DeserializeObject<IndexJson>(jsonResponseString);
            reader.Close();
            response.Close();

            // DO the HASH
            ComputeHash(values);
              
            return jsonResponseString;
        }

  

        /// <summary>
        /// Kirjaudutaan evästeiden kanssa 
        /// </summary>
        /// <param name="LoginUrl"></param>
        /// <returns></returns>
        public string LoginWCookies(string LoginUrl)
        {    
            string loginParameters = "Login=" + username + "&Password=" + passwd + "&SessionId=" + values.SessionID + "&ApiKey=" + "sha1:" + hash + "&format=json";
            var bytes = Encoding.UTF8.GetBytes(loginParameters);

            request = CookiedRequestFactory.CreateHttpWebRequest(LoginUrl + "?" + loginParameters);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            // Send the json data to the Rest service
            var postStream = request.GetRequestStream();
            postStream.Write(bytes, 0, bytes.Length);
            postStream.Close();

            // Get the login status from the service
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var jsonResponseString = reader.ReadToEnd();
            values = JsonConvert.DeserializeObject<IndexJson>(jsonResponseString);
            reader.Close();
            response.Close();           
            return jsonResponseString;
        }

        public void ComputeHash(IndexJson indexJson)
        {
            string plain;
            // Hash                     
            byte[] temp;            
            plain = username + "|" + indexJson.SessionID + "|" + companySpesificKey;
            SHA1 sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            temp = sha.ComputeHash(Encoding.UTF8.GetBytes(plain));

            //storing hashed value into byte data type
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                sb.Append(temp[i].ToString("x2"));
            }
            hash = sb.ToString();           
        }



    }

}
