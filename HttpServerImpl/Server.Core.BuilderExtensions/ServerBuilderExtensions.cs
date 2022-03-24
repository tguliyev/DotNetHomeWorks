using System;
using System.Net;
using HttpServerImpl.Server.Abstractions;

namespace HttpServerImpl.Server.Core.Exstensions
{
    public static class ServerBuilderExtension
    {
        public static IServerBuilder UseUriPrefix(this IServerBuilder serverBuilder, string uriPrefix)
        {
            return serverBuilder.ConfigureServerConfiguration(serverOptions =>
            {
                serverOptions.UriPrefix = uriPrefix;
            });
        }

        public static IServerBuilder UseAuthenticationScheme(this IServerBuilder serverBuilder, AuthenticationSchemes authScheme)
        {
            return serverBuilder.ConfigureServerConfiguration(serverOptions =>
            {
                serverOptions.AuthScheme = authScheme;
            });
        }

        public static IServerBuilder ConfigureDefaults(this IServerBuilder serverBuilder)
        {
            return serverBuilder.UseUriPrefix(@"http://127.0.0.1:80/")
                            .UseAuthenticationScheme(AuthenticationSchemes.None);
        }
    }
}