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

namespace HoangHuyTuan_5951071116
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetStudentRecord();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }
        public void ResetData()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-KR3IR33;Initial Catalog=DemoCRUD;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordData.DataSource = dt;
            TxtHName.Text = "";
            TxtNName.Text = "";
            TxtAdress.Text = "";
            TxtRoll.Text = "";
            txtPhone.Text = "";
        }
        public void GetStudentRecord()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-KR3IR33;Initial Catalog=DemoCRUD;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordData.DataSource = dt;
        }
        private bool IsValidData()
        {
            if(TxtHName.Text == string.Empty || TxtNName.Text == string.Empty
                || TxtAdress.Text == string.Empty
                || string.IsNullOrEmpty(txtPhone.Text)
                || string.IsNullOrEmpty(TxtRoll.Text))
                {
                MessageBox.Show("Vui lòng nhập dữ liệu !!!",
                                "Lỗi dữ liệu", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-KR3IR33;Initial Catalog=DemoCRUD;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("Insert INTO StudentsTb VALUES" +
                                            "(@Name,@FatherName,@RollNumber,@Address,@Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAdress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtPhone.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
                ResetData();
            }    
        }
        public int StudentID;

        private void StudentRecordData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(StudentRecordData.Rows[0].Cells[0].Value);
            TxtHName.Text = StudentRecordData.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtNName.Text = StudentRecordData.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtAdress.Text = StudentRecordData.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtRoll.Text = StudentRecordData.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtPhone.Text = StudentRecordData.Rows[e.RowIndex].Cells[5].Value.ToString();
        }
        
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-KR3IR33;Initial Catalog=DemoCRUD;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET " +
                    "Name = @Name, FatherName = @FatherName," +
                    "RollNumber = @RollNumber,Address = @Address," +
                    "Mobile =@Mobile WHERE StudentID =@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAdress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtPhone.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudentRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Cập nhật bị lỗi !!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-KR3IR33;Initial Catalog=DemoCRUD;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("Delete From StudentsTb WHERE StudentID = @ID",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Cập nhật bị lỗi !!!", "lỗi !",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
