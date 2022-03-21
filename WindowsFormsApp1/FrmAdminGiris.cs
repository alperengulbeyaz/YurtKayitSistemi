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
    public partial class FrmAdminGiris : Form
    {
        public FrmAdminGiris()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl=new SqlBaglantim();
        private void BtnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from admin where yoneticiad=@p1 and yoneticisifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtKullanici.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if(oku.Read())
            {
                FrmAnaForm fr =new FrmAnaForm();
                fr.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Hatalı kullanıcı adı ve şifre");
                TxtKullanici.Clear();
                TxtSifre.Clear();
                TxtKullanici.Focus();
            }

            

        }

        private void BtnGiris_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void FrmAdminGiris_Load(object sender, EventArgs e)
        {
            
        }

        private void FrmAdminGiris_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
    }
}
