using NAudio.Wave;

namespace GuessMelody
{
    public partial class FGame : Form
    {
        public static readonly Random rnd = new();
        public static int MusicNumber { get; set; }
        public static int Cnt { get; set; }
        public static bool CbRnd { get; set; }
        public static int Success { get; set; }
        private int musicDuration = Quiz.MusicDuration;
                             
        public void ProgressBarToZero()
        {
            try
            {
                progressBar1.Invoke(() => progressBar1.Value = 0);
            }
            catch
            {
                progressBar1.Value = 0;
            }
        }

        public void ResetTheScore()
        {
            lblCounter1.Text = "0";
            lblCounter2.Text = "0";
        }

        public FGame()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Stretch; // - stretch image of background

        }

        private void FGame_Load(object sender, EventArgs e)
        {
            Connection.CancelAudioConn();
            ResetTheScore();
            lblNumberOfMelody.Text = Quiz.List.Count.ToString();
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Quiz.GameDuration;
            musicDuration = Quiz.MusicDuration;
            lblMusicDuration.Text = musicDuration.ToString();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ProgressBarToZero();
            Connection.CancelAudioConn();
            LoopStream.Countm = 0;
            LoopStream.I = 0;
            Cnt = 0;
            Quiz.ReadParam();
            Quiz.ReadMusic();
            timer1.Stop();
            this.Close();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            //if ((waveOutDevice == null && audioFileReader == null) || (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Stopped))  // -- || - if one of the conditions is true
            //{
            //    success = Int32.Parse(lblNumberOfMelody.Text);
            //    if (success >= 1)
            //    {
            //        lblNumberOfMelody.Text = (success - 1).ToString();
            //    }
            //    PlayThePlaylist();
            //    Quiz.list.RemoveAt(musicNumber);
            //}
            //else if (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Playing)
            //{
            //    waveOutDevice.Pause();
            //}
            //else if (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Paused)
            //{
            //    waveOutDevice.Play();
            //}

        }

        public void PlayThePlaylist()
        {
            if ((Cnt > Quiz.List.Count - 1) || (CbRnd && Quiz.List.Count == 0)) // stop playing and show popup menu
            {
                ProgressBarToZero();
                Connection.CancelAudioConn();
                lblNumberOfMelody.Text = 0.ToString();
                timer1.Stop();
                FormPopup _formPopup = new();
                _formPopup.ShowDialog();
                if (Cnt == 0 && !FormPopup.IsCancel)
                {
                    ResetTheScore();
                }

            }
            else // play music and increase counter of tracks count
            {
                if (CbRnd)
                {
                    MusicNumber = rnd.Next(0, Quiz.List.Count);
                    Connection.CreateAudioConn();
                    Quiz.List.RemoveAt(MusicNumber);
                    lblNumberOfMelody.Text = Quiz.List.Count.ToString();
                }
                else
                {
                    Connection.CreateAudioConn();
                    lblNumberOfMelody.Text = (Quiz.List.Count - Cnt).ToString();
                    Cnt++;
                }

            }
        }


        private void BtnNext_Click(object sender, EventArgs e)
        {
            NextMelody();
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            GamePause();
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            GameContinue();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            FormPopup _formPopup = new();
            if (progressBar1.Value == progressBar1.Maximum || Form.ActiveForm == _formPopup)
            {
                timer1.Stop();
                progressBar1.Value = 0;
                if ((Cnt > Quiz.List.Count - 1) || (CbRnd && Quiz.List.Count == 0)) // stop playing and show popup menu
                {
                    Connection.CancelAudioConn();
                    lblNumberOfMelody.Text = 0.ToString();
                    timer1.Stop();
                    _formPopup.ShowDialog();
                    if (Cnt == 0 && !FormPopup.IsCancel)
                    {
                        ResetTheScore();
                    }

                }
                return;
            }
            else
            {
                progressBar1.Value++;
                musicDuration--;
                lblMusicDuration.Text = musicDuration.ToString();
            }
            if (musicDuration == 0 && Convert.ToInt32(lblNumberOfMelody.Text) != 0)
            {
                NextMelody();
            }
        }

        private static void ShowMsgBoxForPause()
        {
            string text = "Music not playing";
            MessageBox.Show(text);
        }

        private static void ShowMsgBoxForContinue()
        {
            string text = "Music not paused or not playing";
            MessageBox.Show(text);
        }

        void GamePause()
        {
            if (Connection.WaveOutDevice != null && Connection.WaveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                Connection.WaveOutDevice.Pause();
                timer1.Stop();
            }
            else if (Connection.WaveOutDevice == null)
            {
                ShowMsgBoxForPause();
            }
            else if (Connection.WaveOutDevice != null && Connection.WaveOutDevice.PlaybackState == PlaybackState.Paused)
            {
                string text = "Music is already paused";
                MessageBox.Show(text);
            }
            else if (Connection.WaveOutDevice != null && Connection.WaveOutDevice.PlaybackState == PlaybackState.Stopped)
            {
                ShowMsgBoxForPause();
            }

        }

        void GameContinue()
        {
            if (Connection.WaveOutDevice != null && Connection.WaveOutDevice.PlaybackState == PlaybackState.Paused)
            {
                Connection.WaveOutDevice.Play();
                timer1.Start();
            }
            else if (Connection.WaveOutDevice == null)
            {
                ShowMsgBoxForContinue();
            }
            else if (Connection.WaveOutDevice != null && Connection.WaveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                string text = "Music is already playing";
                MessageBox.Show(text);
            }
            else if (Connection.WaveOutDevice != null && Connection.WaveOutDevice.PlaybackState == PlaybackState.Stopped)
            {
                ShowMsgBoxForContinue();
            }
        }

        void NextMelody()
        {
            musicDuration = Quiz.MusicDuration;
            lblMusicDuration.Text = musicDuration.ToString();
            ProgressBarToZero();
            timer1.Start();
            Success = Int32.Parse(lblNumberOfMelody.Text);
            if (Success >= 1)
            {
                lblNumberOfMelody.Text = (Success - 1).ToString();
            }
            PlayThePlaylist();
        }

        private void FGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }

        private void PlayerAnswer(KeyEventArgs btnPressed)
        {
            Message message = new();
            string player;
            if (btnPressed.KeyData == Keys.A)
            {
                player = "Player 1";
                message.lblMessage.Text = player;
            }
            else if (btnPressed.KeyData == Keys.Add)
            {
                player = "Player 2";
                message.lblMessage.Text = player;
            }

            if (Connection.WaveOutDevice != null)
            {
                Connection.WaveOutDevice.Pause();
                timer1.Stop();
            }
            else
            {
                timer1.Stop();
                return;
            }           

            if (message.ShowDialog() == DialogResult.Yes)
            {
                if (btnPressed.KeyData == Keys.A)
                {
                    lblCounter1.Text = Convert.ToString(Convert.ToInt32(lblCounter1.Text) + 1);
                }
                else if (btnPressed.KeyData == Keys.Add)
                {
                    lblCounter2.Text = Convert.ToString(Convert.ToInt32(lblCounter2.Text) + 1);
                }

                NextMelody(); // - autoplay the next song, but it can be run by the host
            }
            else
            {
                if (Connection.WaveOutDevice != null)
                {
                    Connection.WaveOutDevice.Play();
                    timer1.Start();
                }
                else
                {
                    string text = "Music not paused or not playing";
                    MessageBox.Show(text);
                }
            }
        }

        private void FGame_KeyDown(object sender, KeyEventArgs e)
        {
            PlayerAnswer(e);
        }
               
    }
}
