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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TheProgram
{
    public partial class Form3 : MetroFramework.Forms.MetroForm
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM TheUser";
            con = new SqlConnection("Data Source=BS-BGN-0020\\MSSQLSERVER01;Initial Catalog=Books;Integrated Security=True;TrustServerCertificate=True");
            adapter = new SqlDataAdapter(sorgu, con);
            DataTable dt = new DataTable();

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "Onay";
            checkColumn.Name = "isCompleted";
            checkColumn.DataPropertyName = "isCompleted";

            dataGridView1.Columns.Add(checkColumn);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = new DataGridViewCheckBoxCell();
                checkBoxCell.Value = row.Cells["isCompleted"].Value;
                row.Cells["isCompleted"] = checkBoxCell;
            }
            try
            {
                con.Open();
                adapter.Fill(dt);


                dataGridView1.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dataGridView1.Rows.Add(row["Note"], row["DateT"]);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            //try
            //{
            //    DataTable dt = new DataTable();


            //    con = new SqlConnection("Data Source=BS-BGN-0020\\MSSQLSERVER01;Initial Catalog=Books;Integrated Security=True;TrustServerCertificate=True");
            //    con.Open();
            //    cmd = new SqlCommand("SELECT ID FROM Users WHERE Usr = @username AND Paswd = @password", con);
            //    cmd.Parameters.AddWithValue("@username","@Usr");
            //    cmd.Parameters.AddWithValue("@password", "@Paswd");
            //    int userId = (int)cmd.ExecuteScalar();
            //    con.Close();


            //    con.Open();
            //    cmd = new SqlCommand("SELECT Note, CreationDate FROM TheUser" + userId + "_Notes", con);
            //    da = new SqlDataAdapter(cmd);
            //    dt = new DataTable();
            //    da.Fill(dt);
            //    con.Close();


            //    dataGridView1.DataSource = dt;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Notları yüklerken bir hata oluştu:  " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}


        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
        }
    }
}
