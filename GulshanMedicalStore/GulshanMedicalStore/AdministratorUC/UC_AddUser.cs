using System;
using System.Data;
using System.Windows.Forms;

namespace GulshanMedicalStore.AdministratorUC
{
    public partial class UC_AddUser : UserControl
    {
        Function fn = new Function();
        string query;
        public UC_AddUser()
        {
            InitializeComponent();
        }

        
        private void btnDob_Click(object sender, EventArgs e)
        {
            string role = txtUserRole.Text;
            string name = txtName.Text;
            string dob = txtCnic.Text;
            string mobile = Convert.ToString(txtMobile.Text);
            string username = TxtUsername.Text;
            string pass = TxtPassword.Text;

            try
            {
                query = "insert into users(userRole,name,cnic,mobile,username,pass) values('"+role+"','"+name+"','"+dob+"','"+mobile+ "','"+username+"','"+pass+"')";
                fn.setData(query, "Sign Up Successful.");
             }
            catch 
            {
                MessageBox.Show("username already exist", "error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
}

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            clearAll();
        }
        public void clearAll()
        {
            txtName.Clear();
            txtCnic.ResetText();
            txtMobile.Clear();
            TxtUsername.Clear();
            TxtPassword.Clear();
            txtUserRole.SelectedIndex = 1;
            

        }
        private void TxtUsername_TextChanged_1(object sender, EventArgs e)
        {
            query = "select * from users where username='" + TxtUsername.Text + "'";
            DataSet ds = fn.getData(query);
            if (ds.Tables[0].Rows.Count == 0)
            {
                pictureBox1.ImageLocation = @"E:\Aptech\Project\Pharmacy Management System\GulshanMedicalStore\GulshanMedicalStore\Image\yes.png";

            }
            else
            {
                pictureBox1.ImageLocation = @"E:\Aptech\Project\Pharmacy Management System\GulshanMedicalStore\GulshanMedicalStore\Image\no.png";

            }
        }
        
        

        

       

        

      
    }
}
