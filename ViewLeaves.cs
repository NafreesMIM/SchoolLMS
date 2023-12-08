using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeaveMS
{
    public partial class ViewLeaves : Form
    {
        SqlConnection cnct = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=AsLeavemg;Integrated Security=True");
        DataTable dt;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds = new DataSet();
        int emID;
        int CatId;
       
        public ViewLeaves()
        {
           
            InitializeComponent();
            reportViewer1.RefreshReport();
 
        }

      

      

        private void pictureBox7_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void ViewLeaves_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
