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
        public static formPopup _formPopup = new formPopup();
        public static IWavePlayer waveOutDevice;
        public static AudioFileReader audioFileReader;
        public static Random rnd = new Random();
        public static int musicNumber;
        public static int cnt = 0;
        public static bool cbRnd = false;
        public static int success = 0;
        public static void createAudioConn()
        {
            timer1.Start();
            musicNumber = rnd.Next(0, quiz.list.Count);
            if (waveOutDevice == null && audioFileReader == null) // -- && - if both conditions are true
            {
                if (LoopStream.cbLoop == true) 
                {
                    waveOutDevice = new WaveOutEvent();
                    // audioFileReader = new AudioFileReader(@"E:\C# projects (lessons)\GuessMelody\Melodies\melody_1.mp3");
                    if (cbRnd == true) 
                    {
                        if (quiz.list.Count <= 0)
                        {
                            _formPopup.ShowDialog();
                        }
                        else
                        {
                            audioFileReader = new AudioFileReader(quiz.list[musicNumber]);                            
                            LoopStream loop = new LoopStream(audioFileReader);
                            waveOutDevice.Init(loop);
                            waveOutDevice.Play();
                            quiz.list.RemoveAt(musicNumber);
                            lblNumberOfMelody.Text = quiz.list.Count().ToString();
                        }
                    }
                    else
                    {                        
                        audioFileReader = new AudioFileReader(quiz.list[cnt]);
                        LoopStream loop = new LoopStream(audioFileReader);
                        waveOutDevice.Init(loop);
                        waveOutDevice.Play();
                        lblNumberOfMelody.Text = (quiz.list.Count() - cnt).ToString();
                    }                    
                    
                }
                else
                {
                    waveOutDevice = new WaveOutEvent();
                    // audioFileReader = new AudioFileReader(@"E:\C# projects (lessons)\GuessMelody\Melodies\melody_1.mp3");
                    if (cbRnd == true)
                    {
                        if (quiz.list.Count <= 0)
                        {
                            _formPopup.ShowDialog();
                        }
                        else
                        {
                            audioFileReader = new AudioFileReader(quiz.list[musicNumber]);                            
                            waveOutDevice.Init(audioFileReader);
                            waveOutDevice.Play();
                            quiz.list.RemoveAt(musicNumber);
                            lblNumberOfMelody.Text = quiz.list.Count().ToString();
                        }
                        
                    }
                    else
                    {
                        if (cnt > quiz.list.Count() - 1)
                        {
                            //cancelAudioConn();
                            _formPopup.ShowDialog();
                        }
                        else
                        {
                            audioFileReader = new AudioFileReader(quiz.list[cnt]);
                            waveOutDevice.Init(audioFileReader);
                            waveOutDevice.Play();
                        }
                    }

                }
            } 
            else if (waveOutDevice != null && audioFileReader != null)
            {                
                cancelAudioConn();
                if (LoopStream.cbLoop == true)
                {
                    waveOutDevice = new WaveOutEvent();
                    // audioFileReader = new AudioFileReader(@"E:\C# projects (lessons)\GuessMelody\Melodies\melody_1.mp3");
                    if (cbRnd == true)
                    {
                        if (quiz.list.Count <= 0)
                        {
                            _formPopup.ShowDialog();
                        }
                        else
                        {
                            audioFileReader = new AudioFileReader(quiz.list[musicNumber]);                            
                            LoopStream loop = new LoopStream(audioFileReader);                            
                            waveOutDevice.Init(loop);
                            waveOutDevice.Play();
                            quiz.list.RemoveAt(musicNumber);
                            lblNumberOfMelody.Text = quiz.list.Count().ToString();
                        }
                    }
                    else
                    {
                        audioFileReader = new AudioFileReader(quiz.list[cnt]);
                        LoopStream loop = new LoopStream(audioFileReader);
                        waveOutDevice.Init(loop);
                        waveOutDevice.Play();
                        lblNumberOfMelody.Text = (quiz.list.Count() - cnt).ToString();
                    }                        
                }
                else
                {
                    waveOutDevice = new WaveOutEvent();
                    // audioFileReader = new AudioFileReader(@"E:\C# projects (lessons)\GuessMelody\Melodies\melody_1.mp3");
                    if (cbRnd == true)
                    {
                        if (quiz.list.Count <= 0)
                        {
                            lblNumberOfMelody.Text = 0.ToString();
                            _formPopup.ShowDialog();
                        }
                        else
                        {
                            audioFileReader = new AudioFileReader(quiz.list[musicNumber]);                            
                            waveOutDevice.Init(audioFileReader);
                            waveOutDevice.Play();
                            quiz.list.RemoveAt(musicNumber);
                            //lblNumberOfMelody.Text = quiz.list.Count().ToString();
                        }

                    }
                    else
                    {
                        if (cnt > quiz.list.Count() - 1)
                        {
                            //cancelAudioConn();
                            lblNumberOfMelody.Text = 0.ToString();
                            _formPopup.ShowDialog();
                        }
                        else
                        {
                            audioFileReader = new AudioFileReader(quiz.list[cnt]);
                            waveOutDevice.Init(audioFileReader);
                            waveOutDevice.Play();
                            //lblNumberOfMelody.Text = (quiz.list.Count() - cnt).ToString();
                        }
                            
                    }

                }
            }
            else
            {
                cancelAudioConn();
                lblNumberOfMelody.Text = 0.ToString();
                _formPopup.ShowDialog();
            }
        }

        public static void cancelAudioConn()
        {
            progressBar1.Value = 0;
            if (waveOutDevice != null && audioFileReader != null)
            {
                waveOutDevice.Stop();
                audioFileReader.Dispose();
                waveOutDevice.Dispose();
                waveOutDevice = null;
                audioFileReader = null;
            }
            else if (waveOutDevice == null && audioFileReader == null)
            {
            }
            else if (waveOutDevice != null && audioFileReader == null)
            {
                waveOutDevice.Stop();
                //audioFileReader.Dispose();
                waveOutDevice.Dispose();
                waveOutDevice = null;
                audioFileReader = null;
            }
        }

        public fGame()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Stretch; // - stretch image of background
            
        }

        private void fGame_Load(object sender, EventArgs e)
        {
            lblNumberOfMelody.Text = quiz.list.Count.ToString();
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = quiz.gameDuration;
            /*
                if (waveOutDevice != null && audioFileReader != null) Debug.WriteLine(waveOutDevice.PlaybackState);
                else if (waveOutDevice == null && audioFileReader == null) Debug.WriteLine("<b>waveOutDevice\nis\nnull!</b>");
            */
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {           
            cancelAudioConn();
            LoopStream.countm = 0;
            LoopStream.i = 0;
            cnt = 0;
            quiz.ReadParam();
            quiz.ReadMusic();
            timer1.Stop();
            this.Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if ( ( waveOutDevice == null && audioFileReader == null ) || ( waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Stopped ) )  // -- || - if one of the conditions is true
            {
                success = Int32.Parse(lblNumberOfMelody.Text);
                if (success >= 1)
                {
                    lblNumberOfMelody.Text = (success - 1).ToString();
                }
                playThePlaylist();
                //quiz.list.RemoveAt(musicNumber);
            }
            else if (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                waveOutDevice.Pause();
            }
            else if (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Paused)
            {
                waveOutDevice.Play();
            }
            
        }

        public static void playThePlaylist()
        {
            if (cnt > quiz.list.Count - 1 && fGame.cbRnd == true) // back to begin
            {               
                cnt = 0;
                createAudioConn();    
            }
            else if (cnt > quiz.list.Count - 1 && fGame.cbRnd == false) // stop playing and show popup menu
            {
                cancelAudioConn();
                lblNumberOfMelody.Text = 0.ToString();
                _formPopup.ShowDialog();
            }
            else // play music and increase counter of tracks count
            {
                createAudioConn();
                if (cnt == 0)
                {
                    //lblNumberOfMelody.Text = (quiz.list.Count - cnt - 1).ToString();
                }
                else
                {
                    //lblNumberOfMelody.Text = (quiz.list.Count - cnt).ToString();
                }
                
                cnt++;              
            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            timer1.Start();
            success = Int32.Parse(lblNumberOfMelody.Text);
            if (success >= 1)
            {               
                lblNumberOfMelody.Text = (success - 1).ToString();
            }
            
            if (quiz.list.Count == 0)
            {
                cancelAudioConn();
                lblNumberOfMelody.Text = 0.ToString();
                _formPopup.ShowDialog();
            }
            else if (cnt >= 1)
            {
                //lblNumberOfMelody.Text = (quiz.list.Count - cnt - 1).ToString();
                playThePlaylist();
            }
            else
            {
                playThePlaylist();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                waveOutDevice.Pause();
                timer1.Stop();
            }
            else if (waveOutDevice == null)
            {
                string text = "Music not playing";
                MessageBox.Show(text);
            }
            else if (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Paused)
            {
                string text = "Music is already paused";
                MessageBox.Show(text);
            }
            else if (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Stopped)
            {
                string text = "Music not playing";
                MessageBox.Show(text);
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {            
            if (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Paused)
            {
                waveOutDevice.Play();
                timer1.Start();
            }
            else if (waveOutDevice == null)
            {
                string text = "Music not paused or not playing";
                MessageBox.Show(text);
            }
            else if (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                string text = "Music is already playing";
                MessageBox.Show(text);
            }
            else if (waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Stopped)
            {
                string text = "Music not paused or not playing";
                MessageBox.Show(text);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {           
            if (progressBar1.Value == progressBar1.Maximum || Form.ActiveForm == _formPopup)
            {
                timer1.Stop();
                progressBar1.Value = 0;
            }
            else
            {
                progressBar1.Value++;
            }            
        }

        private void fGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }
    }
}
