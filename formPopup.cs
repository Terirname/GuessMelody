using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace GuessMelody
{
    public partial class FormPopup : Form
    {
        public static bool IsCancel { get; set; }

        public FormPopup()
        {
            InitializeComponent();
        }       
        private void BtnReset_Click(object sender, EventArgs e)
        {

            FGame _fGame = new();
            Connection.CancelAudioConn();
            LoopStream.Countm = 0;
            LoopStream.I = 0;
            FGame.Cnt = 0;
            _fGame.timer1.Stop();
            Quiz.List.Clear();
            Quiz.ReadParam();
            Quiz.ReadMusic();
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            IsCancel = true;
            this.Close();
        }

        private void FormPopup_Load(object sender, EventArgs e)
        {
            IsCancel = false;
            Connection.CancelAudioConn();
            FGame _fGame = new();
            _fGame.timer1.Stop();
        }
    }
}
