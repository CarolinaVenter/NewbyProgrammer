using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagineTrailvan
{
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
            InitializeComponent();
        }//end of public frmLogIn()

        private void frmLogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }//end of private void frmLogIn_FormClosing(object sender, FormClosingEventArgs e)
    }//end of public partial class frmLogIn : Form
}//end of namespace ImagineTrailvan
