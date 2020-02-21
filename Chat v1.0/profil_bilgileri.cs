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
    public partial class profil_bilgileri : Form
    {
        public profil_bilgileri()
        {
            InitializeComponent();
        }

        bool suruklenmedurumu = false;
        Point ilkkonum;
        db veri_baglanti = new db();

        private void profil_bilgileri_Load(object sender, EventArgs e)
        {
            label15.Text = veri_baglanti.ana_sayfa_hosgeldin(ana_sayfa.diger_kisi_kullanici_adi,ana_sayfa.diger_kisi_parola);//ad
            label16.Text = veri_baglanti.ana_sayfa_soyad(ana_sayfa.diger_kisi_kullanici_adi, ana_sayfa.diger_kisi_parola);//soyad
            label14.Text = veri_baglanti.ana_sayfa_email(ana_sayfa.diger_kisi_kullanici_adi, ana_sayfa.diger_kisi_parola);//email
            label17.Text = veri_baglanti.ana_sayfa_yas(ana_sayfa.diger_kisi_kullanici_adi, ana_sayfa.diger_kisi_parola);//yaş
            label18.Text = veri_baglanti.ana_sayfa_cinsiyet(ana_sayfa.diger_kisi_kullanici_adi, ana_sayfa.diger_kisi_parola);//cinsiyet
            label19.Text = veri_baglanti.ana_sayfa_telefon(ana_sayfa.diger_kisi_kullanici_adi, ana_sayfa.diger_kisi_parola);//telefon
            pictureBox2.ImageLocation = veri_baglanti.ana_sayfa_diger_resim(ana_sayfa.diger_kisi_kullanici_adi, ana_sayfa.diger_kisi_parola);//resim
        }

        private void label3_Click(object sender, EventArgs e)
        {
            veri_baglanti.gizle(this);
        }//gizleme tuşu

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
    }
}
