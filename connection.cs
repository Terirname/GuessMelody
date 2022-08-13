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
        public static readonly Random rnd = new();
        public static bool rndPart = false;

        public static void InitialConn(bool isLoop, bool isRnd)
        {
            if (isRnd)
            {
                audioFileReader = new AudioFileReader(Quiz.list[FGame.musicNumber]);
                RandomPart(audioFileReader);
            }
            else
            {
                audioFileReader = new AudioFileReader(Quiz.list[FGame.cnt]);
                RandomPart(audioFileReader);
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

            waveOutDevice!.Play();                         
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

        private static void RandomPart(AudioFileReader audioFileReader)
        {
            if (rndPart)
            {
                audioFileReader.Position = rnd.Next(0, (int)audioFileReader.Length / 2);
            }
            
        }
    }
}
