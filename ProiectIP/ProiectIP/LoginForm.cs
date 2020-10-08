using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProiectIP
{
    /// <summary>
    ///  Creates the Login window to log in to an account to access the shop.
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Constructor of LoginForm class;
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            MakePwAsterisk();
        }
        /// <summary>
        /// Changes password characters when typing to "*", so the characters are not visible.
        /// </summary>
        private void MakePwAsterisk()
        {        
            passwordTextBox.PasswordChar = '*';       
        }
        /// <summary>
        /// Checks if username and password are valid and then logs to the shop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marius\source\repos\ProiectIP\DataBase\LoginDB.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                string query = "Select * from [Table] where Username = '" + usernameTextBox.Text.Trim() + " ' COLLATE SQL_Latin1_General_CP1_CS_AS and Password= '" + Cript.SHA256hash(passwordTextBox.Text.ToString()) + "' COLLATE SQL_Latin1_General_CP1_CS_AS";
                SqlDataAdapter sdata = new SqlDataAdapter(query, sqlCon);
                DataTable dtbl = new DataTable();
                sdata.Fill(dtbl);
                if (dtbl.Rows.Count == 1)
                {
                    MainForm objMain = new MainForm();
                    this.Hide();
                    objMain.Show();
                    DisplayEmail();
                    DisplayName();
                    DisplayAdress();
                }
                else
                {
                    MessageBox.Show("Invalid username and/or password!");
                }
            }
        }
        /// <summary>
        /// Closes the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Opens the registration formular.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm signUpForm = new RegisterForm();
            signUpForm.Show();
        }
        /// <summary>
        /// Closes the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Gets user's email from database.
        /// </summary>
        public void DisplayEmail()
        {

            using (SqlConnection sqlConn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Marius\source\repos\ProiectIP\DataBase\LoginDB.mdf; Integrated Security = True; Connect Timeout = 30"))
            {
                sqlConn.Open();
                string query = "Select Email from [Table] where Username= '" + usernameTextBox.Text.Trim() + "'";
                SqlDataAdapter data = new SqlDataAdapter(query, sqlConn);
                DataTable dtbl = new DataTable();
                data.Fill(dtbl);
                GlobalVariables.email = dtbl.Rows[0].ItemArray[0].ToString();
            }

        }
        /// <summary>
        /// Gets user's first name from database.
        /// </summary>
        public void DisplayName()
        {

            using (SqlConnection sqlConn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Marius\source\repos\ProiectIP\DataBase\LoginDB.mdf; Integrated Security = True; Connect Timeout = 30"))
            {
                sqlConn.Open();
                string query = "Select FirstName  from [Table] where Username= '" + usernameTextBox.Text.Trim() + "'";
                SqlDataAdapter data = new SqlDataAdapter(query, sqlConn);
                DataTable dtbl = new DataTable();
                data.Fill(dtbl);
                GlobalVariables.name = dtbl.Rows[0].ItemArray[0].ToString();
            }

        }
        /// <summary>
        /// Gets user's adress from database.
        /// </summary>
        public void DisplayAdress()
        {

            using (SqlConnection sqlConn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Marius\source\repos\ProiectIP\DataBase\LoginDB.mdf; Integrated Security = True; Connect Timeout = 30"))
            {
                sqlConn.Open();
                string query = "Select Adress  from [Table] where Username= '" + usernameTextBox.Text.Trim() + "'";
                SqlDataAdapter data = new SqlDataAdapter(query, sqlConn);
                DataTable dtbl = new DataTable();
                data.Fill(dtbl);
                GlobalVariables.adress = dtbl.Rows[0].ItemArray[0].ToString();
            }

        }


    }
    /// <summary>
    /// Creates global variables.
    /// </summary>
    public static class GlobalVariables
    {
        /// <summary>
        /// User's email adress;
        /// </summary>
        public static string email;
        /// <summary>
        /// User's name;
        /// </summary>
        public static string name;
        /// <summary>
        /// User's adress;
        /// </summary>
        public static string adress;
    }
}
