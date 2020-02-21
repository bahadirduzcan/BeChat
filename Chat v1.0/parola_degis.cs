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
    public partial class parola_degis : Form
    {
        public parola_degis()
        {
            InitializeComponent();
        }

        db veri_baglanti = new db();
        int a = 1;

        private void parola_degis_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            veri_baglanti.gizle(this);
        }//iptal

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (a == 1)
            {
                textBox2.PasswordChar = '*';
                textBox3.PasswordChar = '*';
                a = 0;
            }
            else
            {
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
                a = 1;
            }
        }//göster gizle

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox1.Text)
            {
                pictureBox5.Image = Properties.Resources.check;
            }
            else
            {
                pictureBox5.Image = Properties.Resources.uncheck;
            }
        }//tekrarı ile parola aynı ise

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                veri_baglanti.ana_sayfa_offline(giris.kullanici_adi, giris.parola);
                veri_baglanti.sifre_degistir(giris.kullanici_adi, textBox3.Text, textBox1.Text);
                Application.Restart();                
            }
            catch (Exception)
            {
                MessageBox.Show("Eksik bilgi girdiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }
    }
}
