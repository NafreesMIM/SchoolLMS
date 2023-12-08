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
    public partial class Login : Form
    {

        SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=AsLeavemg;Integrated Security=True");
        DataTable dt;
        SqlCommand cmd;
        public Login()
        {
            InitializeComponent();
            textBoxuser.Select();
            
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {


            cnct.Open();
            string log = "SELECT * FROM pswd_Tbl WHERE Uname=@uname AND Pswd=@pswd";
            cmd = new SqlCommand(log, cnct);
            cmd.Parameters.AddWithValue("@uname", textBoxuser.Text);
            cmd.Parameters.AddWithValue("@pswd", textBoxPsw.Text);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);

            // You should execute the query before checking the results
            int rowCount = dt.Rows.Count;

            cnct.Close();
            if (textBoxuser.Text != string.Empty && textBoxPsw.Text != string.Empty)
            {
                if (rowCount > 0)
                {
                    //MessageBox.Show("Login Success!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Leaves leaves = new Leaves();
                    leaves.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxuser.Clear();
                    textBoxPsw.Clear();
                    textBoxuser.Select();
                }
            }
            else
            {
                MessageBox.Show("Please Enter Your Username and Password!","Worning",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textBoxuser.Select();
            }
            


        }
    }
}
