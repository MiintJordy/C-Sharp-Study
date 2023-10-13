﻿namespace chat_server
{
    partial class SERVER
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
            this.progress_status = new System.Windows.Forms.ProgressBar();
            this.msg_display = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // msg_title
            // 
            this.msg_title.Font = new System.Drawing.Font("굴림", 9F);
            this.msg_title.Location = new System.Drawing.Point(13, 16);
            this.msg_title.Name = "msg_title";
            this.msg_title.Size = new System.Drawing.Size(65, 23);
            this.msg_title.TabIndex = 0;
            this.msg_title.Text = "메시지 :";
            this.msg_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // msg_input
            // 
            this.msg_input.Location = new System.Drawing.Point(82, 16);
            this.msg_input.Multiline = true;
            this.msg_input.Name = "msg_input";
            this.msg_input.Size = new System.Drawing.Size(340, 21);
            this.msg_input.TabIndex = 1;
            // 
            // msg_btn
            // 
            this.msg_btn.Location = new System.Drawing.Point(440, 15);
            this.msg_btn.Name = "msg_btn";
            this.msg_btn.Size = new System.Drawing.Size(75, 23);
            this.msg_btn.TabIndex = 2;
            this.msg_btn.Text = "전송";
            this.msg_btn.UseVisualStyleBackColor = true;
            // 
            // progress_status
            // 
            this.progress_status.Location = new System.Drawing.Point(16, 51);
            this.progress_status.Name = "progress_status";
            this.progress_status.Size = new System.Drawing.Size(499, 23);
            this.progress_status.TabIndex = 3;
            // 
            // msg_display
            // 
            this.msg_display.Location = new System.Drawing.Point(16, 91);
            this.msg_display.Multiline = true;
            this.msg_display.Name = "msg_display";
            this.msg_display.ReadOnly = true;
            this.msg_display.Size = new System.Drawing.Size(499, 339);
            this.msg_display.TabIndex = 4;
            // 
            // SERVER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 450);
            this.Controls.Add(this.msg_display);
            this.Controls.Add(this.progress_status);
            this.Controls.Add(this.msg_btn);
            this.Controls.Add(this.msg_input);
            this.Controls.Add(this.msg_title);
            this.Name = "SERVER";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label msg_title;
        private System.Windows.Forms.TextBox msg_input;
        private System.Windows.Forms.Button msg_btn;
        private System.Windows.Forms.ProgressBar progress_status;
        private System.Windows.Forms.TextBox msg_display;
    }
}

