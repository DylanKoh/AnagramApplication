using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnagramApplication
{
    public partial class Form1 : Form
    {
        //Initial Commit
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method generates 10 random characters into the textbox when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRandom_Click(object sender, EventArgs e)
        {
            txtCharacter.Text = string.Empty;
            txtCharacter.Text += randomString();
        }

        private string randomString()
        {
            Random random = new Random();
            const string x = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var toFill = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                toFill += x.Select(s => x[random.Next(x.Length)]).FirstOrDefault();
            }

            return toFill;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {

        }
    }
}
