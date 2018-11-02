using System;
using System.Net;
using System.Collections.Generic;

namespace SharedLibraries.ClientUtilities
{
    public class CookiedRequestFactory
    {
        // This dictionary keeps all the cookie containers for
        // each domain.
        private static Dictionary<string, CookieContainer> containers
            = new Dictionary<string, CookieContainer>();

        public static HttpWebRequest CreateHttpWebRequest(string url)
        {
            // Create a HttpWebRequest object
            var request = (HttpWebRequest)WebRequest.Create(url);

            // this gets the dmain part of from the url
            string domain = (new Uri(url)).GetLeftPart(UriPartial.Authority);

            // try to get a container from the dictionary, if it is in the
            // dictionary, use it. Otherwise, create a new one and put it
            // into the dictionary and use it.
            CookieContainer container;
            if (!containers.TryGetValue(domain, out container))
            {
                container = new CookieContainer();
                containers[domain] = container;
            }

            // Assign the cookie container to the HttpWebRequest object
            request.CookieContainer = container;

            return request;
        }
    }
}
