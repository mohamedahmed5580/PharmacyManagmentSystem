using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GulshanMedicalStore.Pharmacy
{
    public partial class UC_P_Dashboard : UserControl
    {
        Function fn = new Function();
        DataSet ds;
        string query;
        Int64 count;


        public UC_P_Dashboard()
        {
            InitializeComponent();
            query = "delete sold where  datediff(DD,getdate(),invoiceD)>=10";
            fn.datagetiing(query);
            
        }
        private void UC_P_Dashboard_Load(object sender, EventArgs e)
        {
            loadChart();
            //sold
            query = "select invoiceno as InvoiceNo,invoiceD as InvoiceDate,mid as BatchNo,mname as MEDI_Name,eDate as Expiry,perunit as PerUnit,numunit as Qty,total as Total,disc as disc ,Afttotal as AfterDisc from sold";
            ds = fn.getData(query);
            guna2DataGridView2.DataSource = ds.Tables[0];
            
        }
        //fetch the 
        public void loadChart()
        {
            query = "select count(mname) from medic where DATEDIFF(MM, GETDATE(), eDate) > 6";
            ds = fn.getData(query);
            count = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
            this.chart1.Series["Valid Medicine"].Points.AddXY("Medicine Validity Chart", count);
            ////expiry date
            query = " select count(mname)  from medic where DATEDIFF(MM, GETDATE(), eDate) <= 6";
            ds = fn.getData(query);
            count = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
            this.chart1.Series["Expired Medicine"].Points.AddXY("Medicine Validity Chart", count);

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            UC_P_Dashboard_Load(this, null);
            ViewData.ResetText();
            txtMedi.Clear();
            chart1.Series["Expired Medicine"].Points.Clear();
            chart1.Series["Valid Medicine"].Points.Clear();
            loadChart();

        }

        private void ViewData_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            if (ViewData.SelectedIndex == 0)
            {
                //sold

                txtMedi.PlaceholderText = "---Search by Medicine Or Batch";
                query = "select invoiceno as InvoiceNo,invoiceD as InvoiceDate,mid as BatchNo,mname as MEDI_Name,eDate as Expiry,perunit as PerUnit,numunit as Qty,total as Total,disc as disc ,Afttotal as AfterDisc from sold";
                 ds = fn.getData(query);
                guna2DataGridView2.DataSource = ds.Tables[0];
            }
           
            else if (ViewData.SelectedIndex == 1)
            {
                //running
                txtMedi.PlaceholderText = "---Search by Medicine Name";
                query = "select mid as BatchNo,mname as MedicineName from sold where numunit>10";
                ds = fn.getData(query);
                guna2DataGridView2.DataSource = ds.Tables[0];
            }
            else if (ViewData.SelectedIndex == 2)
            {
                //Few Left
                txtMedi.PlaceholderText = "---Search by Medicine Name";
                query = "SELECT medic.mid,medic.mname,quantity FROM medic INNER JOIN sold ON medic.mname = sold.mname where quantity<= 15";
                ds = fn.getData(query);
                guna2DataGridView2.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Error","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //sold
                txtMedi.PlaceholderText = "---Search by Invoice No";
                query = "select invoiceno as InvoiceNo,invoiceD as InvoiceDate,mid as BatchNo,mname as MEDI_Name,eDate as Expiry,perunit as PerUnit,numunit as Qty,total as Total,disc as disc ,Afttotal as AfterDisc from sold where invoiceno like'%" + txtMedi.Text + "%' OR mid like'%" + txtMedi.Text + "%'";
                DataSet ds = fn.getData(query);
                guna2DataGridView2.DataSource = ds.Tables[0];
                if (ViewData.SelectedIndex == 0)
                {
                    //sold

                    query = "select invoiceno as InvoiceNo,invoiceD as InvoiceDate,mid as BatchNo,mname as MEDI_Name,eDate as Expiry,perunit as PerUnit,numunit as Qty,total as Total,disc as disc ,Afttotal as AfterDisc from sold where mname like'%" + txtMedi.Text + "%' OR mid like'%" + txtMedi.Text + "%'";
                    ds = fn.getData(query);
                    guna2DataGridView2.DataSource = ds.Tables[0];
                }
                else if (ViewData.SelectedIndex == 1)
                {
                    //running
                    query = "select mid as BatchNo,mname as MedicineName from sold where numunit>10 AND mname like'%" + txtMedi.Text + "%'";
                    ds = fn.getData(query);
                    guna2DataGridView2.DataSource = ds.Tables[0];
                }
                else if (ViewData.SelectedIndex == 2)
                {
                    //Few Left
                    query = "SELECT medic.mid,medic.mname,quantity FROM medic INNER JOIN sold ON medic.mname = sold.mname where quantity<= 15 AND mname like'%" + txtMedi.Text + "%'";
                    ds = fn.getData(query);
                    guna2DataGridView2.DataSource = ds.Tables[0];
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
