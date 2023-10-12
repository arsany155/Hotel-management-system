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
    public partial class Locations : Form
    {
        public Locations()
        {
            InitializeComponent();
            populate();
            GetCategories();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ARSANY\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30"); private void populate()
        {
            Con.Open();
            string Query = "select * from LocationTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            LocationsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void EditLocation()
        {
            if (LocationTb.Text == "" || LHotel_idCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update LocationTbl set HLocation=@HL where HNum=@LKey and HLocation=@LKey2", Con);
                    cmd.Parameters.AddWithValue("@HL", LocationTb.Text);
                    cmd.Parameters.AddWithValue("@LKey", Key);
                    cmd.Parameters.AddWithValue("@LKey2", Key2);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Location Updated!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }

        }
        private void DeleteLocation()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Location!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from LocationTbl where HNum=@LKey and HLocation=@LKey2", Con);
                    cmd.Parameters.AddWithValue("@LKey", Key);
                    cmd.Parameters.AddWithValue("@LKey2", Key2);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Location Deleted!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }


        }
        private void InsertLocation()
        {
            if (LocationTb.Text == "" || LHotel_idCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into LocationTbl(HNum,HLocation) values(@HN,@HL)", Con);
                    cmd.Parameters.AddWithValue("@HN", LHotel_idCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@HL", LocationTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Location Added!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }


        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        int Key = 0;
        String Key2 ;
        private void RnameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditLocation();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertLocation();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteLocation();
        }

        private void GetCategories()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select HNum from HotelTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HNum", typeof(int));
            dt.Load(rdr);
            LHotel_idCb.ValueMember = "HNum";
            LHotel_idCb.DataSource = dt;
            Con.Close();
        }

        private void LocationsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            LHotel_idCb.Text = LocationsDGV.SelectedRows[0].Cells[0].Value.ToString();
            LocationTb.Text = LocationsDGV.SelectedRows[0].Cells[1].Value.ToString();
            if (LocationTb.Text == ""|| LHotel_idCb.SelectedIndex == -1)
            {
                Key = 0;
                Key2 = "";
            }
            else
            {
                Key = Convert.ToInt32(LocationsDGV.SelectedRows[0].Cells[0].Value.ToString());
                Key2 = LocationsDGV.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void LHotel_idCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
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

        private void label4_Click_1(object sender, EventArgs e)
        {
            Types obj = new Types();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Receptions obj = new Receptions();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
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
    }
}
