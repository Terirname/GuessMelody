namespace GuessMelody
{
    public partial class fMain : Form
    {
        fParam fp = new fParam();
        fGame fg = new fGame();

        public fMain()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            fg.ShowDialog();
        }

        private void btnParam_Click(object sender, EventArgs e)
        {
            
            fp.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            quiz.ReadParam();
            quiz.ReadMusic();
        }
    }
}