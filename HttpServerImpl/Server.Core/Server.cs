using System.Net;
using Microsoft.Extensions.Hosting;
using HttpServerImpl.Server.Abstractions;

namespace HttpServerImpl.Server.Core
{
    internal class Server : IServer
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
            // while(true)
            // {
            //     System.Console.WriteLine("Hello My Incredible Future!!!");
            //     Thread.Sleep(2000);
            // }
            return Task.Run(() =>
            {
                Console.WriteLine("Hello My Incredible Future!!!");
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("App Gracefully Stopped!!!");
            });
        }
    }
}