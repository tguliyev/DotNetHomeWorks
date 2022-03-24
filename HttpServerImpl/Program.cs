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

namespace HttpServerImpl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // IHostBuilder hostBuilder = new HostBuilder().ConfigureDefaults(args);
            // IHost host = hostBuilder.Build();
            // var hostedServices = host.Services.GetService<IEnumerable<IHostedService>>();
            // foreach (var hostedService in hostedServices)
            // {
            //     Console.WriteLine(hostedService.GetType());
            // }

            HttpListener server = new HttpListener();
            server.Prefixes.Add(@"http://127.0.0.1:80/");
            server.Prefixes.Add(@"http://127.1.1.255:80/");
            server.Start();
            System.Console.WriteLine("Hello My Incredible Future!!!");
        }
    }
}
