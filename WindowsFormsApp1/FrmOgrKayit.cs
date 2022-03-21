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
        SqlBaglantim bgl = new SqlBaglantim();
        private void FrmOgrKayit_Load(object sender, EventArgs e)
        {
            //Bölümleri comboboxa çekme komutları   
              
            SqlCommand komut=new SqlCommand("Select BolumAd  From Bolumler", bgl.baglanti());
            SqlDataReader oku=komut.ExecuteReader();
            while (oku.Read())
            {
                CmbBolum.Items.Add(oku[0].ToString());
            }
            bgl.baglanti().Close();
            ////////////////////////////////////
            //Boş odaları listeleme komutları

            
            SqlCommand komut2 = new SqlCommand("Select OdaNo  From Odalar where OdaKapasite !=OdaAktif", bgl.baglanti());
            SqlDataReader oku2 = komut2.ExecuteReader();
            while (oku2.Read())
            {
                CmbOdaNo.Items.Add(oku2[0].ToString());
            }
            bgl.baglanti().Close();
            /////////////////////////////////
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                //Ogrenci Kayıtlarını sqle ekleme
                
                SqlCommand komutKaydet = new SqlCommand("insert into Ogrenci(OgrAd,OgrSoyad,OgrTC,OgrTelefon,OgrDogum,OgrBolum,OgrMail,OgrOdaNo,OgrVeliAdSoyad,OgrVeliTelefon,OgrVeliAdres) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
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
                bgl.baglanti().Close();
                MessageBox.Show("Başarılı bir şekilde kayıt oldunuz!");

                // id labela çekme  
                SqlCommand komut =new SqlCommand("select Ogrid from Ogrenci",bgl.baglanti());
                SqlDataReader oku=komut.ExecuteReader();
                while(oku.Read())
                {
                    label12.Text=oku[0].ToString();
                }
                bgl.baglanti().Close(); 



                //Öğrenci Borç alanı oluşturma
                SqlCommand komutkaydet2 = new SqlCommand("insert into Borclar (Ogrid,OgrAd,OgrSoyad) values(@b1,@b2,@b3)", bgl.baglanti());
                komutkaydet2.Parameters.AddWithValue("@b1", label12.Text);
                komutkaydet2.Parameters.AddWithValue("@b2", TxtOgrAd.Text);
                komutkaydet2.Parameters.AddWithValue("@b3", TxtOgrSoyad.Text);
                komutkaydet2.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
            catch (Exception)
            {

                MessageBox.Show("HATA! Lütfen yeniden deneyiniz");
            }

            //Öğrenci Oda Kontenjanını Arttırma

            SqlCommand komutoda = new SqlCommand("update Odalar set OdaAktif=OdaAktif+1 where OdaNo=@oda1", bgl.baglanti());
            komutoda.Parameters.AddWithValue("@oda1", CmbOdaNo.Text);
            komutoda.ExecuteNonQuery();
            bgl.baglanti().Close();


        }
    }
}
