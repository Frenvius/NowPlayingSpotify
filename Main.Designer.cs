namespace NowPlayingSpotify {
    partial class Main {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.LabelExit = new System.Windows.Forms.Label();
            this.PanelExit = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            this.MusicLabel = new System.Windows.Forms.Label();
            this.ArtistLabel = new System.Windows.Forms.Label();
            this.PanelMove = new System.Windows.Forms.PictureBox();
            this.PanelSettings = new System.Windows.Forms.PictureBox();
            this.ButtonSettings = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PanelExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelMove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelExit
            // 
            this.LabelExit.AutoSize = true;
            this.LabelExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.LabelExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelExit.ForeColor = System.Drawing.Color.White;
            this.LabelExit.Location = new System.Drawing.Point(278, 7);
            this.LabelExit.Name = "LabelExit";
            this.LabelExit.Size = new System.Drawing.Size(14, 13);
            this.LabelExit.TabIndex = 5;
            this.LabelExit.Text = "X";
            this.LabelExit.Click += new System.EventHandler(this.MinimizeToTray);
            // 
            // PanelExit
            // 
            this.PanelExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.PanelExit.Location = new System.Drawing.Point(270, 0);
            this.PanelExit.Name = "PanelExit";
            this.PanelExit.Size = new System.Drawing.Size(30, 25);
            this.PanelExit.TabIndex = 4;
            this.PanelExit.TabStop = false;
            this.PanelExit.Click += new System.EventHandler(this.MinimizeToTray);
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
            this.ArtistLabel.Location = new System.Drawing.Point(15, 67);
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
            // PanelSettings
            // 
            this.PanelSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.PanelSettings.InitialImage = null;
            this.PanelSettings.Location = new System.Drawing.Point(242, 0);
            this.PanelSettings.Name = "PanelSettings";
            this.PanelSettings.Size = new System.Drawing.Size(30, 25);
            this.PanelSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PanelSettings.TabIndex = 9;
            this.PanelSettings.TabStop = false;
            this.PanelSettings.Click += new System.EventHandler(this.Open_Settings);
            this.PanelSettings.Visible = false;
            // 
            // ButtonSettings
            // 
            this.ButtonSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.ButtonSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonSettings.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSettings.Image")));
            this.ButtonSettings.InitialImage = null;
            this.ButtonSettings.Location = new System.Drawing.Point(250, 6);
            this.ButtonSettings.Name = "ButtonSettings";
            this.ButtonSettings.Size = new System.Drawing.Size(14, 14);
            this.ButtonSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ButtonSettings.TabIndex = 10;
            this.ButtonSettings.TabStop = false;
            this.ButtonSettings.Click += new System.EventHandler(this.Open_Settings);
            this.ButtonSettings.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(26)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(300, 100);
            this.Controls.Add(this.ButtonSettings);
            this.Controls.Add(this.PanelSettings);
            this.Controls.Add(this.ArtistLabel);
            this.Controls.Add(this.MusicLabel);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.LabelExit);
            this.Controls.Add(this.PanelExit);
            this.Controls.Add(this.PanelMove);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.PanelExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelMove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        internal System.Windows.Forms.PictureBox PanelSettings;

        internal System.Windows.Forms.PictureBox ButtonSettings;

        private System.Windows.Forms.Label ArtistLabel;

        private System.Windows.Forms.Label MusicLabel;

        private System.Windows.Forms.Label Title;
        #endregion

        internal System.Windows.Forms.Label LabelExit;
        internal System.Windows.Forms.PictureBox PanelExit;
        internal System.Windows.Forms.PictureBox PanelMove;
    }
}