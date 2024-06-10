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
using TheProgram.TcService;



namespace TheProgram
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;

        public void KayıtEkle()
        {
            try
            {
                long Tckn = long.Parse(maskedTextBox1.Text);
                int date = int.Parse(textBox6.Text);
                bool status;


                using (TcService.KPSPublicSoapClient service = new TcService.KPSPublicSoapClient { })
                {
                    status = service.TCKimlikNoDogrula(Tckn, textBox1.Text, textBox2.Text, date);
                }
                if (status)
                {

                    bool textBoxEmpty = true;
                    TextBox[] textBoxArray = { textBox1, textBox2, textBox3, textBox4, textBox6 };
                    foreach (TextBox textBox in textBoxArray)
                    {
                        if ((textBox.Text != "") && (maskedTextBox1.Text != ""))
                        {
                            con = new SqlConnection("Data Source=BS-BGN-0020\\MSSQLSERVER01;Initial Catalog=Books;Integrated Security=True;TrustServerCertificate=True");
                            String sorgu = "INSERT INTO Users (NameLastName,Tc,Usr,Paswd,Mail) VALUES (@Name,@Tc,@Usr,@Paswd,@Mail)";
                            cmd = new SqlCommand(sorgu, con);
                            cmd.Parameters.AddWithValue("@Name", (textBox1.Text + " " + textBox2.Text));
                            cmd.Parameters.AddWithValue("@Tc", maskedTextBox1.Text);
                            cmd.Parameters.AddWithValue("@Usr", textBox3.Text);
                            cmd.Parameters.AddWithValue("@Paswd", textBox4.Text);
                            cmd.Parameters.AddWithValue("@Mail", textBox5.Text);
                            //int userId=Convert.ToInt32(cmd.ExecuteScalar());
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            F1Gizle();
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Lütfen Kırmızı Kutucukları Doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tc Kimlik Numaranız Hatalı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Boşlukları Doldurun Lütfen!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
       
        public void F1Gizle()
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
        private void btnKaydol_Click(object sender, EventArgs e)
        {
            KayıtEkle();
        }
        public void CreateTable(int Id)
        {
            con = new SqlConnection("Data Source=BS-BGN-0020\\MSSQLSERVER01;Initial Catalog=Books;Integrated Security=True;TrustServerCertificate=True");
            {
                string sorgu = "CREATE TABLE User" + Id + "_Notes (ID INT PRIMARY KEY IDENTITY, Note NVARCHAR(100), CreationDate DATETIME DEFAULT GETDATE());";

                using (SqlConnection connection = new SqlConnection())
                using (SqlCommand command = new SqlCommand(sorgu, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            RepeatLabel((TextBox)sender, label8);
        }
        public void RepeatLabel(TextBox textBox, Label label)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                label.Visible = true;
                label1.Focus();

            }
            else
            {
                label.Visible = false;
            }
        }
        public void MaskedRepeatLabel(MaskedTextBox maskedTextBox, Label label)
        {
            if (string.IsNullOrWhiteSpace(maskedTextBox.Text))
            {
                label.Visible = true;
                label1.Focus();

            }
            else
            {
                label.Visible = false;
            }
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            F1Gizle();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            RepeatLabel((TextBox)sender, label9);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            RepeatLabel((TextBox)sender, label10);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            RepeatLabel((TextBox)sender, label12);
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            RepeatLabel((TextBox)sender, label13);
        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            MaskedRepeatLabel((MaskedTextBox)sender, label11);
        }
    }
}
