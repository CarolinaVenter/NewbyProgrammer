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
        #region Public Variables
        DataAccess datac = new DataAccess();
        public string[] fieldInv = { "InventoryID", "InvCode", "InvItem", "InvDescription", "InvCategory", "InvReorderLevel", "InvMarkup", "InvIsDeleted" };
        public string[] fieldInvAll = { "InventoryID", "InvCode", "InvItem", "InvDescription", "InvSupplierDescription", "InvCategory", "InvReorderLevel", "InvMarkup", "InvIsDeleted" };
        public string[] fieldSup = { "SupplierID", "SupName", "SupContactPerson", "SupREP", "SupBusinessNr", "SupCellNr", "SupEmail", "SupAddress", "SupCity", "SupProvince", "SupPrefix", "SupReorderTime", "SupPaymentTerm", "SupIsDeleted" ,"SupDiscount"};
        public string[] fieldStockOut = { "SubStockOUTID", "InventoryID", "SSOQuantityOut", "SSODateOut", "InvIsAccountedFor" };
        public string[] fieldStockIN = { "SubStockINID", "InventoryID", "SSIQuantityIN", "SSIPrice", "ISIID", "SSIStockLeft" };
        public string[] fieldInvoiceStockIN = { "ISIID", "ISIInvoiceNo", "ISIDateReceived", "SupplierID", "ISIInvoiceTotalIncl" };
        public string[] fieldTotalStock = { "InventoryStockID", "InventoryID", "ISTotalStock" };
        public string[] fieldOrders = { "OrdersID", "OrderNumber", "SupplierID", "OrdersDate", "OrderEstimateTotal" };
        public string[] fieldSubOrders = { "SubOrdersID", "InventoryID", "SOOrderedQuantity", "SOPrice", "OrdersID", "SOLength" };        
        public string vatLabel = "15%";
        PdfCreator pdfc = new PdfCreator();

        #endregion
       
        public frmInventory()
        {
            InitializeComponent();
            
        }//end of public frmInventory()

        private void frmInventory_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supplierOrderDataSet.Supplier' table. You can move, or remove it, as needed.
            this.supplierTableAdapter2.Fill(this.supplierOrderDataSet.Supplier);
            // TODO: This line of code loads data into the 'supInvValueDBDataSet.Supplier' table. You can move, or remove it, as needed.
            this.supplierTableAdapter1.Fill(this.supInvValueDBDataSet.Supplier);
            try
            {
                // TODO: This line of code loads data into the 'supplierDataSet.Supplier' table. You can move, or remove it, as needed.
                this.supplierTableAdapter.Fill(this.supplierDataSet.Supplier);

                #region Variables
                DataTable dtDisplayAllInvoices = new DataTable();
             //   double totalStockValue = 0.00;
                #endregion

                #region StockOut Controls on Load
                tabStockOUT.Controls.Add(dtgInventory);
                dtgInventory.DataSource = datac.getTable("Inventory");

                btnClear.Enabled = true;
                btnSearch.Enabled = true;
                btnStockUpdate.Enabled = false;

                dtpDateStockOUT.Value = DateTime.Now;
                #endregion

                #region StockIn Controls on Load
                tabStockIN.Controls.Add(dtgStockIn);
                tabStockIN.Controls.Add(dtgInvoiceHistory);
                dtgStockIn.DataSource = datac.getTable("Inventory");
                dtgInvoiceHistory.DataSource = datac.getAllInvoice();

                btnISIdelItemInvoice.Enabled = false;
                btnISIinvDeleteItem.Enabled = false;
                btnISIpreviewCurrent.Enabled = false;
                btnISIrecordNewInvoice.Enabled = false;
              //  btnISIsave.Enabled = false;
                btnISIclear.Enabled = true;

                dtpInvoiceDate.Value = DateTime.Now;
                #endregion

                #region StockCheck Controls on Load
                tabStockCheck.Controls.Add(dtgCheckStock);
                dtgCheckStock.DataSource = datac.getTable("Inventory");
                #endregion

                #region Suppliers Controls on Load
                tabSuppliers.Controls.Add(dtgSuppliers);
                dtgSuppliers.DataSource = datac.getTable("Supplier");

                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;
                btnSupINSERT.Enabled = true;
                btnSupSearch.Enabled = true;
                btnDeleteSup.Enabled = false;
                #endregion

                #region InventoryValue Controls on Load
                tabInventoryValue.Controls.Add(dtgInventoryValue);
                dtgInventoryValue.DataSource = datac.getInventoryValue();

                //to calculate total value of stock based on quantity and price on inventoryValue tab****
                displayInventoryValue();
                //for (int i = 0; i < dtgInventoryValue.Rows.Count - 1; i++)
                //{
                //    totalStockValue += (int.Parse(dtgInventoryValue.Rows[i].Cells[4].Value.ToString()) * double.Parse(dtgInventoryValue.Rows[i].Cells[5].Value.ToString()));
                //}//end of for (int i = 0; i < dtgInventoryValue.Rows.Count - 1; i++)
                //txtStockValue.Text = double.Parse(totalStockValue.ToString()).ToString("C");
                #endregion

                #region OrderStock Controls on Load
                dtgSupOrderHistory.DataSource = datac.getAllOrderHistory();
                dtgSupOrderList.DataSource = datac.getLowStock();
                tabOrderStock.Controls.Add(dtgSupOrderHistory);
                tabOrderStock.Controls.Add(dtgSupOrderList);
                tabOrderStock.Controls.Add(dtgOrderInvList);
                btnOrderSavePDF.Enabled = false;
                //for drag and drop between dtgs in the order tab
                dtgOrderInvList.AllowDrop = true;
                dtgSupOrderList.MouseMove += new MouseEventHandler(dtgSupOrderList_MouseMove);
                dtgOrderInvList.DragEnter += new DragEventHandler(dtgOrderInvList_DragEnter);
                dtgOrderInvList.DragDrop += new DragEventHandler(dtgOrderInvList_DragDrop);
                #endregion

            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tab: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void frmInventory_Load(object sender, EventArgs e)

        private void frmInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmSwitchboard obj = new frmSwitchboard();
            this.Hide();
            obj.ShowDialog();
            Environment.Exit(0);
        }//end of private void frmInventory_FormClosing(object sender, FormClosingEventArgs e)

        #region tabStockOUT Controls
        private void tabStock_Click(object sender, EventArgs e)
        {}//end of private void tabStock_Click(object sender, EventArgs e)

        private void btnSearch_Click(object sender, EventArgs e)
        {
            #region Variables
            ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
            #endregion
            try
            {
                if (txtInvCode.Text != "")
                {
                    string[] searchField = { "InvCode" };
                    values.Add(txtInvCode.Text);  
                    dtgInventory.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvCode.Text!=null)
                else if (txtInvItem.Text != "")
                {
                    string[] searchField = { "InvItem" };
                    values.Add(txtInvItem.Text.ToUpper());
                    dtgInventory.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvItem.Text!=null)
                else if (txtInvDesc.Text != "")
                {
                    string[] searchField = { "InvDescription" };
                    values.Add(txtInvDesc.Text.ToUpper());
                    dtgInventory.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvDesc.Text!=null)
                else if (txtInvCat.Text != "")
                {
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
                dtgInventory.DataSource = datac.getTable("Inventory");        //change dtgInventory back to original data in table

                btnStockUpdate.Enabled = false;
                btnSearch.Enabled = true;

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

        private void fifoDatedPrice()
        {
            #region Variables
            DataTable dtStockIn = new DataTable();
            Boolean exists = new Boolean();
            #endregion

            dtStockIn = datac.getTable("SubStockIN");
            exists = testDTIfExistAddOne(dtStockIn, 1, txtInvID.Text);
          //  dtStockIn = datac.getFIFODatedPrice(txtInvID.Text);//before i do this, i'll have to run a test exist first
            if (exists==true)
            {
                dtStockIn = datac.getFIFODatedPrice(txtInvID.Text);
                txtInvPrice.Text = dtStockIn.Rows[0][4].ToString();
            }//end of if (exists==true)
            else
            {
                MessageBox.Show("No record found ");
            } //end of else, if (exists==true)
        }//end of  private void fifoDatedPrice()

        private void dtgInventory_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                //datatables
                DataTable testTotalStock = new DataTable();
                DataTable dtTotalStock = new DataTable();
                DataTable dtStockIn = new DataTable();
                //arraylists
                ArrayList getIDValue = new ArrayList();
                //arrays
                string[] fieldTotal = { "InventoryID" };
                //variables
                Boolean exists = new Boolean();
               // Boolean flag = new Boolean();
                double price = 0.00;
                double markup = 0.00;
                double sellPrice = 0.00;
                #endregion

                txtInvID.Text = dtgInventory.SelectedRows[0].Cells[0].Value.ToString(); //populte the textboxes from the dtg
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
                
                testTotalStock = datac.getTable("InventoryStock");
                //test if there is any references in the linked tables
                exists = testDTIfExistAddOne(testTotalStock, 1, txtInvID.Text);
               
                //here the flag will be tested, doing the actual determining of what should be done when it exists
                if (exists == true)
                {//if the flag is true, meaning the record already exists, will then get references
                    //****************Get the stock total quantity from InventoryStock table, relevant to the InventoryID******************
                    getIDValue.Add("=" + txtInvID.Text);

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
                    fifoDatedPrice();             

                    //to calculate the selling price (price + markup excl VAT)***
                    //   txtSellPrice.Text = (double.Parse(txtInvPrice.Text) * ((100 + (int.Parse(cmbInvMarkup.ValueMember))) / 100)).ToString(); //tried but failed
                    price = double.Parse(txtInvPrice.Text); 
                    markup = double.Parse(cmbInvMarkup.Text);
                    sellPrice = price * ((100 + markup) / 100);
                    txtInvPrice.Text=price.ToString("C");
                    txtSellPrice.Text = sellPrice.ToString("C");
                }//end of if (flag == true)
                else
                {
                    MessageBox.Show(@"Please log the stock in the STOCK IN tab FIRST to ensure all the correct references are established.", "Reference reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnStockUpdate.Enabled = false; //disable button so that nothing can happen- force to either choose another item or go to StockIN
                }//end of else of if (flag == true)
              
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
                #region Variables
                //datatables
                DataTable dtStockOut = new DataTable();
                DataTable dtTotalStock = new DataTable();
                DataTable dtSubStockIN = new DataTable();
                DataTable dtStockIn = new DataTable();
                //arraylists
                ArrayList invValues = new ArrayList(); //make arrayList to store all values of current record
                ArrayList stockOutValues = new ArrayList();
                ArrayList totalValues = new ArrayList();
                ArrayList subStockIN = new ArrayList();
                #endregion

                btnClear.Enabled = false;
                btnStockUpdate.Enabled = false;

                //**********SEND INVENTORY DETAILS TO BE UPDATED**********
                //this is done even if no stock is taken out......
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
                        stockOutValues.Add(0);
                        stockOutValues.Add(txtInvID.Text);
                        stockOutValues.Add(txtInvStockOut.Text);
                        stockOutValues.Add(dtpDateStockOUT.Value);
                        stockOutValues.Add("true");
                        datac.insertCmd("SubStockOUT", fieldStockOut, stockOutValues);

                        //*********SEND update TO TOTALSTOCK TABLE**************
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
                        dtSubStockIN = datac.getFIFODatedPrice(txtInvID.Text);
                        
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

                if (int.Parse(txtInvReLevel.Text)>=int.Parse(txtInvTotalStock.Text))
                {
                    MessageBox.Show(@"This item has reached the reorder level. 
Please be adviced to place an order for this item as soon as possible.", "Stock Running Low", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }//end of if (int.Parse(txtInvReLevel.Text)>=int.Parse(txtInvTotalStock.Text))

                dtgInventory.DataSource = datac.getTable("Inventory");
                dtgInventoryValue.DataSource = datac.getInventoryValue();
                dtgSupOrderList.DataSource = datac.getLowStock();
                //to calculate total value of stock based on quantity and price on inventoryValue tab****
                displayInventoryValue();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnInvUpdate_Click(object sender, EventArgs e)
        #endregion

        #region tabStockIN
        private void dtgStockIn_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                //datatables
                DataTable dtStockIN = new DataTable();
                //arraylists
                ArrayList getIDValue = new ArrayList();
                //arrays
                string[] fieldTotal = { "InventoryID" };
                //variables
                Boolean exists = new Boolean();
                #endregion
           
                txtISIinvID.Text = dtgStockIn.SelectedRows[0].Cells[0].Value.ToString();
                txtISIinvCode.Text = dtgStockIn.SelectedRows[0].Cells[1].Value.ToString();
                txtISIinvItem.Text = dtgStockIn.SelectedRows[0].Cells[2].Value.ToString().ToUpper();
                txtISIinvDescription.Text = dtgStockIn.SelectedRows[0].Cells[3].Value.ToString().ToUpper();
                txtISISupDescription.Text = dtgStockIn.SelectedRows[0].Cells[4].Value.ToString().ToUpper();
                txtISIinvCategory.Text = dtgStockIn.SelectedRows[0].Cells[5].Value.ToString().ToUpper();
                txtISIinvReLevel.Text = dtgStockIn.SelectedRows[0].Cells[6].Value.ToString();
                cmbISIinvMarkup.Text = dtgStockIn.SelectedRows[0].Cells[7].Value.ToString(); //add a string Collection
               
                txtISIstockReceived.Clear();
                txtISIstockPrice.Clear();
             //   flag = false;
                dtStockIN = datac.getTable("InventoryStock");
                exists = testDTIfExistAddOne(dtStockIN, 1, txtISIinvID.Text);
                     //   MessageBox.Show("Exists value: " + flag, "Testing complete");
                if (exists == true)
                { //****************Get the stock total quantity from InventoryStock table, relevant to the InventoryID******************
                    getIDValue.Add("=" + txtISIinvID.Text);
                    dtStockIN = datac.getMathRecord("InventoryStock", fieldTotal, getIDValue);//get the stock totals using the inventory ID reference
                    txtISIstockTotal.Text = dtStockIN.Rows[0][2].ToString();
                    btnISIsave.Enabled = true;
                }//end of if (flag == true)
                else
                {
                    MessageBox.Show("No reference to stock on hand was found. The default value '0' will be used, until stock was inserted.", "Load unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtISIstockTotal.Text = "0.00";
                }//end of else, if (flag == true)
                btnISIsearchItem.Enabled = false;
                btnISIinvDeleteItem.Enabled = true;
                btnISIdelItemInvoice.Enabled = true;
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
                #region Variables
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                #endregion

                btnISIclear.Enabled = true;
                //  btnISIdeleteItem.Enabled = true;
                btnISIinvDeleteItem.Enabled = true;
                btnISIsave.Enabled = true;
                if (txtISIinvCode.Text != "")
                {
                    string[] searchField = { "InvCode" };
                    values.Add(txtISIinvCode.Text);
                    dtgStockIn.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvCode.Text!=null)
                else if (txtISIinvItem.Text != "")
                {
                    string[] searchField = { "InvItem" };
                    values.Add(txtISIinvItem.Text.ToUpper());
                    dtgStockIn.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvItem.Text!=null)
                else if (txtISIinvDescription.Text != "")
                {
                    string[] searchField = { "InvDescription" };
                    values.Add(txtISIinvDescription.Text.ToUpper());
                    dtgStockIn.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvDesc.Text!=null)
                else if (txtISIinvCategory.Text != "")
                {
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
                btnISIsave.Enabled = true;

                txtISIinvID.Clear();        //clear txtboxes for next search/ item
                txtISIinvCode.Clear();
                txtISIinvItem.Clear();
                txtISIinvDescription.Clear();
                txtISISupDescription.Clear();
                txtISIinvCategory.Clear();
                txtISIinvReLevel.Clear();
                txtISIstockReceived.Clear();
                txtISIstockPrice.Clear();
                txtISIstockTotal.Clear();

                dtgStockIn.DataSource = datac.getTable("Inventory");
                btnISIinvDeleteItem.Enabled = false;
                btnISIdelItemInvoice.Enabled = false;
                btnISIsearchItem.Enabled = true;
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
                #region Variables
                //datatables
                DataTable dtInvoiceIN = new DataTable();
                DataTable dtAllInvoices = new DataTable();
                DataTable dtGetNewItemID = new DataTable();
                //arraylists
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                ArrayList createTotalStock = new ArrayList();
                ArrayList createNewItemStockIN = new ArrayList();
                ArrayList invoiceValues = new ArrayList();
                ArrayList totalStockChanged = new ArrayList();
                ArrayList stockINValues = new ArrayList();
                ArrayList getNewItemDetails = new ArrayList();
                ArrayList getIDValue = new ArrayList();
                //arrays
                string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"
                string[] fieldNewItemID = { "InvItem" };
                //variables
                Boolean exists = new Boolean();
                Boolean flag = new Boolean();
                DataTable dtLastStockInvoice = datac.getLastStockInvoice();
                string arrLastStockInvoice = dtLastStockInvoice.Rows[0][0].ToString();
                string[] arrayInvoiceNo = arrLastStockInvoice.Split(' ');
                int stockInvNo = int.Parse(arrayInvoiceNo[1].ToString());
                #endregion

                if (txtISIinvItem.Text != "")
                {
                    values.Add("0");          //store textBox / comboBox value in the ArrayList
                    values.Add(txtISIinvCode.Text);
                    values.Add(txtISIinvItem.Text.ToUpper());
                    values.Add(txtISIinvDescription.Text.ToUpper());
                    values.Add(txtISISupDescription.Text.ToUpper());
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
                    }//end of if (cmbISIinvMarkup.Text != "")
                    else
                    {
                        values.Add("25");                   //if user gave no value for markup%, then make 25% default
                    }//end of else, if (cmbISIinvMarkup.Text != "")
                    values.Add("false");

                    flag = false;

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
                                        if (exists == false) 
                                        {
                                            if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())//&& dtAllInvoices.Rows[i][4].ToString() == txtISITotal.Text
                                            {
                                                exists = true;
                                            }//end of  if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())
                                            else
                                            {
                                                exists = false;
                                            }//end of else
                                        }//end of if (exists == false)
                                        else
                                        {
                                            //this will be my flag to signal if it exists, since the first part of exist will only test each row and then move on, thus not stopping if it exists
                                            flag = true;
                                        }//end of else of if (exists == false)
                                    }//end of for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
                                    //   MessageBox.Show("Exists value: " + exists, "Testing complete");
                                    if (flag == true)
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

                                        dtgStockIn.DataSource = datac.getTable("Inventory");
                                    }//end of if (flag = true)
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
                                        dtgStockIn.DataSource = datac.getTable("Inventory");

                                        totalStockChanged.Add(0);
                                        totalStockChanged.Add(dtGetNewItemID.Rows[0][0].ToString());
                                        totalStockChanged.Add(txtISIstockReceived.Text);
                                        datac.insertCmd("InventoryStock", fieldTotalStock, totalStockChanged);//create a reference to totalStock with first stock
                                    }//end of else, if (flag == true)
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
                            invoiceValues.Add(arrayInvoiceNo[0].ToString() + " " + (stockInvNo + 1));
                            invoiceValues.Add(dtpInvoiceDate.Text);
                            invoiceValues.Add("1");
                            invoiceValues.Add("0.00");
                            datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, invoiceValues);

                            getIDValue.Add(" LIKE '" + (arrayInvoiceNo[0].ToString() +" "+ (stockInvNo+1))+"'");
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
                            dtgStockIn.DataSource = datac.getTable("Inventory");

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
                dtgStockIn.DataSource = datac.getTable("Inventory");
                dtgCheckStock.DataSource = datac.getTable("Inventory");
                dtgSupOrderList.DataSource = datac.getLowStock();
                dtgInventoryValue.DataSource = datac.getInventoryValue();
                btnISIpreviewCurrent.Enabled = false;
                //to calculate total value of stock based on quantity and price on inventoryValue tab****
                displayInventoryValue();
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
                #region Variables
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                string fieldID = "InventoryID";
                #endregion
                btnISIsave.Enabled = false;

                values.Add(txtISIinvID.Text);          //store textBox / comboBox value in the ArrayList
                values.Add(txtISIinvCode.Text);
                values.Add(txtISIinvItem.Text.ToUpper());
                values.Add(txtISIinvDescription.Text.ToUpper());
                values.Add(txtISIinvCategory.Text.ToUpper());
                values.Add(txtISIinvReLevel.Text);
                values.Add(cmbISIinvMarkup.Text);
                values.Add("true");
                
                //Send values in fieldInv string format from textBox/ comboBox through updateCmd query to database table using InvID as key, and only updating column isDeleted to true
                datac.updateRecCmd("Inventory", fieldID, txtISIinvID.Text, fieldInv, values);        
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
                #region Variables
                //datatables
                DataTable dtInvoiceIN = new DataTable();
                DataTable dtStockIN = new DataTable();
                DataTable dtTotalStock = new DataTable();
                //arraylist
                ArrayList getIDValue = new ArrayList();
                ArrayList getStockInID = new ArrayList();
                ArrayList removeTotalStockvalues = new ArrayList();
                ArrayList getTotalRecord = new ArrayList();
                //arrays
                string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };
                string[] fieldsFilter = { "InventoryID", "ISIID" };
                string[] getTotalStock = { "InventoryID" };
                //variables
                #endregion

                btnISIinvDeleteItem.Enabled = false;
                btnISIsave.Enabled = false;

                getIDValue.Add(" LIKE '" + txtInvoiceNo.Text+"'");
                getIDValue.Add("=" + cmbISISupplier.SelectedValue);
                dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

                getStockInID.Add("=" + txtISIinvID.Text);
                getStockInID.Add("=" + dtInvoiceIN.Rows[0][0].ToString());
                dtStockIN = datac.getMathRecord("SubStockIN", fieldsFilter, getStockInID);  //save stock in details in this dataTable, to get the precise SubStockINID
                datac.removeCmd("SubStockIN", dtStockIN.Rows[0][0].ToString());      //delete the record in the table using the key from the dataTable from getMathRecord

                getTotalRecord.Add("=" + txtISIinvID.Text); //should remember to remove that same item's quantity from the totalStock, Since it wont be added anymore
                dtTotalStock = datac.getMathRecord("InventoryStock", getTotalStock, getTotalRecord);
                removeTotalStockvalues.Add(dtTotalStock.Rows[0][0].ToString());
                removeTotalStockvalues.Add(txtISIinvID.Text);//dtTotalStock.Rows[0][1].ToString());

                removeTotalStockvalues.Add((int.Parse(dtTotalStock.Rows[0][2].ToString()) - int.Parse(dtStockIN.Rows[0][2].ToString())));
                datac.updateRecCmd("InventoryStock",fieldTotalStock[1],dtTotalStock.Rows[0][0].ToString(),fieldTotalStock,removeTotalStockvalues);//update the item's stock reference to undo the insert of quantity that was now beinig removed

                MessageBox.Show("Record has been deleted from the invoice and stock.", "Record Deleted from Invoice!");

                dtgStockIn.DataSource = datac.getTable("Inventory"); ;//refresh datasource
                dtgInventoryValue.DataSource = datac.getInventoryValue();
                displayInventoryValue();
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
                #region Variables
                DataTable dtInvoiceIN = new DataTable();
                ArrayList getIDValue = new ArrayList();
                string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };
                #endregion
                if (txtInvoiceNo.Text!="")
                {
                    //Create new dataTable with current invoice record, so when 2nd if is tested, the same invoice wont be recorded twice, only the items get recorded
                    getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
                    //  getIDValue.Add("=" + dtpInvoiceDate.Text);
                    getIDValue.Add("=" + cmbISISupplier.SelectedValue);
                    //   getIDValue.Add("=" + txtISITotal.Text);
                    dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID
                    dtgStockIn.DataSource = datac.getStockInInvoice(dtInvoiceIN.Rows[0][0].ToString());//give a datasource to preview a certain invoice reference's details in the dtg
                }//end of if (txtInvoiceNo.Text!="")
                else
                {
                    MessageBox.Show("No invoice number is given to preview.", "Preview not available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                txtInvoiceNo.Clear();
                txtISITotal.Clear();
                txtISIinvID.Clear();
                txtISIinvCode.Clear();
                txtISIinvItem.Clear();
                txtISIinvDescription.Clear();
                txtISISupDescription.Clear();
                txtISIinvCategory.Clear();
                txtISIinvReLevel.Clear();
                txtISIstockReceived.Clear();
                txtISIstockPrice.Clear();
                txtISIstockTotal.Clear();

                dtgStockIn.DataSource = datac.getTable("Inventory");//refresh the dtg datasource
                dtgInvoiceHistory.DataSource = datac.getAllInvoice();//refresh the dtg datasource
                btnISIdelItemInvoice.Enabled = false;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error creating New Invoice record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnISIrecordNewInvoice_Click(object sender, EventArgs e)
     
        #region tabStockIn_Save Controls
        private string getInventoryID()
        {
            string getInvID = "";
            DataTable dtGetInventory = new DataTable();
            string[] fieldInv = { "InvCode", "InvItem", "InvDescription" };
            ArrayList arGetInventory = new ArrayList();

            arGetInventory.Add(txtISIinvCode.Text);
            arGetInventory.Add(txtISIinvItem.Text);
            arGetInventory.Add(txtISIinvDescription.Text);

            dtGetInventory = datac.getRecord("Inventory", fieldInv, arGetInventory);
            getInvID = dtGetInventory.Rows[0][0].ToString();

            txtISIinvID.Text = getInvID;

            return getInvID;
        }//end of private int getInventoryID()
        private Boolean testNewItemExist()
        {
            //my dynamic way to see if the record (or the reference thereof) exists within the table within the database
            #region Variables
            DataTable dtAllItems = new DataTable();
            Boolean exists = false;
            Boolean flag = false;
            #endregion

            dtAllItems = datac.getTable("Inventory");

            for (int i = 0; i < dtAllItems.Rows.Count; i++)
            {
                if (exists == false)
                {
                    if (dtAllItems.Rows[i][2].ToString() == txtISIinvItem.Text)
                    {
                        exists = true;
                        flag = true;
                    }//end of if (dtAllItems.Rows[i][2].ToString() == txtISIinvItem.Text)
                    else
                    {
                        exists = false;
                    }//end of else, if (dtAllItems.Rows[i][2].ToString() == txtISIinvItem.Text)
                }//end of if (exists == false)
                else
                {
                    //this will be my flag to signal if it exists, since the first part of exist will only test each row and then move on, thus not stopping if it exists
                    flag = true;
                }//end of else of if (exists == false)
            }//end of for (int i = 0; i < dtAllItems.Rows.Count; i++)
            return flag;
        }//end of  private Boolean testNewItemExist()
        private Boolean testInvoiceExist()
        {
            //my dynamic way to see if the record (or the reference thereof) exists within the table within the database
            #region Variables
            DataTable dtAllInvoices = new DataTable();
            Boolean exists = false;
            Boolean flag = false;
            #endregion

            dtAllInvoices = datac.getTable("InvoiceStockIN");

            for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
            {
                if (exists == false)
                {
                    if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())//&& dtAllInvoices.Rows[i][4].ToString() == txtISITotal.Text
                    {
                        exists = true;
                        flag = true;
                    }//end of if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())
                    else
                    {
                        exists = false;
                    }//end of else, if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString()) 
                }//end of if (exists == false)
                else
                {
                    //this will be my flag to signal if it exists, since the first part of exist will only test each row and then move on, thus not stopping if it exists
                    flag = true;
                }//end of else of if (exists == false)
            }//end of for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
            return flag;
        }//end of  private Boolean testInvoiceExist()
        private void insertRECEIVEDIntoInventoryStock()
        {
            DataTable dtGetNewItem = new DataTable();
            ArrayList arTotalStock = new ArrayList();
            ArrayList arNewItem = new ArrayList();
            string[] filterField = { "InvItem" };

            arNewItem.Add(txtISIinvItem.Text.ToUpper());//consider more than 1 search field to ensure there is NO other record close to it ( ref male/ female door retainer rubber)
            dtGetNewItem = datac.getRecord("Inventory", filterField, arNewItem);

            arTotalStock.Add(0);
            arTotalStock.Add(dtGetNewItem.Rows[0][0].ToString());
            arTotalStock.Add(txtISIstockReceived.Text);
            datac.insertCmd("InventoryStock", fieldTotalStock, arTotalStock);//create a reference to totalStock with first stock

        }//end of private void insertRECEIVEDIntoInventoryStock()
        private void insertRECEIVEDIntoStockIn(string getItemID)
        {
            DataTable dtInvoiceIN = new DataTable();
            ArrayList arGetInvoice = new ArrayList();
            ArrayList arStockIn = new ArrayList();
            string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"

            arGetInvoice.Add(" LIKE '" + txtInvoiceNo.Text + "'");
            arGetInvoice.Add("=" + cmbISISupplier.SelectedValue);
            dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, arGetInvoice);  //save invoice details in this dataTable, to get the precise ISIID

            if (txtISIstockPrice.Text!=""&&txtISIstockReceived.Text!="")
            {
                arStockIn.Add(0);
                arStockIn.Add(getItemID);
                if (txtISIstockReceived.Text != "")
                {
                    arStockIn.Add(txtISIstockReceived.Text);
                }//end of if (txtISIstockReceived.Text!="")
                else
                {
                    MessageBox.Show("Please enter a Quantity Received value in the <Quantity Received> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//end of else, if (txtISIstockReceived.Text!="")
                //arStockIn.Add(txtISIstockPrice.Text);
                if (txtISIstockPrice.Text != "")
                {
                    arStockIn.Add(txtISIstockPrice.Text);
                }//end of if (xtISIstockPrice.Text!="")
                else
                {
                    MessageBox.Show("Please enter a Price/Unit value in the <Price/ Unit excl VAT> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//end of else, if (xtISIstockPrice.Text!="")
                arStockIn.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
                arStockIn.Add(txtISIstockReceived.Text);
                datac.insertCmd("SubStockIN", fieldStockIN, arStockIn);// a new transaction with a reference to an invoice is created and inserted
            }//end of if (txtISIstockPrice.Text!=""&&txtISIstockReceived.Text!="")
            else
            {
                MessageBox.Show(@"Please enter a Price/Unit value and Quantity Received value in the corresponding fields.
                A reference to the Invoice has been created.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//end of else, if (txtISIstockPrice.Text!=""&&txtISIstockReceived.Text!="")
        }//end of private void insertRECEIVEDIntoStockIn()
        private void insertRECEIVEDIntoInvoiceIn()
        {
            ArrayList arInvoiceIn = new ArrayList();
            DataTable dtInvoiceIN = new DataTable();
            ArrayList arGetInvoice = new ArrayList();
            string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"

            arInvoiceIn.Add(0);
            arInvoiceIn.Add(txtInvoiceNo.Text);
            arInvoiceIn.Add(dtpInvoiceDate.Text);
            arInvoiceIn.Add(cmbISISupplier.SelectedValue);
            arInvoiceIn.Add(txtISITotal.Text);
            datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, arInvoiceIn);
        }//end of private void insertRECEIVEDIntoInvoiceIn()
        private void insertRECEIVEDIntoInventory()
        {
            ArrayList arInventory = new ArrayList();
            if (txtISIinvItem.Text!="")
            {
                arInventory.Add("0");          //store textBox / comboBox value in the ArrayList
                arInventory.Add(txtISIinvCode.Text);
                arInventory.Add(txtISIinvItem.Text.ToUpper());
                arInventory.Add(txtISIinvDescription.Text.ToUpper());
                arInventory.Add(txtISISupDescription.Text.ToUpper());//supplierDescription
                arInventory.Add(txtISIinvCategory.Text.ToUpper());
                if (txtISIinvReLevel.Text != "")     //See if user gave value for reorderLevel, and use value
                {
                    arInventory.Add(txtISIinvReLevel.Text);
                }//end of if (txtInvReLeveli.Text!="")
                else
                {
                    arInventory.Add("0");            //if user gave no value, make default 0: for prototyping purposes
                }//end of else, if (txtInvReLeveli.Text!="")
                if (cmbISIinvMarkup.Text != "")
                {
                    arInventory.Add(cmbISIinvMarkup.Text);       //if user gave value, use value
                }//end of if (cmbInvMarkupi.Text!="")
                else
                {
                    arInventory.Add("25");                   //if user gave no value for markup%, then make 25% default
                }//end of else, if (cmbInvMarkupi.Text!="")
                arInventory.Add("false");
                datac.insertCmd("Inventory", fieldInvAll, arInventory);       ////Send values in fieldInv string format through insertCmd query to database table
            }//end of if (txtISIinvItem.Text!="")
            else
            {
                MessageBox.Show(@"Please enter an Item value and the Quantity Received in the corresponding fields.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//end of else, if (txtISIinvItem.Text!="")
        }//end of private void insertRECEIVEDIntoInventory()
        private void insertRECEIVEDIntoInvoiceIn_Stock()
        {
            ArrayList arInvoiceIn = new ArrayList();
            ArrayList arGetInvoiceID = new ArrayList();
            DataTable dtLastStockInvoice = datac.getLastStockInvoice();
            string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"

            string arrLastStockInvoice = dtLastStockInvoice.Rows[0][0].ToString();
            string[] arrayInvoiceNo = arrLastStockInvoice.Split(' ');
            int stockInvNo = int.Parse(arrayInvoiceNo[1].ToString());

            arInvoiceIn.Add(0);
            arInvoiceIn.Add(arrayInvoiceNo[0].ToString() + " " + (stockInvNo + 1));
            arInvoiceIn.Add(dtpInvoiceDate.Text);
            arInvoiceIn.Add(cmbISISupplier.SelectedValue);
            if (txtISITotal.Text != "")
            {
                arInvoiceIn.Add(txtISITotal.Text);
            }//end of if (txtISITotal.Text!="")
            else
            {
                arInvoiceIn.Add("0.00");
            }//end of else, if (txtISITotal.Text!="")
          //  arInvoiceIn.Add(txtISITotal.Text);
            datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, arInvoiceIn);

            txtInvoiceNo.Text = arrayInvoiceNo[0].ToString() + " " + (stockInvNo + 1);
        }//end of private void insertRECEIVEDIntoInvoiceIn_Stock()
        private void updateRECEIVEDInventoryStock()
        {
            ArrayList arTotalStock = new ArrayList();
            if (txtISIstockReceived.Text!="")
            {
                arTotalStock.Add(0);
                arTotalStock.Add(txtISIinvID.Text);
                arTotalStock.Add((int.Parse(txtISIstockReceived.Text) + int.Parse(txtISIstockTotal.Text)));
                datac.updateRecCmd("InventoryStock", fieldTotalStock[1].ToString(), txtISIinvID.Text, fieldTotalStock, arTotalStock);//changing totalStock
            }//end of if (txtISIstockReceived.Text!="")
            else
            {
                MessageBox.Show(@"Please enter a Quantity Received value in the corresponding field.
The Quantity has been set to the default value of 0.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//end of else, if (txtISIstockReceived.Text!="")
        }//end of private void updateRECEIVEDInventoryStock()
        private void updateRECEIVEDStockIn()
        {
            DataTable dtFIFOInvoice = new DataTable();
            ArrayList arStockIn = new ArrayList();

            dtFIFOInvoice = datac.getFIFODatedPrice(txtISIinvID.Text);

            arStockIn.Add(dtFIFOInvoice.Rows[0][0].ToString());
            arStockIn.Add(dtFIFOInvoice.Rows[0][1].ToString());
            arStockIn.Add(dtFIFOInvoice.Rows[0][3].ToString());
            arStockIn.Add(dtFIFOInvoice.Rows[0][4].ToString());
            arStockIn.Add(dtFIFOInvoice.Rows[0][5].ToString());
            arStockIn.Add(int.Parse(dtFIFOInvoice.Rows[0][6].ToString()) + +int.Parse(txtISIstockReceived.Text));
            datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtFIFOInvoice.Rows[0][0].ToString(), fieldStockIN, arStockIn);//update existing record
        }//end of private void updateRECEIVEDStockIn()
        private void updateRECEIVEDInventory()
        {
            ArrayList arInvValues = new ArrayList();
            arInvValues.Add(txtISIinvID.Text);          //store textBox / comboBox value in the ArrayList
            arInvValues.Add(txtISIinvCode.Text);
            arInvValues.Add(txtISIinvItem.Text.ToUpper());
            arInvValues.Add(txtISIinvDescription.Text.ToUpper());
            arInvValues.Add(txtISISupDescription.Text.ToUpper());
            arInvValues.Add(txtISIinvCategory.Text.ToUpper());
            arInvValues.Add(txtISIinvReLevel.Text);
            arInvValues.Add(cmbISIinvMarkup.Text);
            arInvValues.Add("false");

            datac.updateRecCmd("Inventory", fieldInv[0], txtISIinvID.Text, fieldInvAll, arInvValues);        //Send values in fieldInv string format from textBox/ comboBox through updateCmd query to database table using InvID as key            
            //  dtgStockIn.DataSource = datac.getTable("Inventory");
        }//end of private void updateRECEIVEDInventory()
        private void invoiceExist_IDexists()
        {
            if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
            {
                insertRECEIVEDIntoStockIn(txtISIinvID.Text);
                test_Update_Insert_InventoryStock();
            }//end of if (txtISIstockPrice.Text!="" &&txtISIstockReceived.Text!="")
            else
            {
                MessageBox.Show(@"Please enter a Price/Unit value and Quantity Received value in the corresponding fields.
                A reference to the Invoice has been created.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//end of else, if (txtISIstockPrice.Text!="" &&txtISIstockReceived.Text!="")
        }//end of private void invoiceExist_IDexists()
        private void invoiceExist_LookupID()
        {
            string getInvID = "";
            Boolean stockIDExists = false;
            DataTable dtGetStock = new DataTable();
            DataTable dtGetInventory = new DataTable();
            Boolean invIDExists = false;

            dtGetInventory = datac.getTable("Inventory");
            dtGetStock = datac.getTable("InventoryStock");

            invIDExists = testDTIfExistAddOne(dtGetInventory, 2, txtISIinvItem.Text);//see if the item is present
            if (invIDExists == true)
            {//when item exists but ID is wrong, get correct ID
                getInvID = getInventoryID();
                stockIDExists = testDTIfExistAddOne(dtGetStock, 1, txtISIinvID.Text);
                if (stockIDExists == true)
                {//if the relevant inventory ID is found in the inventoryStock table (only update that record)
                    updateRECEIVEDInventoryStock();
                }//end of if (stockIDExists == true)
                else
                {//if the relevant inventory ID was NOT found in the inventoryStock table (do an insert to create a reference)
                    insertRECEIVEDIntoInventoryStock();
                }//end of else if (stockIDExists == true)
                if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
                {
                    insertRECEIVEDIntoStockIn(txtISIinvID.Text);
                }//end of if (txtISIstockPrice.Text!="" &&txtISIstockReceived.Text!="")
                else
                {
                    MessageBox.Show(@"Please enter a Price/Unit value and Quantity Received value in the corresponding fields.
                A reference to the Invoice has been created.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//end of else, if (txtISIstockPrice.Text!="" &&txtISIstockReceived.Text!="")
            }//end of if (invIDExists == true)
            else
            {
                MessageBox.Show("ID could not be found.");
            }//end of else if (invIDExists == true)
        }//end of  private void invoiceExist_LookupID()
        private void invoiceExist_IDnotExist()
        {
            Boolean invIDExists = false;
            //Boolean stockIDExists = false;
            string getInvID = "";
            DataTable dtGetInventory = new DataTable();
            DataTable dtGetStock = new DataTable();
            DataTable dtGetInvoice = new DataTable();
            dtGetInventory = datac.getTable("Inventory");
            dtGetStock = datac.getTable("InventoryStock");
            invIDExists = testNewItemExist();
            if (invIDExists == true)
            {//if the item was found, get the correct ID

                if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
                {
                    insertRECEIVEDIntoStockIn(txtISIinvID.Text);
                    test_Update_Insert_InventoryStock();
                }//end of if (txtISIstockPrice.Text!="" &&txtISIstockReceived.Text!="")
                else
                {
                    MessageBox.Show(@"Please enter a Price/Unit value and Quantity Received value in the corresponding fields.
                A reference to the Invoice has been created.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//end of else, if (txtISIstockPrice.Text!="" &&txtISIstockReceived.Text!="")
            }//end of if (invIDExists == true)
            else
            {//no id found for this item
                if (txtISIinvItem.Text!="")
                {
                    if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
                    {
                        MessageBox.Show("ID could not found. Creating new");
                        insertRECEIVEDIntoInventory();
                        getInvID = getInventoryID();
                        insertRECEIVEDIntoStockIn(txtISIinvID.Text);
                        test_Update_Insert_InventoryStock();
                    }//end of if (txtISIstockPrice.Text!="" &&txtISIstockReceived.Text!="")
                    else
                    {
                        MessageBox.Show(@"Please enter a Price/Unit value and Quantity Received value in the corresponding fields.
A reference to the Invoice has been created.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }//end of else, if (txtISIstockPrice.Text!="" &&txtISIstockReceived.Text!="")
                }//end of if (txtISIinvItem.Text!="")
                else
                {
                    MessageBox.Show(" No Item Found... Please provide an item.");
                }//end of else, if (txtISIinvItem.Text!="")
            }//end of else if (invIDExists == true)
        }//end of  private void invoiceExist_IDnotExist()
        private void invoiceNOTexist_IDexist()
        {
            //Boolean stockIDExists = false;
            //DataTable dtGetStock = new DataTable();
            //dtGetStock = datac.getTable("InventoryStock");
            //stockIDExists = testDTIfExistAddOne(dtGetStock, 1, txtISIinvID.Text);
            //if (stockIDExists == true)
            //{//if the relevant inventory ID is found in the inventoryStock table (only update that record)
            //    updateRECEIVEDInventoryStock();
            //}//end of if (stockIDExists == true)
            //else
            //{//if the relevant inventory ID was NOT found in the inventoryStock table (do an insert to create a reference)
            //    insertRECEIVEDIntoInventoryStock();
            //}//end of else if (stockIDExists == true)
            doInsertIntoInvoiceAndStock();
            //insertRECEIVEDIntoInvoiceIn();
            //insertRECEIVEDIntoStockIn(txtISIinvID.Text);
        }//end of private void invoiceNOTexist_IDexist()
        private void invoiceNOTexist_LookupID()
        {
            Boolean invIDExists = false;
            Boolean stockIDExists = false;
            string getInvID = "";
            DataTable dtGetInventory = new DataTable();
            DataTable dtGetStock = new DataTable();
            //do stuff
            dtGetInventory = datac.getTable("Inventory");
            dtGetStock = datac.getTable("InventoryStock");

            invIDExists = testDTIfExistAddOne(dtGetInventory, 2, txtISIinvItem.Text);//see if the item is present
            if (invIDExists == true)
            {//when item exists but ID is wrong, get correct ID
                getInvID = getInventoryID();
                stockIDExists = testDTIfExistAddOne(dtGetStock, 1, txtISIinvID.Text);
                //if (stockIDExists == true)
                //{//if the relevant inventory ID is found in the inventoryStock table (only update that record)
                //    updateRECEIVEDInventoryStock();
                //}//end of if (stockIDExists == true)
                //else
                //{//if the relevant inventory ID was NOT found in the inventoryStock table (do an insert to create a reference)
                //    insertRECEIVEDIntoInventoryStock();
                //}//end of else if (stockIDExists == true)
                doInsertIntoInvoiceAndStock();
                //insertRECEIVEDIntoInvoiceIn();
                //insertRECEIVEDIntoStockIn(txtISIinvID.Text);
            }//end of if (invIDExists == true)
            else
            {
                MessageBox.Show("ID could not be found.");
            }//end of else if (invIDExists == true)
        }//end of  private void invoiceNOTexist_LookupID()
        private void invoiceNOTexist_IDnotExist()
        {
            Boolean invIDExists = false;
            Boolean stockIDExists = false;
            string getInvID = "";
            DataTable dtGetInventory = new DataTable();
            DataTable dtGetStock = new DataTable();
            //do stuff
            dtGetInventory = datac.getTable("Inventory");
            dtGetStock = datac.getTable("InventoryStock");
            invIDExists = testNewItemExist();
            if (invIDExists == true)
            {//when item exists but ID is wrong, get correct ID
                getInvID = getInventoryID();
                stockIDExists = testDTIfExist(dtGetStock, 1, txtISIinvID.Text);
                //if (stockIDExists == true)
                //{//if the relevant inventory ID is found in the inventoryStock table (only update that record)
                //    updateRECEIVEDInventoryStock();
                //}//end of if (stockIDExists == true)
                //else
                //{//if the relevant inventory ID was NOT found in the inventoryStock table (do an insert to create a reference)
                //    insertRECEIVEDIntoInventoryStock();
                //}//end of else if (stockIDExists == true)

                doInsertIntoInvoiceAndStock();
                //insertRECEIVEDIntoInvoiceIn();
                //insertRECEIVEDIntoStockIn(txtISIinvID.Text);
            }//end of if (invIDExists == true)
            else
            {
             //   IDnotExistCreateNewItem();
                if (txtISIinvItem.Text!="")
                {
                    MessageBox.Show("Making new item.");
                    //item is new
                    if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
                    {

                        insertRECEIVEDIntoInventory();
                        getInvID = getInventoryID();
                        // insertRECEIVEDIntoInventoryStock();
                        doInsertIntoInvoiceAndStock();
                        //insertRECEIVEDIntoInvoiceIn();
                        //insertRECEIVEDIntoStockIn(txtISIinvID.Text);
                    }//end of if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
                    else
                    {
                        MessageBox.Show(@"Please enter a Price/Unit value and Quantity Received value in the corresponding fields for this new item.", "New Item Save Unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }//end of else, if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
                }//end of if (txtISIinvItem.Text!="")
                else
                {
                    MessageBox.Show(" No Item Found... Please provide an item.");
                }//end of else, if (txtISIinvItem.Text!="")
            }//end of else if (invIDExists == true)
        }//end of  private void invoiceNOTexist_IDnotExist()
        private void invoiceEmpty_IDexist()
        {
            DataTable dtGetInventory = new DataTable();
            DataTable dtGetStock = new DataTable();
            //do stuff
            dtGetInventory = datac.getTable("Inventory");
          //  test_Update_Insert_InventoryStock();
            invoiceEmpty_IDExist_recirculate();
        }//end of private void invoiceEmpty_IDexist()
        private void invoiceEmpty_LookupID()
        {
            Boolean invIDExists = false;
            DataTable dtGetInventory = new DataTable();
            //do stuff
            dtGetInventory = datac.getTable("Inventory");
            invIDExists = testDTIfExistAddOne(dtGetInventory, 2, txtISIinvItem.Text);
            if (invIDExists == true)//check if name exists in the Inventory table
            {//if id exist for item, give correct id
                txtISIinvID.Text = getInventoryID();
               // test_Update_Insert_InventoryStock();
                invoiceEmpty_IDExist_recirculate();
            }//end of  if (invIDExists == true)
            else
            {//error
                MessageBox.Show("ID could not be found.");
                //consider calling insert method instead
                invoiceEmpty_IDnotExist();
            }//end of else  if (invIDExists == true)
        }//end of  private void invoiceEmpty_LookupID()
        private void invoiceEmpty_IDnotExist()
        {//item is new
            string getInvID = "";
            if (txtISIinvItem.Text!="")
            {
                if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
                {
                    insertRECEIVEDIntoInventory();
                    getInvID = getInventoryID();
                    // insertRECEIVEDIntoInventoryStock();
                    doInsertIntoInvoiceAndStock();
                }//end of if (txtInvoiceNo.Text!="" && txtISIstockPrice.Text!="" && txtISIstockReceived.Text!="")
                else
                {
                    MessageBox.Show(@"Please ensure all necessary fields are filled out.

Fields that may cause this record not to save: Quantity received and Price.

Record was not saved.

Please revise and try again.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//end of else, if (txtInvoiceNo.Text!="" && txtISIstockPrice.Text!="" && txtISIstockReceived.Text!="")
            }//end of if (txtISIinvItem.Text!="")
            else
            {
                MessageBox.Show(@"Please ensure all necessary fields are filled out.

Fields that may cause this record not to save: Item Name.

Record was not saved.

Please revise and try again.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//end of else, //end of if (txtISIinvItem.Text!="")
        }//end of  private void invoiceEmpty_IDnotExist()

        private void test_Update_Insert_InventoryStock()
        {
            Boolean stockIDExists = false;
            DataTable dtGetStock = new DataTable();
            dtGetStock = datac.getTable("InventoryStock");
            stockIDExists = testDTIfExist(dtGetStock, 1, txtISIinvID.Text);
            if (txtISIstockReceived.Text!="")
            {
                if (stockIDExists == true)
                {
                    updateRECEIVEDInventoryStock();
                }//end of if (stockIDExists == true)
                else
                {
                    insertRECEIVEDIntoInventoryStock();
                }//end of else if (stockIDExists == true)  
            }//end of if (txtISIstockReceived.Text!="")
            else
            {
                MessageBox.Show(@"Please enter a Quantity Received value in the corresponding field.
                A reference to the Invoice has been created.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//end of else if (txtISIstockReceived.Text!="")
           
        }//end of private void test_Update_Insert_InventoryStock()

        private void invoiceEmpty_IDExist_recirculate()
        {
            DataTable dtFifoDatedRec = new DataTable();
            DataTable dtAllStockIn=new DataTable();
            string[] StockInCol = { "SubStockINID", "InventoryID", "SSIStockLeft" };
            ArrayList updateStockValues = new ArrayList();
            Boolean exist = false;

            dtAllStockIn=datac.getTable("SubStockIn");
            exist = testDTIfExist(dtAllStockIn, 1, txtISIinvID.Text);
            if (exist==true)
            {//get oldest dated record that has a >0 value, then do update to add stock to it from stock received
                if (txtISIstockReceived.Text!="")
                {
                    updateRECEIVEDStockIn(); //this method already gets the fifo record and updates the QUANTITY
                    test_Update_Insert_InventoryStock();
                }//end of if (txtISIstockReceived.Text!="")
                else
                {
                    MessageBox.Show(@"Please enter a Quantity Received value in the corresponding field.
                A reference to the Invoice has been created.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//end of else, if (txtISIstockReceived.Text!="")
            }//end of if (exist==true)
            else
            {//there is no previous record to recirculate to.
                MessageBox.Show(@"No history of this item was found.

Therefore a 'STOCK' invoice will be created, since this is saved as stock recirculated,
and not as an invoice received from a supplier.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //do stock -invoice create here
                doInsertIntoInvoiceAndStock();
            }//end of else, if (exist==true)
        }//end of private void invoiceEmpty_IDExist_recirculate()

        private void doInsertIntoInvoiceAndStock()
        {
            if (txtISIinvID.Text!="")
            {
                if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
                {
                    if (txtInvoiceNo.Text=="")
                    {
                        insertRECEIVEDIntoInvoiceIn_Stock();
                    }//end of if (txtInvoiceNo.Text=="")
                    else
                    {
                        insertRECEIVEDIntoInvoiceIn();
                    }//end of else, if (txtInvoiceNo.Text=="")
                    insertRECEIVEDIntoStockIn(txtISIinvID.Text);
                    test_Update_Insert_InventoryStock();
                }//end of if (txtISIstockPrice.Text!="" &&txtISIstockReceived.Text!="")
                else
                {
                    MessageBox.Show(@"Please enter a Price/Unit value and Quantity Received value in the corresponding fields.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//end of else, if (txtISIstockPrice.Text!="" &&txtISIstockReceived.Text!="")  
            }//end of if (txtISIinvID.Text!="")
        }//end of private void doInsertIntoInvoiceAndStock()

        private void btnISISave_Click(object sender, EventArgs e)
        {
            //variables
            Boolean invoiceExists = false;
            Boolean invIDExists = false;
         //   Boolean stockIDExists = false;
            DataTable dtGetInventory = new DataTable();
            //do stuff
            dtGetInventory = datac.getTable("Inventory");
            if (txtInvoiceNo.Text != "")
            {//invoice number NOT empty
                invoiceExists = testInvoiceExist();
                if (invoiceExists == true)
                {//when invoice is looked up and was found as existed
                    DialogResult result = MessageBox.Show("this exists, add to it?", "exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {//clicked yes to use this invoice and add to it
                        if (txtISIinvID.Text != "")
                        {//the inventory ID is NOT empty
                            invIDExists = testDTIfExistAddOne(dtGetInventory, 0, txtISIinvID.Text);
                            if (invIDExists == true)
                            {//see if the inventory ID was found in the inventoryStock table
                                invoiceExist_IDexists();
                            }//end of if (stockIDExists == true)
                            else
                            {//this is where the ID is found as non existant, but consider that the ID is only wrong and item might exist
                                invoiceExist_LookupID();
                            }//end of else if (stockIDExists == true)
                        }//end of  if (txtISIinvID.Text != "")
                        else
                        {//if the ID textfield is completely empty, for example for new item
                            invoiceExist_IDnotExist();
                        }//end of else if (txtISIinvID.Text != "")
                    }//end of if (result == DialogResult.Yes)
                    else
                    {//result is NO to exist invoice, clear to make possible for new invoice
                        MessageBox.Show("Give new invoice ref");
                        txtInvoiceNo.Clear();
                        txtISITotal.Clear();
                        btnISIpreviewCurrent.Enabled = false;
                    }//end of else if (result == DialogResult.Yes)
                }//end of  if (invoiceExists == true)
                else
                {//invoice not exist
                    if (txtISIinvID.Text != "")
                    {//the inventory ID is NOT empty
                        invIDExists = testDTIfExistAddOne(dtGetInventory, 0, txtISIinvID.Text);
                        if (invIDExists == true)
                        {//see if the inventory ID was found in the inventoryStock table
                            invoiceNOTexist_IDexist();
                        }//end of if (stockIDExists == true)
                        else
                        {//this is where the ID is not empty, but is found as non existant, so consider that the ID is only wrong and item might exist
                            invoiceNOTexist_LookupID();
                        }//end of else if (stockIDExists == true)
                    }//end of  if (txtISIinvID.Text != "")
                    else
                    {//if the ID textfield is completely empty, for example for new item
                        invoiceNOTexist_IDnotExist();
                    }//end of else if (txtISIinvID.Text != "")
                }//end of else if (invoiceExists == true)
            }//end of if (txtInvoiceNo.Text != "")
            else
            {//empty invoice
                DialogResult resultStock = MessageBox.Show(@"Invoice Number field is empty.
                    Reciculate the stock?", "Empty invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultStock == DialogResult.Yes)
                {
                    if (txtISIinvID.Text != "")
                    {
                        invIDExists = testDTIfExistAddOne(dtGetInventory, 0, txtISIinvID.Text);
                        if (invIDExists == true)
                        {
                            invoiceEmpty_IDexist();
                        }//end of if (invIDExists == true)
                        else
                        {//look for correct id, is id is wrong
                            invoiceEmpty_LookupID();
                        }//end of else if (invIDExists == true)
                    }//end of  if (txtISIinvID.Text != "")
                    else
                    { //item is new
                        invoiceEmpty_IDnotExist();
                    }//end of else  if (txtISIinvID.Text != "")
                }//end of if (resultStock == DialogResult.Yes)
                else
                {//result click no, message
                    MessageBox.Show("Please provide an invoice number.");
                }//end of else if (resultStock == DialogResult.Yes)
            }//end of else if (txtInvoiceNo.Text != "")
            dtgStockIn.DataSource = datac.getTable("Inventory");//to do a datasource update after any modifications made to inventory
            btnISIpreviewCurrent.Enabled = true;
            btnISIrecordNewInvoice.Enabled = true;
            btnISIdelItemInvoice.Enabled = true;
        }//end of  private void btnISIDUMMY_Click(object sender, EventArgs e)
                
        private void btnISIinvoiceClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtInvoiceNo.Clear();
                txtISITotal.Text = "0.00";
                dtpInvoiceDate.Value = DateTime.Now;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing data from Invoice textBoxes : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch
        }//end of private void btnISIinvoiceClear_Click(object sender, EventArgs e)
        #endregion
        #endregion

        #region tabSupplier Controls
        private void btnSupSearch_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                string[] searchField = { "SupName" };
                #endregion

                btnSupINSERT.Enabled = false;
                btnSupSearch.Enabled = true;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;
                btnDeleteSup.Enabled = false;

                if (txtSName.Text != "")
                {
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
                    txtsupDiscount.Clear();
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
                dtgSuppliers.DataSource = datac.getTable("Supplier");

                btnSupINSERT.Enabled = true;
                btnSupSearch.Enabled = true;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;
                btnDeleteSup.Enabled = false;

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
                txtsupDiscount.Clear();
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
                btnDeleteSup.Enabled = true;

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
                txtsupDiscount.Text = dtgSuppliers.SelectedRows[0].Cells[14].Value.ToString();
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
                #region Variables
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                string fieldID = "SupplierID";
                #endregion

                btnSupINSERT.Enabled = false;
                btnSupSearch.Enabled = false;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = true;

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
                values.Add(cmbSupPayTerm.Text);//.DisplayMember);
                values.Add("false");
                if (txtsupDiscount.Text!="")
                {
                    values.Add(txtsupDiscount.Text);
                }//end of if (txtsupDiscount.Text!="")
                else
                {
                    values.Add(txtsupDiscount.Text);
                }//end of else, if (txtsupDiscount.Text!="")

                datac.updateRecCmd("Supplier", fieldID, txtSID.Text, fieldSup, values);        //Send values in fieldSup string format from textBox/ comboBox through updateCmd query to database table using InvID as key
                dtgSuppliers.DataSource = datac.getTable("Supplier");//get the entire table as datasource to dtg
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
                #region Variables
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                #endregion

                btnSupINSERT.Enabled = true;
                btnSupSearch.Enabled = false;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;

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
                if (txtsupDiscount.Text != "")
                {
                    values.Add(txtsupDiscount.Text);
                }//end of if (txtsupDiscount.Text!="")
                else
                {
                    values.Add(txtsupDiscount.Text);
                }//end of else, if (txtsupDiscount.Text!="")
           
                datac.insertCmd("Supplier", fieldSup, values);       ////Send values in fieldSup string format through insertCmd query to database table
                dtgSuppliers.DataSource = datac.getTable("Supplier");//refresh the datasource to display the newly inserted
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
                #region Variables
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                string fieldID = "SupplierID";
                #endregion

                btnSupINSERT.Enabled = false;
                btnSupSearch.Enabled = false;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;

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
                values.Add(txtsupDiscount.Text);
          
                datac.updateRecCmd("Supplier", fieldID, txtSID.Text, fieldSup, values);        //Send values in fieldSup string format from textBox/ comboBox through updateCmd query to database table using InvID as key and only update isDeleted column
                txtSID.Clear();
                txtSName.Clear();
                txtSContactP.Clear();
                txtSREP.Clear();
                txtSBusNr.Clear();
                txtSCell.Clear();
                txtSEmail.Clear();
                txtSAddress.Clear();
                txtSCity.Clear();
                txtSProv.Clear();
                txtSupPrefix.Clear();
                txtSupReTime.Clear();

                dtgSuppliers.DataSource = datac.getTable("Supplier");
                MessageBox.Show(@"This supplier has been marked as deleted.
It will still show in the grid, because there may be references to this record.", "Deleted Record!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                #region Variables
                //datatables
                DataTable testTotalStock = new DataTable();
                DataTable dtTotalStock = new DataTable();
                //arraylists
                ArrayList getIDValue = new ArrayList();
                //arrays
                string[] fieldTotal = { "InventoryID" };
                //variables
                Boolean exists = new Boolean();
              //  Boolean flag = new Boolean();
                #endregion

                txtSCinvID.Text = dtgCheckStock.SelectedRows[0].Cells[0].Value.ToString();
                txtSCinvCode.Text = dtgCheckStock.SelectedRows[0].Cells[1].Value.ToString();
                txtSCinvItem.Text = dtgCheckStock.SelectedRows[0].Cells[2].Value.ToString().ToUpper();
                txtSCinvDesc.Text = dtgCheckStock.SelectedRows[0].Cells[3].Value.ToString().ToUpper();
                txtSCinvCat.Text = dtgCheckStock.SelectedRows[0].Cells[5].Value.ToString().ToUpper();
                txtSCinvRelevel.Text = dtgCheckStock.SelectedRows[0].Cells[6].Value.ToString();

                txtSCactualTotal.Clear();

                //****************Get the stock total quantity from InventoryStock table, relevant to the InventoryID******************
                testTotalStock = datac.getTable("InventoryStock");
                exists = testDTIfExistAddOne(testTotalStock, 1, txtSCinvID.Text);

                // for (int i = 0; i < testTotalStock.Rows.Count+1; i++)
                //{
                //    if (exists == false)
                //    {
                //        if (testTotalStock.Rows[i][1].ToString() == txtSCinvID.Text)
                //        {
                //            exists = true;
                //        }//end of if (testTotalStock.Rows[i][1].ToString() == txtSCinvID.Text)
                //        else
                //        {
                //            exists = false;
                //        }//end of else of if (testTotalStock.Rows[i][1].ToString() == txtSCinvID.Text) 
                //    }//end of if (exists == false)
                //    else
                //    {
                //        flag = true;
                //    }//end of else of if (exists == false)
                //}//end of for (int i = 0; i < testTotalStock.Rows.Count; i++)

                //here the flag will be tested, doing the actual determining of what should be done.
                if (exists == true)
                {//if the flag is true, meaning the record already exists, will then get references
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
                }//end of if (flag == true)
                else
                {
                    MessageBox.Show(@"Please log the stock in the STOCK IN tab as stock received FIRST, to ensure that all the correct references are established.", "No References Were Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSCsave.Enabled = false; //disable button so that nothing can happen- force to either choose another item or go to StockIN
                }//end of else of if (flag == true)
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
                #region Variables
                //datatables
                DataTable dtTempStockIN = new DataTable();
                DataTable dtTotalStock = new DataTable();
                //arraylists
                ArrayList updateStock = new ArrayList();
                ArrayList updateStockIn = new ArrayList();
                ArrayList addToOUT = new ArrayList();
                ArrayList getIDValue = new ArrayList();
                //arrays
                string[] fieldTotal = { "InventoryID" };
                //variables
                int difference = 0;
                #endregion

                getIDValue.Add("=" + txtSCinvID.Text);
                
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
                                     updateStockIn.Add(dtTempStockIN.Rows[0][5].ToString());
                                     updateStockIn.Add(int.Parse(dtTempStockIN.Rows[0][6].ToString())-1);
                                     datac.updateRecCmd("SubStockIN", fieldStockIN[0],dtTempStockIN.Rows[0][0].ToString(),fieldStockIN,updateStockIn);
                                 }//end of if (int.Parse(dtSubStockIN.Rows[0][2].ToString())>0)
                                 else
                                 {//this is for when the last loaded record's LEFT has run to 0 and the counter is not yet 0
                                     dtTempStockIN = datac.getFIFODatedPrice(txtSCinvID.Text);
                                     updateStockIn.Add(dtTempStockIN.Rows[0][0].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][1].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][2].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][3].ToString());
                                     updateStockIn.Add(dtTempStockIN.Rows[0][5].ToString());
                                     updateStockIn.Add(int.Parse(dtTempStockIN.Rows[0][6].ToString()) - 1);
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
                            updateStockIn.Add(dtTempStockIN.Rows[0][5].ToString());
                            updateStockIn.Add(int.Parse(dtTempStockIN.Rows[0][6].ToString()) + 1);
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
                dtgCheckStock.DataSource = datac.getTable("Inventory");
                dtgInventoryValue.DataSource = datac.getInventoryValue();
                dtgSupOrderList.DataSource = datac.getLowStock();

                //to calculate total value of stock based on quantity and price on inventoryValue tab****
                displayInventoryValue();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Stock Taking: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSCsave_Click(object sender, EventArgs e)

        private void btnClearSCinv_Click(object sender, EventArgs e)
        {
            try
            {               
                txtSCinvID.Clear();
                txtSCinvCode.Clear();
                txtSCinvItem.Clear();
                txtSCinvDesc.Clear();
                txtSCinvCat.Clear();
                txtSCinvRelevel.Clear();
                txtSCactualTotal.Clear();
                txtSCsysTotal.Clear();

                dtgCheckStock.DataSource = datac.getInventoryValue();
                dtgInventoryValue.DataSource = datac.getInventoryValue();
                displayInventoryValue();
                dtgSupOrderList.DataSource = datac.getLowStock();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing textboxes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnClearSCinv_Click(object sender, EventArgs e)
        #endregion

        #region tabOrderStock
        private void btnOrderSupGO_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                //datatables
                DataTable orderNum = new DataTable();
                DataTable orderSupDetails = new DataTable();
                DataTable ordersTable = new DataTable();
                //arraylists
                ArrayList supplierIDvalue = new ArrayList();
                //arrays
                string[] supplierIDfield = { "SupplierID" };
                //variables
                string year = "";
                string finalOrderNumber = "";
                Boolean exists = false;
             //   Boolean flag = new Boolean();
                string newOrdernumber = "0001";
                #endregion

                //give the dtg new datasource from dropdownbox with supplier as filter
                //construct the order number here
                //test if there any previous orders from this supplier (while this supplier exists)
                //first get the table of orders

                ordersTable = datac.getTable("Orders");
                exists = testDTIfExist(ordersTable, 2, cbxOrderSup.SelectedValue.ToString());              
                ////run through all the orders

                //do something if there is a history
                if (exists == true)
                {
                    supplierIDvalue.Add(cbxOrderSup.SelectedValue.ToString());
                    year = DateTime.Now.ToString("yy");//get the date for the year block in the orderNumber-string
                    orderSupDetails = datac.getRecord("Supplier", supplierIDfield, supplierIDvalue);
                    orderNum = datac.getLastOrderNumber(cbxOrderSup.SelectedValue.ToString()); //get the last ordernumber for specific supplier

                    finalOrderNumber = "IT" + "-" + year + "-" + orderSupDetails.Rows[0][10].ToString() + "-" + (int.Parse(orderNum.Rows[0][0].ToString()) + 1);
                    txtOrderNumber.Text = finalOrderNumber;
                    //  dtgSupOrderHistory.DataSource = datac.getSupplierOrderHistory(cbxOrderSup.SelectedValue.ToString());
                }//end of if (flag==true)
                //do something if there is NO history
                else
                {
                    supplierIDvalue.Add(cbxOrderSup.SelectedValue.ToString());
                    year = DateTime.Now.ToString("yy");//get the date for the year block in the orderNumber-string
                    orderSupDetails = datac.getRecord("Supplier", supplierIDfield, supplierIDvalue);

                    finalOrderNumber = "IT" + "-" + year + "-" + orderSupDetails.Rows[0][10].ToString() + "-" + newOrdernumber;
                    txtOrderNumber.Text = finalOrderNumber;
                    MessageBox.Show(@"No order history found for the SUPPLIER selected. 
                Create order 0001?", "No History Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // dtgSupOrderHistory.DataSource = datac.getAllOrderHistory();          //new datasource?
                }//end of else of if (flag==true)
                btnOrderSavePDF.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error generating order number: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnOrderSupGO_Click(object sender, EventArgs e)

        private void btnOrderFilter_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                //datatables
                DataTable orderSupDetails = new DataTable();
                DataTable ordersTable = new DataTable();
                //arraylists
                ArrayList supplierIDvalue = new ArrayList();
                //arrays
                string[] supplierIDfield = { "SupplierID" };
                //variables
                Boolean exists = false;
              //  Boolean flag = new Boolean();
                #endregion

                //give the dtg new datasource from dropdownbox with supplier as filter
                //test if there are any previous orders from this supplier (while this supplier exists)
                //first get the table of orders
               // flag = false;
                ordersTable = datac.getTable("Orders");
                exists = testDTIfExist(ordersTable, 2, cbxOrderSup.SelectedValue.ToString());              



                //do something if there is a history
                if (exists == true)
                {
                    dtgSupOrderHistory.DataSource = datac.getSupplierOrderHistory(cbxOrderSup.SelectedValue.ToString());
                }//end of if (flag==true)
                //do something if there is NO history
                else
                {
                    MessageBox.Show(@"No order history is found for the SUPPLIER selected.", "No History Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtgSupOrderHistory.DataSource = datac.getAllOrderHistory();
                }//end of else of if (flag==true)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading and updating drag/ drop data from datagridview into order grid: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of  catch (Exception ex)
        }//end of private void btnOrderFilter_Click(object sender, EventArgs e)
        
        private void dtgSupOrderHistory_Click(object sender, EventArgs e)
        {
            try
            {
                //give the details dtg a new datasource depended on the order selected
                //make if to see if there is at all any history of this supplier.
                dtgSupOrderList.DataSource = datac.getSupOrderDetails(dtgSupOrderHistory.SelectedRows[0].Cells[4].Value.ToString());
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data into datagridview: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void dtgSupOrderHistory_Click(object sender, EventArgs e)

        //begin drag and drop methods
        void dtgOrderInvList_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                #region Variables
                //datatables
                DataTable maxPrice = new DataTable();//use datatable to save the maxDatedPrice query results in, to fetch latest price on item dragged to current dtg.
                //variables
                Boolean exists = new Boolean();
                //other
                DataGridViewRow row = e.Data.GetData(typeof(DataGridViewRow))
                as DataGridViewRow;
                #endregion

                //Note: use exist-testing first, then look for id, then change column in the dtg.
                if (row != null)
                {
                    DataGridViewRow newrow = row.Clone() as DataGridViewRow;
                    for (int i = 0; i < newrow.Cells.Count; i++)
                    {//get whatever is in the 1st dtg, then place it in the row that will go into 2nd dtg
                        newrow.Cells[i].Value = row.Cells[i].Value;
                    }//end of for (int i = 0; i < newrow.Cells.Count; i++)
                    //this part is to change the price column to latest price on system, first testing if there is an invoice to reference it to.
                    maxPrice = datac.getTable("SubStockIN");
                    //  maxPrice = datac.getMaxDatedPrice(newrow.Cells[0].Value.ToString());//the price is on space 4
                    exists = testDTIfExistAddOne(maxPrice, 1, newrow.Cells[0].Value.ToString());
                    if (exists == true)
                    {//if the flag is true, meaning the record already exists, will then get references
                        maxPrice = datac.getMaxDatedPrice(newrow.Cells[0].Value.ToString());//the price is on space 4
                        newrow.Cells[7].Value = maxPrice.Rows[0][4].ToString();//this line updates the price column to the last recorded invoice price received
                        dtgOrderInvList.Rows.Add(newrow);//assuming this actually puts the row in the dtg
                    }//end of if (flag == true)
                    else
                    {
                        MessageBox.Show(@"No previous invoices were found to update the prices with, therefor the previous order from which this was taken will be used for the price.
Please ensure prices are updated regularly.", "Price Update Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //with no invoice reference, use the drag destination price
                        dtgOrderInvList.Rows.Add(newrow);//assuming this actually puts the row in the dtg
                    }//end of else of if (flag == true)
                }//end of  if (row != null)
                //This part below is to calculate the running total for this order in the current dtg
                calculateOrderTotal();
                btnOrderSavePDF.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error in dragDrop method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of  void dtgOrderInvList_DragDrop(object sender, DragEventArgs e)
        void dtgOrderInvList_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
                {
                    e.Effect = DragDropEffects.Copy;
                }//end of if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error in dragEnter method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of void dtgOrderInvList_DragEnter(object sender, DragEventArgs e)
        void dtgSupOrderList_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.dtgSupOrderList.DoDragDrop(this.dtgSupOrderList.CurrentRow, DragDropEffects.All);
                }//end of if (e.Button == MouseButtons.Left)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error in MouseMove method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of void dtgSupOrderList_MouseMove(object sender, MouseEventArgs e)
        //end drag and drop methods

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            try
            {//this adds the entire dtg1 to dtg2 as is and recalculates the dtg
                #region Variables
                DataGridViewRow row = new DataGridViewRow();
                DataTable maxPrice = new DataTable();//use datatable to save the maxDatedPrice query results in, to fetch latest price on item dragged to current dtg.
                Boolean exists = new Boolean();
                #endregion
                    for (int iCnt = 0; iCnt < dtgSupOrderList.Rows.Count; iCnt++) 
                    {
                        row = (DataGridViewRow)dtgSupOrderList.Rows[iCnt].Clone();
                        int iColIndex = 0;
                        foreach (DataGridViewCell cell in dtgSupOrderList.Rows[iCnt].Cells)
                        {
                            row.Cells[iColIndex].Value = cell.Value;
                            iColIndex += 1;
                        }//end of foreach (DataGridViewCell cell in dtgSupOrderList.Rows[iCnt].Cells)
                      //  dtgOrderInvList.Rows.Add(row);
                        //this part will make it get the newest price for each item
                        maxPrice = datac.getTable("SubStockIN");
                        //  maxPrice = datac.getMaxDatedPrice(newrow.Cells[0].Value.ToString());//the price is on space 4
                        exists = testDTIfExistAddOne(maxPrice, 1, dtgSupOrderList.Rows[iCnt].Cells[0].Value.ToString());
                         if (exists == true)
                         {//if the flag is true, meaning the record already exists, will then get references
                             maxPrice = datac.getMaxDatedPrice(dtgSupOrderList.Rows[iCnt].Cells[0].Value.ToString());//the price is on space 4
                             row.Cells[7].Value = maxPrice.Rows[0][4].ToString();//this line updates the price column to the last recorded invoice price received
                             dtgOrderInvList.Rows.Add(row);//assuming this actually puts the row in the dtg
                         }//end of if (flag == true)
                         else
                         {
                             MessageBox.Show(@"No previous invoices were found to update the prices with, therefor the previous order from which this was taken will be used for the price.
Please ensure prices are updated regularly.", "Price Update Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                             //with no invoice reference, use the drag destination price
                         }//end of else of if (flag == true)
                    }//end of for (int iCnt = 0; iCnt < dtgSupOrderList.Rows.Count; iCnt++) 
                    //This part below is to calculate the running total for this order in the current dtg
                    calculateOrderTotal();
                    btnOrderSavePDF.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Select All data from datagridview into order grid: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnAddAll_Click(object sender, EventArgs e)

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {//this clears the entire dtg and recalculates the dtg as values with 0.00
                dtgOrderInvList.Rows.Clear();
                btnOrderSavePDF.Enabled = false;
                //This part below is to calculate the running total for this order in the current dtg
                calculateOrderTotal();
            }//end of try 
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch
        }//end of private void btnRemoveAll_Click(object sender, EventArgs e)

        private void dtgOrderInvList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {//this is to calculate the total when a row is removed from the dtg
                calculateOrderTotal();
                btnOrderSavePDF.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch (Exception ex)
        }//end of  private void dtgOrderInvList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        
        private void dtgOrderInvList_Click(object sender, EventArgs e)
        {
            try
            {//this is so that the total can still be calculated even after an edit or manual row inserted, since nothing else has to happen on click to this dtg
                #region Variables
                double orderTotal = 0.00;
                double vat = datac.getVATrate();
                #endregion

                for (int i = 0; i < dtgOrderInvList.Rows.Count - 1; i++)
                {
                    if (dtgOrderInvList.Rows[i].Cells[7].Value != null)
                    {
                        orderTotal += (int.Parse(dtgOrderInvList.Rows[i].Cells[6].Value.ToString()) * double.Parse(dtgOrderInvList.Rows[i].Cells[7].Value.ToString()));
                    }//end of if (dtgOrderInvList.Rows[i].Cells[7].Value!=null)
                }//end of for (int i = 0; i < dtgOrderInvList.Rows.Count - 1; i++)
                txtOrderTotalExcl.Text = double.Parse(orderTotal.ToString()).ToString("C");
                txtOrderVAT.Text = (double.Parse(orderTotal.ToString()) * vat).ToString("C");
                txtOrderTotalIncl.Text = (double.Parse(orderTotal.ToString()) * (1+vat)).ToString("C");
                btnOrderSavePDF.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch catch (Exception ex)
        }//end of private void dtgOrderInvList_Click(object sender, EventArgs e)
      
        private void calculateOrderTotal()
        {
            #region Variables
            double orderTotal = 0.00;
            double vat = datac.getVATrate();
            #endregion

            //This part below is to calculate the running total for this order in the current dtg
            for (int i = 0; i < dtgOrderInvList.Rows.Count - 1; i++)
            {
                orderTotal += (int.Parse(dtgOrderInvList.Rows[i].Cells[6].Value.ToString()) * double.Parse(dtgOrderInvList.Rows[i].Cells[7].Value.ToString()));
            }//end of for (int i = 0; i < dtgOrderInvList.Rows.Count - 1; i++)
            txtOrderTotalExcl.Text = double.Parse(orderTotal.ToString()).ToString("C");
            txtOrderVAT.Text = (double.Parse(orderTotal.ToString()) * vat).ToString("C");
            txtOrderTotalIncl.Text = (double.Parse(orderTotal.ToString()) * (1+vat)).ToString("C");
        }//end of private void calculateOrderTotal()
        #endregion

        #region PDF Controls
        private void btnPDForder_Click(object sender, EventArgs e)
        {
            try
            {//does all the gathering for the creating of the pdf, and then saves the records in the relevant tables
                #region PDF Variables
                //datatable
                DataTable dtInvValues = new DataTable();        //this variable is the datatable that will contain all the details of the inventory being ordered
                DataTable dtSupValues = new DataTable();
                //arrayList
                ArrayList val = new ArrayList();
                //array
                string[] field = { "SupplierID" };
                //variable
                string fileName = txtOrderNumber.Text;
                string getID = cbxOrderSup.SelectedValue.ToString();
                #endregion
                #region Variables for Order Recording
                //datatable
                DataTable dtSupplierList = new DataTable();
                DataTable getOrderRef = new DataTable();
                DataTable getInvID = new DataTable();
                //arrayList
                ArrayList getSupPrefix = new ArrayList();
                ArrayList ordersValues = new ArrayList();
                ArrayList invOrdered = new ArrayList();
                ArrayList orderRefValues = new ArrayList();
                ArrayList newInvItem = new ArrayList();
                ArrayList newStockItem = new ArrayList();
                ArrayList searchNewID = new ArrayList();
                ArrayList recordItemOrder = new ArrayList();
                //array
                string[] arrayOrderNo = fileName.Split('-');
                string[] searchField = { "SupPrefix" };
                string[] searchOrderRef = { "OrderNumber", "SupplierID" };
                string[] searchNewInv = { "InvCode", "InvItem", "InvDescription", "InvSupplierDescription" };
                //variable
                string supPrefix = arrayOrderNo[2].ToString();//here is the supplier prefix
                int orderNoDigit = int.Parse(arrayOrderNo[3].ToString());//here is the order number digits
                double orderTotal = 0.00;
                double vat = datac.getVATrate();
                #endregion
                // dtInvValues = (DataTable)dtgSupOrderList.DataSource;
                // dtInvValues = datac.getStockInInvoice(getID);

                DialogResult result = MessageBox.Show("Do you wish to save this order?", "Save Order", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewColumn header in dtgOrderInvList.Columns)
                    {
                        dtInvValues.Columns.Add(header.Name);
                    }//end of foreach (DataGridViewColumn header in dtgOrderInvList.Columns)
                    foreach (DataGridViewRow row in dtgOrderInvList.Rows)
                    {
                        if (row.IsNewRow)
                            continue;
                        DataRow dtRow = dtInvValues.NewRow();
                        for (int i1 = 0; i1 < dtgOrderInvList.Columns.Count; i1++)
                            dtRow[i1] = (row.Cells[i1].Value == null ? DBNull.Value : row.Cells[i1].Value);
                        dtInvValues.Rows.Add(dtRow);
                    }//end of foreach (DataGridViewRow row in dtgOrderInvList.Rows)

                    //get the applicable supplier details
                    val.Add(getID);
                    dtSupValues = datac.getRecord("Supplier", field, val);

                    if (dtInvValues != null)
                    {
                        getSupPrefix.Add(supPrefix);
                        dtSupplierList = datac.getRecord("Supplier", searchField, getSupPrefix);//here we have the supplier filtered by prefix

                        for (int i = 0; i < dtgOrderInvList.Rows.Count - 1; i++)
                        {//this gets the estimate cost for this order
                            orderTotal += (int.Parse(dtgOrderInvList.Rows[i].Cells[6].Value.ToString()) * double.Parse(dtgOrderInvList.Rows[i].Cells[7].Value.ToString()));
                        }//end of for (int i = 0; i < dtgOrderInvList.Rows.Count - 1; i++)
                        orderTotal = orderTotal * (1+vat);

                        ordersValues.Add(0);
                        ordersValues.Add(orderNoDigit);
                        ordersValues.Add(dtSupplierList.Rows[0][0].ToString());
                        ordersValues.Add(DateTime.Now);
                        ordersValues.Add(orderTotal);
                        datac.insertCmd("Orders", fieldOrders, ordersValues);       //do the first insert: into orders

                        orderRefValues.Add(" =" + orderNoDigit);
                        orderRefValues.Add(" =" + dtSupplierList.Rows[0][0].ToString());
                        getOrderRef = datac.getMathRecord("Orders", searchOrderRef, orderRefValues);//get the ID reference for the order just created

                        foreach (DataRow dtRow in dtInvValues.Rows)
                        {
                            if (dtRow["invID"].ToString() != "")
                            {
                                invOrdered.Add(0);
                                invOrdered.Add(dtRow["invID"].ToString());
                                if (dtRow["orderQuantity"].ToString() != "")
                                {
                                    invOrdered.Add(dtRow["orderQuantity"].ToString());
                                }// end of if (dtRow["orderQuantity"].ToString()!="")
                                else
                                {
                                    MessageBox.Show("Please enter a quantity to be ordered.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }//end of else, if (dtRow["orderQuantity"].ToString()!="")
                                if (dtRow["orderPrice"].ToString() != "")
                                {
                                    invOrdered.Add(dtRow["orderPrice"].ToString());
                                }// end of if (dtRow["orderPrice"].ToString()!="")
                                else
                                {
                                    invOrdered.Add(0.00);
                                }//end of else, if (dtRow["orderPrice"].ToString()!="")
                                invOrdered.Add(getOrderRef.Rows[0][0].ToString());//get the order reference that was created.
                                if (dtRow["invLength"].ToString() != "")
                                {
                                    invOrdered.Add(dtRow["invLength"].ToString());
                                }// end of if (dtRow["invLength"].ToString()!="")
                                else
                                {
                                    invOrdered.Add(0.00);
                                }//end of else, if (dtRow["invLength"].ToString()!="")

                                datac.insertCmd("SubOrders", fieldSubOrders, invOrdered);//insert this item in the created order
                                invOrdered = new ArrayList();//clear the arraylist for the next item
                            }//end of if (dtRow["ID"].ToString() !="")
                            else
                            {
                                //create a new reference ID in the inventory table, by first inserting new item into table
                                //then insert into the StockTotal table with new ID
                                //then do final insert into subOrder
                                //since there is nothing being received at all yet, no need to insert into substock- stock and price (invoiced price) will be entered when the invoice is received
                                newInvItem.Add(0);
                                if (dtRow["invCode"].ToString() != "")
                                {
                                    newInvItem.Add(dtRow["invCode"].ToString());
                                }// end of if (dtRow["invCode"].ToString()!="")
                                else
                                {
                                    newInvItem.Add("");
                                }//end of else, if (dtRow["invCode"].ToString()!="")
                                if (dtRow["invItem"].ToString() != "")
                                {
                                    newInvItem.Add(dtRow["invItem"].ToString());
                                }// end of if (dtRow["invItem"].ToString()!="")
                                else
                                {
                                    MessageBox.Show("Please enter a name for the item being ordered.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }//end of else, if (dtRow["invItem"].ToString()!="")
                                if (dtRow["invDescription"].ToString() != "")
                                {
                                    newInvItem.Add(dtRow["invDescription"].ToString());
                                }// end of if (dtRow["invDescription"].ToString()!="")
                                else
                                {
                                    newInvItem.Add("");
                                }//end of else, if (dtRow["invDescription"].ToString()!="")
                                if (dtRow["invSupDescription"].ToString() != "")
                                {
                                    newInvItem.Add(dtRow["invSupDescription"].ToString());
                                }// end of if (dtRow["invSupDescription"].ToString()!="")
                                else
                                {
                                    newInvItem.Add("");
                                }//end of else, if (dtRow["invSupDescription"].ToString()!="")
                                newInvItem.Add("");
                                newInvItem.Add(1);
                                newInvItem.Add(25);
                                newInvItem.Add("false");

                                datac.insertCmd("Inventory", fieldInvAll, newInvItem);//create an ID (reference) in the inventory table for this new item
                                newInvItem = new ArrayList();//clear the arraylist for the next item

                                if (dtRow["invCode"].ToString() != "")
                                {
                                    searchNewID.Add(dtRow["invCode"].ToString());
                                }// end of if (dtRow["invCode"].ToString()!="")
                                else
                                {
                                    searchNewID.Add("");
                                }//end of else, if (dtRow["invCode"].ToString()!="")
                                searchNewID.Add(dtRow["invItem"].ToString());
                                if (dtRow["invDescription"].ToString() != "")
                                {
                                    searchNewID.Add(dtRow["invDescription"].ToString());
                                }// end of if (dtRow["invDescription"].ToString()!="")
                                else
                                {
                                    searchNewID.Add("");
                                }//end of else, if (dtRow["invDescription"].ToString()!="")
                                if (dtRow["invSupDescription"].ToString() != "")
                                {
                                    searchNewID.Add(dtRow["invSupDescription"].ToString());
                                }// end of if (dtRow["invSupDescription"].ToString()!="")
                                else
                                {
                                    searchNewID.Add("");
                                }//end of else, if (dtRow["invSupDescription"].ToString()!="")

                                getInvID = datac.getRecord("Inventory", searchNewInv, searchNewID);//get the record's ID that was just created
                                searchNewID = new ArrayList();//clear arraylist for next item

                                newStockItem.Add(0);
                                newStockItem.Add(getInvID.Rows[0][0].ToString());
                                newStockItem.Add(0);

                                datac.insertCmd("InventoryStock", fieldTotalStock, newStockItem);//insert newly created item in the stock with a 0 stock count
                                newStockItem = new ArrayList();//clear arraylist for next item

                                invOrdered.Add(0);
                                invOrdered.Add(getInvID.Rows[0][0].ToString());
                                if (dtRow["orderQuantity"].ToString() != "")
                                {
                                    invOrdered.Add(dtRow["orderQuantity"].ToString());
                                }// end of if (dtRow["orderQuantity"].ToString()!="")
                                else
                                {
                                    MessageBox.Show("Please enter a quantity to be ordered.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }//end of else, if (dtRow["orderQuantity"].ToString()!="")
                                if (dtRow["orderPrice"].ToString() != "")
                                {
                                    invOrdered.Add(dtRow["orderPrice"].ToString());
                                }// end of if (dtRow["orderPrice"].ToString()!="")
                                else
                                {
                                    invOrdered.Add(0.00);
                                }//end of else, if (dtRow["orderPrice"].ToString()!="")
                                invOrdered.Add(getOrderRef.Rows[0][0].ToString());//get the order reference that was created.
                                if (dtRow["invLength"].ToString() != "")
                                {
                                    invOrdered.Add(dtRow["invLength"].ToString());
                                }// end of if (dtRow["invLength"].ToString()!="")
                                else
                                {
                                    invOrdered.Add(0.00);
                                }//end of else, if (dtRow["invLength"].ToString()!="")

                                datac.insertCmd("SubOrders", fieldSubOrders, invOrdered); //insert this new item in the order created
                                invOrdered = new ArrayList();//clear arraylist for next item
                            }//end of else, if (dtRow["ID"].ToString() !="")
                        }//end of foreach (DataRow dtRow in dtInvValues)
                        string targetpath = savePDFfileDialog(fileName);
                        pdfc.CreateOrderPDF(dtInvValues, dtSupValues, fileName, targetpath);//simply send a datatable for supplier, a datatable for the items, and a string for the filename- it will do the rest
                    }//end of if (dtInvValues!=null)
                    else
                    {
                        MessageBox.Show("No values were entered. Please enter values to insert into the order form.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }//end of else, if (dtInvValues!=null)
                }//end of  if (result == DialogResult.Yes)
                else if (result == DialogResult.No)
                {
                    foreach (DataGridViewColumn header in dtgOrderInvList.Columns)
                    {
                        dtInvValues.Columns.Add(header.Name);
                    }//end of foreach (DataGridViewColumn header in dtgOrderInvList.Columns)
                    foreach (DataGridViewRow row in dtgOrderInvList.Rows)
                    {
                        if (row.IsNewRow)
                            continue;
                        DataRow dtRow = dtInvValues.NewRow();
                        for (int i1 = 0; i1 < dtgOrderInvList.Columns.Count; i1++)
                            dtRow[i1] = (row.Cells[i1].Value == null ? DBNull.Value : row.Cells[i1].Value);
                        dtInvValues.Rows.Add(dtRow);
                    }//end of foreach (DataGridViewRow row in dtgOrderInvList.Rows)

                    //get the applicable supplier details
                    val.Add(getID);
                    dtSupValues = datac.getRecord("Supplier", field, val);

                    if (dtInvValues != null)
                    {
                        string targetpath = savePDFfileDialog(fileName);
                        pdfc.CreateOrderPDF(dtInvValues, dtSupValues, fileName, targetpath);//simply send a datatable for supplier, a datatable for the items, and a string for the filename- it will do the rest

                    }//end of if (dtInvValues!=null)
                    else
                    {
                        MessageBox.Show("No values were entered. Please enter values to insert into the order form.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }//end of else, if (dtInvValues!=null)
                }//end of else else if (result == DialogResult.No)

                txtOrderNumber.Clear();
                dtgSupOrderHistory.DataSource = datac.getAllOrderHistory();//refresh the datagridview
                dtgOrderInvList.Rows.Clear();//clear the ordering List to enable new order to be made.
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch (Exception ex)            
        }//end of private void btnPDForder_Click(object sender, EventArgs e)

        private string savePDFfileDialog(string fileName)
        {
            string filepath = "";
            // try
            // {//this dynamically creates and displays a saveFileDialog and returns the path that the file is saved to for future use if needed
            SaveFileDialog saveFileDialogObj = new SaveFileDialog();

            saveFileDialogObj.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//@"C:\";// 
            saveFileDialogObj.Title = "Save File";              //these are the properties of the saveFileDialog set programmatically on run-time
            //  saveFileDialogObj.CheckFileExists = true;
            //   saveFileDialogObj.CheckPathExists = true;
            saveFileDialogObj.DefaultExt = ".pdf";
            saveFileDialogObj.Filter = "Adobe PDF Files (*.pdf)|*.pdf|All files (*.*)|*.*";//only all files or .pdf is used as filters
            saveFileDialogObj.FilterIndex = 1;
            saveFileDialogObj.RestoreDirectory = true;
            saveFileDialogObj.FileName = fileName;
            //  saveFileDialogObj.ShowDialog();

            if (saveFileDialogObj.ShowDialog() == DialogResult.OK)// only show the dialog after properties are set otherwise default settings are used- this shows the actual dialog
            {
                //    textBox1.Text = saveFileDialogObj.FileName;
                filepath = saveFileDialogObj.FileName;//whatever is selected in the file name box, with its path, will be saved in this string
            }//end of if (saveFileDialogObj.ShowDialog() == DialogResult.OK) 
            else
            {//this happens if anything except OK is clicked
                MessageBox.Show("Please save this file in order to open it successfully.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//end of else,  if (saveFileDialogObj.ShowDialog() == DialogResult.OK)
            //  }//end of try
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}//end of catch (Exception ex)
            return filepath;//returns the path and filename in a string as selected in the saveFileDialog
        }//end of private string savePDFfileDialog(string fileName)
        #endregion

        private void displayInventoryValue()
        {
            double totalStockValue = 0.00;
            for (int i = 0; i < dtgInventoryValue.Rows.Count - 1; i++)
            {
                totalStockValue += (int.Parse(dtgInventoryValue.Rows[i].Cells[4].Value.ToString()) * double.Parse(dtgInventoryValue.Rows[i].Cells[5].Value.ToString()));
            }//end of for (int i = 0; i < dtgInventoryValue.Rows.Count - 1; i++)
            txtStockValue.Text = double.Parse(totalStockValue.ToString()).ToString("C");
        }//end of private void displayInventoryValue()

        private Boolean testDTIfExistAddOne(DataTable testDatatable, int colIndexnumber, string findText)
        { //my dynamic way to see if the record (or the reference thereof) exists within the table within the database
            #region Variables
            Boolean exists = false;
            Boolean flag = false;
            #endregion
                for (int i = 0; i < testDatatable.Rows.Count + 1; i++)
                {
                    if (exists == false)
                    {
                        if (testDatatable.Rows[i][colIndexnumber].ToString() == findText)
                        {
                            exists = true;
                            flag = true;
                        }//end of if (testDatatable.Rows[i][colIndexnumber].ToString() == findText)
                        else
                        {
                            exists = false;
                        }//end of else of if (testDatatable.Rows[i][1].ToString() == findText)
                    }//end of if (exists == false)
                    else
                    {
                        flag = true;
                    }//end of else of if (exists == false)
                }//end of  for (int i = 0; i < testDatatable.Rows.Count + 1; i++)  
            return flag;
        }//end of private Boolean testIfExist(DataTable testDatatable)

        private Boolean testDTIfExist(DataTable testDatatable, int colIndexnumber, string findText)
        { //my dynamic way to see if the record (or the reference thereof) exists within the table within the database
            #region Variables
            Boolean exists = false;
            Boolean flag = false;
            #endregion

            for (int i = 0; i < testDatatable.Rows.Count; i++)
            {
                if (exists == false)
                {
                    if (testDatatable.Rows[i][colIndexnumber].ToString() == findText)
                    {
                        exists = true;
                        flag = true;
                    }//end of if (testDatatable.Rows[i][colIndexnumber].ToString() == findText)
                    else
                    {
                        exists = false;
                    }//end of else of if (testDatatable.Rows[i][1].ToString() == findText)
                }//end of if (exists == false)
                else
                {
                    flag = true;
                }//end of else of if (exists == false)
            }//end of  for (int i = 0; i < testDatatable.Rows.Count + 1; i++)
            return flag;
        }//end of private Boolean testIfExist(DataTable testDatatable)

        private void btnShowAllItems_Click(object sender, EventArgs e)
        {

        }//end of private void btnShowAllItems_Click(object sender, EventArgs e)


    }//end of public partial class frmInventory : Form
}//end of namespace ImagineTrailvan

#region Sample code
/*   DataTable dt = new DataTable();
   dt.Clear();
   dt.Columns.Add("Name");
   dt.Columns.Add("Marks");            example found to create and pass an array of values in a datatable
   object[] o = { "Ravi", 500 };
   dt.Rows.Add(o);
   dt.Rows.Add(new object[] { "Ravi", 500 }); */

//DataTable dtInvValues = new DataTable();
//dtInvValues = (DataTable)dtgSupOrderList.DataSource;
//dtgOrderInvList.DataSource = dtInvValues;
// dtgOrderInvList.DataSource = dtgSupOrderList.DataSource;

// Add TabPage1
//  TabPage tabStock = new TabPage();
//  tabStock.Name = "Stock";
//   tabStock.Text = "Author";
// tabControl1.TabPages.Add(tabStock);
//    tabControl1.Controls.Add(dtgInventory);
//    TabPage selectedTab = tabControl1.SelectedTab;
//    int selectedIndex = tabControl1.SelectedIndex;
//   tabControl1.SelectedTab = selectedTab;

//-----sample for split, to get the numeric order number value-------
//string strDate = "26/07/2011"; //Format – dd/MM/yyyy
////split string date by separator, here I'm using '/'
//string[] arrDate = strDate.Split('/');
////now use array to get specific date object
//string day = arrDate[0].ToString();
//string month = arrDate[1].ToString();
//string year = arrDate[2].ToString();

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
#endregion

#region old stockIn
        //private void btnISIsave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        #region Variables
        //        //datatables
        //        DataTable dtInvoiceIN = new DataTable();
        //        DataTable dtAllInvoices = new DataTable();
        //        //arraylists
        //        ArrayList invoiceValues = new ArrayList();
        //        ArrayList totalStockChanged = new ArrayList();
        //        ArrayList stockINValues = new ArrayList();
        //        ArrayList getIDValue = new ArrayList();
        //        ArrayList invValues = new ArrayList(); //make arrayList to store all values of current record
        //        //arrays
        //        string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"
        //        //variables
        //        Boolean exists = new Boolean();
        //        Boolean flag = new Boolean();
               

        //                        //datatables
        //      //  DataTable dtInvoiceIN = new DataTable();
        //      // DataTable dtAllInvoices = new DataTable();
        //        DataTable dtGetNewItemID = new DataTable();
        //        //arraylists
        //        ArrayList values = new ArrayList(); //make arrayList to store all values of current record
        //        ArrayList createTotalStock = new ArrayList();
        //        ArrayList createNewItemStockIN = new ArrayList();
        //      //  ArrayList invoiceValues = new ArrayList();
        //      //  ArrayList totalStockChanged = new ArrayList();
        //      //  ArrayList stockINValues = new ArrayList();
        //        ArrayList getNewItemDetails = new ArrayList();
        //      //  ArrayList getIDValue = new ArrayList();
        //        //arrays
        //      //  string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"
        //        string[] fieldNewItemID = { "InvItem" };
        //        //variables
        //        Boolean newexists = new Boolean();
        //        Boolean newflag = new Boolean();
        //        //double totalStockValue = 0.00;

        //        #endregion

        //        if (txtISIinvID.Text != "")
        //        {
        //            //update any inventory details
        //            invValues.Add(txtISIinvID.Text);          //store textBox / comboBox value in the ArrayList
        //            invValues.Add(txtISIinvCode.Text);
        //            invValues.Add(txtISIinvItem.Text.ToUpper());
        //            invValues.Add(txtISIinvDescription.Text.ToUpper());
        //            invValues.Add(txtISIinvCategory.Text.ToUpper());
        //            invValues.Add(txtISIinvReLevel.Text);
        //            invValues.Add(cmbISIinvMarkup.Text);
        //            invValues.Add("false");

        //            datac.updateRecCmd("Inventory", fieldInv[0], txtISIinvID.Text, fieldInv, invValues);        //Send values in fieldInv string format from textBox/ comboBox through updateCmd query to database table using InvID as key            
        //            dtgStockIn.DataSource = datac.getTable("Inventory");
        //            flag = false;
        //            if (txtInvoiceNo.Text != "")//this if makes it invoice based, invoice reference made-else is for recirculation
        //            {
        //                if (dtpInvoiceDate.Text != "")
        //                {
        //                    dtAllInvoices = datac.getTable("InvoiceStockIN");

        //                    for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
        //                    {
        //                        if (exists == false)
        //                        {
        //                            if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())//&& dtAllInvoices.Rows[i][4].ToString() == txtISITotal.Text
        //                            {
        //                                exists = true;
        //                                flag = true;
        //                            }//end of if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())
        //                            else
        //                            {
        //                                exists = false;

        //                            }//end of else, if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString()) 
        //                        }//end of if (exists == false)
        //                        else
        //                        {
        //                            //this will be my flag to signal if it exists, since the first part of exist will only test each row and then move on, thus not stopping if it exists
        //                            flag = true;
        //                        }//end of else of if (exists == false)
        //                    }//end of for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
        //                    //   MessageBox.Show("Exists value: " + flag, "Testing complete");
        //                    if (flag == true)
        //                    {
        //                        // MessageBox.Show("This invoice already exists. ", "Testing complete");
        //                        DialogResult result = MessageBox.Show("This invoice already exists: Do you wish to add items to it?", "Invoice Exists", MessageBoxButtons.YesNo,
        //            MessageBoxIcon.Question);

        //                        //make fancy pop-up to state that invoice exists, want to add to invoice
        //                        if (result == DialogResult.Yes)
        //                        {
        //                            getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
        //                            // getIDValue.Add("='" + dtpInvoiceDate.Text+"00:00:00.000'");
        //                            getIDValue.Add("=" + cmbISISupplier.SelectedValue);
        //                            // getIDValue.Add("=" + txtISITotal.Text + "00");

        //                            dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

        //                            stockINValues.Add(0);
        //                            stockINValues.Add(txtISIinvID.Text);
        //                            stockINValues.Add(txtISIstockReceived.Text);
        //                            stockINValues.Add(txtISIstockPrice.Text);
        //                            stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
        //                            stockINValues.Add(txtISIstockReceived.Text);
        //                            datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted

        //                            totalStockChanged.Add(0);
        //                            totalStockChanged.Add(txtISIinvID.Text);
        //                            totalStockChanged.Add((int.Parse(txtISIstockReceived.Text) + int.Parse(txtISIstockTotal.Text)));
        //                            datac.updateRecCmd("InventoryStock", fieldTotalStock[0].ToString(), txtISIinvID.Text, fieldTotalStock, totalStockChanged);//string tblName, string idField, string ID, string[] fields, ArrayList values

        //                            dtgStockIn.DataSource = datac.getTable("Inventory");
        //                        }//end of if (result == DialogResult.Yes)
        //                        else if ((result == DialogResult.No))
        //                        {
        //                            MessageBox.Show("Please enter a new invoice number value in the Invoice Number field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            txtInvoiceNo.Clear();
        //                            txtISITotal.Clear();
        //                            btnISIpreviewCurrent.Enabled = false;

        //                            dtgStockIn.DataSource = datac.getTable("Inventory");
        //                        }//end of else if (result == DialogResult.No)
        //                    }//end of if (flag == true)
        //                    else
        //                    {//make new invoice thingy
        //                        invoiceValues.Add(0);
        //                        invoiceValues.Add(txtInvoiceNo.Text);
        //                        invoiceValues.Add(dtpInvoiceDate.Text);
        //                        invoiceValues.Add(cmbISISupplier.SelectedValue);
        //                        invoiceValues.Add(txtISITotal.Text);
        //                        datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, invoiceValues);// do an insert to record the invoice received

        //                        getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
        //                        getIDValue.Add("=" + cmbISISupplier.SelectedValue);
        //                        dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

        //                        stockINValues.Add(0);
        //                        stockINValues.Add(txtISIinvID.Text);
        //                        stockINValues.Add(txtISIstockReceived.Text);
        //                        stockINValues.Add(txtISIstockPrice.Text);
        //                        stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
        //                        stockINValues.Add(txtISIstockReceived.Text);
        //                        datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted
        //                        dtgStockIn.DataSource = datac.getStockInInvoice(dtInvoiceIN.Rows[0][0].ToString());//declare new datasource with updated data

        //                        totalStockChanged.Add(0);
        //                        totalStockChanged.Add(txtISIinvID.Text);
        //                        totalStockChanged.Add((int.Parse(txtISIstockReceived.Text) + int.Parse(txtISIstockTotal.Text)));
        //                        datac.updateRecCmd("InventoryStock", fieldTotalStock[0].ToString(), txtISIinvID.Text, fieldTotalStock, totalStockChanged);//string tblName, string idField, string ID, string[] fields, ArrayList values
        //                    }//end of else of if (newflag == true)
        //                }//end of if (dtpInvoiceDate.Text!="")
        //                else
        //                {
        //                    MessageBox.Show("Please enter a date value in the Invoice Date field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }//end of else if (dtpInvoiceDate.Text!="")
        //            }//end of if (txtISIinvoiceNr.Text != "")
        //            else
        //            {
        //                //no reference to invoice is made - stock can go back into circulation here
        //                //therefore, it runs an update, NOT an insert
        //                dtInvoiceIN = datac.getFIFODatedPrice(txtISIinvID.Text);//get the first dated price of this item

        //                totalStockChanged.Add(0);
        //                totalStockChanged.Add(txtISIinvID.Text);
        //                totalStockChanged.Add((int.Parse(txtISIstockReceived.Text) + int.Parse(txtISIstockTotal.Text)));
        //                datac.updateRecCmd("InventoryStock", fieldTotalStock[1].ToString(), txtISIinvID.Text, fieldTotalStock, totalStockChanged);//changing totalStock
        //                for (int i = 0; i < int.Parse(txtISIstockReceived.Text) - 1; i++)
        //                {
        //                    stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());
        //                    stockINValues.Add(dtInvoiceIN.Rows[0][1].ToString());
        //                    stockINValues.Add(dtInvoiceIN.Rows[0][3].ToString());
        //                    stockINValues.Add(dtInvoiceIN.Rows[0][4].ToString());
        //                    stockINValues.Add(dtInvoiceIN.Rows[0][5].ToString());
        //                    stockINValues.Add(int.Parse(dtInvoiceIN.Rows[0][6].ToString()) + 1);
        //                    datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtInvoiceIN.Rows[0][0].ToString(), fieldStockIN, stockINValues);//update existing record

        //                    dtgStockIn.DataSource = datac.getTable("Inventory");
        //                    dtInvoiceIN = datac.getFIFODatedPrice(txtISIinvID.Text);
        //                    stockINValues = new ArrayList();//clear the arraylist to be able to record next item
        //                }//end of for (int i = 0; i < int.Parse(txtISIquantityIn.Text-1); i++)    
        //                string getID = dtInvoiceIN.Rows[0][5].ToString();               //what is the idea of this variable? Do i use it?
        //                //dtgStockIn.DataSource = datac.getCurrentInvoice(getID);
        //            }//end of else if (txtInvoiceNo.Text != "")
        //        }//end of if (txtISIinvID.Text!="")
        //        else
        //        {
        //            if (txtISIinvItem.Text != "")
        //            {
        //                values.Add("0");          //store textBox / comboBox value in the ArrayList
        //                values.Add(txtISIinvCode.Text);
        //                values.Add(txtISIinvItem.Text.ToUpper());
        //                values.Add(txtISIinvDescription.Text.ToUpper());
        //                values.Add("");
        //                values.Add(txtISIinvCategory.Text.ToUpper());

        //                if (txtISIinvReLevel.Text != "")     //See if user gave value for reorderLevel, and use value
        //                {
        //                    values.Add(txtISIinvReLevel.Text);
        //                }//end of if (txtInvReLeveli.Text!="")
        //                else
        //                {
        //                    values.Add("0");            //if user gave no value, make default 0: for prototyping purposes
        //                }//end of else, if (txtInvReLeveli.Text!="")
        //                if (cmbISIinvMarkup.Text != "")
        //                {
        //                    values.Add(cmbISIinvMarkup.Text);       //if user gave value, use value
        //                }//end of if (cmbInvMarkupi.Text!="")
        //                else
        //                {
        //                    values.Add("25");                   //if user gave no value for markup%, then make 25% default
        //                }//end of else, if (cmbInvMarkupi.Text!="")
        //                values.Add("false");

        //                newflag = false;

        //                datac.insertCmd("Inventory", fieldInvAll, values);       ////Send values in fieldInv string format through insertCmd query to database table
        //                dtgStockIn.DataSource = datac.getTable("Inventory");

        //                getNewItemDetails.Add(txtISIinvItem.Text.ToUpper());//consider more than 1 search field to ensure there is NO other record close to it ( ref male/ female door retainer rubber)
        //                dtGetNewItemID = datac.getRecord("Inventory", fieldNewItemID, getNewItemDetails);
        //                //make reference for a new item to call other tables from
        //                if (txtISIstockReceived.Text != "")
        //                {//create instance for this new item to have a reference to StockIN and a StockTotal
        //                    if (txtInvoiceNo.Text != "")//this if makes it invoice based, invoice reference made-else is for recirculation
        //                    {
        //                        if (dtpInvoiceDate.Text != "")
        //                        {
        //                            dtAllInvoices = datac.getTable("InvoiceStockIN");
        //                            if (txtISIstockPrice.Text != "")
        //                            {
        //                                for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
        //                                {
        //                                    if (newexists == false)
        //                                    {
        //                                        if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())//&& dtAllInvoices.Rows[i][4].ToString() == txtISITotal.Text
        //                                        {
        //                                            newexists = true;
        //                                            newflag = true;
        //                                        }//end of  if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())//&& dtAllInvoices.Rows[i][4].ToString() == txtISITotal.Text
        //                                        else
        //                                        {
        //                                            newexists = false;
        //                                        }//end of else
        //                                    }//end of if (newexists == false)
        //                                    else
        //                                    {
        //                                        //this will be my flag to signal if it exists, since the first part of exist will only test each row and then move on, thus not stopping if it exists
        //                                        newflag = true;
        //                                    }//end of else of if (exists == false)
        //                                }//end of for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
        //                                //   MessageBox.Show("Exists value: " + exists, "Testing complete");
        //                                if (newflag == true)
        //                                {
        //                                    getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
        //                                    // getIDValue.Add("='" + dtpInvoiceDate.Text+"00:00:00.000'");
        //                                    getIDValue.Add("=" + cmbISISupplier.SelectedValue);
        //                                    // getIDValue.Add("=" + txtISITotal.Text + "00");
        //                                    dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

        //                                    // createNewItemStockIN.Add();
        //                                    stockINValues.Add(0);
        //                                    stockINValues.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                                    stockINValues.Add(txtISIstockReceived.Text);
        //                                    stockINValues.Add(txtISIstockPrice.Text);
        //                                    stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
        //                                    stockINValues.Add(txtISIstockReceived.Text);
        //                                    datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted

        //                                    totalStockChanged.Add(0);
        //                                    totalStockChanged.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                                    totalStockChanged.Add(txtISIstockReceived.Text);
        //                                    datac.insertCmd("InventoryStock", fieldTotalStock, totalStockChanged);//create a reference to totalStock with first stock

        //                                    // dtgStockIn.DataSource = datac.getAllInvoice();
        //                                    dtgStockIn.DataSource = datac.getTable("Inventory");
        //                                }//end of if (newflag = true)

        //                                else
        //                                {//make new invoice thingy
        //                                    invoiceValues.Add(0);
        //                                    invoiceValues.Add(txtInvoiceNo.Text);
        //                                    invoiceValues.Add(dtpInvoiceDate.Text);
        //                                    invoiceValues.Add(cmbISISupplier.SelectedValue);
        //                                    invoiceValues.Add(txtISITotal.Text);
        //                                    datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, invoiceValues);

        //                                    getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
        //                                    getIDValue.Add("=" + cmbISISupplier.SelectedValue);
        //                                    dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID


        //                                    stockINValues.Add(0);
        //                                    stockINValues.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                                    stockINValues.Add(txtISIstockReceived.Text);
        //                                    stockINValues.Add(txtISIstockPrice.Text);
        //                                    stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
        //                                    stockINValues.Add(txtISIstockReceived.Text);
        //                                    datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted
        //                                    dtgStockIn.DataSource = datac.getTable("Inventory");

        //                                    totalStockChanged.Add(0);
        //                                    totalStockChanged.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                                    totalStockChanged.Add(txtISIstockReceived.Text);
        //                                    datac.insertCmd("InventoryStock", fieldTotalStock, totalStockChanged);//create a reference to totalStock with first stock
        //                                }//end of else, if (flag == true)
        //                            }//end of if (txtISIstockPrice.Text!="")
        //                            else
        //                            {
        //                                MessageBox.Show("Please enter a Price/Unit value in the <Price/ Unit excl VAT> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            }//end of else, if (txtISIstockPrice.Text!="")
        //                        }//end of if (dtpInvoiceDate.Text!="")
        //                        else
        //                        {
        //                            MessageBox.Show("Please enter a date value in the Invoice Date field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        }//end of else if (dtpInvoiceDate.Text!="")
        //                    }//end of if (txtISIinvoiceNr.Text != "")
        //                    else
        //                    {//since no invoiceNo is given, make it a STOCK invoice             //cant make a stock item if item is entered for the very first time!!!!!
        //                        invoiceValues.Add(0);
        //                        invoiceValues.Add("STOCK " + (emptySupplier += 1));
        //                        invoiceValues.Add(dtpInvoiceDate.Text);
        //                        invoiceValues.Add("1");
        //                        invoiceValues.Add("0.00");
        //                        datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, invoiceValues);

        //                        getIDValue.Add(" LIKE 'STOCK " + (emptySupplier += 1) + "'");
        //                        getIDValue.Add("= 1");
        //                        dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

        //                        stockINValues.Add(0);
        //                        stockINValues.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                        if (txtISIstockReceived.Text != "")
        //                        {
        //                            stockINValues.Add(txtISIstockReceived.Text);
        //                        }//end of if (txtISIstockReceived.Text!="")
        //                        else
        //                        {
        //                            MessageBox.Show("Please enter a Quantity Received value in the <Quantity Received> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            stockINValues.Add(txtISIstockReceived.Text);
        //                        }//end of else, if (txtISIstockReceived.Text!="")
        //                        if (txtISIstockPrice.Text != "")
        //                        {
        //                            stockINValues.Add(txtISIstockPrice.Text);
        //                        }//end of if (xtISIstockPrice.Text!="")
        //                        else
        //                        {
        //                            MessageBox.Show("Please enter a Price/Unit value in the <Price/ Unit excl VAT> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            stockINValues.Add(txtISIstockPrice.Text);
        //                        }//end of else, if (xtISIstockPrice.Text!="")

        //                        stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
        //                        stockINValues.Add(txtISIstockReceived.Text);
        //                        datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted
        //                        dtgStockIn.DataSource = datac.getTable("Inventory");

        //                        totalStockChanged.Add(0);
        //                        totalStockChanged.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                        totalStockChanged.Add(txtISIstockReceived.Text);
        //                        datac.insertCmd("InventoryStock", fieldTotalStock, totalStockChanged);//create a reference to totalStock with first stock
        //                    }//end of if (txtISIinvoiceNr.Text != "")
        //                }//end of if (txtISIstockReceived.Text!="")
        //            }//end of  if (txtISIinvItem.Text != "")
        //            else
        //            {
        //                MessageBox.Show("No record was added: Item Field is empty. Please provide an item's name to add.", "New record unsuccessful!");
        //            }//end of else, if (txtISIinvItem.Text!="")
        //        }//end of else  if (txtISIinvID.Text != "")


        //        dtgStockIn.DataSource = datac.getTable("Inventory");
        //        dtgCheckStock.DataSource = datac.getTable("Inventory");
        //        dtgSupOrderList.DataSource = datac.getLowStock();
        //        dtgInventoryValue.DataSource = datac.getInventoryValue();
        //       //btnISIpreviewCurrent.Enabled = false;





        //       // dtgInvoiceHistory.DataSource = datac.getAllInvoice();
        //   //     dtgInventoryValue.DataSource = datac.getInventoryValue();

        //        //to calculate total value of stock based on quantity and price on inventoryValue tab****
        //        displayInventoryValue();

        //        txtISIinvID.Clear();        //clear txtboxes for next search/ item or whichever
        //        txtISIinvCode.Clear();
        //        txtISIinvItem.Clear();
        //        txtISIinvDescription.Clear();
        //        txtISIinvCategory.Clear();
        //        txtISIstockReceived.Clear();
        //        txtISIstockPrice.Clear();
        //        txtISIstockTotal.Clear();

        //        btnISIclear.Enabled = true;
        //        btnISIinvDeleteItem.Enabled = false;
        //        btnISIrecordNewInvoice.Enabled = true;
        //        btnISIsave.Enabled = false;
        //        btnISIpreviewCurrent.Enabled = true;
        //        btnISIdelItemInvoice.Enabled = true;
        //    }//end of try
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error saving invoice details : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }//end of catch(Exception ex)
        //}//end of private void btnISIsave_Click(object sender, EventArgs e) 
#endregion
