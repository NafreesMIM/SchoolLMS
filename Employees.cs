using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace LeaveMS
{
    public partial class Employees : Form
    {
        SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=AsLeavemg;Integrated Security=True");
        DataTable dt;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds=new DataSet();
        int empid;
       
        public Employees()
        {
            InitializeComponent();
            dataCode();
            dgv.Columns[0].Visible = false;
            

        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            cnct.Open();

            if (Snametb.Text != string.Empty && Gencobo.Text != string.Empty && Designtb.Text != string.Empty && Regtb.Text != string.Empty && addtx.Text != string.Empty && Sphntb.Text != string.Empty && nictxt.Text != string.Empty && mailtxt.Text != string.Empty)
            {
                cmd = new SqlCommand("SELECT * FROM employee_Tbl WHERE regNo='" + Regtb.Text + "' and Phn='" + Sphntb.Text + "'and nic='" + nictxt.Text + "'", cnct);
                adapter = new SqlDataAdapter(cmd);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    MessageBox.Show("This Data Already Exist!");
                    ds.Clear();
                    cnct.Close();
                    return;
                }
                rdr.Close();

                
                cmd = new SqlCommand("INSERT INTO employee_Tbl(eName,Gender,Designation,regNo,dateO_F_A,Address,Phn,nic,mail) " +
                    "Values(@name,@gen,@dsn,@reg,@date,@add,@phn,@nic,@mail)", cnct);
                cmd.Parameters.AddWithValue("@name", Snametb.Text);
                cmd.Parameters.AddWithValue("@gen", Gencobo.Text);
                cmd.Parameters.AddWithValue("@dsn", Designtb.Text);
                cmd.Parameters.AddWithValue("@reg", Regtb.Text);
                cmd.Parameters.AddWithValue("@date", dofaPic.Value);
                cmd.Parameters.AddWithValue("@add", addtx.Text);
                cmd.Parameters.AddWithValue("@phn", Sphntb.Text);
                cmd.Parameters.AddWithValue("@nic", nictxt.Text);
                cmd.Parameters.AddWithValue("@mail", mailtxt.Text);
                cmd.ExecuteNonQuery();
                cnct.Close();
                //MessageBox.Show("Saved Successfully!");
                dataCode();
                clear();
                Snametb.Select();

            }
            else
            {
                MessageBox.Show("Please Enter All Details!");
            }cnct.Close();
        }
        void dataCode()
        {
            cnct.Open();
            dt = new DataTable();
            adapter = new SqlDataAdapter("SELECT emID,eName,Gender,Designation,regNo,dateO_F_A,Address,Phn,nic,mail FROM employee_Tbl", cnct);
            adapter.Fill(dt);
            dgv.DataSource = dt;
            cnct.Close();
        }
        void clear()
        {
            Snametb.Clear();
            Gencobo.Text = "Male";
            Designtb.Clear();
            Regtb.Clear();
            addtx.Clear();
            Sphntb.Clear();
            nictxt.Clear();
            mailtxt.Clear();
            Snametb.Select();
        }

        private void pictureBox7_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void uptbtn_Click(object sender, EventArgs e)
        {
            if (Snametb.Text != string.Empty && Gencobo.Text != string.Empty && Designtb.Text != string.Empty && Regtb.Text != string.Empty && addtx.Text != string.Empty && Sphntb.Text != string.Empty && nictxt.Text != string.Empty && mailtxt.Text != string.Empty)
            { 
                cnct.Open();
                cmd = new SqlCommand("UPDATE employee_Tbl SET eName=@name, Gender=@gen, Designation=@dsn, regNo=@reg, dateO_F_A=@date, Address=@add, Phn=@phn,nic=@nic,mail=@mail WHERE emID = @id", cnct);
                cmd.Parameters.AddWithValue("@id", empid);
                cmd.Parameters.AddWithValue("@name", Snametb.Text);
                cmd.Parameters.AddWithValue("@gen", Gencobo.Text);
                cmd.Parameters.AddWithValue("@dsn", Designtb.Text);
                cmd.Parameters.AddWithValue("@reg", Regtb.Text);
                cmd.Parameters.AddWithValue("@date", dofaPic.Text);
                cmd.Parameters.AddWithValue("@add", addtx.Text);
                cmd.Parameters.AddWithValue("@phn", Sphntb.Text);
                cmd.Parameters.AddWithValue("@nic", nictxt.Text);
                cmd.Parameters.AddWithValue("@mail", mailtxt.Text);
                cmd.ExecuteNonQuery();
                cnct.Close();
                MessageBox.Show("Updated Successfully!","Update",MessageBoxButtons.OK,MessageBoxIcon.Information);
                dataCode();
                clear();
                Snametb.Select();

            }
            else
            {
                MessageBox.Show("Please Enter All Details!");
            }
        }

        private void dgv_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cnct.Open();
            empid = int.Parse(dgv.Rows[e.RowIndex].Cells[0].Value.ToString());
            Snametb.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            Gencobo.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            Designtb.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            Regtb.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
            dofaPic.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
            addtx.Text = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
            Sphntb.Text = dgv.Rows[e.RowIndex].Cells[7].Value.ToString();
            nictxt.Text = dgv.Rows[e.RowIndex].Cells[8].Value.ToString();
            mailtxt.Text = dgv.Rows[e.RowIndex].Cells[9].Value.ToString();
            cnct.Close();
        }

        private void dltbtn_Click(object sender, EventArgs e)
        {
            cnct.Open();
            cmd = new SqlCommand("DELETE FROM employee_Tbl WHERE emID = @id", cnct);
            cmd.Parameters.AddWithValue("@id", empid);
            cmd.Parameters.AddWithValue("@name", Snametb.Text);
            cmd.Parameters.AddWithValue("@gen", Gencobo.Text);
            cmd.Parameters.AddWithValue("@dsn", Designtb.Text);
            cmd.Parameters.AddWithValue("@reg", Regtb.Text);
            cmd.Parameters.AddWithValue("@date", dofaPic.Value);
            cmd.Parameters.AddWithValue("@add", addtx.Text);
            cmd.Parameters.AddWithValue("@phn", Sphntb.Text);
            cmd.ExecuteNonQuery();
            cnct.Close();
            MessageBox.Show("Deleted Successfully!","Delete",MessageBoxButtons.OK,MessageBoxIcon.Information);
            dataCode();
            clear();
            Snametb.Select();
        }

        private void nictxt_Validating(object sender, CancelEventArgs e)
        {
            if (nictxt.Text.Length == 12)
            {
               // MessageBox.Show("Please Type Valid NIC No!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //nictxt.Select();
            }
            else if (nictxt.Text.Length==10 && nictxt.Text.EndsWith("V")|| nictxt.Text.EndsWith("v"))
            {

            }
            else if (nictxt.Text.Length == 10 && nictxt.Text.EndsWith("X")|| nictxt.Text.EndsWith("x"))
            {

            }
            else
            {
                MessageBox.Show("Please Type Valid NIC No!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nictxt.Select();
            }
          
            
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            searchData("");
        }
        void searchData(string valueToFind)
        {
            string searchQ = "SELECT * FROM employee_Tbl WHERE CONCAT(eName,regNo,nic) LIKE'%"+valueToFind+"%' ";
            adapter = new SqlDataAdapter(searchQ,cnct);
            dt=new DataTable();
            adapter.Fill(dt);
            dgv.DataSource = dt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            searchData(textBoxsrch.Text);
        }

        private void Sphntb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)&& !char.IsControl(e.KeyChar))
            {
                e.Handled= true;
                MessageBox.Show("Connot contain Letters!");
            }
        }

        private void Sphntb_TextChanged(object sender, EventArgs e)
        {
            if (Sphntb.TextLength ==10)
            {
                nictxt.Focus();
            }
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

        private void label19_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
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
    }
}
