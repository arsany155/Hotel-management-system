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

namespace MyHotelTuto
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ARSANY\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string Query = "select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomersDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void EditCustomer()
        {
            if (CSsnTb.Text == "" ||CnameTb.Text == "" || GenderCb.SelectedIndex == -1 || phoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTbl set CustSsn=@Cs,CustName=@CN,CustPhone=@CP,CustGender=@CG where CustSsn=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CS", CSsnTb.Text);
                    cmd.Parameters.AddWithValue("@CN", CnameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", phoneTb.Text);
                    cmd.Parameters.AddWithValue("@CG", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void DeleteCustomer()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Customer!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CustSsn = @CKey", Con);
                    cmd.Parameters.AddWithValue("@CKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }


        }
        private void InsertCustomer()
        {
            if (CSsnTb.Text == "" || CnameTb.Text == "" || GenderCb.SelectedIndex == -1 || phoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl(CustSsn,CustName,CustPhone,CustGender) values(@CS,@CN,@CP,@CG)", Con);
                    cmd.Parameters.AddWithValue("@CS", CSsnTb.Text);
                    cmd.Parameters.AddWithValue("@CN", CnameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", phoneTb.Text);
                    cmd.Parameters.AddWithValue("@CG", GenderCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }


        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertCustomer();
        }
        int Key = 0;
        private void CustomersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CSsnTb.Text = CustomersDGV.SelectedRows[0].Cells[0].Value.ToString();
            CnameTb.Text = CustomersDGV.SelectedRows[0].Cells[1].Value.ToString();
            phoneTb.Text = CustomersDGV.SelectedRows[0].Cells[2].Value.ToString();
            GenderCb.Text = CustomersDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (CnameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteCustomer();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditCustomer();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Rooms Obj = new Rooms();
            Obj.Show();
            this.Hide();
        }

        private void CnameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void CustSsn_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Hotels obj = new Hotels();
            obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Rooms obj = new Rooms();
            obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Types obj = new Types();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Bookings obj = new Bookings();
            obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Locations obj = new Locations();
            obj.Show();
            this.Hide();
        }

        private void GenderCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            Receptions obj = new Receptions();
            obj.Show();
            this.Hide();
        }

        private void phoneTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
