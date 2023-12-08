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

namespace LeaveMS
{
    public partial class Register : Form
    {
        SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=AsLeavemg;Integrated Security=True");
        DataTable dt;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        public Register()
        {
            InitializeComponent();
            textBoxuser.Select();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();

        }
        void clear()
        {
            textBoxuser.Clear();
            textBoxOPsw.Clear();
            textBoxNpsd.Clear();
            textBoxCpsd.Clear();
            textBoxuser.Select();
        }

        

        private void buttonpsdchng_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter();
            dt = new DataTable();

            // Check if a user with the provided username and old password exists
            cmd = new SqlCommand("SELECT Uname, Pswd FROM pswd_Tbl WHERE Uname=@uname AND Pswd=@opswd", cnct);
            cmd.Parameters.AddWithValue("@uname", textBoxuser.Text);
            cmd.Parameters.AddWithValue("@opswd", textBoxOPsw.Text);

            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            if (textBoxuser.Text != string.Empty && textBoxOPsw.Text != string.Empty && textBoxNpsd.Text != string.Empty && textBoxCpsd.Text != string.Empty)
            {
                if (dt.Rows.Count == 1)
                {
                    if (textBoxNpsd.Text == textBoxCpsd.Text)
                    {
                        cnct.Open();

                        // Create a new SqlCommand for the UPDATE query
                        cmd = new SqlCommand("UPDATE pswd_Tbl SET Pswd=@cpsd WHERE Uname=@uname", cnct);
                        cmd.Parameters.AddWithValue("@uname", textBoxuser.Text);
                        cmd.Parameters.AddWithValue("@cpsd", textBoxCpsd.Text);

                        cmd.ExecuteNonQuery();

                        cnct.Close();
                        label6.Text = "Successfully Updated!";
                        clear();
                        label6.ForeColor = Color.Blue;
                    }
                    else
                    {
                        label6.Text = "Please Check Your Old Password";
                        clear() ;
                        label6.ForeColor = Color.Red;
                    }
                }
                else
                {
                    label6.Text = "Your Passwords Do Not Match!";
                    clear();
                    label6.ForeColor = Color.Red;
                }
            }
            else
            {
                label6.Text = "Please Fill All.";
                label6.ForeColor = Color.Red;
            }
        }

    }

    
}
