namespace UI
{
    partial class PlaceAddSingle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaceAddSingle));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblChildren = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lblPid = new System.Windows.Forms.Label();
            this.cbInuse = new System.Windows.Forms.CheckBox();
            this.cbEnd = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "b_cencer.bmp");
            this.imageList1.Images.SetKeyName(1, "b_ok.bmp");
            // 
            // lblChildren
            // 
            this.lblChildren.AutoSize = true;
            this.lblChildren.Location = new System.Drawing.Point(33, 278);
            this.lblChildren.Name = "lblChildren";
            this.lblChildren.Size = new System.Drawing.Size(17, 12);
            this.lblChildren.TabIndex = 97;
            this.lblChildren.Text = "Id";
            this.lblChildren.Visible = false;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(157, 16);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(17, 12);
            this.lblId.TabIndex = 96;
            this.lblId.Text = "Id";
            // 
            // lblPid
            // 
            this.lblPid.AutoSize = true;
            this.lblPid.Location = new System.Drawing.Point(157, 71);
            this.lblPid.Name = "lblPid";
            this.lblPid.Size = new System.Drawing.Size(17, 12);
            this.lblPid.TabIndex = 95;
            this.lblPid.Text = "Id";
            // 
            // cbInuse
            // 
            this.cbInuse.AutoSize = true;
            this.cbInuse.Location = new System.Drawing.Point(159, 184);
            this.cbInuse.Name = "cbInuse";
            this.cbInuse.Size = new System.Drawing.Size(15, 14);
            this.cbInuse.TabIndex = 93;
            this.cbInuse.UseVisualStyleBackColor = true;
            // 
            // cbEnd
            // 
            this.cbEnd.AutoSize = true;
            this.cbEnd.Location = new System.Drawing.Point(159, 158);
            this.cbEnd.Name = "cbEnd";
            this.cbEnd.Size = new System.Drawing.Size(15, 14);
            this.cbEnd.TabIndex = 94;
            this.cbEnd.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(33, 184);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 89;
            this.label12.Text = "是否可用：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(33, 158);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 88;
            this.label11.Text = "最小控制单元：";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(159, 209);
            this.txtMemo.MaxLength = 25;
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(150, 81);
            this.txtMemo.TabIndex = 87;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 92;
            this.label5.Text = "备注：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 90;
            this.label1.Text = "编码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 91;
            this.label2.Text = "上级编码：";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.ForeColor = System.Drawing.Color.Red;
            this.lbl1.Location = new System.Drawing.Point(312, 45);
            this.lbl1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(11, 12);
            this.lbl1.TabIndex = 86;
            this.lbl1.Text = "*";
            this.lbl1.Visible = false;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(159, 126);
            this.txtWidth.MaxLength = 25;
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(150, 21);
            this.txtWidth.TabIndex = 78;
            this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 85;
            this.label4.Text = "宽度：";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(159, 95);
            this.txtLength.MaxLength = 25;
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(150, 21);
            this.txtLength.TabIndex = 77;
            this.txtLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 84;
            this.label3.Text = "长度：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(159, 41);
            this.txtName.MaxLength = 25;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 21);
            this.txtName.TabIndex = 79;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 83;
            this.label7.Text = "名称：";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.LightGray;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.ImageIndex = 1;
            this.btnSave.ImageList = this.imageList1;
            this.btnSave.Location = new System.Drawing.Point(159, 319);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.TabIndex = 80;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.BackColor = System.Drawing.Color.LightGray;
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuit.ImageIndex = 0;
            this.btnQuit.ImageList = this.imageList1;
            this.btnQuit.Location = new System.Drawing.Point(234, 319);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(70, 25);
            this.btnQuit.TabIndex = 81;
            this.btnQuit.Text = "返回(&L)";
            this.btnQuit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(19, 305);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(325, 1);
            this.label6.TabIndex = 82;
            // 
            // PlaceAddSingle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(362, 361);
            this.ControlBox = false;
            this.Controls.Add(this.lblChildren);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblPid);
            this.Controls.Add(this.cbInuse);
            this.Controls.Add(this.cbEnd);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.label6);
            this.Name = "PlaceAddSingle";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "增加货位";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lblChildren;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblPid;
        private System.Windows.Forms.CheckBox cbInuse;
        private System.Windows.Forms.CheckBox cbEnd;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label6;


    }
}