using System.Windows.Forms;

namespace NowPlayingSpotify {
    partial class AppSettings {
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
            this.LabelExit = new System.Windows.Forms.Label();
            this.PanelExit = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            this.PanelMove = new System.Windows.Forms.PictureBox();
            this.StartWithWindows = new System.Windows.Forms.CheckBox();
            this.VersionPanel = new System.Windows.Forms.PictureBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PanelExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelMove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VersionPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelExit
            // 
            this.LabelExit.AutoSize = true;
            this.LabelExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.LabelExit.ForeColor = System.Drawing.Color.White;
            this.LabelExit.Location = new System.Drawing.Point(278, 7);
            this.LabelExit.Name = "LabelExit";
            this.LabelExit.Size = new System.Drawing.Size(14, 13);
            this.LabelExit.TabIndex = 5;
            this.LabelExit.Text = "X";
            this.LabelExit.Click += new System.EventHandler(this.Close_Settings);
            // 
            // PanelExit
            // 
            this.PanelExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.PanelExit.Location = new System.Drawing.Point(270, 0);
            this.PanelExit.Name = "PanelExit";
            this.PanelExit.Size = new System.Drawing.Size(30, 25);
            this.PanelExit.TabIndex = 4;
            this.PanelExit.TabStop = false;
            this.PanelExit.Click += new System.EventHandler(this.Close_Settings);
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.Title.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Title.Location = new System.Drawing.Point(12, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(252, 25);
            this.Title.TabIndex = 6;
            this.Title.Text = "Settings";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // StartWithWindows
            // 
            this.StartWithWindows.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.StartWithWindows.Location = new System.Drawing.Point(14, 36);
            this.StartWithWindows.Name = "StartWithWindows";
            this.StartWithWindows.Size = new System.Drawing.Size(274, 24);
            this.StartWithWindows.TabIndex = 7;
            this.StartWithWindows.Text = "Start application with windows";
            this.StartWithWindows.UseVisualStyleBackColor = true;
            this.StartWithWindows.CheckedChanged += new System.EventHandler(this.ToggleWithWindowsSetting);
            // 
            // VersionPanel
            // 
            this.VersionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.VersionPanel.Location = new System.Drawing.Point(0, 69);
            this.VersionPanel.Name = "VersionPanel";
            this.VersionPanel.Size = new System.Drawing.Size(300, 15);
            this.VersionPanel.TabIndex = 8;
            this.VersionPanel.TabStop = false;
            // 
            // VersionLabel
            // 
            this.VersionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.VersionLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.VersionLabel.Location = new System.Drawing.Point(12, 68);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(252, 15);
            this.VersionLabel.TabIndex = 9;
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AppSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(26)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(300, 85);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.VersionPanel);
            this.Controls.Add(this.StartWithWindows);
            this.Controls.Add(this.LabelExit);
            this.Controls.Add(this.PanelExit);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.PanelMove);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "AppSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.PanelExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelMove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VersionPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label VersionLabel;

        internal System.Windows.Forms.PictureBox VersionPanel;

        private System.Windows.Forms.CheckBox StartWithWindows;

        private System.Windows.Forms.Label Title;
        #endregion

        internal System.Windows.Forms.Label LabelExit;
        internal System.Windows.Forms.PictureBox PanelExit;
        internal System.Windows.Forms.PictureBox PanelMove;
    }
}