using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;

namespace Chat_v1._0
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }
        bool suruklenmedurumu = false;
        Point ilkkonum;
        db veri_baglanti = new db();
        
        public static string kullanici_adi, parola;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (veri_baglanti.internetSorgu() == false)
            {
                baglanti_kontrol kntrl = new baglanti_kontrol();
                this.Visible = false;
                kntrl.ShowDialog();
                this.Visible = true;
                MessageBox.Show("İnternet bağlantısı kurulamadı!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (veri_baglanti.baglanti_kontrol() == false)
            {
                MessageBox.Show("Mysql Bağlantı Kurulamadı", "Hata", MessageBoxButtons.OK);
                Application.Exit();
            }

            if (splash.key.GetValue("kutucuk").ToString() != null)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

            if (splash.key.GetValue("KullaniciAdi").ToString() != null)
            {
                textBox1.Text = splash.key.GetValue("KullaniciAdi").ToString();
            }
            else
            {
                textBox1.Text = "";
            }

            if (splash.key.GetValue("Parola").ToString() != null)
            {
                textBox2.Text = splash.key.GetValue("Parola").ToString();
            }
            else
            {
                textBox2.Text = "";
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            veri_baglanti.kapatilsinmi(this);
        }//kapatma tuşu

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }//minimized tuşu

        private void button2_Click(object sender, EventArgs e)
        {
            if (veri_baglanti.kullanici_kontrol(textBox1.Text, textBox2.Text) == false)
            {
                DialogResult cevap = MessageBox.Show("Kayıtlı değilsiniz!\nHemen kayıt olmak için evet tuşlayınız.", "Bildirim", MessageBoxButtons.YesNo);
                if (cevap == DialogResult.Yes)
                {
                    uye frm2 = new uye();
                    this.Hide();
                    frm2.Show();
                }
            }
            else
            {
                if (veri_baglanti.ban_kontrol(textBox1.Text,textBox2.Text) == "0")
                {
                    kullanici_adi = textBox1.Text;
                    parola = textBox2.Text;
                    veri_baglanti.online(textBox1.Text, textBox2.Text);
                    ana_sayfa home = new ana_sayfa();
                    this.Hide();
                    home.Show();
                }
                else
                {
                    MessageBox.Show("Ban yediniz. 1 saat sonra banınız kalkıcaktır.","BAN",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }                
            }
        }//giriş yap tuşu

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources._1;
        }//şekil ayarı

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources._21;
        }//şekil ayarı

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources._3;
        }//şekil ayarı

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources._4;
        }//şekil ayarı

        private void button1_Click(object sender, EventArgs e)
        {
            uye frm2 = new uye();
            this.Hide();
            frm2.Show();
        }//üye ol kısmı

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            beni_hatirla();
        }//beni hatırla ayarı

        private void beni_hatirla()
        {
            try
            {
                if (checkBox1.Checked == true)
                {
                    if (textBox1.Text == "" & textBox2.Text == "")
                    {

                    }
                    else
                    {
                        splash.key.SetValue("KullaniciAdi", textBox1.Text);
                        splash.key.SetValue("Parola", textBox2.Text);
                        splash.key.SetValue("kutucuk", "1");
                    }
                }
                else
                {
                    splash.key.SetValue("KullaniciAdi", "");
                    splash.key.SetValue("Parola", "");
                    splash.key.SetValue("kutucuk", "0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "HATA", MessageBoxButtons.OK);
            }
        }//beni hatırla :D

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sifremi_unuttum sifre = new sifremi_unuttum();
            this.Hide();
            sifre.Show();
        }//şifre unuttum ayarı
    }
}