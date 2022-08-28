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
        int n = 0, GrdTotal = 0, Amount;
        private void AddToBillBtn_Click(object sender, EventArgs e)
             
        {
            
            if (ItQtyTb.Text == "" || Convert.ToInt32(ItQtyTb.Text) > stock || ItNameTb.Text == "")
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
                GrdTotal = GrdTotal + total;
                Amount = GrdTotal;
                TotalLbl.Text = "Rs" + GrdTotal;
                n++;
                UpdateItem();
                Reset();
            }
        }   


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        int stock = 0, Key=0;

        public DataGridView BillDGV { get; private set; }

        private void ItemDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItNameTb.Text = ItemDGV.SelectedRows[0].Cells[1].Value.ToString();
            
            Price.Text = ItemDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (ItNameTb.Text == "")
            {
                stock = 0;
                Key = 0;
            }
            else
            {
                stock = Convert.ToInt32(ItemDGV.SelectedRows[0].Cells[2].Value.ToString());
                Key = Convert.ToInt32(ItemDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }
        private void UpdateItem()
        {
            try
            {
                int newQty = stock - Convert.ToInt32(ItQtyTb.Text);
                Con.Open();
                string query = "Update ItemTbl set ItQty=" + newQty + "where ItId=" + Key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Updated Successfully");

                Con.Close();
                populate();
                //Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void Reset()
        {
            Price.Text = "";
            ItQtyTb.Text = "";
            ClientNameTb.Text = "";
            ItNameTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
        String  EName = "Ename";
        int EId = 1;
        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (ClientNameTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl values(" + EId + ",'" + EName + "','" + ClientNameTb.Text + "'," + Amount + ")", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved Successfully");

                    Con.Close();
                    populate();
                    //Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void EmployeeLbl_Click(object sender, EventArgs e)
        {

        }

        private void Billing_Load(object sender, EventArgs e)
        {
            EmployeeLbl.Text = Login.EmployeeName;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void EmpPassTb_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
