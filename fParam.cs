using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;
using System.Diagnostics;

namespace GuessMelody
{
    public partial class FParam : Form
    {
        public FParam()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Quiz.allDirectories = cbAllFolders.Checked;
            Quiz.gameDuration = Convert.ToInt32(cbGameDuration.Text);
            Quiz.musicDuration = Convert.ToInt32(cbMusicDuration.Text);
            Quiz.startRndTrack = cbRandomStart.Checked;
            LoopStream.cbLoop = cbLoop.Checked;
            FGame.cbRnd = cbRandomStart.Checked;
            Connection.rndPart = cbRndPart.Checked;
            Quiz.WriteParam();
            LoopStream.countm = 0;
            LoopStream.SetI(0);
            FGame.cnt = 0;
            Quiz.ReadParam();
            Quiz.ReadMusic();
            this.Hide();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SetParam();
            this.Hide();
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string[] music_list = Directory.GetFiles(fbd.SelectedPath, "*.mp3", cbAllFolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly); // -- Namespace System.IO included "Directory.GetFiles" and "SearchOption"
                Quiz.lastFolder = fbd.SelectedPath;
                listBox1.Items.Clear();
                listBox1.Items.AddRange(music_list);
                Quiz.list.Clear();
                Quiz.list.AddRange(music_list);
            }
        }

        void SetParam()
        {
            cbAllFolders.Checked = Quiz.allDirectories;
            cbGameDuration.Text = Quiz.gameDuration.ToString();
            cbMusicDuration.Text = Quiz.musicDuration.ToString();
            cbRandomStart.Checked = Quiz.startRndTrack;
            cbLoop.Checked = LoopStream.cbLoop;
            cbRandomStart.Checked = FGame.cbRnd;
            cbRndPart.Checked = Connection.rndPart;
        }

        private void FParam_Load(object sender, EventArgs e)
        {
            SetParam();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Quiz.list.ToArray());
        }

    }
}
