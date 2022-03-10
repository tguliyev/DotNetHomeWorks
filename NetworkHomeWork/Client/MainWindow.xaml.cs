using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using Shared.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using Shared.Tools;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string IP_STRING = "127.0.0.1";
        private const int PORT = 8080;
        
        private byte[] buffer;

        private Socket client;
        private IPEndPoint serverIPEndpoint;
        
        public MainWindow()
        {
            InitializeComponent();

            buffer = new byte[65000];
            this.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            
            IPAddress? ip = null;
            if (!IPAddress.TryParse(IP_STRING, out ip))
                return;

            this.serverIPEndpoint = new IPEndPoint(ip, PORT);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectToServer();

            Task.Run(HandleServerResponces);
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await SendCredentialsToServer(RequestTypes.LOGIN);
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            await SendCredentialsToServer(RequestTypes.CREATE);
        }

        private void ConnectToServer()
        {
            this.client.Connect(this.serverIPEndpoint);
        }

        private async void HandleServerResponces()
        {
            while (true)
            {
                try
                {
                    int dataInBytesLenght = await client.ReceiveAsync(buffer: this.buffer, socketFlags: SocketFlags.None);
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, dataInBytesLenght);
                    MessageBox.Show(receivedData);
                }
                catch (Exception)
                {
                    MessageBox.Show("Disconnected from server...");
                    break;
                }
                
            }    
        }

        private async Task SendCredentialsToServer(RequestTypes requestType)
        {
            UserCredentials userCredentials = new UserCredentials(Name: UserNameTextBox.Text, Password: PasswordTextBox.Text);
            
            Request request = new Request(credentials: userCredentials, requestType: requestType);

            string data = JsonSerializer.Serialize(request);
            byte[] dataInBytes = Encoding.UTF8.GetBytes(data);

            await this.client.SendAsync(buffer: dataInBytes, socketFlags: SocketFlags.None);
        }

    }
}