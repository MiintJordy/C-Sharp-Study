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

                Readdata();
                //Send_File();
            }
            catch (Exception e)
            {
                send_msg("연결 오류 :" + e.Message);
            }
        }
        private async void Readdata()
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
        public async void Send_File(string divide, string who)
        {
            OpenFileDialog search_dialog = new OpenFileDialog();
            search_dialog.Filter = "Video Files|*.mp4;*.avi;*.mkv|All Files|*.*";

            if (search_dialog.ShowDialog() == DialogResult.OK)
            {
                try 
                {
                    // 파일 이름을 포함한 경로
                    string select_file_name = search_dialog.FileName;
                    send_msg("선택 파일 :" + select_file_name);
                    // 파일의 크기
                    long select_file_size = new FileInfo(select_file_name).Length;

                    // 파일 정보 결합
                    string file_info = divide + "/!@#$%/" + who + "/!@#$%/" +
                        select_file_name + "/!@#$%/" + select_file_size.ToString();
                    send_msg("파일 정보 :" + file_info);

                    // 파일 스트림 열기
                    using (FileStream fileStream = new FileStream(select_file_name, FileMode.Open, FileAccess.Read))
                    {
                        // 서버 전송을 위한 형태 변환
                        byte[] message_data = Encoding.UTF8.GetBytes(file_info);
                        stream.Write(message_data, 0, message_data.Length);

                        // 파일 전송
                        byte[] file_buffer = new byte[1024];
                        int bytesRead;

                        while ((bytesRead = fileStream.Read(file_buffer, 0, file_buffer.Length)) > 0)
                        {
                            stream.Write(file_buffer, 0, bytesRead);
                        }
                    }
                }
                catch (Exception e)
                {
                    send_msg("업로드 오류 :" + e.Message);
                }
            }
        }
    }
}
