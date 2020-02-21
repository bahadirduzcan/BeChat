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
    public partial class telefon_degis : Form
    {
        public telefon_degis()
        {
            InitializeComponent();
        }

        db veri_baglanti = new db();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                veri_baglanti.telefon_no_degistir(giris.kullanici_adi,giris.parola,maskedTextBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Eksik bilgi girdiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            veri_baglanti.gizle(this);
        }
    }
}
