namespace server
{
    partial class Server
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
            this.label1 = new System.Windows.Forms.Label();
            this.msg_title = new System.Windows.Forms.Label();
            this.msg_bar = new System.Windows.Forms.TextBox();
            this.msg_btn = new System.Windows.Forms.Button();
            this.dis_trans = new System.Windows.Forms.ProgressBar();
            this.msg_status = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // msg_title
            // 
            this.msg_title.AutoSize = true;
            this.msg_title.Location = new System.Drawing.Point(29, 18);
            this.msg_title.Name = "msg_title";
            this.msg_title.Size = new System.Drawing.Size(49, 12);
            this.msg_title.TabIndex = 1;
            this.msg_title.Text = "메시지 :";
            // 
            // msg_bar
            // 
            this.msg_bar.Location = new System.Drawing.Point(85, 13);
            this.msg_bar.Name = "msg_bar";
            this.msg_bar.Size = new System.Drawing.Size(254, 21);
            this.msg_bar.TabIndex = 2;
            // 
            // msg_btn
            // 
            this.msg_btn.Location = new System.Drawing.Point(358, 12);
            this.msg_btn.Name = "msg_btn";
            this.msg_btn.Size = new System.Drawing.Size(75, 23);
            this.msg_btn.TabIndex = 3;
            this.msg_btn.Text = "전송";
            this.msg_btn.UseVisualStyleBackColor = true;
            // 
            // dis_trans
            // 
            this.dis_trans.Location = new System.Drawing.Point(31, 57);
            this.dis_trans.Name = "dis_trans";
            this.dis_trans.Size = new System.Drawing.Size(402, 23);
            this.dis_trans.TabIndex = 4;
            // 
            // msg_status
            // 
            this.msg_status.Location = new System.Drawing.Point(31, 99);
            this.msg_status.Multiline = true;
            this.msg_status.Name = "msg_status";
            this.msg_status.ReadOnly = true;
            this.msg_status.Size = new System.Drawing.Size(402, 334);
            this.msg_status.TabIndex = 5;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 450);
            this.Controls.Add(this.msg_status);
            this.Controls.Add(this.dis_trans);
            this.Controls.Add(this.msg_btn);
            this.Controls.Add(this.msg_bar);
            this.Controls.Add(this.msg_title);
            this.Controls.Add(this.label1);
            this.Name = "Server";
            this.Text = "SERVER";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label msg_title;
        private System.Windows.Forms.TextBox msg_bar;
        private System.Windows.Forms.Button msg_btn;
        private System.Windows.Forms.ProgressBar dis_trans;
        private System.Windows.Forms.TextBox msg_status;
    }
}

