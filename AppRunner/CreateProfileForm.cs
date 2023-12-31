using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppRunner
{
    public partial class CreateProfileForm : Form
    {
        private Form1 parent;

        public CreateProfileForm(Form1 form1)
        {
            InitializeComponent();
            this.parent = form1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile(this.textBox1.Text);
            this.parent.Profiles.Add(profile);
            this.parent.ReloadUIData();
            this.parent.SaveProfiles();
            this.Close();
        }
    }
}
