using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat_client
{
    // 대리자 선언
    delegate void Delivery_Msg(string msg);
    internal class Client_Code
    {
        // 대리자 객체 생성
        public event Delivery_Msg send_msg;

        // 클라이언트 소켓을 저장할 변수
        private TcpClient client_socket;
        // 클라이언트 stream을 저장할 변수
        private NetworkStream stream;
        // 저장할 파일 경로를 저장할 변수
        private string file_path;
        // 파일을 보낸 유저의 닉네임을 저장 변수
        private string file_send_user;
        // 수신할 파일 실제 크기를 저장할 변수
        private long file_size;

        // (비동기) 서버 연결 함수
        public async void connect_server()
        {
            try
            {
                // 클라이언트 소켓 초기화
                client_socket = new TcpClient();
                // 클라이언트 소켓 연결
                client_socket.Connect("10.10.21.110", 25000);
                // 클라이언트 stream 저장
                stream = client_socket.GetStream();
                // 대리자 함수
                send_msg("서버 연결");

                // 데이터 수신 함수 호출
                Readdata();
            }
            catch (Exception e)
            {
                send_msg("연결 오류 :" + e.Message);
            }
        }

        // 데이터를 수신하고 변환하는 함수
        private async void Readdata()
        {
            try
            {
                while (true)
                {
                    // 수신한 데이터를 담을 버퍼(1024byte)
                    byte[] buffer = new byte[1024];
                    // 읽은 바이트의 길이 = (비동기) stream에 있는 데이터를 buffer에 0부터 길이까지 담기
                    int byte_read = await stream.ReadAsync(buffer, 0, buffer.Length);
                    // 읽은 길이가 0보다 클 때
                    if (byte_read > 0)
                    {
                        // buffer에 있는 데이터를 0부터 읽은 바이트의 길이까지 string 변환
                        string receive_msg = Encoding.UTF8.GetString(buffer, 0, byte_read);

                        // 받은 데이터를 구분자 /!@#$%/ 으로 split하여 배열에 저장
                        // StringSplitOptions.None: 공백을 포함하여 저장
                        string[] split_msg = receive_msg.Split(new string[] { "/!@#$%/" }, StringSplitOptions.None);


                        // 기능 구분자가 chamsg 일 때(채팅 메시지)
                        if (split_msg[0] == "chamsg")
                        {
                            // 대리자 함수 실행(화면 출력)
                            send_msg(split_msg[1]);
                        }

                        // 서버 코드에서 2번에 걸쳐 write
                        // 1번째: 기능 구분자, 발신 유저 아이디, 파일 명, 실제 파일 크기
                        // 기능 구분자가 file 일 때(파일 업로드)
                        else if (split_msg[0] == "file")
                        {
                            // 발신한 유저의 아이디
                            file_send_user = split_msg[1];
                            // 파일 저장 경로 = 폴더 경로 + 파일 명
                            file_path = Path.Combine("C:\\Users\\lms110\\source\\repos\\chat_client", split_msg[2]);
                            // 전송받을 파일 크기를 long으로 형변환
                            file_size = long.Parse(split_msg[3]);
                        }

                        // 구분자가 없을 때, 파일 수신
                        // 2번째: 빈 1024 byte + 실제 파일
                        // Line 69에서 Line 62 buffer 크기만큼 변환하여 데이터가 사라진 채로 전송되기 때문에 빈 byte 전송
                        else
                        {
                            // 현재까지 받은 데이터 크기
                            long total_read_file = 0;
                            // 버퍼 크기 설정
                            byte[] file_buffer = new byte[1024];
                            // file_buffer가 읽은 byte 길이 저장
                            long read_byte = 0;
                            // 메모리 스트림 객체 생성
                            MemoryStream memoryStream = new MemoryStream();
                            
                            // 수신한 파일 크기가 실제 파일 크기보다 작을 때
                            while (total_read_file < file_size)
                            {
                                // 읽은 byte 길이 = filebuffer에 0부터 1024까지 저장
                                read_byte = stream.Read(file_buffer, 0, 1024);
                                // memoryStream에 file_buffer에 있는 0부터 읽은 길이까지 저장
                                memoryStream.Write(file_buffer, 0, (int)read_byte);
                                // 수신한 누적 파일 크기 += 읽은 byte 길이
                                total_read_file += read_byte;
                            }

                            // 파일 경로에 memoryStream에 저장된 byte array 저장
                            File.WriteAllBytes(file_path, memoryStream.ToArray());
                            
                            send_msg(file_send_user + "님 에게파일 수신 완료");

                        }
                    }
                }
            }
            catch (Exception e)
            {
                send_msg("연결 종료 :" + e.Message);
            }
        }

        // 메시지 발신 함수(기능 구분자, 수신/발신자 정보, 보낼 메시지)
        public async void Send_Message(string divide, string who, string change_msg)
        {
            // 전송할 메시지 정보 결합
            string message = divide + "/!@#$%/" + who + "/!@#$%/" + change_msg;
            // 전송할 메시지 byte 형변환
            byte[] message_data = Encoding.UTF8.GetBytes(message);
            // message_data의 0부터 길이까지의 byte 전송
            await stream.WriteAsync(message_data, 0, message_data.Length);
        }

        // 파일 전송 함수(기능 구분자, 수신/발신자 정보)
        public async void Send_File(string divide, string who)
        {
            
            // 파일 대화 상자 객체 생성
            OpenFileDialog search_dialog = new OpenFileDialog();
            // Video File 형식 혹은 전체 파일 Filter 설정
            search_dialog.Filter = "Video Files|*.mp4;*.avi;*.mkv|All Files|*.*";

            // 생성한 객체를 Show하고, 파일 대화상자에서 확인 버튼을 눌렀을 때
            if (search_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 파일 이름을 포함한 경로
                    string select_file_path = search_dialog.FileName;
                    // 파일 이름
                    string select_file_name = Path.GetFileName(select_file_path);
                    // 실제 파일의 크기
                    long select_file_size = new FileInfo(select_file_path).Length;

                    // 파일 정보 결합(기능 구분자, 수신/발신자, 파일 명, 파일 크기)
                    string file_info = divide + "/!@#$%/" + who + "/!@#$%/" +
                        select_file_name + "/!@#$%/" + select_file_size.ToString();

                    // 파일 스트림 열기
                    // using은 자원을 정리하기 위해서 사용되어 파일, 데이터베이스 연결, 네트워크 연결 등
                    // 자원을 사용한 후 자동으로 자원을 정리하고 해제하기 위해 사용
                    // FilreStream은 파일을 읽고 열고 쓸 수 있는 메서드와 속성을 제공
                    // FileMode
                    // FileMode.Create : 파일을 생성하고 열며, 이미 파일이 존재하면 덮어씀.
                    // FileMode.Open: 파일을 열고, 파일이 존재하지 않으면 예외가 발생
                    // File.OpenOrCreate: 파일을 열고 존재하지 않으면 생성
                    // FileAppend: 파일을 열고 파일이 존재하면 끝에서부터 데이터를 추가
                    // FileAccess
                    // FileAccess.Read: 파일을 읽기 전용으로 열고, 파일을 읽을 수 있으나 쓸 수 없음
                    // FileAccess.Write: 파일을 쓰기 전용으로 열고, 파일을 쓸 수 있으나 읽을 수 없음
                    // FileAccess.ReadWrite: 파일을 읽고 쓸 수 있음
                    using (FileStream fileStream = new FileStream(select_file_path, FileMode.Open, FileAccess.Read))
                    {
                        // 서버 전송을 위한 형태 변환
                        byte[] message_data = Encoding.UTF8.GetBytes(file_info);
                        //message_data의 byte를 0부터 길이까지 stream으로 전송
                        stream.Write(message_data, 0, message_data.Length);

                        // 파일 전송을 위한 버퍼 생성
                        byte[] file_buffer = new byte[1024];
                        // 읽은 바이트의 길이를 저장할 변수
                        int read_file_byte;
                        // 전송된 데이터 크기를 담을 변수
                        long total_byte = 0;

                        // fileStream의 데이터를 file_buffer에 0부터 길이까지 저장하여 읽은 길이가 0보다 클 때
                        while ((read_file_byte = fileStream.Read(file_buffer, 0, file_buffer.Length)) > 0)
                        {
                            // file_buffer가 0부터 read_file_byte만큼 읽어서 stream으로 전송
                            stream.Write(file_buffer, 0, read_file_byte);

                            // 누적 수신 파일 크기 += 읽은 파일 길이
                            total_byte += read_file_byte;
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
