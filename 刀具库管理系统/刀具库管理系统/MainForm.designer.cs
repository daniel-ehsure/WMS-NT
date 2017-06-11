namespace UI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.基础数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tolMainTool = new System.Windows.Forms.ToolStrip();
            this.tbn_show = new System.Windows.Forms.ToolStripButton();
            this.tbnLogout = new System.Windows.Forms.ToolStripButton();
            this.tbnSetPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbnChangePassword = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBakData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbnExit = new System.Windows.Forms.ToolStripButton();
            this.lblPlaceholder3 = new System.Windows.Forms.Label();
            this.lblPlaceholder5 = new System.Windows.Forms.Label();
            this.lblPlaceholder6 = new System.Windows.Forms.Label();
            this.lblPlaceholder4 = new System.Windows.Forms.Label();
            this.MainToolBar = new 工具箱控件.OutlookBar();
            this.lblPlaceholder7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imlMain = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.tolMainTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基础数据ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 22);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // 基础数据ToolStripMenuItem
            // 
            this.基础数据ToolStripMenuItem.Name = "基础数据ToolStripMenuItem";
            this.基础数据ToolStripMenuItem.Size = new System.Drawing.Size(68, 18);
            this.基础数据ToolStripMenuItem.Text = "基础数据";
            // 
            // tolMainTool
            // 
            this.tolMainTool.BackColor = System.Drawing.Color.LightBlue;
            this.tolMainTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbn_show,
            this.tbnLogout,
            this.tbnSetPrint,
            this.toolStripSeparator1,
            this.tbnChangePassword,
            this.toolStripSeparator3,
            this.btnBakData,
            this.toolStripSeparator2,
            this.tbnExit});
            this.tolMainTool.Location = new System.Drawing.Point(0, 0);
            this.tolMainTool.Name = "tolMainTool";
            this.tolMainTool.Size = new System.Drawing.Size(984, 25);
            this.tolMainTool.TabIndex = 3;
            this.tolMainTool.Text = "toolStrip1";
            // 
            // tbn_show
            // 
            this.tbn_show.Image = ((System.Drawing.Image)(resources.GetObject("tbn_show.Image")));
            this.tbn_show.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbn_show.Name = "tbn_show";
            this.tbn_show.Size = new System.Drawing.Size(52, 22);
            this.tbn_show.Text = "隐藏";
            this.tbn_show.Click += new System.EventHandler(this.lblPlaceholder7_Click);
            // 
            // tbnLogout
            // 
            this.tbnLogout.Image = ((System.Drawing.Image)(resources.GetObject("tbnLogout.Image")));
            this.tbnLogout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnLogout.Name = "tbnLogout";
            this.tbnLogout.Size = new System.Drawing.Size(76, 22);
            this.tbnLogout.Text = "注销用户";
            this.tbnLogout.Click += new System.EventHandler(this.tbnLogout_Click);
            // 
            // tbnSetPrint
            // 
            this.tbnSetPrint.Image = ((System.Drawing.Image)(resources.GetObject("tbnSetPrint.Image")));
            this.tbnSetPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnSetPrint.Name = "tbnSetPrint";
            this.tbnSetPrint.Size = new System.Drawing.Size(88, 22);
            this.tbnSetPrint.Text = "设置打印机";
            this.tbnSetPrint.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbnChangePassword
            // 
            this.tbnChangePassword.Image = ((System.Drawing.Image)(resources.GetObject("tbnChangePassword.Image")));
            this.tbnChangePassword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnChangePassword.Name = "tbnChangePassword";
            this.tbnChangePassword.Size = new System.Drawing.Size(76, 22);
            this.tbnChangePassword.Text = "更改密码";
            this.tbnChangePassword.Click += new System.EventHandler(this.tbnChangePassword_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnBakData
            // 
            this.btnBakData.Image = ((System.Drawing.Image)(resources.GetObject("btnBakData.Image")));
            this.btnBakData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBakData.Name = "btnBakData";
            this.btnBakData.Size = new System.Drawing.Size(100, 22);
            this.btnBakData.Text = "初始化数据库";
            this.btnBakData.Visible = false;
            this.btnBakData.Click += new System.EventHandler(this.btnBakData_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tbnExit
            // 
            this.tbnExit.Image = ((System.Drawing.Image)(resources.GetObject("tbnExit.Image")));
            this.tbnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnExit.Name = "tbnExit";
            this.tbnExit.Size = new System.Drawing.Size(52, 22);
            this.tbnExit.Text = "退出";
            this.tbnExit.Click += new System.EventHandler(this.tbnExit_Click);
            // 
            // lblPlaceholder3
            // 
            this.lblPlaceholder3.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPlaceholder3.Location = new System.Drawing.Point(0, 25);
            this.lblPlaceholder3.Name = "lblPlaceholder3";
            this.lblPlaceholder3.Size = new System.Drawing.Size(984, 15);
            this.lblPlaceholder3.TabIndex = 6;
            // 
            // lblPlaceholder5
            // 
            this.lblPlaceholder5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPlaceholder5.Location = new System.Drawing.Point(0, 576);
            this.lblPlaceholder5.Name = "lblPlaceholder5";
            this.lblPlaceholder5.Size = new System.Drawing.Size(984, 18);
            this.lblPlaceholder5.TabIndex = 8;
            // 
            // lblPlaceholder6
            // 
            this.lblPlaceholder6.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPlaceholder6.Location = new System.Drawing.Point(0, 40);
            this.lblPlaceholder6.Name = "lblPlaceholder6";
            this.lblPlaceholder6.Size = new System.Drawing.Size(8, 536);
            this.lblPlaceholder6.TabIndex = 9;
            // 
            // lblPlaceholder4
            // 
            this.lblPlaceholder4.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPlaceholder4.Location = new System.Drawing.Point(976, 40);
            this.lblPlaceholder4.Name = "lblPlaceholder4";
            this.lblPlaceholder4.Size = new System.Drawing.Size(8, 536);
            this.lblPlaceholder4.TabIndex = 10;
            // 
            // MainToolBar
            // 
            this.MainToolBar.BackColor = System.Drawing.Color.LightBlue;
            this.MainToolBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainToolBar.ButtonHeight = 25;
            this.MainToolBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MainToolBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainToolBar.Location = new System.Drawing.Point(8, 40);
            this.MainToolBar.Name = "MainToolBar";
            this.MainToolBar.SelectedBand = 0;
            this.MainToolBar.Size = new System.Drawing.Size(128, 536);
            this.MainToolBar.TabIndex = 13;
            // 
            // lblPlaceholder7
            // 
            this.lblPlaceholder7.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPlaceholder7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPlaceholder7.ForeColor = System.Drawing.Color.Blue;
            this.lblPlaceholder7.Location = new System.Drawing.Point(136, 40);
            this.lblPlaceholder7.Name = "lblPlaceholder7";
            this.lblPlaceholder7.Size = new System.Drawing.Size(12, 536);
            this.lblPlaceholder7.TabIndex = 14;
            this.lblPlaceholder7.Text = "＜";
            this.lblPlaceholder7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPlaceholder7.Click += new System.EventHandler(this.lblPlaceholder7_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightBlue;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(148, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(828, 536);
            this.panel1.TabIndex = 16;
            // 
            // imlMain
            // 
            this.imlMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlMain.ImageStream")));
            this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imlMain.Images.SetKeyName(0, "jc24.bmp");
            this.imlMain.Images.SetKeyName(1, "jc01.bmp");
            this.imlMain.Images.SetKeyName(2, "jc02.bmp");
            this.imlMain.Images.SetKeyName(3, "jc03.bmp");
            this.imlMain.Images.SetKeyName(4, "jc04.bmp");
            this.imlMain.Images.SetKeyName(5, "jc05.bmp");
            this.imlMain.Images.SetKeyName(6, "jc06.bmp");
            this.imlMain.Images.SetKeyName(7, "jc07.bmp");
            this.imlMain.Images.SetKeyName(8, "jc08.bmp");
            this.imlMain.Images.SetKeyName(9, "jc09.bmp");
            this.imlMain.Images.SetKeyName(10, "jc10.bmp");
            this.imlMain.Images.SetKeyName(11, "jc11.bmp");
            this.imlMain.Images.SetKeyName(12, "jc12.bmp");
            this.imlMain.Images.SetKeyName(13, "jc13.bmp");
            this.imlMain.Images.SetKeyName(14, "jc14.bmp");
            this.imlMain.Images.SetKeyName(15, "jc15.bmp");
            this.imlMain.Images.SetKeyName(16, "jc16.bmp");
            this.imlMain.Images.SetKeyName(17, "jc17.bmp");
            this.imlMain.Images.SetKeyName(18, "jc18.bmp");
            this.imlMain.Images.SetKeyName(19, "jc19.bmp");
            this.imlMain.Images.SetKeyName(20, "jc20.bmp");
            this.imlMain.Images.SetKeyName(21, "jc21.bmp");
            this.imlMain.Images.SetKeyName(22, "jc22.bmp");
            this.imlMain.Images.SetKeyName(23, "jc23.bmp");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 594);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblPlaceholder7);
            this.Controls.Add(this.MainToolBar);
            this.Controls.Add(this.lblPlaceholder4);
            this.Controls.Add(this.lblPlaceholder6);
            this.Controls.Add(this.lblPlaceholder5);
            this.Controls.Add(this.lblPlaceholder3);
            this.Controls.Add(this.tolMainTool);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "点创科技——自动化生产库房报工系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tolMainTool.ResumeLayout(false);
            this.tolMainTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 基础数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tolMainTool;
        private System.Windows.Forms.ToolStripButton tbn_show;
        private System.Windows.Forms.ToolStripButton tbnSetPrint;
        private System.Windows.Forms.ToolStripButton tbnLogout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbnChangePassword;
        private System.Windows.Forms.ToolStripButton btnBakData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tbnExit;
        private System.Windows.Forms.Label lblPlaceholder3;
        private System.Windows.Forms.Label lblPlaceholder5;
        private System.Windows.Forms.Label lblPlaceholder6;
        private System.Windows.Forms.Label lblPlaceholder4;
        protected internal 工具箱控件.OutlookBar MainToolBar;
        private System.Windows.Forms.Label lblPlaceholder7;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imlMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

    }
}