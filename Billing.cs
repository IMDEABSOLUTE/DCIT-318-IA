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

namespace Shoprite_IA
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C: \Users\HP\OneDrive\Documents\ShopRiteInventory Management System.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        
        private void AddToBillBtn_Click(object sender, EventArgs e)
             
        {
            int n = 0;
            if (ItQtyTb.Text == "" || Convert.ToInt32(ItQtyTb.Text) > stock)
            {
                MessageBox.Show("Enter Quantity");
            }
            else
            {
                int total = Convert.ToInt32(ItQtyTb.Text) * Convert.ToInt32(Price.Text);
                DataGridViewRow newRow= new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ItNameTb.Text;
                newRow.Cells[2].Value = Price.Text;
                newRow.Cells[3].Value = ItQtyTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                n++;

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        int stock = 0;

        public DataGridView BillDGV { get; private set; }

        private void ItemDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItNameTb.Text = ItemDGV.SelectedRows[0].Cells[1].Value.ToString();
            
            Price.Text = ItemDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (ItNameTb.Text == "")
            {
                stock = 0;
            }
            else
            {
                stock = Convert.ToInt32(ItemDGV.SelectedRows[0].Cells[2].Value.ToString());
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
