using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat_client
{
    delegate void Delivery_Msg(string msg);
    internal class Client_Code
    {
        public event Delivery_Msg send_msg;

        private TcpClient client_socket;
        private NetworkStream stream;
        public async void connect_server()
        {
            try
            {
                // 서버 연결 요청: 클라이언트 소켓 생성 및 전송할 stream 생성
                client_socket = new TcpClient();
                client_socket.Connect("10.10.21.110", 25000);
                stream = client_socket.GetStream();
                send_msg("서버 연결");

                await Readdata();
            }
            catch (Exception e)
            {
                send_msg("연결 오류 :" + e.Message);
            }
        }
        private async Task Readdata()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int byte_read = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (byte_read > 0)
                    {
                        string receive_msg = Encoding.UTF8.GetString(buffer, 0, byte_read);
                        send_msg(receive_msg);
                    }
                }
            }
            catch (Exception e)
            {
                send_msg("연결 종료 :" + e.Message);
            }
        }

        public async void Send_Message(string divide, string who, string change_msg)
        {
            string message = divide + "/!@#$%/" + who + "/!@#$%/" + change_msg;
            byte[] message_data = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(message_data, 0, message_data.Length);
        }
    }
}
