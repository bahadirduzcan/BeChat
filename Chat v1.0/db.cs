using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data;

namespace Chat_v1._0
{
    class db
    {
        //AllowUserVariables=True;SslMode=Preferred;
        MySqlConnection baglanti = new MySqlConnection("Server=185.50.69.30;Database=bechat;Uid=root;Pwd='951210300';");
        //MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=bechat;Uid=root;Pwd='';");
        //Baglanti adında bir bağlantı oluşturdum
        [DllImport("wininet.dll")]
        public static extern bool InternetGetConnectedState(ref InternetConnectionState lpdwFlags, int dwReserved);

        public string GetExternalIP()
        {
            string externalIP;
            externalIP = (new System.Net.WebClient()).DownloadString("http://checkip.dyndns.org/");
            externalIP = (new System.Text.RegularExpressions.Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(externalIP)[0].ToString();
            return externalIP;
        }//ip adresi gösterme kayıt yaptırmada kullanıyorum

        public void kapatilsinmi(Form Frm)
        {
            DialogResult cevap = MessageBox.Show("Çıkmak istiyor musunuz?", "Çıkış", MessageBoxButtons.YesNo);
            if (cevap == DialogResult.Yes)
            {
                Application.Exit();
            }
        }//çıkış yapma mesajı

        public void gizle(Form Frm)
        {
            Frm.Hide();
        }// gizle mesajı

        public void anamenü_dön(Form Frm)
        {
            DialogResult cevap = MessageBox.Show("Ana Menüye geçmek istiyor musunuz?", "Bildirim", MessageBoxButtons.YesNoCancel);
            if (cevap == DialogResult.Yes)
            {
                giris grs = new giris();
                grs.Show();
                Frm.Hide();
            }
            else if (cevap == DialogResult.No)
            {
                Application.Exit();
            }
            else
            {
                
            }
        }//ana menü dön mesajı

        public bool baglanti_kontrol()
        {
            try
            {
                baglanti.Open();
                return true;
                //Veritabanına bağlanırsa baglanti_kontrol fonksiyonu "true" değeri gönderecek
            }
            catch (Exception)
            {
                return false;
                //Veritabanına bağlanamazsa "false" değeri dönecek
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//mysql baglantı kontrol

        public bool veriekle(string kullanici_adi, string parola, string e_mail, string ad, string soyad, string profil_resmi, string yas, string cinsiyet, string telefon, string uyelik_tarihi, string ip_adresi)
        {
            try
            {
                baglanti.Open();
                string komut = "insert into kullanici_bilgileri (kullanici_adi,parola,e_mail,ad,soyad,profil_resmi,yas,cinsiyet,telefon,uyelik_tarihi,ip_adresi) values('" + kullanici_adi + "','" + parola + "','" + e_mail + "', '" + ad + "', '" + soyad + "', '" + profil_resmi + "','" + yas + "','" + cinsiyet + "','" + telefon + "','" + uyelik_tarihi + "','" + ip_adresi + "')";
                MySqlCommand kmt = new MySqlCommand(komut, baglanti);
                kmt.ExecuteNonQuery();
                return true;
                //Veritabanına veriler eklenirse "true" değeri gönderecek
            }
            catch (Exception)
            {
                return false;
                //Veriler eklenmezse "false" değeri dönecek
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//üye ol kısmından veriler alıp kayıt ettiriyor

        public bool kullanici_kontrol(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//giriş yapması için kullanıcıları kontrol ediyor

        public enum InternetConnectionState : int
        {
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }//internet kontrolü

        public bool internetSorgu()
        {
            InternetConnectionState flags = 0;
            bool isConfigured = (flags & InternetConnectionState.INTERNET_CONNECTION_CONFIGURED) != 0;
            bool isConnected = InternetGetConnectedState(ref flags, 0);

            if (isConnected != true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }//internet kontrolü

        public bool uploadFile(string filePath)
        {
            try
            {
                //Create FTP request
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(@"ftp://lisansliyim.xyz/KullaniciResimleri" + "/" + Path.GetFileName(filePath));

                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("bahax41", "@Bhdr951210");
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                //Load the file
                FileStream stream = File.OpenRead(filePath);
                byte[] buffer = new byte[stream.Length];

                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                //Upload file
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }//dosya upload yapmak için

        public bool sifre_gonder(string kullanici_adi, string e_mail)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and e_mail='" + e_mail + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == true)
                {
                    MailMessage mesajım = new MailMessage();
                    SmtpClient istemci = new SmtpClient();
                    istemci.Credentials = new System.Net.NetworkCredential("bechat41@gmail.com", "951210300");
                    istemci.Port = 587;
                    istemci.Host = "smtp.gmail.com";
                    istemci.EnableSsl = true;
                    mesajım.To.Add(e_mail);
                    mesajım.From = new MailAddress("bechat41@gmail.com");
                    mesajım.Subject = "Şifremi Unuttum";
                    mesajım.Body = "Şifreniz: " + dr[1].ToString();
                    istemci.Send(mesajım);
                    MessageBox.Show("Mesaj başarıyla gönderilmiştir.\nGelen kutunuzu kontrol ediniz.", "Başarılı", MessageBoxButtons.OK);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Mesaj gönderilememiştir.", "Başarısız", MessageBoxButtons.OK);
                return false;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//unutulan şifreyi mail olarak gönderiyor

        public bool mail_gonder(string sifre, string mail)
        {
            try
            {
                MailMessage mesajım = new MailMessage();
                SmtpClient istemci = new SmtpClient();
                istemci.Credentials = new System.Net.NetworkCredential("kayit@bahario.com", "@Bhdr951210");
                istemci.Port = 587;
                istemci.Host = "mail.yoncu.com";
                istemci.EnableSsl = true;
                mesajım.To.Add(mail);
                mesajım.From = new MailAddress("kayit@bahario.com");
                mesajım.Subject = "Üye Kayıt İşlemi";
                mesajım.Body = "BeChat Uygulamasına Hoşgeldiniz" + "\n" + "\n" + "Kayıt olduğun için teşekkür ederiz." + "\n" + "\n" + "Girdiğiniz e-posta adresinin geçerliliğini doğrulamak için kayıt işlemini onaylamanız gerekiyor." + "\n" + "\n" + "Bu sizi istenmeyen reklam ve benzeri olaylara karşı korumak içindir. Hesabınızı etkinleştirmek için bu onay kodunu giriniz." + "\n" + "\n" + "Onay Kodu: " + sifre + "\n" + "\n" + "Bu kodu uygulamada uygun olan yere yapıştırınız hesabınız doğrulanacaktır.";
                istemci.Send(mesajım);
                MessageBox.Show("Mesaj başarıyla gönderilmiştir.\nGelen kutunuzu kontrol ediniz.", "Başarılı", MessageBoxButtons.OK);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Mesaj gönderilememiştir.", "Başarısız", MessageBoxButtons.OK);
                return false;
            }
        }//üretilen şifreyi mail olarak gönderiyor
        
        public bool online(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return false;
                }
                else
                {
                    string komut = "insert into online (ad,kullanici_adi,parola) values('" + dr[3].ToString() + "','" + kullanici_adi + "','" + parola + "')";
                    baglanti.Close();
                    MySqlCommand kmt2 = new MySqlCommand(komut, baglanti);
                    baglanti.Open();
                    kmt2.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//giriş yapan kişiyi online listesine atıyor

        public bool ana_sayfa_offline(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return false;
                }
                else
                {
                    string komut = "delete from online Where kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                    baglanti.Close();
                    MySqlCommand kmt2 = new MySqlCommand(komut, baglanti);
                    baglanti.Open();
                    kmt2.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//çıkış yapan kişiyi online listesinden siliyor

        public string ana_sayfa_hosgeldin(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[3].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//giriş yapan kişinin ismini getiriyor

        public string ana_sayfa_resim(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[5].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//resim yolunu getiriyor

        public string ana_sayfa_k_adi(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[0].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//kullanici adını getiriyor

        public string ana_sayfa_parola(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[1].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//parola getiriyor

        public string ana_sayfa_email(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[2].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//email getiriyor

        public string ana_sayfa_soyad(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[4].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//soyad getiriyor

        public string ana_sayfa_yas(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[6].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//yaş getiriyor

        public string ana_sayfa_diger_resim(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[5].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//karşı kişinin resmini getiriyor

        public string ana_sayfa_cinsiyet(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[7].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//cinsiyet getiriyor

        public string ana_sayfa_telefon(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[8].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//tel no getiriyor

        public DataTable online_kisileri_goster()
        {
            try
            {
                baglanti.Open();
                string sorgu = "SELECT ad FROM online";
                MySqlDataAdapter da = new MySqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//online kişileri gösteriyor

        public DataTable mesajlar()
        {
            try
            {
                baglanti.Open();
                string sorgu = "SELECT * FROM ana_sayfa";
                MySqlDataAdapter da = new MySqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//ana sayfa mesajları getiriyor

        public bool mesaj_gonder(string ad, string mesaj, string saat)
        {
            try
            {
                baglanti.Open();
                string komut = "insert into ana_sayfa (isim,mesajlar,saat) values('" + ad + "','" + mesaj + "', '" + saat + "')";
                MySqlCommand kmt = new MySqlCommand(komut, baglanti);
                kmt.ExecuteNonQuery();
                return true;
                //Veritabanına veriler eklenirse "true" değeri gönderecek
            }
            catch (Exception)
            {
                return false;
                //Veriler eklenmezse "false" değeri dönecek
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//ana sayfa mesaj gonderme

        public bool sifre_degistir(string kullanici_adi, string eski_parola, string yeni_parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + eski_parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return false;
                }
                else
                {
                    baglanti.Close();
                    baglanti.Open();
                    string komut = "update kullanici_bilgileri set parola='" + yeni_parola + "' Where kullanici_adi='" + kullanici_adi + "'";
                    MySqlCommand kmt1 = new MySqlCommand(komut, baglanti);
                    kmt1.ExecuteNonQuery();
                    MessageBox.Show("Şifreniz başarılı bir şekilde değiştirilmiştir.", "Bildirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//parola değiştirme

        public bool e_mail_degistir(string kullanici_adi, string parola, string e_mail)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return false;
                }
                else
                {
                    baglanti.Close();
                    baglanti.Open();
                    string komut = "update kullanici_bilgileri set e_mail='" + e_mail + "' Where kullanici_adi='" + kullanici_adi + "'";
                    MySqlCommand kmt1 = new MySqlCommand(komut, baglanti);
                    kmt1.ExecuteNonQuery();
                    MessageBox.Show("E-mail başarılı bir şekilde değiştirilmiştir.", "Bildirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//E-mail değiştirme

        public bool yas_degistir(string kullanici_adi, string parola, string yas)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return false;
                }
                else
                {
                    baglanti.Close();
                    baglanti.Open();
                    string komut = "update kullanici_bilgileri set yas='" + yas + "' Where kullanici_adi='" + kullanici_adi + "'";
                    MySqlCommand kmt1 = new MySqlCommand(komut, baglanti);
                    kmt1.ExecuteNonQuery();
                    MessageBox.Show("Yaş başarılı bir şekilde değiştirilmiştir.", "Bildirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//yaş değiştirme

        public bool telefon_no_degistir(string kullanici_adi, string parola, string telefon)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return false;
                }
                else
                {
                    baglanti.Close();
                    baglanti.Open();
                    string komut = "update kullanici_bilgileri set telefon='" + telefon + "' Where kullanici_adi='" + kullanici_adi + "'";
                    MySqlCommand kmt1 = new MySqlCommand(komut, baglanti);
                    kmt1.ExecuteNonQuery();
                    MessageBox.Show("Telefon numaranız başarılı bir şekilde değiştirilmiştir.", "Bildirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//telefon no değiştirme

        public string online_kisi_kontrol_k_adi(string adi)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM online WHERE ad='" + adi + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[1].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//online daki kişilerin profil bilgilerine giriş yapması için

        public string online_kisi_kontrol_parola(string adi)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM online WHERE ad='" + adi + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[2].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//online daki kişilerin profil bilgilerine giriş yapması için

        public bool admin_kontrol(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM admin WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//adminliği kontrol ediyor

        public bool ana_sayfa_mesajlari_sil_admin()
        {
            try
            {
                string sorgu = "Delete FROM ana_sayfa";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                kmt.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//ana sayfa mesajlari sil admin komutu

        public DataTable kisileri_getir_admin()
        {
            try
            {
                baglanti.Open();
                string sorgu = "SELECT kullanici_adi,parola,ad,soyad,e_mail,telefon,ban FROM kullanici_bilgileri";
                MySqlDataAdapter da = new MySqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//kişi bilgileri getiriyor admin komutu

        public string ban_kontrol(string kullanici_adi, string parola)
        {
            try
            {
                MySqlDataReader dr;
                string sorgu = "SELECT * FROM kullanici_bilgileri WHERE kullanici_adi='" + kullanici_adi + "' and parola='" + parola + "'";
                MySqlCommand kmt = new MySqlCommand(sorgu, baglanti);
                baglanti.Open();
                dr = kmt.ExecuteReader();
                bool sorgula = Convert.ToBoolean(dr.Read());
                if (sorgula == false)
                {
                    return null;
                }
                else
                {
                    return dr[11].ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                }
            }
        }//yaş getiriyor
    }
}