using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace GulshanMedicalStore
{
    public partial class Administrator : Form
    {
        string user = "";
        //Constructor
        public Administrator()
        {
            InitializeComponent();
        }
        public string ID
        {
            get { return user.ToString(); }
        }
        //Another Constructor is Created
        public Administrator(string username)
        {
            InitializeComponent();
            userNameLabel.Text = username;
            //Username is setting in user so current user willn't be able to remove there own self;
            user = username;
            //this GET will fetch the values and give that to ViewUser (ID) and SET those Values;
            uC_ViewUser1.ID = ID;
            //it will link username label to 
            uC_Profile1.ID = ID;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            uC_dashboard1.Visible=true;
            uC_dashboard1.BringToFront();
        }

        private void Administrator_Load(object sender, EventArgs e)
        {
            uC_dashboard1.Visible = false;
            uC_AddUser1.Visible = false;
            uC_ViewUser1.Visible = false;
            uC_Profile1.Visible = false;
            btnDashboard.PerformClick();
          
             

            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new System.Drawing.Point(0, 0);
            this.Size = new System.Drawing.Size(w, h);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void uC_dashboard1_Load(object sender, EventArgs e)
        {
           //dont use this
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            uC_AddUser1.Visible = true;
            uC_AddUser1.BringToFront();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Visible = true;
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            uC_ViewUser1.Visible=true;
            uC_ViewUser1.BringToFront();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            uC_Profile1.Visible = true;
            uC_Profile1.BringToFront();
        }
    }
}
