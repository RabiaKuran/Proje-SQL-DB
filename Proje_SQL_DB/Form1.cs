using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_SQL_DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        private void BtnKategori_Click(object sender, EventArgs e)
        {
            FormKategoriler fr = new FormKategoriler();
            fr.Show();

        }

        private void BtnMusteri_Click_1(object sender, EventArgs e)
        {
            FormMusteri fr2 = new FormMusteri();
            fr2.Show();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-ST8S73E\SQLEXPRESS;Initial Catalog=DbYeni;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            //Ürünlerin Durum Seviyesi
            SqlCommand komut = new SqlCommand("Execute STOKKONTROL", baglanti);
            SqlDataAdapter da= new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Grafiğe Veri Çekme İşlemi

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT KATEGORIAD, COUNT(*) FROM TBLKATEGORİ INNER JOIN TBLURUN ON TBLKATEGORİ.KATEGORIID = TBLURUN.KATEGORI GROUP BY KATEGORIAD", baglanti);
            SqlDataReader dr = komut2.ExecuteReader();
            while(dr.Read())
            {
                chart1.Series["Kategoriler"].Points.AddXY(dr[0], dr[1]);

            }
            baglanti.Close();

            //2. Grafiğe Veri Çekme İşlemi

            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("SELECT SEHIR, COUNT(*) FROM TBLMUSTERI GROUP BY SEHIR", baglanti);
            SqlDataReader sdr = komut3.ExecuteReader();
            while (sdr.Read())
            {
                chart2.Series["Şehirler"].Points.AddXY(sdr[0], sdr[1]);

            }
            baglanti.Close();
            



        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
    }
}
