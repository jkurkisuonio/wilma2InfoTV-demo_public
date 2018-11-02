using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Primus_Json
{
  public  class IndexJson
    {
        public string LoginResult { get; set; }
        public string SessionID { get; set; }
        public string ApiVersion { get; set; }
        public List<string> Workers { get; set; }
    }
}
