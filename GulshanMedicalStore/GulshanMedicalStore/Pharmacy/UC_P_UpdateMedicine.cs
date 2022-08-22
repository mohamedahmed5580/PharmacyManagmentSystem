using System;
using System.Data;
using System.Windows.Forms;

namespace GulshanMedicalStore.Pharmacy
{
    public partial class UC_P_UpdateMedicine : UserControl
    {
        Function fn = new Function();
        string query;

        public UC_P_UpdateMedicine()
        {
            InitializeComponent();
        }

        DataSet ds;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMediID.Text != "")
                {
                    query = "Select * from medic where mid='" + txtMediID.Text + "'";
                    ds = fn.getData(query);
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        txtMediName.Text = ds.Tables[0].Rows[0][3].ToString();
                        txtMediNumber.Text = ds.Tables[0].Rows[0][4].ToString();
                        txtManifacture.Text = ds.Tables[0].Rows[0][5].ToString();
                        txtExpiry.Text = ds.Tables[0].Rows[0][6].ToString();
                        txtAvaliableQuantity.Text = ds.Tables[0].Rows[0][7].ToString();
                        txtPricePerUnit.Text = ds.Tables[0].Rows[0][8].ToString();
                    }
                    else
                    {

                        MessageBox.Show("No Medicine Found With ID : " + txtMediID.Text + "", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else if (txtMediID.Text == "" && txtMediName.Text != "")
                {
                    try
                    {
                        query = "Select * from medic where mname='" + txtMediName.Text + "'";
                        ds = fn.getData(query);
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            txtMediNumber.Text = ds.Tables[0].Rows[0][4].ToString();
                            txtManifacture.Text = ds.Tables[0].Rows[0][5].ToString();
                            txtExpiry.Text = ds.Tables[0].Rows[0][6].ToString();
                            txtAvaliableQuantity.Text = ds.Tables[0].Rows[0][7].ToString();
                            txtPricePerUnit.Text = ds.Tables[0].Rows[0][8].ToString();
                        }

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("No Medicine Found With ID : " + txtMediID.Text + "", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }

                else
                {

                    clearAll();
                }

            }
            catch (Exception)
            {

                MessageBox.Show("There are Two Same Batch Number");
            }
             }
        private void clearAll()
        {
            
            txtMediID.Clear();
            txtMediName.Clear();
            txtMediNumber.Clear();
            txtManifacture.ResetText();
            txtExpiry.ResetText();
            txtAvaliableQuantity.Clear();
            txtPricePerUnit.Clear() ;
            if (txtAddQuantity.Text!="0")
            {
                txtAddQuantity.Text = "0";
            }
            else
            {
                txtAddQuantity.Text = "0";
            }
        }
        Int64 TotalQuantity;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string mname = txtMediName.Text;
            string mnumber = txtMediNumber.Text;
            string mDate = txtManifacture.Text;
            string eDate = txtExpiry.Text;
            Int64 quantity = Int64.Parse(txtAvaliableQuantity.Text);
            Int64 addQuantity = Int64.Parse(txtAddQuantity.Text);
            Int64 perunit = Int64.Parse(txtPricePerUnit.Text);

            TotalQuantity = quantity + addQuantity;
            //mid,mname,mnumber,mDate,eDate,quantity,perunit
            query = "update medic set mname='"+mname+"',mnumber= '"+mnumber+ "',mDate='"+mDate+ "',eDate='"+eDate+ "',quantity='"+TotalQuantity+ "',perunit='"+perunit+ "' where mid='"+txtMediID.Text+"'";
            fn.setData(query, "Information Updated!");
        }

       
        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();
        }

       
    }
}
