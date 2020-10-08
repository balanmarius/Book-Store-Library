using System.Drawing;
using System.Windows.Forms;

namespace ProiectIP
{
    /// <summary>
    /// Creates shop's home page.
    /// </summary>
    public partial class HomeForm : Form
    {
        /// <summary>
        /// Constructor of HomeForm class;
        /// </summary>
        public HomeForm()
        {
            InitializeComponent();

            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;
            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;

        }
    }
}
