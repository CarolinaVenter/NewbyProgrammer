//GOD SHALL BLESS THIS APPLICATION AND ALL WHO USES IT, IMAGINE SHALL PROSPER BY THE GRACE OF GOD AND THE BLESSINGS GOD POURS IN, SO THAT WE CAN BLESS OUR CLIENTS. AMEN
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
        PdfCreator pdfc = new PdfCreator();
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
            // TODO: This line of code loads data into the 'supplierDataSet.Supplier' table. You can move, or remove it, as needed.
            this.supplierTableAdapter.Fill(this.supplierDataSet.Supplier);
            try
            {
                #region StockOut Controls on Load
                stockOutLoadControls();
                #endregion

                #region StockIn Controls on Load
                stockInLoadControls();
                #endregion

                #region StockCheck Controls on Load
                stockCheckLoadControls();
                #endregion

                #region Suppliers Controls on Load
                supplierLoadControls();
                #endregion

                #region InventoryValue Controls on Load
                inventoryValueLoadControls();
                #endregion

                #region OrderStock Controls on Load
                orderStockLoadControls();
                #endregion
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Inventory Form from frmInventory_Load method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void frmInventory_Load(object sender, EventArgs e)

        #region InventoryFormLoadControls
        private void stockOutLoadControls()
        {
            try
            {
                tabStockOUT.Controls.Add(dtgInventory);
                dtgInventory.DataSource = datac.getTable("Inventory");

                btnClear.Enabled = true;
                btnSearch.Enabled = true;
                btnStockUpdate.Enabled = false;

                dtpDateStockOUT.Value = DateTime.Now;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Controls on StockOut tab from stockOutLoadControls() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void stockOutLoadControls()
        private void stockInLoadControls()
        {
            try
            {
                tabStockIN.Controls.Add(dtgStockIn);
                tabStockIN.Controls.Add(dtgInvoiceHistory);
                dtgStockIn.DataSource = datac.getTable("Inventory");
                dtgInvoiceHistory.DataSource = datac.getAllInvoice();

                btnISIdelItemInvoice.Enabled = false;
                btnISIinvDeleteItem.Enabled = false;
                btnISIpreviewCurrent.Enabled = false;
                btnISIrecordNewInvoice.Enabled = false;
                btnISIclear.Enabled = true;

                dtpInvoiceDate.Value = DateTime.Now;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Controls on StockIn tab from stockInLoadControls() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void stockInLoadControls()
        private void stockCheckLoadControls()
        {
            try
            {
                tabStockCheck.Controls.Add(dtgCheckStock);
                dtgCheckStock.DataSource = datac.getTable("Inventory");
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Controls on StockCheck tab from stockCheckLoadControls() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void stockCheckLoadControls()
        private void supplierLoadControls()
        {
            try
            {
                tabSuppliers.Controls.Add(dtgSuppliers);
                dtgSuppliers.DataSource = datac.getTable("Supplier");

                buttonsSupInitialSet();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Controls on Supplier tab from supplierLoadControls() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void supplierLoadControls()
        private void inventoryValueLoadControls()
        {
            try
            {
                tabInventoryValue.Controls.Add(dtgInventoryValue);
                dtgInventoryValue.DataSource = datac.getInventoryValue();
                //to calculate total value of stock based on quantity and price on inventoryValue tab****
                displayInventoryValue();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Controls on InventoryValue tab from inventoryValueLoadControls() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void inventoryValueLoadControls()
        private void orderStockLoadControls()
        {
            try
            {
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Controls on OrderStock tab from orderStockLoadControls() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void orderStockLoadControls()
        #endregion

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
                    ClearStockOutTextboxes();
                }//end of else
                btnClear.Enabled = true;
                btnStockUpdate.Enabled = false;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Search Error occurred in btnSearch_Click method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSearch_Click(object sender, EventArgs e)

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                dtgInventory.DataSource = datac.getTable("Inventory");        //change dtgInventory back to original data in table
                btnStockUpdate.Enabled = false;
                btnSearch.Enabled = true;
                ClearStockOutTextboxes();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Search Clear Error occurred in btnClear_Click method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)            
        }//end of private void btnClear_Click(object sender, EventArgs e)
        private void ClearStockOutTextboxes()
        {
            try
            {
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
                MessageBox.Show("Error occurred while clearing the textboxes on stockOut tab in ClearStockOutTextboxes() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of  private void ClearStockOutTextboxes()

        #region dtgInventory_Click_Load
        private void dtgInventory_Click(object sender, EventArgs e)
        {
            try
            {
                txtInvID.Text = dtgInventory.SelectedRows[0].Cells[0].Value.ToString(); //populte the textboxes from the dtg
                txtInvCode.Text = dtgInventory.SelectedRows[0].Cells[1].Value.ToString();
                txtInvItem.Text = dtgInventory.SelectedRows[0].Cells[2].Value.ToString().ToUpper();
                txtInvDesc.Text = dtgInventory.SelectedRows[0].Cells[3].Value.ToString().ToUpper();
                txtInvCat.Text = dtgInventory.SelectedRows[0].Cells[5].Value.ToString().ToUpper();
                txtInvReLevel.Text = dtgInventory.SelectedRows[0].Cells[6].Value.ToString();
                cmbInvMarkup.Text = dtgInventory.SelectedRows[0].Cells[7].Value.ToString(); //add a string Collection
                stockOutGetTotalStock();
                dtgInventoryClickButtonSet();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes in dtgInventory_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void dtgInventory_Click(object sender, EventArgs e)
        private void stockOutGetTotalStock()
        {
            try
            {
                #region Variables
                DataTable testTotalStock = new DataTable();
                DataTable dtTotalStock = new DataTable();
                ArrayList getIDValue = new ArrayList();
                string[] fieldTotal = { "InventoryID" };
                Boolean exists = new Boolean();
                #endregion
                
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
                    }//end of if (dtTotalStock.Rows[0] != null)
                    else
                    {
                        MessageBox.Show("No records found in datatable. Please revise. ");
                    } //end of else, if (dtTotalStock.Rows[0] != null)

                    //**************Get the oldest date's stock price via the InvoiceStockIN table's date and the InventoryID found in the SubStockIN table*********
                    fifoDatedPrice();             
                    //to calculate the selling price (price + markup excl VAT)***
                    stockOutSellPriceCalc();//   txtSellPrice.Text = (double.Parse(txtInvPrice.Text) * ((100 + (int.Parse(cmbInvMarkup.ValueMember))) / 100)).ToString(); //tried but failed
                }//end of if (flag == true)
                else
                {
                    MessageBox.Show(@"Please log the stock in the STOCK IN tab FIRST to ensure all the correct references are established.", "Reference reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnStockUpdate.Enabled = false; //disable button so that nothing can happen- force to either choose another item or go to StockIN
                }//end of else of if (flag == true)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data to textBoxes in stockOutGetTotalStock() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void stockOutGetTotalStock()
        private void dtgInventoryClickButtonSet()
        {
            try
            {
                txtInvStockOut.Clear();
                btnClear.Enabled = true;
                btnStockUpdate.Enabled = true;
                btnSearch.Enabled = false;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading button settings in dtgInventoryClickButtonSet() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of dtgInventoryClickButtonSet()
        private void stockOutSellPriceCalc()
        {
            try
            {
                #region Variables
                double price = 0.00;
                double markup = 0.00;
                double sellPrice = 0.00;
                #endregion
                price = double.Parse(txtInvPrice.Text); 
                markup = double.Parse(cmbInvMarkup.Text);
                sellPrice = price * ((100 + markup) / 100);
                txtInvPrice.Text=price.ToString("C");
                txtSellPrice.Text = sellPrice.ToString("C");
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from textboxes to calculate selling price in stockOutSellPriceCalc() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void stockOutSellPriceCalc()
        private void fifoDatedPrice()
        {
            try
            {
                #region Variables
                DataTable dtStockIn = new DataTable();
                Boolean exists = new Boolean();
                #endregion

                dtStockIn = datac.getTable("SubStockIN");
                exists = testDTIfExistAddOne(dtStockIn, 1, txtInvID.Text);
                //  dtStockIn = datac.getFIFODatedPrice(txtInvID.Text);//before i do this, i'll have to run a test exist first
                if (exists == true)
                {
                    dtStockIn = datac.getFIFODatedPrice(txtInvID.Text);
                    txtInvPrice.Text = dtStockIn.Rows[0][4].ToString();
                }//end of if (exists==true)
                else
                {
                    MessageBox.Show("No record found for the item " + txtInvItem.Text);
                } //end of else, if (exists==true)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving FIFO dated price from fifoDatedPrice() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex) 
        }//end of  private void fifoDatedPrice()
        #endregion

        #region btnInvUpdate_Click_Load
                private void btnInvUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                btnClear.Enabled = false;
                btnStockUpdate.Enabled = false;
                //**********SEND INVENTORY DETAILS TO BE UPDATED**********
                //this is done even if no stock is taken out......
                invOutUpdateDetails();
                //*********SEND INSERT TO STOCK OUT TABLE**************
                if (txtInvStockOut.Text!="")
                {
                    if (dtpDateStockOUT.Text!="")
                    {
                        stockOutInsert();
                        //*********SEND update TO TOTALSTOCK TABLE**************
                        totalStockOutUpdate();
                        //*******Minus quantity-out from First stock in LEFT from table SubStockIN, while using oldest date in InvoiceStockIN********    
                        // #1) lookup oldest date in the InvoiceStockIN table, with get query.
                        //#2) minus left until counter (stockOUT quantity is used for counter) is 0
                        //#3) if left is smaller than counter (thus make if statement), jump to next date and left
                        //#4) keep minussing until counter is 0 and then update to SubStockIN
                        stockLeftUpdate();
                        getnewstockoutFIFOprice();
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
                reloadInventoryGrids();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating StockOut in btnInvUpdate_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnInvUpdate_Click(object sender, EventArgs e)
        private void invOutUpdateDetails()
        {
            try
            {
                #region Variables
                ArrayList invValues = new ArrayList(); //make arrayList to store all values of current record
                #endregion
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating StockOut in invOutUpdateDetails() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void invOutUpdateDetails()
        private void stockOutInsert()
        {
            try
            {
                #region Variables
                 ArrayList stockOutValues = new ArrayList();
                #endregion
                stockOutValues.Add(0);
                stockOutValues.Add(txtInvID.Text);
                stockOutValues.Add(txtInvStockOut.Text);
                stockOutValues.Add(dtpDateStockOUT.Value);
                stockOutValues.Add("true");
                datac.insertCmd("SubStockOUT", fieldStockOut, stockOutValues);
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while inserting record in StockOut in stockOutInsert() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void stockOutInsert()
        private void totalStockOutUpdate()
        {
            try
            {
                #region Variables
                ArrayList totalValues = new ArrayList();
                #endregion
                txtInvTotalStock.Text = (int.Parse(txtInvTotalStock.Text) - int.Parse(txtInvStockOut.Text)).ToString();
                totalValues.Add(0);
                totalValues.Add(txtInvID.Text);
                totalValues.Add(txtInvTotalStock.Text);
                datac.updateRecCmd("InventoryStock", fieldTotalStock[1], txtInvID.Text, fieldTotalStock, totalValues);
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating record in TotalStock in totalStockOutUpdate() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void totalStockOutUpdate()
        private void stockLeftUpdate()
        {
            try
            {
                #region Variables
                 DataTable dtSubStockIN = new DataTable();
                ArrayList subStockIN = new ArrayList();
                #endregion
                dtSubStockIN = datac.getFIFODatedPrice(txtInvID.Text);

                for (int i = 0; i < int.Parse(txtInvStockOut.Text); i++)
                {
                    if (int.Parse(dtSubStockIN.Rows[0][5].ToString()) > 0)
                    {
                        stockLeftUpdateBuilder(dtSubStockIN);
                    }//end of if (int.Parse(dtSubStockIN.Rows[0][5].ToString())>0)
                    else
                    {//assuming when it jumps to the else, it should probably minus again before it exits the else and runs the next counter which then continues to the next if
                        dtSubStockIN = datac.getFIFODatedPrice(txtInvID.Text);
                        stockLeftUpdateBuilder(dtSubStockIN);
                    }//end of else
                }//end of for (int i = 0; i < int.Parse(txtInvStockOut.Text); i++)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating record in StockIn in stockLeftUpdate() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void stockLeftUpdate()
        private void stockLeftUpdateBuilder(DataTable dtSubStockIN)
        {
            try
            {
                #region Variables
                ArrayList subStockIN = new ArrayList();
                #endregion
                subStockIN.Add(dtSubStockIN.Rows[0][0].ToString());
                subStockIN.Add(dtSubStockIN.Rows[0][1].ToString());
                subStockIN.Add(dtSubStockIN.Rows[0][3].ToString());
                subStockIN.Add(dtSubStockIN.Rows[0][4].ToString());
                subStockIN.Add(dtSubStockIN.Rows[0][5].ToString());//invoice number
                subStockIN.Add((int.Parse(dtSubStockIN.Rows[0][6].ToString()) - 1));
                datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtSubStockIN.Rows[0][0].ToString(), fieldStockIN, subStockIN);
                subStockIN = new ArrayList();//basically clearing this arrayList
                dtSubStockIN = datac.getFIFODatedPrice(txtInvID.Text);//get the record again after the update
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating record in StockIn in stockLeftUpdateBuilder(DataTable dtSubStockIN) method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void stockLeftUpdateBuilder()
        private void getnewstockoutFIFOprice()
        {
            try
            {
                DataTable dtStockIn = new DataTable();

                dtStockIn = datac.getFIFODatedPrice(txtInvID.Text);
                if (dtStockIn.Rows[0] != null)
                {
                    txtInvPrice.Text = dtStockIn.Rows[0][4].ToString();
                }//end of if (dtStockIn.Rows[0] != null)
                else
                {
                    MessageBox.Show("No records found in datatable for the price. Please revise. ");
                } //end of else
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while retreiving record in StockIn in getnewstockoutFIFOprice() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void getnewstockoutFIFOprice()
        private void reloadInventoryGrids()
        {
            try
            {
                dtgInventory.DataSource = datac.getTable("Inventory");
                dtgInventoryValue.DataSource = datac.getInventoryValue();
                dtgSupOrderList.DataSource = datac.getLowStock();
                //to calculate total value of stock based on quantity and price on inventoryValue tab****
                displayInventoryValue();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while reloading data in reloadInventoryGrids() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void reloadInventoryGrids()
        #endregion

        #endregion

        #region tabStockIN
        #region isiDTG Controls
        private void dtgStockIn_Click(object sender, EventArgs e)
        {
            try
            {
                isiDTGclickLoad();
                isiDTGloadSet();
                isigetInventoryStock();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data and contents from dtgStockIn_Click method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch
        }//end of private void dtgStockIn_Click(object sender, EventArgs e)
        private void isiDTGclickLoad()
		{
    		try
    		{
    			txtISIinvID.Text = dtgStockIn.SelectedRows[0].Cells[0].Value.ToString();
        		txtISIinvCode.Text = dtgStockIn.SelectedRows[0].Cells[1].Value.ToString();
        		txtISIinvItem.Text = dtgStockIn.SelectedRows[0].Cells[2].Value.ToString().ToUpper();
        		txtISIinvDescription.Text = dtgStockIn.SelectedRows[0].Cells[3].Value.ToString().ToUpper();
        		txtISISupDescription.Text = dtgStockIn.SelectedRows[0].Cells[4].Value.ToString().ToUpper();
        		txtISIinvCategory.Text = dtgStockIn.SelectedRows[0].Cells[5].Value.ToString().ToUpper();
        		txtISIinvReLevel.Text = dtgStockIn.SelectedRows[0].Cells[6].Value.ToString();
        		cmbISIinvMarkup.Text = dtgStockIn.SelectedRows[0].Cells[7].Value.ToString(); 
    		}//end of try
    		catch (Exception ex)
    		{
        		MessageBox.Show("Error loading data from datagrid to textBoxes in isiDTGclickLoad() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    		}//end of catch
		}//end of public void isiDTGclickLoad()
        private void isiDTGloadSet()
        {
            try
            {
                txtISIstockReceived.Clear();
                txtISIstockPrice.Clear();
                btnISIsearchItem.Enabled = false;
                btnISIinvDeleteItem.Enabled = true;
                btnISIdelItemInvoice.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading form settings for StockIn tab using isiDTGloadSet() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch
        }//end of public void isiDTGloadSet()
        private void isigetInventoryStock()
        {
            try
            {
                #region variables
                DataTable dtStockIN = new DataTable();
                ArrayList getIDValue = new ArrayList();
                Boolean exists = new Boolean();
                string[] fieldTotal = { "InventoryID" };
                #endregion
                dtStockIN = datac.getTable("InventoryStock");
                exists = testDTIfExistAddOne(dtStockIN, 1, txtISIinvID.Text);
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading InventoryStock data to textBoxes using isigetInventoryStock() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch
        }//end of public void isigetInventoryStock()
        #endregion

        #region isiSearch Controls
        private void btnISIsearchItem_Click(object sender, EventArgs e)
        {
            try
            {
				btnISIsearchItem_clickSet();
                inventoryIDSearchLookup();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Search Error occurred in btnISIsearchItem_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnISIsearchItem_Click(object sender, EventArgs e)
		private void btnISIsearchItem_clickSet()
		{
			try
			{
				btnISIclear.Enabled = true;
                btnISIinvDeleteItem.Enabled = true;
                btnISIsave.Enabled = true;
				btnISIdelItemInvoice.Enabled = false;
			}//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in setting content on StockIn tab using btnISIsearchItem_clickSet() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
		}//end of private void btnISIsearchItem_clickSet()
		private void inventoryIDSearchLookup()
		{
            try
            {
                #region Variables
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                #endregion
				if (txtISIinvCode.Text != "")
                {
                    string[] searchField = {"InvCode"};
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
				    isiClearTextboxes();                    
                }//end of else
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while searching textboxes in StockIn tab using inventoryIDSearchLookup() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
		}//end of private void inventorySearchLookup()
        #endregion

        #region isiDelItemInvoice Controls
        private void btnISIdelItemInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                DataTable dtInvoiceIN = new DataTable();
                DataTable dtStockIN = new DataTable();
                DataTable dtTotalStock = new DataTable();
                #endregion
                
                dtInvoiceIN = ISIdelItemInvoice_getInvoice();
                dtStockIN = ISIdelItemInvoice_RemoveStockIn(dtInvoiceIN);
                dtTotalStock = ISIdelItemInvoice_getTotalStock();
                ISIdelItemInvoice_updateInventoryStock(dtStockIN, dtTotalStock);
                ISIdelItemInvoiceSet();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting current item from invoice using btnISIdelItemInvoice_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch(Exception ex)
        }//end of private void btnISIdelItemInvoice_Click(object sender, EventArgs e)
        private DataTable ISIdelItemInvoice_getInvoice()
        {
            #region Variables
            ArrayList getIDValue = new ArrayList();
            string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };
            DataTable dtInvoiceIN = new DataTable();
            #endregion
            
            getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
            getIDValue.Add("=" + cmbISISupplier.SelectedValue);
            dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID
            return dtInvoiceIN;
        }//end of private DataTable ISIdelItemInvoice_getInvoice()
        private DataTable ISIdelItemInvoice_RemoveStockIn(DataTable dtInvoiceIN)
        {
            #region Variables
            ArrayList getStockInID = new ArrayList();
            DataTable dtStockIN = new DataTable();
            string[] fieldsFilter = { "InventoryID", "ISIID" }; 
            #endregion
            
            getStockInID.Add("=" + txtISIinvID.Text);
            getStockInID.Add("=" + dtInvoiceIN.Rows[0][0].ToString());
            dtStockIN = datac.getMathRecord("SubStockIN", fieldsFilter, getStockInID);  //save stock in details in this dataTable, to get the precise SubStockINID
            datac.removeCmd("SubStockIN", dtStockIN.Rows[0][0].ToString());      //delete the record in the table using the key from the dataTable from getMathRecord
            return dtStockIN;
        }//end of private DataTable ISIdelItemInvoice_RemoveStockIn()
        private DataTable ISIdelItemInvoice_getTotalStock()
        {
            #region Variables
            ArrayList getTotalRecord = new ArrayList();
            string[] getTotalStock = { "InventoryID" };
            DataTable dtTotalStock = new DataTable();
            #endregion
            
            getTotalRecord.Add("=" + txtISIinvID.Text); //should remember to remove that same item's quantity from the totalStock, Since it wont be added anymore
            dtTotalStock = datac.getMathRecord("InventoryStock", getTotalStock, getTotalRecord);
            return dtTotalStock;
        }//end of ISIdelItemInvoice_getTotalStock()
        private void ISIdelItemInvoice_updateInventoryStock(DataTable dtStockIN, DataTable dtTotalStock)
        {
            try
            {
                #region MyRegion
                ArrayList removeTotalStockvalues = new ArrayList();
                #endregion

                removeTotalStockvalues.Add(dtTotalStock.Rows[0][0].ToString());
                removeTotalStockvalues.Add(txtISIinvID.Text);//dtTotalStock.Rows[0][1].ToString());
                removeTotalStockvalues.Add((int.Parse(dtTotalStock.Rows[0][2].ToString()) - int.Parse(dtStockIN.Rows[0][2].ToString())));
                datac.updateRecCmd("InventoryStock", fieldTotalStock[1], dtTotalStock.Rows[0][0].ToString(), fieldTotalStock, removeTotalStockvalues);//update the item's stock reference to undo the insert of quantity that was now beinig removed
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting current item from invoice using ISIdelItemInvoice_updateInventoryStock() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch(Exception ex)
        }//end of private void ISIdelItemInvoice_updateInventoryStock()
        private void ISIdelItemInvoiceSet()
        {
            try
            {
                MessageBox.Show("Record has been deleted from the invoice and stock.", "Record Deleted from Invoice!");
                btnISIinvDeleteItem.Enabled = false;
                btnISIsave.Enabled = false;
                dtgStockIn.DataSource = datac.getTable("Inventory"); ;//refresh datasource
                dtgInventoryValue.DataSource = datac.getInventoryValue();
                displayInventoryValue();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while loading settings for StockIn tab for deleteing current item from invoice using ISIdelItemInvoiceSet() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch(Exception ex)
        }//end of private void ISIdelItemInvoiceSet()
        #endregion

        private void isiClearTextboxes()
		{
			try
			{
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
			}//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while clearing textboxes in StockIn tab using isiClearTextboxes() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
		}//end of private void isiClearTextboxes()
       
        private void btnISIclear_Click(object sender, EventArgs e)
        {
            try
            {
                isiClearTextboxes();
                dtgStockIn.DataSource = datac.getTable("Inventory");
                btnISIinvDeleteItem.Enabled = false;
                btnISIdelItemInvoice.Enabled = false;
                btnISIsearchItem.Enabled = true;
                btnISIsave.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing textboxes using btnISIclear_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnISIclear_Click(object sender, EventArgs e)
       
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
                MessageBox.Show("Error occured while deleting record using btnISIinvDeleteItem_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnISIinvDeleteItem_Click(object sender, EventArgs e)
              
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
                    getIDValue.Add("=" + cmbISISupplier.SelectedValue);
                    dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID
                    dtgStockIn.DataSource = datac.getStockInInvoice(dtInvoiceIN.Rows[0][0].ToString());//give a datasource to preview a certain invoice reference's details in the dtg
                }//end of if (txtInvoiceNo.Text!="")
                else
                {
                    MessageBox.Show("No invoice number is given to preview.", "Preview not available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//end of else of if (txtInvoiceNo.Text!="")
               btnISIdelItemInvoice.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error previewing Invoice details using btnISIpreviewCurrent_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch(Exception ex)
        }//end of private void btnISIpreviewCurrent_Click(object sender, EventArgs e)
      
        private void btnISIrecordNewInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                isiClearTextboxes();
                dtgStockIn.DataSource = datac.getTable("Inventory");//refresh the dtg datasource
                dtgInvoiceHistory.DataSource = datac.getAllInvoice();//refresh the dtg datasource
                btnISIdelItemInvoice.Enabled = false;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error creating New Invoice record using btnISIrecordNewInvoice_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnISIrecordNewInvoice_Click(object sender, EventArgs e)
     
        #region tabStockIn_Save Controls
        private string getInventoryID()
        {
            #region Variables
            string getInvID = "";
            DataTable dtGetInventory = new DataTable();
            string[] fieldInv = { "InvCode", "InvItem", "InvDescription" };
            ArrayList arGetInventory = new ArrayList();
            #endregion

            arGetInventory.Add(txtISIinvCode.Text);
            arGetInventory.Add(txtISIinvItem.Text);
            arGetInventory.Add(txtISIinvDescription.Text);

            dtGetInventory = datac.getRecord("Inventory", fieldInv, arGetInventory);
            getInvID = dtGetInventory.Rows[0][0].ToString();

            txtISIinvID.Text = getInvID;

            return getInvID;
        }//end of private int getInventoryID()

        #region InsertReceived Methods
        private void insertRECEIVEDIntoInventoryStock()
        {
            try
            {
                #region Variables
                DataTable dtGetNewItem = new DataTable();
                ArrayList arTotalStock = new ArrayList();
                ArrayList arNewItem = new ArrayList();
                string[] filterField = { "InvItem" };
                #endregion 
                
                arNewItem.Add(txtISIinvItem.Text.ToUpper());//consider more than 1 search field to ensure there is NO other record close to it ( ref male/ female door retainer rubber)
                dtGetNewItem = datac.getRecord("Inventory", filterField, arNewItem);

                arTotalStock.Add(0);
                arTotalStock.Add(dtGetNewItem.Rows[0][0].ToString());
                arTotalStock.Add(txtISIstockReceived.Text);
                datac.insertCmd("InventoryStock", fieldTotalStock, arTotalStock);//create a reference to totalStock with first stock
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting new stock-count into InventoryStock using insertRECEIVEDIntoInventoryStock() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void insertRECEIVEDIntoInventoryStock()
        private void insertRECEIVEDIntoStockIn(string getItemID)
        {
            try
            {
                #region Variables
                DataTable dtInvoiceIN = new DataTable();
                string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"
                #endregion
                
                dtInvoiceIN = insertRECEIVEDIntoStockIn_getInvoice();
                if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
                {
                    insertRECEIVEDIntoStockIn_InsertStockIn(getItemID, dtInvoiceIN);
                }//end of if (txtISIstockPrice.Text!=""&&txtISIstockReceived.Text!="")
                else
                {
                    MessageBox.Show(@"Please enter a Price/Unit value and Quantity Received value in the corresponding fields.
                A reference to the Invoice has been created.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//end of else, if (txtISIstockPrice.Text!=""&&txtISIstockReceived.Text!="")
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting new stock into StockIn using insertRECEIVEDIntoStockIn() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void insertRECEIVEDIntoStockIn()
        private DataTable insertRECEIVEDIntoStockIn_getInvoice()
        {
            #region Variables
            DataTable dtInvoiceIN = new DataTable();
            ArrayList arGetInvoice = new ArrayList();
            string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"
            #endregion
            
            arGetInvoice.Add(" LIKE '" + txtInvoiceNo.Text + "'");
            arGetInvoice.Add("=" + cmbISISupplier.SelectedValue);
            dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, arGetInvoice);  //save invoice details in this dataTable, to get the precise ISIID
            return dtInvoiceIN;
        }//end of private DataTable insertRECEIVEDIntoStockIn_getInvoice()
        private void insertRECEIVEDIntoStockIn_InsertStockIn(string getItemID, DataTable dtInvoiceIN)
        {
            try
            {
                #region Variables
                ArrayList arStockIn = new ArrayList();
                #endregion

                arStockIn.Add(0);
                arStockIn.Add(getItemID);
                arStockIn.Add(txtISIstockReceived.Text);
                arStockIn.Add(txtISIstockPrice.Text);
                arStockIn.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
                arStockIn.Add(txtISIstockReceived.Text);
                datac.insertCmd("SubStockIN", fieldStockIN, arStockIn);// a new transaction with a reference to an invoice is created and inserted
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting new stock into StockIn using insertRECEIVEDIntoStockIn_InsertStockIn() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void insertRECEIVEDIntoStockIn_InsertStockIn()
        private void insertRECEIVEDIntoInvoiceIn()
        {
            try
            {
                #region Variables
                ArrayList arInvoiceIn = new ArrayList();
                DataTable dtInvoiceIN = new DataTable();
                ArrayList arGetInvoice = new ArrayList();
                string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"
                #endregion
                
                arInvoiceIn.Add(0);
                arInvoiceIn.Add(txtInvoiceNo.Text);
                arInvoiceIn.Add(dtpInvoiceDate.Text);
                arInvoiceIn.Add(cmbISISupplier.SelectedValue);
                arInvoiceIn.Add(txtISITotal.Text);
                datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, arInvoiceIn);
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting new invoice into InvoiceIn using insertRECEIVEDIntoInvoiceIn() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
            
        }//end of private void insertRECEIVEDIntoInvoiceIn()
        private void insertRECEIVEDIntoInventory()
        {
            try
            {
                #region Variables
                ArrayList arInventory = new ArrayList();
                #endregion
                if (txtISIinvItem.Text != "")
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting new item into Inventory using insertRECEIVEDIntoInventory() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void insertRECEIVEDIntoInventory()
        private void insertRECEIVEDIntoInvoiceIn_Stock()
        {
            try
            {
                #region Variables
                ArrayList arInvoiceIn = new ArrayList();
                ArrayList arGetInvoiceID = new ArrayList();
                DataTable dtLastStockInvoice = datac.getLastStockInvoice();
                string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"

                string arrLastStockInvoice = dtLastStockInvoice.Rows[0][0].ToString();
                string[] arrayInvoiceNo = arrLastStockInvoice.Split(' ');
                #endregion
                
               // int stockInvNo = int.Parse(arrayInvoiceNo[1].ToString());
                insertRECEIVEDIntoInvoiceIn_Stock_insertInvoice(arrayInvoiceNo);
                txtInvoiceNo.Text = arrayInvoiceNo[0].ToString() + " " + (int.Parse(arrayInvoiceNo[1].ToString()) + 1);
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting a stock invoice into InvoiceIn using insertRECEIVEDIntoInvoiceIn_Stock() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void insertRECEIVEDIntoInvoiceIn_Stock()
        private void insertRECEIVEDIntoInvoiceIn_Stock_insertInvoice(string[] arrayInvoiceNo)
        {
            try
            {
                #region Variables
                ArrayList arInvoiceIn = new ArrayList();
                #endregion

                arInvoiceIn.Add(0);
                arInvoiceIn.Add(arrayInvoiceNo[0].ToString() + " " + (int.Parse(arrayInvoiceNo[1].ToString()) + 1));
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
                datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, arInvoiceIn);
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting a stock invoice into InvoiceIn using insertRECEIVEDIntoInvoiceIn_Stock_insertInvoice() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void insertRECEIVEDIntoInvoiceIn_Stock_insertInvoice() 
        #endregion

        #region UpdateReceived Methods
        private void updateRECEIVEDInventoryStock()
        {
            try
            {
                #region MyRegion
                ArrayList arTotalStock = new ArrayList();
                #endregion
                if (txtISIstockReceived.Text != "")
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating stock in InventoryStock using updateRECEIVEDInventoryStock() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
           
        }//end of private void updateRECEIVEDInventoryStock()
        private void updateRECEIVEDStockIn()
        {
            try
            {
                #region Variables
                DataTable dtFIFOInvoice = new DataTable();
                ArrayList arStockIn = new ArrayList();
                #endregion

                dtFIFOInvoice = datac.getFIFODatedPrice(txtISIinvID.Text);

                arStockIn.Add(dtFIFOInvoice.Rows[0][0].ToString());
                arStockIn.Add(dtFIFOInvoice.Rows[0][1].ToString());
                arStockIn.Add(dtFIFOInvoice.Rows[0][3].ToString());
                arStockIn.Add(dtFIFOInvoice.Rows[0][4].ToString());
                arStockIn.Add(dtFIFOInvoice.Rows[0][5].ToString());
                arStockIn.Add(int.Parse(dtFIFOInvoice.Rows[0][6].ToString()) + +int.Parse(txtISIstockReceived.Text));
                datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtFIFOInvoice.Rows[0][0].ToString(), fieldStockIN, arStockIn);//update existing record
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating stock received in StockIn using updateRECEIVEDStockIn() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
            
        }//end of private void updateRECEIVEDStockIn()
        private void updateRECEIVEDInventory()
        {
            try
            {
                #region MyRegion
                ArrayList arInvValues = new ArrayList();
                #endregion
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating stock in Inventrory using updateRECEIVEDInventory() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void updateRECEIVEDInventory()
        #endregion

        #region InvoiceExist Methods/ Variations
        private void invoiceExist_IDexists()
        {
            try
            {
                updateRECEIVEDInventory();//use this to update inventory details
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceExist_IDexists() while updating an invoice with existing item received: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void invoiceExist_IDexists()
        private void invoiceExist_LookupID()
        {
            try
            {
                #region Variables
                DataTable dtGetStock = new DataTable();
                DataTable dtGetInventory = new DataTable();
                Boolean invIDExists = false;
                #endregion
                
                dtGetInventory = datac.getTable("Inventory");
                dtGetStock = datac.getTable("InventoryStock");

                invIDExists = testDTIfExistAddOne(dtGetInventory, 2, txtISIinvItem.Text);//see if the item is present
                if (invIDExists == true)
                {//when item exists but ID is wrong, get correct ID
                    invoiceExist_LookupID_CheckID(dtGetStock);
                }//end of if (invIDExists == true)
                else
                {
                    MessageBox.Show("ID could not be found.");
                }//end of else if (invIDExists == true)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceExist_LookupID() while updating an invoice with item to lookup ID : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of  private void invoiceExist_LookupID()
        private void invoiceExist_LookupID_CheckID(DataTable dtGetStock)
        {
            try
            {
                #region Variables
                string getInvID = "";
                Boolean stockIDExists = false;
                #endregion                

                getInvID = getInventoryID();
                stockIDExists = testDTIfExistAddOne(dtGetStock, 1, txtISIinvID.Text);
                updateRECEIVEDInventory();//use to update inventory details
                invoiceExist_LookupID_ReceivedIN(stockIDExists);
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceExist_LookupID_CheckID(DataTable dtGetStock) while updating an invoice with item to lookup ID : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void invoiceExist_LookupID_CheckID()
        private void invoiceExist_LookupID_ReceivedIN(Boolean stockIDExists)
        {
            try
            {
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceExist_LookupID_ReceivedIN(Boolean stockIDExists) while updating an invoice with item to lookup ID : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void invoiceExist_LookupID_ReceivedIN()
        private void invoiceExist_IDnotExist()
        {
            try
            {
                #region Variables
                Boolean invIDExists = false;
                #endregion
                
                invIDExists = testNewItemExist();
                if (invIDExists == true)
                {//if the item was found, get the correct ID
                    InvoiceExist_IDnotExist_IDtestFound();
                }//end of if (invIDExists == true)
                else
                {//no id found for this item
                   InvoiceExist_IDnotExist_IDtestNotFound(); 
                }//end of else if (invIDExists == true)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceExist_IDnotExist while updating an invoice with new item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of  private void invoiceExist_IDnotExist()
        private void InvoiceExist_IDnotExist_IDtestFound()
        {
            try
            {
                updateRECEIVEDInventory();//use to update inventory details
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred inInvoiceExist_IDnotExist_IDtestFound() while updating an invoice with an item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void InvoiceExist_IDnotExist_IDtestFound()
        private void InvoiceExist_IDnotExist_IDtestNotFound()
        {
            try
            {
                #region Variables
                string getInvID = "";
                #endregion
                
                if (txtISIinvItem.Text != "")
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred InvoiceExist_IDnotExist_IDtestNotFound() while updating an invoice with new item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void InvoiceExist_IDnotExist_IDtestNotFound()
        #endregion

        #region InvoiceNOTexist Methods / Variations
        private void invoiceNOTexist_IDexist()
        {
            try
            {
                updateRECEIVEDInventory();//use to update inventory details
                doInsertIntoInvoiceAndStock();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceNOTexist_IDexist() while creating new invoice with existing item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void invoiceNOTexist_IDexist()
        private void invoiceNOTexist_LookupID()
        {
            try
            {
                #region Variables
                Boolean invIDExists = false;
                Boolean stockIDExists = false;
                string getInvID = "";
                DataTable dtGetInventory = new DataTable();
                DataTable dtGetStock = new DataTable();
                #endregion
                
                //do stuff
                dtGetInventory = datac.getTable("Inventory");
                dtGetStock = datac.getTable("InventoryStock");

                invIDExists = testDTIfExistAddOne(dtGetInventory, 2, txtISIinvItem.Text);//see if the item is present
                if (invIDExists == true)
                {//when item exists but ID is wrong, get correct ID
                    getInvID = getInventoryID();
                    stockIDExists = testDTIfExistAddOne(dtGetStock, 1, txtISIinvID.Text);
                    updateRECEIVEDInventory();
                    doInsertIntoInvoiceAndStock();
                }//end of if (invIDExists == true)
                else
                {
                    MessageBox.Show("ID could not be found.");
                }//end of else if (invIDExists == true)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceNOTexist_LookupID() while creating new invoice with lookup item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
            
        }//end of  private void invoiceNOTexist_LookupID()
        private void invoiceNOTexist_IDnotExist()
        {
            try
            {
                #region Variables
                Boolean invIDExists = false;
                DataTable dtGetStock = new DataTable();
                #endregion
                
                //do stuff
                dtGetStock = datac.getTable("InventoryStock");
                invIDExists = testNewItemExist();

                if (invIDExists == true)
                {//when item exists but ID is wrong, get correct ID
                    invoiceNOTexist_IDnotExist_ItemFound(dtGetStock);

                }//end of if (invIDExists == true)
                else
                {
                    invoiceNOTexist_IDnotExist_CreateNewItem();
                }//end of else if (invIDExists == true)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceNOTexist_IDnotExist() while creating new invoice with new item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of  private void invoiceNOTexist_IDnotExist()
        private void invoiceNOTexist_IDnotExist_CreateNewItem()
        {
            try
            {
                #region Variables
                string getInvID = "";
                #endregion
                
                if (txtISIinvItem.Text != "")
                {
                    MessageBox.Show("Making new item.");
                    //item is new
                    if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
                    {
                        insertRECEIVEDIntoInventory();
                        getInvID = getInventoryID();
                        doInsertIntoInvoiceAndStock();
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceNOTexist_IDnotExist_CreateNewItem() while creating new invoice with new item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void invoiceNOTexist_IDnotExist_CreateNewItem()
        private void invoiceNOTexist_IDnotExist_ItemFound(DataTable dtGetStock)
        {
            try
            {
                #region Variables
                string getInvID = "";
                Boolean stockIDExists = false;
                #endregion
                
                getInvID = getInventoryID();
                stockIDExists = testDTIfExist(dtGetStock, 1, txtISIinvID.Text);
                doInsertIntoInvoiceAndStock();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceNOTexist_IDnotExist_ItemFound(DataTable dtGetStock) while creating new invoice with new item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void invoiceNOTexist_IDnotExist_ItemFound()
        #endregion

        #region InvoiceEmpty Methods / Variations
        private void invoiceEmpty_IDexist()
        {
            try
            {
                #region Variables
                DataTable dtGetInventory = new DataTable();
                DataTable dtGetStock = new DataTable();
                #endregion
                
                //do stuff
                dtGetInventory = datac.getTable("Inventory");
                updateRECEIVEDInventory();//use to update inventory details
                invoiceEmpty_IDExist_recirculate();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceEmpty_IDexist() while creating stock invoice with existing item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)

        }//end of private void invoiceEmpty_IDexist()
        private void invoiceEmpty_LookupID()
        {
            try
            {
                #region Variables
                Boolean invIDExists = false;
                DataTable dtGetInventory = new DataTable();
                #endregion
                
                //do stuff
                dtGetInventory = datac.getTable("Inventory");
                invIDExists = testDTIfExistAddOne(dtGetInventory, 2, txtISIinvItem.Text);
                if (invIDExists == true)//check if name exists in the Inventory table
                {//if id exist for item, give correct id
                    txtISIinvID.Text = getInventoryID();
                    invoiceEmpty_IDExist_recirculate();
                }//end of  if (invIDExists == true)
                else
                {//error
                    MessageBox.Show("ID could not be found.");
                    invoiceEmpty_IDnotExist();
                }//end of else  if (invIDExists == true)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceEmpty_LookupID() while creating stock invoice with lookup item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
            
        }//end of  private void invoiceEmpty_LookupID()
        private void invoiceEmpty_IDnotExist()
        {//item is new
            try
            {
                #region Variables
                string getInvID = "";
                #endregion
                
                if (txtISIinvItem.Text != "")
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceEmpty_IDnotExist() while creating stock invoice with new item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
          
        }//end of  private void invoiceEmpty_IDnotExist()
        private void invoiceEmpty_IDExist_recirculate()
        {
            try
            {
                #region Variables
                DataTable dtFifoDatedRec = new DataTable();
                DataTable dtAllStockIn = new DataTable();
                string[] StockInCol = { "SubStockINID", "InventoryID", "SSIStockLeft" };
                ArrayList updateStockValues = new ArrayList();
                Boolean exist = false;
                #endregion
                
                dtAllStockIn = datac.getTable("SubStockIn");
                exist = testDTIfExist(dtAllStockIn, 1, txtISIinvID.Text);
                updateRECEIVEDInventory();//use to update inventory details
                if (exist == true)
                {//get oldest dated record that has a >0 value, then do update to add stock to it from stock received
                    if (txtISIstockReceived.Text != "")
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in invoiceEmpty_IDExist_recirculate() while trying to recirculate stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void invoiceEmpty_IDExist_recirculate()
        #endregion
               
        #region Save Realted Methods
        private void btnISISave_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                Boolean invoiceExists = false;
                DataTable dtGetInventory = new DataTable();
                #endregion
                
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
                            saveInvoiceExist();
                        }//end of if (result == DialogResult.Yes)
                        else
                        {
                            saveInvoiceFound_notAdd();
                        }//end of else if (result == DialogResult.Yes)
                    }//end of  if (invoiceExists == true)
                    else
                    {//invoice not exist
                        saveInvoiceNotExist();
                    }//end of else if (invoiceExists == true)
                }//end of if (txtInvoiceNo.Text != "")
                else
                {//empty invoice
                    DialogResult resultStock = MessageBox.Show(@"Invoice Number field is empty.
                    Reciculate the stock?", "Empty invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultStock == DialogResult.Yes)
                    {
                        saveInvoiceEmpty_Recirculate_();
                    }//end of if (resultStock == DialogResult.Yes)
                    else
                    {//result click no, message
                        MessageBox.Show("Please provide an invoice number.");
                    }//end of else if (resultStock == DialogResult.Yes)
                }//end of else if (txtInvoiceNo.Text != "")
                saveButtonSet();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in btnISISave_Click() while saving: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of  private void btnISIDUMMY_Click(object sender, EventArgs e)
        private void saveInvoiceExist()
        {
            try
            {
                //the inventory ID is NOT empty
                #region Variables
                Boolean invIDExists = false;
                DataTable dtGetInventory = new DataTable();
                #endregion
                
                dtGetInventory = datac.getTable("Inventory");
                if (txtISIinvID.Text != "")
                {
                    //------------------------------the inventory ID is NOT empty
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in saveInvoiceExist() while saving: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void saveInvoiceExist_InvIDExist()
        private void saveInvoiceNotExist()
        {
            try
            {
                #region Variables
                 Boolean invIDExists = false;
                DataTable dtGetInventory = new DataTable();
                #endregion
               
                dtGetInventory = datac.getTable("Inventory");
                if (txtISIinvID.Text != "")
                {
                    //------------------------------the inventory ID is NOT empty
                    invIDExists = testDTIfExistAddOne(dtGetInventory, 0, txtISIinvID.Text);
                    if (invIDExists == true)
                    {//see if the inventory ID was found in the inventoryStock table
                        invoiceNOTexist_IDexist();
                    }//end of if (stockIDExists == true)
                    else
                    {//this is where the ID is not empty, but is found as non existant, so consider that the ID is only wrong and item might exist
                        invoiceNOTexist_LookupID();
                    }//end of else if (stockIDExists == true)
                    //------------------------------
                }//end of  if (txtISIinvID.Text != "")
                else
                {//if the ID textfield is completely empty, for example for new item
                    invoiceNOTexist_IDnotExist();
                }//end of else if (txtISIinvID.Text != "")
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in saveInvoiceNotExist() while saving: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void saveInvoiceNotExist_InvIDExist()
        private void saveInvoiceEmpty_Recirculate_()
        {
            try
            {
                #region MyRegion
                Boolean invIDExists = false;
                DataTable dtGetInventory = new DataTable();
                #endregion
                
                dtGetInventory = datac.getTable("Inventory");
                if (txtISIinvID.Text != "")
                {
                    //------------------------------
                    invIDExists = testDTIfExistAddOne(dtGetInventory, 0, txtISIinvID.Text);
                    if (invIDExists == true)
                    {
                        invoiceEmpty_IDexist();
                    }//end of if (invIDExists == true)
                    else
                    {//look for correct id, if id is wrong
                        invoiceEmpty_LookupID();
                    }//end of else if (invIDExists == true)
                    //------------------------------
                }//end of  if (txtISIinvID.Text != "")
                else
                { //item is new
                    invoiceEmpty_IDnotExist();
                }//end of else  if (txtISIinvID.Text != "")
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in saveInvoiceEmpty_Recirculate_() while saving: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void saveInvoiceEmpty_Recirculate_()
        private void saveInvoiceFound_notAdd()
        {
            try
            {
                //result is NO to exist invoice, clear to make possible for new invoice
                MessageBox.Show("Give new invoice ref");
                txtInvoiceNo.Clear();
                txtISITotal.Clear();
                btnISIpreviewCurrent.Enabled = false;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in saveInvoiceFound_notAdd() while saving: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void saveInvoiceFound_notAdd()
        private void saveButtonSet()
        {
            try
            {
                dtgStockIn.DataSource = datac.getTable("Inventory");//to do a datasource update after any modifications made to inventory
                btnISIpreviewCurrent.Enabled = true;
                btnISIrecordNewInvoice.Enabled = true;
                btnISIdelItemInvoice.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in saveButtonSet() while saving: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void saveButtonSet()
        #endregion

        #region Test Existance methods
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
        private void test_Update_Insert_InventoryStock()
        {
            try
            {
                #region Variables
                Boolean stockIDExists = false;
                DataTable dtGetStock = new DataTable();
                #endregion

                dtGetStock = datac.getTable("InventoryStock");
                stockIDExists = testDTIfExist(dtGetStock, 1, txtISIinvID.Text);
                if (txtISIstockReceived.Text != "")
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in test_Update_Insert_InventoryStock() while testing for InventoryStock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)

        }//end of private void test_Update_Insert_InventoryStock()
        #endregion
       
        private void doInsertIntoInvoiceAndStock()
        {
            try
            {
                if (txtISIinvID.Text != "")
                {
                    if (txtISIstockPrice.Text != "" && txtISIstockReceived.Text != "")
                    {
                        if (txtInvoiceNo.Text == "")
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in doInsertIntoInvoiceAndStock() while doing inserts for stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
          
        }//end of private void doInsertIntoInvoiceAndStock()
        
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
                MessageBox.Show("Error clearing data from Invoice textBoxes using btnISIinvoiceClear_Click(): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch
        }//end of private void btnISIinvoiceClear_Click(object sender, EventArgs e)
        #endregion
        #endregion

        #region tabSupplier Controls
        private ArrayList supplierValues()
        {
            #region Variables
            ArrayList values = new ArrayList(); //make arrayList to store all values of current record
            #endregion
            
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
            if (txtsupDiscount.Text != "")
            {
                values.Add(txtsupDiscount.Text);
            }//end of if (txtsupDiscount.Text!="")
            else
            {
                values.Add(txtsupDiscount.Text);
            }//end of else, if (txtsupDiscount.Text!="")

            return values;
        }//end of private ArrayList supplierValues()

        private void btnSupSearch_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record    
                string[] searchField = { "SupName" };
                #endregion
                buttonsSupInitialSet();
                if (txtSName.Text != "")
                {
                    values.Add(txtSName.Text);
                    dtgSuppliers.DataSource = datac.getRecord("Supplier", searchField, values);
                }//end of if (txtSName.Text!=null)
                else
                {
                    MessageBox.Show("Please enter a supplier to search for", "Search failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearSupplierTextboxes();
                }//end of else if (txtSName.Text != "")
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes using btnSupSearch_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSupSearch_Click(object sender, EventArgs e)
    
        private void buttonsSupInitialSet()
        {
            try
            {
                btnSupINSERT.Enabled = false;
                btnSupSearch.Enabled = true;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;
                btnDeleteSup.Enabled = false;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Suppliers tab using btnSupSearchSet() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void btnSupSearchSet()
               
        private void populateSupTxtboxSet()
        {
            try
            {
                btnSupINSERT.Enabled = false;
                btnSupSearch.Enabled = false;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = true;
                btnDeleteSup.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Suppliers tab using clickSupDTGbuttonSet() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }//end of catch (Exception ex)
        }//end of public void clickSupDTGbuttonSet()

        private void clearSupplierTextboxes()
        {
            try
            {
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
                MessageBox.Show("Error clearing data from textBoxes using clearSupplierTextboxes() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of public void clearSupplierTextboxes()
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

                clearSupplierTextboxes();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing textBoxes using btnSupClear_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSupClear_Click(object sender, EventArgs e)

        private void dtgSuppliers_Click(object sender, EventArgs e)
        {
            try
            {
                populateSupTxtboxSet();
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
                MessageBox.Show("Error loading data from datagrid to textBoxes using dtgSuppliers_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                populateSupTxtboxSet();
                values= supplierValues();

                datac.updateRecCmd("Supplier", fieldID, txtSID.Text, fieldSup, values);        //Send values in fieldSup string format from textBox/ comboBox through updateCmd query to database table using InvID as key
                dtgSuppliers.DataSource = datac.getTable("Supplier");//get the entire table as datasource to dtg
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating using btnSupUPDATE_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSupUPDATE_Click(object sender, EventArgs e)

        private void btnSupINSERT_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                #endregion

                btnSupInsertSet();

                values = supplierValues();
           
                datac.insertCmd("Supplier", fieldSup, values);       ////Send values in fieldSup string format through insertCmd query to database table
                dtgSuppliers.DataSource = datac.getTable("Supplier");//refresh the datasource to display the newly inserted
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating new record using btnSupINSERT_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSupINSERT_Click(object sender, EventArgs e)
        private void btnSupInsertSet()
        {
            try
            {
                btnSupINSERT.Enabled = true;
                btnSupSearch.Enabled = false;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Suppliers tab using btnSupInsertSet() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSupInsertSet()
              
        private void deleteSupSet()
        {
            try
            {
                btnSupINSERT.Enabled = false;
                btnSupSearch.Enabled = false;
                btnSupClear.Enabled = true;
                btnSupUPDATE.Enabled = false;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Suppliers tab using deleteSupSet() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void deleteSupSet()
        private ArrayList supplierDeleteValues()
        {
            #region Variables
            ArrayList values = new ArrayList(); //make arrayList to store all values of current record
            #endregion
            
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
            return values;
        }//end of private ArrayList supplierDeleteValues()
        private void btnDeleteSup_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record
                string fieldID = "SupplierID";
                #endregion
                deleteSupSet();

                values = supplierDeleteValues();
          
                datac.updateRecCmd("Supplier", fieldID, txtSID.Text, fieldSup, values);        //Send values in fieldSup string format from textBox/ comboBox through updateCmd query to database table using InvID as key and only update isDeleted column
                clearSupplierTextboxes();

                dtgSuppliers.DataSource = datac.getTable("Supplier");
                MessageBox.Show(@"This supplier has been marked as deleted.
It will still show in the grid, because there may be references to this record.", "Deleted Record!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting record using btnDeleteSup_Click(): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnDeleteSup_Click(object sender, EventArgs e)
        #endregion

        #region tabStockCheck Controls
        #region StockCheck Search
        private void btnSearchSCinv_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                ArrayList values = new ArrayList(); //make arrayList to store all values of current record 
                #endregion
                  
                if (txtSCinvCode.Text != "")
                {
                    string[] searchField= { "InvCode" };
                    values.Add(txtSCinvCode.Text);
                    dtgCheckStock.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvCode.Text!=null)
                else if (txtSCinvItem.Text != "")
                {
                    string[] searchField = { "InvItem" };
                    values.Add(txtSCinvItem.Text.ToUpper());
                    dtgCheckStock.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvItem.Text!=null)
                else if (txtSCinvDesc.Text != "")
                {
                    string[] searchField = { "InvDescription" };
                    values.Add(txtSCinvDesc.Text.ToUpper());
                    dtgCheckStock.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvDesc.Text!=null)
                else if (txtSCinvCat.Text != "")
                {
                    string[] searchField = { "InvCategory" };
                    values.Add(txtSCinvCat.Text.ToUpper());
                    dtgCheckStock.DataSource = datac.getRecord("Inventory", searchField, values);
                }//end of if (txtInvDesc.Text!=null)
                else //if all searchable fiels are still empty, then do this. 
                {
                    MessageBox.Show("Please enter value to search for", "Search failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSearchSCinv_clearText();
                }//end of else
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred using btnSearchSCinv_Click() method to search: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSearchSCinv_Click(object sender, EventArgs e)
        private void btnSearchSCinv_clearText()
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred using btnSearchSCinv_clearText() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSearchSCinv_clearText()
        #endregion

        #region StockCheck dtgCheckStock_click
        private void dtgCheckStock_Click(object sender, EventArgs e)
        {
            try
            {
                txtSCinvID.Text = dtgCheckStock.SelectedRows[0].Cells[0].Value.ToString();
                txtSCinvCode.Text = dtgCheckStock.SelectedRows[0].Cells[1].Value.ToString();
                txtSCinvItem.Text = dtgCheckStock.SelectedRows[0].Cells[2].Value.ToString().ToUpper();
                txtSCinvDesc.Text = dtgCheckStock.SelectedRows[0].Cells[3].Value.ToString().ToUpper();
                txtSCinvCat.Text = dtgCheckStock.SelectedRows[0].Cells[5].Value.ToString().ToUpper();
                txtSCinvRelevel.Text = dtgCheckStock.SelectedRows[0].Cells[6].Value.ToString();

                txtSCactualTotal.Clear();

                dtgCheckStock_Click_getTotalStock();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes from Check Stock using dtgCheckStock_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void dtgCheckStock_Click(object sender, EventArgs e)
        private void dtgCheckStock_Click_getTotalStock()
        {
            try
            {
                #region Variables
                DataTable testTotalStock = new DataTable();
                Boolean exists = new Boolean();
                ArrayList getIDValue = new ArrayList();
                DataTable dtTotalStock = new DataTable();
                string[] fieldTotal = { "InventoryID" };
                #endregion
                
                testTotalStock = datac.getTable("InventoryStock");
                exists = testDTIfExistAddOne(testTotalStock, 1, txtSCinvID.Text);

                if (exists == true)
                {//if the flag is true, meaning the record already exists, will then get references
                    dtgCheckStock_Click_getTotalStockExist(); 
                }//end of if (flag == true)
                else
                {
                    MessageBox.Show(@"Please log the stock in the STOCK IN tab as stock received FIRST, to ensure that all the correct references are established.", "No References Were Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSCsave.Enabled = false; //disable button so that nothing can happen- force to either choose another item or go to StockIN
                }//end of else of if (flag == true)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes from Check Stock using dtgCheckStock_Click_getTotalStock() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of  private void dtgCheckStock_Click_getTotalStock()
        private void dtgCheckStock_Click_getTotalStockExist()
        {
            try
            {
                #region Variables
                ArrayList getIDValue = new ArrayList();
                DataTable dtTotalStock = new DataTable();
                string[] fieldTotal = { "InventoryID" };
                #endregion
                
                getIDValue.Add("=" + txtSCinvID.Text);
                dtTotalStock = datac.getMathRecord("InventoryStock", fieldTotal, getIDValue);
                if (dtTotalStock.Rows[0] != null)
                {
                    txtSCsysTotal.Text = dtTotalStock.Rows[0][2].ToString();
                }//end of if (dtStockIn.Rows[0] != null)
                else
                {
                    MessageBox.Show("No records found in datatable using dtgCheckStock_Click() method: ");
                } //end of else  
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from datagrid to textBoxes from Check Stock using dtgCheckStock_Click_getTotalStockExist() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void dtgCheckStock_Click_getTotalStockExist()
        #endregion

        #region StockCheck btnSCsave methods
        private void btnSCsave_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                ////datatables
                //DataTable dtTempStockIN = new DataTable();
                //DataTable dtTotalStock = new DataTable();
                ////arraylists
                //ArrayList updateStock = new ArrayList();
                //ArrayList updateStockIn = new ArrayList();
                //ArrayList addToOUT = new ArrayList();
                //ArrayList getIDValue = new ArrayList();
                ////arrays
                //string[] fieldTotal = { "InventoryID" };
                //variables
              //  int difference = 0;
                #endregion
                #region oldStuff
                //getIDValue.Add("=" + txtSCinvID.Text);

                //if (txtSCactualTotal.Text != "")//if there is something to compare the system total with, then do this
                //{
                //    //do some testing with the two values, then do something with the difference, if any
                //    if ((int.Parse(txtSCsysTotal.Text)) > (int.Parse(txtSCactualTotal.Text)))
                //    {
                //        //part 1: change totalStock
                //        updateStock.Add(0);
                //        updateStock.Add(txtSCinvID.Text);
                //        updateStock.Add(txtSCactualTotal.Text);
                //        datac.updateRecCmd("InventoryStock", fieldTotalStock[1].ToString(), txtSCinvID.Text, fieldTotalStock, updateStock);
                //        //part 2: if system is more, stockIn should decrease with the difference
                //        difference = ((int.Parse(txtSCsysTotal.Text)) - (int.Parse(txtSCactualTotal.Text)));//the difference between system and actual
                //        dtTempStockIN = datac.getFIFODatedPrice(txtSCinvID.Text);
                //        for (int i = 0; i < difference - 1; i++)
                //        {
                //            if (int.Parse(dtTempStockIN.Rows[0][5].ToString()) > 0)
                //            {
                //                updateStockIn.Add(dtTempStockIN.Rows[0][0].ToString());
                //                updateStockIn.Add(dtTempStockIN.Rows[0][1].ToString());
                //                updateStockIn.Add(dtTempStockIN.Rows[0][2].ToString());
                //                updateStockIn.Add(dtTempStockIN.Rows[0][3].ToString());
                //                updateStockIn.Add(dtTempStockIN.Rows[0][5].ToString());
                //                updateStockIn.Add(int.Parse(dtTempStockIN.Rows[0][6].ToString()) - 1);
                //                datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtTempStockIN.Rows[0][0].ToString(), fieldStockIN, updateStockIn);
                //            }//end of if (int.Parse(dtSubStockIN.Rows[0][2].ToString())>0)
                //            else
                //            {//this is for when the last loaded record's LEFT has run to 0 and the counter is not yet 0
                //                dtTempStockIN = datac.getFIFODatedPrice(txtSCinvID.Text);
                //                updateStockIn.Add(dtTempStockIN.Rows[0][0].ToString());
                //                updateStockIn.Add(dtTempStockIN.Rows[0][1].ToString());
                //                updateStockIn.Add(dtTempStockIN.Rows[0][2].ToString());
                //                updateStockIn.Add(dtTempStockIN.Rows[0][3].ToString());
                //                updateStockIn.Add(dtTempStockIN.Rows[0][5].ToString());
                //                updateStockIn.Add(int.Parse(dtTempStockIN.Rows[0][6].ToString()) - 1);
                //                datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtTempStockIN.Rows[0][0].ToString(), fieldStockIN, updateStockIn);
                //            }//end of else
                //        }//end of for (int i = 0; i < int.Parse(txtInvStockOut.Text); i++)
                //        //part 3: record difference as a outgoing transaction
                //        addToOUT.Add(0);
                //        addToOUT.Add(txtSCinvID.Text);
                //        addToOUT.Add(difference);
                //        addToOUT.Add(DateTime.Now);//not 100% sure if this is how you do is programmatically-get today's date and time
                //        addToOUT.Add("false");//stating that somewhere stock was taken with no record of it
                //        datac.insertCmd("SubStockOUT", fieldStockOut, addToOUT);
                //    }//end of if (int.Parse(txtSCsysTotal.Text)>int.Parse(txtSCactualTotal.Text)) 
                //    else if ((int.Parse(txtSCsysTotal.Text)) < (int.Parse(txtSCactualTotal.Text)))
                //    {
                //        updateStock.Add(0);
                //        updateStock.Add(txtSCinvID.Text);
                //        updateStock.Add(txtSCactualTotal.Text);
                //        datac.updateRecCmd("InventoryStock", fieldTotalStock[1].ToString(), txtSCinvID.Text, fieldTotalStock, updateStock);
                //        difference = (int.Parse(txtSCactualTotal.Text) - int.Parse(txtSCsysTotal.Text));//the difference between system and actual
                //        for (int i = 0; i < difference - 1; i++)
                //        {
                //            updateStockIn.Add(dtTempStockIN.Rows[0][0].ToString());
                //            updateStockIn.Add(dtTempStockIN.Rows[0][1].ToString());
                //            updateStockIn.Add(dtTempStockIN.Rows[0][2].ToString());
                //            updateStockIn.Add(dtTempStockIN.Rows[0][3].ToString());
                //            updateStockIn.Add(dtTempStockIN.Rows[0][5].ToString());
                //            updateStockIn.Add(int.Parse(dtTempStockIN.Rows[0][6].ToString()) + 1);
                //            datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtTempStockIN.Rows[0][0].ToString(), fieldStockIN, updateStockIn);
                //            updateStockIn = new ArrayList();//basically clearing this arrayList
                //            dtTempStockIN = datac.getFIFODatedPrice(txtSCinvID.Text);//get the record again after the update
                //        }//end of for (int i = 0; i < int.Parse(txtInvStockOut.Text); i++)
                //    }//end of  else if (int.Parse(txtSCsysTotal.Text) < int.Parse(txtSCactualTotal.Text))
                //    else if (int.Parse(txtSCsysTotal.Text) == int.Parse(txtSCactualTotal.Text))
                //    {
                //        MessageBox.Show("No changes are needed. Please select another item.");
                //    }//end of else if (int.Parse(txtSCsysTotal.Text) == int.Parse(txtSCactualTotal.Text))
                //}//enf of if (txtSCactualTotal.Text!="")
                //else
                //{
                //    MessageBox.Show("Please provide a value to compare the System's stock with.");
                //}//end of else if (txtSCactualTotal.Text!="")
                //dtTotalStock = datac.getMathRecord("InventoryStock", fieldTotal, getIDValue);
                //if (dtTotalStock.Rows[0] != null)
                //{
                //    txtSCsysTotal.Text = dtTotalStock.Rows[0][2].ToString();
                //}//end of if (dtStockIn.Rows[0] != null)
                //else
                //{
                //    MessageBox.Show("No records found in datatable using btnSCsave_Click() method: ");
                //} //end of else 
                //dtgCheckStock.DataSource = datac.getTable("Inventory");
                //dtgInventoryValue.DataSource = datac.getInventoryValue();
                //dtgSupOrderList.DataSource = datac.getLowStock();

                ////to calculate total value of stock based on quantity and price on inventoryValue tab****
                //displayInventoryValue();
                #endregion
                if (txtSCactualTotal.Text != "")//if there is something to compare the system total with, then do this
                {
                    if ((int.Parse(txtSCsysTotal.Text)) > (int.Parse(txtSCactualTotal.Text)))
                    {
                        btnSCsave_SystemStockMore();
                    }//end of if ((int.Parse(txtSCsysTotal.Text)) > (int.Parse(txtSCactualTotal.Text)))
                    else if ((int.Parse(txtSCsysTotal.Text)) < (int.Parse(txtSCactualTotal.Text)))
                    {
                        btnSCsave_SystemStockLess();
                    }//end of else if ((int.Parse(txtSCsysTotal.Text)) < (int.Parse(txtSCactualTotal.Text)))
                     else if (int.Parse(txtSCsysTotal.Text) == int.Parse(txtSCactualTotal.Text))
                    {
                        MessageBox.Show("No changes are needed. Please select another item.");
                    }//end of else if (int.Parse(txtSCsysTotal.Text) == int.Parse(txtSCactualTotal.Text))
                }//enf of if (txtSCactualTotal.Text!="")
                else
                {
                    MessageBox.Show("Please provide a value to compare the System's stock with.");
                }//end of else if (txtSCactualTotal.Text!="")
                btnSCsave_getSysTotal();
                btnSCsave_RefreshGrids();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Stock Taking using btnSCsave_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSCsave_Click(object sender, EventArgs e)
        private void btnSCsave_SystemStockMore()
        {
            try
            {
                #region Variables
                int difference = 0;
                DataTable dtTempStockIN = new DataTable();
                #endregion

                btnSCsave_UpdateInventoryStock();
                difference = ((int.Parse(txtSCsysTotal.Text)) - (int.Parse(txtSCactualTotal.Text)));//the difference between system and actual
                dtTempStockIN = datac.getFIFODatedPrice(txtSCinvID.Text);

                for (int i = 0; i < difference - 1; i++)
                {
                    if (int.Parse(dtTempStockIN.Rows[0][5].ToString()) > 0)
                    {
                        btnSCsave_insertStockin();
                    }//end of if (int.Parse(dtTempStockIN.Rows[0][5].ToString()) > 0)
                    else
                    {
                        dtTempStockIN = datac.getFIFODatedPrice(txtSCinvID.Text);
                        btnSCsave_insertStockin();
                    }//end of else, if (int.Parse(dtTempStockIN.Rows[0][5].ToString()) > 0)
                }//end of for (int i = 0; i < difference - 1; i++)
                btnSCsave_InsertStockOut(difference);
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Stock Taking using btnSCsave_SystemStockMore() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSCsave_SystemStockMore()
        private void btnSCsave_SystemStockLess()
        {
            try
            {
                #region Variables
                int difference = 0;
                #endregion
                
                btnSCsave_UpdateInventoryStock();
                difference =(int.Parse(txtSCactualTotal.Text)- int.Parse(txtSCsysTotal.Text)) ;//the difference between system and actual
                for (int i = 0; i < difference - 1; i++)
                {
                    btnSCsave_InsertMoreStockIn();
                }//end of for (int i = 0; i < int.Parse(txtInvStockOut.Text); i++)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Stock Taking using btnSCsave_SystemStockLess() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSCsave_SytemStockLess()
        private void btnSCsave_insertStockin()
        {
            try
            {
                #region Variables
                ArrayList updateStockIn = new ArrayList();
                DataTable dtTempStockIN = new DataTable();
                #endregion
                
                updateStockIn.Add(dtTempStockIN.Rows[0][0].ToString());
                updateStockIn.Add(dtTempStockIN.Rows[0][1].ToString());
                updateStockIn.Add(dtTempStockIN.Rows[0][2].ToString());
                updateStockIn.Add(dtTempStockIN.Rows[0][3].ToString());
                updateStockIn.Add(dtTempStockIN.Rows[0][5].ToString());
                updateStockIn.Add(int.Parse(dtTempStockIN.Rows[0][6].ToString())-1);
                datac.updateRecCmd("SubStockIN", fieldStockIN[0],dtTempStockIN.Rows[0][0].ToString(),fieldStockIN,updateStockIn);
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Stock Taking using btnSCsave_insertStockin() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSCsave_insertStockin()
        private void btnSCsave_UpdateInventoryStock()
        {
            try
            {
                #region Variables
                ArrayList updateStock = new ArrayList();
                #endregion

                updateStock.Add(0);
                updateStock.Add(txtSCinvID.Text);
                updateStock.Add(txtSCactualTotal.Text);
                datac.updateRecCmd("InventoryStock", fieldTotalStock[1].ToString(), txtSCinvID.Text, fieldTotalStock, updateStock);
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Stock Taking using btnSCsave_UpdateInventoryStock() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSCsave_UpdateInventoryStock()
        private void btnSCsave_InsertStockOut(int difference)
        {
            try
            {
                #region Variables
                ArrayList addToOUT = new ArrayList();
                #endregion
                
                addToOUT.Add(0);
                addToOUT.Add(txtSCinvID.Text);
                addToOUT.Add(difference);
                addToOUT.Add(DateTime.Now);//not 100% sure if this is how you do is programmatically-get today's date and time
                addToOUT.Add("false");//stating that somewhere stock was taken with no record of it
                datac.insertCmd("SubStockOUT", fieldStockOut, addToOUT);
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Stock Taking using btnSCsave_InsertStockOut(int difference) method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSCsave_InsertStockOut()
        private void btnSCsave_InsertMoreStockIn()
        {
            try
            {
                #region MyRegion
                ArrayList updateStockIn = new ArrayList();
                DataTable dtTempStockIN = new DataTable();
                #endregion
                
                dtTempStockIN = datac.getFIFODatedPrice(txtSCinvID.Text);//get the record again after the update

                updateStockIn.Add(dtTempStockIN.Rows[0][0].ToString());
                updateStockIn.Add(dtTempStockIN.Rows[0][1].ToString());
                updateStockIn.Add(dtTempStockIN.Rows[0][2].ToString());
                updateStockIn.Add(dtTempStockIN.Rows[0][3].ToString());
                updateStockIn.Add(dtTempStockIN.Rows[0][5].ToString());
                updateStockIn.Add(int.Parse(dtTempStockIN.Rows[0][6].ToString()) + 1);
                datac.updateRecCmd("SubStockIN", fieldStockIN[0], dtTempStockIN.Rows[0][0].ToString(), fieldStockIN, updateStockIn);
                updateStockIn = new ArrayList();//basically clearing this arrayList
                dtTempStockIN = datac.getFIFODatedPrice(txtSCinvID.Text);//get the record again after the update
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Stock Taking using btnSCsave_InsertMoreStockIn() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end ofprivate void btnSCsave_InsertMoreStockIn() 
        private void btnSCsave_RefreshGrids()
        {
            try
            {
                dtgCheckStock.DataSource = datac.getTable("Inventory");
                dtgInventoryValue.DataSource = datac.getInventoryValue();
                dtgSupOrderList.DataSource = datac.getLowStock();
                displayInventoryValue();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Stock Taking using btnSCsave_RefreshGrids()) method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSCsave_RefreshGrids()
        private void btnSCsave_getSysTotal()
        {
            try
            {
                #region Variables
                ArrayList getIDValue = new ArrayList();
                DataTable dtTotalStock = new DataTable();
                string[] fieldTotal = { "InventoryID" };
                #endregion
                
                getIDValue.Add("=" + txtSCinvID.Text);

                dtTotalStock = datac.getMathRecord("InventoryStock", fieldTotal, getIDValue);
                if (dtTotalStock.Rows[0] != null)
                {
                    txtSCsysTotal.Text = dtTotalStock.Rows[0][2].ToString();
                }//end of if (dtStockIn.Rows[0] != null)
                else
                {
                    MessageBox.Show("No records found in datatable using btnSCsave_Click() method: ");
                } //end of else, if (dtStockIn.Rows[0] != null)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Stock Taking using btnSCsave_getSysTotal() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnSCsave_getSysTotal()
        #endregion

        #region StockCheck btnClearSCinv methods
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
                btnClearSCinv_RefreshGrids();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while clearing textboxes using btnClearSCinv_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnClearSCinv_Click(object sender, EventArgs e)
        private void btnClearSCinv_RefreshGrids()
        {
            try
            {
                dtgCheckStock.DataSource = datac.getInventoryValue();
                dtgInventoryValue.DataSource = datac.getInventoryValue();
                displayInventoryValue();
                dtgSupOrderList.DataSource = datac.getLowStock();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while clearing textboxes using btnClearSCinv_RefreshGrids() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnClearSCinv_RefreshGrids()
        #endregion
        #endregion

        #region tabOrderStock
        private void btnOrderSupGO_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                 DataTable ordersTable = new DataTable();
                Boolean exists = false;
                DataTable orderNum = new DataTable();
                string finalOrderNumber = "";
                string orderNumTemp = btnOrderSupGO_OrderNumBuildExist();
                #endregion
               
                ordersTable = datac.getTable("Orders");
                exists = testDTIfExist(ordersTable, 2, cbxOrderSup.SelectedValue.ToString());

                if (exists == true)
                {
                    finalOrderNumber = btnOrderSupGO_OrderNumBuildExist();
                }//end of if (flag==true)
                //do something if there is NO history
                else
                {
                    finalOrderNumber = btnOrderSupGO_OrderNumBuildNew();
                }//end of else of if (flag==true)
                txtOrderNumber.Text = finalOrderNumber;
                btnOrderSavePDF.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error generating order number using  btnOrderSupGO_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnOrderSupGO_Click(object sender, EventArgs e)
        private string btnOrderSupGO_OrderNumBuildExist()
        {
            #region Variables
            ArrayList supplierIDvalue = new ArrayList();
            DataTable orderSupDetails = new DataTable();
            DataTable orderNum = new DataTable();
            string year = "";
            string[] supplierIDfield = { "SupplierID" };
            string finalOrderNumber = "";
            #endregion
            
            supplierIDvalue.Add(cbxOrderSup.SelectedValue.ToString());
            year = DateTime.Now.ToString("yy");//get the date for the year block in the orderNumber-string
            orderSupDetails = datac.getRecord("Supplier", supplierIDfield, supplierIDvalue);

            orderNum = datac.getLastOrderNumber(cbxOrderSup.SelectedValue.ToString()); //get the last ordernumber for specific supplier
            finalOrderNumber = "IT" + "-" + year + "-" + orderSupDetails.Rows[0][10].ToString() + "-" + (int.Parse(orderNum.Rows[0][0].ToString()) + 1);
            return finalOrderNumber;
        }//end of private void btnOrderSupGO_OrderNumBuildExist()
        private string btnOrderSupGO_OrderNumBuildNew()
        {
            #region Variables
            ArrayList supplierIDvalue = new ArrayList();
            DataTable orderSupDetails = new DataTable();
            string year = "";
            string newOrdernumber = "0001";
            string[] supplierIDfield = { "SupplierID" };
            string finalOrderNumber = "";
            #endregion
            
            supplierIDvalue.Add(cbxOrderSup.SelectedValue.ToString());
            year = DateTime.Now.ToString("yy");//get the date for the year block in the orderNumber-string
            orderSupDetails = datac.getRecord("Supplier", supplierIDfield, supplierIDvalue);

            finalOrderNumber = "IT" + "-" + year + "-" + orderSupDetails.Rows[0][10].ToString() + "-" + newOrdernumber;
            MessageBox.Show(@"No order history found for the SUPPLIER selected. 
                Create order 0001?", "No History Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return finalOrderNumber;
        }//end of private void btnOrderSupGO_OrderNumBuildNew()
        private void btnOrderFilter_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variables
                DataTable ordersTable = new DataTable();
                Boolean exists = false;
                #endregion

                //give the dtg new datasource from dropdownbox with supplier as filter
                //test if there are any previous orders from this supplier (while this supplier exists)
                //first get the table of orders
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
                MessageBox.Show("Error loading filter data into datagridview using btnOrderFilter_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error loading data into datagridview using dtgSupOrderHistory_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void dtgSupOrderHistory_Click(object sender, EventArgs e)
        #region DragDrop methods
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
        #endregion
        private void btnAddAll_Click(object sender, EventArgs e)
        {
            try
            {//this adds the entire dtg1 to dtg2 as is and recalculates the dtg
                #region Variables
                DataGridViewRow row = new DataGridViewRow();
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
                        btnAddAll_GetPrice(iCnt, row);
                    }//end of for (int iCnt = 0; iCnt < dtgSupOrderList.Rows.Count; iCnt++) 
                    //This part below is to calculate the running total for this order in the current dtg
                    calculateOrderTotal();
                    btnOrderSavePDF.Enabled = true;
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Select All data from datagridview into order grid using btnAddAll_Click method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnAddAll_Click(object sender, EventArgs e)
        private void btnAddAll_GetPrice(int iCnt, DataGridViewRow row)
        {
            try
            {
                #region Variables
                DataTable maxPrice = new DataTable();//use datatable to save the maxDatedPrice query results in, to fetch latest price on item dragged to current dtg.
                Boolean exists = new Boolean();
                #endregion
                
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Select All data from datagridview into order grid using btnAddAll_GetPrice(int iCnt, DataGridViewRow row) method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void btnAddAll_GetPrice()
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
                MessageBox.Show("Error removing All data from datagridview using btnRemoveAll_Click(object sender, EventArgs e) method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error removing rows from datagridview using dtgOrderInvList_RowsRemoved() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error loading data from datagridview using dtgOrderInvList_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch catch (Exception ex)
        }//end of private void dtgOrderInvList_Click(object sender, EventArgs e)
      
        private void calculateOrderTotal()
        {
            try
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating order total from datagridview data using calculateOrderTotal() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch catch (Exception ex)
        }//end of private void calculateOrderTotal()
        private void btnShowAllItems_Click(object sender, EventArgs e)
        {
            try
            {
                dtgSupOrderList.DataSource = datac.getAllStockWithPrice();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading all items to datagridview using btnShowAllItems_Click() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch catch (Exception ex)
        }//end of private void btnShowAllItems_Click(object sender, EventArgs e)  
        #endregion

        #region PDF Controls
        private void btnPDForder_Click(object sender, EventArgs e)
        {
              try
              {
                  #region Variables
                  string fileName = txtOrderNumber.Text;
                  string targetpath = "";
                  DataTable dtInvValues = new DataTable();
                  DataTable dtSupValues = new DataTable();
                  dtInvValues = pdfSave_dtInvValues();
                  dtSupValues = pdfSave_dtSupplier();
                  #endregion
                  
                  DialogResult result = MessageBox.Show("Do you wish to save this order?", "Save Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                  if (result == DialogResult.Yes)
                  {
                      pdfSave_createAndSave();
                  }//end of  if (result == DialogResult.Yes)
                  else if (result == DialogResult.No)
                  {
                      if (dtInvValues != null)
                      {
                          targetpath = savePDFfileDialog(fileName);
                          pdfc.CreateOrderPDF(dtInvValues, dtSupValues, fileName, targetpath);
                      }//end of if (dtInvValues != null)
                      else
                      {
                          MessageBox.Show("No values were entered. Please enter values to insert into the order form.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      }//end of else, if (dtInvValues != null)
                  }//end of else else if (result == DialogResult.No)
              }//end of try
              catch (Exception ex)
              {
                  MessageBox.Show(ex.ToString());
              }//end of catch catch (Exception ex) 
        }//end of private void btnPDForder_Click(object sender, EventArgs e)

        private void pdfSave_addNewInventory(DataRow dtRow)//will receive the dtrow as parameter, but not return
        {
            try
            {//this only does new inventory inserts which is found in the dtg, then transferred to a dataTable (whish in affect is drawn from the dataRow
                #region Variables
                ArrayList newInvItem = new ArrayList();
                #endregion

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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch catch (Exception ex)
        }//private void pdfSave_addNewInventory(DataRow dtRow)

        private DataTable pdfSave_getInventoryID(DataRow dtRow)//will receive the dtrow as parameter, but not return dtrow
        {
            #region Variables
            ArrayList searchNewID = new ArrayList();
            DataTable getInvID = new DataTable();
            string[] searchNewInv = { "InvCode", "InvItem", "InvDescription", "InvSupplierDescription" };
            #endregion

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
            return getInvID;
        }//end of private DataTable pdfSave_getInventoryID(DataRow dtRow)

        private void pdfSave_addNewInventoryStock(DataRow dtRow)//will need to get inventoryID first,does insert for new inventory only, stock=0
        {
            try
            {
                #region Variables
                DataTable getInvID = new DataTable();
                ArrayList newStockItem = new ArrayList();
                #endregion
 
                getInvID = pdfSave_getInventoryID(dtRow);
                newStockItem.Add(0);
                newStockItem.Add(getInvID.Rows[0][0].ToString());
                newStockItem.Add(0);

                datac.insertCmd("InventoryStock", fieldTotalStock, newStockItem);//insert newly created item in the stock with a 0 stock count
                newStockItem = new ArrayList();//clear arraylist for next item
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch catch (Exception ex)
        }//end of private void pdfSave_addNewInventoryStock(DataRow dtRow

        private DataTable pdfSave_dtInvValues()
        {
            #region Variables
            DataTable dtInvValues = new DataTable();
            #endregion
            
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
            return dtInvValues;
        }//end of private DataTable pdfSave_dtInvValues()

        private void pdfSave_InsertIntoOrder()
        {
            try
            {
                #region Variables
                ArrayList ordersValues = new ArrayList();
                double orderTotal = 0.00;
                double vat = datac.getVATrate();
                DataTable dtSupplierList = new DataTable();
                string[] arrayOrderNo = pdfSave_FileName();
                int orderNoDigit = int.Parse(arrayOrderNo[3].ToString());//here is the order number digits
                #endregion
               
                dtSupplierList = pdfSave_dtSupplier();
                        
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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch catch (Exception ex)
        }//end of private void pdfSave_InsertIntoOrder()

        private DataTable pdfSave_dtSupplier()
        {
            #region Variables
            DataTable dtSupplierList = new DataTable();
            ArrayList getSupPrefix = new ArrayList();
            string[] arrayOrderNo = pdfSave_FileName();
            string supPrefix = arrayOrderNo[2].ToString();//here is the supplier prefix
            string[] searchField = { "SupPrefix" };
            #endregion

            getSupPrefix.Add(" LIKE '"+supPrefix+"'");
            dtSupplierList = datac.getMathRecord("Supplier", searchField, getSupPrefix);//here we have the supplier filtered by prefix
            return dtSupplierList;
        }//end of private DataTable pdfSave_dtSupplier()

        private DataTable pdfSave_getOrderID()
        {
            #region Variables
            ArrayList orderRefValues = new ArrayList();
            string[] arrayOrderNo = pdfSave_FileName();
            int orderNoDigit = int.Parse(arrayOrderNo[3].ToString());//here is the order number digits
            DataTable dtSupplierList = pdfSave_dtSupplier();
            DataTable getOrderRef = new DataTable();
            string[] searchOrderRef = { "OrderNumber", "SupplierID" };
            #endregion

            orderRefValues.Add(" =" + orderNoDigit);
            orderRefValues.Add(" =" + dtSupplierList.Rows[0][0].ToString());
            getOrderRef = datac.getMathRecord("Orders", searchOrderRef, orderRefValues);//get the ID reference for the order just created
            return getOrderRef;
        }//end of private DataTable pdfSave_getOrderID()

        private void pdfSave_InsertIntoSubOrder_IDexist(DataRow dtRow)
        {
            try
            {
                #region Variables
                ArrayList invOrdered = new ArrayList();
                DataTable getOrderRef = new DataTable();
                #endregion
                getOrderRef = pdfSave_getOrderID();

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
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch catch (Exception ex)
        }//end of pdfSave_InsertIntoSubOrder(DataRow dtRow)

        private void pdfSave_InsertIntoSubOrder_IDnew(DataRow dtRow)
        {
            try
            {
                #region Variables
                ArrayList invOrdered = new ArrayList();
                DataTable getOrderRef = new DataTable();
                DataTable getInvID = new DataTable();
                #endregion
                getOrderRef = pdfSave_getOrderID();

                getInvID = pdfSave_getInventoryID(dtRow);

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

                datac.insertCmd("SubOrders", fieldSubOrders, invOrdered);//insert this item in the created order
                invOrdered = new ArrayList();//clear the arraylist for the next item
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch catch (Exception ex)
        }//end of pdfSave_InsertIntoSubOrder(DataRow dtRow)

        private string[] pdfSave_FileName()
        {
            #region Variables
            string fileName = txtOrderNumber.Text;
            string[] arrayOrderNo = fileName.Split('-');
            #endregion
            
            return arrayOrderNo;
        }//end of private string[] pdfSave_FileName()

        private void pdfSave_createAndSave()
        {
            try
            {
                #region Variables
                string fileName = txtOrderNumber.Text;
                string targetpath = "";
                DataTable dtInvValues = new DataTable();
                DataTable dtSupValues= new DataTable();
                #endregion
                
                dtInvValues = pdfSave_dtInvValues();
                dtSupValues=pdfSave_dtSupplier();
                if (dtInvValues!=null)
                {
                    pdfSave_InsertIntoOrder();
                    foreach (DataRow dtRow in dtInvValues.Rows)
                    {
                        if (dtRow["invID"].ToString()!="")
                        {
                            pdfSave_InsertIntoSubOrder_IDexist(dtRow);
                        }//end of if (dtRow["invID"].ToString()!="")
                        else
                        {//first check if the item might have ID but was dragged or copied without ID
                            pdfSave_LookupInventoryExist(dtRow);
                            //pdfSave_addNewInventory(dtRow);
                            //pdfSave_addNewInventoryStock(dtRow);
                            //pdfSave_InsertIntoSubOrder_IDnew(dtRow);
                        }//end of else, if (dtRow["invID"].ToString()!="")
                    } //end of foreach (DataRow dtRow in dtInvValues.Rows)
                    targetpath = savePDFfileDialog(fileName);
                    pdfc.CreateOrderPDF(dtInvValues, dtSupValues, fileName, targetpath);
                    pdfSave_clear();
                }//end of if (dtInvValues!=null)
                else
                {
                    MessageBox.Show("No values were entered. Please enter values to insert into the order form.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//end of else, if (dtInvValues!=null)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch catch (Exception ex)
        }//end of private void pdfSave_createAndSave()

        private void pdfSave_LookupInventoryExist(DataRow dtRow)
        {
            try
            {
                #region Variables
                DataTable invValues = new DataTable();
                Boolean exists = new Boolean();
                ArrayList getInvItemID = new ArrayList();
                string[] fieldInvSearch = {"invItem"};
                #endregion

                invValues = datac.getTable("Inventory");
                //test if there is any references in the linked tables
                exists = testDTIfExistAddOne(invValues, 2, dtRow["invItem"].ToString());
                //here the flag will be tested, doing the actual determining of what should be done when it exists
                if (exists == true)
                {//if the flag is true, meaning the record already exists, will then get references
                    pdfSave_InsertIntoSubOrder_IDnew(dtRow);
                }//end of if (exists == true)
                else
                {
                    pdfSave_addNewInventory(dtRow);
                    pdfSave_addNewInventoryStock(dtRow);
                    pdfSave_InsertIntoSubOrder_IDnew(dtRow);
                }//end of else if (exists == true)
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch (Exception ex)
        }//end of private void pdfSave_LookupInventoryExist(DataRow dtRow)

        private void pdfSave_clear()
        {
            try
            {
                txtOrderNumber.Clear();
                dtgSupOrderHistory.DataSource = datac.getAllOrderHistory();
                dtgOrderInvList.Rows.Clear();
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }//end of catch catch (Exception ex)
        }//end of private void pdfSave_clear()

        private string savePDFfileDialog(string fileName)
        {
            #region Variables and objects
            string filepath = "";
            // //this dynamically creates and displays a saveFileDialog and returns the path that the file is saved to for future use if needed
            SaveFileDialog saveFileDialogObj = new SaveFileDialog();
            #endregion
            
            saveFileDialogObj.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//@"C:\";// 
            saveFileDialogObj.Title = "Save File";              //these are the properties of the saveFileDialog set programmatically on run-time
            //  saveFileDialogObj.CheckFileExists = true;
            //   saveFileDialogObj.CheckPathExists = true;
            saveFileDialogObj.DefaultExt = ".pdf";
            saveFileDialogObj.Filter = "Adobe PDF Files (*.pdf)|*.pdf|All files (*.*)|*.*";//only all files or .pdf is used as filters
            saveFileDialogObj.FilterIndex = 1;
            saveFileDialogObj.RestoreDirectory = true;
            saveFileDialogObj.FileName = fileName;

            if (saveFileDialogObj.ShowDialog() == DialogResult.OK)// only show the dialog after properties are set otherwise default settings are used- this shows the actual dialog
            {
                filepath = saveFileDialogObj.FileName;//whatever is selected in the file name box, with its path, will be saved in this string
            }//end of if (saveFileDialogObj.ShowDialog() == DialogResult.OK) 
            else
            {//this happens if anything except OK is clicked
                MessageBox.Show("Please save this file in order to open it successfully.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//end of else,  if (saveFileDialogObj.ShowDialog() == DialogResult.OK)
            return filepath;//returns the path and filename in a string as selected in the saveFileDialog
        }//end of private string savePDFfileDialog(string fileName)
        #endregion

        private void displayInventoryValue()
        {
            try
            {
                #region Variables
                double totalStockValue = 0.00;
                #endregion
                
                for (int i = 0; i < dtgInventoryValue.Rows.Count - 1; i++)
                {
                    totalStockValue += (int.Parse(dtgInventoryValue.Rows[i].Cells[4].Value.ToString()) * double.Parse(dtgInventoryValue.Rows[i].Cells[5].Value.ToString()));
                }//end of for (int i = 0; i < dtgInventoryValue.Rows.Count - 1; i++)
                txtStockValue.Text = double.Parse(totalStockValue.ToString()).ToString("C");
            }//end of try
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Inventory Value using displayInventoryValue() method: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end of catch (Exception ex)
        }//end of private void displayInventoryValue()

        #region Test Existance in DataTable
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
        #endregion
            
        private void frmInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmSwitchboard obj = new frmSwitchboard();
            this.Hide();
            obj.ShowDialog();
            Environment.Exit(0);
        }//end of private void frmInventory_FormClosing(object sender, FormClosingEventArgs e)

        private void cmbISISupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            this.tabStockIN.Focus();
        }//end of private void cmbISISupplier_SelectedValueChanged(object sender, EventArgs e)

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.tabInventoryValue.Focus();
        }//end of private void comboBox1_SelectedValueChanged(object sender, EventArgs e)

        private void cbxOrderSup_SelectedValueChanged(object sender, EventArgs e)
        {
            this.tabOrderStock.Focus();
        }//end of private void cbxOrderSup_SelectedValueChanged(object sender, EventArgs e)
       //end of private void cmbISISupplier_SelectedValueChanged(object sender, EventArgs e)
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
        //private void btnISInewItem_Click(object sender, EventArgs e)//considering deleting this method
        //{
        //    try
        //    {
        //        #region Variables
        //        //datatables
        //        DataTable dtInvoiceIN = new DataTable();
        //        DataTable dtAllInvoices = new DataTable();
        //        DataTable dtGetNewItemID = new DataTable();
        //        //arraylists
        //        ArrayList values = new ArrayList(); //make arrayList to store all values of current record
        //        ArrayList createTotalStock = new ArrayList();
        //        ArrayList createNewItemStockIN = new ArrayList();
        //        ArrayList invoiceValues = new ArrayList();
        //        ArrayList totalStockChanged = new ArrayList();
        //        ArrayList stockINValues = new ArrayList();
        //        ArrayList getNewItemDetails = new ArrayList();
        //        ArrayList getIDValue = new ArrayList();
        //        //arrays
        //        string[] fieldFilter = { "ISIInvoiceNo", "SupplierID" };//cant work with this guy "ISIInvoiceTotalIncl"
        //        string[] fieldNewItemID = { "InvItem" };
        //        //variables
        //        Boolean exists = new Boolean();
        //        Boolean flag = new Boolean();
        //        DataTable dtLastStockInvoice = datac.getLastStockInvoice();
        //        string arrLastStockInvoice = dtLastStockInvoice.Rows[0][0].ToString();
        //        string[] arrayInvoiceNo = arrLastStockInvoice.Split(' ');
        //        int stockInvNo = int.Parse(arrayInvoiceNo[1].ToString());
        //        #endregion

        //        if (txtISIinvItem.Text != "")
        //        {
        //            values.Add("0");          //store textBox / comboBox value in the ArrayList
        //            values.Add(txtISIinvCode.Text);
        //            values.Add(txtISIinvItem.Text.ToUpper());
        //            values.Add(txtISIinvDescription.Text.ToUpper());
        //            values.Add(txtISISupDescription.Text.ToUpper());
        //            values.Add(txtISIinvCategory.Text.ToUpper());
        //            if (txtISIinvReLevel.Text != "")     //See if user gave value for reorderLevel, and use value
        //            {
        //                values.Add(txtISIinvReLevel.Text);
        //            }//end of if (txtInvReLeveli.Text!="")
        //            else
        //            {
        //                values.Add("0");            //if user gave no value, make default 0: for prototyping purposes
        //            }//end of else, if (txtInvReLeveli.Text!="")
        //            if (cmbISIinvMarkup.Text != "")
        //            {
        //                values.Add(cmbISIinvMarkup.Text);       //if user gave value, use value
        //            }//end of if (cmbISIinvMarkup.Text != "")
        //            else
        //            {
        //                values.Add("25");                   //if user gave no value for markup%, then make 25% default
        //            }//end of else, if (cmbISIinvMarkup.Text != "")
        //            values.Add("false");

        //            flag = false;

        //            datac.insertCmd("Inventory", fieldInvAll, values);       ////Send values in fieldInv string format through insertCmd query to database table
        //            dtgStockIn.DataSource = datac.getTable("Inventory");

        //            getNewItemDetails.Add(txtISIinvItem.Text.ToUpper());
        //            dtGetNewItemID = datac.getRecord("Inventory", fieldNewItemID, getNewItemDetails);
        //            //make reference for a new item to call other tables from
        //            if (txtISIstockReceived.Text!="")
        //            {//create instance for this new item to have a reference to StockIN and a StockTotal
        //                if (txtInvoiceNo.Text != "")//this if makes it invoice based, invoice reference made-else is for recirculation
        //                {
        //                    if (dtpInvoiceDate.Text != "")
        //                    {
        //                        dtAllInvoices = datac.getTable("InvoiceStockIN");
        //                        if (txtISIstockPrice.Text != "")
        //                        {
        //                            for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
        //                            {
        //                                if (exists == false) 
        //                                {
        //                                    if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())//&& dtAllInvoices.Rows[i][4].ToString() == txtISITotal.Text
        //                                    {
        //                                        exists = true;
        //                                    }//end of  if (dtAllInvoices.Rows[i][1].ToString() == txtInvoiceNo.Text && dtAllInvoices.Rows[i][3].ToString() == cmbISISupplier.SelectedValue.ToString())
        //                                    else
        //                                    {
        //                                        exists = false;
        //                                    }//end of else
        //                                }//end of if (exists == false)
        //                                else
        //                                {
        //                                    //this will be my flag to signal if it exists, since the first part of exist will only test each row and then move on, thus not stopping if it exists
        //                                    flag = true;
        //                                }//end of else of if (exists == false)
        //                            }//end of for (int i = 0; i < dtAllInvoices.Rows.Count; i++)
        //                            //   MessageBox.Show("Exists value: " + exists, "Testing complete");
        //                            if (flag == true)
        //                            {
        //                                getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
        //                                // getIDValue.Add("='" + dtpInvoiceDate.Text+"00:00:00.000'");
        //                                getIDValue.Add("=" + cmbISISupplier.SelectedValue);
        //                                // getIDValue.Add("=" + txtISITotal.Text + "00");

        //                                dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID
                                   
        //                                // createNewItemStockIN.Add();
        //                                stockINValues.Add(0);
        //                                stockINValues.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                                stockINValues.Add(txtISIstockReceived.Text);
        //                                stockINValues.Add(txtISIstockPrice.Text);
        //                                stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
        //                                stockINValues.Add(txtISIstockReceived.Text);
        //                                datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted

        //                                totalStockChanged.Add(0);
        //                                totalStockChanged.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                                totalStockChanged.Add(txtISIstockReceived.Text);
        //                                datac.insertCmd("InventoryStock", fieldTotalStock, totalStockChanged);//create a reference to totalStock with first stock

        //                                dtgStockIn.DataSource = datac.getTable("Inventory");
        //                            }//end of if (flag = true)
        //                            else
        //                            {//make new invoice thingy
        //                                invoiceValues.Add(0);
        //                                invoiceValues.Add(txtInvoiceNo.Text);
        //                                invoiceValues.Add(dtpInvoiceDate.Text);
        //                                invoiceValues.Add(cmbISISupplier.SelectedValue);
        //                                invoiceValues.Add(txtISITotal.Text);
        //                                datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, invoiceValues);

        //                                getIDValue.Add(" LIKE '" + txtInvoiceNo.Text + "'");
        //                                getIDValue.Add("=" + cmbISISupplier.SelectedValue);
        //                                dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

        //                                stockINValues.Add(0);
        //                                stockINValues.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                                stockINValues.Add(txtISIstockReceived.Text);
        //                                stockINValues.Add(txtISIstockPrice.Text);
        //                                stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
        //                                stockINValues.Add(txtISIstockReceived.Text);
        //                                datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted
        //                                dtgStockIn.DataSource = datac.getTable("Inventory");

        //                                totalStockChanged.Add(0);
        //                                totalStockChanged.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                                totalStockChanged.Add(txtISIstockReceived.Text);
        //                                datac.insertCmd("InventoryStock", fieldTotalStock, totalStockChanged);//create a reference to totalStock with first stock
        //                            }//end of else, if (flag == true)
        //                        }//end of if (txtISIstockPrice.Text!="")
        //                        else
        //                        {
        //                            MessageBox.Show("Please enter a Price/Unit value in the <Price/ Unit excl VAT> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        }//end of else, if (txtISIstockPrice.Text!="")
        //                    }//end of if (dtpInvoiceDate.Text!="")
        //                    else
        //                    {
        //                        MessageBox.Show("Please enter a date value in the Invoice Date field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    }//end of else if (dtpInvoiceDate.Text!="")
        //                }//end of if (txtISIinvoiceNr.Text != "")
        //                else
        //                {//since no invoiceNo is given, make it a STOCK invoice
        //                    invoiceValues.Add(0);
        //                    invoiceValues.Add(arrayInvoiceNo[0].ToString() + " " + (stockInvNo + 1));
        //                    invoiceValues.Add(dtpInvoiceDate.Text);
        //                    invoiceValues.Add("1");
        //                    invoiceValues.Add("0.00");
        //                    datac.insertCmd("InvoiceStockIN", fieldInvoiceStockIN, invoiceValues);

        //                    getIDValue.Add(" LIKE '" + (arrayInvoiceNo[0].ToString() +" "+ (stockInvNo+1))+"'");
        //                    getIDValue.Add("= 1");
        //                    dtInvoiceIN = datac.getMathRecord("InvoiceStockIN", fieldFilter, getIDValue);  //save invoice details in this dataTable, to get the precise ISIID

        //                    stockINValues.Add(0);
        //                    stockINValues.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                    if (txtISIstockReceived.Text!="")
        //                    {
        //                        stockINValues.Add(txtISIstockReceived.Text);
        //                    }//end of if (txtISIstockReceived.Text!="")
        //                    else
        //                    {
        //                        MessageBox.Show("Please enter a Quantity Received value in the <Quantity Received> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        stockINValues.Add(txtISIstockReceived.Text);
        //                    }//end of else, if (txtISIstockReceived.Text!="")
        //                    if (txtISIstockPrice.Text!="")
        //                    {
        //                        stockINValues.Add(txtISIstockPrice.Text);
        //                    }//end of if (xtISIstockPrice.Text!="")
        //                    else
        //                    {
        //                        MessageBox.Show("Please enter a Price/Unit value in the <Price/ Unit excl VAT> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        stockINValues.Add(txtISIstockPrice.Text);
        //                    }//end of else, if (xtISIstockPrice.Text!="")
                            
        //                    stockINValues.Add(dtInvoiceIN.Rows[0][0].ToString());//use dtInvoiceIN for the reference to the invoice received
        //                    stockINValues.Add(txtISIstockReceived.Text);
        //                    datac.insertCmd("SubStockIN", fieldStockIN, stockINValues);// a new transaction with a reference to an invoice is created and inserted
        //                    dtgStockIn.DataSource = datac.getTable("Inventory");

        //                    totalStockChanged.Add(0);
        //                    totalStockChanged.Add(dtGetNewItemID.Rows[0][0].ToString());
        //                    totalStockChanged.Add(txtISIstockReceived.Text);
        //                    datac.insertCmd("InventoryStock", fieldTotalStock, totalStockChanged);//create a reference to totalStock with first stock
        //                }//end of else, if (txtISIinvoiceNr.Text != "")
        //            }//end of if (txtISIstockReceived.Text!="")
        //            else
        //            {
        //                MessageBox.Show("Please enter a Quantity Received value in the <Quantity Received> field", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }//end of else, if (txtISIstockReceived.Text!="")
        //        }//end of if(txtISIinvItem.Text!="")
        //        else
        //        {
        //            MessageBox.Show("No record was added: Item Field is empty. Please provide an item's name to add.", "New record unsuccessful!");
        //        }//end of else, if (txtISIinvItem.Text!="")
        //        dtgStockIn.DataSource = datac.getTable("Inventory");
        //        dtgCheckStock.DataSource = datac.getTable("Inventory");
        //        dtgSupOrderList.DataSource = datac.getLowStock();
        //        dtgInventoryValue.DataSource = datac.getInventoryValue();
        //        btnISIpreviewCurrent.Enabled = false;
        //        //to calculate total value of stock based on quantity and price on inventoryValue tab****
        //        displayInventoryValue();
        //    }//end of try
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error while creating new record : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }//end of catch (Exception ex)
        //}//end of private void btnISInewItem_Click(object sender, EventArgs e)

#endregion

#region Dummy save pdf new
        //private void pdfsavedummy()
        //{
        //    try
        //    {
        //        string fileName = txtOrderNumber.Text;
        //        string targetpath = "";
        //        DataTable dtInvValues = new DataTable();
        //        DataTable dtSupValues= new DataTable();
        //        dtInvValues = pdfSave_dtInvValues();
        //        dtSupValues=pdfSave_dtSupplier();
        //        DialogResult result = MessageBox.Show("Do you wish to save this order?", "Save Order", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
        //         if (result == DialogResult.Yes)
        //         {
        //             pdfSave_createAndSave();
        //         }//end of  if (result == DialogResult.Yes)
        //         else if (result == DialogResult.No)
        //         {
        //             if (dtInvValues != null)
        //             {
        //                 targetpath = savePDFfileDialog(fileName);
        //                 pdfc.CreateOrderPDF(dtInvValues, dtSupValues, fileName, targetpath);
        //             }
        //             else
        //             {
        //                 MessageBox.Show("No values were entered. Please enter values to insert into the order form.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //             }
        //         }//end of else else if (result == DialogResult.No)
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}
#endregion

#region Old PDF save button
//old pdfOrderButtonSave
// try
//            {//does all the gathering for the creating of the pdf, and then saves the records in the relevant tables
//                #region PDF Variables
//                //datatable
//                DataTable dtInvValues = new DataTable();        //this variable is the datatable that will contain all the details of the inventory being ordered
//                DataTable dtSupValues = new DataTable();
//                //arrayList
//                ArrayList val = new ArrayList();
//                //array
//                string[] field = { "SupplierID" };
//                //variable
//                string fileName = txtOrderNumber.Text;
//                string getID = cbxOrderSup.SelectedValue.ToString();
//                #endregion
//                #region Variables for Order Recording
//                //datatable
//                DataTable dtSupplierList = new DataTable();
//                DataTable getOrderRef = new DataTable();
//                DataTable getInvID = new DataTable();
//                //arrayList
//                ArrayList getSupPrefix = new ArrayList();
//                ArrayList ordersValues = new ArrayList();
//                ArrayList invOrdered = new ArrayList();
//                ArrayList orderRefValues = new ArrayList();
//                ArrayList newInvItem = new ArrayList();
//                ArrayList newStockItem = new ArrayList();
//                ArrayList searchNewID = new ArrayList();
//                ArrayList recordItemOrder = new ArrayList();
//                //array
//                string[] arrayOrderNo = fileName.Split('-');
//                string[] searchField = { "SupPrefix" };
//                string[] searchOrderRef = { "OrderNumber", "SupplierID" };
//                string[] searchNewInv = { "InvCode", "InvItem", "InvDescription", "InvSupplierDescription" };
//                //variable
//                string supPrefix = arrayOrderNo[2].ToString();//here is the supplier prefix
//                int orderNoDigit = int.Parse(arrayOrderNo[3].ToString());//here is the order number digits
//                double orderTotal = 0.00;
//                double vat = datac.getVATrate();
//                #endregion

//                DialogResult result = MessageBox.Show("Do you wish to save this order?", "Save Order", MessageBoxButtons.YesNo,
//              MessageBoxIcon.Question);

//                if (result == DialogResult.Yes)
//                {
////--------------------------    savePdfOrderDtgToDatatable   -send values             
//                    foreach (DataGridViewColumn header in dtgOrderInvList.Columns)
//                    {
//                        dtInvValues.Columns.Add(header.Name);
//                    }//end of foreach (DataGridViewColumn header in dtgOrderInvList.Columns)
//                    foreach (DataGridViewRow row in dtgOrderInvList.Rows)
//                    {
//                        if (row.IsNewRow)
//                            continue;
//                        DataRow dtRow = dtInvValues.NewRow();
//                        for (int i1 = 0; i1 < dtgOrderInvList.Columns.Count; i1++)
//                            dtRow[i1] = (row.Cells[i1].Value == null ? DBNull.Value : row.Cells[i1].Value);
//                        dtInvValues.Rows.Add(dtRow);
//                    }//end of foreach (DataGridViewRow row in dtgOrderInvList.Rows)
//                    //get the applicable supplier details
//                    val.Add(getID);
//                    dtSupValues = datac.getRecord("Supplier", field, val);

//                    if (dtInvValues != null)
//                    {
//                        getSupPrefix.Add(supPrefix);
//                        dtSupplierList = datac.getRecord("Supplier", searchField, getSupPrefix);//here we have the supplier filtered by prefix
//                        for (int i = 0; i < dtgOrderInvList.Rows.Count - 1; i++)
//                        {//this gets the estimate cost for this order
//                            orderTotal += (int.Parse(dtgOrderInvList.Rows[i].Cells[6].Value.ToString()) * double.Parse(dtgOrderInvList.Rows[i].Cells[7].Value.ToString()));
//                        }//end of for (int i = 0; i < dtgOrderInvList.Rows.Count - 1; i++)
//                        orderTotal = orderTotal * (1+vat);

//                        ordersValues.Add(0);
//                        ordersValues.Add(orderNoDigit);
//                        ordersValues.Add(dtSupplierList.Rows[0][0].ToString());
//                        ordersValues.Add(DateTime.Now);
//                        ordersValues.Add(orderTotal);
//                        datac.insertCmd("Orders", fieldOrders, ordersValues);       //do the first insert: into orders
////--------------------------savePdfOrderGetOrderID- send value back
//                        orderRefValues.Add(" =" + orderNoDigit);
//                        orderRefValues.Add(" =" + dtSupplierList.Rows[0][0].ToString());
//                        getOrderRef = datac.getMathRecord("Orders", searchOrderRef, orderRefValues);//get the ID reference for the order just created
////--------------------------
//                        foreach (DataRow dtRow in dtInvValues.Rows)
//                        {
//                            if (dtRow["invID"].ToString() != "")
//                            {
////--------------------------savePdfOrderInsertIntoSubOrderTable
//                                invOrdered.Add(0);
//                                invOrdered.Add(dtRow["invID"].ToString());
//                                if (dtRow["orderQuantity"].ToString() != "")
//                                {
//                                    invOrdered.Add(dtRow["orderQuantity"].ToString());
//                                }// end of if (dtRow["orderQuantity"].ToString()!="")
//                                else
//                                {
//                                    MessageBox.Show("Please enter a quantity to be ordered.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                                }//end of else, if (dtRow["orderQuantity"].ToString()!="")
//                                if (dtRow["orderPrice"].ToString() != "")
//                                {
//                                    invOrdered.Add(dtRow["orderPrice"].ToString());
//                                }// end of if (dtRow["orderPrice"].ToString()!="")
//                                else
//                                {
//                                    invOrdered.Add(0.00);
//                                }//end of else, if (dtRow["orderPrice"].ToString()!="")
//                                invOrdered.Add(getOrderRef.Rows[0][0].ToString());//get the order reference that was created.
//                                if (dtRow["invLength"].ToString() != "")
//                                {
//                                    invOrdered.Add(dtRow["invLength"].ToString());
//                                }// end of if (dtRow["invLength"].ToString()!="")
//                                else
//                                {
//                                    invOrdered.Add(0.00);
//                                }//end of else, if (dtRow["invLength"].ToString()!="")

//                                datac.insertCmd("SubOrders", fieldSubOrders, invOrdered);//insert this item in the created order
//                                invOrdered = new ArrayList();//clear the arraylist for the next item
////--------------------------
//                            }//end of if (dtRow["ID"].ToString() !="")
//                            else
//                            {
//                                //create a new reference ID in the inventory table, by first inserting new item into table
//                                //then insert into the StockTotal table with new ID
//                                //then do final insert into subOrder
//                                //since there is nothing being received at all yet, no need to insert into substock- stock and price (invoiced price) will be entered when the invoice is received
////--------------------------savePdfOrderInsertNewInventory
//                                newInvItem.Add(0);
//                                if (dtRow["invCode"].ToString() != "")
//                                {
//                                    newInvItem.Add(dtRow["invCode"].ToString());
//                                }// end of if (dtRow["invCode"].ToString()!="")
//                                else
//                                {
//                                    newInvItem.Add("");
//                                }//end of else, if (dtRow["invCode"].ToString()!="")
//                                if (dtRow["invItem"].ToString() != "")
//                                {
//                                    newInvItem.Add(dtRow["invItem"].ToString());
//                                }// end of if (dtRow["invItem"].ToString()!="")
//                                else
//                                {
//                                    MessageBox.Show("Please enter a name for the item being ordered.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                                }//end of else, if (dtRow["invItem"].ToString()!="")
//                                if (dtRow["invDescription"].ToString() != "")
//                                {
//                                    newInvItem.Add(dtRow["invDescription"].ToString());
//                                }// end of if (dtRow["invDescription"].ToString()!="")
//                                else
//                                {
//                                    newInvItem.Add("");
//                                }//end of else, if (dtRow["invDescription"].ToString()!="")
//                                if (dtRow["invSupDescription"].ToString() != "")
//                                {
//                                    newInvItem.Add(dtRow["invSupDescription"].ToString());
//                                }// end of if (dtRow["invSupDescription"].ToString()!="")
//                                else
//                                {
//                                    newInvItem.Add("");
//                                }//end of else, if (dtRow["invSupDescription"].ToString()!="")
//                                newInvItem.Add("");
//                                newInvItem.Add(1);
//                                newInvItem.Add(25);
//                                newInvItem.Add("false");

//                                datac.insertCmd("Inventory", fieldInvAll, newInvItem);//create an ID (reference) in the inventory table for this new item
//                                newInvItem = new ArrayList();//clear the arraylist for the next item
////--------------------------savePdfOrderGetNewInventory-send back a value
//                                if (dtRow["invCode"].ToString() != "")
//                                {
//                                    searchNewID.Add(dtRow["invCode"].ToString());
//                                }// end of if (dtRow["invCode"].ToString()!="")
//                                else
//                                {
//                                    searchNewID.Add("");
//                                }//end of else, if (dtRow["invCode"].ToString()!="")
//                                searchNewID.Add(dtRow["invItem"].ToString());
//                                if (dtRow["invDescription"].ToString() != "")
//                                {
//                                    searchNewID.Add(dtRow["invDescription"].ToString());
//                                }// end of if (dtRow["invDescription"].ToString()!="")
//                                else
//                                {
//                                    searchNewID.Add("");
//                                }//end of else, if (dtRow["invDescription"].ToString()!="")
//                                if (dtRow["invSupDescription"].ToString() != "")
//                                {
//                                    searchNewID.Add(dtRow["invSupDescription"].ToString());
//                                }// end of if (dtRow["invSupDescription"].ToString()!="")
//                                else
//                                {
//                                    searchNewID.Add("");
//                                }//end of else, if (dtRow["invSupDescription"].ToString()!="")

//                                getInvID = datac.getRecord("Inventory", searchNewInv, searchNewID);//get the record's ID that was just created
//                                searchNewID = new ArrayList();//clear arraylist for next item
////--------------------------savePdfOrderInsertNewInventoryStock
//                                newStockItem.Add(0);
//                                newStockItem.Add(getInvID.Rows[0][0].ToString());
//                                newStockItem.Add(0);

//                                datac.insertCmd("InventoryStock", fieldTotalStock, newStockItem);//insert newly created item in the stock with a 0 stock count
//                                newStockItem = new ArrayList();//clear arraylist for next item
////--------------------------savePdfOrderInsertNewIntoSubOrder
//                                invOrdered.Add(0);
//                                invOrdered.Add(getInvID.Rows[0][0].ToString());
//                                if (dtRow["orderQuantity"].ToString() != "")
//                                {
//                                    invOrdered.Add(dtRow["orderQuantity"].ToString());
//                                }// end of if (dtRow["orderQuantity"].ToString()!="")
//                                else
//                                {
//                                    MessageBox.Show("Please enter a quantity to be ordered.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                                }//end of else, if (dtRow["orderQuantity"].ToString()!="")
//                                if (dtRow["orderPrice"].ToString() != "")
//                                {
//                                    invOrdered.Add(dtRow["orderPrice"].ToString());
//                                }// end of if (dtRow["orderPrice"].ToString()!="")
//                                else
//                                {
//                                    invOrdered.Add(0.00);
//                                }//end of else, if (dtRow["orderPrice"].ToString()!="")
//                                invOrdered.Add(getOrderRef.Rows[0][0].ToString());//get the order reference that was created.
//                                if (dtRow["invLength"].ToString() != "")
//                                {
//                                    invOrdered.Add(dtRow["invLength"].ToString());
//                                }// end of if (dtRow["invLength"].ToString()!="")
//                                else
//                                {
//                                    invOrdered.Add(0.00);
//                                }//end of else, if (dtRow["invLength"].ToString()!="")

//                                datac.insertCmd("SubOrders", fieldSubOrders, invOrdered); //insert this new item in the order created
//                                invOrdered = new ArrayList();//clear arraylist for next item
////--------------------------
//                            }//end of else, if (dtRow["ID"].ToString() !="")
//                        }//end of foreach (DataRow dtRow in dtInvValues)
////--------------------------createPdfOrder_targetPath
//                        string targetpath = savePDFfileDialog(fileName);
//                        pdfc.CreateOrderPDF(dtInvValues, dtSupValues, fileName, targetpath);//simply send a datatable for supplier, a datatable for the items, and a string for the filename- it will do the rest
//                    }//end of if (dtInvValues!=null)
//                    else
//                    {
//                        MessageBox.Show("No values were entered. Please enter values to insert into the order form.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }//end of else, if (dtInvValues!=null)
//                }//end of  if (result == DialogResult.Yes)
//                else if (result == DialogResult.No)
//                {
////--------------------------createPdfOrderDtgToDatatable-send values
//                    foreach (DataGridViewColumn header in dtgOrderInvList.Columns)
//                    {
//                        dtInvValues.Columns.Add(header.Name);
//                    }//end of foreach (DataGridViewColumn header in dtgOrderInvList.Columns)
//                    foreach (DataGridViewRow row in dtgOrderInvList.Rows)
//                    {
//                        if (row.IsNewRow)
//                            continue;
//                        DataRow dtRow = dtInvValues.NewRow();
//                        for (int i1 = 0; i1 < dtgOrderInvList.Columns.Count; i1++)
//                            dtRow[i1] = (row.Cells[i1].Value == null ? DBNull.Value : row.Cells[i1].Value);
//                        dtInvValues.Rows.Add(dtRow);
//                    }//end of foreach (DataGridViewRow row in dtgOrderInvList.Rows)
////--------------------------createPdfOrderDtgToDatatable
//                    //get the applicable supplier details
//                    val.Add(getID);
//                    dtSupValues = datac.getRecord("Supplier", field, val);
////--------------------------createPdfOrder_targetPath
//                    if (dtInvValues != null)
//                    {
//                        string targetpath = savePDFfileDialog(fileName);
//                        pdfc.CreateOrderPDF(dtInvValues, dtSupValues, fileName, targetpath);//simply send a datatable for supplier, a datatable for the items, and a string for the filename- it will do the rest

//                    }//end of if (dtInvValues!=null)
//                    else
//                    {
//                        MessageBox.Show("No values were entered. Please enter values to insert into the order form.", "Save unsuccessful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }//end of else, if (dtInvValues!=null)
////--------------------------
//                }//end of else else if (result == DialogResult.No)
////--------------------------savePdfOrderClear
//                txtOrderNumber.Clear();
//                dtgSupOrderHistory.DataSource = datac.getAllOrderHistory();//refresh the datagridview
//                dtgOrderInvList.Rows.Clear();//clear the ordering List to enable new order to be made.
////--------------------------
//            }//end of try
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.ToString());
//            }//end of catch (Exception ex)     
//dummy
//dummy
#endregion
