using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;



namespace server
{
    public partial class Server : Form
    {
        private string db_address = "Server=127.0.0.1;Database=project;User=root;Password=qwer1234;";
        private TcpListener server_socket;

        public Server()
        {
            InitializeComponent();
            InitializeAsync();
        }
        private async Task InitializeAsync()
        {
            await database_connect();
            await start_server();
        }

        private async Task database_connect()
        {
            using (MySqlConnection connection = new MySqlConnection(db_address))
            {
                try
                {
                    await connection.OpenAsync();
                    msg_status.AppendText("MySQL 데이터베이스에 연결되었습니다.");
                }
                catch (Exception ex)
                {
                    msg_status.AppendText($"예외 발생: {ex.GetType().Name}, 메시지: {ex.Message}");
                }
            }
        }
        private async Task start_server()
        {
            try
            {
                string bind_ip = "10.10.21.110";
                const int bind_port = 25000;

                IPEndPoint server_address = new IPEndPoint(IPAddress.Parse(bind_ip), bind_port);
                server_socket = new TcpListener(server_address);
                server_socket.Start();
                msg_status.AppendText("서버 시작");
                
                while (true)
                {
                    TcpClient client_socket = await server_socket.AcceptTcpClientAsync();
                    msg_status.AppendText("클라이언트 접속 : " + ((IPEndPoint)client_socket.Client.RemoteEndPoint).ToString());

                    await video_receive(client_socket);
                    await video_send(client_socket);
                }
            }

            catch(SocketException ex)
            {
                msg_status.AppendText("소켓 예외 : " + ex.Message);
            }

            catch(Exception ex)
            {
                msg_status.AppendText("예외 : " + ex.Message);
            }
        }

        private void add_text(string message)
        {
            msg_status.AppendText(message + Environment.NewLine);
        }

        private async Task video_send(TcpClient client)
        {
            using (MySqlConnection connection = new MySqlConnection(db_address))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM boktube", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(reader.GetOrdinal("TITLE"));
                            string video = reader.GetString(reader.GetOrdinal("VIDEO"));

                            byte[] nameBytes = Encoding.UTF8.GetBytes(name);
                            byte[] videoBytes = Encoding.UTF8.GetBytes(video);

                            using (NetworkStream stream = client.GetStream())
                            {
                                byte[] NameBytes = BitConverter.GetBytes(nameBytes.Length);
                                byte[] VideoBytes = BitConverter.GetBytes(videoBytes.Length);

                                await stream.WriteAsync(NameBytes, 0, NameBytes.Length);
                                await stream.WriteAsync(VideoBytes, 0, VideoBytes.Length);

                                await stream.WriteAsync(nameBytes, 0, nameBytes.Length);
                                await stream.WriteAsync(videoBytes, 0, videoBytes.Length);
                            }
                            msg_status.AppendText("Name :" + name + ", video :" + video);
                        }
                    }
                }
            }
        }

        private async Task video_receive(TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string fileInfo = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                string[] fileInfoParts = fileInfo.Split('|');
                string fileName = fileInfoParts[0];
                msg_status.AppendText($"{fileName}");
                int fileLength = int.Parse(fileInfoParts[1]);

                byte[] fileBytes = new byte[fileLength];
                int totalBytesReceived = 0;

                while (totalBytesReceived < fileLength)
                {
                    bytesRead = await stream.ReadAsync(fileBytes, totalBytesReceived, fileLength - totalBytesReceived);
                    totalBytesReceived += bytesRead;
                }

                string savePath = Path.Combine("C:\\Users\\lms110\\Downloads\\1009sever", fileName);
                File.WriteAllBytes(savePath, fileBytes);

                msg_status.AppendText("저장경로 : " + savePath);

                using (MySqlConnection connection = new MySqlConnection(db_address))
                {
                    try
                    {
                        connection.Open();
                        using (MySqlCommand command = new MySqlCommand("INSERT INTO boktube (TITLE, VIDEO) VALUES (@name, @video)", connection))
                        {
                            command.Parameters.AddWithValue("@name", fileName);
                            command.Parameters.AddWithValue("@video", savePath);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (MySqlException ex)
                    {
                        msg_status.AppendText($"MySQL 예외 발생: {ex.Message}");
                        // 예외 처리 코드 추가
                    }
                    catch (Exception ex)
                    {
                        msg_status.AppendText($"예외 발생: {ex.Message}");
                        // 예외 처리 코드 추가
                    }
                }

                string response = "파일 업로드가 완료되었습니다.";
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);

                msg_status.AppendText($"파일 수신 및 저장 완료: {fileName}");
            }
        }
    }
}
