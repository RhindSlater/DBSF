using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBZFinal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CharacterSelect cs = new CharacterSelect();
            this.Hide();
            cs.team = false;
            cs.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CharacterSelect cs = new CharacterSelect();
            this.Hide();
            cs.Draft = true;
            cs.team = true;
            cs.ShowDialog();
            this.Show();
        }
    }
}
