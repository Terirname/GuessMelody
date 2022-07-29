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

namespace GuessMelody
{
    public partial class fGame : Form
    {
        //public static AudioFileReader audioFileReader;              
        public static Random rnd = new Random();
        public static int musicNumber = rnd.Next(0, quiz.list.Count);
        public static int cnt = 0;
        public bool cbRnd = false;
        public static int success = 0;
        //formPopup _formPopup = new formPopup();
        quiz _quiz = new quiz();
        connection _connection = new connection();
        int musicDuration = quiz.musicDuration;
       
        public void progressBarToZero()
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

        public fGame()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Stretch; // - stretch image of background
            
        }

        private void fGame_Load(object sender, EventArgs e)
        {
            _connection.cancelAudioConn();
            lblCounter1.Text = "0";
            lblCounter2.Text = "0";
            lblNumberOfMelody.Text = quiz.list.Count.ToString();
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = quiz.gameDuration;
            musicDuration = quiz.musicDuration;
            lblMusicDuration.Text = musicDuration.ToString();           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            progressBarToZero();
            _connection.cancelAudioConn();
            LoopStream.countm = 0;
            LoopStream.i = 0;
            cnt = 0;
            _quiz.ReadParam();
            _quiz.ReadMusic();
            timer1.Stop();
            this.Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //if ((waveOutDevice == null && audioFileReader == null) || (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Stopped))  // -- || - if one of the conditions is true
            //{
            //    success = Int32.Parse(lblNumberOfMelody.Text);
            //    if (success >= 1)
            //    {
            //        lblNumberOfMelody.Text = (success - 1).ToString();
            //    }
            //    playThePlaylist();
            //    quiz.list.RemoveAt(musicNumber);
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

        public void playThePlaylist()
        {
            if (cnt > quiz.list.Count - 1 && cbRnd) // back to begin
            {               
                cnt = 0;
                progressBarToZero();
                _connection.cancelAudioConn();
                _connection.createAudioConn();    
            }
            else if (cnt > quiz.list.Count - 1 && !cbRnd) // stop playing and show popup menu
            {
                progressBarToZero();
                _connection.cancelAudioConn();
                lblNumberOfMelody.Text = 0.ToString();               
                timer1.Stop();
                formPopup _formPopup = new formPopup();
                _formPopup.ShowDialog();
                if (cnt == 0)
                {
                    lblCounter1.Text = "0";
                    lblCounter2.Text = "0";
                }
                
            }
            else // play music and increase counter of tracks count
            {
                if (cbRnd)
                {
                    _connection.createAudioConn();
                    quiz.list.RemoveAt(musicNumber);
                    lblNumberOfMelody.Text = quiz.list.Count().ToString();
                }
                else
                {
                    _connection.createAudioConn();
                    lblNumberOfMelody.Text = (quiz.list.Count() - cnt).ToString();
                    cnt++;
                }
                              
            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            nextMelody();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            gamePause();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            gameContinue();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            formPopup _formPopup = new formPopup();
            if (progressBar1.Value == progressBar1.Maximum || Form.ActiveForm == _formPopup)
            {
                timer1.Stop();
                progressBar1.Value = 0;
                return;
            }
            else
            {
                progressBar1.Value++;
                musicDuration--; 
                lblMusicDuration.Text = musicDuration.ToString();
            } 
            if (musicDuration == 0)
            {
                nextMelody();
            }
        }

        void gamePause()
        {
            if (connection.waveOutDevice != null && connection.waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                connection.waveOutDevice.Pause();
                timer1.Stop();
            }
            else if (connection.waveOutDevice == null)
            {
                string text = "Music not playing";
                MessageBox.Show(text);
            }
            else if (connection.waveOutDevice != null && connection.waveOutDevice.PlaybackState == PlaybackState.Paused)
            {
                string text = "Music is already paused";
                MessageBox.Show(text);
            }
            else if (connection.waveOutDevice != null && connection.waveOutDevice.PlaybackState == PlaybackState.Stopped)
            {
                string text = "Music not playing";
                MessageBox.Show(text);
            }

        }

        void gameContinue()
        {
            if (connection.waveOutDevice != null && connection.waveOutDevice.PlaybackState == PlaybackState.Paused)
            {
                connection.waveOutDevice.Play();
                timer1.Start();
            }
            else if (connection.waveOutDevice == null)
            {
                string text = "Music not paused or not playing";
                MessageBox.Show(text);
            }
            else if (connection.waveOutDevice != null && connection.waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                string text = "Music is already playing";
                MessageBox.Show(text);
            }
            else if (connection.waveOutDevice != null && connection.waveOutDevice.PlaybackState == PlaybackState.Stopped)
            {
                string text = "Music not paused or not playing";
                MessageBox.Show(text);
            }
        }

        void nextMelody()
        {
            timer1.Start();
            success = Int32.Parse(lblNumberOfMelody.Text);
            if (success >= 1)
            {
                lblNumberOfMelody.Text = (success - 1).ToString();
            }

            if (quiz.list.Count == 0)
            {
                _connection.cancelAudioConn();
                lblNumberOfMelody.Text = 0.ToString();
                formPopup _formPopup = new formPopup();
                _formPopup.ShowDialog();
            }
            //else if (cnt >= 1)
            //{
            //    //lblNumberOfMelody.Text = (quiz.list.Count - cnt - 1).ToString();
            //    playThePlaylist();
            //}
            else
            {
                playThePlaylist();
            }
        }

        private void fGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }

        private void fGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.A)
            {
                if (connection.waveOutDevice != null)
                {
                    connection.waveOutDevice.Pause();
                    timer1.Stop();
                }
                else
                {
                    timer1.Stop();
                    return;
                }
                if (MessageBox.Show("Is the answer correct?", "Player 1", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lblCounter1.Text = Convert.ToString(Convert.ToInt32(lblCounter1.Text) + 1);
                    nextMelody(); // - autoplay next song, but it can be run by the host
                }
                else
                {
                    if (connection.waveOutDevice != null)
                    {
                        connection.waveOutDevice.Play();
                        timer1.Start();
                    }
                    else
                    {
                        string text = "Music not paused or not playing";
                        MessageBox.Show(text);
                    }
                }
                
            }

            if (e.KeyData == Keys.Add) // plus or numpad plus
            {
                if (connection.waveOutDevice != null)
                {
                    connection.waveOutDevice.Pause();
                    timer1.Stop();
                }
                else
                {
                    timer1.Stop();
                    return;
                }
                if (MessageBox.Show("Is the answer correct?", "Player 2", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Debug.WriteLine(lblCounter2.Text);
                    lblCounter2.Text = Convert.ToString(Convert.ToInt32(lblCounter2.Text) + 1);
                    nextMelody(); // - autoplay next song, but it can be run by the host
                }
                else
                {
                    if (connection.waveOutDevice != null)
                    {
                        connection.waveOutDevice.Play();
                        timer1.Start();
                    }
                    else
                    {
                        string text = "Music not paused or not playing";
                        MessageBox.Show(text);
                    }
                }

            }
        }
       
        private void fGame_Activated(object sender, EventArgs e)
        {
            
        }
    }
}
