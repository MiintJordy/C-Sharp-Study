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
        public event Msg_Delivery to_server_msg;
        public event Trans_Delivery display_bar;

        private const string bind_ip = "10.10.21.110";
        private const int bind_port = 25000;
        private TcpListener server_socket;
        private Dictionary<string, NetworkStream> connect_info = new Dictionary<string, NetworkStream>();
        private byte[] buffer = new byte[1024];

        public async void start_server()
        {
            try
            {
                server_socket = new TcpListener(IPAddress.Parse(bind_ip), bind_port);
                server_socket.Start();
                to_server_msg("서버 시작");

                while (true)
                {
                    // 클라이언트 연결요청 대기(비동기)
                    TcpClient client = await server_socket.AcceptTcpClientAsync();

                    // 접속한 클라이언트 stream 저장
                    NetworkStream stream = client.GetStream();
                    to_server_msg("클라이언트 접속 ");

                    // 클라이언트로부터 받은 메시지를 처리하는 함수(비동기)
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
                            string combinedMessage = $"{split_msg[1]}: {split_msg[3]}";
                            byte[] change_message = Encoding.UTF8.GetBytes(combinedMessage);

                            // 상대방 아이디를 입력하지 않았을 때, 전체 메시지 전송
                            NetworkStream self_stream = connect_info[split_msg[1]];
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
                        // split_msg[3] = 파일 경로, split_msg[4] = 파일 크기
                        else if (split_msg[0] == "file")
                        {
                            // 파일 저장 경로
                            string save_path = Path.Combine("C:\\Users\\lms110\\source\\repos\\chat_server", split_msg[3]);
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
                                    // 읽은 데이터를 filestream에 시용
                                    fileStream.Write(file_buffer, 0, (int)read_byte);
                                    // 읽은 파일 데이터 크기를 업데이트
                                    total_read_file += read_byte;

                                    // Progress Bar 진행률 표시
                                    display_bar(total_read_file, file_size);
                                }

                                to_server_msg("파일 수신 완료: " + save_path);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                foreach (string exit_user in connect_info.Keys)
                {
                    if (connect_info[exit_user] == stream)
                    {
                        to_server_msg($"클라이언트 '{exit_user}'와의 연결이 끊어졌습니다.");
                        // 클라이언트와의 연결이 끊어질 때 클라이언트 리스트에서 제거
                        connect_info.Remove(exit_user);
                        break;
                    }
                }

                /*
                // 딕셔너리에서 연결 종료 후, 클라이언트 연결 정보 제거 확인
                foreach (var kvp in connect_info)
                {
                    to_server_msg($"Key: {kvp.Key}, Value: {kvp.Value}");
                }
                */
                to_server_msg(e.Message);
            }
        }
    }
}
