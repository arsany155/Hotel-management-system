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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            GetHotelids();

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ARSANY\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");
        private void CountRooms()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(RNum) from RoomTbl where HNum = " + HotelCb.SelectedValue.ToString() + "", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            RoomsLbl.Text = dt.Rows[0][0].ToString() + "    Rooms";
            Con.Close();
        }
        private void CountCustomers()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(CustSsn) from CustomerTbl ",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustLbl.Text = dt.Rows[0][0].ToString() + "    Customers";
            Con.Close();
        }
        private void SumAmount()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum(BCost) from BookingTbl  where HNum = " + HotelCb.SelectedValue.ToString() + "", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BookingLbl.Text = " $ " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void GetReceptionists()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(RSsn) from ReceptionistTbl  where HNum = " + HotelCb.SelectedValue.ToString() + "", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ReceptionistLabel.Text = dt.Rows[0][0].ToString() + "   Receptionists";
            Con.Close();
        }

        private void GetBookingIds()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(BookNum) from BookingTbl  where HNum = " + HotelCb.SelectedValue.ToString() + "", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BookingIDsLabel.Text = dt.Rows[0][0].ToString() + "   Booking";
            Con.Close();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void BDate_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void CustomerCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
        
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DIncomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TypesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        

        private void HotelCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CountRooms();
                CountCustomers();
                SumAmount();
                GetReceptionists();
                GetBookingIds();
            }
            catch
            {

            }
            finally
            {
                Con.Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Hotels obj = new Hotels();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Rooms obj = new Rooms();
            obj.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Types obj = new Types();
            obj.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Receptions obj = new Receptions();
            obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            Bookings obj = new Bookings();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Locations obj = new Locations();
            obj.Show();
            this.Hide();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BookingLbl_Click(object sender, EventArgs e)
        {

        }
    }
}
