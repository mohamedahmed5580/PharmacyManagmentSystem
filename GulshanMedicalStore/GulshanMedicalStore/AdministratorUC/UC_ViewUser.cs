using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GulshanMedicalStore.AdministratorUC
{
    public partial class UC_ViewUser : UserControl
    {
        Function fn = new Function();
        string query;
        string currentUser = "";
        public UC_ViewUser()
        {
            InitializeComponent();
        }
        public string ID
        {

            //this GET will fetch the values and give that to ViewUser (ID) and SET those Values;
            set { currentUser = value; }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UC_ViewUser_Load(object sender, EventArgs e)
        {
            //query for showing database table
            query = "select userRole as UserRole ,name as Name,cnic as Cnic ,mobile as MobileNo,username as UserName,pass as PassCode from users ";
            DataSet ds = fn.getData(query);
            guna2DataGridView2.DataSource = ds.Tables[0];
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            //search bar working
            query = "select userRole as UserRole ,name as Name,cnic as Cnic ,mobile as MobileNo,username as UserName,pass as PassCode from users where username like'%" + txtUserName.Text+"%'";
            DataSet ds = fn.getData(query);
            guna2DataGridView2.DataSource = ds.Tables[0];
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_ViewUser_Load(this, null);
            txtUserName.Clear();
            
        }
        string userName;
        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                userName = guna2DataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure!", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes)
            {

                if (currentUser != userName)
                {
                    query = "delete from users where username='"+userName+"'";
                    fn.setData(query,"User Record Deleted");
                    UC_ViewUser_Load(this, null);

                }
                else
                {
                    MessageBox.Show("You are trying to delete your own profile", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

        }

      
    }
}