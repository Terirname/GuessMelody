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
    public partial class fParam : Form
    {
        public fParam()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            quiz.allDirectories = cbAllFolders.Checked;
            quiz.gameDuration = Convert.ToInt32(cbGameDuration.Text);
            quiz.musicDuration = Convert.ToInt32(cbMusicDuration.Text);
            quiz.randomStart = cbRandomStart.Checked;
            LoopStream.cbLoop = cbLoop.Checked;
            fGame.cbRnd = cbRandomStart.Checked;
            quiz.WriteParam();
            fGame.cancelAudioConn();
            LoopStream.countm = 0;
            LoopStream.i = 0;
            fGame.cnt = 0;
            quiz.ReadParam();
            quiz.ReadMusic();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setParam();
            this.Hide();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string[] music_list = Directory.GetFiles(fbd.SelectedPath, "*.mp3", cbAllFolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly); // -- Namespace System.IO included "Directory.GetFiles" and "SearchOption"
                quiz.lastFolder = fbd.SelectedPath;
                listBox1.Items.Clear();
                listBox1.Items.AddRange(music_list);
                quiz.list.Clear();
                quiz.list.AddRange(music_list);
            }
        }

        void setParam()
        {
            cbAllFolders.Checked = quiz.allDirectories;
            cbGameDuration.Text = quiz.gameDuration.ToString();
            cbMusicDuration.Text = quiz.musicDuration.ToString();
            cbRandomStart.Checked = quiz.randomStart;
            cbLoop.Checked = LoopStream.cbLoop;
            cbRandomStart.Checked = fGame.cbRnd;
        }

        private void fParam_Load(object sender, EventArgs e)
        {
            setParam();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(quiz.list.ToArray());
        }

        private void cbLoop_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbRandomStart_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
