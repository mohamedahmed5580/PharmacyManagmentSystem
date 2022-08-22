using System;
using System.Windows.Forms;

namespace GulshanMedicalStore.Pharmacy
{
    public partial class UC_P_AddMedicine : UserControl
    {
        Function fn = new Function();
        string query;
        public UC_P_AddMedicine()
        {
            InitializeComponent();
        }

       

        private void UC_P_AddMedicine_Load(object sender, EventArgs e)
        {

        }

      

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if ( txtInvoicenum.Text != "" && txtBatchNo.Text != "" && txtMediName.Text != "" && txtMediNumber.Text != "" && txtQuantity.Text != "" && txtPricePerUnitR.Text != "")
                {

                    string invoice = txtInvoicenum.Text;
                    string mid = txtBatchNo.Text;
                    string mname = txtMediName.Text;
                    string mdistri = txtMediNumber.Text;
                    string mDate = txtManifacture.Text;
                    string eDate = txtExpiry.Text;
                    Int64 quantity = Int64.Parse(txtQuantity.Text);
                    Int64 perunit = Int64.Parse(txtPricePerUnitR.Text);
                    Int64 Tradeperunit = Int64.Parse(txtPricePerUnitT.Text);


                    if (Tradeperunit <= perunit && Tradeperunit>0){
                        //query for inserting all data into sql server;
                        query = "insert into medic(invoice,mid,mname,mnumber,mDate,eDate,quantity,perunit,tradeunit) values ('" + invoice + "','" + mid + "','" + mname + "','" + mdistri + "','" + mDate + "','" + eDate + "','" + quantity + "','" + perunit + "','" + Tradeperunit + "')";
                        fn.setData(query, "Medicine Added to Database");

                    }
                    else
                    {
                        MessageBox.Show("Trade Price is Greater", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                  
                

                }
                else if (txtInvoicenum.Text == "" && txtBatchNo.Text == ""  &&  txtMediName.Text != "" && txtMediNumber.Text != "" && txtQuantity.Text != "" && txtPricePerUnitR.Text != "")
                {

                    string invoice = txtInvoicenum.Text;
                    string mid = txtBatchNo.Text;
                    string mname = txtMediName.Text;
                    string mdistri = txtMediNumber.Text;
                    string mDate = txtManifacture.Text;
                    string eDate = txtExpiry.Text;
                    Int64 quantity = Int64.Parse(txtQuantity.Text);
                    Int64 perunit = Int64.Parse(txtPricePerUnitR.Text);
                    Int64 Tradeperunit = Int64.Parse(txtPricePerUnitT.Text);

                    //buyer should contain profit always
                    if (Tradeperunit <= perunit && Tradeperunit > 0)
                    {
                        invoice = "Market";
                        //query for inserting all data into sql server;
                        query = "insert into medic(invoice,mid,mname,mnumber,mDate,eDate,quantity,perunit,tradeunit) values ('" + invoice + "','" + mid + "','" + mname + "','" + mdistri + "','" + mDate + "','" + eDate + "','" + quantity + "','" + perunit + "','" + Tradeperunit + "')";
                        fn.setData(query, "Medicine Added to Database");
                      
                    }
                    else
                    {
                        MessageBox.Show("Trade Price is Greater", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Enter All Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Incorrect Format","Mild Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }
        public void clearAll()
        {
            txtInvoicenum.Clear();
            txtBatchNo.Clear();
            txtMediName.Clear();
            txtMediNumber.Clear();
            txtQuantity.Clear();
            txtPricePerUnitR.Clear();
            txtManifacture.ResetText();
            txtManifacture.ResetText();
            ProfPerUnit.ResetText();
            ProfStock.ResetText();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();
        }

     
        private void btnProf_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtPricePerUnitR.Text != " " || txtPricePerUnitT.Text != " " || txtPricePerUnitR.Text != null || txtPricePerUnitT.Text != null)
                {

                    Int64 perunit = Int64.Parse(txtPricePerUnitR.Text);
                    Int64 Tradeperunit = Int64.Parse(txtPricePerUnitT.Text);
                    ProfPerUnit.Text ="PKR"+(perunit - Tradeperunit).ToString()+"/-";
                    //for stock
                    Int64 quantity = Int64.Parse(txtQuantity.Text);
                    ProfStock.Text = "PKR"+((quantity * perunit) - (quantity * Tradeperunit)).ToString()+ "/-";
                }
                else
                {
                    MessageBox.Show("Enter Stuff Properly", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Quantity isn't there", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
