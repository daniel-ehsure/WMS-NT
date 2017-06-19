namespace UI
{
    partial class PlaceAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaceAdd));
            this.txtName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.cbEnd = new System.Windows.Forms.CheckBox();
            this.lblPid = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(149, 47);
            this.txtName.MaxLength = 25;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(105, 21);
            this.txtName.TabIndex = 47;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(183, 21);
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
            this.label6.Location = new System.Drawing.Point(188, 259);
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
            this.btnQuit.Location = new System.Drawing.Point(403, 273);
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
            this.btnSave.Location = new System.Drawing.Point(328, 273);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.TabIndex = 49;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(423, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 59;
            this.label11.Text = "最小控制单元：";
            // 
            // cbEnd
            // 
            this.cbEnd.AutoSize = true;
            this.cbEnd.Location = new System.Drawing.Point(439, 50);
            this.cbEnd.Name = "cbEnd";
            this.cbEnd.Size = new System.Drawing.Size(15, 14);
            this.cbEnd.TabIndex = 60;
            this.cbEnd.UseVisualStyleBackColor = true;
            // 
            // lblPid
            // 
            this.lblPid.AutoSize = true;
            this.lblPid.Location = new System.Drawing.Point(25, 9);
            this.lblPid.Name = "lblPid";
            this.lblPid.Size = new System.Drawing.Size(29, 12);
            this.lblPid.TabIndex = 61;
            this.lblPid.Text = "上级";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(282, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 62;
            this.label1.Text = "长度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(358, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 62;
            this.label2.Text = "高度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 63;
            this.label3.Text = "数量：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(271, 47);
            this.textBox1.MaxLength = 25;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(61, 21);
            this.textBox1.TabIndex = 64;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(349, 47);
            this.textBox2.MaxLength = 25;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(61, 21);
            this.textBox2.TabIndex = 64;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(71, 47);
            this.textBox3.MaxLength = 25;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(61, 21);
            this.textBox3.TabIndex = 64;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(155, 74);
            this.textBox4.MaxLength = 25;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(105, 21);
            this.textBox4.TabIndex = 47;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(445, 77);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 60;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(277, 74);
            this.textBox5.MaxLength = 25;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(61, 21);
            this.textBox5.TabIndex = 64;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(355, 74);
            this.textBox6.MaxLength = 25;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(61, 21);
            this.textBox6.TabIndex = 64;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(77, 74);
            this.textBox7.MaxLength = 25;
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(61, 21);
            this.textBox7.TabIndex = 64;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(161, 101);
            this.textBox8.MaxLength = 25;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(105, 21);
            this.textBox8.TabIndex = 47;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(451, 104);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 60;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(283, 101);
            this.textBox9.MaxLength = 25;
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(61, 21);
            this.textBox9.TabIndex = 64;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(361, 101);
            this.textBox10.MaxLength = 25;
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(61, 21);
            this.textBox10.TabIndex = 64;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(83, 101);
            this.textBox11.MaxLength = 25;
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(61, 21);
            this.textBox11.TabIndex = 64;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(167, 128);
            this.textBox12.MaxLength = 25;
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(105, 21);
            this.textBox12.TabIndex = 47;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(457, 131);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(15, 14);
            this.checkBox3.TabIndex = 60;
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(289, 128);
            this.textBox13.MaxLength = 25;
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(61, 21);
            this.textBox13.TabIndex = 64;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(367, 128);
            this.textBox14.MaxLength = 25;
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(61, 21);
            this.textBox14.TabIndex = 64;
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(89, 128);
            this.textBox15.MaxLength = 25;
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(61, 21);
            this.textBox15.TabIndex = 64;
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(173, 155);
            this.textBox16.MaxLength = 25;
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(105, 21);
            this.textBox16.TabIndex = 47;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(463, 158);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(15, 14);
            this.checkBox4.TabIndex = 60;
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(295, 155);
            this.textBox17.MaxLength = 25;
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(61, 21);
            this.textBox17.TabIndex = 64;
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(373, 155);
            this.textBox18.MaxLength = 25;
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(61, 21);
            this.textBox18.TabIndex = 64;
            // 
            // textBox19
            // 
            this.textBox19.Location = new System.Drawing.Point(95, 155);
            this.textBox19.MaxLength = 25;
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(61, 21);
            this.textBox19.TabIndex = 64;
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(179, 182);
            this.textBox20.MaxLength = 25;
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(105, 21);
            this.textBox20.TabIndex = 47;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(469, 185);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(15, 14);
            this.checkBox5.TabIndex = 60;
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(301, 182);
            this.textBox21.MaxLength = 25;
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(61, 21);
            this.textBox21.TabIndex = 64;
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(379, 182);
            this.textBox22.MaxLength = 25;
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(61, 21);
            this.textBox22.TabIndex = 64;
            // 
            // textBox23
            // 
            this.textBox23.Location = new System.Drawing.Point(101, 182);
            this.textBox23.MaxLength = 25;
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(61, 21);
            this.textBox23.TabIndex = 64;
            // 
            // textBox24
            // 
            this.textBox24.Location = new System.Drawing.Point(185, 209);
            this.textBox24.MaxLength = 25;
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new System.Drawing.Size(105, 21);
            this.textBox24.TabIndex = 47;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(475, 212);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(15, 14);
            this.checkBox6.TabIndex = 60;
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // textBox25
            // 
            this.textBox25.Location = new System.Drawing.Point(307, 209);
            this.textBox25.MaxLength = 25;
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new System.Drawing.Size(61, 21);
            this.textBox25.TabIndex = 64;
            // 
            // textBox26
            // 
            this.textBox26.Location = new System.Drawing.Point(385, 209);
            this.textBox26.MaxLength = 25;
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new System.Drawing.Size(61, 21);
            this.textBox26.TabIndex = 64;
            // 
            // textBox27
            // 
            this.textBox27.Location = new System.Drawing.Point(107, 209);
            this.textBox27.MaxLength = 25;
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new System.Drawing.Size(61, 21);
            this.textBox27.TabIndex = 64;
            // 
            // PlaceAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(532, 306);
            this.ControlBox = false;
            this.Controls.Add(this.textBox27);
            this.Controls.Add(this.textBox23);
            this.Controls.Add(this.textBox19);
            this.Controls.Add(this.textBox15);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox26);
            this.Controls.Add(this.textBox22);
            this.Controls.Add(this.textBox18);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox25);
            this.Controls.Add(this.textBox21);
            this.Controls.Add(this.textBox17);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPid);
            this.Controls.Add(this.checkBox6);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cbEnd);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox24);
            this.Controls.Add(this.textBox20);
            this.Controls.Add(this.textBox16);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label6);
            this.Name = "PlaceAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "增加库房";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cbEnd;
        private System.Windows.Forms.Label lblPid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.TextBox textBox25;
        private System.Windows.Forms.TextBox textBox26;
        private System.Windows.Forms.TextBox textBox27;
    }
}