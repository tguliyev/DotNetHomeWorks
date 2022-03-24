using System;
using System.Net;

namespace HttpServerImpl.Server.Core
{
    public class ServerOptions
    {
        public static ServerOptions Default => new ServerOptions();
        public string UriPrefix { get; set; } 
        public AuthenticationSchemes AuthScheme { get; set; } 
    }
}