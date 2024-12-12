using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics; // Add this to use Process class

namespace BJ_LS
{
    public partial class splashscreen : Form
    {
        public splashscreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            progressBar1.Increment(2);
            if (progressBar1.Value == 100)
            {
                timer1.Enabled = false;

                // Path to your console application's executable
                string consoleAppPath = @"C:\Blackjack\BlackJack-Respository\BJ LS\bj\BlackJack.exe";

                try
                {
                    // Start the console application
                    Process.Start(consoleAppPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to open console application: " + ex.Message);
                }

                this.Hide();
            }
        }
    }
}