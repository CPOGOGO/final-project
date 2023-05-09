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

namespace WindowsFormsApp2
{
    public partial class Billfm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=WDF;Integrated Security=True");

        private void populate()
        {
            Con.Open();
            String query = "select * from Goodeliverynote";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        public Billfm()
        {
            InitializeComponent();
        }

        private void Billfm_Load(object sender, EventArgs e)
        {
            populate();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Home ag = new Home();
            ag.Show();
            this.Hide();
        }

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0 || index >= BillDGV.RowCount)
                return;
            try
            {
                DataGridViewRow row = BillDGV.Rows[index];
                String ID = Convert.ToString(row.Cells[0].Value);
                String ProductID = Convert.ToString(row.Cells[2].Value);
                String Quantity = Convert.ToString(row.Cells[3].Value);
                String Delivery = Convert.ToString(row.Cells[4].Value);
                String UnitAmount = Convert.ToString(row.Cells[5].Value);

                txtID.Text = ID;
                txtProductID.Text = ProductID;
                txtQuantity.Text = Quantity;
                txtDelivery.Text = Delivery;
                txtUnitAmount.Text = UnitAmount;


            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            
                e.Graphics.DrawString("Phone Selling yo", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(250));
                e.Graphics.DrawString("ID: " + txtID.Text, new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, new Point(200, 100));
                e.Graphics.DrawString("Product ID: " + txtProductID.Text, new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, new Point(200, 120));
                e.Graphics.DrawString("Quantity: " + txtQuantity.Text, new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, new Point(200, 140));
                e.Graphics.DrawString("Delivery status: " + txtDelivery.Text, new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, new Point(200, 160));
                e.Graphics.DrawString("UnitAmount: " + txtUnitAmount.Text, new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, new Point(200, 180));


        }

        private void txtUnitAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }

  
    
}
