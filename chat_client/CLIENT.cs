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

namespace chat_client
{
    public partial class CLIENT : Form
    {
        private TcpClient client_socket;
        private NetworkStream stream;
        public CLIENT()
        {
            InitializeComponent();
            connect_server();
        }
        private async void connect_server()
        {
            try
            {
                client_socket = new TcpClient();
                client_socket.Connect("10.10.21.110", 25000);
                stream = client_socket.GetStream();
                msg_display.AppendText("서버 연결" + Environment.NewLine);

                await Readdata();
            }
            catch(Exception e)
            {
                msg_display.AppendText("연결 오류 :" + e.Message + Environment.NewLine);
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
                        msg_display.AppendText(receive_msg + Environment.NewLine);
                    }
                }
            }
            catch(Exception e)
            {
                msg_display.AppendText("연결 종료 :" + e.Message + Environment.NewLine);
            }
        }

        private async void msg_btn_Click(object sender, EventArgs e)
        {
            msg_display.AppendText("나 :" + msg_input.Text + Environment.NewLine);
            string message = "chamsg" + "/!@#$%/" + msg_input.Text;
            byte[] message_data = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(message_data, 0, message_data.Length);
            msg_input.Clear();
        }
    }
}
