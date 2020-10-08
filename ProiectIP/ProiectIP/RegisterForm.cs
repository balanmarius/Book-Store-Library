using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace ProiectIP
{
    /// <summary>
    /// Creates the register formular.
    /// </summary>
    public partial class RegisterForm : Form
    {
        /// <summary>
        /// Constructor of RegisterForm class;
        /// </summary>
        public RegisterForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Saves the registration data of the new user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text=="" || txtPassword.Text == "")
            {
                MessageBox.Show("Please fill mandatory fields!(*)");
            }
            else
            {
                using (SqlConnection sqlConn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Marius\source\repos\ProiectIP\DataBase\LoginDB.mdf; Integrated Security = True; Connect Timeout = 30"))
                {

                    sqlConn.Open();

                    SqlCommand sqlCmdID = new SqlCommand("ResetID", sqlConn);
                    sqlCmdID.ExecuteNonQuery();

                    SqlCommand sqlCmd = new SqlCommand("UserAdd", sqlConn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Age", txtAge.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Password", Cript.SHA256hash(txtPassword.Text.ToString()));
                    sqlCmd.Parameters.AddWithValue("@Adress", txtAdress.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text.Trim());
                    sqlCmd.ExecuteNonQuery();

                    MessageBox.Show("Registration is succesfull! Now log in to your new account!");
                    this.Hide();
                    LoginForm login = new LoginForm();
                    login.Show();
                }
                    
            }

        }
        /// <summary>
        /// Clears all fields from registration formular.
        /// </summary>
        void Clear()
        {
            txtFirstName.Text = txtLastName.Text = txtEmail.Text = txtAge.Text = txtUsername.Text = txtPassword.Text = "";
        }
        /// <summary>
        /// Closes the registration form and goes back to login window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
        }
    }
}
