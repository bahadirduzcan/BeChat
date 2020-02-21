using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Chat_v1._0
{
    public partial class splash : Form
    {
        public splash()
        {
            InitializeComponent();
        }

        public static RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\BeChat\KullaniciBilgileri");

        private void timer1_Tick(object sender, EventArgs e)
        {
            giris formgiris = new giris();
            this.Hide();
            formgiris.Show();
            timer1.Enabled = false;
        }

        private void splash_Load(object sender, EventArgs e)
        {
            if (key != null)
            {
                key.SetValue("KullaniciAdi", "");
                key.SetValue("Parola", "");
                key.SetValue("kutucuk", "0");
            }
        }
    }
}
