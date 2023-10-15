namespace chat_client
{
    partial class CLIENT
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.msg_title = new System.Windows.Forms.Label();
            this.msg_input = new System.Windows.Forms.TextBox();
            this.msg_btn = new System.Windows.Forms.Button();
            this.file_btn = new System.Windows.Forms.Button();
            this.progress_status = new System.Windows.Forms.ProgressBar();
            this.msg_display = new System.Windows.Forms.TextBox();
            this.id_title = new System.Windows.Forms.Label();
            this.my_id = new System.Windows.Forms.TextBox();
            this.btn_connect = new System.Windows.Forms.Button();
            this.user_title = new System.Windows.Forms.Label();
            this.user_id = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // msg_title
            // 
            this.msg_title.AutoSize = true;
            this.msg_title.Location = new System.Drawing.Point(13, 46);
            this.msg_title.Name = "msg_title";
            this.msg_title.Size = new System.Drawing.Size(49, 12);
            this.msg_title.TabIndex = 0;
            this.msg_title.Text = "메시지 :";
            // 
            // msg_input
            // 
            this.msg_input.Location = new System.Drawing.Point(68, 41);
            this.msg_input.Name = "msg_input";
            this.msg_input.Size = new System.Drawing.Size(367, 21);
            this.msg_input.TabIndex = 1;
            // 
            // msg_btn
            // 
            this.msg_btn.Location = new System.Drawing.Point(450, 40);
            this.msg_btn.Name = "msg_btn";
            this.msg_btn.Size = new System.Drawing.Size(75, 23);
            this.msg_btn.TabIndex = 2;
            this.msg_btn.Text = "전송";
            this.msg_btn.UseVisualStyleBackColor = true;
            this.msg_btn.Click += new System.EventHandler(this.msg_btn_Click);
            // 
            // file_btn
            // 
            this.file_btn.Location = new System.Drawing.Point(450, 5);
            this.file_btn.Name = "file_btn";
            this.file_btn.Size = new System.Drawing.Size(75, 23);
            this.file_btn.TabIndex = 3;
            this.file_btn.Text = "업로드";
            this.file_btn.UseVisualStyleBackColor = true;
            this.file_btn.Click += new System.EventHandler(this.file_btn_Click);
            // 
            // progress_status
            // 
            this.progress_status.Location = new System.Drawing.Point(15, 73);
            this.progress_status.Name = "progress_status";
            this.progress_status.Size = new System.Drawing.Size(510, 23);
            this.progress_status.TabIndex = 4;
            // 
            // msg_display
            // 
            this.msg_display.Location = new System.Drawing.Point(15, 109);
            this.msg_display.Multiline = true;
            this.msg_display.Name = "msg_display";
            this.msg_display.ReadOnly = true;
            this.msg_display.Size = new System.Drawing.Size(510, 220);
            this.msg_display.TabIndex = 5;
            // 
            // id_title
            // 
            this.id_title.AutoSize = true;
            this.id_title.Location = new System.Drawing.Point(13, 12);
            this.id_title.Name = "id_title";
            this.id_title.Size = new System.Drawing.Size(49, 12);
            this.id_title.TabIndex = 6;
            this.id_title.Text = "닉네임 :";
            // 
            // my_id
            // 
            this.my_id.Location = new System.Drawing.Point(69, 7);
            this.my_id.Name = "my_id";
            this.my_id.Size = new System.Drawing.Size(111, 21);
            this.my_id.TabIndex = 7;
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(189, 6);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(75, 23);
            this.btn_connect.TabIndex = 8;
            this.btn_connect.Text = "접속";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // user_title
            // 
            this.user_title.AutoSize = true;
            this.user_title.Location = new System.Drawing.Point(270, 11);
            this.user_title.Name = "user_title";
            this.user_title.Size = new System.Drawing.Size(49, 12);
            this.user_title.TabIndex = 9;
            this.user_title.Text = "상대방 :";
            // 
            // user_id
            // 
            this.user_id.Location = new System.Drawing.Point(323, 6);
            this.user_id.Name = "user_id";
            this.user_id.Size = new System.Drawing.Size(112, 21);
            this.user_id.TabIndex = 10;
            // 
            // CLIENT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 344);
            this.Controls.Add(this.user_id);
            this.Controls.Add(this.user_title);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.my_id);
            this.Controls.Add(this.id_title);
            this.Controls.Add(this.msg_display);
            this.Controls.Add(this.progress_status);
            this.Controls.Add(this.file_btn);
            this.Controls.Add(this.msg_btn);
            this.Controls.Add(this.msg_input);
            this.Controls.Add(this.msg_title);
            this.Name = "CLIENT";
            this.Text = "Clinet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label msg_title;
        private System.Windows.Forms.TextBox msg_input;
        private System.Windows.Forms.Button msg_btn;
        private System.Windows.Forms.Button file_btn;
        private System.Windows.Forms.ProgressBar progress_status;
        private System.Windows.Forms.TextBox msg_display;
        private System.Windows.Forms.Label id_title;
        private System.Windows.Forms.TextBox my_id;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Label user_title;
        private System.Windows.Forms.TextBox user_id;
    }
}

