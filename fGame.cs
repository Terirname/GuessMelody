using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using NAudio;
using NAudio.Wave;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace GuessMelody
{
    public partial class FGame : Form
    {
        public static readonly Random rnd = new();
        public static int musicNumber;
        public static int cnt = 0;
        public static bool cbRnd = false;
        public static int success = 0;
        int musicDuration = Quiz.musicDuration;

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
            lblNumberOfMelody.Text = Quiz.list.Count.ToString();
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Quiz.gameDuration;
            musicDuration = Quiz.musicDuration;
            lblMusicDuration.Text = musicDuration.ToString();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ProgressBarToZero();
            Connection.CancelAudioConn();
            LoopStream.countm = 0;
            LoopStream.i = 0;
            cnt = 0;
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
            if ((cnt > Quiz.list.Count - 1) || (cbRnd && Quiz.list.Count == 0)) // stop playing and show popup menu
            {
                ProgressBarToZero();
                Connection.CancelAudioConn();
                lblNumberOfMelody.Text = 0.ToString();
                timer1.Stop();
                FormPopup _formPopup = new();
                _formPopup.ShowDialog();
                if (cnt == 0 && FormPopup.isCancel == false)
                {
                    ResetTheScore();
                }

            }
            else // play music and increase counter of tracks count
            {
                if (cbRnd)
                {
                    musicNumber = rnd.Next(0, Quiz.list.Count);
                    Connection.CreateAudioConn();
                    Quiz.list.RemoveAt(musicNumber);
                    lblNumberOfMelody.Text = Quiz.list.Count.ToString();
                }
                else
                {
                    Connection.CreateAudioConn();
                    lblNumberOfMelody.Text = (Quiz.list.Count - cnt).ToString();
                    cnt++;
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
                if ((cnt > Quiz.list.Count - 1) || (cbRnd && Quiz.list.Count == 0)) // stop playing and show popup menu
                {
                    Connection.CancelAudioConn();
                    lblNumberOfMelody.Text = 0.ToString();
                    timer1.Stop();
                    _formPopup.ShowDialog();
                    if (cnt == 0 && FormPopup.isCancel == false)
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

        void GamePause()
        {
            if (Connection.waveOutDevice != null && Connection.waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                Connection.waveOutDevice.Pause();
                timer1.Stop();
            }
            else if (Connection.waveOutDevice == null)
            {
                string text = "Music not playing";
                MessageBox.Show(text);
            }
            else if (Connection.waveOutDevice != null && Connection.waveOutDevice.PlaybackState == PlaybackState.Paused)
            {
                string text = "Music is already paused";
                MessageBox.Show(text);
            }
            else if (Connection.waveOutDevice != null && Connection.waveOutDevice.PlaybackState == PlaybackState.Stopped)
            {
                string text = "Music not playing";
                MessageBox.Show(text);
            }

        }

        void GameContinue()
        {
            if (Connection.waveOutDevice != null && Connection.waveOutDevice.PlaybackState == PlaybackState.Paused)
            {
                Connection.waveOutDevice.Play();
                timer1.Start();
            }
            else if (Connection.waveOutDevice == null)
            {
                string text = "Music not paused or not playing";
                MessageBox.Show(text);
            }
            else if (Connection.waveOutDevice != null && Connection.waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                string text = "Music is already playing";
                MessageBox.Show(text);
            }
            else if (Connection.waveOutDevice != null && Connection.waveOutDevice.PlaybackState == PlaybackState.Stopped)
            {
                string text = "Music not paused or not playing";
                MessageBox.Show(text);
            }
        }

        void NextMelody()
        {
            musicDuration = Quiz.musicDuration;
            lblMusicDuration.Text = musicDuration.ToString();
            ProgressBarToZero();
            timer1.Start();
            success = Int32.Parse(lblNumberOfMelody.Text);
            if (success >= 1)
            {
                lblNumberOfMelody.Text = (success - 1).ToString();
            }
            PlayThePlaylist();
        }

        private void FGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }

        private void PlayerAnswer(KeyEventArgs btnPressed)
        {
            string player = "";
            Label label = new();

            if (btnPressed.KeyData == Keys.A)
            {
                player = "Player 1";
                label.Text = lblCounter1.Text;
            }
            else if (btnPressed.KeyData == Keys.Add)
            {
                player = "Player 2";
                label.Text = lblCounter2.Text;
            }

            if (Connection.waveOutDevice != null)
            {
                Connection.waveOutDevice.Pause();
                timer1.Stop();
            }
            else
            {
                timer1.Stop();
                return;
            }
            if (MessageBox.Show("Is the answer correct?", player, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (btnPressed.KeyData == Keys.A)
                {
                    lblCounter1.Text = Convert.ToString(Convert.ToInt32(lblCounter1.Text) + 1);
                }
                else if (btnPressed.KeyData == Keys.Add)
                {
                    lblCounter2.Text = Convert.ToString(Convert.ToInt32(lblCounter2.Text) + 1);
                }

                NextMelody(); // - autoplay next song, but it can be run by the host
            }
            else
            {
                if (Connection.waveOutDevice != null)
                {
                    Connection.waveOutDevice.Play();
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
