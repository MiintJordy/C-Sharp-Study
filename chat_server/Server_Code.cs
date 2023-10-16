using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace chat_server
{
    delegate void Msg_Delivery(string msg);
    delegate void Trans_Delivery(long receive_size, long total_size);
    internal class Server_Code
    {
        // 서버 textBox에 문자를 append하는 대리자의 객체
        public event Msg_Delivery to_server_msg;
        // 파일 업로드가 진행 상황을 progress_bar에 백분율로 표시하는 대리자의 객체
        public event Trans_Delivery display_bar;

        // 서버 ip 주소
        private const string bind_ip = "10.10.21.110";
        // 서버 port
        private const int bind_port = 25000;
        // 서버 소켓을 저장할 변수
        private TcpListener server_socket;
        // 클라이언트 닉네임과 생성되는 스트림에 대한 Dictionary
        private Dictionary<string, NetworkStream> connect_info = new Dictionary<string, NetworkStream>();
        // 수신한 데이터를 받을 버퍼
        private byte[] buffer = new byte[1024];

        // 서버 시작 함수
        public async void start_server()
        {
            try
            {
                // 서버 소켓 생성
                server_socket = new TcpListener(IPAddress.Parse(bind_ip), bind_port);
                server_socket.Start();
                to_server_msg("서버 시작");

                while (true)
                {
                    // (비동기)클라이언트 연결요청 대기
                    TcpClient client = await server_socket.AcceptTcpClientAsync();

                    // 접속한 클라이언트 stream 저장
                    NetworkStream stream = client.GetStream();
                    to_server_msg("클라이언트 접속 ");

                    // (비동기)클라이언트로부터 받은 메시지를 처리하는 함수
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

        // 데이터 수신 함수
        private async void receive_data(NetworkStream stream, byte[] buffer)
        {
            try
            {
                while (true) 
                {
                    // (비동기)stream을 통한 데이터 수신
                    int byte_read = await stream.ReadAsync(buffer, 0, buffer.Length);
                    // 읽을 데이터가 있을 때
                    if (byte_read > 0)
                    {
                        // buffer[1024]만큼 수신한 데이터를 string으로 변환
                        string receive_msg = Encoding.UTF8.GetString(buffer, 0, byte_read);
                        // "/!@#$%/"를 구분자로 receive_msg를 split하여 배열에 저장
                        string[] split_msg = receive_msg.Split(new string[] { "/!@#$%/" }, StringSplitOptions.None);

                        // 로그인 정보
                        // split_msg[0] = login 데이터 구분자
                        // split_msg[1] = 로그인 id
                        if (split_msg[0] == "login")
                        {
                            // Dictionary에 nickname(key), 생성된 stream(value) 저장
                            connect_info.Add(split_msg[1], stream);
                        }
                        // chamsg split 정보
                        // split_msg[0] = chamsg 데이터 구분자
                        // split_msg[1] = 발신 id, split_msg[2] = 수신 id, split_msg[3] = 메시지 내용
                        else if (split_msg[0] == "chamsg")
                        {
                            // 전송할 메시지 결합
                            string combinedMessage = $"{split_msg[0] + "/!@#$%/" + split_msg[1]}: {split_msg[3]}";
                            // 전송할 메시지 변환
                            byte[] change_message = Encoding.UTF8.GetBytes(combinedMessage);

                            // 메시지를 보낸 유저의 스트림
                            NetworkStream self_stream = connect_info[split_msg[1]];

                            // 수신할 아이디를 입력하지 않았을 때, 전체 메시지 전송
                            if (split_msg[2] == "") 
                            {
                                foreach (NetworkStream send_stream in connect_info.Values)
                                {
                                    // 보낸 유저를 제외하고
                                    if (send_stream != self_stream)
                                    {
                                        await send_stream.WriteAsync(change_message, 0, change_message.Length);
                                    }
                                }
                            }
                            // 상대방 아이디를 입력했을 때, 특정 유저에게만 메시지 전송
                            else
                            {
                                NetworkStream select_stream;
                                connect_info.TryGetValue(split_msg[2], out select_stream);
                                await select_stream.WriteAsync(change_message, 0, change_message.Length);
                            }
                        }

                        // file split 정보
                        // split_msg[0] = file 데이터 구분자
                        // split_msg[1] = 발신 id, split_msg[2] = 수신 id
                        // split_msg[3] = 파일 이름, split_msg[4] = 파일 크기
                        else if (split_msg[0] == "file")
                        {
                            // 파일 경로 = 폴더 경로 + 파일 이름
                            string save_path = Path.Combine("C:\\Users\\lms110\\source\\repos\\chat_server", split_msg[3]);
                            // 수신자가 없을 때, 서버에 저장
                            if (split_msg[2] == "")
                            {
                                // (대리자)파일 저장 경로
                                to_server_msg("파일이름 : " + split_msg[3]);
                                // 전송받을 파일 크기
                                long file_size = long.Parse(split_msg[4]);
                                // 현재까지 받은 데이터 크기
                                long total_read_file = 0;

                                // 파일을 저장하기 위한 FileStream
                                // 저장경로에 파일 생성 및 쓰기 엑세스 권한 설정
                                using (FileStream fileStream = new FileStream(save_path, FileMode.Create, FileAccess.Write))
                                {
                                    // 버퍼 크기 설정
                                    byte[] file_buffer = new byte[1024];
                                    // 파일을 읽을 변수 선언
                                    long read_byte;

                                    // 파일 크기가 전체 크기보다 작을 때
                                    while (total_read_file < file_size)
                                    {
                                        // 데이터를 받아서 file_buffer에 저장
                                        read_byte = stream.Read(file_buffer, 0, 1024);
                                        // 읽은 데이터를 filestream에 사용
                                        fileStream.Write(file_buffer, 0, (int)read_byte);
                                        // 읽은 파일 데이터 크기를 업데이트
                                        total_read_file += read_byte;

                                        // (대리자)Progress Bar 진행률 표시
                                        display_bar(total_read_file, file_size);
                                    }

                                    to_server_msg("파일 수신 완료: " + save_path);
                                }
                            }
                            // 수신자가 있을 때, 서버를 경유해서 전송
                            else
                            {
                                // 발신 유저의 stream을 저장할 변수
                                NetworkStream order_stream;
                                // 수신 유저의 stream을 저장할 변수
                                NetworkStream select_stream;
                                // 유저의 아이디로 dictionary에서 value 스트림값을 얻음
                                connect_info.TryGetValue(split_msg[1], out order_stream);
                                connect_info.TryGetValue(split_msg[2], out select_stream);

                                // 파일정보 : 구분자 + 발신자 + 파일 이름 + 파일 크기
                                string file_info = "file" + "/!@#$%/" + split_msg[1] + "/!@#$%/" +
                                    split_msg[3] + "/!@#$%/" + split_msg[4];

                                // 전송할 파일정보 변환
                                byte[] message_data = Encoding.UTF8.GetBytes(file_info);

                                // 수신 유저에게 파일정보 전송
                                // 수신유저 stremam으로 Write(전송할 메시지, 처음부터, 길이까지)
                                select_stream.Write(message_data, 0, message_data.Length);

                                // 수신한 파일 크기를 저장할 변수 선언
                                long total_file_size = 0;

                                // 수신할 실제 파일 크기
                                long file_size = long.Parse(split_msg[4]);

                                // 파일을 분할해서 받을 buffer 선언
                                byte[] send_buffer = new byte[1024];

                                // buffer로 읽어온 데아터 크기를 저장할 변수 선언
                                long read_Byte = 0;

                                // memoryStream 객체 생성
                                MemoryStream memoryStream = new MemoryStream();

                                // 수신한 파일 크기 < 실제 파일 크기
                                while (total_file_size < file_size)
                                {
                                    // 실제 읽은 바이트 수 = sendbuffer로 길이 0부터 1024만큼 읽기
                                    read_Byte = stream.Read(send_buffer, 0, 1024);
                                    // send_buffer에 있는 데이터를 생성한 memoryStream 객체에 저장
                                    memoryStream.Write(send_buffer, 0, (int)read_Byte);
                                    // 수신할 파일 크기 += 실제 읽은 바이트 수
                                    total_file_size += read_Byte;

                                    // progressbar에 진행을 백분율로 표시
                                    display_bar(total_file_size, file_size);
                                }

                                // client에서 data를 수신하자마자 구분자를 찾기 위해 1024만큼 변환하여
                                // 파일 데이터가 소실되어 0으로 이루어진 1024byte 배열을 더하여 전송
                                byte[] divide = new byte[1024];
                                byte[] fileData = divide.Concat(memoryStream.ToArray()).ToArray();

                                // 수신유저 stream에 fileData를 0부터 길이까지 전송
                                select_stream.Write(fileData, 0, fileData.Length);

                                to_server_msg("파일 전송 완료: " + split_msg[2]);
                                }
                            }
                        }
                    }
                }
            catch (Exception e)
            {
                // Dictionary에 저장된 strema(key 값)을 전체 반복
                foreach (string exit_user in connect_info.Keys)
                {
                    // 종료한 유저의 아이디로 얻은 value(stream)이 종료한 유저의 stream과 같을 때
                    if (connect_info[exit_user] == stream)
                    {
                        to_server_msg($"클라이언트 '{exit_user}'와의 연결이 끊어졌습니다.");
                        // 클라이언트와의 연결이 끊어질 때 클라이언트 리스트에서 제거
                        connect_info.Remove(exit_user);
                        break;
                    }
                }
                to_server_msg(e.Message);
            }
        }
    }
}
