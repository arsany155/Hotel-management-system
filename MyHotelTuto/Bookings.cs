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
    public partial class Bookings : Form
    {
        public Bookings()
        {
            InitializeComponent();
           // populate();
            GetHotelids();
            GetCustomerSsn();



        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ARSANY\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30"); private void populate()
        {
            Con.Open();
            string Query = "select * from BookingTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookingDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        
        int Price=1;
        private void fetchCost()
        {
            Con.Open();
            string Query = "select TypeTbl.TypeCost from RoomTbl join TypeTbl on RoomTbl.RType=TypeTbl.TypeNum where RoomTbl.RNum=" + RoomCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                Price = Convert.ToInt32(dr["TypeCost"].ToString());
               
            }
            Con.Close();
        }
       
        private void BookBtn_Click(object sender, EventArgs e)
        {
            if ( HotelCb.SelectedIndex == -1 || RssnCb.SelectedIndex == -1 || CustomerCb.SelectedIndex == -1|| RoomCb.SelectedIndex == -1 || AmountTb.Text == ""||DurationTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BookingTbl(HNum,RNum,RSsn,CustSsn,BDate,BDuration,BCost) values(@HN,@RN,@RS,@CS,@BD,@BDu,@BC)", Con);
                    cmd.Parameters.AddWithValue("@HN", HotelCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RN", RoomCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RS", RssnCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CS", CustomerCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@BD", BDate.Value.Date);
                    cmd.Parameters.AddWithValue("@BDu", DurationTb.Text);
                    cmd.Parameters.AddWithValue("@BC", AmountTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Booked!!!");
                    Con.Close();
                    populate();
                    setBooked();
                    GetRooms();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void RoomCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCost();
        }

        private void DurationTb_TextChanged(object sender, EventArgs e)
        {
            if(AmountTb.Text == "")
            {
                AmountTb.Text = "0";
            }
            else
            {
                try
                {
                    int Total = Price * Convert.ToInt32(DurationTb.Text);
                    AmountTb.Text = "" + Total;
                }
                catch (Exception Ex)
                {

                   // MessageBox.Show("")
                }
                                                    
            }            
        }
        int Key = 0;
        private void CancelBooking()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Booking!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from BookingTbl where BookNum = @BKey", Con);
                    cmd.Parameters.AddWithValue("@BKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Booking Cancelled!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }


        }
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            CancelBooking();
            setAvailable();
            GetRooms();
        }
        private void setBooked()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update RoomTbl set RStatus=@RS where RNum = @RKey", Con);
                cmd.Parameters.AddWithValue("@RS","Booked");
                cmd.Parameters.AddWithValue("@RKey", RoomCb.SelectedValue.ToString());
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
        private void setAvailable()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update RoomTbl set RStatus=@RS where RNum = @RKey", Con);
                cmd.Parameters.AddWithValue("@RS", "Available");
                cmd.Parameters.AddWithValue("@RKey", RoomCb.Text);
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
        private void BookingDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HotelCb.Text = BookingDGV.SelectedRows[0].Cells[1].Value.ToString();
            RoomCb.Text = BookingDGV.SelectedRows[0].Cells[2].Value.ToString();
            RssnCb.Text = BookingDGV.SelectedRows[0].Cells[3].Value.ToString();
            CustomerCb.Text = BookingDGV.SelectedRows[0].Cells[4].Value.ToString();
            BDate.Text = BookingDGV.SelectedRows[0].Cells[5].Value.ToString();
            DurationTb.Text = BookingDGV.SelectedRows[0].Cells[6].Value.ToString();
            AmountTb.Text = BookingDGV.SelectedRows[0].Cells[7].Value.ToString();
            if (AmountTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(BookingDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
           
        }

        private void label8_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void CustomerCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RoomCb_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void BDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void HotelCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetRooms();
                GetReceptionistSsn();

            }
            catch
            {

            }
            finally
            {

            }
        }
        private void GetRooms()
        {
            Con.Open();
            string Query = "select RNum from RoomTbl where RStatus='Available' and HNum = " + HotelCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RNum", typeof(int));
            dt.Load(rdr);
            RoomCb.ValueMember = "RNum";
            RoomCb.DataSource = dt;
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
        private void GetReceptionistSsn()
        {

            Con.Open();
            SqlCommand cmd = new SqlCommand("select RSsn from ReceptionistTbl where  HNum = " + HotelCb.SelectedValue.ToString() + "", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RSsn", typeof(int));
            dt.Load(rdr);
            RssnCb.ValueMember = "RSsn";
            RssnCb.DataSource = dt;
            Con.Close();
        }
        private void GetCustomerSsn()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CustSsn from CustomerTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustSsn", typeof(int));
            dt.Load(rdr);
            CustomerCb.ValueMember = "CustSsn";
            CustomerCb.DataSource = dt;
            Con.Close();
        }

        private void RssnCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AmountTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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

        private void label4_Click(object sender, EventArgs e)
        {
            Types obj = new Types();
            obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
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

        private void label8_Click_1(object sender, EventArgs e)
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
    }
}
