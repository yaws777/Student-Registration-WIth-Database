using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;    
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace ClubRegistration
{
    public partial class FrmUpdatecs : Form
    {
  

        SqlConnection sqlConnection;
        public BindingSource bindingSource;
        public FrmUpdatecs()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {
           

        }

        private void FrmUpdatecs_Load(object sender, EventArgs e)
        {
            loadComboBox();
        }

        private void loadComboBox()
        {
          
            
            sqlConnection = new SqlConnection("Data Source=LAPTOP-1H5TD92U\\SQLEXPRESS01;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand("SELECT StudentId, Gender, Program FROM [ClubDB].dbo.ClubMembers", sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string studentId = reader["StudentId"].ToString();
                if (!cbStudentID.Items.Contains(studentId))
                    cbStudentID.Items.Add(studentId);
                
            }
        }


        private void cbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection("Data Source=LAPTOP-1H5TD92U\\SQLEXPRESS01;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

            SqlCommand cmd = new SqlCommand("SELECT ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM [ClubDB].dbo.ClubMembers WHERE StudentID = @StudentID", sqlConnection);
            sqlConnection.Open();
            cmd.Parameters.AddWithValue("@StudentID", SqlDbType.BigInt).Value = cbStudentID.Text;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                cbStudentID.Text = (reader["StudentId"].ToString());
                tbFirstName.Text = reader["FirstName"].ToString();
                tbMiddleName.Text = reader["MiddleName"].ToString();
                tbLastName.Text = reader["LastName"].ToString();
                tbAge.Text = reader["Age"].ToString();
                cbGender.SelectedItem = reader["Gender"].ToString();
                cbProgram.SelectedItem = reader["Program"].ToString();
            }
            FrmUpdatecs_Load(sender, e);
            sqlConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection("Data Source=LAPTOP-1H5TD92U\\SQLEXPRESS01;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand("UPDATE [ClubDB].dbo.ClubMembers SET StudentID = @StudentID, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Age = @Age, Gender = @Gender, Program = @Program WHERE StudentID = @StudentID", sqlConnection);
            
            cmd.Parameters.AddWithValue("@StudentID", SqlDbType.BigInt).Value = cbStudentID.Text;
            cmd.Parameters.AddWithValue("@FirstName", SqlDbType.VarChar).Value = tbFirstName.Text;
            cmd.Parameters.AddWithValue("@MiddleName", SqlDbType.VarChar).Value = tbMiddleName.Text;
            cmd.Parameters.AddWithValue("@LastName", SqlDbType.VarChar).Value = tbLastName.Text;
            cmd.Parameters.AddWithValue("@Age", SqlDbType.Int).Value = tbAge.Text;
            cmd.Parameters.AddWithValue("@Gender", SqlDbType.VarChar).Value = cbGender.Text;
            cmd.Parameters.AddWithValue("@Program", SqlDbType.VarChar).Value = cbProgram.Text;

            cmd.ExecuteNonQuery();
            sqlConnection.Close();

            ClubRegistrationQuery crq = new ClubRegistrationQuery();
            crq.DisplayList();

        }

    }
}
