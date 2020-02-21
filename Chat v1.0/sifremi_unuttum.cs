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
    public partial class sifremi_unuttum : Form
    {
        public sifremi_unuttum()
        {
            InitializeComponent();
        }

        db veri_baglanti = new db();
        bool suruklenmedurumu = false;
        Point ilkkonum;

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }//minimized tuşu

        private void label3_Click(object sender, EventArgs e)
        {
            veri_baglanti.anamenü_dön(this);
        }//kapatma tuşu

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

        private void sifremi_unuttum_Load(object sender, EventArgs e)
        {
            textBox1.Select();
        }//form load

        private void button1_Click(object sender, EventArgs e)
        {
            if (veri_baglanti.sifre_gonder(textBox1.Text,textBox2.Text) == false)
            {
                MessageBox.Show("Yanlış bilgi girdiniz!", "HATA", MessageBoxButtons.OK);
            }
        }//şifre gönder tuşu
    }
}
