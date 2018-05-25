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
    public partial class frmSwitchboard : Form
    {
        public frmSwitchboard()
        {
            InitializeComponent();
        }//end of public Form1()

        private void button1_Click(object sender, EventArgs e)
        {
          //  frmInventory obj = new frmInventory();
            frmLogIn obj = new frmLogIn();
            this.Hide();
            obj.ShowDialog();
        }//end of private void button1_Click(object sender, EventArgs e)

        private void linkStock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventory obj = new frmInventory();
            this.Hide();
            obj.ShowDialog();
        }//end of private void linkStock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)

        private void frmSwitchboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmLogIn obj = new frmLogIn();
            this.Hide();
            obj.ShowDialog();
            Environment.Exit(0);
        }//end of private void frmSwitchboard_FormClosing(object sender, FormClosingEventArgs e)
    }//end of public partial class Form1 : Form
}//end of namespace ImagineTrailvan
