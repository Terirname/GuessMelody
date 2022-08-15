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
        private static bool isCancel;

        public FormPopup()
        {
            InitializeComponent();
        }

        public static bool Get_isCancel()
        {
            return isCancel;
        }
        public static void Set_isCancel(bool isCancel_public)
        {
            isCancel = isCancel_public;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {

            FGame _fGame = new();
            Connection.CancelAudioConn();
            LoopStream.Set_countm(0);
            LoopStream.SetI(0);
            FGame.Set_cnt(0);
            _fGame.timer1.Stop();
            Quiz.Get_list().Clear();
            Quiz.ReadParam();
            Quiz.ReadMusic();
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Set_isCancel(true);
            this.Close();
        }

        private void FormPopup_Load(object sender, EventArgs e)
        {
            Set_isCancel(false);
            Connection.CancelAudioConn();
            FGame _fGame = new();
            _fGame.timer1.Stop();
        }
    }
}
