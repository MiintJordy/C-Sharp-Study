using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat_server
{
    public partial class SERVER : Form
    {
        private const string bind_ip = "10.10.21.110";
        private const int bind_port = 25000;
        private TcpListener server_socket;
        private List<TcpClient> client_sockets = new List<TcpClient>();
        private List<NetworkStream> client_streams = new List<NetworkStream>();
        private List<byte[]> buffers = new List<byte[]>();

        public SERVER()
        {
            InitializeComponent();
            start_server();
        }

        private async void start_server()
        {
            try
            {
                server_socket = new TcpListener(IPAddress.Parse(bind_ip), bind_port);
                server_socket.Start();
                msg_display.AppendText("서버 시작" + Environment.NewLine);

                while (true)
                {
                    TcpClient client = await server_socket.AcceptTcpClientAsync();
                    client_sockets.Add(client);

                    NetworkStream stream = client.GetStream();
                    msg_display.AppendText("클라이언트 접속 " + stream + Environment.NewLine);
                    client_streams.Add(stream);
                    byte[] buffer = new byte[1024];
                    buffers.Add(buffer);

                    receive_data(stream, buffer);
                }
            }
            catch (SocketException e)
            {
                msg_display.AppendText("서버 시작 예외1: " + e.Message + Environment.NewLine);
            }
            catch (Exception e)
            {
                msg_display.AppendText("서버 시작 예외2: " + e.Message + Environment.NewLine);
            }
        }

        private async void receive_data(NetworkStream stream, byte[] buffer)
        {
            int list_index = client_streams.IndexOf(stream);

            try
            {
                while (true)
                {
                    int byte_read = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (byte_read > 0)
                    {
                        string receive_msg = Encoding.UTF8.GetString(buffer, 0, byte_read);
                        string[] split_msg = receive_msg.Split(new string[] { "/!@#$%/" }, StringSplitOptions.None);

                        if (split_msg[0] == "chamsg")
                        {
                            msg_display.AppendText(split_msg[1] + Environment.NewLine);

                            string combinedMessage = $"{list_index}: {split_msg[1]}";
                            byte[] change_message = Encoding.UTF8.GetBytes(combinedMessage);
                            foreach (NetworkStream idx in client_streams)
                            {
                                if (idx != stream)
                                {
                                    await idx.WriteAsync(change_message, 0, change_message.Length);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // 클라이언트와의 연결이 끊어질 때 클라이언트 리스트에서 제거
                client_sockets.RemoveAt(list_index);
                client_streams.RemoveAt(list_index);
                buffers.RemoveAt(list_index);
                msg_display.AppendText($"클라이언트 {list_index}와의 연결이 끊어졌습니다." + Environment.NewLine);
            }
        }

        private void msg_btn_Click(object sender, EventArgs e)
        {
            msg_input.Clear();
        }
    }
}
