using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    public partial class FrmOgrDuzenle : Form
    {
        public FrmOgrDuzenle()
        {
            InitializeComponent();
        }
        
        public string id, ad, soyad, TC, telefon, dogum, bolum;

        private void BtnOgrSil_Click(object sender, EventArgs e)
        {
            //Öğrenci Silme
            try
            {
                SqlCommand komutsil = new SqlCommand("delete from Ogrenci where ogrid=@k1", bgl.baglanti());
                komutsil.Parameters.AddWithValue("@k1", TxtOgrid.Text);
                komutsil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt başarıyla silindi!");
            }
            catch (Exception)
            {

                MessageBox.Show("Hata!");
            }

            SqlCommand komutoda = new SqlCommand("update odalar set OdaAktif=OdaAktif-1 where OdaNo=@oda", bgl.baglanti());
            komutoda.Parameters.AddWithValue("@oda", CmbOdaNo.Text);
            komutoda.ExecuteNonQuery();
            bgl.baglanti().Close();
            
        }

        public string mail,odano,veliadsoyad,velitelefon,adres;


        SqlBaglantim bgl=new SqlBaglantim();
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("update Ogrenci set OgrAd=@p2,OgrSoyad=@p3,OgrTC=@p4,OgrTelefon=@p5,OgrDogum=@p6,OgrBolum=@p7,OgrMail=@p8,OgrOdaNo=@p9,OgrVeliAdSoyad=@p10,OgrVeliTelefon=@p11,OgrVeliAdres=@p12 where  Ogrid=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtOgrid.Text);
                komut.Parameters.AddWithValue("@p2", TxtOgrAd.Text);
                komut.Parameters.AddWithValue("@p3", TxtOgrSoyad.Text);
                komut.Parameters.AddWithValue("@p4", MskTC.Text);
                komut.Parameters.AddWithValue("@p5", MskOgrTelefon.Text);
                komut.Parameters.AddWithValue("@p6", MskDogum.Text);
                komut.Parameters.AddWithValue("@p7", CmbBolum.Text);
                komut.Parameters.AddWithValue("@p8", TxtMail.Text);
                komut.Parameters.AddWithValue("@p9", CmbOdaNo.Text);
                komut.Parameters.AddWithValue("@p10", TxtVeliAdSoyad.Text);
                komut.Parameters.AddWithValue("@p11", MskVeliTelefon.Text);
                komut.Parameters.AddWithValue("@p12", RchAdres.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
               

                MessageBox.Show("Kayıt başarıyla güncellendi!");    
            }
            catch (Exception)
            {

                MessageBox.Show("Hata!");
            }

        }

        private void FrmOgrDuzenle_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select BolumAd  From Bolumler", bgl.baglanti());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                CmbBolum.Items.Add(oku[0].ToString());
            }
            bgl.baglanti().Close();

            SqlCommand komut2 = new SqlCommand("Select OdaNo  From Odalar where OdaKapasite !=OdaAktif", bgl.baglanti());
            SqlDataReader oku2 = komut2.ExecuteReader();
            while (oku2.Read())
            {
                CmbOdaNo.Items.Add(oku2[0].ToString());
            }
            bgl.baglanti().Close();
            TxtOgrid.Text = id;
            TxtOgrAd.Text = ad;
            TxtOgrSoyad.Text = soyad;
            MskTC.Text = TC;
            MskOgrTelefon.Text=telefon;
            MskDogum.Text = dogum;
            CmbBolum.Text = bolum;  
            TxtMail.Text = mail;
            CmbOdaNo.Text = odano;
            TxtVeliAdSoyad.Text = veliadsoyad;
            MskVeliTelefon.Text = velitelefon;
            RchAdres.Text = adres;
        }
    }
}
