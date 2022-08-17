namespace GuessMelody
{
    public partial class FParam : Form
    {
        public FParam()
        {
            InitializeComponent();
        }
        private void BtnOK_Click(object sender, EventArgs e)
        {
            Quiz.AllDirectories = cbAllFolders.Checked;
            Quiz.GameDuration = Convert.ToInt32(cbGameDuration.Text);
            Quiz.MusicDuration = Convert.ToInt32(cbMusicDuration.Text);
            Quiz.StartRndTrack = cbRandomStart.Checked;
            LoopStream.CbLoop = cbLoop.Checked;
            FGame.CbRnd = cbRandomStart.Checked;
            Connection.RndPart = cbRndPart.Checked;
            Quiz.WriteParam();
            LoopStream.Countm = 0;
            LoopStream.I = 0;
            FGame.Cnt = 0;
            Quiz.ReadParam();
            Quiz.ReadMusic();
            this.Hide();
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SetParam();
            this.Hide();
        }
        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string[] music_list = Directory.GetFiles(fbd.SelectedPath, "*.mp3", cbAllFolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly); // -- Namespace System.IO included "Directory.GetFiles" and "SearchOption"
                Quiz.LastFolder = fbd.SelectedPath;
                listBox1.Items.Clear();
                listBox1.Items.AddRange(music_list);
                Quiz.List.Clear();
                Quiz.List.AddRange(music_list);
            }
        }

        void SetParam()
        {
            cbAllFolders.Checked = Quiz.AllDirectories;
            cbGameDuration.Text = Quiz.GameDuration.ToString();
            cbMusicDuration.Text = Quiz.MusicDuration.ToString();
            cbRandomStart.Checked = Quiz.StartRndTrack;
            cbLoop.Checked = LoopStream.CbLoop;
            cbRandomStart.Checked = FGame.CbRnd;
            cbRndPart.Checked = Connection.RndPart;
        }

        private void FParam_Load(object sender, EventArgs e)
        {
            SetParam();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Quiz.List.ToArray());
        }

    }
}
