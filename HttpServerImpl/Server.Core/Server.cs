using System;
using System.Net;
using Microsoft.Extensions.Hosting;
using HttpServerImpl.Server.Abstractions;

namespace HttpServerImpl.Server.Core
{
    internal class Server : IServer, IHostedService
    {
        private readonly HttpListener listener;

        public Server(ServerOptions options)
        {
            listener = new HttpListener();
            listener.AuthenticationSchemes = options.AuthScheme;
            listener.Prefixes.Add(options.UriPrefix);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}