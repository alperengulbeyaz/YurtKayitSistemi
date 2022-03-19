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

namespace WindowsFormsApp1
{
    public partial class FrmBolumler : Form
    {
        public FrmBolumler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-F21T3JE;Initial Catalog=YurtKayit;Integrated Security=True");
        private void FrmBolumler_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'yurtKayitDataSet.Bolumler' table. You can move, or remove it, as needed.
            this.bolumlerTableAdapter.Fill(this.yurtKayitDataSet.Bolumler);

        }

        private void PcbBolumEkle_Click(object sender, EventArgs e)
        {

            try
            {
                baglanti.Open();
                SqlCommand komut1 = new SqlCommand("insert into Bolumler (BolumAd) values (@p1)", baglanti);
                komut1.Parameters.AddWithValue("@p1", TxtBolumAd.Text);
                komut1.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Bölüm eklendi!");
                this.bolumlerTableAdapter.Fill(this.yurtKayitDataSet.Bolumler);

            }
            catch (Exception)
            {

                MessageBox.Show("Hata oluştu! Tekrar Deneyin.");
            }
        }

        private void PcbBolumSil_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("delete from Bolumler where Bolumid=@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1", TxtBolumid.Text);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme işlemi gerçekleşti!");
                this.bolumlerTableAdapter.Fill(this.yurtKayitDataSet.Bolumler);
            }
            catch (Exception)
            {

                MessageBox.Show("Hata oluştu! Tekrar Deneyin.");
            }

        }
        int secilen;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string id, bolumad;
            secilen=dataGridView1.SelectedCells[0].RowIndex;
            id=dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            bolumad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();

            TxtBolumAd.Text = bolumad;
            TxtBolumid.Text = id;
        }

        private void PcbBolumDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update Bolumler Set Bolumad=@p1 where Bolumid=@p2", baglanti);
                komut2.Parameters.AddWithValue("@p2", TxtBolumid.Text);
                komut2.Parameters.AddWithValue("@p1", TxtBolumAd.Text);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme gerçekleşti!");
                this.bolumlerTableAdapter.Fill(this.yurtKayitDataSet.Bolumler);

            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu! Tekrar Deneyin.");

            }
        }
    }
}
