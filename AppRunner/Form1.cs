using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppRunner
{
    public partial class Form1 : Form
    {
        public String Database { get; set; }
        public List<Profile> Profiles { get; set; }
        public Form1()
        {
            InitializeComponent();

            String appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo fileInfo = new FileInfo(appPath);
            Database = fileInfo.Directory + "/profiles.json";

            if(File.Exists(Database))
            {
                this.LoadProfiles();
                this.ReloadUIData();
            }
            else
            {
                this.Profiles = new List<Profile>();
            }
        }

        /// <summary>
        /// Loads the profiles from the database
        /// </summary>
        public void LoadProfiles()
        {
            string json = File.ReadAllText(Database);
            Profiles = JsonConvert.DeserializeObject<List<Profile>>(json);
        }

        /// <summary>
        /// Saves the profiles into the database
        /// </summary>
        public void SaveProfiles()
        {
            string json = JsonConvert.SerializeObject(Profiles);
            File.WriteAllText(Database, json);
        }

        /// <summary>
        /// On form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Reloads the ui data for the profiles
        /// </summary>
        public void ReloadUIData()
        {
            this.comboBox1.Items.Clear();
            foreach (Profile profile in Profiles)
            {
                this.comboBox1.Items.Add(profile.Name);
            }
            //this.listView1.Items.Clear();
        }

        /// <summary>
        /// Load Applications from the selected profile
        /// </summary>
        public void LoadApplications()
        {
            Profile profile = this.Profiles[comboBox1.SelectedIndex];
            if (profile != null)
            {
                this.listView1.Items.Clear();

                ImageList icons = new ImageList();
                icons.ColorDepth = ColorDepth.Depth32Bit;
                icons.ImageSize = new Size(24, 24);
                this.listView1.LargeImageList = icons;
                this.listView1.SmallImageList = icons;

                foreach (var item in profile.Applications)
                {
                    var icon = System.Drawing.Icon.ExtractAssociatedIcon(item.Path);
                    icons.Images.Add(item.Name, icon);
                    ListViewItem listItem = new ListViewItem(item.Name);
                    listItem.ImageKey = item.Name;
                    this.listView1.Items.Add(listItem);
                }
            }
        }

        /// <summary>
        /// Create Profile Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            CreateProfileForm createProfileForm = new CreateProfileForm(this);
            createProfileForm.ShowDialog();
        }

        /// <summary>
        /// Selected Profile Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApplications();
        }

        /// <summary>
        /// Add Application Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Profile profile = this.Profiles[comboBox1.SelectedIndex];
            if(profile != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    App application = new App(openFileDialog.FileName);
                    profile.Applications.Add(application);
                    LoadApplications();
                    SaveProfiles();
                }
            }
        }

        /// <summary>
        /// Run Applications Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Profile profile = this.Profiles[comboBox1.SelectedIndex];
            if (profile != null)
            {
                foreach(App application in profile.Applications)
                {
                    Process.Start(application.Path);
                }
            }
        }
        
        /// <summary>
        /// Remove Applications Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Profile profile = this.Profiles[comboBox1.SelectedIndex];
            if (profile != null)
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem item in this.listView1.SelectedItems)
                    {
                        profile.RemoveApp(item.Text);
                    }
                    this.LoadApplications();
                    this.SaveProfiles();
                }
            }
            
        }

        /// <summary>
        /// Remove Selected Profile Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want delete the selected profile?", "Delete Profile", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Profile profile = this.Profiles[comboBox1.SelectedIndex];
                this.Profiles.Remove(profile);
                this.ReloadUIData();
                this.SaveProfiles();
                this.comboBox1.SelectedIndex = -1;
                this.comboBox1.Text = "";
            }
        }
    }
}
