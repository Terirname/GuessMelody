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

        private static IWavePlayer? waveOutDevice;
        private static AudioFileReader? audioFileReader;
        public static readonly Random rnd = new();
        private static bool rndPart;

        public static IWavePlayer? Get_waveOutDevice()
        {
            return waveOutDevice;
        }
        public static void Set_waveOutDevice(IWavePlayer? waveOutDevice_public)
        {
             waveOutDevice = waveOutDevice_public;
        }
        public static void Set_rndPart(bool rndPart_public)
        {
            rndPart = rndPart_public;
        }

        public static bool Get_rndPart()
        {
            return rndPart;
        }


        public static void InitialConn(bool isLoop, bool isRnd)
        {
            if (isRnd)
            {
                audioFileReader = new AudioFileReader(Quiz.Get_list()[FGame.Get_musicNumber()]);
                RandomPart(audioFileReader);
            }
            else
            {
                audioFileReader = new AudioFileReader(Quiz.Get_list()[FGame.Get_cnt()]);
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
            InitialConn(LoopStream.Get_cbLoop(), FGame.Get_cbRnd());                            
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
