using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Client : Form
    {
        private TcpClient client_socket;
        private NetworkStream stream;
        private OpenFileDialog find_file;

        string server_ip = "10.10.21.110";
        int server_port = 25000;
        public Client()
        {
            InitializeComponent();
            client_socket = new TcpClient();
            InitializeAsync();

            find_file = new OpenFileDialog();
            find_file.Filter = "Video Files|*.mp4;*.avi;*.mkv|All Files|*.*";
        }

        private async void InitializeAsync()
        {
            try
            {
                await client_socket.ConnectAsync(server_ip, server_port);
                stream = client_socket.GetStream();
                MessageBox.Show("서버 연결");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"연결 오류 : {ex.Message}");
            }
        }


        private void upload_btn_Click(object sender, EventArgs e)
        {
            if (find_file.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string select_file = find_file.FileName;
                    byte[] file_size = File.ReadAllBytes(select_file);

                    string file_name = Path.GetFileName(select_file);
                    string file_info = $"{file_name}|{file_size.Length}";
                    byte[] file_info_bytes = Encoding.UTF8.GetBytes(file_info);
                    stream.Write(file_info_bytes, 0, file_info_bytes.Length);

                    byte[] file_length_bytes = BitConverter.GetBytes(file_size.Length);
                    stream.Write(file_length_bytes, 0, file_length_bytes.Length);

                    stream.Write(file_size, 0, file_size.Length);

                    byte[] response_buff = new byte[1024];
                    int bytes_read = stream.Read(response_buff, 0, response_buff.Length);
                    string response = Encoding.UTF8.GetString(response_buff, 0, bytes_read);
                    MessageBox.Show($"서버 응답: {response}");
                }

                catch (Exception ex )
                {
                    MessageBox.Show($"업로드 오류 : {ex.Message}");
                }
            }
        }

        private async Task receive_data_func(object sender,DataReceivedEventArgs e)
        {
            try
            {
                byte[] byte_length = new byte[sizeof(int)];
                await stream.ReadAsync(byte_length, 0, sizeof(int));
                int data_length = BitConverter.ToInt32(byte_length, 0);

                byte[] data_byte = new byte[data_length];
                int bytes_read = 0;
                while (bytes_read < data_length)
                {
                    bytes_read += await stream.ReadAsync(data_byte, bytes_read, data_length - bytes_read);
                }

                int name_length = BitConverter.ToInt32(data_byte, 0);
                int video_length = BitConverter.ToInt32(data_byte, sizeof(int));

                byte[] name_byte = new byte[name_length];
                byte[] video_byte = new byte[video_length];

                Buffer.BlockCopy(data_byte, sizeof(int) * 2, name_byte, 0, name_length);
                Buffer.BlockCopy(data_byte, sizeof(int) * 2 + name_length, video_byte, 0, video_length);

                string name = Encoding.UTF8.GetString(name_byte);
                string video = Encoding.UTF8.GetString(video_byte);

                MessageBox.Show($"Name : {name}, Video: {video}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 수신 오류: {ex.Message}");
            }
        }
    }
}
