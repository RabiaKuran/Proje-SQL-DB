using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_SQL_DB
{
    public partial class FormKategoriler : Form
    {
        public FormKategoriler()
        {
            InitializeComponent();
        }

        private void FormUrunler_Load(object sender, EventArgs e)
        {

        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-ST8S73E\SQLEXPRESS;Initial Catalog=DbYeni;Integrated Security=True");
        private void BtnListele_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLKATEGORİ", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);  
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("INSERT INTO TBLKATEGORİ (AD) VALUES (@p1)",baglanti);
            komut2.Parameters.AddWithValue("@p1", TxtKategoriAd.Text);
            komut2.ExecuteNonQuery();   
            baglanti.Close();
            MessageBox.Show("Kategori kaydetme işlemi başarı ile kaydedildi");

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtKategoriId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtKategoriAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("DELETE FROM TBLKATEGORİ WHERE KATEGORIID=@p1", baglanti);
            komut3.Parameters.AddWithValue("@p1", TxtKategoriId.Text);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori silme işlemi gerçekleştirildi.");
        
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("UPDATE TBLKATEGORİ SET AD=@p1 WHERE KATEGORIID= @p2 ",baglanti);
            komut4.Parameters.AddWithValue("@p1", TxtKategoriAd.Text); 
            komut4.Parameters.AddWithValue("@p2", TxtKategoriId.Text);
            komut4.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori güncelleme işlemi gerçekleştirildi.");

        }
    }
}
//Data Source=DESKTOP-ST8S73E\SQLEXPRESS;Initial Catalog=DbYeni;Integrated Security=True