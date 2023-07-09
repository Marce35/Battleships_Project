using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleships
{
    public partial class GameEnd : Form
    {
        public GameEnd()
        {
            InitializeComponent();
            CenterToScreen();
            MaximizeBox = false;
        }

        private void btnPlayAgain_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnQuitGame_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnPlayAgain_MouseHover(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.ForeColor = Color.Red;
        }

        private void btnPlayAgain_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.ForeColor = Color.Black;
        }
    }
}
