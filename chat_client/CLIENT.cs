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

        // 유저의 로그인 아이디를 저장할 변수 선언
        private string login_id;

        public CLIENT()
        {
            InitializeComponent();

            // 대리자 객체에 문자 출력 함수 결합
            client.send_msg += Append_Text_Func;
           
        }

        // Form의 TextBox에 문자를 출력하는 함수
        private void Append_Text_Func(string msg)
        {
            msg_display.AppendText(msg + Environment.NewLine);
        }
        
        // 메세지 "전송" 버튼 클릭 시, 작동하는 함수
        private void msg_btn_Click(object sender, EventArgs e)
        {
            // Textbox에 전송할 메시지를 temp에 저장
            string temp = msg_input.Text;
            msg_display.AppendText("나 :" + temp + Environment.NewLine);

            // 발신자 아이디 + 보낼 수신자 아이디 정보 결합
            string msg_user_info = login_id + "/!@#$%/" + user_id.Text;

            // Client Logic 객체의 메시지 전송 함수
            // (기능 구분자, 발신/수신 정보 결합, 전송할 메시지)
            client.Send_Message("chamsg", msg_user_info, temp);

            // 메시지 입력 Textbox에 문자 지우기
            msg_input.Clear();
        }

        // 서버 "연결" 버튼 클릭시, 작동하는 함수
        private void btn_connect_Click(object sender, EventArgs e)
        {
            // 서버 연결 클릭 시, 본인 id 입력 Textbox를 readonly로 설정 변경
            my_id.ReadOnly = true;
            
            // Client Logic 객체의 서버 연결 함수 
            client.connect_server();

            // 로그인 아이디 저장
            login_id = my_id.Text;

            // Client Logic 객체에 메시지 전송 함수(기능 구분자, 본인 아이디, 빈칸)
            client.Send_Message("login", login_id, "");
        }

        // 파일 "업로드" 버튼 클릭시 작동하는 함수
        private void file_btn_Click(object sender, EventArgs e)
        {
            // 본인 아이디, 구분자, 상대방 아이디
            string file_user_info = login_id + "/!@#$%/" + user_id.Text;
            // Client Logic 객체에 파일 전송 함수(기능 구분자, 발신/수신자 정보 결합)
            client.Send_File("file", file_user_info);
        }
    }
}
