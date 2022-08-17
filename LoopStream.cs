using NAudio.Wave;

namespace GuessMelody
{
    /// <summary>
    /// Stream for looping playback
    /// </summary>
    internal class LoopStream : WaveStream
    {
        private WaveStream? _sourceStream;
        public static bool CbLoop { get; set; }
        public static int Countm { get; set; }
        public static int I { get; set; }        
        private readonly static Random lrnd = new();
        private readonly FormPopup _formPopup = new();
        private readonly FGame _fGame = new();
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
                if (Quiz.List.Count <= 0)
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
            int lmusicNumber;

            while (totalBytesRead < count && _sourceStream != null)
            {                
                int bytesRead = _sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (CbLoop)
                    {
                        if (_sourceStream.Position == 0 || !EnableLooping)
                        {
                            // something wrong with the source stream
                            break;
                        }
                        // loop                        
                        I++;
                        if (Countm > Quiz.List.Count - 1)
                        {
                            if (Quiz.List.Count <= 0)
                            {
                                Quiz.List.Clear();
                                Connection.CancelAudioConn();
                                _formPopup.ShowDialog();
                                break;
                            }
                            if (!FGame.CbRnd)
                            {
                                Quiz.List.Clear();
                                Connection.CancelAudioConn();
                                _formPopup.ShowDialog();
                                break;
                            }
                            else
                            {
                                Countm = 0;
                                _sourceStream = new AudioFileReader(Quiz.List[Countm]);
                                Countm++;
                                _sourceStream.Position = 0;
                            }
                        }
                        else
                        {
                            if (I == 1)
                            {
                                if (FGame.CbRnd)
                                {
                                    if (Quiz.List.Count <= 0)
                                    {
                                        break;
                                    }
                                    //Quiz.list.RemoveAt(lmusicNumber);
                                    lmusicNumber = lrnd.Next(0, Quiz.List.Count);
                                    _sourceStream = new AudioFileReader(Quiz.List[lmusicNumber]);
                                    Quiz.List.RemoveAt(lmusicNumber);
                                    try
                                    {
                                        _fGame.lblNumberOfMelody.Invoke(() => _fGame.lblNumberOfMelody.Text = Quiz.List.Count.ToString());
                                    }
                                    catch
                                    {
                                        _fGame.lblNumberOfMelody.Text = Quiz.List.Count.ToString();
                                    }
                                    //countm = countm + 2;
                                    _sourceStream.Position = 0;                                   
                                }
                                else
                                {
                                    _sourceStream = new AudioFileReader(Quiz.List[Countm + 1]);
                                    try
                                    {
                                        _fGame.lblNumberOfMelody.Invoke(() => _fGame.lblNumberOfMelody.Text = (Quiz.List.Count - Countm - 2).ToString());
                                    }
                                    catch
                                    {
                                        _fGame.lblNumberOfMelody.Text = (Quiz.List.Count - Countm - 2).ToString();
                                    }
                                    Countm += 2;
                                    _sourceStream.Position = 0;
                                }
                            }
                            else
                            {
                                if (FGame.CbRnd)
                                {
                                    if (Quiz.List.Count <= 0)
                                    {
                                        Quiz.List.Clear();
                                        Connection.CancelAudioConn();
                                        _formPopup.ShowDialog();
                                        break;
                                    }
                                    lmusicNumber = lrnd.Next(0, Quiz.List.Count);
                                    _sourceStream = new AudioFileReader(Quiz.List[lmusicNumber]);
                                    Quiz.List.RemoveAt(lmusicNumber);
                                    _fGame.lblNumberOfMelody.Invoke(() => _fGame.lblNumberOfMelody.Text = Quiz.List.Count.ToString());
                                    //countm++;
                                    _sourceStream.Position = 0;
                                }
                                else
                                {
                                    _sourceStream = new AudioFileReader(Quiz.List[Countm]);
                                    _fGame.lblNumberOfMelody.Invoke(() => _fGame.lblNumberOfMelody.Text = (Quiz.List.Count - Countm - 1).ToString());
                                    Countm++;
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
