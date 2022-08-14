using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace GuessMelody
{
    internal class Quiz
    {
        protected Quiz()
        {

        }

        private readonly static List<string> list = new();
        private static int gameDuration = 60;
        private static int musicDuration = 10;
        private static bool startRndTrack;
        private static string lastFolder = "";
        private static bool allDirectories;
        private const string LastFolder = "LastFolder";
        private const string StartRandomTrack = "StartRandomTrack";
        private const string RandomPart = "RandomPart";
        private const string GameDuration = "GameDuration";
        private const string MusicDuration = "MusicDuration";
        private const string AllDirectories = "AllDirectories";
        private const string Looped = "Looped";

        public static void Set_gameDuration(int gameDuration_public)
        {
            gameDuration = gameDuration_public;
        }

        public static int Get_gameDuration()
        {
            return gameDuration;
        }

        public static List<string> Get_list()
        {
            return list;
        }

        public static void Set_musicDuration(int musicDuration_public)
        {
            musicDuration = musicDuration_public;
        }

        public static int Get_musicDuration()
        {
            return musicDuration;
        }
        public static void Set_startRndTrack(bool startRndTrack_public)
        {
            startRndTrack = startRndTrack_public;
        }

        public static bool Get_startRndTrack()
        {
            return startRndTrack;
        }
        public static string Get_lastFolder()
        {
            return lastFolder;
        }
        public static void Set_lastFolder(string lastFolder_public)
        {
            lastFolder = lastFolder_public;
        }
        public static bool Get_allDirectories()
        {
            return allDirectories;
        }
        public static void Set_allDirectories(bool allDirectories_public)
        {
            allDirectories = allDirectories_public;
        }
        public static void ReadMusic()
        {
            try 
            { 
                string[] music_files = Directory.GetFiles(lastFolder, "*.mp3", allDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                list.Clear();
                list.AddRange(music_files);
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
                rk.SetValue(LastFolder, lastFolder);
                rk.SetValue(StartRandomTrack, startRndTrack);
                rk.SetValue(GameDuration, gameDuration);
                rk.SetValue(MusicDuration, musicDuration);
                rk.SetValue(AllDirectories, allDirectories);
                rk.SetValue(Looped, LoopStream.Get_cbLoop());
                rk.SetValue(StartRandomTrack, FGame.Get_cbRnd());
                rk.SetValue(RandomPart, Connection.Get_rndPart());
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
                    lastFolder = (string)rk.GetValue(LastFolder)!;
                    gameDuration = (int)rk.GetValue(GameDuration)!;
                    startRndTrack = Convert.ToBoolean(rk.GetValue(StartRandomTrack, false));
                    musicDuration = (int)rk.GetValue(MusicDuration)!;
                    allDirectories = Convert.ToBoolean(rk.GetValue(AllDirectories, false));
                    LoopStream.Set_cbLoop(Convert.ToBoolean(rk.GetValue(Looped, false)));
                    FGame.Set_cbRnd(Convert.ToBoolean(rk.GetValue(StartRandomTrack, false)));
                    Connection.Set_rndPart(Convert.ToBoolean(rk.GetValue(RandomPart, false)));
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
