namespace OfficeRemoteService
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.OfficeRemoteNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.OfficeRemoteContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OfficeRemoteContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(48, 15);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(100, 20);
            this.tbPort.TabIndex = 1;
            this.tbPort.Text = "59256";
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(154, 13);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(110, 23);
            this.btnRegister.TabIndex = 2;
            this.btnRegister.Text = "Start Service";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(12, 42);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(360, 207);
            this.rtbLog.TabIndex = 3;
            this.rtbLog.Text = "";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(270, 13);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(101, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop Service";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // OfficeRemoteNotifyIcon
            // 
            this.OfficeRemoteNotifyIcon.ContextMenuStrip = this.OfficeRemoteContextMenuStrip;
            this.OfficeRemoteNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("OfficeRemoteNotifyIcon.Icon")));
            this.OfficeRemoteNotifyIcon.Text = "Office Remote Service";
            this.OfficeRemoteNotifyIcon.Visible = true;
            this.OfficeRemoteNotifyIcon.DoubleClick += new System.EventHandler(this.OfficeRemoteNotifyIcon_DoubleClick);
            // 
            // OfficeRemoteContextMenuStrip
            // 
            this.OfficeRemoteContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.OfficeRemoteContextMenuStrip.Name = "OfficeRemoteContextMenuStrip";
            this.OfficeRemoteContextMenuStrip.Size = new System.Drawing.Size(153, 70);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Office Remote Service";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.OfficeRemoteContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnStop;
		  private System.Windows.Forms.NotifyIcon OfficeRemoteNotifyIcon;
          private System.Windows.Forms.ContextMenuStrip OfficeRemoteContextMenuStrip;
          private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
          private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}

