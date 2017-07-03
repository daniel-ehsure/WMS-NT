using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace UI
{
    public partial class QuerryStocksPlaceForm : Form
    {
        Image im;       

        public QuerryStocksPlaceForm(Image im)
        {
            InitializeComponent();
            this.im = im;
        }

        private void QuerryStocksPlaceForm_Load(object sender, EventArgs e)
        {
            this.label12.BackColor = Color.FromArgb(0, 134, 139);
            this.pictureBox1.Image = im;
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


       


       
    }
}
