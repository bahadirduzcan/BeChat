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
    public partial class ana_sayfa : Form
    {
        public ana_sayfa()
        {
            InitializeComponent();
        }
        
        bool suruklenmedurumu = false;
        Point ilkkonum;
        db veri_baglanti = new db();
        string saat;
        int a = 3;
        int b = 1;

        public static string diger_kisi_kullanici_adi;
        public static string diger_kisi_parola;

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
        }//minimized yapma

        private void label3_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Ana Menüye geçmek istiyor musunuz?", "Bildirim", MessageBoxButtons.YesNoCancel);
            if (cevap == DialogResult.Yes)
            {
                veri_baglanti.ana_sayfa_offline(giris.kullanici_adi, giris.parola);
                giris grs = new giris();
                this.Hide();
                grs.Show();
            }
            else if (cevap == DialogResult.No)
            {
                veri_baglanti.ana_sayfa_offline(giris.kullanici_adi, giris.parola);
                Application.Exit();
            }
            else
            {

            }
        }//çıkış yap

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Ana Menüye geçmek istiyor musunuz?", "Bildirim", MessageBoxButtons.YesNoCancel);
            if (cevap == DialogResult.Yes)
            {
                veri_baglanti.ana_sayfa_offline(giris.kullanici_adi, giris.parola);
                giris grs = new giris();
                this.Hide();
                grs.Show();
            }
            else if (cevap == DialogResult.No)
            {
                veri_baglanti.ana_sayfa_offline(giris.kullanici_adi, giris.parola);
                Application.Exit();
            }
            else
            {
                
            }           
        }// çıkış yap menüsü

        private void ana_sayfa_Load(object sender, EventArgs e)
        {
            label6.Text = veri_baglanti.ana_sayfa_hosgeldin(giris.kullanici_adi, giris.parola);//isim getirme
            pictureBox2.ImageLocation = veri_baglanti.ana_sayfa_resim(giris.kullanici_adi, giris.parola);//resim yolu
            dataGridView1.DataSource = veri_baglanti.online_kisileri_goster();//online kisileri gösterme
            dataGridView1.ClearSelection();//seçililiği kaldırma

            dataGridView2.DataSource = veri_baglanti.mesajlar();//mesajları getirme
            dataGridView2.Columns["isim"].Width = 126;
            dataGridView1.Columns["ad"].Width = 140;
            dataGridView2.Columns["mesajlar"].Width = 415;
            dataGridView2.ClearSelection();
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            if (dataGridView2.Rows.Count > 0)
            {
                dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.RowCount - 1;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (label6.Text == dataGridView1.Rows[i].Cells[0].Value.ToString())
                {
                    dataGridView1.Rows[i].Selected = true;
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                }
            }
        }//form load kısmı

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }//yanlışlıkla tıklayıp duruyorum :D 

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ClearSelection();
        }//seçililiği kaldırma

        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ClearSelection();
        }//seçililiği kaldırma

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                timer1.Enabled = true;
                label10.Text = "3";
                button1.Enabled = false;
                saat = DateTime.Now.ToShortTimeString();
                veri_baglanti.mesaj_gonder(label6.Text, textBox1.Text, saat);
                dataGridView2.DataSource = veri_baglanti.mesajlar();
                dataGridView2.ClearSelection();
                dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.RowCount - 1;
            }
            else
            {
                MessageBox.Show("Boş mesaj gönderemezsiniz!","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            textBox1.Clear();
        }//mesaj yollama

        private void timer1_Tick(object sender, EventArgs e)
        {
            a--;
            label10.Text = a.ToString();
            if (a == 0)
            {
                a = 3;
                timer1.Enabled = false;
                button1.Enabled = true;
                label10.Text = "-";
            }
        }//flood engelleme kısa yoldan :D 

        private void label11_Click(object sender, EventArgs e)
        {
            if (veri_baglanti.admin_kontrol(giris.kullanici_adi,giris.parola))
            {
                Admin_menu admin = new Admin_menu();
                admin.Show();
            }
            else
            {
                hakkimda hakkka = new hakkimda();
                hakkka.Show();
            }
        }//hakkımda menüsü

        private void timer2_Tick(object sender, EventArgs e)
        {
            dataGridView1.DataSource = veri_baglanti.online_kisileri_goster();
            dataGridView1.ClearSelection();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (label6.Text == dataGridView1.Rows[i].Cells[0].Value.ToString())
                {
                    dataGridView1.Rows[i].Selected = true;
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                }
            }
            dataGridView2.ClearSelection();
        }//online kişileri takip ediyor

        private void timer3_Tick(object sender, EventArgs e)
        {
            dataGridView2.DataSource = veri_baglanti.mesajlar();
            dataGridView2.ClearSelection();
            if (dataGridView2.Rows.Count > 0)
            {
                dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.RowCount - 1;
            }
        }//mesajları takip ediyor

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label13.Text = (Convert.ToInt32(textBox1.MaxLength - textBox1.Text.Length)).ToString();
        }//karakter sınırlaması :D

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            profil_bilgileri_ben profil_me = new profil_bilgileri_ben();
            profil_me.Show();
        }//profil bilgilerimi açıyor

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            diger_kisi_kullanici_adi = veri_baglanti.online_kisi_kontrol_k_adi(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            diger_kisi_parola = veri_baglanti.online_kisi_kontrol_parola(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            profil_bilgileri proff = new profil_bilgileri();
            proff.Show();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            if (b == 1)
            {
                this.WindowState = FormWindowState.Maximized;
                dataGridView2.Columns["isim"].Width = 126;
                dataGridView2.Columns["mesajlar"].Width = 1290;
                b = 0;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                dataGridView2.Columns["isim"].Width = 126;
                dataGridView2.Columns["mesajlar"].Width = 415;
                //this.Height = 568;
                //this.Width = 1046;
                b = 1;
            }
        }//formu büyüt küçült
    }
}
