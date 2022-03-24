using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.ComponentModel;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Diagnostics;
using HttpServerImpl.Server.Abstractions;
using HttpServerImpl.Server.Core;

namespace HttpServerImpl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IHostBuilder hostBuilder = new HostBuilder().ConfigureDefaults(args);
            hostBuilder.ConfigureServices((builderContext, servicesCollection) =>
            {
                IServer serverInstance = new ServerBuilder()
                                        .ConfigureDefaults()
                                        .Build();
                List<IHostedService> hostedServices = new List<IHostedService>();
                hostedServices.Add(serverInstance);
                // servicesCollection.AddSingleton<IServer>(serverInstance);
                servicesCollection.AddSingleton<IEnumerable<IHostedService>>(hostedServices);
            });
            IHost host = hostBuilder.Build();
            host.Run();
            //Console.ReadKey();
        }
    }
}
