namespace GuessMelody
{
    public partial class FMain : Form
    {
        private readonly FParam fp = new();
        private readonly FGame fg = new();
        public FMain()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            fg.ShowDialog();
        }

        private void BtnParam_Click(object sender, EventArgs e)
        {
            
            fp.ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            Quiz.ReadParam();
            Quiz.ReadMusic();
        }
    }
}