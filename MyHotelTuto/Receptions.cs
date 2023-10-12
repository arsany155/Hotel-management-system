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
    public partial class Receptions : Form
    {
        public Receptions()
        {
            InitializeComponent();
            populate();
            GetHotelids();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ARSANY\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30"); private void populate()
        {
            Con.Open();
            string Query = "select * from ReceptionistTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ReceptionistDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void EditReceptionist()
        {
            if (RssnTb.Text==""||RnameTb.Text == "" || RphoneTb.Text == "" || HotelCb.SelectedIndex == -1 || GenderCb.SelectedIndex == -1 || RpasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ReceptionistTbl set RSsn=@RS,RName=@RN,RPhone=@RP,RGender=@RG ,RPassword=@RPh where RSsn = @RKey ", Con);
                    cmd.Parameters.AddWithValue("@RS", RssnTb.Text);
                    cmd.Parameters.AddWithValue("@RN", RnameTb.Text);
                    cmd.Parameters.AddWithValue("@RP", RphoneTb.Text);
                    cmd.Parameters.AddWithValue("@RG", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@RPh", RpasswordTb.Text);
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist Updated!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void DeleteReceptionist()
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Receptionist!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ReceptionistTbl where RSsn = @RKey", Con);
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist Deleted!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }


        }
        private void InsertReceptionist()
        {
            if (RssnTb.Text == "" || RnameTb.Text == "" || RphoneTb.Text == "" || HotelCb.SelectedIndex == -1 || GenderCb.SelectedIndex == -1 || RpasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ReceptionistTbl(HNum,RSsn,RName,RPhone,RGender,RPassword) values(@HN,@RS,@RN,@RP,@RG,@RPh)", Con);
                    cmd.Parameters.AddWithValue("@HN", HotelCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RS", RssnTb.Text);
                    cmd.Parameters.AddWithValue("@RN", RnameTb.Text);
                    cmd.Parameters.AddWithValue("@RP", RphoneTb.Text);
                    cmd.Parameters.AddWithValue("@RG", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@RPh", RpasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist Added!!!");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }


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
        private void Receptions_Load(object sender, EventArgs e)
        {

        }

        private void RssnTb_TextChanged(object sender, EventArgs e)
        {

        }
        int Key = 0;
        private void ReceptionistDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HotelCb.Text = ReceptionistDGV.SelectedRows[0].Cells[0].Value.ToString();
            RssnTb.Text = ReceptionistDGV.SelectedRows[0].Cells[1].Value.ToString();
            RnameTb.Text = ReceptionistDGV.SelectedRows[0].Cells[2].Value.ToString();
            RphoneTb.Text = ReceptionistDGV.SelectedRows[0].Cells[3].Value.ToString();
            GenderCb.Text = ReceptionistDGV.SelectedRows[0].Cells[4].Value.ToString();
            RpasswordTb.Text = ReceptionistDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (RnameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ReceptionistDGV.SelectedRows[0].Cells[1].Value.ToString());
                
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteReceptionist();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditReceptionist();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertReceptionist();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RphoneTb_TextChanged(object sender, EventArgs e)
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

        private void label12_Click(object sender, EventArgs e)
        {
            Types obj = new Types();
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

        private void label3_Click(object sender, EventArgs e)
        {
            Locations obj = new Locations();
            obj.Show();
            this.Hide();
        }

        private void RpasswordTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void RnameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
