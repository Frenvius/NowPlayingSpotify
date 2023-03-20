namespace NowPlayingSpotify {
    partial class Form1 {
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
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LabelExit = new System.Windows.Forms.Label();
            this.PanelExit = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            this.MusicLabel = new System.Windows.Forms.Label();
            this.ArtistLabel = new System.Windows.Forms.Label();
            this.PanelMove = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PanelExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelMove)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelExit
            // 
            this.LabelExit.AutoSize = true;
            this.LabelExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.LabelExit.ForeColor = System.Drawing.Color.White;
            this.LabelExit.Location = new System.Drawing.Point(278, 6);
            this.LabelExit.Name = "LabelExit";
            this.LabelExit.Size = new System.Drawing.Size(14, 13);
            this.LabelExit.TabIndex = 5;
            this.LabelExit.Text = "X";
            this.LabelExit.Click += new System.EventHandler(this.LabelExit_Click);
            // 
            // PanelExit
            // 
            this.PanelExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.PanelExit.Location = new System.Drawing.Point(270, 0);
            this.PanelExit.Name = "PanelExit";
            this.PanelExit.Size = new System.Drawing.Size(30, 25);
            this.PanelExit.TabIndex = 4;
            this.PanelExit.TabStop = false;
            this.PanelExit.Click += new System.EventHandler(this.PanelExit_Click);
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.Title.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Title.Location = new System.Drawing.Point(12, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(100, 25);
            this.Title.TabIndex = 6;
            this.Title.Text = "Now Playing";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MusicLabel
            // 
            this.MusicLabel.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MusicLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.MusicLabel.Location = new System.Drawing.Point(12, 37);
            this.MusicLabel.Name = "MusicLabel";
            this.MusicLabel.Size = new System.Drawing.Size(276, 30);
            this.MusicLabel.TabIndex = 7;
            this.MusicLabel.Text = "No music playing";
            // 
            // ArtistLabel
            // 
            this.ArtistLabel.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ArtistLabel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.ArtistLabel.Location = new System.Drawing.Point(12, 67);
            this.ArtistLabel.Name = "ArtistLabel";
            this.ArtistLabel.Size = new System.Drawing.Size(276, 25);
            this.ArtistLabel.TabIndex = 8;
            // 
            // PanelMove
            // 
            this.PanelMove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.PanelMove.Location = new System.Drawing.Point(0, 0);
            this.PanelMove.Name = "PanelMove";
            this.PanelMove.Size = new System.Drawing.Size(300, 25);
            this.PanelMove.TabIndex = 3;
            this.PanelMove.TabStop = false;
            this.PanelMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseDown);
            this.PanelMove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseMove);
            this.PanelMove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(26)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(300, 100);
            this.Controls.Add(this.ArtistLabel);
            this.Controls.Add(this.MusicLabel);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.LabelExit);
            this.Controls.Add(this.PanelExit);
            this.Controls.Add(this.PanelMove);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.PanelExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelMove)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label ArtistLabel;

        private System.Windows.Forms.Label MusicLabel;

        private System.Windows.Forms.Label Title;
        #endregion

        internal System.Windows.Forms.Label LabelExit;
        internal System.Windows.Forms.PictureBox PanelExit;
        internal System.Windows.Forms.PictureBox PanelMove;
    }
}