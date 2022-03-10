using Shared.Models;
using Server.Services;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Shared.Tools;

namespace Server
{
    public static class Program
    {
        private const string IP_STRING = "127.0.0.1";
        private const int PORT = 8080;

        private static Socket socket;
        private static IPAddress? ip;
        private static IPEndPoint endPoint;

        private static IEFService eFService = new EFService();

        static async Task Main(string[] args)
        {
            socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            if (!IPAddress.TryParse(IP_STRING, out ip))
            {
                System.Console.WriteLine("Invalid IP adress");
                return;
            }

            endPoint = new IPEndPoint(ip, PORT);

            await StartServer();
        }

        private static async Task StartServer()
        {
            socket.Bind(endPoint);
            socket.Listen(10);

            while (true)
            {
                Socket client = await socket.AcceptAsync();
                Console.WriteLine("User Connected...");

                ThreadPool.QueueUserWorkItem(async obj =>
                {
                    while (true)
                    {
                        try
                        {
                            byte[] buffer = new byte[65000];
                            int size = await client.ReceiveAsync(buffer, SocketFlags.None);
                            string data = Encoding.UTF8.GetString(buffer, 0, size);
                            Request? clientRequest = JsonSerializer.Deserialize<Request>(data);

                            if(clientRequest == null || clientRequest?.Credentials == null)
                                await SendMessageToClient(client, false);
                            else
                            {
                                if (clientRequest.RequestType == RequestTypes.LOGIN)
                                {
                                    if (eFService.CheckCredentials(clientRequest.Credentials))
                                        await SendMessageToClient(client, true);
                                    else
                                        await SendMessageToClient(client, false);
                                }
                                else if (clientRequest.RequestType == RequestTypes.CREATE)
                                {
                                    try
                                    {
                                        eFService.CreateUser(clientRequest.Credentials);
                                        await SendMessageToClient(client, true);
                                    }
                                    catch (Exception)
                                    {
                                        await SendMessageToClient(client, false);
                                    }
                                    
                                }
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("user disconnected...");
                            break;
                        }
                    }
                });
            }
        }

        private static async Task SendMessageToClient(Socket client, bool message)
        {
            byte[] messageInBytes = Encoding.UTF8.GetBytes(message.ToString());

            await client.SendAsync(messageInBytes, SocketFlags.None);
        }
    }
}