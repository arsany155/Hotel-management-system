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
    public partial class Hotels : Form
    {
        public Hotels()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ARSANY\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30"); private void populate()
        {
            Con.Open();
            string Query = "select * from HotelTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            HotelsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void EditHotel()
        {
            if (HnameTb.Text == "" || HphoneTb.Text == "" )
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update HotelTbl set HName=@HN,HPhone=@HP where HNum=@HKey", Con);
                    cmd.Parameters.AddWithValue("@HN", HnameTb.Text);
                    cmd.Parameters.AddWithValue("@HP", HphoneTb.Text);
                    cmd.Parameters.AddWithValue("@HKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hotel Updated!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void DeleteHotel()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Hotel!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from HotelTbl where HNum = @HKey", Con);
                    cmd.Parameters.AddWithValue("@HKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hotel Deleted!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }


        }
        private void InsertHotel()
        {
            if (HnameTb.Text == "" || HphoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into HotelTbl(HName,HPhone) values(@HN,@HP)", Con);
                    cmd.Parameters.AddWithValue("@HN", HnameTb.Text);
                    cmd.Parameters.AddWithValue("@HP", HphoneTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hotel Added!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }


        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void HnameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditHotel();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertHotel();
        }
        int Key = 0;

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteHotel();
        }

        private void HotelsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            HnameTb.Text = HotelsDGV.SelectedRows[0].Cells[1].Value.ToString();
            HphoneTb.Text = HotelsDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (HnameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(HotelsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void HaddressTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
                    }

        private void label11_Click(object sender, EventArgs e)
        {
            Rooms obj = new Rooms();
            obj.Show();
            this.Hide();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Locations obj = new Locations();
            obj.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Types Obj = new Types();
            Obj.Show();
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
