using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private string[] fotoYolları;
        private int aktifFotoIndex = 0;
        private Timer slaytTimer;

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Sol panele panel ekleyip arka planını zoom olarak ayarlama
            panel1.BackgroundImageLayout = ImageLayout.Zoom;

            // Sağ panele ListBox ve butonları ekleme
            button1.Click += DosyaEkleButtonClick;
            button2.Click += BaslatButtonClick;
            button3.Click += DurdurButtonClick;

            // Timer oluşturma
            slaytTimer = new Timer();
            slaytTimer.Interval = 3000; // 3 saniye
            slaytTimer.Tick += SlaytTimer_Tick;
        }

        private void DosyaEkleButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Tüm Dosyalar|*.*";
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fotoYolları = openFileDialog1.FileNames;
                listBox1.Items.AddRange(fotoYolları);
            }
        }

        private void BaslatButtonClick(object sender, EventArgs e)
        {
            if (fotoYolları != null && fotoYolları.Length > 0)
            {
                // İlk fotoğrafı kullanarak slayt gösterisini başlatma
                panel1.BackgroundImage = Image.FromFile(fotoYolları[aktifFotoIndex]);
                slaytTimer.Start();
            }
            else
            {
                MessageBox.Show("Lütfen önce bir fotoğraf ekleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DurdurButtonClick(object sender, EventArgs e)
        {
            slaytTimer.Stop();
        }

        private void SlaytTimer_Tick(object sender, EventArgs e)
        {
            // Slayt gösterisi sıradaki fotoğrafa geçiyor
            aktifFotoIndex = (aktifFotoIndex + 1) % fotoYolları.Length;
            panel1.BackgroundImage = Image.FromFile(fotoYolları[aktifFotoIndex]);
        }
    }
}
