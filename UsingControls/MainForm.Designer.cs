namespace UsingControls
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
         void ChangeFont()
        {
            if (cboFont.SelectedIndex < 0)
            {
                return;
            }
            FontStyle style = FontStyle.Regular;

            if (chkBold.Checked)
            {
                style |= FontStyle.Bold;
            }

            if (chkItalic.Checked)
            {
                style |= FontStyle.Italic;
            }

            txtSampleText.Font = new Font((string)cboFont.SelectedItem, 10, style);
        }
        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Text = "Form1";
        }

        #endregion
    }
}