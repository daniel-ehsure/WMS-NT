﻿namespace UI
{
    partial class MaterielTypeAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterielTypeAdd));
            this.lbl1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbEnd = new System.Windows.Forms.CheckBox();
            this.cbJx = new System.Windows.Forms.CheckBox();
            this.lblPid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.ForeColor = System.Drawing.Color.Red;
            this.lbl1.Location = new System.Drawing.Point(311, 21);
            this.lbl1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(11, 12);
            this.lbl1.TabIndex = 57;
            this.lbl1.Text = "*";
            this.lbl1.Visible = false;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(158, 17);
            this.txtName.MaxLength = 25;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 21);
            this.txtName.TabIndex = 47;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 56;
            this.label7.Text = "名称：";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "b_cencer.bmp");
            this.imageList1.Images.SetKeyName(1, "b_ok.bmp");
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(18, 228);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(325, 1);
            this.label6.TabIndex = 55;
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.BackColor = System.Drawing.Color.LightGray;
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuit.ImageIndex = 0;
            this.btnQuit.ImageList = this.imageList1;
            this.btnQuit.Location = new System.Drawing.Point(233, 242);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(70, 25);
            this.btnQuit.TabIndex = 50;
            this.btnQuit.Text = "返回(&L)";
            this.btnQuit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.LightGray;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.ImageIndex = 1;
            this.btnSave.ImageList = this.imageList1;
            this.btnSave.Location = new System.Drawing.Point(158, 242);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.TabIndex = 49;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 59;
            this.label2.Text = "上级编码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 59;
            this.label5.Text = "备注：";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(158, 125);
            this.txtMemo.MaxLength = 25;
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(150, 81);
            this.txtMemo.TabIndex = 58;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 59;
            this.label11.Text = "是否末级：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(32, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 59;
            this.label12.Text = "是否定检：";
            // 
            // cbEnd
            // 
            this.cbEnd.AutoSize = true;
            this.cbEnd.Location = new System.Drawing.Point(158, 74);
            this.cbEnd.Name = "cbEnd";
            this.cbEnd.Size = new System.Drawing.Size(15, 14);
            this.cbEnd.TabIndex = 60;
            this.cbEnd.UseVisualStyleBackColor = true;
            // 
            // cbJx
            // 
            this.cbJx.AutoSize = true;
            this.cbJx.Location = new System.Drawing.Point(158, 100);
            this.cbJx.Name = "cbJx";
            this.cbJx.Size = new System.Drawing.Size(15, 14);
            this.cbJx.TabIndex = 60;
            this.cbJx.UseVisualStyleBackColor = true;
            // 
            // lblPid
            // 
            this.lblPid.AutoSize = true;
            this.lblPid.Location = new System.Drawing.Point(156, 47);
            this.lblPid.Name = "lblPid";
            this.lblPid.Size = new System.Drawing.Size(17, 12);
            this.lblPid.TabIndex = 61;
            this.lblPid.Text = "Id";
            // 
            // MaterielTypeAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(362, 275);
            this.ControlBox = false;
            this.Controls.Add(this.lblPid);
            this.Controls.Add(this.cbJx);
            this.Controls.Add(this.cbEnd);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label6);
            this.Name = "MaterielTypeAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "增加物料类别";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbEnd;
        private System.Windows.Forms.CheckBox cbJx;
        private System.Windows.Forms.Label lblPid;
    }
}