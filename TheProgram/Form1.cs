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
using MetroFramework;

namespace TheProgram
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //SqlConnection con;
        //SqlCommand cmd;
        //SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KaydolSayfası();
        }
        public void KaydolSayfası()
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Giris();
        }
        public void Giris()
        {
            string sorgu = "SELECT * FROM Users WHERE Usr=@User AND Paswd=@Password";
            using (SqlConnection con = new SqlConnection("Data Source=BS-BGN-0020\\MSSQLSERVER01;Initial Catalog=Books;Integrated Security=True;TrustServerCertificate=True"))
            {
                using (SqlCommand cmd = new SqlCommand(sorgu, con))
                {
                    cmd.Parameters.AddWithValue("@User", txtKAdi.Text);
                    cmd.Parameters.AddWithValue("@Password", txtSifre.Text);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Form1.ActiveForm.Hide();
                            Form3 form3 = new Form3();
                            form3.Show();
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı Kaydolmak İstemisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {
                                KaydolSayfası();
                            }

                        }
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
