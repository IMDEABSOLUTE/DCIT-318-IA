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
using Microsoft.Azure.AppService.ApiApps.Service;

namespace Shoprite_IA
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C: \Users\HP\OneDrive\Documents\ShopRiteInventory Management System.mdf;Integrated Security=True;Connect Timeout=30");     
       private void populate()
        {
            Con.Open();
            string query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpAddTb.Text == "" || EmpPassTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl values('" + EmpNameTb.Text + "','" + EmpPhoneTb.Text + "','" + EmpAddTb.Text + "','" + EmpPassTb.Text + "')", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Saved Successfully");

                    Con.Close();
                    populate();
                    Clear();
                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void Clear()
        {
            EmpNameTb.Text = "";
            EmpPassTb.Text = "";
            EmpPhoneTb.Text = "";
            EmpAddTb.Text = "";
            Key = 0;

        }
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Employee_Load(object sender, EventArgs e)
        {

        }

        int Key = 0;
        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpPhoneTb.Text = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpAddTb.Text = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPassTb.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();
            if(EmpNameTb.Text == "")
            {
                Key = 0;
            }else
            {
                Key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Employee To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Deleted From EmployeeTbl where EmpId=" + Key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted Successfully");

                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpAddTb.Text == "" || EmpPassTb.Text == "")

            {
                    MessageBox.Show("Select The Employee To Be Updated");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Update EmployeeTbl set EmpName= '" +EmpNameTb.Text+ "',EmpPhone='" +EmpPhoneTb.Text+ "',EmpAdd='" +EmpAddTb.Text+ "',EmpPass='" +EmpPassTb.Text+ "'where EmpId='" + Key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated Successfully");

                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
    
}
