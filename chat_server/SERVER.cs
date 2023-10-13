using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;

namespace chat_server
{
    public partial class SERVER : Form
    {
        private const string bind_ip = "10.10.21.110";
        private const int bind_port = 25000;
        private Socket server_socket;
        private List<Socket> client_sockets = new List<Socket>();
        private List<byte[]> buffers = new List<byte[]>();

        public SERVER()
        {
            InitializeComponent();
            start_server();
        }

        private async Task start_server()
        {
            try
            {
                server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server_socket.Bind(new IPEndPoint(IPAddress.Parse(bind_ip), bind_port));
                server_socket.Listen(5);
                msg_display.AppendText("서버 시작" + Environment.NewLine);

                while (true)
                {
                    Socket client = await server_socket.AcceptAsync();
                    msg_display.AppendText($"{client} 접속" + Environment.NewLine);
                    client_sockets.Add(client);

                    byte[] buffer = new byte[1024];
                    buffers.Add(buffer);

                    receive_data(client, buffer);
                }
            }
            catch (SocketException e)
            {
                msg_display.AppendText("서버 시작 예외1 :" + e.Message + Environment.NewLine);
            }
            catch (Exception e)
            {
                msg_display.AppendText("서버 시작 예외2 :" + e.Message + Environment.NewLine);
            }
        }


        private async Task receive_data(Socket client, byte[] buffer)
        {
            int list_index = client_sockets.IndexOf(client);

            try
            {
                while (true)
                {
                    int byte_read = client.Receive(buffer);
                    if (byte_read > 0)
                    {
                        string receive_msg = Encoding.UTF8.GetString(buffer, 0, byte_read);
                        string[] split_msg = receive_msg.Split(new string[] { "/!@#$%/" }, StringSplitOptions.None);
                        msg_display.AppendText(split_msg[1] + Environment.NewLine);

                        string combinedMessage = $"{list_index}: {split_msg[1]}";
                        byte[] change_message = Encoding.UTF8.GetBytes(combinedMessage);
                        foreach (Socket socket in client_sockets)
                        {
                            socket.Send(change_message);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // 클라이언트와의 연결이 끊어질 때 클라이언트 리스트에서 제거
                client_sockets.RemoveAt(list_index);
                buffers.RemoveAt(list_index);
                msg_display.AppendText($"클라이언트 {list_index}와의 연결이 끊어졌습니다." + Environment.NewLine);
            }
        }
    }
}
