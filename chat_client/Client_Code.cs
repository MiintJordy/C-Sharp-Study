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
                    string select_file_path = search_dialog.FileName;
                    send_msg("선택 파일 :" + select_file_path);
                    // 파일 이름
                    string select_file_name = Path.GetFileName(select_file_path);
                    // 파일의 크기
                    long select_file_size = new FileInfo(select_file_path).Length;

                    // 파일 정보 결합
                    string file_info = divide + "/!@#$%/" + who + "/!@#$%/" +
                        select_file_name + "/!@#$%/" + select_file_size.ToString();
                    send_msg("파일 정보 :" + file_info);

                    // 파일 스트림 열기
                    using (FileStream fileStream = new FileStream(select_file_path, FileMode.Open, FileAccess.Read))
                    {
                        // 서버 전송을 위한 형태 변환
                        byte[] message_data = Encoding.UTF8.GetBytes(file_info);
                        stream.Write(message_data, 0, message_data.Length);

                        // 파일 전송
                        byte[] file_buffer = new byte[1024];
                        int read_file_byte;
                        // 전송된 데이터 크기를 담을 변수
                        long total_byte = 0;

                        while ((read_file_byte = fileStream.Read(file_buffer, 0, file_buffer.Length)) > 0)
                        {
                            // file_buffer가 read_file_byte에 담긴 데이터를 읽어서 stream으로 전송
                            stream.Write(file_buffer, 0, read_file_byte);

                            // 전송된 바이트 수 업데이트
                            total_byte += read_file_byte;
                            send_msg("현재까지 전송된 파일 크기: " + total_byte);
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
