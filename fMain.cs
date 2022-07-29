namespace GuessMelody
{
    public partial class fMain : Form
    {
        private fParam fp = new fParam();
        private fGame fg = new fGame();
        private quiz _quiz = new quiz();

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
            _quiz.ReadParam();
            _quiz.ReadMusic();
        }
    }
}