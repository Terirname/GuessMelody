using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Diagnostics;

namespace GuessMelody
{
    internal class Connection
    {       
        public static IWavePlayer? waveOutDevice;
        public static AudioFileReader? audioFileReader;

        public static void InitialConn(bool isLoop, bool isRnd)
        {
            if (isRnd)
            {
                audioFileReader = new AudioFileReader(Quiz.list[FGame.musicNumber]);
            }
            else
            {
                audioFileReader = new AudioFileReader(Quiz.list[FGame.cnt]);
            }

            if (isLoop && waveOutDevice != null)
            {                
                LoopStream loop = new(audioFileReader);
                waveOutDevice.Init(loop);
            }
            else if (waveOutDevice != null)
            {
                waveOutDevice.Init(audioFileReader);
            }
            if (waveOutDevice != null)
            {
                waveOutDevice.Play();
            }               
        }

        public static void CreateAudioConn()
        {
            CancelAudioConn();
            waveOutDevice = new WaveOutEvent();
            // audioFileReader = new AudioFileReader(@"E:\C# projects (lessons)\GuessMelody\Melodies\melody_1.mp3");
            InitialConn(LoopStream.cbLoop, FGame.cbRnd);                            
        }

        public static void CancelAudioConn()
        {
            if (waveOutDevice != null && audioFileReader != null)
            {
                waveOutDevice.Stop();
                audioFileReader.Dispose();
                waveOutDevice.Dispose();
                waveOutDevice = null;
                audioFileReader = null;
            }
        }
    }
}
