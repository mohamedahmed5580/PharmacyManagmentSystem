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
    public partial class UC_P_MedicineValidityCheck : UserControl
    {
        Function fn = new Function();
        string query;
        public UC_P_MedicineValidityCheck()
        {
            InitializeComponent();
            query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as PerUnit,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff   from medic; ";
            setDataGridView(query, "All Medicine", Color.Black);
          
        }
    
        private void txtCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtCheck.SelectedIndex == 0)
            {
                btnSixMonths.Visible = false;
                btnExpired.Visible = false;
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as PerUnit,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff   from medic where eDate > getdate()";
                setDataGridView(query, "Valid Medicine", Color.Teal);
              
            }
            else if (txtCheck.SelectedIndex ==1)
            {
                btnSixMonths.Visible = true;
                btnExpired.Visible = true;
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as  DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as TradePrice ,tradeunit as Retail ,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff  from medic where DATEDIFF(MM,GETDATE(),eDate)<=6 ";
                DataSet ds = fn.getData(query);
                guna2DataGridView2.DataSource = ds.Tables[0];
                SetLabel.Text = "Expired Medicine";
                SetLabel.ForeColor = Color.DarkRed;
               
                
            }
            else if (txtCheck.SelectedIndex == 2)
            {
                btnSixMonths.Visible = false;
                btnExpired.Visible = false;
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as PerUnit,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff   from medic ";
                setDataGridView(query,"All Medicine",Color.Black);
               
            }
        }
        private void setDataGridView(string query,string labelName,Color col)
        {
            DataSet ds = fn.getData(query);
            guna2DataGridView2.DataSource = ds.Tables[0];
            SetLabel.Text = labelName;
            SetLabel.ForeColor = col;
        }

       

        private void btnSixMonths_Click(object sender, EventArgs e)
        {
            if (txtCheck.SelectedIndex == 1)
            {
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as  DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as TradePrice ,tradeunit as Retail ,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff  from medic where DATEDIFF(MM,GETDATE(),eDate)<=6  AND DATEDIFF(MM,GETDATE(),eDate)!=0 AND DATEDIFF(MM,GETDATE(),eDate)!<0 ";
                DataSet ds = fn.getData(query);
                guna2DataGridView2.DataSource = ds.Tables[0];
                SetLabel.Text = "Expired Medicine";
                SetLabel.ForeColor = Color.DarkRed;

            }
        }

        private void btnExpired_Click(object sender, EventArgs e)
        {
            if (txtCheck.SelectedIndex == 1)
            {
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as  DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as TradePrice ,tradeunit as Retail ,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff  from medic where DATEDIFF(MM,GETDATE(),eDate)<=2 ";
                DataSet ds = fn.getData(query);
                guna2DataGridView2.DataSource = ds.Tables[0];
                SetLabel.Text = "Expired Medicine";
                SetLabel.ForeColor = Color.DarkRed;
              



            }
        }
    }
}
