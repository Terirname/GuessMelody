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

            fGame _fGame = new fGame();
            quiz _quiz = new quiz();
            connection _connection = new connection();
            _connection.cancelAudioConn();
            LoopStream.countm = 0;
            LoopStream.i = 0;
            fGame.cnt = 0;
            _fGame.timer1.Stop();
            quiz.list.Clear();
            _quiz.ReadParam();
            _quiz.ReadMusic();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formPopup_Load(object sender, EventArgs e)
        {
             fGame _fGame = new fGame();
            _fGame.timer1.Stop();
        }
    }
}
