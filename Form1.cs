using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MultipleMailSender
{
    public partial class Form1 : Form
    {
        Mail_Sender mailSender = new Mail_Sender();
        string gondericiMail, gondericiSifre, SmtpServerName, konu, mailIcerigi,YOLattachment,hataliMailler="";
        int port;
        List<string> aliciMailler = new List<string>();
        private void button2_Click(object sender, EventArgs e)
        {
            gondericiMail = textBoxMail.Text;
            gondericiSifre = textBoxSifre.Text;
            SmtpServerName = textBoxSmtpServerName.Text;
            port = Convert.ToInt32(textBoxPort.Text);
            konu = textBoxKonu.Text;
            mailIcerigi = textBoxMailIcerigi.Text;

            mailIcerigi = mailIcerigi.Replace("\n", "<br>");

            if (aliciMailler.Count > 0)
            {
                foreach (var item in aliciMailler)
                {
                    if (string.IsNullOrEmpty(YOLattachment))
                    {
                        if(mailSender.SendMail(item, konu, mailIcerigi, SmtpServerName, gondericiMail, gondericiSifre, port))
                            label8.Text = "Durum: " + item + " adresine mail gönderildi.";
                        else
                        {
                            label8.Text = "Durum: " + item + " adresine mail gönderilirken HATA.";
                            hataliMailler += item + " ";
                        }
                            
                    }
                        
                    else
                    {
                        if (mailSender.SendMailWithAttachment(item, konu, mailIcerigi, SmtpServerName, gondericiMail, gondericiSifre, port,YOLattachment))
                            label8.Text = "Durum: " + item + " adresine mail gönderildi.";
                        else
                        {
                            label8.Text = "Durum: " + item + " adresine mail gönderilirken HATA.";
                            hataliMailler += item + " ";
                        }
                            
                    }
                        
                    label8.Refresh();
                }
                label8.Text = "Durum: Mail gönderme işlemi tamamlanmıştır.\nLütfen gönderilen kutunuzu kontrol ediniz. Mevcut sayıya ulaşılmadıysa hala gönderiliyor demektir. Bu durumda programı kapatmayınız.";
                if (!string.IsNullOrEmpty(hataliMailler))
                {
                    MessageBox.Show("Şu adreslere mail gönderilirken hata oluştu:\n" + hataliMailler);
                }
            }
            else
                label8.Text = "Durum: Alıcı mail bulunamadı.";

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "PDF Dosyası |*.pdf";
            if (file.ShowDialog() == DialogResult.OK)
            {
                YOLattachment = file.FileName;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }
        string YOLtxt = "";
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "TXT Dosyası |*.txt";
            if (file.ShowDialog() == DialogResult.OK)
            {
                YOLtxt = file.FileName;
                string text = System.IO.File.ReadAllText(YOLtxt);
                foreach (var item in text.Split(' '))
                {
                    if(!aliciMailler.Contains(item))
                    aliciMailler.Add(item);

                }
                label8.Text = "Durum: " + aliciMailler.Count + " Adet alıcı mail bulunmuştur.";
            }
            
        }

      
    }
}
