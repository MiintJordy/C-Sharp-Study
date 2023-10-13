namespace client
{
    partial class Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.search_title = new System.Windows.Forms.Label();
            this.search_input = new System.Windows.Forms.TextBox();
            this.search_btn = new System.Windows.Forms.Button();
            this.upload_btn = new System.Windows.Forms.Button();
            this.category_box = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cate_btn_1 = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.video_btn_1 = new System.Windows.Forms.Button();
            this.video_btn_2 = new System.Windows.Forms.Button();
            this.video_btn_3 = new System.Windows.Forms.Button();
            this.video_btn_4 = new System.Windows.Forms.Button();
            this.video_btn_5 = new System.Windows.Forms.Button();
            this.video_btn_6 = new System.Windows.Forms.Button();
            this.video_title_1 = new System.Windows.Forms.Label();
            this.video_title_2 = new System.Windows.Forms.Label();
            this.video_title_3 = new System.Windows.Forms.Label();
            this.video_title_4 = new System.Windows.Forms.Label();
            this.video_title_5 = new System.Windows.Forms.Label();
            this.video_title_6 = new System.Windows.Forms.Label();
            this.category_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // search_title
            // 
            this.search_title.AutoSize = true;
            this.search_title.Location = new System.Drawing.Point(150, 78);
            this.search_title.Name = "search_title";
            this.search_title.Size = new System.Drawing.Size(49, 12);
            this.search_title.TabIndex = 0;
            this.search_title.Text = "검색어 :";
            // 
            // search_input
            // 
            this.search_input.Location = new System.Drawing.Point(205, 73);
            this.search_input.Name = "search_input";
            this.search_input.Size = new System.Drawing.Size(355, 21);
            this.search_input.TabIndex = 1;
            // 
            // search_btn
            // 
            this.search_btn.Location = new System.Drawing.Point(576, 72);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(75, 23);
            this.search_btn.TabIndex = 2;
            this.search_btn.Text = "검색";
            this.search_btn.UseVisualStyleBackColor = true;
            // 
            // upload_btn
            // 
            this.upload_btn.Location = new System.Drawing.Point(664, 72);
            this.upload_btn.Name = "upload_btn";
            this.upload_btn.Size = new System.Drawing.Size(75, 23);
            this.upload_btn.TabIndex = 3;
            this.upload_btn.Text = "업로드";
            this.upload_btn.UseVisualStyleBackColor = true;
            this.upload_btn.Click += new System.EventHandler(this.upload_btn_Click);
            // 
            // category_box
            // 
            this.category_box.Controls.Add(this.button4);
            this.category_box.Controls.Add(this.button3);
            this.category_box.Controls.Add(this.button2);
            this.category_box.Controls.Add(this.button1);
            this.category_box.Controls.Add(this.cate_btn_1);
            this.category_box.Location = new System.Drawing.Point(18, 108);
            this.category_box.Name = "category_box";
            this.category_box.Size = new System.Drawing.Size(205, 330);
            this.category_box.TabIndex = 4;
            this.category_box.TabStop = false;
            this.category_box.Text = "카테고리";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 265);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(192, 55);
            this.button4.TabIndex = 4;
            this.button4.Text = "운동";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 204);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(192, 55);
            this.button3.TabIndex = 3;
            this.button3.Text = "동물";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 143);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(192, 55);
            this.button2.TabIndex = 2;
            this.button2.Text = "자연";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 55);
            this.button1.TabIndex = 1;
            this.button1.Text = "게임";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cate_btn_1
            // 
            this.cate_btn_1.Location = new System.Drawing.Point(7, 21);
            this.cate_btn_1.Name = "cate_btn_1";
            this.cate_btn_1.Size = new System.Drawing.Size(192, 55);
            this.cate_btn_1.TabIndex = 0;
            this.cate_btn_1.Text = "음악";
            this.cate_btn_1.UseVisualStyleBackColor = true;
            // 
            // logo
            // 
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(25, 13);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(119, 82);
            this.logo.TabIndex = 5;
            this.logo.UseVisualStyleBackColor = true;
            // 
            // title
            // 
            this.title.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.title.Location = new System.Drawing.Point(152, 13);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(587, 47);
            this.title.TabIndex = 6;
            this.title.Text = "Boktube";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // video_btn_1
            // 
            this.video_btn_1.Location = new System.Drawing.Point(246, 129);
            this.video_btn_1.Name = "video_btn_1";
            this.video_btn_1.Size = new System.Drawing.Size(152, 111);
            this.video_btn_1.TabIndex = 7;
            this.video_btn_1.UseVisualStyleBackColor = true;
            // 
            // video_btn_2
            // 
            this.video_btn_2.Location = new System.Drawing.Point(418, 129);
            this.video_btn_2.Name = "video_btn_2";
            this.video_btn_2.Size = new System.Drawing.Size(152, 111);
            this.video_btn_2.TabIndex = 8;
            this.video_btn_2.UseVisualStyleBackColor = true;
            // 
            // video_btn_3
            // 
            this.video_btn_3.Location = new System.Drawing.Point(587, 129);
            this.video_btn_3.Name = "video_btn_3";
            this.video_btn_3.Size = new System.Drawing.Size(152, 111);
            this.video_btn_3.TabIndex = 9;
            this.video_btn_3.UseVisualStyleBackColor = true;
            // 
            // video_btn_4
            // 
            this.video_btn_4.Location = new System.Drawing.Point(246, 284);
            this.video_btn_4.Name = "video_btn_4";
            this.video_btn_4.Size = new System.Drawing.Size(152, 111);
            this.video_btn_4.TabIndex = 10;
            this.video_btn_4.UseVisualStyleBackColor = true;
            // 
            // video_btn_5
            // 
            this.video_btn_5.Location = new System.Drawing.Point(418, 284);
            this.video_btn_5.Name = "video_btn_5";
            this.video_btn_5.Size = new System.Drawing.Size(152, 111);
            this.video_btn_5.TabIndex = 11;
            this.video_btn_5.UseVisualStyleBackColor = true;
            // 
            // video_btn_6
            // 
            this.video_btn_6.Location = new System.Drawing.Point(587, 284);
            this.video_btn_6.Name = "video_btn_6";
            this.video_btn_6.Size = new System.Drawing.Size(152, 111);
            this.video_btn_6.TabIndex = 12;
            this.video_btn_6.UseVisualStyleBackColor = true;
            // 
            // video_title_1
            // 
            this.video_title_1.Location = new System.Drawing.Point(246, 248);
            this.video_title_1.Name = "video_title_1";
            this.video_title_1.Size = new System.Drawing.Size(152, 21);
            this.video_title_1.TabIndex = 13;
            this.video_title_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // video_title_2
            // 
            this.video_title_2.Location = new System.Drawing.Point(418, 248);
            this.video_title_2.Name = "video_title_2";
            this.video_title_2.Size = new System.Drawing.Size(152, 21);
            this.video_title_2.TabIndex = 14;
            this.video_title_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // video_title_3
            // 
            this.video_title_3.Location = new System.Drawing.Point(587, 248);
            this.video_title_3.Name = "video_title_3";
            this.video_title_3.Size = new System.Drawing.Size(152, 21);
            this.video_title_3.TabIndex = 15;
            this.video_title_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // video_title_4
            // 
            this.video_title_4.Location = new System.Drawing.Point(246, 407);
            this.video_title_4.Name = "video_title_4";
            this.video_title_4.Size = new System.Drawing.Size(152, 21);
            this.video_title_4.TabIndex = 16;
            this.video_title_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // video_title_5
            // 
            this.video_title_5.Location = new System.Drawing.Point(418, 407);
            this.video_title_5.Name = "video_title_5";
            this.video_title_5.Size = new System.Drawing.Size(152, 21);
            this.video_title_5.TabIndex = 17;
            this.video_title_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // video_title_6
            // 
            this.video_title_6.Location = new System.Drawing.Point(587, 407);
            this.video_title_6.Name = "video_title_6";
            this.video_title_6.Size = new System.Drawing.Size(152, 21);
            this.video_title_6.TabIndex = 18;
            this.video_title_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 450);
            this.Controls.Add(this.video_title_6);
            this.Controls.Add(this.video_title_5);
            this.Controls.Add(this.video_title_4);
            this.Controls.Add(this.video_title_3);
            this.Controls.Add(this.video_title_2);
            this.Controls.Add(this.video_title_1);
            this.Controls.Add(this.video_btn_6);
            this.Controls.Add(this.video_btn_5);
            this.Controls.Add(this.video_btn_4);
            this.Controls.Add(this.video_btn_3);
            this.Controls.Add(this.video_btn_2);
            this.Controls.Add(this.video_btn_1);
            this.Controls.Add(this.title);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.category_box);
            this.Controls.Add(this.upload_btn);
            this.Controls.Add(this.search_btn);
            this.Controls.Add(this.search_input);
            this.Controls.Add(this.search_title);
            this.Name = "Client";
            this.Text = "Form1";
            this.category_box.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label search_title;
        private System.Windows.Forms.TextBox search_input;
        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.Button upload_btn;
        private System.Windows.Forms.GroupBox category_box;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cate_btn_1;
        private System.Windows.Forms.Button logo;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button video_btn_1;
        private System.Windows.Forms.Button video_btn_2;
        private System.Windows.Forms.Button video_btn_3;
        private System.Windows.Forms.Button video_btn_4;
        private System.Windows.Forms.Button video_btn_5;
        private System.Windows.Forms.Button video_btn_6;
        private System.Windows.Forms.Label video_title_1;
        private System.Windows.Forms.Label video_title_2;
        private System.Windows.Forms.Label video_title_3;
        private System.Windows.Forms.Label video_title_4;
        private System.Windows.Forms.Label video_title_5;
        private System.Windows.Forms.Label video_title_6;
    }
}

