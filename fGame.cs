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
        private static int musicNumber;
        private static int cnt;
        private static bool cbRnd;
        private static int success;
        int musicDuration = Quiz.Get_musicDuration();

        public static void Set_cnt(int cnt_public)
        {
            cnt = cnt_public;
        }

        public static int Get_cnt()
        {
            return cnt;
        }
        public static void Set_cbRnd(bool cbRnd_public)
        {
            cbRnd = cbRnd_public;
        }

        public static bool Get_cbRnd()
        {
            return cbRnd;
        }
        public static void Set_success(int success_public)
        {
            success = success_public;
        }

        public static int Get_success()
        {
            return success;
        }
        public static int Get_musicNumber()
        {
            return musicNumber;
        }
        public static void Set_musicNumber(int musicNumber_public)
        {
            musicNumber = musicNumber_public;
        }
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
            lblNumberOfMelody.Text = Quiz.Get_list().Count.ToString();
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Quiz.Get_gameDuration();
            musicDuration = Quiz.Get_musicDuration();
            lblMusicDuration.Text = musicDuration.ToString();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ProgressBarToZero();
            Connection.CancelAudioConn();
            LoopStream.Set_countm(0);
            LoopStream.SetI(0);
            Set_cnt(0);
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
            if ((Get_cnt() > Quiz.Get_list().Count - 1) || (cbRnd && Quiz.Get_list().Count == 0)) // stop playing and show popup menu
            {
                ProgressBarToZero();
                Connection.CancelAudioConn();
                lblNumberOfMelody.Text = 0.ToString();
                timer1.Stop();
                FormPopup _formPopup = new();
                _formPopup.ShowDialog();
                if (Get_cnt() == 0 && !FormPopup.Get_isCancel())
                {
                    ResetTheScore();
                }

            }
            else // play music and increase counter of tracks count
            {
                if (cbRnd)
                {
                    Set_musicNumber(rnd.Next(0, Quiz.Get_list().Count));
                    Connection.CreateAudioConn();
                    Quiz.Get_list().RemoveAt(Get_musicNumber());
                    lblNumberOfMelody.Text = Quiz.Get_list().Count.ToString();
                }
                else
                {
                    Connection.CreateAudioConn();
                    lblNumberOfMelody.Text = (Quiz.Get_list().Count - Get_cnt()).ToString();
                    Set_cnt(cnt + 1);
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
                if ((Get_cnt() > Quiz.Get_list().Count - 1) || (cbRnd && Quiz.Get_list().Count == 0)) // stop playing and show popup menu
                {
                    Connection.CancelAudioConn();
                    lblNumberOfMelody.Text = 0.ToString();
                    timer1.Stop();
                    _formPopup.ShowDialog();
                    if (Get_cnt() == 0 && !FormPopup.Get_isCancel())
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
            if (Connection.Get_waveOutDevice() != null && Connection.Get_waveOutDevice()!.PlaybackState == PlaybackState.Playing)
            {
                Connection.Get_waveOutDevice()!.Pause();
                timer1.Stop();
            }
            else if (Connection.Get_waveOutDevice() == null)
            {
                ShowMsgBoxForPause();
            }
            else if (Connection.Get_waveOutDevice() != null && Connection.Get_waveOutDevice()!.PlaybackState == PlaybackState.Paused)
            {
                string text = "Music is already paused";
                MessageBox.Show(text);
            }
            else if (Connection.Get_waveOutDevice() != null && Connection.Get_waveOutDevice()!.PlaybackState == PlaybackState.Stopped)
            {
                ShowMsgBoxForPause();
            }

        }

        void GameContinue()
        {
            if (Connection.Get_waveOutDevice() != null && Connection.Get_waveOutDevice()!.PlaybackState == PlaybackState.Paused)
            {
                Connection.Get_waveOutDevice()!.Play();
                timer1.Start();
            }
            else if (Connection.Get_waveOutDevice() == null)
            {
                ShowMsgBoxForContinue();
            }
            else if (Connection.Get_waveOutDevice() != null && Connection.Get_waveOutDevice()!.PlaybackState == PlaybackState.Playing)
            {
                string text = "Music is already playing";
                MessageBox.Show(text);
            }
            else if (Connection.Get_waveOutDevice() != null && Connection.Get_waveOutDevice()!.PlaybackState == PlaybackState.Stopped)
            {
                ShowMsgBoxForContinue();
            }
        }

        void NextMelody()
        {
            musicDuration = Quiz.Get_musicDuration();
            lblMusicDuration.Text = musicDuration.ToString();
            ProgressBarToZero();
            timer1.Start();
            Set_success(Int32.Parse(lblNumberOfMelody.Text));
            if (Get_success() >= 1)
            {
                lblNumberOfMelody.Text = (Get_success() - 1).ToString();
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

            if (Connection.Get_waveOutDevice() != null)
            {
                Connection.Get_waveOutDevice()!.Pause();
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
                if (Connection.Get_waveOutDevice() != null)
                {
                    Connection.Get_waveOutDevice()!.Play();
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
