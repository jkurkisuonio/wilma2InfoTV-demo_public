using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Primus_Json
{
    public class LoginResultJson
    {
        /*  $LoginResult = $reply.LoginResult
            $WilmaId     = $reply.WilmaId
            $ApiVersion  = $reply.ApiVersion
            $FormKey     = $reply.FormKey
            $ConnectIds  = $reply.ConnectIds
            $Slug        = $reply.Slug
            $Name        = $reply.Name
            $Type        = $reply.Type
            $PrimusId    = $reply.PrimusId
            $School      = $reply.School */

        public string LoginResult { get; set; }
        public string WilmaId { get; set; }
        public string ApiVersion { get; set; }
        public string FormKey { get; set; }
        public List<string> ConnectIds { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PrimusId { get; set; }
        public string School { get; set; }
        public List<string> Exams { get; set; }
        public List<string> News { get; set; }
        public List<string> Groups { get; set; }
        public string Photo { get; set; }
        public bool EarlyEduUser { get; set; }
        public List<string> Roles { get; set; }
    }
}
