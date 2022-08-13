using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NAudio;
using NAudio.Wave;
using System.Diagnostics;

namespace GuessMelody
{
    /// <summary>
    /// Stream for looping playback
    /// </summary>
    public class LoopStream : WaveStream
    {
        public static WaveStream? _sourceStream;
        static public bool cbLoop = false;
        static public int countm = 0;
        static public int i = 0;
        public static int lmusicNumber;
        public static Random lrnd = new();
        public FormPopup _formPopup = new();
        public FGame _fGame = new();
        /// <summary>
        /// Creates a new Loop stream
        /// </summary>
        /// <param name="sourceStream">The stream to read from. Note: the Read method of this stream should return 0 when it reaches the end
        /// or else we will not loop to the start again.</param>

        public LoopStream(WaveStream sourceStream)
        {
            _sourceStream = sourceStream;
            EnableLooping = true;
        }

        /// <summary>
        /// Use this to turn looping on or off
        /// </summary>

        public static bool EnableLooping { get; set; }

        /// <summary>
        /// Return source stream's wave format
        /// </summary>

        public override WaveFormat? WaveFormat
        {
            get {
                if (Quiz.list.Count <= 0)
                {
                    return null;
                }
                else if (_sourceStream != null) 
                { 
                    return _sourceStream.WaveFormat;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// LoopStream simply returns
        /// </summary>

        public override long Length
        {
            get 
            { 
                if (_sourceStream != null)
                {
                    return _sourceStream.Length;
                }
                else
                {
                    return 0;
                } 
            }
        }

        /// <summary>
        /// LoopStream simply passes on positioning to source stream
        /// </summary>

        public override long Position
        {
            get 
            {

                if (_sourceStream != null)
                {
                    return _sourceStream.Position;
                }
                else
                {
                    return 0;
                }
            }
            set 
            {

                if (_sourceStream != null)
                {
                    _sourceStream.Position = value;
                }
                else
                {
                    _sourceStream = null;
                }
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;
            

            while (totalBytesRead < count && _sourceStream != null)
            {                
                int bytesRead = _sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (cbLoop == true)
                    {
                        if (_sourceStream.Position == 0 || !EnableLooping)
                        {
                            // something wrong with the source stream
                            break;
                        }
                        // loop                    
                        i++;
                        if (countm > Quiz.list.Count - 1)
                        {
                            if (Quiz.list.Count <= 0)
                            {
                                Quiz.list.Clear();
                                Connection.CancelAudioConn();
                                _formPopup.ShowDialog();
                                break;
                            }
                            if (FGame.cbRnd == false)
                            {
                                Quiz.list.Clear();
                                Connection.CancelAudioConn();
                                _formPopup.ShowDialog();
                                break;
                            }
                            else
                            {
                                countm = 0;
                                _sourceStream = new AudioFileReader(Quiz.list[countm]);
                                countm++;
                                _sourceStream.Position = 0;
                            }
                        }
                        else
                        {
                            if (i == 1)
                            {
                                if (FGame.cbRnd == true)
                                {
                                    if (Quiz.list.Count <= 0)
                                    {
                                        break;
                                    }
                                    //Quiz.list.RemoveAt(lmusicNumber);
                                    lmusicNumber = lrnd.Next(0, Quiz.list.Count);
                                    _sourceStream = new AudioFileReader(Quiz.list[lmusicNumber]);
                                    Quiz.list.RemoveAt(lmusicNumber);
                                    try
                                    {
                                        _fGame.lblNumberOfMelody.Invoke(() => _fGame.lblNumberOfMelody.Text = Quiz.list.Count.ToString());
                                    }
                                    catch
                                    {
                                        _fGame.lblNumberOfMelody.Text = Quiz.list.Count.ToString();
                                    }
                                    //countm = countm + 2;
                                    _sourceStream.Position = 0;                                   
                                }
                                else
                                {
                                    _sourceStream = new AudioFileReader(Quiz.list[countm + 1]);
                                    try
                                    {
                                        _fGame.lblNumberOfMelody.Invoke(() => _fGame.lblNumberOfMelody.Text = (Quiz.list.Count - countm - 2).ToString());
                                    }
                                    catch
                                    {
                                        _fGame.lblNumberOfMelody.Text = (Quiz.list.Count - countm - 2).ToString();
                                    }
                                    countm += 2;
                                    _sourceStream.Position = 0;
                                }
                            }
                            else
                            {
                                if (FGame.cbRnd == true)
                                {
                                    if (Quiz.list.Count <= 0)
                                    {
                                        Quiz.list.Clear();
                                        Connection.CancelAudioConn();
                                        _formPopup.ShowDialog();
                                        break;
                                    }
                                    lmusicNumber = lrnd.Next(0, Quiz.list.Count);
                                    _sourceStream = new AudioFileReader(Quiz.list[lmusicNumber]);
                                    Quiz.list.RemoveAt(lmusicNumber);
                                    _fGame.lblNumberOfMelody.Invoke(() => _fGame.lblNumberOfMelody.Text = Quiz.list.Count.ToString());
                                    //countm++;
                                    _sourceStream.Position = 0;
                                }
                                else
                                {
                                    _sourceStream = new AudioFileReader(Quiz.list[countm]);
                                    _fGame.lblNumberOfMelody.Invoke(() => _fGame.lblNumberOfMelody.Text = (Quiz.list.Count - countm - 1).ToString());
                                    countm++;
                                    _sourceStream.Position = 0;
                                }

                            }
                        }
                    }
                    _sourceStream.Position = 0;
                }                
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }

    }
}
