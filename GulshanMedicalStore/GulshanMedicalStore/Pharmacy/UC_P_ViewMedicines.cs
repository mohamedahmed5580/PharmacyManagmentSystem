using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;

namespace GulshanMedicalStore.Pharmacy
{
   
    public partial class UC_P_ViewMedicines : UserControl
    {
        Function fn = new Function();
        string query;
        public UC_P_ViewMedicines()
        {
            InitializeComponent();
        }

        private void UC_P_ViewMedicines_Load(object sender, EventArgs e)
        {
            query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as  Retail,tradeunit as TradePrice,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff,perunit-tradeunit as profit from medic; ";
            DataSet ds = fn.getData(query);
            guna2DataGridView2.DataSource = ds.Tables[0];
            query = "delete from medic where DATEDIFF(MM, GETDATE(), eDate) <= 0";
            fn.datagetiing(query);


            if (VisibleOnclickcombo.SelectedIndex == 0)
            {
                //Distributors
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as  Retail,tradeunit as TradePrice,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff,perunit-tradeunit as profit  from medic where mid !='' AND mname like'%" + txtMedi.Text + "%'";
                ds = fn.getData(query);
                innergridviewdisorsell.DataSource = ds.Tables[0];
               
            }
           else if (VisibleOnclickcombo.SelectedIndex == 1)
           {
                //Market
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as  Retail,tradeunit as TradePrice,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff,perunit-tradeunit as profit  from medic where mid ='' AND mname like'%" + txtMedi.Text + "%'";
                ds = fn.getData(query);
                innergridviewdisorsell.DataSource = ds.Tables[0];
               
            }
            else
            {
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as  Retail,tradeunit as TradePrice,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff,perunit-tradeunit as profit  from medic where mid !='' ";
                ds = fn.getData(query);
                innergridviewdisorsell.DataSource = ds.Tables[0];
                instruc.Text = "Delete By ID";
            }
            

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            DataSet ds;
            if (VisibleOnclickcombo.SelectedIndex == 0)
            {
                //Distributor
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as  Retail,tradeunit as TradePrice,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff,perunit-tradeunit as profit  from medic where mid !='' AND mname like'%" + txtMedi.Text + "%'";
                ds = fn.getData(query);
                innergridviewdisorsell.DataSource = ds.Tables[0];
            }
            if (VisibleOnclickcombo.SelectedIndex==1)
            {
                //for market
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as  Retail,tradeunit as TradePrice,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff,perunit-tradeunit as profit  from medic where mid ='' AND mname like'%" + txtMedi.Text + "%'";
                ds = fn.getData(query);
                innergridviewdisorsell.DataSource = ds.Tables[0];
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_P_ViewMedicines_Load (this, null);
        }

        string BatchNo,medicineName,seller,Quantity,invoice;
        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                invoice = guna2DataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                BatchNo = guna2DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                medicineName = guna2DataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                seller = guna2DataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                Quantity = guna2DataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
             
            }
            catch 
            {

               
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //Delete Record btn
            if (MessageBox.Show("Are You Sure?","Delete Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                if (medicineName==null)
                {
                    query = "Delete from medic where mid='" +BatchNo+ "' AND  invoice='" +invoice+ "' ";
                    fn.setData(query, "Record Deleted");
                    UC_P_ViewMedicines_Load(this, null);
                }
                else if(medicineName!=""  || BatchNo!=null)
                {
                    query = "Delete from medic where mname='"+medicineName+ "' AND mnumber='"+seller+"' AND quantity='"+Quantity+"' AND invoice='"+invoice+"' ";
                    fn.setData(query, "Record Deleted");
                    UC_P_ViewMedicines_Load(this, null);
                }
                else
                {
                    MessageBox.Show("Error");
                }
                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //removeddd
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {

            //PRINT BUTTON
            DGVPrinter print = new DGVPrinter();
            print.Title = "Medincine Bill";
            print.SubTitle = string.Format("Date:- {0}", DateTime.Now.Date);
            print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            print.PageNumbers = true;
            print.PageNumberInHeader = false;
            print.PorportionalColumns = true;
            print.HeaderCellAlignment = StringAlignment.Near;
            //print.Footer = "Total Payable Amount :"+TotalLabel.Text;
            //print.FooterSpacing = 15;
            print.PrintDataGridView(guna2DataGridView2);

            //totalAmount = 0;
            //TotalLabel.Text = "Rs.00";
            //guna2DataGridView1.DataSource = 0;

            if (VisibleOnclickcombo.SelectedIndex == 1)
            {
                DGVPrinter printspecific = new DGVPrinter();
                print.Title = "Medincine Bill";
                print.SubTitle = string.Format("Date:- {0}", DateTime.Now.Date);
                print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                print.PageNumbers = true;
                print.PageNumberInHeader = false;
                print.PorportionalColumns = true;
                print.HeaderCellAlignment = StringAlignment.Near;
                print.PrintDataGridView(innergridviewdisorsell);
            }


        }

        private void DistributorNseller_Click(object sender, EventArgs e)
        {
            if(VisibleOnclickcombo.Visible!=true)
            {
                //Make Combobox Visible first
                VisibleOnclickcombo.Visible = true;
                //Make Gridview Visible 
                innergridviewdisorsell.BringToFront();
                guna2DataGridView2.SendToBack();
            }
            else
            {
                //Make Combobox Visible first
                VisibleOnclickcombo.Visible = false;
                //Make Gridview Visible 
                guna2DataGridView2.BringToFront();
                innergridviewdisorsell.SendToBack();
            }
            
        }

        private void VisibleOnclickcombo_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            if (VisibleOnclickcombo.SelectedIndex == 0)
            {
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as  Retail,tradeunit as TradePrice,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff,perunit-tradeunit as profit  from medic where mid !='' ";
                DataSet ds = fn.getData(query);
                innergridviewdisorsell.DataSource = ds.Tables[0];
                instruc.Text = "Delete By ID";
            }
            if (VisibleOnclickcombo.SelectedIndex == 1)
            {
                query = "select invoice as InvoiceNo,mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as  Retail,tradeunit as TradePrice,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff,perunit-tradeunit as profit  from medic where mid =''";
                DataSet ds= fn.getData(query);
                innergridviewdisorsell.DataSource = ds.Tables[0];
                instruc.Text = "Delete By Medicine Name";

            }
            
            
        }

        private void innergridviewdisorsell_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                invoice = guna2DataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                BatchNo = innergridviewdisorsell.Rows[e.RowIndex].Cells[1].Value.ToString();
                medicineName = innergridviewdisorsell.Rows[e.RowIndex].Cells[2].Value.ToString();
                seller = innergridviewdisorsell.Rows[e.RowIndex].Cells[3].Value.ToString();
                Quantity = innergridviewdisorsell.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            catch
            {
                

            }
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            query = "select mid as BatchNo,mname as MedicineName,mnumber as DistributerORSeller,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as  Retail,tradeunit as TradePrice from medic where mid !='' ";
            DataSet ds = fn.getData(query);
            innergridviewdisorsell.DataSource = ds.Tables[0];
            instruc.Text = "Delete By ID";
        }


    }
}
