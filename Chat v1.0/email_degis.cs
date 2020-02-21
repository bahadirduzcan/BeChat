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
    public partial class email_degis : Form
    {
        public email_degis()
        {
            InitializeComponent();
        }

        db veri_baglanti = new db();

        private void button2_Click(object sender, EventArgs e)
        {
            veri_baglanti.gizle(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                veri_baglanti.e_mail_degistir(giris.kullanici_adi, giris.parola, textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Eksik bilgi girdiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
