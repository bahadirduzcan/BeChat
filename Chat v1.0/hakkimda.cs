using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chat_v1._0
{
    public partial class hakkimda : Form
    {
        public hakkimda()
        {
            InitializeComponent();
        }

        bool suruklenmedurumu = false;
        Point ilkkonum;
        db veri_baglanti = new db();

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = true;
            this.Cursor = Cursors.SizeAll;
            ilkkonum = e.Location;
        }//kaydırma ayarı

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (suruklenmedurumu)
            {
                this.Left = e.X + this.Left - (ilkkonum.X);
                this.Top = e.Y + this.Top - (ilkkonum.Y);
            }
        }//kaydırma ayarı

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = false;
            this.Cursor = Cursors.Default;
        }//kaydırma ayarı

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }//minimized tuşu

        private void label3_Click(object sender, EventArgs e)
        {
            veri_baglanti.gizle(this);
        }//kapatma tuşu

        private void hakkimda_Load(object sender, EventArgs e)
        {

        }//form load
    }
}
