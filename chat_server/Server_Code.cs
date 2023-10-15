using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace chat_server
{
    delegate void Msg_Delivery(string msg);
    internal class Server_Code
    {
        public event Msg_Delivery to_server_msg;

        private const string bind_ip = "10.10.21.110";
        private const int bind_port = 25000;
        private TcpListener server_socket;
        private List<TcpClient> client_sockets = new List<TcpClient>();
        private List<NetworkStream> client_streams = new List<NetworkStream>();
        private Dictionary<string, NetworkStream> connect_info = new Dictionary<string, NetworkStream>();

        public async void start_server()
        {
            try
            {
                server_socket = new TcpListener(IPAddress.Parse(bind_ip), bind_port);
                server_socket.Start();
                to_server_msg("서버 시작");

                while (true)
                {
                    TcpClient client = await server_socket.AcceptTcpClientAsync();
                    client_sockets.Add(client);

                    NetworkStream stream = client.GetStream();
                    client_streams.Add(stream);
                    to_server_msg("클라이언트 접속 ");
                    byte[] buffer = new byte[1024];

                    receive_data(stream, buffer);

                }
            }
            catch (SocketException e)
            {
                to_server_msg("서버 시작 예외1: " + e.Message);
            }
            catch (Exception e)
            {
                to_server_msg("서버 시작 예외2: " + e.Message);
            }
        }

        private async void receive_data(NetworkStream stream, byte[] buffer)
        {
            try
            {
                while (true) 
                {
                    int byte_read = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (byte_read > 0)
                    {
                        string receive_msg = Encoding.UTF8.GetString(buffer, 0, byte_read);
                        string[] split_msg = receive_msg.Split(new string[] { "/!@#$%/" }, StringSplitOptions.None);
                        foreach (string split in split_msg)
                        {
                            to_server_msg(split);
                        }


                        // 로그인 정보
                        // split_msg[0] = login 데이터 구분자
                        // split_msg[1] = 로그인 id
                        if (split_msg[0] == "login")
                        {
                            connect_info.Add(split_msg[1], stream);
                        }
                        // chamsg split 정보
                        // split_msg[0] = chamsg 데이터 구분자
                        // split_msg[1] = 발신 id, split_msg[2] = 수신 id, split_msg[3] = 메시지 내용
                        else if (split_msg[0] == "chamsg")
                        {
                            to_server_msg(split_msg[1]);
                            string combinedMessage = $"{split_msg[1]}: {split_msg[3]}";
                            byte[] change_message = Encoding.UTF8.GetBytes(combinedMessage);

                            NetworkStream self_stream;
                            connect_info.TryGetValue(split_msg[1], out self_stream);
                            if (split_msg[2] == "") 
                            {
                                foreach (NetworkStream send_stream in connect_info.Values)
                                {
                                    if (send_stream != self_stream)
                                    {
                                        await send_stream.WriteAsync(change_message, 0, change_message.Length);
                                    }
                                }
                            }
                            else
                            {
                                NetworkStream select_stream;
                                connect_info.TryGetValue(split_msg[2], out select_stream);
                                await select_stream.WriteAsync(change_message, 0, change_message.Length);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // 클라이언트와의 연결이 끊어질 때 클라이언트 리스트에서 제거
                //connect_info.Remove(list_index);
                //connect_info.Remove(list_index);
                //to_server_msg($"클라이언트와의 연결이 끊어졌습니다.");
                to_server_msg(e.Message);
            }
        }
    }
}
