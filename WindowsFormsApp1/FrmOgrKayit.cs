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
    public partial class FrmOgrKayit : Form
    {
        //////////truncate table sql temizleme ////////
        public FrmOgrKayit()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-F21T3JE;Initial Catalog=YurtKayit;Integrated Security=True");
        private void FrmOgrKayit_Load(object sender, EventArgs e)
        {
            //Bölümleri comboboxa çekme komutları   
            baglanti.Open();    
            SqlCommand komut=new SqlCommand("Select BolumAd  From Bolumler",baglanti);
            SqlDataReader oku=komut.ExecuteReader();
            while (oku.Read())
            {
                CmbBolum.Items.Add(oku[0].ToString());
            }
            baglanti.Close();
            ////////////////////////////////////
            //Boş odaları listeleme komutları

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select OdaNo  From Odalar where OdaKapasite !=OdaAktif", baglanti);
            SqlDataReader oku2 = komut2.ExecuteReader();
            while (oku2.Read())
            {
                CmbOdaNo.Items.Add(oku2[0].ToString());
            }
            baglanti.Close();
            /////////////////////////////////
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                //Ogrenci Kayıtlarını sqle ekleme
                baglanti.Open();
                SqlCommand komutKaydet = new SqlCommand("insert into Ogrenci(OgrAd,OgrSoyad,OgrTC,OgrTelefon,OgrDogum,OgrBolum,OgrMail,OgrOdaNo,OgrVeliAdSoyad,OgrVeliTelefon,OgrVeliAdres) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", baglanti);
                komutKaydet.Parameters.AddWithValue("@p1", TxtOgrAd.Text);
                komutKaydet.Parameters.AddWithValue("@p2", TxtOgrSoyad.Text);
                komutKaydet.Parameters.AddWithValue("@p3", MskTC.Text);
                komutKaydet.Parameters.AddWithValue("@p4", MskOgrTelefon.Text);
                komutKaydet.Parameters.AddWithValue("@p5", MskDogum.Text);
                komutKaydet.Parameters.AddWithValue("@p6", CmbBolum.Text);
                komutKaydet.Parameters.AddWithValue("@p7", TxtMail.Text);
                komutKaydet.Parameters.AddWithValue("@p8", CmbOdaNo.Text);
                komutKaydet.Parameters.AddWithValue("@p9", TxtVeliAdSoyad.Text);
                komutKaydet.Parameters.AddWithValue("@p10", MskVeliTelefon.Text);
                komutKaydet.Parameters.AddWithValue("@p11", RchAdres.Text);
                komutKaydet.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Başarılı bir şekilde kayıt oldunuz!");
            }
            catch (Exception)
            {

                MessageBox.Show("HATA! Lütfen yeniden deneyiniz");
            }

        }
    }
}
