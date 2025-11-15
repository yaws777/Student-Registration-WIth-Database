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
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Diagnostics.Eventing.Reader;
namespace ClubRegistration
{
    public partial class Form1 : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery = new ClubRegistrationQuery();
        private int ID,Age,count = 0;
        private string FirstName, MiddleName, LastName, Gender, Program;
        private long StudentID;

        private void btnRegister_Click(object sender, EventArgs e)
        {
            StudentID = long.Parse(tbStudentID.Text);
            FirstName = tbFirstName.Text;
            MiddleName = tbMiddleName.Text;
            LastName = tbLastName.Text;
            Age = int.Parse(tbAge.Text);
            Gender = cbGender.SelectedItem.ToString();
            Program = cbProgram.SelectedItem.ToString();

            RegistrationID();
            clubRegistrationQuery.RegisterStudent(count,StudentID,FirstName,MiddleName,LastName,Age,Gender,Program);
            RefreshListOfClubMembers();
            ClearField();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmUpdatecs frmUpdatecs = new FrmUpdatecs();
            frmUpdatecs.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClubRegistrationQuery clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();

        }

        private int RegistrationID() { 
            return count++;  
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        private void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView.DataSource = clubRegistrationQuery.bindingSource;
        }

        private void ClearField() { 
            tbFirstName.Clear();
            tbAge.Clear();
            tbLastName.Clear(); 
            tbMiddleName.Clear();
            tbStudentID.Clear();
            cbGender.SelectedIndex = 0;
            cbProgram.SelectedIndex = 0;
        }

        public Form1()
        {
            InitializeComponent();
        }


    }

    public class ClubRegistrationQuery
    {
        private string connectionString;
        private SqlConnection sqlConnect;
        private SqlDataAdapter sqlAdapter;
        private DataTable dataTable;
        public BindingSource bindingSource;
        private SqlCommand sqlCommand;

        public ClubRegistrationQuery()
        {
            sqlConnect = new SqlConnection("Data Source=LAPTOP-1H5TD92U\\SQLEXPRESS01;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }

        public bool DisplayList()
        {
            string ViewClubMembers = "SELECT ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM [ClubDB].dbo.ClubMembers";
            sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);

            dataTable.Clear();                 
            sqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;

            return true;
        }

        public bool RegisterStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program) {
            sqlCommand = new SqlCommand("INSERT INTO [ClubDB].dbo.ClubMembers VALUES(@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect);
            sqlCommand.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = ID;
            sqlCommand.Parameters.AddWithValue("@StudentID", SqlDbType.BigInt).Value = StudentID;
            sqlCommand.Parameters.AddWithValue("@FirstName", SqlDbType.VarChar).Value = FirstName;
            sqlCommand.Parameters.AddWithValue("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
            sqlCommand.Parameters.AddWithValue("@LastName", SqlDbType.VarChar).Value = LastName;
            sqlCommand.Parameters.AddWithValue("@Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.AddWithValue("@Gender", SqlDbType.VarChar).Value = Gender;
            sqlCommand.Parameters.AddWithValue("@Program", SqlDbType.VarChar).Value = Program;

            sqlConnect.Open(); 
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();

            return true;
        }
    }


}
