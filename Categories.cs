using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeaveMS
{
    public partial class Categories : Form
    {
        SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=AsLeavemg;Integrated Security=True");
        DataTable dt;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds = new DataSet();
        int catID;
        public Categories()
        {
            InitializeComponent();
            dataCode();
            catTexBox.Select();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
        void dataCode()
        {
            cnct.Open();
            dt = new DataTable();
            adapter = new SqlDataAdapter("SELECT * FROM cat_Tbl", cnct);
            adapter.Fill(dt);
            dgv.DataSource = dt;
            cnct.Close();
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (catTexBox.Text != string.Empty)
            {
                cnct.Open();
                cmd = new SqlCommand("UPDATE Cat_Tbl SET catName=@catname WHERE CatId = @id", cnct);
                cmd.Parameters.AddWithValue("@id", catID);
                cmd.Parameters.AddWithValue("@catname", catTexBox.Text);
                cmd.ExecuteNonQuery();
                cnct.Close();
                MessageBox.Show("Updated Successfully!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataCode();
                catTexBox.Clear();

            }
            else
            {
                MessageBox.Show("Please Select a Category Name!");
            }
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            
        }

        private void Savebtn_Click_1(object sender, EventArgs e)
        {
            cnct.Open();

            if (catTexBox.Text != string.Empty)
            {
                cmd = new SqlCommand("SELECT * FROM cat_Tbl WHERE catName='" + catTexBox.Text + "' ", cnct);
                adapter = new SqlDataAdapter(cmd);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    MessageBox.Show("This Data Already Exist!");
                    ds.Clear();
                    cnct.Close();
                    return;
                }

                cnct.Close ();
                cnct.Open();
                cmd = new SqlCommand("INSERT INTO cat_Tbl(catName)  Values(@catname)", cnct);
                cmd.Parameters.AddWithValue("@catname", catTexBox.Text);
                cmd.ExecuteNonQuery();
                cnct.Close();


                //MessageBox.Show("Saved Successfully!");
                dataCode();
                catTexBox.Clear();
                catTexBox.Select();

            }
            else
            {
                MessageBox.Show("Please Enter the Catagory Name!");
            }
        }

        private void dgv_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cnct.Open();
            catID = int.Parse(dgv.Rows[e.RowIndex].Cells[0].Value.ToString());
            catTexBox.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            cnct.Close();
        }

        private void dltbtn_Click(object sender, EventArgs e)
        {
            cnct.Open();
            cmd = new SqlCommand("DELETE FROM cat_Tbl WHERE CatId = @id", cnct);
            cmd.Parameters.AddWithValue("@id", catID);
            cmd.Parameters.AddWithValue("@name", catTexBox.Text);
            cmd.ExecuteNonQuery();
            cnct.Close();
            MessageBox.Show("Deleted Successfully!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            catTexBox.Clear();
            dataCode();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Employees employees = new Employees();
            employees.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Employees employees = new Employees();
            employees.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Leaves leaves = new Leaves();
            leaves.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Leaves leaves = new Leaves();
            leaves.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Leaves leaves = new Leaves();
            leaves.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Employees employees = new Employees();
            employees.Show();
            this.Hide();
        }
    }
}
