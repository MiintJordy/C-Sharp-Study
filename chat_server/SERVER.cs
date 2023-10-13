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
            Server_Code server = new Server_Code();
            server.to_server_msg += new Msg_Delivery(Append_Text_Func);
            server.start_server();
        }

        private void Append_Text_Func(string msg)
        {
            msg_display.AppendText(msg + Environment.NewLine);
        }

        private void msg_btn_Click(object sender, EventArgs e)
        {
            msg_input.Clear();
        }
    }
}
