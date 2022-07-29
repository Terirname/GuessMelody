using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Diagnostics;

namespace GuessMelody
{
    internal class connection
    {       
        public static IWavePlayer waveOutDevice;
        public static AudioFileReader audioFileReader;

        public void initialConn(bool isLoop, bool isRnd)
        {
            if (isRnd)
            {
                audioFileReader = new AudioFileReader(quiz.list[fGame.musicNumber]);
            }
            else
            {
                audioFileReader = new AudioFileReader(quiz.list[fGame.cnt]);
            }

            if (isLoop)
            {                
                LoopStream loop = new LoopStream(audioFileReader);
                waveOutDevice.Init(loop);                
            }
            else
            {
                waveOutDevice.Init(audioFileReader);
            }

            waveOutDevice.Play();

        }

        public void createAudioConn()
        {
            cancelAudioConn();
            fGame _fGame = new fGame();
            waveOutDevice = new WaveOutEvent();
            // audioFileReader = new AudioFileReader(@"E:\C# projects (lessons)\GuessMelody\Melodies\melody_1.mp3");
            initialConn(LoopStream.cbLoop, _fGame.cbRnd);                            
        }

        public void cancelAudioConn()
        {
            if (waveOutDevice != null)
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
