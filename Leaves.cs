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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LeaveMS
{
    public partial class Leaves : Form
    {
        SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=AsLeavemg;Integrated Security=True");
        DataTable dt;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds = new DataSet();
        int emID;
        int CatId;
        public Leaves()
        {
            InitializeComponent();
            dataCode();
        }
        void dataCode()
        {
            cnct.Open();
            dt = new DataTable();
            adapter = new SqlDataAdapter("SELECT dbo.lev_Tbl.Lid, dbo.employee_Tbl.eName, dbo.Cat_Tbl.catName, dbo.lev_Tbl.reason, dbo.lev_Tbl.sDate, dbo.lev_Tbl.eDate FROM dbo.lev_Tbl INNER JOIN dbo.Cat_Tbl ON dbo.lev_Tbl.catID = dbo.Cat_Tbl.CatId INNER JOIN dbo.employee_Tbl ON dbo.lev_Tbl.emID = dbo.employee_Tbl.emID", cnct);
            adapter.Fill(dt);
            dgv.DataSource = dt;
            cnct.Close();
        }
        private void Leaves_Load(object sender, EventArgs e)
        {
            // Define your connection string


            // Create a SqlConnection object

            try
            {
                // Open the connection
                cnct.Open();

                // Define your SQL query
                string query = "SELECT * FROM employee_Tbl";
                string query0 = "SELECT * FROM employee_Tbl WHERE eName='"+staffscom.SelectedItem+"'";
                string query1 = "SELECT * FROM cat_Tbl";
                

                // Create a SqlCommand object
                using (SqlCommand command = new SqlCommand(query, cnct))
                {
                    // Execute the query and retrieve data
                    SqlDataReader reader = command.ExecuteReader();

                    // Check if there are rows
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Add each item to the ComboBox
                            staffscom.Items.Add(reader["eName"].ToString());
                        }
                    }

                    // Close the SqlDataReader
                    reader.Close();
                }
                
                using (SqlCommand command = new SqlCommand(query1, cnct))
                {
                    // Execute the query and retrieve data
                    SqlDataReader reader = command.ExecuteReader();

                    // Check if there are rows
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Add each item to the ComboBox
                            catCombo.Items.Add(reader["catName"].ToString());
                        }
                    }

                    // Close the SqlDataReader
                    reader.Close();
                }
                cnct.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void label14_Click(object sender, EventArgs e)
        {
            Leaves leaves = new Leaves();
            leaves.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Leaves leaves = new Leaves();
            leaves.Show();
            this.Hide();
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            cnct.Open();

            if (staffscom.Text != string.Empty && catCombo.Text != string.Empty && Rsntb.Text != string.Empty && dateTimePicker1.Text != string.Empty && dateTimePicker2.Text != string.Empty)
            {
                cmd = new SqlCommand("INSERT INTO lev_Tbl(emID,catID,reason,sDate,eDate) Values(@emID,@catID,@reason,@sDate,@eDate) ", cnct);
                cmd.Parameters.AddWithValue("@emID", emID);
                cmd.Parameters.AddWithValue("@catID", CatId);
                cmd.Parameters.AddWithValue("@reason", Rsntb.Text);
                cmd.Parameters.AddWithValue("@sDate", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@eDate", dateTimePicker2.Value);
                cmd.ExecuteNonQuery();
                cnct.Close();
                dataCode();
                //MessageBox.Show("Saved Successfully!");

            }
            else
            {
                MessageBox.Show("Please Enter All Details!");
            }
            cnct.Close();
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("Correct the End Date.");
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {

        }

        private void staffscom_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Open the connection
            cnct.Open();

            // Define your SQL query
            string query0 = "SELECT * FROM employee_Tbl WHERE eName='" + staffscom.SelectedItem + "'";

            using (SqlCommand command = new SqlCommand(query0, cnct))
            {
                // Execute the query and retrieve data
                SqlDataReader reader = command.ExecuteReader();

                // Check if there are rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Add each item to the ComboBox
                        emID = (int)reader["emID"];
                        label13.Text=emID.ToString();
                    }
                }

                // Close the SqlDataReader
                reader.Close();
                cnct.Close();
            }
        }

        private void catCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Open the connection
            cnct.Open();

            // Define your SQL query
            string query01 = "SELECT * FROM cat_Tbl WHERE catName='"+catCombo.SelectedItem+"'";

            using (SqlCommand command = new SqlCommand(query01, cnct))
            {
                // Execute the query and retrieve data
                SqlDataReader reader = command.ExecuteReader();

                // Check if there are rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Add each item to the ComboBox
                        CatId = (int)reader["CatId"];
                        label15.Text = CatId.ToString();
                    }
                }

                // Close the SqlDataReader
                reader.Close();
                cnct.Close();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value.Date;
            DateTime date2 = dateTimePicker2.Value.Date;

            int datt = ((TimeSpan)(date2 - date1)).Days;
            labelDay.Text = datt.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewLeaves frm = new ViewLeaves();
            frm.ShowDialog();
        }
    }
}
