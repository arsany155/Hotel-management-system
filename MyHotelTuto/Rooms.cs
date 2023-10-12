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
 
namespace MyHotelTuto
{
    public partial class Rooms : Form
    {
        public Rooms()
        {
            InitializeComponent();
            populate();
            GetCategories();
            GetHotelids();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ARSANY\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30"); private void populate()
        {
            Con.Open();
            string Query = "select * from RoomTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            RoomsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void EditRooms()
        {
            if (RnameTb.Text == "" || RTypeCb.SelectedIndex == -1 || StatusCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update RoomTbl set RName=@RN,RType=@RT,RStatus=@RS where RNum = @RKey and HNum=@HKey", Con);
                    cmd.Parameters.AddWithValue("@RN", RnameTb.Text);
                    cmd.Parameters.AddWithValue("@RT", RTypeCb.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@RS", StatusCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.Parameters.AddWithValue("@HKey", Key2);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Updated!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void DeleteRooms()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Room!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from RoomTbl where Rnum = @RKey and HNum = @HKey", Con);
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.Parameters.AddWithValue("@HKey", Key2);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Deleted!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }


        }
        private void InsertRooms()
        {
            if(RnameTb.Text == "" || RTypeCb.SelectedIndex == -1 || StatusCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!!!");
            }else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into RoomTbl(HNum,RName,RType,RStatus) values(@HN,@RN,@RT,@RS)", Con);
                    cmd.Parameters.AddWithValue("@HN", HotelCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RN", RnameTb.Text);
                    cmd.Parameters.AddWithValue("@RT", RTypeCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RS", "Available");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Added!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
           
           
        }
        private void GetCategories()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select TypeNum from TypeTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TypeNum", typeof(int));
            dt.Load(rdr);
            RTypeCb.ValueMember = "TypeNum";
            RTypeCb.DataSource = dt;
            Con.Close();
        }
        private void GetHotelids()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select HNum from HotelTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HNum", typeof(int));
            dt.Load(rdr);
            HotelCb.ValueMember = "HNum";
            HotelCb.DataSource = dt;
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertRooms();
        }
        int Key = 0;
        int Key2 = 0;
        private void RoomsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            RnameTb.Text = RoomsDGV.SelectedRows[0].Cells[2].Value.ToString();
            RTypeCb.Text = RoomsDGV.SelectedRows[0].Cells[3].Value.ToString();
            StatusCb.Text = RoomsDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (RnameTb.Text == ""  ||HotelCb.SelectedIndex == -1)
            {
                Key = 0;
                Key2 = 0;
            }
            else
            {
                Key = Convert.ToInt32(RoomsDGV.SelectedRows[0].Cells[1].Value.ToString());
                Key2 = Convert.ToInt32(RoomsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditRooms();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteRooms();
        }

        private void label4_Click(object sender, EventArgs e)
        {
          
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void label6_Click(object sender, EventArgs e)
        {
           
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void RnameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RTypeCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void HotelCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StatusCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Hotels obj = new Hotels();
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

        private void label7_Click_1(object sender, EventArgs e)
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }
    }
}
