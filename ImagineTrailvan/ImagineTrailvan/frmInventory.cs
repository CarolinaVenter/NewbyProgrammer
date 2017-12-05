using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;

namespace ImagineTrailvan
{
    public partial class frmInventory : Form
    {
        public string[] fieldInv = { "InventoryID", "InvCode", "InvItem", "InvDescription", "InvCategory", "InvReorderLevel", "InvMarkup", "InvIsDeleted" };
        public string[] fieldInvAll = { "InventoryID", "InvCode", "InvItem", "InvDescription", "InvSupplierDescription", "InvCategory", "InvReorderLevel", "InvMarkup", "InvIsDeleted" };
        public string[] fieldSup = { "SupplierID", "SupName", "SupContactPerson", "SupREP", "SupBusinessNr", "SupCellNr", "SupEmail", "SupAddress", "SupCity", "SupProvince", "SupPrefix", "SupReorderTime", "SupPaymentTerm", "SupIsDeleted" };
        public string[] fieldStockOut = { "SubStockOUTID", "InventoryID", "SSOQuantityOut", "SSODateOut", "InvIsAccountedFor" };
        public string[] fieldStockIN = { "SubStockINID", "InventoryID", "SSIQuantityIN", "SSIPrice", "ISIID", "SSIStockLeft" };
        public string[] fieldInvoiceStockIN = { "ISIID", "ISIInvoiceNo", "ISIDateReceived", "SupplierID", "ISIInvoiceTotalIncl" };
        public string[] fieldTotalStock = { "InventoryStockID", "InventoryID", "ISTotalStock" };
        public string[] fieldOrders = { "OrdersID", "OrderNumber", "SupplierID", "OrdersDate" };
        public string[] fieldSubOrders = { "SubOrdersID", "InventoryID", "SOOrderedQuantity", "SOPrice", "OrdersID", "SOLength" };
        public int emptySupplier;

        public frmInventory()
        {
            InitializeComponent();
        }//end of public frmInventory()

        private void frmInventory_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supplierDataSet.Supplier' table. You can move, or remove it, as needed.
            this.supplierTableAdapter.Fill(this.supplierDataSet.Supplier);
           
            try
            {
                DataAccess datac = new DataAccess();
                DataTable dtDisplayAllInvoices = new DataTable();

                dtgInventory.DataSource = datac.getTable("Inventory");
                dtgSuppliers.DataSource = datac.getTable("Supplier");
                dtgStockIn.DataSource = datac.getAllInvoice();

                dtgCheckStock.DataSource = datac.getTable("Inventory");
                dtgInventoryValue.DataSource = datac.getInventoryValue();
                dtgOrderInvList.DataSource = datac.getLowStock();
                dtgSupOrderHistory.DataSource = datac.getAllOrderHistory();
                dtgSupOrderList.DataSource = datac.getLowStock();

                tabStockOUT.Controls.Add(dtgInventory);
                tabSuppliers.Controls.Add(dtgSuppliers);
                tabStockIN.Controls.Add(dtgStockIn);
                tabInventoryValue.Controls.Add(dtgInventoryValue);
                tabStockCheck.Controls.Add(dtgCheckStock);
                tabLowStock.Controls.Add(dtgSupOrderHistory);
                tabLowStock.Controls.Add(dtgSupOrderList);
                tabLowStock.Controls.Add(dtgOrderInvList);
                
                btnClear.Enabled = true;
                btnSearch.Enabled = true;
                btnStockUpdate.Enabled = false;
                btnISIdelItemInvoice.Enabled = false;
                btnISIinvDeleteItem.Enabled = false;                          
                btnISIpreviewCurrent.Enabled = false;
                btnISIrecordNewInvoice.Enabled = false;
                btnISIsave.Enabled = false;
                btnISIclear.Enabled = true;

                btnSupINSERT.Enabled = true;
                btnSupSearch.Enabled = true;

                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;

                dtpDateStockOUT.Value = DateTime.Now;
                dtpInvoiceDate.Value = DateTime.Now;

                //tabSupplierSummary.Controls.Add(dtgSupplierSearch);
                //tabSupplierSummary.Controls.Add(dtgInventorySummary);
                //tabSupplierSummary.Controls.Add(txtSupSummaryValue);
                //tabLowStock.Controls.Add(dtgOrderSupSearch);
                //tabLowStock.Controls.Add(dtgSupInvList);
                //tabLowStock.Controls.Add(dtgOrderInvList);

            //to calculate total value of stock based on quantity and price on inventoryValue tab****
                double totalStockValue = 0;

                for (int i = 0; i < dtgInventoryValue.Rows.Count - 1; i++)
                {
                    totalStockValue += (int.Parse(dtgInventoryValue.Rows[i].Cells[4].Value.ToString())*double.Parse(dtgInventoryValue.Rows[i].Cells[5].Value.ToString()));
                }//end of for (int i = 0; i < fieldInv.Length; i++)
                txtStockValue.Text = totalStockValue.ToString();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tab: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void frmInventory_Load(object sender, EventArgs e)

        private void frmInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 obj = new Form1();
            this.Hide();
            obj.ShowDialog();
            Environment.Exit(0);
        }//end of private void frmInventory_FormClosing(object sender, FormClosingEventArgs e)

        #region tabStockOUT Controls
        private void tabStock_Click(object sender, EventArgs e)
        {}//end of private void tabStock_Click(object sender, EventArgs e)

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInvCode.Text != "")
                {
                    DataAccess datac = new DataAccess();                     
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvCode" };
                    
                    values.Add(txtInvCode.Text);  
                    dtgInventory.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvCode.Text!=null)
                else if (txtInvItem.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvItem" };

                    values.Add(txtInvItem.Text.ToUpper());
                    dtgInventory.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvItem.Text!=null)
                else if (txtInvDesc.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvDescription" };

                    values.Add(txtInvDesc.Text.ToUpper());
                    dtgInventory.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvDesc.Text!=null)
                else if (txtInvCat.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvCategory" };

                    values.Add(txtInvCat.Text.ToUpper());
                    dtgInventory.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvDesc.Text!=null)
                else //if all is still empty, then do this. 
                {
                    MessageBox.Show("Please enter value to search for", "Search failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvID.Clear();
                    txtInvCode.Clear();
                    txtInvItem.Clear();
                    txtInvDesc.Clear();
                    txtInvCat.Clear();
                    txtInvPrice.Clear();
                    txtInvReLevel.Clear();
                    txtInvTotalStock.Clear();
                    txtInvStockOut.Clear();
                    txtSellPrice.Clear();       //clear all textboxes 
                }//end of else
                
                btnClear.Enabled = true;
                btnStockUpdate.Enabled = false;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSearch_Click(object sender, EventArgs e)

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                btnStockUpdate.Enabled = false;

                DataAccess datac = new DataAccess();
                dtgInventory.DataSource = datac.getTable("Inventory");        //change dtgInventory back to original data in table

                txtInvID.Clear();
                txtInvCode.Clear();
                txtInvItem.Clear();
                txtInvDesc.Clear();
                txtInvCat.Clear();
                txtInvPrice.Clear();
                txtInvReLevel.Clear();
                txtInvTotalStock.Clear();
                txtInvStockOut.Clear();
                txtSellPrice.Clear();       //clear all textboxes 
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Search Clear Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)            

        }//end of private void btnClear_Click(object sender, EventArgs e)

        private void dtgInventory_Click(object sender, EventArgs e)
        {
            try
            {
                txtInvID.Text = dtgInventory.SelectedRows[0].Cells[0].Value.ToString();
                txtInvCode.Text = dtgInventory.SelectedRows[0].Cells[1].Value.ToString();
                txtInvItem.Text = dtgInventory.SelectedRows[0].Cells[2].Value.ToString().ToUpper();
                txtInvDesc.Text = dtgInventory.SelectedRows[0].Cells[3].Value.ToString().ToUpper();
                txtInvCat.Text = dtgInventory.SelectedRows[0].Cells[5].Value.ToString().ToUpper();
                txtInvReLevel.Text = dtgInventory.SelectedRows[0].Cells[6].Value.ToString();
                cmbInvMarkup.Text = dtgInventory.SelectedRows[0].Cells[7].Value.ToString(); //add a string Collection

                txtInvStockOut.Clear();
                btnClear.Enabled = true;
                btnStockUpdate.Enabled = true;
                btnSearch.Enabled = false;
                /*   DataTable dt = new DataTable();
   dt.Clear();
   dt.Columns.Add("Name");
   dt.Columns.Add("Marks");            example found to create and pass an array of values in a datatable
   object[] o = { "Ravi", 500 };
   dt.Rows.Add(o);
   dt.Rows.Add(new object[] { "Ravi", 500 }); */

                DataAccess datac = new DataAccess();
                //****************Get the stock total quantity from InventoryStock table, relevant to the InventoryID******************
                DataTable dtTotalStock = new DataTable();               
                string [] fieldTotal={"InventoryID"};
                ArrayList getIDValue=new ArrayList();
                getIDValue.Add("="+txtInvID.Text);

                dtTotalStock = datac.getMathRecord("InventoryStock", fieldTotal, getIDValue);
                if (dtTotalStock.Rows[0] != null)
                {
                    txtInvTotalStock.Text = dtTotalStock.Rows[0][2].ToString();
                }//end of if (dtStockIn.Rows[0] != null)
                else
                {
                    MessageBox.Show("no rows in datatable: ");
                } //end of else      

                //**************Get the oldest date's stock price via the InvoiceStockIN table's date and the InventoryID found in the SubStockIN table*********
                DataTable dtStockIn = new DataTable();
                dtStockIn = datac.getFIFODatedPrice(txtInvID.Text);
                if (dtStockIn.Rows[0] != null)
                {
                    txtInvPrice.Text = dtStockIn.Rows[0][4].ToString();
                }//end of if (dtStockIn.Rows[0] != null)
                else
                {
                    MessageBox.Show("No rows in datatable: "); 
                } //end of else              
             
                //to calculate the selling price (price + markup excl VAT)***
                //   txtSellPrice.Text = (double.Parse(txtInvPrice.Text) * ((100 + (int.Parse(cmbInvMarkup.ValueMember))) / 100)).ToString(); //tried but failed
                double price = 0;
                double markup = 0;
                double sellPrice = 0;
                price = double.Parse(txtInvPrice.Text);
                markup = int.Parse(cmbInvMarkup.Text);
                sellPrice = price * ((100 + markup) / 100);
                txtSellPrice.Text = sellPrice.ToString();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void dtgInventory_Click(object sender, EventArgs e)

        private void btnInvUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                btnClear.Enabled = false;
                btnStockUpdate.Enabled = false;

                //**********SEND INVENTORY DETAILS TO BE UPDATED**********
                //this is done even if no stock is taken out......
                DataAccess datac = new DataAccess();
                ArrayList invValues = new ArrayList(); //make arrayList to store all values of current record
                invValues.Add(txtInvID.Text);          //store textBox / comboBox value in the ArrayList
                invValues.Add(txtInvCode.Text);
                invValues.Add(txtInvItem.Text.ToUpper());
                invValues.Add(txtInvDesc.Text.ToUpper());
                invValues.Add(txtInvCat.Text.ToUpper());
                invValues.Add(txtInvReLevel.Text);
                invValues.Add(cmbInvMarkup.Text);
                invValues.Add("false");

                datac.updateRecCmd("Inventory", fieldInv[0], txtInvID.Text, fieldInv, invValues);        //Send values in fieldInv string format from textBox/ comboBox through updateCmd query to database table using InvID as key            
                dtgInventory.DataSource = datac.getTable("Inventory");

                //*********SEND INSERT TO STOCK OUT TABLE**************
                if (txtInvStockOut.Text!="")
                {
                    if (dtpDateStockOUT.Text!="")
                    {
                        DataTable dtStockOut = new DataTable();
                        ArrayList stockOutValues = new ArrayList();
                        stockOutValues.Add(0);
                        stockOutValues.Add(txtInvID.Text);
                        stockOutValues.Add(txtInvStockOut.Text);
                        stockOutValues.Add(dtpDateStockOUT.Value);
                        stockOutValues.Add("true");
                        datac.insertCmd("SubStockOUT", fieldStockOut, stockOutValues);

                        //*********SEND update TO TOTALSTOCK TABLE**************
                        DataTable dtTotalStock = new DataTable();
                        ArrayList totalValues = new ArrayList();
                        txtInvTotalStock.Text = (int.Parse(txtInvTotalStock.Text) - int.Parse(txtInvStockOut.Text)).ToString();
                        totalValues.Add(0);
                        totalValues.Add(txtInvID.Text);
                        totalValues.Add(txtInvTotalStock.Text);
                        datac.updateRecCmd("InventoryStock", fieldTotalStock[1], txtInvID.Text, fieldTotalStock, totalValues);
                   
                        //*******Minus quantity-out from First stock in LEFT from table SubStockIN, while using oldest date in InvoiceStockIN********    

                        // #1) lookup oldest date in the InvoiceStockIN table, with get query.
                        //#2) minus left until counter (stockOUT quantity is used for counter) is 0
                        //#3) if left is smaller than counter (thus make if statement), jump to next date and left
                        //#4) keep minussing until counter is 0 and then update to SubStockIN
                       
                        DataTable dtSubStockIN = new DataTable();
                        DataTable dtStockIn = new DataTable();
                        dtSubStockIN = datac.getFIFODatedPrice(txtInvID.Text);
                        ArrayList subStockIN = new ArrayList();
                    
                        for (int i = 0; i < int.Parse(txtInvStockOut.Text); i++)
                        {
                            if (int.Parse(dtSubStockIN.Rows[0][5].ToString())>0)
                            {
                                subStockIN.Add(dtSubStockIN.Rows[0][0].ToString());
                                subStockIN.Add(dtSubStockIN.Rows[0][1].ToString());
                                subStockIN.Add(dtSubStockIN.Rows[0][3].ToString());
                                subStockIN.Add(dtSubStockIN.Rows[0][4].ToString());
                                subStockIN.Add(dtSubStockIN.Rows[0][5].ToString());//invoice number
                                subStockIN.Add((int.Parse(dtSubStockIN.Rows[0][6].ToString())-1));
                                datac.updateRecCmd("SubStockIN", fieldStockIN[0],dtSubStockIN.Rows[0][0].ToString(),fieldStockIN,subStockIN);
                                subStockIN = new ArrayList();//basically clearing this arrayList
                                dtSubStockIN = datac.getFIFODatedPrice(txtInvID.Text);//get the record again after the update
                            }//end of if (int.Parse(dtSubStockIN.Rows[0][5].ToString())>0)
                            else
                            {//assuming when it jumps to the else, it should probably minus again before it exits the else and runs the next counter which then continues to the next if
                                dtSubStockIN = datac.getFIFODatedPrice(txtInvID.Text);
                                subStockIN.Add(dtSubStockIN.Rows[0][0].ToString());
                                subStockIN.Add(dtSubStockIN.Rows[0][1].ToString());
                                subStockIN.Add(dtSubStockIN.Rows[0][3].ToString());
                                subStockIN.Add(dtSubStockIN.Rows[0][4].ToString());
                                subStockIN.Add(dtSubStockIN.Rows[0][5].ToString());//invoice number
                                subStockIN.Add(int.Parse(dtSubStockIN.Rows[0][6].ToString()) - 1);
                                datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtSubStockIN.Rows[0][0].ToString(), fieldStockIN, subStockIN);
                                subStockIN = new ArrayList();//basically clearing this arrayList
                                dtSubStockIN = datac.getFIFODatedPrice(txtInvID.Text);//get the record again after the update
                            //not very sure how to continue from here
                            }//end of else
                        }//end of for (int i = 0; i < int.Parse(txtInvStockOut.Text); i++)
                        dtStockIn = datac.getFIFODatedPrice(txtInvID.Text);
                        if (dtStockIn.Rows[0] != null)
                        {
                            txtInvPrice.Text = dtStockIn.Rows[0][4].ToString();
                        }//end of if (dtStockIn.Rows[0] != null)
                        else
                        {
                            MessageBox.Show("No rows in datatable: ");
                        } //end of else 
                    }//end of if((txtInvStockOut.Text!="")
                    else
                    {
                        MessageBox.Show("Please add a date to this transaction.");
                    }//end of else, if (dtpDateStockOUT.Text!="")
                }//end of if (txtInvStockOut.Text!="")

                dtgInventory.DataSource = datac.getTable("Inventory");
                btnClear.Enabled = true;
                btnStockUpdate.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnInvUpdate_Click(object sender, EventArgs e)
        #endregion

        #region tabStockIN
        private void btnISIsave_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccess datac = new DataAccess();
                ArrayList invoiceValues = new ArrayList();
                ArrayList totalStockChanged = new ArrayList();
                ArrayList stockINValues = new ArrayList();
                DataTable dtInvoiceIN = new DataTable();
                DataTable dtAllInvoices = new DataTable();
                ArrayList getIDValue = new ArrayList();
                string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"
                Boolean exists = new Boolean();

                //update any inventory details
                ArrayList invValues = new ArrayList(); //make arrayList to store all values of current record
                invValues.Add(txtISIinvID.Text);          //store textBox / comboBox value in the ArrayList
                invValues.Add(txtISIinvCode.Text);
                invValues.Add(txtISIinvItem.Text.ToUpper());
                invValues.Add(txtISIinvDescription.Text.ToUpper());
                invValues.Add(txtISIinvCategory.Text.ToUpper());
                invValues.Add(txtISIinvReLevel.Text);
                invValues.Add(cmbISIinvMarkup.Text);
                invValues.Add("false");

                datac.updateRecCmd("Inventory", fieldInv[0], txtISIinvID.Text, fieldInv, invValues);        //Send values in fieldInv string format from textBox/ comboBox through updateCmd query to database table using InvID as key            
                dtgStockIn.DataSource = datac.getTable("Inventory");

                if (txtInvoiceNo.Text != "")//this if makes it invoice based, invoice reference made-else is for recirculation
                {
                    if (dtpInvoiceDate.Text != "")
                    {
                        dtAllInvoices = datac.getTable("InvoiceStockIN");

                        for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
                        {
                            if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())//&& dtAllInvoices.Rows[i][4].ToString() == txtISITotal.Text
                            {
                                exists = true;
                            }//end of if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())
                            else
                            {
                                exists = false;
                            }//end of else, if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())
                        }//end of for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
                     //   MessageBox.Show("Exists value: " + exists, "Testing complete");

                        if (exists == true)
                        {
                            //make fancy pop-up to state that invoice exists, want to add to invoice?
                            getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
                            // getIDValue.Add("='" + dtpInvoiceDate.Text+"00:00:00.000'");
                            getIDValue.Add("=" + cmbISISupplier.SelectedValue);
                            // getIDValue.Add("=" + txtISITotal.Text + "00");

                            dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

                            stockINValues.Add(0);
                            stockINValues.Add(txtISIinvID.Text);
                            stockINValues.Add(txtISIstockReceived.Text);
                            stockINValues.Add(txtISIstockPrice.Text);
                            stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
                            stockINValues.Add(txtISIstockReceived.Text);
                            datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted

                            totalStockChanged.Add(0);
                            totalStockChanged.Add(txtISIinvID.Text);
                            totalStockChanged.Add((int.Parse(txtISIstockReceived.Text) + int.Parse(txtISIstockTotal.Text)));
                            datac.updateRecCmd("InventoryStock", fieldTotalStock[0].ToString(), txtISIinvID.Text, fieldTotalStock, totalStockChanged);//string tblName, string idField, string ID, string[] fields, ArrayList values

                            // dtgStockIn.DataSource = datac.getAllInvoice();
                            dtgStockIn.DataSource = datac.getStockInInvoice(dtInvoiceIN.Rows[0][0].ToString());
                            
                        }//end of if (exists = true)
                        else
                        {//make new invoice thingy
                            invoiceValues.Add(0);
                            invoiceValues.Add(txtInvoiceNo.Text);
                            invoiceValues.Add(dtpInvoiceDate.Text);
                            invoiceValues.Add(cmbISISupplier.SelectedValue);
                            invoiceValues.Add(txtISITotal.Text);
                            datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, invoiceValues);

                            getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
                            getIDValue.Add("=" + cmbISISupplier.SelectedValue);
                            dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

                            stockINValues.Add(0);
                            stockINValues.Add(txtISIinvID.Text);
                            stockINValues.Add(txtISIstockReceived.Text);
                            stockINValues.Add(txtISIstockPrice.Text);
                            stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
                            stockINValues.Add(txtISIstockReceived.Text);
                            datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted
                            dtgStockIn.DataSource = datac.getStockInInvoice(dtInvoiceIN.Rows[0][0].ToString());

                            totalStockChanged.Add(0);
                            totalStockChanged.Add(txtISIinvID.Text);
                            totalStockChanged.Add((int.Parse(txtISIstockReceived.Text) + int.Parse(txtISIstockTotal.Text)));
                            datac.updateRecCmd("InventoryStock", fieldTotalStock[0].ToString(), txtISIinvID.Text, fieldTotalStock, totalStockChanged);//string tblName, string idField, string ID, string[] fields, ArrayList values
                        }//end of else
                    }//end of if (dtpInvoiceDate.Text!="")
                    else
                    {
                        MessageBox.Show("Please enter a date value in the Invoice Date field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }//end of else if (dtpInvoiceDate.Text!="")
                }//end of if (txtISIinvoiceNr.Text != "")
                else
                {
                    //no reference to invoice is made - stock can go back into circulation here
                    //therefore, it runs an update, NOT an insert
                    dtInvoiceIN = datac.getFIFODatedPrice(txtISIinvID.Text);

                    totalStockChanged.Add(0);
                    totalStockChanged.Add(txtISIinvID.Text);
                    totalStockChanged.Add((int.Parse(txtISIstockReceived.Text) + int.Parse(txtISIstockTotal.Text)));
                    datac.updateRecCmd("InventoryStock", fieldTotalStock[1].ToString(), txtISIinvID.Text, fieldTotalStock, totalStockChanged);//changing totalStock
                    for (int i = 0; i < int.Parse(txtISIstockReceived.Text) - 1; i++)
                    {
                        stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());
                        stockINValues.Add(dtInvoiceIN.Rows[0][1].ToString());
                        stockINValues.Add(dtInvoiceIN.Rows[0][3].ToString());
                        stockINValues.Add(dtInvoiceIN.Rows[0][4].ToString());
                        stockINValues.Add(dtInvoiceIN.Rows[0][5].ToString());
                        stockINValues.Add(int.Parse(dtInvoiceIN.Rows[0][6].ToString()) + 1);
                        datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtInvoiceIN.Rows[0][0].ToString(), fieldStockIN, stockINValues);

                        dtgStockIn.DataSource = datac.getAllInvoice();
                        dtInvoiceIN = datac.getFIFODatedPrice(txtISIinvID.Text);
                        stockINValues = new ArrayList();
                    }//end of for (int i = 0; i < int.Parse(txtISIquantityIn.Text-1); i++)    
                    string getID = dtInvoiceIN.Rows[0][5].ToString();
                    //dtgStockIn.DataSource = datac.getCurrentInvoice(getID);
                }//end of else
                txtISIinvID.Clear();        //clear txtboxes for next search/ item or whichever
                txtISIinvCode.Clear();
                txtISIinvItem.Clear();
                txtISIinvDescription.Clear();
                txtISIinvCategory.Clear();
                txtISIstockReceived.Clear();
                txtISIstockPrice.Clear();
                txtISIstockTotal.Clear();

                btnISIclear.Enabled = true;
                btnISIinvDeleteItem.Enabled = true;
                btnISIrecordNewInvoice.Enabled = true;
                btnISIsave.Enabled = true;
                btnISIpreviewCurrent.Enabled = true;
                btnISIdelItemInvoice.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error saving invoice details : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch
        }//end of private void btnISIsave_Click(object sender, EventArgs e) 

        private void dtgStockIn_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccess datac = new DataAccess();
             //   if (dtgStockIn.DataSource.Equals(datac.getAllInvoice()))
             //   {
             //       MessageBox.Show("Click <clear> to reload stock details in the grid", "Grid click unsuccessful ", MessageBoxButtons.OK, MessageBoxIcon.Information);
             //   }
            //    else if (dtgStockIn.DataSource.Equals(datac.getTable("Inventory")))
            //    {
                    txtISIinvID.Text = dtgStockIn.SelectedRows[0].Cells[0].Value.ToString();
                    txtISIinvCode.Text = dtgStockIn.SelectedRows[0].Cells[1].Value.ToString();
                    txtISIinvItem.Text = dtgStockIn.SelectedRows[0].Cells[2].Value.ToString().ToUpper();
                    txtISIinvDescription.Text = dtgStockIn.SelectedRows[0].Cells[3].Value.ToString().ToUpper();
                    txtISIinvCategory.Text = dtgStockIn.SelectedRows[0].Cells[5].Value.ToString().ToUpper();
                    txtISIinvReLevel.Text = dtgStockIn.SelectedRows[0].Cells[6].Value.ToString();
                    cmbISIinvMarkup.Text = dtgStockIn.SelectedRows[0].Cells[7].Value.ToString(); //add a string Collection

                    //****************Get the stock total quantity from InventoryStock table, relevant to the InventoryID******************
                    DataTable dtStockIN = new DataTable();
                    string[] fieldTotal = { "InventoryID" };
                    ArrayList getIDValue = new ArrayList();
                    getIDValue.Add("=" + txtISIinvID.Text);

                    dtStockIN = datac.getMathRecord("InventoryStock", fieldTotal, getIDValue);
                    txtISIstockTotal.Text = dtStockIN.Rows[0][2].ToString();
             //   }
             //   else
             //   {
             //       MessageBox.Show("Click <clear> to reload stock details in the grid", "Grid click unsuccessful ", MessageBoxButtons.OK, MessageBoxIcon.Information);
              //  }
                    btnISIsave.Enabled = true;
              
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch
        }//end of private void dtgStockIn_Click(object sender, EventArgs e)

        private void btnISIsearchItem_Click(object sender, EventArgs e)
        {
            try
            {
                btnISIclear.Enabled = true;
                btnISInewItem.Enabled = false;
                //  btnISIdeleteItem.Enabled = true;
                btnISIinvDeleteItem.Enabled = true;
                btnISIsave.Enabled = true;
                if (txtISIinvCode.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvCode" };
                    values.Add(txtISIinvCode.Text);

                    dtgStockIn.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvCode.Text!=null)
                else if (txtISIinvItem.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvItem" };

                    values.Add(txtISIinvItem.Text.ToUpper());
                    dtgStockIn.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvItem.Text!=null)
                else if (txtISIinvDescription.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvDescription" };

                    values.Add(txtISIinvDescription.Text.ToUpper());
                    dtgStockIn.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvDesc.Text!=null)
                else if (txtISIinvCategory.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvCategory" };

                    values.Add(txtISIinvCategory.Text.ToUpper());
                    dtgStockIn.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvDesc.Text!=null)
                else //if all is still empty, then do this. 
                {
                    MessageBox.Show("Please enter value to search for", "Search failed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtISIinvID.Clear();        //clear txtboxes for next search/ item or whichever
                    txtISIinvCode.Clear();
                    txtISIinvItem.Clear();
                    txtISIinvDescription.Clear();
                    txtISIinvCategory.Clear();
                    txtISIstockReceived.Clear();
                    txtISIstockPrice.Clear();
                    txtISIstockTotal.Clear();
                }//end of else
                btnISIdelItemInvoice.Enabled = false;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnISIsearchItem_Click(object sender, EventArgs e)

        private void btnISIclear_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccess datac = new DataAccess();
                btnISIsave.Enabled = false;
                btnISInewItem.Enabled = true;

                txtISIinvID.Clear();        //clear txtboxes for next search/ item
                txtISIinvCode.Clear();
                txtISIinvItem.Clear();
                txtISIinvDescription.Clear();
                txtISIinvCategory.Clear();
                txtISIstockReceived.Clear();
                txtISIstockPrice.Clear();
                txtISIstockTotal.Clear();

                dtgStockIn.DataSource = datac.getTable("Inventory");
                btnISIinvDeleteItem.Enabled = false;
                btnISIdelItemInvoice.Enabled = false;
                btnISInewItem.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing textboxes : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)

        }//end of private void btnISIclear_Click(object sender, EventArgs e)

        private void btnISInewItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtISIinvItem.Text != "")
                {
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                    ArrayList createTotalStock = new ArrayList();
                    ArrayList createNewItemStockIN = new ArrayList();

                    DataAccess datac = new DataAccess();
                    ArrayList invoiceValues = new ArrayList();
                    ArrayList totalStockChanged = new ArrayList();
                    ArrayList stockINValues = new ArrayList();
                    DataTable dtInvoiceIN = new DataTable();
                    DataTable dtAllInvoices = new DataTable();
                    DataTable dtGetNewItemID = new DataTable();
                    ArrayList getNewItemDetails = new ArrayList();
                    ArrayList getIDValue = new ArrayList();
                    string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"
                    string[] fieldNewItemID= { "InvItem" };
                    Boolean exists = new Boolean();

                    values.Add("0");          //store textBox / comboBox value in the ArrayList
                    values.Add(txtISIinvCode.Text);
                    values.Add(txtISIinvItem.Text.ToUpper());
                    values.Add(txtISIinvDescription.Text.ToUpper());
                    values.Add("");
                    values.Add(txtISIinvCategory.Text.ToUpper());
                    if (txtISIinvReLevel.Text != "")     //See if user gave value for reorderLevel, and use value
                    {
                        values.Add(txtISIinvReLevel.Text);
                    }//end of if (txtInvReLeveli.Text!="")
                    else
                    {
                        values.Add("0");            //if user gave no value, make default 0: for prototyping purposes
                    }//end of else, if (txtInvReLeveli.Text!="")
                    if (cmbISIinvMarkup.Text != "")
                    {
                        values.Add(cmbISIinvMarkup.Text);       //if user gave value, use value
                    }//end of if (cmbInvMarkupi.Text!="")
                    else
                    {
                        values.Add("25");                   //if user gave no value for markup%, then make 25% default
                    }//end of else, if (cmbInvMarkupi.Text!="")
                    values.Add("false");

                    datac.insertCmd("Inventory", fieldInvAll, values);       ////Send values in fieldInv string format through insertCmd query to database table
                    dtgStockIn.DataSource = datac.getTable("Inventory");

                    getNewItemDetails.Add(txtISIinvItem.Text.ToUpper());
                    dtGetNewItemID = datac.getRecord("Inventory", fieldNewItemID, getNewItemDetails);
                    //make reference for a new item to call other tables from
                    if (txtISIstockReceived.Text!="")
                    {//create instance for this new item to have a reference to StockIN and a StockTotal

                        if (txtInvoiceNo.Text != "")//this if makes it invoice based, invoice reference made-else is for recirculation
                        {
                            if (dtpInvoiceDate.Text != "")
                            {
                                dtAllInvoices = datac.getTable("InvoiceStockIN");
                                if (txtISIstockPrice.Text != "")
                                {
                                    for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
                                    {
                                        if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())//&& dtAllInvoices.Rows[i][4].ToString() == txtISITotal.Text
                                        {
                                            exists = true;
                                        }//end of 
                                        else
                                        {
                                            exists = false;
                                        }//end of else
                                    }//end of for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
                                    //   MessageBox.Show("Exists value: " + exists, "Testing complete");
                                    if (exists == true)
                                    {
                                        getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
                                        // getIDValue.Add("='" + dtpInvoiceDate.Text+"00:00:00.000'");
                                        getIDValue.Add("=" + cmbISISupplier.SelectedValue);
                                        // getIDValue.Add("=" + txtISITotal.Text + "00");

                                        dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID
                                   
                                        // createNewItemStockIN.Add();
                                        stockINValues.Add(0);
                                        stockINValues.Add(dtGetNewItemID.Rows[0][0].ToString());
                                        stockINValues.Add(txtISIstockReceived.Text);
                                        stockINValues.Add(txtISIstockPrice.Text);
                                        stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
                                        stockINValues.Add(txtISIstockReceived.Text);
                                        datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted

                                        totalStockChanged.Add(0);
                                        totalStockChanged.Add(dtGetNewItemID.Rows[0][0].ToString());
                                        totalStockChanged.Add(txtISIstockReceived.Text);
                                        datac.insertCmd("InventoryStock", fieldTotalStock, totalStockChanged);//create a reference to totalStock with first stock

                                        // dtgStockIn.DataSource = datac.getAllInvoice();
                                        dtgStockIn.DataSource = datac.getStockInInvoice(dtInvoiceIN.Rows[0][0].ToString());
                                    }//end of if (exists = true)
                                    else
                                    {//make new invoice thingy
                                        invoiceValues.Add(0);
                                        invoiceValues.Add(txtInvoiceNo.Text);
                                        invoiceValues.Add(dtpInvoiceDate.Text);
                                        invoiceValues.Add(cmbISISupplier.SelectedValue);
                                        invoiceValues.Add(txtISITotal.Text);
                                        datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, invoiceValues);

                                        getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
                                        getIDValue.Add("=" + cmbISISupplier.SelectedValue);
                                        dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

                                        stockINValues.Add(0);
                                        stockINValues.Add(dtGetNewItemID.Rows[0][0].ToString());
                                        stockINValues.Add(txtISIstockReceived.Text);
                                        stockINValues.Add(txtISIstockPrice.Text);
                                        stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
                                        stockINValues.Add(txtISIstockReceived.Text);
                                        datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted
                                        dtgStockIn.DataSource = datac.getStockInInvoice(dtInvoiceIN.Rows[0][0].ToString());

                                        totalStockChanged.Add(0);
                                        totalStockChanged.Add(dtGetNewItemID.Rows[0][0].ToString());
                                        totalStockChanged.Add(txtISIstockReceived.Text);
                                        datac.insertCmd("InventoryStock", fieldTotalStock, totalStockChanged);//create a reference to totalStock with first stock
                                    }//end of else, if (exists == true)
                                }//end of if (txtISIstockPrice.Text!="")
                                else
                                {
                                    MessageBox.Show("Please enter a Price/Unit value in the <Price/ Unit excl VAT> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }//end of else, if (txtISIstockPrice.Text!="")
                            }//end of if (dtpInvoiceDate.Text!="")
                            else
                            {
                                MessageBox.Show("Please enter a date value in the Invoice Date field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }//end of else if (dtpInvoiceDate.Text!="")
                        }//end of if (txtISIinvoiceNr.Text != "")
                        else
                        {//since no invoiceNo is given, make it a STOCK invoice
                            invoiceValues.Add(0);
                            invoiceValues.Add("STOCK " + (emptySupplier+=1));
                            invoiceValues.Add(dtpInvoiceDate.Text);
                            invoiceValues.Add("1");
                            invoiceValues.Add("0.00");
                            datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, invoiceValues);

                            getIDValue.Add(" LIKE 'STOCK " + (emptySupplier += 1)+"'");
                            getIDValue.Add("= 1");
                            dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

                            stockINValues.Add(0);
                            stockINValues.Add(dtGetNewItemID.Rows[0][0].ToString());
                            if (txtISIstockReceived.Text!="")
                            {
                                stockINValues.Add(txtISIstockReceived.Text);
                            }//end of if (txtISIstockReceived.Text!="")
                            else
                            {
                                MessageBox.Show("Please enter a Quantity Received value in the <Quantity Received> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                stockINValues.Add(txtISIstockReceived.Text);
                            }//end of else, if (txtISIstockReceived.Text!="")
                            if (txtISIstockPrice.Text!="")
                            {
                                stockINValues.Add(txtISIstockPrice.Text);
                            }//end of if (xtISIstockPrice.Text!="")
                            else
                            {
                                MessageBox.Show("Please enter a Price/Unit value in the <Price/ Unit excl VAT> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                stockINValues.Add(txtISIstockPrice.Text);
                            }//end of else, if (xtISIstockPrice.Text!="")
                            
                            stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
                            stockINValues.Add(txtISIstockReceived.Text);
                            datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted
                            dtgStockIn.DataSource = datac.getAllInvoice();

                            totalStockChanged.Add(0);
                            totalStockChanged.Add(dtGetNewItemID.Rows[0][0].ToString());
                            totalStockChanged.Add(txtISIstockReceived.Text);
                            datac.insertCmd("InventoryStock", fieldTotalStock, totalStockChanged);//create a reference to totalStock with first stock
                        }//end of else, if (txtISIinvoiceNr.Text != "")
                    }//end of if (txtISIstockReceived.Text!="")
                    else
                    {
                        MessageBox.Show("Please enter a Quantity Received value in the <Quantity Received> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }//end of else, if (txtISIstockReceived.Text!="")
                }//end of if(txtISIinvItem.Text!="")
                else
                {
                    MessageBox.Show("No record was added: Item Field is empty. Please provide an item's name to add.", "New record unsuccessful!");
                }//end of else, if (txtISIinvItem.Text!="")
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating new record : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnISInewItem_Click(object sender, EventArgs e)

        private void btnISIinvDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                btnISIsave.Enabled = false;

                string fieldID = "InventoryID";
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                values.Add(txtISIinvID.Text);          //store textBox / comboBox value in the ArrayList
                values.Add(txtISIinvCode.Text);
                values.Add(txtISIinvItem.Text.ToUpper());
                values.Add(txtISIinvDescription.Text.ToUpper());
                values.Add(txtISIinvCategory.Text.ToUpper());
                values.Add(txtISIinvReLevel.Text);
                values.Add(cmbISIinvMarkup.Text);
                values.Add("true");

                DataAccess datac = new DataAccess();
                //  datac.removeCmd("Inventory", txtISIinvID.Text);      //delete the record in the table using the key from the textBox
                datac.updateRecCmd("Inventory", fieldID, txtISIinvID.Text, fieldInv, values);        //Send values in fieldInv string format from textBox/ comboBox through updateCmd query to database table using InvID as key
                //deleting this will create a reference issue, since many tables use the inventoryID as a reference.
                dtgStockIn.DataSource = datac.getTable("Inventory");
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while deleting record : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnISIinvDeleteItem_Click(object sender, EventArgs e)

        private void btnISIdelItemInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                btnISIinvDeleteItem.Enabled = false;
                btnISInewItem.Enabled = false;
                btnISIsave.Enabled = false;

                DataAccess datac = new DataAccess();
                DataTable dtInvoiceIN = new DataTable();
                DataTable dtStockIN = new DataTable();
                DataTable dtTotalStock = new DataTable();
                ArrayList getIDValue = new ArrayList();
                ArrayList getStockInID = new ArrayList();
                ArrayList removeTotalStockvalues = new ArrayList(); 
                string[] fieldFilter = { "ISIInvoiceNo", "SupplierID"};
                string[] fieldsFilter = { "InventoryID", "ISIID" };
                string[] getTotalStock = { "InventoryID" };

                getIDValue.Add(" LIKE '" + txtInvoiceNo.Text+"'");
              //  getIDValue.Add("=" + dtpInvoiceDate.Text);
                getIDValue.Add("=" + cmbISISupplier.SelectedValue);
              //  getIDValue.Add("=" + txtISITotal.Text);
                dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

                getStockInID.Add("=" + txtISIinvID.Text);
              //  getStockInID.Add("=" + txtISIstockReceived.Text);
               // getStockInID.Add("=" + txtISIstockPrice.Text);
                getStockInID.Add("=" + dtInvoiceIN.Rows[0][0].ToString());
                dtStockIN = datac.getMathRecord("SubStockIN", fieldsFilter, getStockInID);  //save stock in details in this dataTable, to get the precise SubStockINID
                datac.removeCmd("SubStockIN", dtStockIN.Rows[0][0].ToString());      //delete the record in the table using the key from the dataTable from getMathRecord

                removeTotalStockvalues.Add("=" + txtISIinvID.Text); //should remember to remove that same item's quantity from the totalStock, Since it wont be added anymore
                dtTotalStock = datac.getRecord("InventoryStock", getTotalStock, removeTotalStockvalues);
                removeTotalStockvalues.Add(dtTotalStock.Rows[0][0].ToString());
                removeTotalStockvalues.Add(dtTotalStock.Rows[0][1].ToString());
                removeTotalStockvalues.Add((int.Parse(dtTotalStock.Rows[0][1].ToString())-int.Parse(dtStockIN.Rows[0][2].ToString())));
                datac.updateRecCmd("InventoryStock",fieldTotalStock[0].ToString(),dtTotalStock.Rows[0][0].ToString(),fieldTotalStock,removeTotalStockvalues);

                dtgStockIn.DataSource = datac.getStockInInvoice(dtInvoiceIN.Rows[0][0].ToString());
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting current item from invoice: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch(Exception ex)
        }//end of private void btnISIdelItemInvoice_Click(object sender, EventArgs e)

        private void btnISIpreviewCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccess datac = new DataAccess();
                DataTable dtInvoiceIN = new DataTable();
                ArrayList getIDValue = new ArrayList();
                string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };
                //Create new dataTable with current invoice record, so when 2nd if is tested, the same invoice wont be recorded twice, only the items get recorded
                getIDValue.Add(" LIKE '" + txtInvoiceNo.Text+"'");
              //  getIDValue.Add("=" + dtpInvoiceDate.Text);
                getIDValue.Add("=" + cmbISISupplier.SelectedValue);
             //   getIDValue.Add("=" + txtISITotal.Text);
                dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID
                dtgStockIn.DataSource = datac.getStockInInvoice(dtInvoiceIN.Rows[0][0].ToString());
                btnISIdelItemInvoice.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error previewing Invoice details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch(Exception ex)
        }//end of private void btnISIpreviewCurrent_Click(object sender, EventArgs e)

        private void btnISIrecordNewInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccess datac = new DataAccess();

                txtInvoiceNo.Clear();
                txtISITotal.Clear();
                txtISIinvID.Clear();
                txtISIinvCode.Clear();
                txtISIinvItem.Clear();
                txtISIinvDescription.Clear();
                txtISIinvCategory.Clear();
                txtISIinvReLevel.Clear();
                txtISIstockReceived.Clear();
                txtISIstockPrice.Clear();
                txtISIstockTotal.Clear();

                dtgStockIn.DataSource = datac.getAllInvoice();
                btnISIdelItemInvoice.Enabled = false;
                btnISInewItem.Enabled = true;

            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error creating New Invoice record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnISIrecordNewInvoice_Click(object sender, EventArgs e)
        #endregion

        #region tabSupplier Controls
        private void btnSupSearch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSupINSERT.Enabled = false;
                btnSupSearch.Enabled = true;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;

                if (txtSName.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "SupName" };

                    values.Add(txtSName.Text);
                    dtgSuppliers.DataSource = datac.getRecord("Supplier", searchField, values);
                }//end of if (txtSName.Text!=null)
                else
                {
                    MessageBox.Show("Please enter a supplier to search for", "Search failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSID.Clear();
                    txtSName.Clear();
                    txtSupPrefix.Clear();
                    txtSREP.Clear();
                    txtSContactP.Clear();
                    txtSCell.Clear();
                    txtSBusNr.Clear();
                    txtSEmail.Clear();
                    txtSAddress.Clear();
                    txtSCity.Clear();
                    txtSProv.Clear();
                    txtSupReTime.Clear();

                }//end of else if (txtSName.Text != "")
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSupSearch_Click(object sender, EventArgs e)

        private void btnSupClear_Click(object sender, EventArgs e)
        {
            try
            {
                btnSupINSERT.Enabled = true;
                btnSupSearch.Enabled = true;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;

                txtSID.Clear();
                txtSName.Clear();
                txtSupPrefix.Clear();
                txtSREP.Clear();
                txtSContactP.Clear();
                txtSCell.Clear();
                txtSBusNr.Clear();
                txtSEmail.Clear();
                txtSAddress.Clear();
                txtSCity.Clear();
                txtSProv.Clear();
                txtSupReTime.Clear();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing textBoxes : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSupClear_Click(object sender, EventArgs e)

        private void dtgSuppliers_Click(object sender, EventArgs e)
        {
            try
            {
                btnSupINSERT.Enabled = false;
                btnSupSearch.Enabled = false;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = true;

                //"SupID", "SName", "SContactPerson", "SREP", "SBusNr", "SCellNr", "SAddress", "SCity", "SProv
                txtSID.Text = dtgSuppliers.SelectedRows[0].Cells[0].Value.ToString();
                txtSName.Text = dtgSuppliers.SelectedRows[0].Cells[1].Value.ToString();
                txtSContactP.Text = dtgSuppliers.SelectedRows[0].Cells[2].Value.ToString();
                txtSREP.Text = dtgSuppliers.SelectedRows[0].Cells[3].Value.ToString();
                txtSCell.Text = dtgSuppliers.SelectedRows[0].Cells[4].Value.ToString();
                txtSBusNr.Text = dtgSuppliers.SelectedRows[0].Cells[5].Value.ToString();
                txtSEmail.Text = dtgSuppliers.SelectedRows[0].Cells[6].Value.ToString();
                txtSAddress.Text = dtgSuppliers.SelectedRows[0].Cells[7].Value.ToString();
                txtSCity.Text = dtgSuppliers.SelectedRows[0].Cells[8].Value.ToString();
                txtSProv.Text = dtgSuppliers.SelectedRows[0].Cells[9].Value.ToString();
                txtSupPrefix.Text = dtgSuppliers.SelectedRows[0].Cells[10].Value.ToString();
                txtSupReTime.Text = dtgSuppliers.SelectedRows[0].Cells[11].Value.ToString();
                cmbSupPayTerm.Text = dtgSuppliers.SelectedRows[0].Cells[12].Value.ToString();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }//end of catch (Exception ex)

        }//end of private void dtgSuppliers_Click(object sender, EventArgs e)

        private void btnSupUPDATE_Click(object sender, EventArgs e)
        {
            try
            {
                btnSupINSERT.Enabled = false;
                btnSupSearch.Enabled = false;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = true;

                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                values.Add(txtSID.Text);          //store textBox / comboBox value in the ArrayList
                values.Add(txtSName.Text);
                values.Add(txtSContactP.Text);
                values.Add(txtSREP.Text);
                values.Add(txtSCell.Text);
                values.Add(txtSBusNr.Text);
                values.Add(txtSEmail.Text);
                values.Add(txtSAddress.Text);
                values.Add(txtSCity.Text);
                values.Add(txtSProv.Text);
                values.Add(txtSupPrefix.Text);
                values.Add(txtSupReTime.Text);
                values.Add(cmbSupPayTerm.DisplayMember);
                values.Add("false");
                string fieldID = "SupplierID";
                DataAccess datac = new DataAccess();
                datac.updateRecCmd("Supplier", fieldID, txtSID.Text, fieldSup, values);        //Send values in fieldSup string format from textBox/ comboBox through updateCmd query to database table using InvID as key
                dtgSuppliers.DataSource = datac.getTable("Supplier");
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSupUPDATE_Click(object sender, EventArgs e)

        private void btnSupINSERT_Click(object sender, EventArgs e)
        {
            try
            {
                btnSupINSERT.Enabled = true;
                btnSupSearch.Enabled = false;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;

            ArrayList values = new ArrayList(); //make arrayList to store all values of current record
            values.Add(txtSID.Text);          //store textBox / comboBox value in the ArrayList
            values.Add(txtSName.Text);
            values.Add(txtSContactP.Text);
            values.Add(txtSREP.Text);
            values.Add(txtSBusNr.Text);
            values.Add(txtSCell.Text);
            values.Add(txtSEmail.Text);
            values.Add(txtSAddress.Text);
            values.Add(txtSCity.Text);
            values.Add(txtSProv.Text);
            values.Add(txtSupPrefix.Text);
            values.Add(txtSupReTime.Text);
            values.Add(cmbSupPayTerm.DisplayMember);
            values.Add("0");

            DataAccess datac = new DataAccess();
            dtgSuppliers.DataSource = datac.insertCmd("Supplier", fieldSup, values);       ////Send values in fieldSup string format through insertCmd query to database table
            dtgSuppliers.DataSource = datac.getTable("Supplier");
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating new record : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSupINSERT_Click(object sender, EventArgs e)

        private void btnDeleteSup_Click(object sender, EventArgs e)
        {
            try
            {
                btnSupINSERT.Enabled = false;
                btnSupSearch.Enabled = false;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;

                string fieldID = "SupplierID";
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                values.Add(txtSID.Text);          //store textBox / comboBox value in the ArrayList
                values.Add(txtSName.Text);
                values.Add(txtSContactP.Text);
                values.Add(txtSREP.Text);
                values.Add(txtSBusNr.Text);
                values.Add(txtSCell.Text);
                values.Add(txtSEmail.Text);
                values.Add(txtSAddress.Text);
                values.Add(txtSCity.Text);
                values.Add(txtSProv.Text);
                values.Add(txtSupPrefix.Text);
                values.Add(txtSupReTime.Text);
                values.Add(cmbSupPayTerm.DisplayMember);
                values.Add("1");
                
                DataAccess datac = new DataAccess();
                datac.updateRecCmd("Supplier", fieldID, txtSID.Text, fieldSup, values);        //Send values in fieldSup string format from textBox/ comboBox through updateCmd query to database table using InvID as key
                // dtgSuppliers.DataSource = datac.removeCmd("Supplier", txtSID.Text);      //delete the record in the table using the key from the textBox
                dtgSuppliers.DataSource = datac.getTable("Supplier");
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting record : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnDeleteSup_Click(object sender, EventArgs e)
        #endregion

        #region tabStockCheck Controls             
        private void btnSearchSCinv_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSCinvCode.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvCode" };

                    values.Add(txtSCinvCode.Text);
                    dtgCheckStock.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvCode.Text!=null)
                else if (txtSCinvItem.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvItem" };

                    values.Add(txtSCinvItem.Text.ToUpper());
                    dtgCheckStock.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvItem.Text!=null)
                else if (txtSCinvDesc.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvDescription" };

                    values.Add(txtSCinvDesc.Text.ToUpper());
                    dtgCheckStock.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvDesc.Text!=null)
                else if (txtSCinvCat.Text != "")
                {
                    DataAccess datac = new DataAccess();
                    ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                    string[] searchField = { "InvCategory" };

                    values.Add(txtSCinvCat.Text.ToUpper());
                    dtgCheckStock.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvDesc.Text!=null)
                else //if all searchable fiels are still empty, then do this. 
                {
                    MessageBox.Show("Please enter value to search for", "Search failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       //clear all textboxes 
                    txtSCinvID.Clear();
                    txtSCinvCode.Clear();
                    txtSCinvItem.Clear();
                    txtSCinvDesc.Clear();
                    txtSCinvCat.Clear();
                    txtSCinvRelevel.Clear();
                    txtSCactualTotal.Clear();
                    txtSCsysTotal.Clear();
                }//end of else
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSearchSCinv_Click(object sender, EventArgs e)

        private void dtgCheckStock_Click(object sender, EventArgs e)
        {
            try
            {
                txtSCinvID.Text = dtgCheckStock.SelectedRows[0].Cells[0].Value.ToString();
                txtSCinvCode.Text = dtgCheckStock.SelectedRows[0].Cells[1].Value.ToString();
                txtSCinvItem.Text = dtgCheckStock.SelectedRows[0].Cells[2].Value.ToString().ToUpper();
                txtSCinvDesc.Text = dtgCheckStock.SelectedRows[0].Cells[3].Value.ToString().ToUpper();
                txtSCinvCat.Text = dtgCheckStock.SelectedRows[0].Cells[4].Value.ToString().ToUpper();
                txtSCinvRelevel.Text = dtgCheckStock.SelectedRows[0].Cells[5].Value.ToString();

                txtSCactualTotal.Clear();

                DataAccess datac = new DataAccess();
                //****************Get the stock total quantity from InventoryStock table, relevant to the InventoryID******************
                DataTable dtTotalStock = new DataTable();
                string[] fieldTotal = { "InventoryID" };
                ArrayList getIDValue = new ArrayList();
                getIDValue.Add("=" + txtSCinvID.Text);

                dtTotalStock = datac.getMathRecord("InventoryStock", fieldTotal, getIDValue);
                if (dtTotalStock.Rows[0] != null)
                {
                    txtSCsysTotal.Text = dtTotalStock.Rows[0][2].ToString();
                }//end of if (dtStockIn.Rows[0] != null)
                else
                {
                    MessageBox.Show("no rows in datatable: ");
                } //end of else                     
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes from Check Stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void dtgCheckStock_Click(object sender, EventArgs e)

        private void btnSCsave_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccess datac = new DataAccess();
                ArrayList updateStock = new ArrayList();
                ArrayList updateStockIn = new ArrayList();
                ArrayList addToOUT = new ArrayList();
                DataTable dtTempStockIN = new DataTable();
                DataTable dtTotalStock = new DataTable();
                ArrayList getIDValue = new ArrayList();
                getIDValue.Add("=" + txtSCinvID.Text);
                string[] fieldTotal = { "InventoryID" };
                int difference = 0;
                
                if (txtSCactualTotal.Text!="")//if there is something to compare the system total with, then do this
                {
                    //do some testing with the two values, then do something with the difference, if any
                    if ((int.Parse(txtSCsysTotal.Text))>(int.Parse(txtSCactualTotal.Text)))
                    {
                        //part 1: change totalStock
                        updateStock.Add(0);
                        updateStock.Add(txtSCinvID.Text);
                        updateStock.Add(txtSCactualTotal.Text);
                        datac.updateRecCmd("InventoryStock", fieldTotalStock[1].ToString(), txtSCinvID.Text, fieldTotalStock, updateStock);
                        //part 2: if system is more, stockIn should decrease with the difference
                        difference = ((int.Parse(txtSCsysTotal.Text)) - (int.Parse(txtSCactualTotal.Text)));//the difference between system and actual
                        dtTempStockIN=datac.getFIFODatedPrice(txtSCinvID.Text);
                        for (int i = 0; i < difference-1; i++)
                             {
                                 if (int.Parse(dtTempStockIN.Rows[0][5].ToString())>0)
                                 {
                                     updateStockIn.Add(dtTempStockIN.Rows[0][0].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][1].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][2].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][3].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][4].ToString());
                                     updateStockIn.Add(int.Parse(dtTempStockIN.Rows[0][5].ToString())-1);
                                     datac.updateRecCmd("SubStockIN", fieldStockIN[0],dtTempStockIN.Rows[0][0].ToString(),fieldStockIN,updateStockIn);
                                 }//end of if (int.Parse(dtSubStockIN.Rows[0][2].ToString())>0)
                                 else
                                 {//this is for when the last loaded record's LEFT has run to 0 and the counter is not yet 0
                                     dtTempStockIN = datac.getFIFODatedPrice(txtSCinvID.Text);
                                     updateStockIn.Add(dtTempStockIN.Rows[0][0].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][1].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][2].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][3].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][4].ToString());
                                     updateStockIn.Add(int.Parse(dtTempStockIN.Rows[0][5].ToString()) - 1);
                                     datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtTempStockIN.Rows[0][0].ToString(), fieldStockIN, updateStockIn);                          
                                 }//end of else
                             }//end of for (int i = 0; i < int.Parse(txtInvStockOut.Text); i++)
                        //part 3: record difference as a outgoing transaction
                        addToOUT.Add(0);
                        addToOUT.Add(txtSCinvID.Text);
                        addToOUT.Add(difference);
                        addToOUT.Add(DateTime.Now);//not 100% sure if this is how you do is programmatically-get today's date and time
                        addToOUT.Add("false");//stating that somewhere stock was taken with no record of it
                        datac.insertCmd("SubStockOUT", fieldStockOut, addToOUT);
                    }//end of if (int.Parse(txtSCsysTotal.Text)>int.Parse(txtSCactualTotal.Text)) 
                    else if ((int.Parse(txtSCsysTotal.Text)) < (int.Parse(txtSCactualTotal.Text)))
                    {
                        updateStock.Add(0);
                        updateStock.Add(txtSCinvID.Text);
                        updateStock.Add(txtSCactualTotal.Text);
                        datac.updateRecCmd("InventoryStock", fieldTotalStock[1].ToString(), txtSCinvID.Text, fieldTotalStock, updateStock);
                        difference =(int.Parse(txtSCactualTotal.Text)- int.Parse(txtSCsysTotal.Text)) ;//the difference between system and actual
                        for (int i = 0; i < difference - 1; i++)
                        {
                            updateStockIn.Add(dtTempStockIN.Rows[0][0].ToString());
                            updateStockIn.Add(dtTempStockIN.Rows[0][1].ToString());
                            updateStockIn.Add(dtTempStockIN.Rows[0][2].ToString());
                            updateStockIn.Add(dtTempStockIN.Rows[0][3].ToString());
                            updateStockIn.Add(dtTempStockIN.Rows[0][4].ToString());
                            updateStockIn.Add(int.Parse(dtTempStockIN.Rows[0][5].ToString()) + 1);
                            datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtTempStockIN.Rows[0][0].ToString(), fieldStockIN, updateStockIn);
                            updateStockIn = new ArrayList();//basically clearing this arrayList
                            dtTempStockIN = datac.getFIFODatedPrice(txtSCinvID.Text);//get the record again after the update
                        }//end of for (int i = 0; i < int.Parse(txtInvStockOut.Text); i++)
                    }//end of  else if (int.Parse(txtSCsysTotal.Text) < int.Parse(txtSCactualTotal.Text))
                    else if (int.Parse(txtSCsysTotal.Text) == int.Parse(txtSCactualTotal.Text))
                    {
                        MessageBox.Show("No changes are needed. Please select another item.");
                    }//end of else if (int.Parse(txtSCsysTotal.Text) == int.Parse(txtSCactualTotal.Text))
                }//enf of if (txtSCactualTotal.Text!="")
                else
                {
                    MessageBox.Show("Please provide a value to compare the System's stock with.");
                }//end of else if (txtSCactualTotal.Text!="")
                dtTotalStock = datac.getMathRecord("InventoryStock", fieldTotal, getIDValue);
                if (dtTotalStock.Rows[0] != null)
                {
                    txtSCsysTotal.Text = dtTotalStock.Rows[0][2].ToString();
                }//end of if (dtStockIn.Rows[0] != null)
                else
                {
                    MessageBox.Show("no rows in datatable: ");
                } //end of else 
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Stock Taking: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSCsave_Click(object sender, EventArgs e)
        #endregion
        private void btnPDFprint_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccess datac=new DataAccess();
                PdfCreator pdfc = new PdfCreator();
                DataTable dtInvValues = new DataTable();
                DataTable dtSupValues = new DataTable();
                string fileName = "whoop whoop test success!";
                string getID="3";
             //   dtInvValues = (DataTable)dtgInventory.DataSource;
                dtInvValues = datac.getStockInInvoice(getID);
                ArrayList val=new ArrayList();

                string[] field = { "SupplierID" };
                val.Add("1");
                dtSupValues = datac.getRecord("Supplier", field, val);
                //take care when creating datatable for items: Columns needed:1) PartCode, Item, Quantity, Price, ?Length? 2) consider creating the TOTAL col here
                pdfc.CreateOrderPDF(dtInvValues, dtSupValues, fileName);//simply send a datatable for supplier, a datatable for the items, and a string for the filename- it will do the rest
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch (Exception ex)            
        }//end of private void btnPDFprint_Click(object sender, EventArgs e)
       
    }//end of public partial class frmInventory : Form
}//end of namespace ImagineTrailvan

// Add TabPage1
//  TabPage tabStock = new TabPage();
//  tabStock.Name = "Stock";
//   tabStock.Text = "Author";
// tabControl1.TabPages.Add(tabStock);
//    tabControl1.Controls.Add(dtgInventory);
//    TabPage selectedTab = tabControl1.SelectedTab;
//    int selectedIndex = tabControl1.SelectedIndex;
//   tabControl1.SelectedTab = selectedTab;

//for pdf create
//try
//            {
//                int ypoint = 0;

            //    PdfDocument pdf = new PdfDocument();
            //    pdf.Info.Title = "Inventory Test PDF";
            //    PdfPage pdfpage = pdf.AddPage();
            //    XGraphics graph = XGraphics.FromPdfPage(pdfpage);
            //    XFont font = new XFont("Arial", 12, XFontStyle.Regular);
            //    XFont fontHead = new XFont("Sans-serif", 10, XFontStyle.Regular);
            //    ypoint = ypoint + 20;
            //    graph.DrawString("IMAGINE TRAILVANS (PTY) LTD", fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //    ypoint = ypoint + 10;
            //    graph.DrawString("REG. No: 2006/032270/07", fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //    ypoint = ypoint + 10;
            //    graph.DrawString("VAT REG. No: 4150234369", fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //    ypoint = ypoint + 20;
            //    graph.DrawString("PO Box 25238", fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //    ypoint = ypoint + 10;
            //    graph.DrawString("MONUMENTPARK  0105", fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //    ypoint = ypoint + 10;
            //    graph.DrawString("PRETORIA", fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //    ypoint = ypoint + 20;
            //    graph.DrawString("TEL : 27 (012)  349  2636", fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //    ypoint = ypoint + 10;
            //    graph.DrawString("FAX : 27 (012)  349 2625", fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //    ypoint = ypoint + 10;
            //    graph.DrawString("E MAIL: gideon@imagine-trailvan.co.za", fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //    ypoint = ypoint + 20;

            //    graph.DrawString("Inventory Details", fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //    ypoint = ypoint + 40; 
            //    for (int i = 0; i < dtgInventory.Rows.Count-1; i++)
            //    {
            //        graph.DrawString(dtgInventory.Rows[i].Cells[0].Value.ToString(), font, XBrushes.Black, new XRect(10, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //        graph.DrawString(dtgInventory.Rows[i].Cells[1].Value.ToString(), font, XBrushes.Black, new XRect(20, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //        graph.DrawString(dtgInventory.Rows[i].Cells[2].Value.ToString(), font, XBrushes.Black, new XRect(90, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //        graph.DrawString(dtgInventory.Rows[i].Cells[3].Value.ToString(), font, XBrushes.Black, new XRect(295, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //        graph.DrawString(dtgInventory.Rows[i].Cells[4].Value.ToString(), font, XBrushes.Black, new XRect(420, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //        graph.DrawString(dtgInventory.Rows[i].Cells[5].Value.ToString(), font, XBrushes.Black, new XRect(500, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //        graph.DrawString(dtgInventory.Rows[i].Cells[6].Value.ToString(), font, XBrushes.Black, new XRect(510, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //        graph.DrawString(dtgInventory.Rows[i].Cells[7].Value.ToString(), font, XBrushes.Black, new XRect(530, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);

            //        ypoint = ypoint + 20;
            //    }//end of for (int i = 0; i < dtgInventory.Rows.Count-1; i++)
            //    string pdfFilename = "InventoryListTest.pdf";
            //    pdf.Save(pdfFilename);
            //    Process.Start(pdfFilename);                
            //}//end of try
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}//end of catch