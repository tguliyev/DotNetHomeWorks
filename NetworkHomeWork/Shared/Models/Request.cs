using Shared.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Request
    {
        public UserCredentials? Credentials { get; set; }
        public RequestTypes RequestType { get; set; }

        public Request(UserCredentials? credentials = null, RequestTypes requestType = RequestTypes.LOGIN)
        {
            this.Credentials = credentials;
            this.RequestType = requestType;
        }
    }
}
