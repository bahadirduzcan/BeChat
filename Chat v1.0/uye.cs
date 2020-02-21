using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Chat_v1._0
{
    public partial class uye : Form
    {
        public uye()
        {
            InitializeComponent();
        }
        // Tanımlama Kısmı//////////////////////////////
        db veri_baglanti = new db();
        int a = 1;
        bool suruklenmedurumu = false;
        Point ilkkonum;
        OpenFileDialog file = new OpenFileDialog();
        Random rastgele = new Random();
        string DosyaYolu;
        string uret, uyelik_tarihi1;
        string harfler = "ABCDEFGHIJKLMNOPRSTUVYZ0123456789";
        Random rnd = new Random();
        string ip_adresim;
        ///////////////////////////////////////////////


        private void Form2_Load(object sender, EventArgs e)
        {
            if (veri_baglanti.baglanti_kontrol() == false)
            {
                MessageBox.Show("Mysql Bağlantı Kurulamadı", "Hata", MessageBoxButtons.OK);
                Application.Exit();
            }

            ip_adresim = veri_baglanti.GetExternalIP();
        }//form load 

        private void label3_Click(object sender, EventArgs e)
        {
            veri_baglanti.anamenü_dön(this);
        }//kapatma tuşu

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }//minimized tuşu

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources._5;
        }//şekil ayarı

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources._6;
        }//şekil ayarı

        private void button2_Click(object sender, EventArgs e)
        {
            uret = "";
            for (int i = 0; i < 20; i++)
            {
                uret += harfler[rastgele.Next(harfler.Length)];
            }

            veri_baglanti.mail_gonder(uret, textBox4.Text);
        }//şifre al tuşu

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources._3;
        }//şekil ayarı

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources._4;
        }//şekil ayarı

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
        }//şifre göster gizle tuşu

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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                file.Reset();
                file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                file.Filter = ".png |*.png| .jpg |*.jpg";
                file.FilterIndex = 1;               
                file.ShowDialog();                
                FileInfo dosyabilgisi = new FileInfo(file.FileName);
                if (dosyabilgisi.Length >= 4000000)
                {
                    DosyaYolu = "";
                    MessageBox.Show("Dosya Boyutu Çok Büyük!","", MessageBoxButtons.OK);                    
                }
                else
                {
                    DosyaYolu = file.FileName;
                    pictureBox2.Image = Image.FromFile(DosyaYolu);
                    if (textBox1.Text == null)
                    {
                        MessageBox.Show("Kullanıcı adı giriniz!","HATA",MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (File.Exists(@"KullaniciResmi\" + textBox1.Text + ".png"))
                        {
                            File.Delete(@"KullaniciResmi\" + textBox1.Text + ".png");
                            File.Copy(file.FileName, @"KullaniciResmi\" + textBox1.Text + ".png");
                        }
                        else
                        {
                            File.Copy(file.FileName, @"KullaniciResmi\" + textBox1.Text + ".png");
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                pictureBox2.Image = Properties.Resources.new_uye;
                MessageBox.Show(ex.ToString() + " ------------------ "+ "Resim seçmediniz!","HATA", MessageBoxButtons.OK);
            }
        }//resim seçme tuşu

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == textBox2.Text)
            {
                pictureBox5.Image = Properties.Resources.check;
            }
            else
            {
                pictureBox5.Image = Properties.Resources.uncheck;
            }
        }//şekil ayarı

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (maskedTextBox1.TextLength > 0)
            {
                pictureBox12.Image = Properties.Resources.check;
            }
            else
            {
                pictureBox12.Image = Properties.Resources.uncheck;
            }
        }//şekil ayarı

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.TextLength > 0)
            {
                pictureBox9.Image = Properties.Resources.check;
            }
            else
            {
                pictureBox9.Image = Properties.Resources.uncheck;
            }
        }//şekil ayarı

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.TextLength > 0)
            {
                pictureBox8.Image = Properties.Resources.check;
            }
            else
            {
                pictureBox8.Image = Properties.Resources.uncheck;
            }
        }//şekil ayarı

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.TextLength > 0)
            {
                pictureBox7.Image = Properties.Resources.check;
            }
            else
            {
                pictureBox7.Image = Properties.Resources.uncheck;
            }
        }//şekil ayarı

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength >0)
            {
                pictureBox6.Image = Properties.Resources.check;
            }
            else
            {
                pictureBox6.Image = Properties.Resources.uncheck;
            }
        }//şekil ayarı

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (uret == null)
            {

            }
            else
            {
                if (textBox9.Text == uret.ToString())
                {
                    pictureBox4.Image = Properties.Resources.check;
                }
                else
                {
                    pictureBox4.Image = Properties.Resources.uncheck;
                }
            }
        }//şekil ayarı

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }//yanlışlık tuşladım :D 

        private void button1_Click(object sender, EventArgs e)
        {
            if (uret == null)
            {
                MessageBox.Show("Üye Olamazsınız!","HATA",MessageBoxButtons.OK);
            }
            else
            {
                if (textBox9.Text == uret.ToString())
                {
                    try
                    {
                        Application.DoEvents();
                        if (veri_baglanti.uploadFile(@"KullaniciResmi\" + textBox1.Text + ".png") == true)
                        {
                            DosyaYolu = "http://lisansliyim.xyz/KullaniciResimleri/" + textBox1.Text + ".png";
                            uyelik_tarihi1 = DateTime.Now.ToShortDateString();
                            if (veri_baglanti.veriekle(textBox1.Text, textBox2.Text, textBox4.Text, textBox5.Text, textBox6.Text, DosyaYolu, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), maskedTextBox1.Text, uyelik_tarihi1, ip_adresim) == true)
                            {
                                MessageBox.Show("Başarıyla Kayıt Olunmuştur.", "Başarılı", MessageBoxButtons.OK);
                                giris frm1 = new giris();
                                this.Hide();
                                frm1.Show();
                            }
                            else
                            {
                                MessageBox.Show("Hatalı İşlem Yaptınız!\nBilgilerinizi kontrol ediniz.", "HATA", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hatalı İşlem Yaptınız!\nBilgilerinizi kontrol ediniz.", "HATA", MessageBoxButtons.OK);
                        }                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Hatalı İşlem Yaptınız!\nBilgilerinizi kontrol ediniz.", "HATA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Şifrenizi kontrol ediniz!", "HATA", MessageBoxButtons.OK);
                }
            }
        }//üye ol tuşu
    }
}