using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace GuessMelody
{
     class Quiz
    {
        public static FGame? _fGame = new();
        static public List<string> list = new();
        public static int gameDuration = 60;
        public static int musicDuration = 10;
        static public bool rndStart = false;
        static public string lastFolder = "";
        static public bool allDirectories = false;
        private const string LastFolder = "LastFolder";
        private const string RandomStart = "RandomStart";
        private const string GameDuration = "GameDuration";
        private const string MusicDuration = "MusicDuration";
        private const string AllDirectories = "AllDirectories";
        private const string Looped = "Looped";


        public static void ReadMusic()
        {
            try 
            { 
                string[] music_files = Directory.GetFiles(lastFolder, "*.mp3", allDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                list.Clear();
                list.AddRange(music_files);
            }
            catch { }
        }

        static readonly string regKeyName = "Software\\MyProgrammingProjects\\GuessMelody";

        public static void WriteParam()
        {
            RegistryKey? rk = null;
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(regKeyName);
                if (rk == null) return;
                rk.SetValue(LastFolder, lastFolder);
                rk.SetValue(RandomStart, rndStart);
                rk.SetValue(GameDuration, gameDuration);
                rk.SetValue(MusicDuration, musicDuration);
                rk.SetValue(AllDirectories, allDirectories);
                rk.SetValue(Looped, LoopStream.cbLoop);
                rk.SetValue(RandomStart, FGame.cbRnd);
            }
            finally
            {
                if (rk != null) rk.Close();
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
                    lastFolder = (string)rk.GetValue(LastFolder)!;
                    gameDuration = (int)rk.GetValue(GameDuration)!;
                    rndStart = Convert.ToBoolean(rk.GetValue(RandomStart, false));
                    musicDuration = (int)rk.GetValue(MusicDuration)!;
                    allDirectories = Convert.ToBoolean(rk.GetValue(AllDirectories, false));
                    LoopStream.cbLoop = Convert.ToBoolean(rk.GetValue(Looped, false));
                    FGame.cbRnd = Convert.ToBoolean(rk.GetValue(RandomStart, false));
                }
                
            }
            finally
            {
                if (rk != null) rk.Close();
            }
        }
    }
}
