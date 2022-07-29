namespace GuessMelody
{
    partial class fGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fGame));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCounter1 = new System.Windows.Forms.Label();
            this.lblCounter2 = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.lblNumberOfMelody = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblMusicDuration = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(714, 479);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 39);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(615, 479);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(93, 39);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "Play/Stop";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNext.Location = new System.Drawing.Point(716, 427);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(91, 35);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(232, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Player 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(528, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Player 2";
            // 
            // lblCounter1
            // 
            this.lblCounter1.AutoSize = true;
            this.lblCounter1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCounter1.Location = new System.Drawing.Point(267, 99);
            this.lblCounter1.Name = "lblCounter1";
            this.lblCounter1.Size = new System.Drawing.Size(27, 32);
            this.lblCounter1.TabIndex = 5;
            this.lblCounter1.Text = "0";
            // 
            // lblCounter2
            // 
            this.lblCounter2.AutoSize = true;
            this.lblCounter2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCounter2.Location = new System.Drawing.Point(562, 99);
            this.lblCounter2.Name = "lblCounter2";
            this.lblCounter2.Size = new System.Drawing.Size(27, 32);
            this.lblCounter2.TabIndex = 6;
            this.lblCounter2.Text = "0";
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(219, 443);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(93, 39);
            this.btnPause.TabIndex = 7;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(219, 488);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(93, 39);
            this.btnContinue.TabIndex = 8;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // lblNumberOfMelody
            // 
            this.lblNumberOfMelody.AutoSize = true;
            this.lblNumberOfMelody.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNumberOfMelody.Location = new System.Drawing.Point(413, 99);
            this.lblNumberOfMelody.Name = "lblNumberOfMelody";
            this.lblNumberOfMelody.Size = new System.Drawing.Size(27, 32);
            this.lblNumberOfMelody.TabIndex = 9;
            this.lblNumberOfMelody.Text = "0";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(21, 249);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(786, 44);
            this.progressBar1.TabIndex = 10;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblMusicDuration
            // 
            this.lblMusicDuration.AutoSize = true;
            this.lblMusicDuration.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMusicDuration.Location = new System.Drawing.Point(407, 35);
            this.lblMusicDuration.Name = "lblMusicDuration";
            this.lblMusicDuration.Size = new System.Drawing.Size(33, 40);
            this.lblMusicDuration.TabIndex = 11;
            this.lblMusicDuration.Text = "0";
            // 
            // fGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackgroundImage = global::GuessMelody.Properties.Resources.game_bg;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(839, 577);
            this.ControlBox = false;
            this.Controls.Add(this.lblMusicDuration);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblNumberOfMelody);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.lblCounter2);
            this.Controls.Add(this.lblCounter1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "fGame";
            this.Text = "Game";
            this.Activated += new System.EventHandler(this.fGame_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fGame_FormClosed);
            this.Load += new System.EventHandler(this.fGame_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fGame_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnCancel;
        private Button btnPlay;
        private Button btnNext;
        private Label label1;
        private Label label2;
        private Button btnPause;
        private Button btnContinue;
        public Label lblCounter1;
        public Label lblCounter2;
        public Label lblMusicDuration;
        public ProgressBar progressBar1;
        public System.Windows.Forms.Timer timer1;
        public Label lblNumberOfMelody;
    }
}