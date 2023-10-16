using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat_server
{
    public partial class SERVER : Form
    {
        public SERVER()
        {
            InitializeComponent();

            // Server Logic 클래스 객체 생성
            Server_Code server = new Server_Code();

            // 대리자는 함수만 더하면 됨 !
            // 수정 전
            // server.to_server_msg += new Msg_Delivery(Append_Text_Func);
            // server.display_bar += new Trans_Delivery(Display_Progress_Func);
            
            // 수정 후
            // 대리자 객체에 문자 출력 함수 연결
            server.to_server_msg += Append_Text_Func;
            // 대리자 객체에 progress bar 백분율 표시 함수 연결
            server.display_bar += Display_Progress_Func;

            // Server Logic 클래스 서버 시작 함수
            server.start_server();
        }

        // Form에 있는 Textbox에 문자를 출력하는 함수
        private void Append_Text_Func(string msg)
        {
            msg_display.AppendText(msg + Environment.NewLine);
        }

        // Form에 있는 progress bar에 파일 전송 상황을 백분율로 표시하는 함수
        private void Display_Progress_Func(long receive_size, long total_size)
        {
            // progress bar 최대값 100으로 설정
            progress_status.Maximum = 100;
            // 백분율 = (수신한 파일 크기 * 100) / 실제 파일 크기
            long percent = (receive_size * 100) / total_size;
            // progress bar에 백분율 값을 입력
            progress_status.Value = (int)percent;
        }
    }
}
