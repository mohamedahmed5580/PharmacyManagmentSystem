using System;
using System.Data;
using System.Windows.Forms;

namespace GulshanMedicalStore
{
    public partial class Login : Form
    {
        Function fn = new Function();
        string query;
        DataSet ds;
        public Login()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new System.Drawing.Point(0, 0);
            this.Size = new System.Drawing.Size(w, h);
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            //Now Adding Query To connect Login Page With DataBase
            query = "select * from users";
            ds = fn.getData(query);
            if (ds.Tables[0].Rows.Count==0)
            {
                if (txtUsername.Text == "root" && txtPassword.Text == "root")
                {
                    Administrator admin = new Administrator();
                    admin.Show();
                    this.Hide();
                }
            }
            else
            {
                query = "select * from users where username='"+txtUsername.Text+ "'and pass='"+txtPassword.Text+"'";
                ds = fn.getData(query);
                if (ds.Tables[0].Rows.Count!=0)
                { 
                    string role = ds.Tables[0].Rows[0][1].ToString();
                    if (role== "Administrator")
                    {
                        Administrator admin = new Administrator(txtUsername.Text);
                        admin.Show();
                        this.Hide();
                    }
                    else if(role=="Pharmacist")
                    { 
                        Pharmacist pharm = new Pharmacist();
                        pharm.Show();
                        this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("Wrong Username or Password","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }
    }
}
