namespace UsingControls
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            var Fonts = FontFamily.Families;                   // �ü���� ��ġ�Ǿ� �ִ� ��Ʈ ��� �˻�
            foreach (FontFamily font in Fonts)                 // cboFont ��Ʈ�ѿ� �� ��Ʈ �̸� �߰�
            {
                cboFont.Items.Add(font.Name);
            }
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

        private void cboFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFont();
        }
        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFont();
        }
        private void chkItalic_ChkedChanged(object send, EventArgs e)
        {
            ChangeFont();
        }
    }
}