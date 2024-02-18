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
using System.Data.SqlClient;
using DevExpress.Utils.About;

namespace Proje_SQL_DB
{
    public partial class FormMusteri : Form
    {
        public FormMusteri()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-ST8S73E\SQLEXPRESS;Initial Catalog=DbYeni;Integrated Security=True");

        void Listele()
        {
            SqlCommand komut = new SqlCommand("SELECT*FROM TBLMUSTERI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt= new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void FormMusteri_Load(object sender, EventArgs e)
        {
            Listele();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("SELECT*FROM TBLSEHIRLER", baglanti);
            SqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                CmbSehir.Items.Add(dr["SEHIRAD"]);
            }
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            CmbSehir.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtBakiye.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLMUSTERI (AD,SOYAD,SEHIR,BAKIYE) VALUES (@P1,@P2,@P3,@P4)", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", CmbSehir.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtBakiye.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Sisteme Kaydedildi");
            Listele();


        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand delete = new SqlCommand("DELETE FROM TBLMUSTERI WHERE ID=@P1", baglanti);
            delete.Parameters.AddWithValue("@P1",TxtId.Text);
            delete.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Silindi");
            Listele();

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand update = new SqlCommand("UPDATE TBLMUSTERI SET AD = @P1, SOYAD=@P2, SEHIR=@P3, BAKIYE=@P4 WHERE ID=@P5", baglanti);
            update.Parameters.AddWithValue("@P1", TxtAd.Text);
            update.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            update.Parameters.AddWithValue("@P3", CmbSehir.Text);
            update.Parameters.AddWithValue("@P4", decimal.Parse(TxtBakiye.Text));
            update.Parameters.AddWithValue("@P5", TxtId.Text);

            update.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Güncellendi");
            Listele();


        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            SqlCommand search = new SqlCommand("SELECT*FROM TBLMUSTERI WHERE AD=@P1", baglanti);
            search.Parameters.AddWithValue("@p1",TxtAd.Text);
            SqlDataAdapter da = new SqlDataAdapter(search);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
