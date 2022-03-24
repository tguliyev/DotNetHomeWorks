using System;
using System.Net;
using HttpServerImpl.Server.Core;

namespace HttpServerImpl.Server.Abstractions
{
    public interface IServerBuilder
    {
        IServerBuilder ConfigureServerConfiguration(Action<ServerOptions> configDelegate);

        IServer Build();
    }
}