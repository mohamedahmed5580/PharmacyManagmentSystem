using DGVPrinterHelper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GulshanMedicalStore.Pharmacy
{
    public partial class UC_P_SellMedicine : UserControl
    {
        Function fn = new Function();
        DataSet ds;
        string query;
        public UC_P_SellMedicine()
        {
            InitializeComponent();
           
        }

        private void UC_P_SellMedicine_Load(object sender, EventArgs e)
        {
            listBoxMedicines.Items.Clear();
            query = "Select mname from medic where eDate>= getdate() and quantity >'0'";
            ds = fn.getData(query);
            for (int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                listBoxMedicines.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            clearAll();
            
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_P_SellMedicine_Load(this,null);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listBoxMedicines.Items.Clear();
            query = "select mname from medic where mname like'%"+ txtSearch.Text +"%' and eDate > getdate() and quantity >'0'";
            ds = fn.getData(query);
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBoxMedicines.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        private void listBoxMedicines_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNoOfUnit.Clear();
            string name = listBoxMedicines.GetItemText(listBoxMedicines.SelectedItem);
            txtMediName.Text = name;

            //Entering query for other text boxes to auto fill;
            try
            {
                query = "Select mid,perunit,eDate,tradeunit from medic where mname='" + name + "'";
                ds = fn.getData(query);
                //Fetching ID
                txtMediID.Text = ds.Tables[0].Rows[0][0].ToString();
                txtPricePerUnit.Text = ds.Tables[0].Rows[0][1].ToString();
                txtExpiry.Text = ds.Tables[0].Rows[0][2].ToString();
                //profit Counter
                Int64 cal = Int64.Parse(ds.Tables[0].Rows[0][1].ToString())- Int64.Parse(ds.Tables[0].Rows[0][3].ToString());
                labelProfit.Text = "PKR"+cal.ToString()+"/-";
            }
            catch 
            {

                MessageBox.Show("Select Medicine Correctly");
            }
        }

        
        private void txtNoOfUnit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Medicine Unit Price On total
                if (txtNoOfUnit.Text != "")
                {
                    Int64 unitprice = Int64.Parse(txtPricePerUnit.Text);
                    Int64 noOfUnit = Int64.Parse(txtNoOfUnit.Text);
                    Int64 total = unitprice * noOfUnit;
                    txtTotalPrice.Text = total.ToString();
                    txtOriTotal.Text = total.ToString();

                }
                else
                {
                    //Total Price Remain Clear
                    txtTotalPrice.Clear();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Medicine isn't Selected");
            }
           
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           //remove
        }

        int valueAmount;
        string valueid,validname;
        protected Int64 noofunit;
        string valuename;
        
        //Protected
        protected int n, totalAmount = 0;
        protected Int64 quantity, newQuantity;

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (valueid != null)
            {
                try
                {
                    guna2DataGridView1.Rows.RemoveAt(this.guna2DataGridView1.SelectedRows[0].Index);
                }
                catch
                {

                   
                }
                finally
                {
                    query = "Select quantity from medic where mid ='"+valueid+"'";
                    
                    ds = fn.getData(query);
                    quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    newQuantity = quantity + noofunit;
                   

                    query = "update medic set quantity ='" + newQuantity + "' where mid='"+valueid+"'  ";
                    fn.setData(query, "Medicine Removed from Cart");
                    totalAmount = totalAmount - valueAmount;
                    TotalLabel.Text = "Rs." + totalAmount.ToString();


                    query = "Delete from sold where  mid='" + valueid + "' OR mname='" + validname + "' AND numunit='" + noofunit + "'";
                    fn.setData(query, "Deleted");
                }
                UC_P_SellMedicine_Load(this, null); 
            }
            else
            {
                try
                {
                    guna2DataGridView1.Rows.RemoveAt(this.guna2DataGridView1.SelectedRows[0].Index);
                }
                catch
                {


                }
                finally
                {
                    try
                    {
                        query = "Select quantity from medic where  mname='" + valuename + "' ";
                        ds = fn.getData(query);
                        quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                        newQuantity = quantity + noofunit;

                        query = "update medic set quantity ='" + newQuantity + "' where  mname='"+ valuename+"' ";
                        fn.setData(query, "Medicine Removed from Cart");
                        totalAmount = totalAmount - valueAmount;
                        TotalLabel.Text = "Rs." + totalAmount.ToString();

                        query = "Delete from smedic where mname='" + validname + "' AND numunit='" + noofunit + "'";
                        fn.setData(query, "Deleted");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Select Row","Information",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                   
                }
                UC_P_SellMedicine_Load(this, null);
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                valueAmount = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                valueid = guna2DataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                valuename = guna2DataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                noofunit = Int64.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());

            }
            catch
            {

            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //printing
            printPreviewDialog1.Document = printDocument1;
            //papersize


            int d1 = guna2DataGridView1.Rows.Count;
            if (d1 > 0 && d1 <= 5)
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 300);
                printPreviewDialog1.ShowDialog();
                printDocument1.Print();
            }
            else if (d1 >= 5 && d1 <= 10)
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 500);
                printPreviewDialog1.ShowDialog();
                printDocument1.Print();
            }
            else if (d1 >= 10 && d1 <= 20)
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 750);
                printPreviewDialog1.ShowDialog();
                printDocument1.Print();
            }
            else if (d1 >= 20 && d1 <= 30)
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 850);
                printPreviewDialog1.ShowDialog();
                printDocument1.Print();
            }
            else if (d1 >= 30 && d1 <= 40)
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 1000);
                printPreviewDialog1.ShowDialog();
                printDocument1.Print();
            }
            //clear
            txtsubtotal.Clear();
            txtBalance.Clear();
            guna2DataGridView1.Rows.Clear();
            txtinvoice.ResetText();
        }
        
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-LLTSED3\\SQLEXPRESS;Initial Catalog=Pharmacy;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        string queryone;
        public void invoice()
        {
            try
            {
                queryone = "select MAX(invoiceno) from sold";
                cmd = new SqlCommand(queryone, con);
                con.Open();
                var maxid = cmd.ExecuteScalar() as string;
                if (maxid == null)
                {
                    txtinvoice.Text = "G-000001"; 
                }
                else
                {
                    int intval = int.Parse(maxid.Substring(2,6));
                    intval++;
                    txtinvoice.Text = string.Format("G-{0:000000}", intval);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invoice Number Error");
              
            }

        }
        
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graph = e.Graphics;
            Font myfont = new Font("Arial", 7, FontStyle.Bold);
            string hell = "____________________________________________________________________________________________________________";
            int my_x = 0;
            int my_y = 0;
            int my_newline = 0;
            StringFormat formt3 = new StringFormat();
            SolidBrush colr = new SolidBrush(Color.Black);
            StringFormat formt0 = new StringFormat();
            formt0.LineAlignment = StringAlignment.Center;
            formt0.Alignment = StringAlignment.Center;
            formt3 = new StringFormat(StringFormatFlags.DirectionVertical);
            //graph.Drawstring()
            //printing
            //heading
            graph.DrawString("Gulshan Medical Store".ToUpper(), new Font("Arial",13,FontStyle.Bold),colr,20,my_y+10);
            //adress
            graph.DrawString("Gulshan Medical Store(shop No 23-B),Near evershine Appartments,", new Font("Arial", 5, FontStyle.Regular), colr,35, my_y+ 33);
            graph.DrawString("Block-10 Gulshan-e-Iqbal,Karachi", new Font("Arial", 5, FontStyle.Regular), colr, 75, my_y + 42);
            //contact details
            graph.DrawString("ContactNo# 03022650101 ,03150214696", new Font("Arial", 5, FontStyle.Bold), colr, 65, my_y + 55);
            //line___________________________
            graph.DrawString(hell, new Font("Arial", 8, FontStyle.Regular), colr, my_x, my_y +58);
            //invoice no addtxtinvoice
            //query
             string invoicedate = DateTime.Now.ToString();
           
            //query
            graph.DrawString("Invoice No:", new Font("Arial", 7, FontStyle.Bold), colr, 10, my_y + 73);
            graph.DrawString(txtinvoice.Text, new Font("Arial", 7, FontStyle.Bold), colr, 70, my_y + 73);
            //invoice Date
            graph.DrawString("Invoice Date:", new Font("Arial", 7, FontStyle.Bold), colr, 120, my_y + 73);

            graph.DrawString(invoicedate, new Font("Arial", 7, FontStyle.Bold), colr, 185, my_y + 73);
            //line___________________________
            graph.DrawString(hell, new Font("Arial", 8, FontStyle.Bold), colr, my_x, my_y + 76);
            //heading of grid
            graph.DrawString("Item Name", new Font("Arial", 7, FontStyle.Bold), colr, 10, my_y + 92);
            graph.DrawString("perPrice", new Font("Arial", 7, FontStyle.Bold), colr, 90, my_y + 92);
            graph.DrawString("Qty", new Font("Arial", 7, FontStyle.Bold),colr, 140, my_y + 92);
            graph.DrawString("Total", new Font("Arial", 7, FontStyle.Bold), colr, 175, my_y + 92);
            graph.DrawString("aftDis", new Font("Arial", 7, FontStyle.Bold), colr, 210, my_y + 92);

            graph.DrawString(hell, new Font("Arial", 8, FontStyle.Bold), colr, my_x, my_y + 100);
            for (int i = 0; i < guna2DataGridView1.Rows.Count - 1; i++)
                {

                    string items = (i + 1) + "-" + guna2DataGridView1.Rows[i].Cells[6].Value.ToString();
                    string priceperunit = guna2DataGridView1.Rows[i].Cells[4].Value.ToString();
                    string qty = guna2DataGridView1.Rows[i].Cells[3].Value.ToString();
                    string Total = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
                    string distotal = guna2DataGridView1.Rows[i].Cells[0].Value.ToString();
                    string disc = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
                    string eDate = guna2DataGridView1.Rows[i].Cells[5].Value.ToString();
                    string midorbathnumber = guna2DataGridView1.Rows[i].Cells[7].Value.ToString();
                    string invoino = txtinvoice.Text;
                
                    graph.DrawString(items, new Font("Arial", 7, FontStyle.Regular), colr, 10, my_y + 120);
                    graph.DrawString(priceperunit, myfont, colr, 90, my_y + 120);
                    graph.DrawString(qty, myfont, colr, 140, my_y + 120);
                    graph.DrawString(Total, myfont, colr, 175, my_y + 120);
                    graph.DrawString(distotal, myfont, colr, 210, my_y + 120);

                    my_y = my_y + 20;
                    int afteruse = my_y;

                query = ("insert into sold(invoiceno,mid,mname,eDate,perunit,numunit,total,disc,Afttotal) values ('" + invoino + "','" + midorbathnumber + "','" + items + "','" + eDate + "','" + priceperunit + "','" + qty + "','" + Total + "','" + disc + "','" + distotal + "')");
                fn.datagetiing(query);
            }
            //int cal=;
            int another_y = int.Parse(guna2DataGridView1.Rows.Count.ToString()) * 18;

            ////line___________________________
            graph.DrawString("_________________________________________________________________________________________________", new Font("Arial", 4, FontStyle.Regular), colr, my_x, 110 + another_y);
            
            string subtotal = "Total:" + txtsubtotal.Text;
            graph.DrawString(subtotal, new Font("Arial", 7, FontStyle.Bold), colr, 190, 120+another_y);
            string Paid = "Paid:" + txtPaidAmount.Text;
            graph.DrawString(Paid, new Font("Arial", 7, FontStyle.Bold), colr, 190, 140+another_y);
            string Balance = "Balance:" + txtBalance.Text;
            graph.DrawString(Balance, new Font(Balance, 7, FontStyle.Bold), colr, 190, 160 + another_y);
            graph.DrawString("_________________________________________________________________________________________________________________", new Font("Arial", 8, FontStyle.Regular), colr, my_x, 175 + another_y);
            graph.DrawString("*******************************************************************************************************************", new Font("Arial", 8, FontStyle.Regular), colr, my_x, 185 + another_y);
            graph.DrawString("No Return No Exchange and No Refund", new Font("Arial", 8, FontStyle.Regular), colr, my_x+150, 200 + another_y, formt0);

        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //paid
                string total = txtsubtotal.Text;
                string paid =txtPaidAmount.Text;
                if (txtPaidAmount.Text != null)
                {
                  
                Int64 bal = Int64.Parse(txtPaidAmount.Text) - Int64.Parse(txtsubtotal.Text) ;
                txtBalance.Text = bal.ToString();
                  
                }
                else
                {
                    txtPaidAmount.FillColor = Color.Red;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter Paid Amount","Information",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //Discount
                if (txtDiscount.SelectedIndex == 0)
                {
                    //var t=(total/5);
                    //var discounted=total-t;
                    Int64 total = Int64.Parse(txtTotalPrice.Text);
                    Int64 Dis_cal = total - (total / 5);
                    txtTotalPrice.Text = Dis_cal.ToString();
                     
                }
                else if (txtDiscount.SelectedIndex == 1)
                {
                    Int64 total = Int64.Parse(txtTotalPrice.Text);
                    Int64 Dis_cal = total - (total / 10);
                    txtTotalPrice.Text = Dis_cal.ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Medicines aren't selected");
            }
           
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
           
            if (txtMediID.Text != "" || txtMediName.Text != "" || txtMediID.Text != null || txtMediName.Text != null)
            {
                if (txtNoOfUnit.Text == "" || txtNoOfUnit.Text == null)
                {
                    MessageBox.Show("Enter No-of-Units", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    query = "Select quantity from medic where mid='" + txtMediID.Text + "' OR mname='" + txtMediName + "'";
                    ds = fn.getData(query);
                    quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    newQuantity = quantity - Int64.Parse(txtNoOfUnit.Text);
                    invoice();
                    if (newQuantity >= 0)
                    {
                        n = guna2DataGridView1.Rows.Add();
                        if (txtMediID.Text == "" || txtMediID == null)
                        {
                            guna2DataGridView1.Rows[n].Cells[7].Value = "-Num-";
                        }
                        else
                        {
                            guna2DataGridView1.Rows[n].Cells[7].Value = txtMediID.Text;
                        }
                        guna2DataGridView1.Rows[n].Cells[6].Value = txtMediName.Text;
                        guna2DataGridView1.Rows[n].Cells[5].Value = txtExpiry.Text;
                        guna2DataGridView1.Rows[n].Cells[4].Value = txtPricePerUnit.Text;
                        guna2DataGridView1.Rows[n].Cells[3].Value = txtNoOfUnit.Text;
                        guna2DataGridView1.Rows[n].Cells[2].Value = txtOriTotal.Text;
                        guna2DataGridView1.Rows[n].Cells[0].Value = txtTotalPrice.Text;


                        if (txtDiscount.SelectedIndex == 0)
                        {
                            guna2DataGridView1.Rows[n].Cells[1].Value = "5%";
                            

                        }
                        else if (txtDiscount.SelectedIndex == 1)
                        {
                            guna2DataGridView1.Rows[n].Cells[1].Value = "10%";
                           
                        }
                        //Total in gridview:
                        totalAmount = totalAmount + int.Parse(txtTotalPrice.Text);
                        TotalLabel.Text = "Rs." + totalAmount.ToString();
                        txtsubtotal.Text = totalAmount.ToString();
                        //Update Database
                        query = "Update medic set quantity='" + newQuantity + "' where mid='" + txtMediID.Text + "'";
                        fn.setData(query, "Medicine Added.");
                        
                        
                      
                    }
                    else
                    {
                        MessageBox.Show("Medicine is Out of Stock.\n Only" + quantity + "left", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    clearAll();
                    UC_P_SellMedicine_Load(this, null);

                }

            }


            
            
        }
        private void clearAll()
        {
            txtMediID.Clear();
            txtMediName.Clear();
            txtExpiry.ResetText();
            txtPricePerUnit.Clear();
            txtNoOfUnit.Clear();
            txtDiscount.ResetText();
            labelProfit.ResetText();

        }


    }
}
