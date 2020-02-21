using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Chat_v1._0
{
    public partial class profil_bilgileri_ben : Form
    {
        public profil_bilgileri_ben()
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

        private void profil_bilgileri_ben_Load(object sender, EventArgs e)
        {
            pictureBox2.ImageLocation = veri_baglanti.ana_sayfa_resim(giris.kullanici_adi, giris.parola);//resim getirme
            label12.Text = veri_baglanti.ana_sayfa_k_adi(giris.kullanici_adi,giris.parola);//kullanıcı adı getiriyor
            label13.Text = veri_baglanti.ana_sayfa_parola(giris.kullanici_adi, giris.parola);//parola getiriyor
            label14.Text = veri_baglanti.ana_sayfa_email(giris.kullanici_adi, giris.parola);//email getiriyor
            label15.Text = veri_baglanti.ana_sayfa_hosgeldin(giris.kullanici_adi, giris.parola);//isim getirme
            label16.Text = veri_baglanti.ana_sayfa_soyad(giris.kullanici_adi, giris.parola);//soyad getirme
            label17.Text = veri_baglanti.ana_sayfa_yas(giris.kullanici_adi, giris.parola);//yaş getiriyor
            label18.Text = veri_baglanti.ana_sayfa_cinsiyet(giris.kullanici_adi, giris.parola);//cinsiyet getiriyor
            label19.Text = veri_baglanti.ana_sayfa_telefon(giris.kullanici_adi, giris.parola);//telefon no getiriyor
        }//form load

        private void label2_Click(object sender, EventArgs e)//minimized tuşu
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            veri_baglanti.gizle(this);
        }//gizleme tuşu

        private void button4_Click(object sender, EventArgs e)
        {
            parola_degis parola = new parola_degis();
            parola.Show();
        }//parola değiş

        private void button3_Click(object sender, EventArgs e)
        {
            email_degis e_mail = new email_degis();
            e_mail.Show();
        }//email değiş

        private void profil_bilgileri_ben_Activated(object sender, EventArgs e)
        {
            pictureBox2.ImageLocation = veri_baglanti.ana_sayfa_resim(giris.kullanici_adi, giris.parola);//resim getirme
            label12.Text = veri_baglanti.ana_sayfa_k_adi(giris.kullanici_adi, giris.parola);//kullanıcı adı getiriyor
            label13.Text = veri_baglanti.ana_sayfa_parola(giris.kullanici_adi, giris.parola);//parola getiriyor
            label14.Text = veri_baglanti.ana_sayfa_email(giris.kullanici_adi, giris.parola);//email getiriyor
            label15.Text = veri_baglanti.ana_sayfa_hosgeldin(giris.kullanici_adi, giris.parola);//isim getirme
            label16.Text = veri_baglanti.ana_sayfa_soyad(giris.kullanici_adi, giris.parola);//soyad getirme
            label17.Text = veri_baglanti.ana_sayfa_yas(giris.kullanici_adi, giris.parola);//yaş getiriyor
            label18.Text = veri_baglanti.ana_sayfa_cinsiyet(giris.kullanici_adi, giris.parola);//cinsiyet getiriyor
            label19.Text = veri_baglanti.ana_sayfa_telefon(giris.kullanici_adi, giris.parola);//telefon no getiriyor
        }//aktiflik

        private void button2_Click(object sender, EventArgs e)
        {
            yas_degis yas = new yas_degis();
            yas.Show();
        }//yaş değiş

        private void button1_Click(object sender, EventArgs e)
        {
            telefon_degis telefon_no = new telefon_degis();
            telefon_no.Show();
        }
    }
}
