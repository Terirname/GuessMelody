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
        static public bool cbRnd = false;
        public static void createAudioConn()
        {            
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
                            quiz.list.RemoveAt(musicNumber);
                            LoopStream loop = new LoopStream(audioFileReader);
                            waveOutDevice.Init(loop);
                            waveOutDevice.Play();
                        }
                    }
                    else
                    {                        
                        audioFileReader = new AudioFileReader(quiz.list[cnt]);
                        LoopStream loop = new LoopStream(audioFileReader);
                        waveOutDevice.Init(loop);
                        waveOutDevice.Play();
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
                            quiz.list.RemoveAt(musicNumber);
                            waveOutDevice.Init(audioFileReader);
                            waveOutDevice.Play();
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
                            quiz.list.RemoveAt(musicNumber);
                            LoopStream loop = new LoopStream(audioFileReader);
                            waveOutDevice.Init(loop);
                            waveOutDevice.Play();
                        }
                    }
                    else
                    {
                        audioFileReader = new AudioFileReader(quiz.list[cnt]);
                        LoopStream loop = new LoopStream(audioFileReader);
                        waveOutDevice.Init(loop);
                        waveOutDevice.Play();
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
                            quiz.list.RemoveAt(musicNumber);
                            waveOutDevice.Init(audioFileReader);
                            waveOutDevice.Play();
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
            else
            {
                cancelAudioConn();
                _formPopup.ShowDialog();
            }
        }

        public static void cancelAudioConn()
        {
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
            this.Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if ( ( waveOutDevice == null && audioFileReader == null ) || ( waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Stopped ) )  // -- || - if one of the conditions is true
            {
                backToBegin();
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

        public static void backToBegin()
        {
            if (cnt > quiz.list.Count - 1 && fGame.cbRnd == true)
            {               
                cnt = 0;
                createAudioConn();    
            }
            else if (cnt > quiz.list.Count - 1 && fGame.cbRnd == false)
            {
                _formPopup.ShowDialog();
            }
            else
            {
                createAudioConn();
                cnt++;
            }
        }

        void MakeMusic()
        {
            //musicNumber = rnd.Next(0, quiz.list.Count);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }
    }
}
