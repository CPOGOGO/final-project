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

namespace WindowsFormsApp2
{
    public partial class Stafffm : Form
    {
        public Stafffm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=WDF;Integrated Security=True");
        private void Agentfm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home ag = new Home();
            ag.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Productfm pr = new Productfm();
            pr.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                String query = "insert into Staff values('" + txtStaffID.Text + "','" + txtStaffName.Text + "','" + txtStaffAddress.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void populate()
        {
            Con.Open();
            String query = "select * from Staff";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AgentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStaffID.Text == "")
                {
                    MessageBox.Show("No Data to delete");
                }
                else
                {
                    Con.Open();
                    String query = "delete from Staff where StaffID=@ID";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@ID", txtStaffID.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfully");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AgentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0 || index >= AgentDGV.RowCount)
                return;
            try
            {
                DataGridViewRow row = AgentDGV.Rows[index];
                String AgentID = Convert.ToString(row.Cells[0].Value);
                String AgentName = Convert.ToString(row.Cells[1].Value);
                String Address = Convert.ToString(row.Cells[2].Value);

                txtStaffID.Text = AgentID;
                txtStaffName.Text = AgentName;
                txtStaffAddress.Text = Address;

            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }
    }
}
