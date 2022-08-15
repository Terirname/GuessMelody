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
        protected Connection()
        {

        }

        public static IWavePlayer? WaveOutDevice { get; set; }
        private static AudioFileReader? audioFileReader;
        public static readonly Random rnd = new();
        public static bool RndPart { get; set; }    
        
        public static void InitialConn(bool isLoop, bool isRnd)
        {
            if (isRnd)
            {
                audioFileReader = new AudioFileReader(Quiz.List[FGame.MusicNumber]);
                RandomPart(audioFileReader);
            }
            else
            {
                audioFileReader = new AudioFileReader(Quiz.List[FGame.Cnt]);
                RandomPart(audioFileReader);
            }

            if (isLoop && WaveOutDevice != null)
            {                
                LoopStream loop = new(audioFileReader);
                WaveOutDevice.Init(loop);
            }
            else if (WaveOutDevice != null)
            {
                WaveOutDevice.Init(audioFileReader);
            }

            WaveOutDevice!.Play();                         
        }

        public static void CreateAudioConn()
        {
            CancelAudioConn();
            WaveOutDevice = new WaveOutEvent();
            // audioFileReader = new AudioFileReader(@"E:\C# projects (lessons)\GuessMelody\Melodies\melody_1.mp3");
            InitialConn(LoopStream.CbLoop, FGame.CbRnd);                            
        }

        public static void CancelAudioConn()
        {
            if (WaveOutDevice != null && audioFileReader != null)
            {
                WaveOutDevice.Stop();
                audioFileReader.Dispose();
                WaveOutDevice.Dispose();
                WaveOutDevice = null;
                audioFileReader = null;
            }
        }

        private static void RandomPart(AudioFileReader audioFileReader)
        {
            if (RndPart)
            {
                audioFileReader.Position = rnd.Next(0, (int)audioFileReader.Length / 2);
            }
            
        }
    }
}
