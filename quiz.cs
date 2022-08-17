using Microsoft.Win32;

namespace GuessMelody
{
    internal class Quiz
    {
        protected Quiz()
        {

        }

        public static List<string> List { get; } = new();
        public static int GameDuration { get; set; } = 60;
        public static int MusicDuration { get; set; } = 10;
        public static bool StartRndTrack { get; set; }
        public static string LastFolder { get; set; }  = "";
        public static bool AllDirectories { get; set; }
        private const string LastFolderRg = "LastFolder";
        private const string StartRandomTrackRg = "StartRandomTrack";
        private const string RandomPartRg = "RandomPart";
        private const string GameDurationRg = "GameDuration";
        private const string MusicDurationRg = "MusicDuration";
        private const string AllDirectoriesRg = "AllDirectories";
        private const string LoopedRg = "Looped";
                                          
        public static void ReadMusic()
        {
            try 
            { 
                string[] music_files = Directory.GetFiles(LastFolder, "*.mp3", AllDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                List.Clear();
                List.AddRange(music_files);
            }
            catch 
            {
                MessageBox.Show("Error with reading the music_files! Please try again or select another path");
            }
        }

        static readonly string regKeyName = "Software\\MyProgrammingProjects\\GuessMelody";

        public static void WriteParam()
        {
            RegistryKey? rk = null;
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(regKeyName);
                if (rk == null) 
                { 
                    return; 
                }
                rk.SetValue(LastFolderRg, LastFolder);
                rk.SetValue(StartRandomTrackRg, StartRndTrack);
                rk.SetValue(GameDurationRg, GameDuration);
                rk.SetValue(MusicDurationRg, MusicDuration);
                rk.SetValue(AllDirectoriesRg, AllDirectories);
                rk.SetValue(LoopedRg, LoopStream.CbLoop);
                rk.SetValue(StartRandomTrackRg, FGame.CbRnd);
                rk.SetValue(RandomPartRg, Connection.RndPart);
            }
            finally
            {
                if (rk != null)
                {
                    rk.Close();
                }
            }
        }

        public static void ReadParam()
        {
            RegistryKey? rk = null;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(regKeyName);
                if (rk != null)
                {
                    LastFolder = (string)rk.GetValue(LastFolderRg)!;
                    GameDuration = (int)rk.GetValue(GameDurationRg)!;
                    StartRndTrack = Convert.ToBoolean(rk.GetValue(StartRandomTrackRg, false));
                    MusicDuration = (int)rk.GetValue(MusicDurationRg)!;
                    AllDirectories = Convert.ToBoolean(rk.GetValue(AllDirectoriesRg, false));
                    LoopStream.CbLoop = Convert.ToBoolean(rk.GetValue(LoopedRg, false));
                    FGame.CbRnd = Convert.ToBoolean(rk.GetValue(StartRandomTrackRg, false));
                    Connection.RndPart = Convert.ToBoolean(rk.GetValue(RandomPartRg, false));
                }
                
            }
            finally
            {
                if (rk != null)
                {
                    rk.Close();
                }
            }
        }
    }
}
