using System;
using System.Net;
using HttpServerImpl.Server.Abstractions;

namespace HttpServerImpl.Server.Core
{
    public class ServerBuilder : IServerBuilder
    {
        private List<Action<ServerOptions>> configureServerConfigActions = new List<Action<ServerOptions>>();
        private bool serverBuilt;

        public IServerBuilder ConfigureServerConfiguration(Action<ServerOptions> configDelegate)
        {
            configureServerConfigActions.Add(configDelegate ?? throw new ArgumentNullException(nameof(configDelegate)));
            return this;
        }

        public IServer Build()
        {
            if (serverBuilt)
                throw new InvalidOperationException("Server already built");
            
            serverBuilt = true;

            ServerOptions options = ServerOptions.Default;

            foreach (var configAction in configureServerConfigActions)
            {
                configAction.Invoke(options);
            }

            return new Server(options);
        }
    }
}