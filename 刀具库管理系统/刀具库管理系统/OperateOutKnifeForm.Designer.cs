namespace UI
{
    partial class OperateOutKnifeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperateOutKnifeForm));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnShouGong = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInMeno = new System.Windows.Forms.TextBox();
            this.dtpIndate = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtInPlace = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblInMateriel = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtStand = new System.Windows.Forms.TextBox();
            this.lblMaterielName = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtMaterielName = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_Data = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(8, 407);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(920, 8);
            this.label4.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(8, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(920, 8);
            this.label3.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(928, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(8, 415);
            this.label2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(8, 415);
            this.label1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnShouGong);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(8, 365);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(920, 42);
            this.panel1.TabIndex = 26;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.LightGray;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.ImageIndex = 5;
            this.button1.ImageList = this.imageList2;
            this.button1.Location = new System.Drawing.Point(819, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 65;
            this.button1.Text = "关闭(&C)";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "p_inseter.bmp");
            this.imageList2.Images.SetKeyName(1, "p_save.bmp");
            this.imageList2.Images.SetKeyName(2, "undo1.bmp");
            this.imageList2.Images.SetKeyName(3, "enhanced.bmp");
            this.imageList2.Images.SetKeyName(4, "p_del.bmp");
            this.imageList2.Images.SetKeyName(5, "bjc12.BMP");
            this.imageList2.Images.SetKeyName(6, "select_mediafile_disable.bmp");
            this.imageList2.Images.SetKeyName(7, "rows.ico");
            this.imageList2.Images.SetKeyName(8, "tt.bmp");
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.ImageIndex = 2;
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(131, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 28;
            this.button2.Text = "联机队列(&L)";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bjc16.bmp");
            this.imageList1.Images.SetKeyName(1, "bjc04.bmp");
            this.imageList1.Images.SetKeyName(2, "bjc07.bmp");
            this.imageList1.Images.SetKeyName(3, "EYE.ICO");
            // 
            // btnShouGong
            // 
            this.btnShouGong.BackColor = System.Drawing.SystemColors.Control;
            this.btnShouGong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShouGong.ImageIndex = 1;
            this.btnShouGong.ImageList = this.imageList1;
            this.btnShouGong.Location = new System.Drawing.Point(26, 10);
            this.btnShouGong.Name = "btnShouGong";
            this.btnShouGong.Size = new System.Drawing.Size(99, 23);
            this.btnShouGong.TabIndex = 27;
            this.btnShouGong.Text = "手工出库(&H)";
            this.btnShouGong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnShouGong.UseVisualStyleBackColor = false;
            this.btnShouGong.Click += new System.EventHandler(this.btnShouGong_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtInMeno);
            this.groupBox1.Controls.Add(this.dtpIndate);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(920, 59);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出库单信息";
            // 
            // txtInMeno
            // 
            this.txtInMeno.Location = new System.Drawing.Point(285, 22);
            this.txtInMeno.Margin = new System.Windows.Forms.Padding(2);
            this.txtInMeno.MaxLength = 100;
            this.txtInMeno.Name = "txtInMeno";
            this.txtInMeno.Size = new System.Drawing.Size(316, 21);
            this.txtInMeno.TabIndex = 26;
            this.txtInMeno.TabStop = false;
            // 
            // dtpIndate
            // 
            this.dtpIndate.Location = new System.Drawing.Point(89, 22);
            this.dtpIndate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpIndate.Name = "dtpIndate";
            this.dtpIndate.Size = new System.Drawing.Size(107, 21);
            this.dtpIndate.TabIndex = 118;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(230, 26);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 12);
            this.label21.TabIndex = 25;
            this.label21.Text = "备   注：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(24, 26);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 117;
            this.label15.Text = "出库时间：";
            // 
            // txtInPlace
            // 
            this.txtInPlace.Location = new System.Drawing.Point(86, 58);
            this.txtInPlace.Margin = new System.Windows.Forms.Padding(2);
            this.txtInPlace.Name = "txtInPlace";
            this.txtInPlace.ReadOnly = true;
            this.txtInPlace.Size = new System.Drawing.Size(130, 21);
            this.txtInPlace.TabIndex = 112;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(23, 61);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 12);
            this.label22.TabIndex = 11;
            this.label22.Text = "货   位：";
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(8, 67);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(920, 8);
            this.label8.TabIndex = 28;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTypeName);
            this.groupBox2.Controls.Add(this.lblMax);
            this.groupBox2.Controls.Add(this.lblInMateriel);
            this.groupBox2.Controls.Add(this.btnOK);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.txtStand);
            this.groupBox2.Controls.Add(this.txtInPlace);
            this.groupBox2.Controls.Add(this.lblMaterielName);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.txtMaterielName);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(8, 75);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(920, 93);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "明细信息";
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.Location = new System.Drawing.Point(692, 68);
            this.lblTypeName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(71, 12);
            this.lblTypeName.TabIndex = 124;
            this.lblTypeName.Text = "lblTypeName";
            this.lblTypeName.Visible = false;
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(699, 48);
            this.lblMax.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(41, 12);
            this.lblMax.TabIndex = 123;
            this.lblMax.Text = "lblMax";
            this.lblMax.Visible = false;
            // 
            // lblInMateriel
            // 
            this.lblInMateriel.AutoSize = true;
            this.lblInMateriel.Location = new System.Drawing.Point(692, 24);
            this.lblInMateriel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInMateriel.Name = "lblInMateriel";
            this.lblInMateriel.Size = new System.Drawing.Size(83, 12);
            this.lblInMateriel.TabIndex = 122;
            this.lblInMateriel.Text = "lblInMateriel";
            this.lblInMateriel.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.ImageIndex = 0;
            this.btnOK.ImageList = this.imageList1;
            this.btnOK.Location = new System.Drawing.Point(536, 58);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 22);
            this.btnOK.TabIndex = 115;
            this.btnOK.Text = "确认(&O)";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(194, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(22, 21);
            this.button3.TabIndex = 10;
            this.button3.TabStop = false;
            this.button3.Text = "…";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtStand
            // 
            this.txtStand.Location = new System.Drawing.Point(304, 24);
            this.txtStand.Margin = new System.Windows.Forms.Padding(2);
            this.txtStand.Name = "txtStand";
            this.txtStand.ReadOnly = true;
            this.txtStand.Size = new System.Drawing.Size(111, 21);
            this.txtStand.TabIndex = 18;
            this.txtStand.TabStop = false;
            // 
            // lblMaterielName
            // 
            this.lblMaterielName.AutoSize = true;
            this.lblMaterielName.ForeColor = System.Drawing.Color.Red;
            this.lblMaterielName.Location = new System.Drawing.Point(217, 24);
            this.lblMaterielName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaterielName.Name = "lblMaterielName";
            this.lblMaterielName.Size = new System.Drawing.Size(11, 12);
            this.lblMaterielName.TabIndex = 29;
            this.lblMaterielName.Text = "*";
            this.lblMaterielName.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(239, 27);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 12);
            this.label26.TabIndex = 17;
            this.label26.Text = "规格型号：";
            // 
            // txtMaterielName
            // 
            this.txtMaterielName.Location = new System.Drawing.Point(89, 19);
            this.txtMaterielName.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaterielName.Name = "txtMaterielName";
            this.txtMaterielName.ReadOnly = true;
            this.txtMaterielName.Size = new System.Drawing.Size(107, 21);
            this.txtMaterielName.TabIndex = 111;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(24, 22);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(65, 12);
            this.label28.TabIndex = 8;
            this.label28.Text = "物料名称：";
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(8, 168);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(920, 8);
            this.label5.TabIndex = 30;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.dgv_Data);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(8, 176);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(920, 189);
            this.panel2.TabIndex = 31;
            // 
            // dgv_Data
            // 
            this.dgv_Data.AllowUserToAddRows = false;
            this.dgv_Data.AllowUserToDeleteRows = false;
            this.dgv_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Data.BackgroundColor = System.Drawing.Color.White;
            this.dgv_Data.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Data.Location = new System.Drawing.Point(0, 0);
            this.dgv_Data.MultiSelect = false;
            this.dgv_Data.Name = "dgv_Data";
            this.dgv_Data.ReadOnly = true;
            this.dgv_Data.RowHeadersVisible = false;
            this.dgv_Data.RowTemplate.Height = 23;
            this.dgv_Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Data.Size = new System.Drawing.Size(916, 185);
            this.dgv_Data.TabIndex = 65;
            this.dgv_Data.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Data_CellDoubleClick);
            // 
            // OperateOutKnifeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(936, 415);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "OperateOutKnifeForm";
            this.ShowInTaskbar = false;
            this.Text = "刀具出库管理";
            this.Load += new System.EventHandler(this.OperateInForm_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnShouGong;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtInMeno;
        private System.Windows.Forms.DateTimePicker dtpIndate;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtInPlace;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtStand;
        private System.Windows.Forms.Label lblMaterielName;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtMaterielName;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dgv_Data;
        private System.Windows.Forms.Label lblInMateriel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblTypeName;
    }
}