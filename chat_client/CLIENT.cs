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
        // Server_Logic.cs 파일 클래스의 객체 생성
        private Client_Code client = new Client_Code();
        // 유저의 로그인 아이디
        private string login_id;

        public CLIENT()
        {
            InitializeComponent();

            // 데이터를 수신받는 로직 + Form TextBox에 데이터 출력 결합
            client.send_msg += new Delivery_Msg(Append_Text_Func);
           
        }

        private void Append_Text_Func(string msg)
        {
            msg_display.AppendText(msg + Environment.NewLine);
        }
        private void msg_btn_Click(object sender, EventArgs e)
        {
            string temp = msg_input.Text;
            msg_display.AppendText("나 :" + temp + Environment.NewLine);

            // 발신자 + 수신자 정보 결합
            string msg_user_info = login_id + "/!@#$%/" + user_id.Text;
            client.Send_Message("chamsg", msg_user_info, temp);
            msg_input.Clear();
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            my_id.ReadOnly = true;
            client.connect_server();
            // 로그인 아이디 저장
            login_id = my_id.Text;
            client.Send_Message("login", login_id, "");
        }
        private void file_btn_Click(object sender, EventArgs e)
        {
            string file_user_info = login_id + "/!@#$%/" + user_id.Text;
            client.Send_File("file", file_user_info);
        }
    }
}
