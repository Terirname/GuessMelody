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
            Quiz.Set_allDirectories(cbAllFolders.Checked);
            Quiz.Set_gameDuration(Convert.ToInt32(cbGameDuration.Text));
            Quiz.Set_musicDuration(Convert.ToInt32(cbMusicDuration.Text));
            Quiz.Set_startRndTrack(cbRandomStart.Checked);
            LoopStream.Set_cbLoop(cbLoop.Checked);
            FGame.Set_cbRnd(cbRandomStart.Checked);
            Connection.Set_rndPart(cbRndPart.Checked);
            Quiz.WriteParam();
            LoopStream.Set_countm(0);
            LoopStream.SetI(0);
            FGame.Set_cnt(0);
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
                Quiz.Set_lastFolder(fbd.SelectedPath);
                listBox1.Items.Clear();
                listBox1.Items.AddRange(music_list);
                Quiz.Get_list().Clear();
                Quiz.Get_list().AddRange(music_list);
            }
        }

        void SetParam()
        {
            cbAllFolders.Checked = Quiz.Get_allDirectories();
            cbGameDuration.Text = Quiz.Get_gameDuration().ToString();
            cbMusicDuration.Text = Quiz.Get_musicDuration().ToString();
            cbRandomStart.Checked = Quiz.Get_startRndTrack();
            cbLoop.Checked = LoopStream.Get_cbLoop();
            cbRandomStart.Checked = FGame.Get_cbRnd();
            cbRndPart.Checked = Connection.Get_rndPart();
        }

        private void FParam_Load(object sender, EventArgs e)
        {
            SetParam();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Quiz.Get_list().ToArray());
        }

    }
}
