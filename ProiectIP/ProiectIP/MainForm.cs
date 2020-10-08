using System;
using System.Windows.Forms;

namespace ProiectIP
{
    /// <summary>
    /// The main form of the shop, contains all the panels and menu buttons.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Constructor of MainForm class;
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Shows Home page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, EventArgs e)
        {
            Form myHome = new HomeForm();
            ModifyMainPanel(myHome);
        }
        /// <summary>
        /// Shows shopping page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShop_Click(object sender, EventArgs e)
        {
            Form myStore = new ShopForm();
            ModifyMainPanel(myStore);
        }
        /// <summary>
        /// Changes the view whenever a menu button containing a page is clicked.
        /// </summary>
        /// <param name="newForm">Form which will be displayed</param>
        private void ModifyMainPanel(Form newForm)
        {
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.TopLevel = false;
            newForm.AutoScroll = true;
            newForm.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(newForm);
            newForm.BringToFront();
            newForm.Show();
        }
        /// <summary>
        /// Closes the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Exits shop and go back to login window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm log = new LoginForm();
            log.Show();
        }
    }
}
