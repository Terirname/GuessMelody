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
    public partial class formPopup : Form
    {
        public formPopup()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            fGame.cancelAudioConn();
            LoopStream.countm = 0;
            LoopStream.i = 0;
            fGame.cnt = 0;
            quiz.ReadParam();
            quiz.ReadMusic();
            fGame.lblNumberOfMelody.Invoke(() => fGame.lblNumberOfMelody.Text = quiz.list.Count().ToString());
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
