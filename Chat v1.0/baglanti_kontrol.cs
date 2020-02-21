using System;
using System.Drawing;
using System.Windows.Forms;
namespace Chat_v1._0
{
    public partial class baglanti_kontrol : Form
    {
        public baglanti_kontrol()
        {
            InitializeComponent();
        }
        
        bool suruklenmedurumu = false;
        db veri_baglanti = new db();
        Point ilkkonum;

        private void baglanti_kontrol_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (veri_baglanti.internetSorgu() == true)
            {
                MessageBox.Show("İnternet bağlantısı var!", "Bildirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            else
            {
                MessageBox.Show("İnternet bağlantısı kurulamadı!\nTekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            veri_baglanti.kapatilsinmi(this);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = true;
            this.Cursor = Cursors.SizeAll;
            ilkkonum = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (suruklenmedurumu)
            {
                this.Left = e.X + this.Left - (ilkkonum.X);
                this.Top = e.Y + this.Top - (ilkkonum.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = false;
            this.Cursor = Cursors.Default;
        }
    }
}
