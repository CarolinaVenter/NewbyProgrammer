﻿namespace ImagineTrailvan
{
    partial class frmInventory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tConInventory = new System.Windows.Forms.TabControl();
            this.tabStockOUT = new System.Windows.Forms.TabPage();
            this.btnPDFprint = new System.Windows.Forms.Button();
            this.dtpDateStockOUT = new System.Windows.Forms.DateTimePicker();
            this.gboxPrice = new System.Windows.Forms.GroupBox();
            this.lblSellPrice = new System.Windows.Forms.Label();
            this.lblIPrice = new System.Windows.Forms.Label();
            this.txtSellPrice = new System.Windows.Forms.TextBox();
            this.cmbInvMarkup = new System.Windows.Forms.ComboBox();
            this.lblIMarkup = new System.Windows.Forms.Label();
            this.txtInvPrice = new System.Windows.Forms.TextBox();
            this.gboxStock = new System.Windows.Forms.GroupBox();
            this.txtInvReLevel = new System.Windows.Forms.TextBox();
            this.txtInvTotalStock = new System.Windows.Forms.TextBox();
            this.lblInvTotalStock = new System.Windows.Forms.Label();
            this.lblIReLevel = new System.Windows.Forms.Label();
            this.lblIStock = new System.Windows.Forms.Label();
            this.txtInvStockOut = new System.Windows.Forms.TextBox();
            this.btnStockUpdate = new System.Windows.Forms.Button();
            this.grbInventory = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblIDesc = new System.Windows.Forms.Label();
            this.lblIItem = new System.Windows.Forms.Label();
            this.lblICode = new System.Windows.Forms.Label();
            this.lblIID = new System.Windows.Forms.Label();
            this.txtInvCat = new System.Windows.Forms.TextBox();
            this.txtInvDesc = new System.Windows.Forms.TextBox();
            this.txtInvItem = new System.Windows.Forms.TextBox();
            this.txtInvCode = new System.Windows.Forms.TextBox();
            this.txtInvID = new System.Windows.Forms.TextBox();
            this.dtgInventory = new System.Windows.Forms.DataGridView();
            this.tabStockIN = new System.Windows.Forms.TabPage();
            this.btnISIsave = new System.Windows.Forms.Button();
            this.btnISIrecordNewInvoice = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label40 = new System.Windows.Forms.Label();
            this.cmbISISupplier = new System.Windows.Forms.ComboBox();
            this.supplierBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.supplierDataSet = new ImagineTrailvan.SupplierDataSet();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.txtISITotal = new System.Windows.Forms.TextBox();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmbISIinvMarkup = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtISIinvReLevel = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txtISIstockTotal = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.txtISIstockReceived = new System.Windows.Forms.TextBox();
            this.txtISIstockPrice = new System.Windows.Forms.TextBox();
            this.btnISIpreviewCurrent = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnISIinvDeleteItem = new System.Windows.Forms.Button();
            this.btnISInewItem = new System.Windows.Forms.Button();
            this.btnISIclear = new System.Windows.Forms.Button();
            this.label49 = new System.Windows.Forms.Label();
            this.btnISIdelItemInvoice = new System.Windows.Forms.Button();
            this.btnISIsearchItem = new System.Windows.Forms.Button();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.txtISIinvCategory = new System.Windows.Forms.TextBox();
            this.txtISIinvDescription = new System.Windows.Forms.TextBox();
            this.txtISIinvItem = new System.Windows.Forms.TextBox();
            this.txtISIinvCode = new System.Windows.Forms.TextBox();
            this.txtISIinvID = new System.Windows.Forms.TextBox();
            this.dtgStockIn = new System.Windows.Forms.DataGridView();
            this.tabStockCheck = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSCinvRelevel = new System.Windows.Forms.TextBox();
            this.txtSCsysTotal = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtSCactualTotal = new System.Windows.Forms.TextBox();
            this.btnSCsave = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClearSCinv = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.btnSearchSCinv = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtSCinvCat = new System.Windows.Forms.TextBox();
            this.txtSCinvDesc = new System.Windows.Forms.TextBox();
            this.txtSCinvItem = new System.Windows.Forms.TextBox();
            this.txtSCinvCode = new System.Windows.Forms.TextBox();
            this.txtSCinvID = new System.Windows.Forms.TextBox();
            this.dtgCheckStock = new System.Windows.Forms.DataGridView();
            this.tabSuppliers = new System.Windows.Forms.TabPage();
            this.label39 = new System.Windows.Forms.Label();
            this.cmbSupPayTerm = new System.Windows.Forms.ComboBox();
            this.btnSupClear = new System.Windows.Forms.Button();
            this.btnSupSearch = new System.Windows.Forms.Button();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSupReTime = new System.Windows.Forms.TextBox();
            this.txtSupPrefix = new System.Windows.Forms.TextBox();
            this.btnDeleteSup = new System.Windows.Forms.Button();
            this.btnSupINSERT = new System.Windows.Forms.Button();
            this.btnSupUPDATE = new System.Windows.Forms.Button();
            this.grbSContact = new System.Windows.Forms.GroupBox();
            this.lblBusnr = new System.Windows.Forms.Label();
            this.lblSEmail = new System.Windows.Forms.Label();
            this.lblSCell = new System.Windows.Forms.Label();
            this.txtSCell = new System.Windows.Forms.TextBox();
            this.txtSBusNr = new System.Windows.Forms.TextBox();
            this.txtSREP = new System.Windows.Forms.TextBox();
            this.lblSREP = new System.Windows.Forms.Label();
            this.txtSEmail = new System.Windows.Forms.TextBox();
            this.lblSContactP = new System.Windows.Forms.Label();
            this.txtSContactP = new System.Windows.Forms.TextBox();
            this.grbSAddress = new System.Windows.Forms.GroupBox();
            this.txtSProv = new System.Windows.Forms.TextBox();
            this.txtSCity = new System.Windows.Forms.TextBox();
            this.txtSAddress = new System.Windows.Forms.TextBox();
            this.txtSName = new System.Windows.Forms.TextBox();
            this.txtSID = new System.Windows.Forms.TextBox();
            this.dtgSuppliers = new System.Windows.Forms.DataGridView();
            this.tabInventoryValue = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.lblInventoryValue = new System.Windows.Forms.Label();
            this.txtStockValue = new System.Windows.Forms.TextBox();
            this.dtgInventoryValue = new System.Windows.Forms.DataGridView();
            this.tabSupplierSummary = new System.Windows.Forms.TabPage();
            this.btnSearchClear = new System.Windows.Forms.Button();
            this.btnSearchGO = new System.Windows.Forms.Button();
            this.lblValueSummary = new System.Windows.Forms.Label();
            this.txtSupSummaryValue = new System.Windows.Forms.TextBox();
            this.dtgInventorySummary = new System.Windows.Forms.DataGridView();
            this.txtSearchSCell = new System.Windows.Forms.TextBox();
            this.txtSearchSProv = new System.Windows.Forms.TextBox();
            this.txtSearchSCity = new System.Windows.Forms.TextBox();
            this.txtSearchSAddress = new System.Windows.Forms.TextBox();
            this.txtSearchSBus = new System.Windows.Forms.TextBox();
            this.txtSearchSEmail = new System.Windows.Forms.TextBox();
            this.txtSearchSREP = new System.Windows.Forms.TextBox();
            this.txtSearchSContact = new System.Windows.Forms.TextBox();
            this.txtSearchSName = new System.Windows.Forms.TextBox();
            this.txtSearchSID = new System.Windows.Forms.TextBox();
            this.lblSearchSup = new System.Windows.Forms.Label();
            this.txtSearchSupplier = new System.Windows.Forms.TextBox();
            this.dtgSupplierSearch = new System.Windows.Forms.DataGridView();
            this.tabOrderStock = new System.Windows.Forms.TabPage();
            this.cbxOrderSup = new System.Windows.Forms.ComboBox();
            this.btnOrderSupGO = new System.Windows.Forms.Button();
            this.btnOrderFilter = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrderTotalIncl = new System.Windows.Forms.TextBox();
            this.txtOrderVAT = new System.Windows.Forms.TextBox();
            this.txtOrderTotalExcl = new System.Windows.Forms.TextBox();
            this.btnOrderNextItem = new System.Windows.Forms.Button();
            this.btnOrderPreview = new System.Windows.Forms.Button();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.dtgSupOrderHistory = new System.Windows.Forms.DataGridView();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnRemoveSingle = new System.Windows.Forms.Button();
            this.btnAddSingle = new System.Windows.Forms.Button();
            this.dtgOrderInvList = new System.Windows.Forms.DataGridView();
            this.invID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invSupDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgSupOrderList = new System.Windows.Forms.DataGridView();
            this.tabOrderInventory = new System.Windows.Forms.TabPage();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbOrderSupplier = new System.Windows.Forms.ComboBox();
            this.dtgNewInvOrderDetails = new System.Windows.Forms.DataGridView();
            this.dtgInvOrderDetails = new System.Windows.Forms.DataGridView();
            this.dtgInvOrderHistory = new System.Windows.Forms.DataGridView();
            this.supplierTableAdapter = new ImagineTrailvan.SupplierDataSetTableAdapters.SupplierTableAdapter();
            this.tConInventory.SuspendLayout();
            this.tabStockOUT.SuspendLayout();
            this.gboxPrice.SuspendLayout();
            this.gboxStock.SuspendLayout();
            this.grbInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventory)).BeginInit();
            this.tabStockIN.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.supplierBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.supplierDataSet)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgStockIn)).BeginInit();
            this.tabStockCheck.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCheckStock)).BeginInit();
            this.tabSuppliers.SuspendLayout();
            this.grbSContact.SuspendLayout();
            this.grbSAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSuppliers)).BeginInit();
            this.tabInventoryValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventoryValue)).BeginInit();
            this.tabSupplierSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventorySummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSupplierSearch)).BeginInit();
            this.tabOrderStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSupOrderHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrderInvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSupOrderList)).BeginInit();
            this.tabOrderInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgNewInvOrderDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInvOrderDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInvOrderHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // tConInventory
            // 
            this.tConInventory.Controls.Add(this.tabStockOUT);
            this.tConInventory.Controls.Add(this.tabStockIN);
            this.tConInventory.Controls.Add(this.tabStockCheck);
            this.tConInventory.Controls.Add(this.tabSuppliers);
            this.tConInventory.Controls.Add(this.tabInventoryValue);
            this.tConInventory.Controls.Add(this.tabSupplierSummary);
            this.tConInventory.Controls.Add(this.tabOrderStock);
            this.tConInventory.Controls.Add(this.tabOrderInventory);
            this.tConInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tConInventory.Location = new System.Drawing.Point(0, 0);
            this.tConInventory.Name = "tConInventory";
            this.tConInventory.SelectedIndex = 0;
            this.tConInventory.Size = new System.Drawing.Size(1044, 551);
            this.tConInventory.TabIndex = 0;
            // 
            // tabStockOUT
            // 
            this.tabStockOUT.Controls.Add(this.btnPDFprint);
            this.tabStockOUT.Controls.Add(this.dtpDateStockOUT);
            this.tabStockOUT.Controls.Add(this.gboxPrice);
            this.tabStockOUT.Controls.Add(this.gboxStock);
            this.tabStockOUT.Controls.Add(this.btnStockUpdate);
            this.tabStockOUT.Controls.Add(this.grbInventory);
            this.tabStockOUT.Controls.Add(this.dtgInventory);
            this.tabStockOUT.Location = new System.Drawing.Point(4, 22);
            this.tabStockOUT.Name = "tabStockOUT";
            this.tabStockOUT.Padding = new System.Windows.Forms.Padding(3);
            this.tabStockOUT.Size = new System.Drawing.Size(1036, 525);
            this.tabStockOUT.TabIndex = 0;
            this.tabStockOUT.Text = "STOCK OUT";
            this.tabStockOUT.UseVisualStyleBackColor = true;
            this.tabStockOUT.Click += new System.EventHandler(this.tabStock_Click);
            // 
            // btnPDFprint
            // 
            this.btnPDFprint.Location = new System.Drawing.Point(266, 154);
            this.btnPDFprint.Name = "btnPDFprint";
            this.btnPDFprint.Size = new System.Drawing.Size(75, 23);
            this.btnPDFprint.TabIndex = 41;
            this.btnPDFprint.Text = "print to pdf";
            this.btnPDFprint.UseVisualStyleBackColor = true;
            this.btnPDFprint.Click += new System.EventHandler(this.btnPDFprint_Click);
            // 
            // dtpDateStockOUT
            // 
            this.dtpDateStockOUT.CustomFormat = "yyyy-MM-dd";
            this.dtpDateStockOUT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateStockOUT.Location = new System.Drawing.Point(440, 154);
            this.dtpDateStockOUT.Name = "dtpDateStockOUT";
            this.dtpDateStockOUT.Size = new System.Drawing.Size(212, 20);
            this.dtpDateStockOUT.TabIndex = 40;
            this.dtpDateStockOUT.Value = new System.DateTime(2017, 10, 18, 8, 44, 41, 0);
            // 
            // gboxPrice
            // 
            this.gboxPrice.Controls.Add(this.lblSellPrice);
            this.gboxPrice.Controls.Add(this.lblIPrice);
            this.gboxPrice.Controls.Add(this.txtSellPrice);
            this.gboxPrice.Controls.Add(this.cmbInvMarkup);
            this.gboxPrice.Controls.Add(this.lblIMarkup);
            this.gboxPrice.Controls.Add(this.txtInvPrice);
            this.gboxPrice.Location = new System.Drawing.Point(854, 3);
            this.gboxPrice.Name = "gboxPrice";
            this.gboxPrice.Size = new System.Drawing.Size(167, 144);
            this.gboxPrice.TabIndex = 39;
            this.gboxPrice.TabStop = false;
            this.gboxPrice.Text = "Price";
            // 
            // lblSellPrice
            // 
            this.lblSellPrice.AutoSize = true;
            this.lblSellPrice.Location = new System.Drawing.Point(7, 95);
            this.lblSellPrice.Name = "lblSellPrice";
            this.lblSellPrice.Size = new System.Drawing.Size(92, 13);
            this.lblSellPrice.TabIndex = 33;
            this.lblSellPrice.Text = "Selling Price/ Unit";
            // 
            // lblIPrice
            // 
            this.lblIPrice.AutoSize = true;
            this.lblIPrice.Location = new System.Drawing.Point(7, 12);
            this.lblIPrice.Name = "lblIPrice";
            this.lblIPrice.Size = new System.Drawing.Size(58, 13);
            this.lblIPrice.TabIndex = 23;
            this.lblIPrice.Text = "Price/ Unit";
            // 
            // txtSellPrice
            // 
            this.txtSellPrice.BackColor = System.Drawing.SystemColors.Control;
            this.txtSellPrice.CausesValidation = false;
            this.txtSellPrice.Location = new System.Drawing.Point(10, 110);
            this.txtSellPrice.Name = "txtSellPrice";
            this.txtSellPrice.ReadOnly = true;
            this.txtSellPrice.Size = new System.Drawing.Size(76, 20);
            this.txtSellPrice.TabIndex = 32;
            // 
            // cmbInvMarkup
            // 
            this.cmbInvMarkup.FormattingEnabled = true;
            this.cmbInvMarkup.Items.AddRange(new object[] {
            "15",
            "20",
            "25",
            "30",
            "40",
            "50"});
            this.cmbInvMarkup.Location = new System.Drawing.Point(10, 71);
            this.cmbInvMarkup.Name = "cmbInvMarkup";
            this.cmbInvMarkup.Size = new System.Drawing.Size(76, 21);
            this.cmbInvMarkup.TabIndex = 30;
            // 
            // lblIMarkup
            // 
            this.lblIMarkup.AutoSize = true;
            this.lblIMarkup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIMarkup.Location = new System.Drawing.Point(7, 55);
            this.lblIMarkup.Name = "lblIMarkup";
            this.lblIMarkup.Size = new System.Drawing.Size(66, 13);
            this.lblIMarkup.TabIndex = 28;
            this.lblIMarkup.Text = "Mark-up %";
            // 
            // txtInvPrice
            // 
            this.txtInvPrice.Location = new System.Drawing.Point(10, 28);
            this.txtInvPrice.Name = "txtInvPrice";
            this.txtInvPrice.ReadOnly = true;
            this.txtInvPrice.Size = new System.Drawing.Size(100, 20);
            this.txtInvPrice.TabIndex = 7;
            // 
            // gboxStock
            // 
            this.gboxStock.Controls.Add(this.txtInvReLevel);
            this.gboxStock.Controls.Add(this.txtInvTotalStock);
            this.gboxStock.Controls.Add(this.lblInvTotalStock);
            this.gboxStock.Controls.Add(this.lblIReLevel);
            this.gboxStock.Controls.Add(this.lblIStock);
            this.gboxStock.Controls.Add(this.txtInvStockOut);
            this.gboxStock.Location = new System.Drawing.Point(676, 4);
            this.gboxStock.Name = "gboxStock";
            this.gboxStock.Size = new System.Drawing.Size(172, 144);
            this.gboxStock.TabIndex = 38;
            this.gboxStock.TabStop = false;
            this.gboxStock.Text = "Stock";
            // 
            // txtInvReLevel
            // 
            this.txtInvReLevel.BackColor = System.Drawing.SystemColors.Window;
            this.txtInvReLevel.Location = new System.Drawing.Point(13, 109);
            this.txtInvReLevel.Name = "txtInvReLevel";
            this.txtInvReLevel.Size = new System.Drawing.Size(78, 20);
            this.txtInvReLevel.TabIndex = 9;
            // 
            // txtInvTotalStock
            // 
            this.txtInvTotalStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtInvTotalStock.Location = new System.Drawing.Point(13, 31);
            this.txtInvTotalStock.Name = "txtInvTotalStock";
            this.txtInvTotalStock.ReadOnly = true;
            this.txtInvTotalStock.Size = new System.Drawing.Size(78, 20);
            this.txtInvTotalStock.TabIndex = 11;
            // 
            // lblInvTotalStock
            // 
            this.lblInvTotalStock.AutoSize = true;
            this.lblInvTotalStock.Location = new System.Drawing.Point(10, 15);
            this.lblInvTotalStock.Name = "lblInvTotalStock";
            this.lblInvTotalStock.Size = new System.Drawing.Size(62, 13);
            this.lblInvTotalStock.TabIndex = 26;
            this.lblInvTotalStock.Text = "Total Stock";
            // 
            // lblIReLevel
            // 
            this.lblIReLevel.AutoSize = true;
            this.lblIReLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIReLevel.Location = new System.Drawing.Point(10, 93);
            this.lblIReLevel.Name = "lblIReLevel";
            this.lblIReLevel.Size = new System.Drawing.Size(83, 13);
            this.lblIReLevel.TabIndex = 24;
            this.lblIReLevel.Text = "ReorderLevel";
            // 
            // lblIStock
            // 
            this.lblIStock.AutoSize = true;
            this.lblIStock.Location = new System.Drawing.Point(10, 54);
            this.lblIStock.Name = "lblIStock";
            this.lblIStock.Size = new System.Drawing.Size(55, 13);
            this.lblIStock.TabIndex = 22;
            this.lblIStock.Text = "Stock Out";
            // 
            // txtInvStockOut
            // 
            this.txtInvStockOut.Location = new System.Drawing.Point(13, 70);
            this.txtInvStockOut.Name = "txtInvStockOut";
            this.txtInvStockOut.Size = new System.Drawing.Size(78, 20);
            this.txtInvStockOut.TabIndex = 6;
            // 
            // btnStockUpdate
            // 
            this.btnStockUpdate.Location = new System.Drawing.Point(676, 154);
            this.btnStockUpdate.Name = "btnStockUpdate";
            this.btnStockUpdate.Size = new System.Drawing.Size(345, 23);
            this.btnStockUpdate.TabIndex = 24;
            this.btnStockUpdate.Text = "Save / Update";
            this.btnStockUpdate.UseVisualStyleBackColor = true;
            this.btnStockUpdate.Click += new System.EventHandler(this.btnInvUpdate_Click);
            // 
            // grbInventory
            // 
            this.grbInventory.Controls.Add(this.btnClear);
            this.grbInventory.Controls.Add(this.lblCategory);
            this.grbInventory.Controls.Add(this.btnSearch);
            this.grbInventory.Controls.Add(this.lblIDesc);
            this.grbInventory.Controls.Add(this.lblIItem);
            this.grbInventory.Controls.Add(this.lblICode);
            this.grbInventory.Controls.Add(this.lblIID);
            this.grbInventory.Controls.Add(this.txtInvCat);
            this.grbInventory.Controls.Add(this.txtInvDesc);
            this.grbInventory.Controls.Add(this.txtInvItem);
            this.grbInventory.Controls.Add(this.txtInvCode);
            this.grbInventory.Controls.Add(this.txtInvID);
            this.grbInventory.Location = new System.Drawing.Point(14, 3);
            this.grbInventory.Name = "grbInventory";
            this.grbInventory.Size = new System.Drawing.Size(656, 145);
            this.grbInventory.TabIndex = 34;
            this.grbInventory.TabStop = false;
            this.grbInventory.Text = "Inventory Details";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(297, 116);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 38;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(141, 91);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(57, 13);
            this.lblCategory.TabIndex = 20;
            this.lblCategory.Text = "Category";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(206, 116);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 39;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblIDesc
            // 
            this.lblIDesc.AutoSize = true;
            this.lblIDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIDesc.Location = new System.Drawing.Point(130, 64);
            this.lblIDesc.Name = "lblIDesc";
            this.lblIDesc.Size = new System.Drawing.Size(71, 13);
            this.lblIDesc.TabIndex = 19;
            this.lblIDesc.Text = "Description";
            // 
            // lblIItem
            // 
            this.lblIItem.AutoSize = true;
            this.lblIItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIItem.Location = new System.Drawing.Point(203, 19);
            this.lblIItem.Name = "lblIItem";
            this.lblIItem.Size = new System.Drawing.Size(31, 13);
            this.lblIItem.TabIndex = 18;
            this.lblIItem.Text = "Item";
            // 
            // lblICode
            // 
            this.lblICode.AutoSize = true;
            this.lblICode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblICode.Location = new System.Drawing.Point(73, 19);
            this.lblICode.Name = "lblICode";
            this.lblICode.Size = new System.Drawing.Size(63, 13);
            this.lblICode.TabIndex = 17;
            this.lblICode.Text = "Part Code";
            // 
            // lblIID
            // 
            this.lblIID.AutoSize = true;
            this.lblIID.Location = new System.Drawing.Point(8, 19);
            this.lblIID.Name = "lblIID";
            this.lblIID.Size = new System.Drawing.Size(18, 13);
            this.lblIID.TabIndex = 16;
            this.lblIID.Text = "ID";
            // 
            // txtInvCat
            // 
            this.txtInvCat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtInvCat.Location = new System.Drawing.Point(203, 87);
            this.txtInvCat.Name = "txtInvCat";
            this.txtInvCat.Size = new System.Drawing.Size(217, 20);
            this.txtInvCat.TabIndex = 8;
            // 
            // txtInvDesc
            // 
            this.txtInvDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtInvDesc.Location = new System.Drawing.Point(203, 61);
            this.txtInvDesc.Name = "txtInvDesc";
            this.txtInvDesc.Size = new System.Drawing.Size(435, 20);
            this.txtInvDesc.TabIndex = 5;
            // 
            // txtInvItem
            // 
            this.txtInvItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtInvItem.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtInvItem.Location = new System.Drawing.Point(203, 35);
            this.txtInvItem.Name = "txtInvItem";
            this.txtInvItem.Size = new System.Drawing.Size(435, 20);
            this.txtInvItem.TabIndex = 4;
            // 
            // txtInvCode
            // 
            this.txtInvCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtInvCode.Location = new System.Drawing.Point(73, 35);
            this.txtInvCode.Name = "txtInvCode";
            this.txtInvCode.Size = new System.Drawing.Size(124, 20);
            this.txtInvCode.TabIndex = 3;
            // 
            // txtInvID
            // 
            this.txtInvID.Location = new System.Drawing.Point(6, 35);
            this.txtInvID.Name = "txtInvID";
            this.txtInvID.ReadOnly = true;
            this.txtInvID.Size = new System.Drawing.Size(61, 20);
            this.txtInvID.TabIndex = 2;
            // 
            // dtgInventory
            // 
            this.dtgInventory.AllowDrop = true;
            this.dtgInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgInventory.Location = new System.Drawing.Point(6, 183);
            this.dtgInventory.Name = "dtgInventory";
            this.dtgInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgInventory.Size = new System.Drawing.Size(1024, 293);
            this.dtgInventory.TabIndex = 0;
            this.dtgInventory.Click += new System.EventHandler(this.dtgInventory_Click);
            // 
            // tabStockIN
            // 
            this.tabStockIN.Controls.Add(this.btnISIsave);
            this.tabStockIN.Controls.Add(this.btnISIrecordNewInvoice);
            this.tabStockIN.Controls.Add(this.groupBox1);
            this.tabStockIN.Controls.Add(this.groupBox4);
            this.tabStockIN.Controls.Add(this.groupBox5);
            this.tabStockIN.Controls.Add(this.btnISIpreviewCurrent);
            this.tabStockIN.Controls.Add(this.groupBox6);
            this.tabStockIN.Controls.Add(this.dtgStockIn);
            this.tabStockIN.Location = new System.Drawing.Point(4, 22);
            this.tabStockIN.Name = "tabStockIN";
            this.tabStockIN.Padding = new System.Windows.Forms.Padding(3);
            this.tabStockIN.Size = new System.Drawing.Size(1036, 525);
            this.tabStockIN.TabIndex = 8;
            this.tabStockIN.Text = "STOCK IN";
            this.tabStockIN.UseVisualStyleBackColor = true;
            // 
            // btnISIsave
            // 
            this.btnISIsave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnISIsave.Location = new System.Drawing.Point(401, 276);
            this.btnISIsave.Name = "btnISIsave";
            this.btnISIsave.Size = new System.Drawing.Size(223, 23);
            this.btnISIsave.TabIndex = 60;
            this.btnISIsave.Text = "Save Details";
            this.btnISIsave.UseVisualStyleBackColor = true;
            this.btnISIsave.Click += new System.EventHandler(this.btnISIsave_Click);
            // 
            // btnISIrecordNewInvoice
            // 
            this.btnISIrecordNewInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnISIrecordNewInvoice.Location = new System.Drawing.Point(778, 276);
            this.btnISIrecordNewInvoice.Name = "btnISIrecordNewInvoice";
            this.btnISIrecordNewInvoice.Size = new System.Drawing.Size(222, 23);
            this.btnISIrecordNewInvoice.TabIndex = 53;
            this.btnISIrecordNewInvoice.Text = "Record New Invoice";
            this.btnISIrecordNewInvoice.UseVisualStyleBackColor = true;
            this.btnISIrecordNewInvoice.Click += new System.EventHandler(this.btnISIrecordNewInvoice_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.cmbISISupplier);
            this.groupBox1.Location = new System.Drawing.Point(447, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 105);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Supplier Details";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(14, 31);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(45, 13);
            this.label40.TabIndex = 12;
            this.label40.Text = "Supplier";
            // 
            // cmbISISupplier
            // 
            this.cmbISISupplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbISISupplier.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.supplierBindingSource, "SupName", true));
            this.cmbISISupplier.DataSource = this.supplierBindingSource;
            this.cmbISISupplier.DisplayMember = "SupName";
            this.cmbISISupplier.FormattingEnabled = true;
            this.cmbISISupplier.Location = new System.Drawing.Point(122, 28);
            this.cmbISISupplier.Name = "cmbISISupplier";
            this.cmbISISupplier.Size = new System.Drawing.Size(417, 21);
            this.cmbISISupplier.TabIndex = 5;
            this.cmbISISupplier.ValueMember = "SupplierID";
            // 
            // supplierBindingSource
            // 
            this.supplierBindingSource.DataMember = "Supplier";
            this.supplierBindingSource.DataSource = this.supplierDataSet;
            // 
            // supplierDataSet
            // 
            this.supplierDataSet.DataSetName = "SupplierDataSet";
            this.supplierDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label41);
            this.groupBox4.Controls.Add(this.label42);
            this.groupBox4.Controls.Add(this.label43);
            this.groupBox4.Controls.Add(this.dtpInvoiceDate);
            this.groupBox4.Controls.Add(this.txtISITotal);
            this.groupBox4.Controls.Add(this.txtInvoiceNo);
            this.groupBox4.Location = new System.Drawing.Point(4, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(429, 105);
            this.groupBox4.TabIndex = 58;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Invoice Details";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(8, 75);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(101, 13);
            this.label41.TabIndex = 13;
            this.label41.Text = "Total Including VAT";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(8, 50);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(68, 13);
            this.label42.TabIndex = 12;
            this.label42.Text = "Invoice Date";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(8, 22);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(82, 13);
            this.label43.TabIndex = 11;
            this.label43.Text = "Invoice Number";
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpInvoiceDate.CustomFormat = "yyyy-MM-dd ";
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(187, 44);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(200, 20);
            this.dtpInvoiceDate.TabIndex = 4;
            this.dtpInvoiceDate.Value = new System.DateTime(2017, 10, 16, 15, 24, 7, 0);
            // 
            // txtISITotal
            // 
            this.txtISITotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtISITotal.Location = new System.Drawing.Point(287, 72);
            this.txtISITotal.Name = "txtISITotal";
            this.txtISITotal.Size = new System.Drawing.Size(100, 20);
            this.txtISITotal.TabIndex = 1;
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInvoiceNo.Location = new System.Drawing.Point(187, 16);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(200, 20);
            this.txtInvoiceNo.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.cmbISIinvMarkup);
            this.groupBox5.Controls.Add(this.label44);
            this.groupBox5.Controls.Add(this.txtISIinvReLevel);
            this.groupBox5.Controls.Add(this.label45);
            this.groupBox5.Controls.Add(this.txtISIstockTotal);
            this.groupBox5.Controls.Add(this.label46);
            this.groupBox5.Controls.Add(this.label47);
            this.groupBox5.Controls.Add(this.label48);
            this.groupBox5.Controls.Add(this.txtISIstockReceived);
            this.groupBox5.Controls.Add(this.txtISIstockPrice);
            this.groupBox5.Location = new System.Drawing.Point(767, 122);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(251, 148);
            this.groupBox5.TabIndex = 57;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Stock";
            // 
            // cmbISIinvMarkup
            // 
            this.cmbISIinvMarkup.FormattingEnabled = true;
            this.cmbISIinvMarkup.Items.AddRange(new object[] {
            "15",
            "20",
            "25",
            "30",
            "40",
            "50"});
            this.cmbISIinvMarkup.Location = new System.Drawing.Point(149, 120);
            this.cmbISIinvMarkup.Name = "cmbISIinvMarkup";
            this.cmbISIinvMarkup.Size = new System.Drawing.Size(78, 21);
            this.cmbISIinvMarkup.TabIndex = 54;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(154, 104);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(66, 13);
            this.label44.TabIndex = 53;
            this.label44.Text = "Mark-up %";
            // 
            // txtISIinvReLevel
            // 
            this.txtISIinvReLevel.BackColor = System.Drawing.SystemColors.Window;
            this.txtISIinvReLevel.Location = new System.Drawing.Point(149, 31);
            this.txtISIinvReLevel.Name = "txtISIinvReLevel";
            this.txtISIinvReLevel.Size = new System.Drawing.Size(78, 20);
            this.txtISIinvReLevel.TabIndex = 51;
            // 
            // label45
            // 
            this.label45.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(8, 59);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(95, 13);
            this.label45.TabIndex = 22;
            this.label45.Text = "Quantity Received";
            // 
            // txtISIstockTotal
            // 
            this.txtISIstockTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtISIstockTotal.Location = new System.Drawing.Point(8, 36);
            this.txtISIstockTotal.Name = "txtISIstockTotal";
            this.txtISIstockTotal.ReadOnly = true;
            this.txtISIstockTotal.Size = new System.Drawing.Size(78, 20);
            this.txtISIstockTotal.TabIndex = 48;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(146, 15);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(83, 13);
            this.label46.TabIndex = 52;
            this.label46.Text = "ReorderLevel";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(8, 18);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(62, 13);
            this.label47.TabIndex = 50;
            this.label47.Text = "Total Stock";
            // 
            // label48
            // 
            this.label48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(129, 59);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(104, 13);
            this.label48.TabIndex = 23;
            this.label48.Text = "Price/ Unit excl VAT";
            // 
            // txtISIstockReceived
            // 
            this.txtISIstockReceived.Location = new System.Drawing.Point(8, 77);
            this.txtISIstockReceived.Name = "txtISIstockReceived";
            this.txtISIstockReceived.Size = new System.Drawing.Size(78, 20);
            this.txtISIstockReceived.TabIndex = 6;
            // 
            // txtISIstockPrice
            // 
            this.txtISIstockPrice.Location = new System.Drawing.Point(149, 77);
            this.txtISIstockPrice.Name = "txtISIstockPrice";
            this.txtISIstockPrice.Size = new System.Drawing.Size(78, 20);
            this.txtISIstockPrice.TabIndex = 7;
            // 
            // btnISIpreviewCurrent
            // 
            this.btnISIpreviewCurrent.Location = new System.Drawing.Point(7, 276);
            this.btnISIpreviewCurrent.Name = "btnISIpreviewCurrent";
            this.btnISIpreviewCurrent.Size = new System.Drawing.Size(240, 23);
            this.btnISIpreviewCurrent.TabIndex = 55;
            this.btnISIpreviewCurrent.Text = "Preview Current Invoiced Items";
            this.btnISIpreviewCurrent.UseVisualStyleBackColor = true;
            this.btnISIpreviewCurrent.Click += new System.EventHandler(this.btnISIpreviewCurrent_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnISIinvDeleteItem);
            this.groupBox6.Controls.Add(this.btnISInewItem);
            this.groupBox6.Controls.Add(this.btnISIclear);
            this.groupBox6.Controls.Add(this.label49);
            this.groupBox6.Controls.Add(this.btnISIdelItemInvoice);
            this.groupBox6.Controls.Add(this.btnISIsearchItem);
            this.groupBox6.Controls.Add(this.label50);
            this.groupBox6.Controls.Add(this.label51);
            this.groupBox6.Controls.Add(this.label52);
            this.groupBox6.Controls.Add(this.label53);
            this.groupBox6.Controls.Add(this.txtISIinvCategory);
            this.groupBox6.Controls.Add(this.txtISIinvDescription);
            this.groupBox6.Controls.Add(this.txtISIinvItem);
            this.groupBox6.Controls.Add(this.txtISIinvCode);
            this.groupBox6.Controls.Add(this.txtISIinvID);
            this.groupBox6.Location = new System.Drawing.Point(4, 122);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(752, 147);
            this.groupBox6.TabIndex = 56;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Inventory Details";
            // 
            // btnISIinvDeleteItem
            // 
            this.btnISIinvDeleteItem.Location = new System.Drawing.Point(395, 118);
            this.btnISIinvDeleteItem.Name = "btnISIinvDeleteItem";
            this.btnISIinvDeleteItem.Size = new System.Drawing.Size(114, 23);
            this.btnISIinvDeleteItem.TabIndex = 42;
            this.btnISIinvDeleteItem.Text = "Delete Existing Item";
            this.btnISIinvDeleteItem.UseVisualStyleBackColor = true;
            this.btnISIinvDeleteItem.Click += new System.EventHandler(this.btnISIinvDeleteItem_Click);
            // 
            // btnISInewItem
            // 
            this.btnISInewItem.Location = new System.Drawing.Point(231, 118);
            this.btnISInewItem.Name = "btnISInewItem";
            this.btnISInewItem.Size = new System.Drawing.Size(114, 23);
            this.btnISInewItem.TabIndex = 41;
            this.btnISInewItem.Text = "Create New Item";
            this.btnISInewItem.UseVisualStyleBackColor = true;
            this.btnISInewItem.Click += new System.EventHandler(this.btnISInewItem_Click);
            // 
            // btnISIclear
            // 
            this.btnISIclear.Location = new System.Drawing.Point(655, 57);
            this.btnISIclear.Name = "btnISIclear";
            this.btnISIclear.Size = new System.Drawing.Size(89, 23);
            this.btnISIclear.TabIndex = 40;
            this.btnISIclear.Text = "Clear";
            this.btnISIclear.UseVisualStyleBackColor = true;
            this.btnISIclear.Click += new System.EventHandler(this.btnISIclear_Click);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(176, 90);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(49, 13);
            this.label49.TabIndex = 20;
            this.label49.Text = "Category";
            // 
            // btnISIdelItemInvoice
            // 
            this.btnISIdelItemInvoice.Location = new System.Drawing.Point(573, 118);
            this.btnISIdelItemInvoice.Name = "btnISIdelItemInvoice";
            this.btnISIdelItemInvoice.Size = new System.Drawing.Size(173, 23);
            this.btnISIdelItemInvoice.TabIndex = 36;
            this.btnISIdelItemInvoice.Text = "Delete Item from Invoice";
            this.btnISIdelItemInvoice.UseVisualStyleBackColor = true;
            this.btnISIdelItemInvoice.Click += new System.EventHandler(this.btnISIdelItemInvoice_Click);
            // 
            // btnISIsearchItem
            // 
            this.btnISIsearchItem.Location = new System.Drawing.Point(655, 31);
            this.btnISIsearchItem.Name = "btnISIsearchItem";
            this.btnISIsearchItem.Size = new System.Drawing.Size(89, 23);
            this.btnISIsearchItem.TabIndex = 39;
            this.btnISIsearchItem.Text = "Search";
            this.btnISIsearchItem.UseVisualStyleBackColor = true;
            this.btnISIsearchItem.Click += new System.EventHandler(this.btnISIsearchItem_Click);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(165, 63);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(60, 13);
            this.label50.TabIndex = 19;
            this.label50.Text = "Description";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(228, 16);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(27, 13);
            this.label51.TabIndex = 18;
            this.label51.Text = "Item";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(73, 19);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(54, 13);
            this.label52.TabIndex = 17;
            this.label52.Text = "Part Code";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(8, 19);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(18, 13);
            this.label53.TabIndex = 16;
            this.label53.Text = "ID";
            // 
            // txtISIinvCategory
            // 
            this.txtISIinvCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtISIinvCategory.Location = new System.Drawing.Point(231, 86);
            this.txtISIinvCategory.Name = "txtISIinvCategory";
            this.txtISIinvCategory.Size = new System.Drawing.Size(278, 20);
            this.txtISIinvCategory.TabIndex = 8;
            // 
            // txtISIinvDescription
            // 
            this.txtISIinvDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtISIinvDescription.Location = new System.Drawing.Point(231, 60);
            this.txtISIinvDescription.Name = "txtISIinvDescription";
            this.txtISIinvDescription.Size = new System.Drawing.Size(407, 20);
            this.txtISIinvDescription.TabIndex = 5;
            // 
            // txtISIinvItem
            // 
            this.txtISIinvItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtISIinvItem.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtISIinvItem.Location = new System.Drawing.Point(231, 34);
            this.txtISIinvItem.Name = "txtISIinvItem";
            this.txtISIinvItem.Size = new System.Drawing.Size(407, 20);
            this.txtISIinvItem.TabIndex = 4;
            // 
            // txtISIinvCode
            // 
            this.txtISIinvCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtISIinvCode.Location = new System.Drawing.Point(73, 35);
            this.txtISIinvCode.Name = "txtISIinvCode";
            this.txtISIinvCode.Size = new System.Drawing.Size(152, 20);
            this.txtISIinvCode.TabIndex = 3;
            // 
            // txtISIinvID
            // 
            this.txtISIinvID.Location = new System.Drawing.Point(6, 35);
            this.txtISIinvID.Name = "txtISIinvID";
            this.txtISIinvID.ReadOnly = true;
            this.txtISIinvID.Size = new System.Drawing.Size(61, 20);
            this.txtISIinvID.TabIndex = 2;
            // 
            // dtgStockIn
            // 
            this.dtgStockIn.AllowDrop = true;
            this.dtgStockIn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgStockIn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgStockIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgStockIn.Location = new System.Drawing.Point(4, 305);
            this.dtgStockIn.Name = "dtgStockIn";
            this.dtgStockIn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgStockIn.Size = new System.Drawing.Size(1012, 209);
            this.dtgStockIn.TabIndex = 54;
            this.dtgStockIn.Click += new System.EventHandler(this.dtgStockIn_Click);
            // 
            // tabStockCheck
            // 
            this.tabStockCheck.Controls.Add(this.groupBox2);
            this.tabStockCheck.Controls.Add(this.btnSCsave);
            this.tabStockCheck.Controls.Add(this.groupBox3);
            this.tabStockCheck.Controls.Add(this.dtgCheckStock);
            this.tabStockCheck.Location = new System.Drawing.Point(4, 22);
            this.tabStockCheck.Name = "tabStockCheck";
            this.tabStockCheck.Padding = new System.Windows.Forms.Padding(3);
            this.tabStockCheck.Size = new System.Drawing.Size(1036, 525);
            this.tabStockCheck.TabIndex = 7;
            this.tabStockCheck.Text = "STOCK CHECK";
            this.tabStockCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtSCinvRelevel);
            this.groupBox2.Controls.Add(this.txtSCsysTotal);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.txtSCactualTotal);
            this.groupBox2.Location = new System.Drawing.Point(771, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 145);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stock";
            // 
            // txtSCinvRelevel
            // 
            this.txtSCinvRelevel.BackColor = System.Drawing.SystemColors.Window;
            this.txtSCinvRelevel.Location = new System.Drawing.Point(13, 109);
            this.txtSCinvRelevel.Name = "txtSCinvRelevel";
            this.txtSCinvRelevel.Size = new System.Drawing.Size(126, 20);
            this.txtSCinvRelevel.TabIndex = 9;
            // 
            // txtSCsysTotal
            // 
            this.txtSCsysTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtSCsysTotal.Location = new System.Drawing.Point(13, 31);
            this.txtSCsysTotal.Name = "txtSCsysTotal";
            this.txtSCsysTotal.ReadOnly = true;
            this.txtSCsysTotal.Size = new System.Drawing.Size(126, 20);
            this.txtSCsysTotal.TabIndex = 11;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 15);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 13);
            this.label16.TabIndex = 26;
            this.label16.Text = "System Stock";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 93);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 13);
            this.label17.TabIndex = 24;
            this.label17.Text = "ReorderLevel";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 54);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 13);
            this.label18.TabIndex = 22;
            this.label18.Text = "Actual Stock";
            // 
            // txtSCactualTotal
            // 
            this.txtSCactualTotal.Location = new System.Drawing.Point(13, 70);
            this.txtSCactualTotal.Name = "txtSCactualTotal";
            this.txtSCactualTotal.Size = new System.Drawing.Size(126, 20);
            this.txtSCactualTotal.TabIndex = 6;
            // 
            // btnSCsave
            // 
            this.btnSCsave.Location = new System.Drawing.Point(676, 154);
            this.btnSCsave.Name = "btnSCsave";
            this.btnSCsave.Size = new System.Drawing.Size(345, 23);
            this.btnSCsave.TabIndex = 24;
            this.btnSCsave.Text = "Save / Update";
            this.btnSCsave.UseVisualStyleBackColor = true;
            this.btnSCsave.Click += new System.EventHandler(this.btnSCsave_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnClearSCinv);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.btnSearchSCinv);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.txtSCinvCat);
            this.groupBox3.Controls.Add(this.txtSCinvDesc);
            this.groupBox3.Controls.Add(this.txtSCinvItem);
            this.groupBox3.Controls.Add(this.txtSCinvCode);
            this.groupBox3.Controls.Add(this.txtSCinvID);
            this.groupBox3.Location = new System.Drawing.Point(24, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(741, 145);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Inventory Details";
            // 
            // btnClearSCinv
            // 
            this.btnClearSCinv.Location = new System.Drawing.Point(297, 116);
            this.btnClearSCinv.Name = "btnClearSCinv";
            this.btnClearSCinv.Size = new System.Drawing.Size(75, 23);
            this.btnClearSCinv.TabIndex = 38;
            this.btnClearSCinv.Text = "Clear";
            this.btnClearSCinv.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(148, 91);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(49, 13);
            this.label19.TabIndex = 20;
            this.label19.Text = "Category";
            // 
            // btnSearchSCinv
            // 
            this.btnSearchSCinv.Location = new System.Drawing.Point(206, 116);
            this.btnSearchSCinv.Name = "btnSearchSCinv";
            this.btnSearchSCinv.Size = new System.Drawing.Size(75, 23);
            this.btnSearchSCinv.TabIndex = 39;
            this.btnSearchSCinv.Text = "Search";
            this.btnSearchSCinv.UseVisualStyleBackColor = true;
            this.btnSearchSCinv.Click += new System.EventHandler(this.btnSearchSCinv_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(137, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 13);
            this.label20.TabIndex = 19;
            this.label20.Text = "Description";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(203, 19);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(27, 13);
            this.label21.TabIndex = 18;
            this.label21.Text = "Item";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(73, 19);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 13);
            this.label22.TabIndex = 17;
            this.label22.Text = "Part Code";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 19);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(18, 13);
            this.label23.TabIndex = 16;
            this.label23.Text = "ID";
            // 
            // txtSCinvCat
            // 
            this.txtSCinvCat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSCinvCat.Location = new System.Drawing.Point(203, 87);
            this.txtSCinvCat.Name = "txtSCinvCat";
            this.txtSCinvCat.Size = new System.Drawing.Size(318, 20);
            this.txtSCinvCat.TabIndex = 8;
            // 
            // txtSCinvDesc
            // 
            this.txtSCinvDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSCinvDesc.Location = new System.Drawing.Point(203, 61);
            this.txtSCinvDesc.Name = "txtSCinvDesc";
            this.txtSCinvDesc.Size = new System.Drawing.Size(499, 20);
            this.txtSCinvDesc.TabIndex = 5;
            // 
            // txtSCinvItem
            // 
            this.txtSCinvItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSCinvItem.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSCinvItem.Location = new System.Drawing.Point(203, 35);
            this.txtSCinvItem.Name = "txtSCinvItem";
            this.txtSCinvItem.Size = new System.Drawing.Size(499, 20);
            this.txtSCinvItem.TabIndex = 4;
            // 
            // txtSCinvCode
            // 
            this.txtSCinvCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSCinvCode.Location = new System.Drawing.Point(73, 35);
            this.txtSCinvCode.Name = "txtSCinvCode";
            this.txtSCinvCode.Size = new System.Drawing.Size(124, 20);
            this.txtSCinvCode.TabIndex = 3;
            // 
            // txtSCinvID
            // 
            this.txtSCinvID.Location = new System.Drawing.Point(6, 35);
            this.txtSCinvID.Name = "txtSCinvID";
            this.txtSCinvID.ReadOnly = true;
            this.txtSCinvID.Size = new System.Drawing.Size(61, 20);
            this.txtSCinvID.TabIndex = 2;
            // 
            // dtgCheckStock
            // 
            this.dtgCheckStock.AllowDrop = true;
            this.dtgCheckStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgCheckStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgCheckStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgCheckStock.Location = new System.Drawing.Point(6, 183);
            this.dtgCheckStock.Name = "dtgCheckStock";
            this.dtgCheckStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgCheckStock.Size = new System.Drawing.Size(1024, 293);
            this.dtgCheckStock.TabIndex = 0;
            this.dtgCheckStock.Click += new System.EventHandler(this.dtgCheckStock_Click);
            // 
            // tabSuppliers
            // 
            this.tabSuppliers.Controls.Add(this.label39);
            this.tabSuppliers.Controls.Add(this.cmbSupPayTerm);
            this.tabSuppliers.Controls.Add(this.btnSupClear);
            this.tabSuppliers.Controls.Add(this.btnSupSearch);
            this.tabSuppliers.Controls.Add(this.lblSupplier);
            this.tabSuppliers.Controls.Add(this.lblPrefix);
            this.tabSuppliers.Controls.Add(this.label11);
            this.tabSuppliers.Controls.Add(this.txtSupReTime);
            this.tabSuppliers.Controls.Add(this.txtSupPrefix);
            this.tabSuppliers.Controls.Add(this.btnDeleteSup);
            this.tabSuppliers.Controls.Add(this.btnSupINSERT);
            this.tabSuppliers.Controls.Add(this.btnSupUPDATE);
            this.tabSuppliers.Controls.Add(this.grbSContact);
            this.tabSuppliers.Controls.Add(this.grbSAddress);
            this.tabSuppliers.Controls.Add(this.txtSName);
            this.tabSuppliers.Controls.Add(this.txtSID);
            this.tabSuppliers.Controls.Add(this.dtgSuppliers);
            this.tabSuppliers.Location = new System.Drawing.Point(4, 22);
            this.tabSuppliers.Name = "tabSuppliers";
            this.tabSuppliers.Padding = new System.Windows.Forms.Padding(3);
            this.tabSuppliers.Size = new System.Drawing.Size(1036, 525);
            this.tabSuppliers.TabIndex = 1;
            this.tabSuppliers.Text = "SUPPLIERS";
            this.tabSuppliers.UseVisualStyleBackColor = true;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(812, 151);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(97, 13);
            this.label39.TabIndex = 46;
            this.label39.Text = "Payment Terms:";
            // 
            // cmbSupPayTerm
            // 
            this.cmbSupPayTerm.FormattingEnabled = true;
            this.cmbSupPayTerm.Items.AddRange(new object[] {
            "COD (Electronic Transfer)",
            "Account",
            "Electronic Transfer"});
            this.cmbSupPayTerm.Location = new System.Drawing.Point(914, 143);
            this.cmbSupPayTerm.Name = "cmbSupPayTerm";
            this.cmbSupPayTerm.Size = new System.Drawing.Size(116, 21);
            this.cmbSupPayTerm.TabIndex = 45;
            // 
            // btnSupClear
            // 
            this.btnSupClear.Location = new System.Drawing.Point(567, 88);
            this.btnSupClear.Name = "btnSupClear";
            this.btnSupClear.Size = new System.Drawing.Size(128, 23);
            this.btnSupClear.TabIndex = 44;
            this.btnSupClear.Text = "Clear";
            this.btnSupClear.UseVisualStyleBackColor = true;
            this.btnSupClear.Click += new System.EventHandler(this.btnSupClear_Click);
            // 
            // btnSupSearch
            // 
            this.btnSupSearch.Location = new System.Drawing.Point(566, 59);
            this.btnSupSearch.Name = "btnSupSearch";
            this.btnSupSearch.Size = new System.Drawing.Size(128, 23);
            this.btnSupSearch.TabIndex = 43;
            this.btnSupSearch.Text = "Search";
            this.btnSupSearch.UseVisualStyleBackColor = true;
            this.btnSupSearch.Click += new System.EventHandler(this.btnSupSearch_Click);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(59, 8);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(45, 13);
            this.lblSupplier.TabIndex = 42;
            this.lblSupplier.Text = "Supplier";
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(564, 8);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(33, 13);
            this.lblPrefix.TabIndex = 41;
            this.lblPrefix.Text = "Prefix";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(697, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(212, 13);
            this.label11.TabIndex = 40;
            this.label11.Text = "Time to Delivery in Days from Order:";
            // 
            // txtSupReTime
            // 
            this.txtSupReTime.Location = new System.Drawing.Point(914, 117);
            this.txtSupReTime.Name = "txtSupReTime";
            this.txtSupReTime.Size = new System.Drawing.Size(116, 20);
            this.txtSupReTime.TabIndex = 39;
            // 
            // txtSupPrefix
            // 
            this.txtSupPrefix.Location = new System.Drawing.Point(567, 24);
            this.txtSupPrefix.Name = "txtSupPrefix";
            this.txtSupPrefix.Size = new System.Drawing.Size(100, 20);
            this.txtSupPrefix.TabIndex = 38;
            // 
            // btnDeleteSup
            // 
            this.btnDeleteSup.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDeleteSup.Location = new System.Drawing.Point(82, 180);
            this.btnDeleteSup.Name = "btnDeleteSup";
            this.btnDeleteSup.Size = new System.Drawing.Size(209, 23);
            this.btnDeleteSup.TabIndex = 37;
            this.btnDeleteSup.Text = "Delete Supplier";
            this.btnDeleteSup.UseVisualStyleBackColor = true;
            this.btnDeleteSup.Click += new System.EventHandler(this.btnDeleteSup_Click);
            // 
            // btnSupINSERT
            // 
            this.btnSupINSERT.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSupINSERT.Location = new System.Drawing.Point(724, 180);
            this.btnSupINSERT.Name = "btnSupINSERT";
            this.btnSupINSERT.Size = new System.Drawing.Size(209, 23);
            this.btnSupINSERT.TabIndex = 36;
            this.btnSupINSERT.Text = "New Supplier";
            this.btnSupINSERT.UseVisualStyleBackColor = true;
            this.btnSupINSERT.Click += new System.EventHandler(this.btnSupINSERT_Click);
            // 
            // btnSupUPDATE
            // 
            this.btnSupUPDATE.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSupUPDATE.Location = new System.Drawing.Point(403, 180);
            this.btnSupUPDATE.Name = "btnSupUPDATE";
            this.btnSupUPDATE.Size = new System.Drawing.Size(209, 23);
            this.btnSupUPDATE.TabIndex = 25;
            this.btnSupUPDATE.Text = "Update";
            this.btnSupUPDATE.UseVisualStyleBackColor = true;
            this.btnSupUPDATE.Click += new System.EventHandler(this.btnSupUPDATE_Click);
            // 
            // grbSContact
            // 
            this.grbSContact.Controls.Add(this.lblBusnr);
            this.grbSContact.Controls.Add(this.lblSEmail);
            this.grbSContact.Controls.Add(this.lblSCell);
            this.grbSContact.Controls.Add(this.txtSCell);
            this.grbSContact.Controls.Add(this.txtSBusNr);
            this.grbSContact.Controls.Add(this.txtSREP);
            this.grbSContact.Controls.Add(this.lblSREP);
            this.grbSContact.Controls.Add(this.txtSEmail);
            this.grbSContact.Controls.Add(this.lblSContactP);
            this.grbSContact.Controls.Add(this.txtSContactP);
            this.grbSContact.Location = new System.Drawing.Point(8, 50);
            this.grbSContact.Name = "grbSContact";
            this.grbSContact.Size = new System.Drawing.Size(552, 104);
            this.grbSContact.TabIndex = 12;
            this.grbSContact.TabStop = false;
            this.grbSContact.Text = "Contact Details";
            // 
            // lblBusnr
            // 
            this.lblBusnr.AutoSize = true;
            this.lblBusnr.Location = new System.Drawing.Point(308, 19);
            this.lblBusnr.Name = "lblBusnr";
            this.lblBusnr.Size = new System.Drawing.Size(49, 13);
            this.lblBusnr.TabIndex = 16;
            this.lblBusnr.Text = "Business";
            // 
            // lblSEmail
            // 
            this.lblSEmail.AutoSize = true;
            this.lblSEmail.Location = new System.Drawing.Point(325, 75);
            this.lblSEmail.Name = "lblSEmail";
            this.lblSEmail.Size = new System.Drawing.Size(32, 13);
            this.lblSEmail.TabIndex = 13;
            this.lblSEmail.Text = "Email";
            // 
            // lblSCell
            // 
            this.lblSCell.AutoSize = true;
            this.lblSCell.Location = new System.Drawing.Point(333, 50);
            this.lblSCell.Name = "lblSCell";
            this.lblSCell.Size = new System.Drawing.Size(24, 13);
            this.lblSCell.TabIndex = 15;
            this.lblSCell.Text = "Cell";
            // 
            // txtSCell
            // 
            this.txtSCell.Location = new System.Drawing.Point(368, 42);
            this.txtSCell.Name = "txtSCell";
            this.txtSCell.Size = new System.Drawing.Size(170, 20);
            this.txtSCell.TabIndex = 6;
            // 
            // txtSBusNr
            // 
            this.txtSBusNr.Location = new System.Drawing.Point(368, 16);
            this.txtSBusNr.Name = "txtSBusNr";
            this.txtSBusNr.Size = new System.Drawing.Size(170, 20);
            this.txtSBusNr.TabIndex = 5;
            // 
            // txtSREP
            // 
            this.txtSREP.Location = new System.Drawing.Point(128, 42);
            this.txtSREP.Name = "txtSREP";
            this.txtSREP.Size = new System.Drawing.Size(153, 20);
            this.txtSREP.TabIndex = 4;
            // 
            // lblSREP
            // 
            this.lblSREP.AutoSize = true;
            this.lblSREP.Location = new System.Drawing.Point(5, 45);
            this.lblSREP.Name = "lblSREP";
            this.lblSREP.Size = new System.Drawing.Size(108, 13);
            this.lblSREP.TabIndex = 14;
            this.lblSREP.Text = "Sales Representative";
            // 
            // txtSEmail
            // 
            this.txtSEmail.Location = new System.Drawing.Point(368, 68);
            this.txtSEmail.Name = "txtSEmail";
            this.txtSEmail.Size = new System.Drawing.Size(170, 20);
            this.txtSEmail.TabIndex = 10;
            // 
            // lblSContactP
            // 
            this.lblSContactP.AutoSize = true;
            this.lblSContactP.Location = new System.Drawing.Point(5, 19);
            this.lblSContactP.Name = "lblSContactP";
            this.lblSContactP.Size = new System.Drawing.Size(80, 13);
            this.lblSContactP.TabIndex = 13;
            this.lblSContactP.Text = "Contact Person";
            // 
            // txtSContactP
            // 
            this.txtSContactP.Location = new System.Drawing.Point(127, 16);
            this.txtSContactP.Name = "txtSContactP";
            this.txtSContactP.Size = new System.Drawing.Size(153, 20);
            this.txtSContactP.TabIndex = 3;
            // 
            // grbSAddress
            // 
            this.grbSAddress.Controls.Add(this.txtSProv);
            this.grbSAddress.Controls.Add(this.txtSCity);
            this.grbSAddress.Controls.Add(this.txtSAddress);
            this.grbSAddress.Location = new System.Drawing.Point(700, 3);
            this.grbSAddress.Name = "grbSAddress";
            this.grbSAddress.Size = new System.Drawing.Size(330, 105);
            this.grbSAddress.TabIndex = 11;
            this.grbSAddress.TabStop = false;
            this.grbSAddress.Text = "Address";
            // 
            // txtSProv
            // 
            this.txtSProv.Location = new System.Drawing.Point(21, 73);
            this.txtSProv.Name = "txtSProv";
            this.txtSProv.Size = new System.Drawing.Size(293, 20);
            this.txtSProv.TabIndex = 9;
            // 
            // txtSCity
            // 
            this.txtSCity.Location = new System.Drawing.Point(21, 47);
            this.txtSCity.Name = "txtSCity";
            this.txtSCity.Size = new System.Drawing.Size(293, 20);
            this.txtSCity.TabIndex = 8;
            // 
            // txtSAddress
            // 
            this.txtSAddress.Location = new System.Drawing.Point(21, 21);
            this.txtSAddress.Name = "txtSAddress";
            this.txtSAddress.Size = new System.Drawing.Size(293, 20);
            this.txtSAddress.TabIndex = 7;
            // 
            // txtSName
            // 
            this.txtSName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSName.Location = new System.Drawing.Point(62, 24);
            this.txtSName.Name = "txtSName";
            this.txtSName.Size = new System.Drawing.Size(499, 20);
            this.txtSName.TabIndex = 2;
            // 
            // txtSID
            // 
            this.txtSID.Location = new System.Drawing.Point(7, 24);
            this.txtSID.Name = "txtSID";
            this.txtSID.Size = new System.Drawing.Size(49, 20);
            this.txtSID.TabIndex = 1;
            // 
            // dtgSuppliers
            // 
            this.dtgSuppliers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgSuppliers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgSuppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSuppliers.Location = new System.Drawing.Point(6, 209);
            this.dtgSuppliers.Name = "dtgSuppliers";
            this.dtgSuppliers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgSuppliers.Size = new System.Drawing.Size(1024, 220);
            this.dtgSuppliers.TabIndex = 0;
            this.dtgSuppliers.Click += new System.EventHandler(this.dtgSuppliers_Click);
            // 
            // tabInventoryValue
            // 
            this.tabInventoryValue.Controls.Add(this.label12);
            this.tabInventoryValue.Controls.Add(this.lblInventoryValue);
            this.tabInventoryValue.Controls.Add(this.txtStockValue);
            this.tabInventoryValue.Controls.Add(this.dtgInventoryValue);
            this.tabInventoryValue.Location = new System.Drawing.Point(4, 22);
            this.tabInventoryValue.Name = "tabInventoryValue";
            this.tabInventoryValue.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventoryValue.Size = new System.Drawing.Size(1036, 525);
            this.tabInventoryValue.TabIndex = 2;
            this.tabInventoryValue.Text = "INVENTORY VALUE";
            this.tabInventoryValue.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(791, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(143, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "(Based on FIFO PRINCIPLE)";
            // 
            // lblInventoryValue
            // 
            this.lblInventoryValue.AutoSize = true;
            this.lblInventoryValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInventoryValue.Location = new System.Drawing.Point(346, 43);
            this.lblInventoryValue.Name = "lblInventoryValue";
            this.lblInventoryValue.Size = new System.Drawing.Size(180, 13);
            this.lblInventoryValue.TabIndex = 2;
            this.lblInventoryValue.Text = "Total Value of Stock on Hand:";
            // 
            // txtStockValue
            // 
            this.txtStockValue.Location = new System.Drawing.Point(539, 40);
            this.txtStockValue.Name = "txtStockValue";
            this.txtStockValue.Size = new System.Drawing.Size(191, 20);
            this.txtStockValue.TabIndex = 1;
            // 
            // dtgInventoryValue
            // 
            this.dtgInventoryValue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgInventoryValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgInventoryValue.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgInventoryValue.Location = new System.Drawing.Point(3, 156);
            this.dtgInventoryValue.Name = "dtgInventoryValue";
            this.dtgInventoryValue.Size = new System.Drawing.Size(1030, 366);
            this.dtgInventoryValue.TabIndex = 0;
            // 
            // tabSupplierSummary
            // 
            this.tabSupplierSummary.Controls.Add(this.btnSearchClear);
            this.tabSupplierSummary.Controls.Add(this.btnSearchGO);
            this.tabSupplierSummary.Controls.Add(this.lblValueSummary);
            this.tabSupplierSummary.Controls.Add(this.txtSupSummaryValue);
            this.tabSupplierSummary.Controls.Add(this.dtgInventorySummary);
            this.tabSupplierSummary.Controls.Add(this.txtSearchSCell);
            this.tabSupplierSummary.Controls.Add(this.txtSearchSProv);
            this.tabSupplierSummary.Controls.Add(this.txtSearchSCity);
            this.tabSupplierSummary.Controls.Add(this.txtSearchSAddress);
            this.tabSupplierSummary.Controls.Add(this.txtSearchSBus);
            this.tabSupplierSummary.Controls.Add(this.txtSearchSEmail);
            this.tabSupplierSummary.Controls.Add(this.txtSearchSREP);
            this.tabSupplierSummary.Controls.Add(this.txtSearchSContact);
            this.tabSupplierSummary.Controls.Add(this.txtSearchSName);
            this.tabSupplierSummary.Controls.Add(this.txtSearchSID);
            this.tabSupplierSummary.Controls.Add(this.lblSearchSup);
            this.tabSupplierSummary.Controls.Add(this.txtSearchSupplier);
            this.tabSupplierSummary.Controls.Add(this.dtgSupplierSearch);
            this.tabSupplierSummary.Location = new System.Drawing.Point(4, 22);
            this.tabSupplierSummary.Name = "tabSupplierSummary";
            this.tabSupplierSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabSupplierSummary.Size = new System.Drawing.Size(1036, 525);
            this.tabSupplierSummary.TabIndex = 3;
            this.tabSupplierSummary.Text = "Supplier Summary";
            this.tabSupplierSummary.UseVisualStyleBackColor = true;
            // 
            // btnSearchClear
            // 
            this.btnSearchClear.Location = new System.Drawing.Point(578, 13);
            this.btnSearchClear.Name = "btnSearchClear";
            this.btnSearchClear.Size = new System.Drawing.Size(56, 20);
            this.btnSearchClear.TabIndex = 17;
            this.btnSearchClear.Text = "Clear";
            this.btnSearchClear.UseVisualStyleBackColor = true;
            // 
            // btnSearchGO
            // 
            this.btnSearchGO.Location = new System.Drawing.Point(478, 13);
            this.btnSearchGO.Name = "btnSearchGO";
            this.btnSearchGO.Size = new System.Drawing.Size(53, 20);
            this.btnSearchGO.TabIndex = 16;
            this.btnSearchGO.Text = "GO";
            this.btnSearchGO.UseVisualStyleBackColor = true;
            // 
            // lblValueSummary
            // 
            this.lblValueSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValueSummary.AutoSize = true;
            this.lblValueSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueSummary.Location = new System.Drawing.Point(838, 500);
            this.lblValueSummary.Name = "lblValueSummary";
            this.lblValueSummary.Size = new System.Drawing.Size(43, 13);
            this.lblValueSummary.TabIndex = 15;
            this.lblValueSummary.Text = "Value:";
            // 
            // txtSupSummaryValue
            // 
            this.txtSupSummaryValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSupSummaryValue.Location = new System.Drawing.Point(881, 497);
            this.txtSupSummaryValue.Name = "txtSupSummaryValue";
            this.txtSupSummaryValue.Size = new System.Drawing.Size(147, 20);
            this.txtSupSummaryValue.TabIndex = 14;
            // 
            // dtgInventorySummary
            // 
            this.dtgInventorySummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgInventorySummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgInventorySummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgInventorySummary.Location = new System.Drawing.Point(10, 259);
            this.dtgInventorySummary.Name = "dtgInventorySummary";
            this.dtgInventorySummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgInventorySummary.Size = new System.Drawing.Size(1020, 232);
            this.dtgInventorySummary.TabIndex = 13;
            // 
            // txtSearchSCell
            // 
            this.txtSearchSCell.Location = new System.Drawing.Point(550, 202);
            this.txtSearchSCell.Name = "txtSearchSCell";
            this.txtSearchSCell.Size = new System.Drawing.Size(173, 20);
            this.txtSearchSCell.TabIndex = 12;
            // 
            // txtSearchSProv
            // 
            this.txtSearchSProv.Location = new System.Drawing.Point(756, 228);
            this.txtSearchSProv.Name = "txtSearchSProv";
            this.txtSearchSProv.Size = new System.Drawing.Size(272, 20);
            this.txtSearchSProv.TabIndex = 11;
            // 
            // txtSearchSCity
            // 
            this.txtSearchSCity.Location = new System.Drawing.Point(756, 202);
            this.txtSearchSCity.Name = "txtSearchSCity";
            this.txtSearchSCity.Size = new System.Drawing.Size(272, 20);
            this.txtSearchSCity.TabIndex = 10;
            // 
            // txtSearchSAddress
            // 
            this.txtSearchSAddress.Location = new System.Drawing.Point(756, 176);
            this.txtSearchSAddress.Name = "txtSearchSAddress";
            this.txtSearchSAddress.Size = new System.Drawing.Size(272, 20);
            this.txtSearchSAddress.TabIndex = 9;
            // 
            // txtSearchSBus
            // 
            this.txtSearchSBus.Location = new System.Drawing.Point(550, 176);
            this.txtSearchSBus.Name = "txtSearchSBus";
            this.txtSearchSBus.Size = new System.Drawing.Size(173, 20);
            this.txtSearchSBus.TabIndex = 8;
            // 
            // txtSearchSEmail
            // 
            this.txtSearchSEmail.Location = new System.Drawing.Point(389, 228);
            this.txtSearchSEmail.Name = "txtSearchSEmail";
            this.txtSearchSEmail.Size = new System.Drawing.Size(334, 20);
            this.txtSearchSEmail.TabIndex = 7;
            // 
            // txtSearchSREP
            // 
            this.txtSearchSREP.Location = new System.Drawing.Point(389, 202);
            this.txtSearchSREP.Name = "txtSearchSREP";
            this.txtSearchSREP.Size = new System.Drawing.Size(155, 20);
            this.txtSearchSREP.TabIndex = 6;
            // 
            // txtSearchSContact
            // 
            this.txtSearchSContact.Location = new System.Drawing.Point(389, 176);
            this.txtSearchSContact.Name = "txtSearchSContact";
            this.txtSearchSContact.Size = new System.Drawing.Size(155, 20);
            this.txtSearchSContact.TabIndex = 5;
            // 
            // txtSearchSName
            // 
            this.txtSearchSName.Location = new System.Drawing.Point(54, 176);
            this.txtSearchSName.Name = "txtSearchSName";
            this.txtSearchSName.Size = new System.Drawing.Size(329, 20);
            this.txtSearchSName.TabIndex = 4;
            // 
            // txtSearchSID
            // 
            this.txtSearchSID.Location = new System.Drawing.Point(8, 176);
            this.txtSearchSID.Name = "txtSearchSID";
            this.txtSearchSID.Size = new System.Drawing.Size(40, 20);
            this.txtSearchSID.TabIndex = 3;
            // 
            // lblSearchSup
            // 
            this.lblSearchSup.AutoSize = true;
            this.lblSearchSup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchSup.Location = new System.Drawing.Point(52, 16);
            this.lblSearchSup.Name = "lblSearchSup";
            this.lblSearchSup.Size = new System.Drawing.Size(120, 13);
            this.lblSearchSup.TabIndex = 2;
            this.lblSearchSup.Text = "Search for Supplier:";
            // 
            // txtSearchSupplier
            // 
            this.txtSearchSupplier.Location = new System.Drawing.Point(180, 13);
            this.txtSearchSupplier.Name = "txtSearchSupplier";
            this.txtSearchSupplier.Size = new System.Drawing.Size(292, 20);
            this.txtSearchSupplier.TabIndex = 1;
            // 
            // dtgSupplierSearch
            // 
            this.dtgSupplierSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgSupplierSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgSupplierSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSupplierSearch.Location = new System.Drawing.Point(8, 44);
            this.dtgSupplierSearch.Name = "dtgSupplierSearch";
            this.dtgSupplierSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgSupplierSearch.Size = new System.Drawing.Size(1020, 119);
            this.dtgSupplierSearch.TabIndex = 0;
            // 
            // tabOrderStock
            // 
            this.tabOrderStock.Controls.Add(this.cbxOrderSup);
            this.tabOrderStock.Controls.Add(this.btnOrderSupGO);
            this.tabOrderStock.Controls.Add(this.btnOrderFilter);
            this.tabOrderStock.Controls.Add(this.label3);
            this.tabOrderStock.Controls.Add(this.label14);
            this.tabOrderStock.Controls.Add(this.txtOrderNumber);
            this.tabOrderStock.Controls.Add(this.label38);
            this.tabOrderStock.Controls.Add(this.label37);
            this.tabOrderStock.Controls.Add(this.label36);
            this.tabOrderStock.Controls.Add(this.label15);
            this.tabOrderStock.Controls.Add(this.label10);
            this.tabOrderStock.Controls.Add(this.label9);
            this.tabOrderStock.Controls.Add(this.label8);
            this.tabOrderStock.Controls.Add(this.label2);
            this.tabOrderStock.Controls.Add(this.txtOrderTotalIncl);
            this.tabOrderStock.Controls.Add(this.txtOrderVAT);
            this.tabOrderStock.Controls.Add(this.txtOrderTotalExcl);
            this.tabOrderStock.Controls.Add(this.btnOrderNextItem);
            this.tabOrderStock.Controls.Add(this.btnOrderPreview);
            this.tabOrderStock.Controls.Add(this.btnSaveOrder);
            this.tabOrderStock.Controls.Add(this.dtgSupOrderHistory);
            this.tabOrderStock.Controls.Add(this.btnRemoveAll);
            this.tabOrderStock.Controls.Add(this.btnAddAll);
            this.tabOrderStock.Controls.Add(this.btnRemoveSingle);
            this.tabOrderStock.Controls.Add(this.btnAddSingle);
            this.tabOrderStock.Controls.Add(this.dtgOrderInvList);
            this.tabOrderStock.Controls.Add(this.dtgSupOrderList);
            this.tabOrderStock.Location = new System.Drawing.Point(4, 22);
            this.tabOrderStock.Name = "tabOrderStock";
            this.tabOrderStock.Padding = new System.Windows.Forms.Padding(3);
            this.tabOrderStock.Size = new System.Drawing.Size(1036, 525);
            this.tabOrderStock.TabIndex = 4;
            this.tabOrderStock.Tag = "";
            this.tabOrderStock.Text = "ORDER STOCK";
            this.tabOrderStock.UseVisualStyleBackColor = true;
            // 
            // cbxOrderSup
            // 
            this.cbxOrderSup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxOrderSup.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.supplierBindingSource, "SupName", true));
            this.cbxOrderSup.DataSource = this.supplierBindingSource;
            this.cbxOrderSup.DisplayMember = "SupName";
            this.cbxOrderSup.FormattingEnabled = true;
            this.cbxOrderSup.Location = new System.Drawing.Point(77, 9);
            this.cbxOrderSup.Name = "cbxOrderSup";
            this.cbxOrderSup.Size = new System.Drawing.Size(366, 21);
            this.cbxOrderSup.TabIndex = 70;
            this.cbxOrderSup.ValueMember = "SupplierID";
            // 
            // btnOrderSupGO
            // 
            this.btnOrderSupGO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOrderSupGO.Location = new System.Drawing.Point(591, 8);
            this.btnOrderSupGO.Name = "btnOrderSupGO";
            this.btnOrderSupGO.Size = new System.Drawing.Size(136, 21);
            this.btnOrderSupGO.TabIndex = 69;
            this.btnOrderSupGO.Text = "Generate Order Number";
            this.btnOrderSupGO.UseVisualStyleBackColor = true;
            this.btnOrderSupGO.Click += new System.EventHandler(this.btnOrderSupGO_Click);
            // 
            // btnOrderFilter
            // 
            this.btnOrderFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOrderFilter.Location = new System.Drawing.Point(449, 8);
            this.btnOrderFilter.Name = "btnOrderFilter";
            this.btnOrderFilter.Size = new System.Drawing.Size(136, 21);
            this.btnOrderFilter.TabIndex = 68;
            this.btnOrderFilter.Text = "Filter History by Supplier";
            this.btnOrderFilter.UseVisualStyleBackColor = true;
            this.btnOrderFilter.Click += new System.EventHandler(this.btnOrderFilter_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(287, 455);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 67;
            this.label3.Text = "Order Number:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(8, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 13);
            this.label14.TabIndex = 63;
            this.label14.Text = "Suppliers:";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOrderNumber.Location = new System.Drawing.Point(395, 452);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(274, 20);
            this.txtOrderNumber.TabIndex = 62;
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(685, 168);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(341, 13);
            this.label38.TabIndex = 61;
            this.label38.Text = "(Click on Item on the right and drag over, or create new or click ALL>>)";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(568, 168);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(93, 13);
            this.label37.TabIndex = 60;
            this.label37.Text = "New Order Details";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(8, 168);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(68, 13);
            this.label36.TabIndex = 59;
            this.label36.Text = "Order Details";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 32);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 13);
            this.label15.TabIndex = 58;
            this.label15.Text = "Order History";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(807, 493);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 13);
            this.label10.TabIndex = 56;
            this.label10.Text = "TOTAL INCL. VAT  R:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(829, 467);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 55;
            this.label9.Text = "14% VAT  R:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(834, 441);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 54;
            this.label8.Text = "TOTAL  R:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 455);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 52;
            // 
            // txtOrderTotalIncl
            // 
            this.txtOrderTotalIncl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOrderTotalIncl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtOrderTotalIncl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderTotalIncl.Location = new System.Drawing.Point(926, 486);
            this.txtOrderTotalIncl.Name = "txtOrderTotalIncl";
            this.txtOrderTotalIncl.Size = new System.Drawing.Size(100, 20);
            this.txtOrderTotalIncl.TabIndex = 50;
            // 
            // txtOrderVAT
            // 
            this.txtOrderVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOrderVAT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtOrderVAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderVAT.Location = new System.Drawing.Point(926, 460);
            this.txtOrderVAT.Name = "txtOrderVAT";
            this.txtOrderVAT.Size = new System.Drawing.Size(100, 20);
            this.txtOrderVAT.TabIndex = 49;
            // 
            // txtOrderTotalExcl
            // 
            this.txtOrderTotalExcl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOrderTotalExcl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtOrderTotalExcl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderTotalExcl.Location = new System.Drawing.Point(926, 434);
            this.txtOrderTotalExcl.Name = "txtOrderTotalExcl";
            this.txtOrderTotalExcl.Size = new System.Drawing.Size(100, 20);
            this.txtOrderTotalExcl.TabIndex = 48;
            // 
            // btnOrderNextItem
            // 
            this.btnOrderNextItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOrderNextItem.Location = new System.Drawing.Point(939, 405);
            this.btnOrderNextItem.Name = "btnOrderNextItem";
            this.btnOrderNextItem.Size = new System.Drawing.Size(87, 23);
            this.btnOrderNextItem.TabIndex = 47;
            this.btnOrderNextItem.Text = "Next";
            this.btnOrderNextItem.UseVisualStyleBackColor = true;
            // 
            // btnOrderPreview
            // 
            this.btnOrderPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOrderPreview.Location = new System.Drawing.Point(506, 483);
            this.btnOrderPreview.Name = "btnOrderPreview";
            this.btnOrderPreview.Size = new System.Drawing.Size(88, 23);
            this.btnOrderPreview.TabIndex = 46;
            this.btnOrderPreview.Text = "Preview";
            this.btnOrderPreview.UseVisualStyleBackColor = true;
            this.btnOrderPreview.Click += new System.EventHandler(this.btnPDFprint_Click);
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveOrder.Location = new System.Drawing.Point(395, 484);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(89, 23);
            this.btnSaveOrder.TabIndex = 45;
            this.btnSaveOrder.Text = "Save";
            this.btnSaveOrder.UseVisualStyleBackColor = true;
            // 
            // dtgSupOrderHistory
            // 
            this.dtgSupOrderHistory.AllowUserToAddRows = false;
            this.dtgSupOrderHistory.AllowUserToDeleteRows = false;
            this.dtgSupOrderHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgSupOrderHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgSupOrderHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSupOrderHistory.Location = new System.Drawing.Point(6, 48);
            this.dtgSupOrderHistory.Name = "dtgSupOrderHistory";
            this.dtgSupOrderHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgSupOrderHistory.Size = new System.Drawing.Size(1024, 107);
            this.dtgSupOrderHistory.TabIndex = 18;
            this.dtgSupOrderHistory.Click += new System.EventHandler(this.dtgSupOrderHistory_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(481, 275);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveAll.TabIndex = 5;
            this.btnRemoveAll.Text = "CLEAR";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            // 
            // btnAddAll
            // 
            this.btnAddAll.Location = new System.Drawing.Point(481, 246);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(75, 23);
            this.btnAddAll.TabIndex = 4;
            this.btnAddAll.Text = "ALL >>";
            this.btnAddAll.UseVisualStyleBackColor = true;
            // 
            // btnRemoveSingle
            // 
            this.btnRemoveSingle.Location = new System.Drawing.Point(481, 217);
            this.btnRemoveSingle.Name = "btnRemoveSingle";
            this.btnRemoveSingle.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveSingle.TabIndex = 3;
            this.btnRemoveSingle.Text = "<";
            this.btnRemoveSingle.UseVisualStyleBackColor = true;
            // 
            // btnAddSingle
            // 
            this.btnAddSingle.Location = new System.Drawing.Point(481, 188);
            this.btnAddSingle.Name = "btnAddSingle";
            this.btnAddSingle.Size = new System.Drawing.Size(75, 23);
            this.btnAddSingle.TabIndex = 2;
            this.btnAddSingle.Text = ">";
            this.btnAddSingle.UseVisualStyleBackColor = true;
            // 
            // dtgOrderInvList
            // 
            this.dtgOrderInvList.AllowDrop = true;
            this.dtgOrderInvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgOrderInvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dtgOrderInvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgOrderInvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.invID,
            this.invCode,
            this.invItem,
            this.invSupDescription,
            this.invDescription,
            this.invLength,
            this.orderQuantity,
            this.orderPrice});
            this.dtgOrderInvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dtgOrderInvList.Location = new System.Drawing.Point(571, 184);
            this.dtgOrderInvList.Name = "dtgOrderInvList";
            this.dtgOrderInvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtgOrderInvList.Size = new System.Drawing.Size(455, 205);
            this.dtgOrderInvList.TabIndex = 1;
            // 
            // invID
            // 
            this.invID.HeaderText = "ID";
            this.invID.Name = "invID";
            this.invID.ReadOnly = true;
            this.invID.Width = 43;
            // 
            // invCode
            // 
            this.invCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.invCode.HeaderText = "Part Code";
            this.invCode.Name = "invCode";
            this.invCode.Width = 73;
            // 
            // invItem
            // 
            this.invItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.invItem.HeaderText = "Item";
            this.invItem.Name = "invItem";
            this.invItem.Width = 52;
            // 
            // invSupDescription
            // 
            this.invSupDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.invSupDescription.HeaderText = "Description to Supplier";
            this.invSupDescription.Name = "invSupDescription";
            this.invSupDescription.Width = 92;
            // 
            // invDescription
            // 
            this.invDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.invDescription.HeaderText = "Local Description";
            this.invDescription.Name = "invDescription";
            this.invDescription.Width = 105;
            // 
            // invLength
            // 
            this.invLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.invLength.HeaderText = "Length";
            this.invLength.Name = "invLength";
            this.invLength.Width = 65;
            // 
            // orderQuantity
            // 
            this.orderQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.orderQuantity.HeaderText = "Quantity";
            this.orderQuantity.Name = "orderQuantity";
            this.orderQuantity.Width = 71;
            // 
            // orderPrice
            // 
            this.orderPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.orderPrice.HeaderText = "Price";
            this.orderPrice.Name = "orderPrice";
            this.orderPrice.Width = 56;
            // 
            // dtgSupOrderList
            // 
            this.dtgSupOrderList.AllowUserToAddRows = false;
            this.dtgSupOrderList.AllowUserToDeleteRows = false;
            this.dtgSupOrderList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtgSupOrderList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSupOrderList.Location = new System.Drawing.Point(6, 184);
            this.dtgSupOrderList.Name = "dtgSupOrderList";
            this.dtgSupOrderList.ReadOnly = true;
            this.dtgSupOrderList.RowTemplate.ReadOnly = true;
            this.dtgSupOrderList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgSupOrderList.Size = new System.Drawing.Size(455, 205);
            this.dtgSupOrderList.TabIndex = 0;
            // 
            // tabOrderInventory
            // 
            this.tabOrderInventory.Controls.Add(this.txtOrderID);
            this.tabOrderInventory.Controls.Add(this.textBox6);
            this.tabOrderInventory.Controls.Add(this.textBox5);
            this.tabOrderInventory.Controls.Add(this.textBox4);
            this.tabOrderInventory.Controls.Add(this.textBox3);
            this.tabOrderInventory.Controls.Add(this.textBox2);
            this.tabOrderInventory.Controls.Add(this.textBox1);
            this.tabOrderInventory.Controls.Add(this.label13);
            this.tabOrderInventory.Controls.Add(this.cmbOrderSupplier);
            this.tabOrderInventory.Controls.Add(this.dtgNewInvOrderDetails);
            this.tabOrderInventory.Controls.Add(this.dtgInvOrderDetails);
            this.tabOrderInventory.Controls.Add(this.dtgInvOrderHistory);
            this.tabOrderInventory.Location = new System.Drawing.Point(4, 22);
            this.tabOrderInventory.Name = "tabOrderInventory";
            this.tabOrderInventory.Padding = new System.Windows.Forms.Padding(3);
            this.tabOrderInventory.Size = new System.Drawing.Size(1036, 525);
            this.tabOrderInventory.TabIndex = 6;
            this.tabOrderInventory.Text = "ORDER INVENTORY";
            this.tabOrderInventory.UseVisualStyleBackColor = true;
            // 
            // txtOrderID
            // 
            this.txtOrderID.Location = new System.Drawing.Point(580, 318);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new System.Drawing.Size(46, 20);
            this.txtOrderID.TabIndex = 11;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(928, 394);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 10;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(738, 394);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 9;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(632, 394);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 8;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(632, 368);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(396, 20);
            this.textBox3.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(632, 342);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(396, 20);
            this.textBox2.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(632, 318);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(259, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(37, 37);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Choose the Supplier:";
            // 
            // cmbOrderSupplier
            // 
            this.cmbOrderSupplier.FormattingEnabled = true;
            this.cmbOrderSupplier.Location = new System.Drawing.Point(181, 34);
            this.cmbOrderSupplier.Name = "cmbOrderSupplier";
            this.cmbOrderSupplier.Size = new System.Drawing.Size(270, 21);
            this.cmbOrderSupplier.TabIndex = 3;
            // 
            // dtgNewInvOrderDetails
            // 
            this.dtgNewInvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgNewInvOrderDetails.Location = new System.Drawing.Point(8, 318);
            this.dtgNewInvOrderDetails.Name = "dtgNewInvOrderDetails";
            this.dtgNewInvOrderDetails.Size = new System.Drawing.Size(443, 166);
            this.dtgNewInvOrderDetails.TabIndex = 2;
            // 
            // dtgInvOrderDetails
            // 
            this.dtgInvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgInvOrderDetails.Location = new System.Drawing.Point(588, 73);
            this.dtgInvOrderDetails.Name = "dtgInvOrderDetails";
            this.dtgInvOrderDetails.Size = new System.Drawing.Size(440, 224);
            this.dtgInvOrderDetails.TabIndex = 1;
            // 
            // dtgInvOrderHistory
            // 
            this.dtgInvOrderHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgInvOrderHistory.Location = new System.Drawing.Point(8, 73);
            this.dtgInvOrderHistory.Name = "dtgInvOrderHistory";
            this.dtgInvOrderHistory.Size = new System.Drawing.Size(443, 224);
            this.dtgInvOrderHistory.TabIndex = 0;
            // 
            // supplierTableAdapter
            // 
            this.supplierTableAdapter.ClearBeforeFill = true;
            // 
            // frmInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 551);
            this.Controls.Add(this.tConInventory);
            this.Name = "frmInventory";
            this.Text = "Inventory";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInventory_FormClosing);
            this.Load += new System.EventHandler(this.frmInventory_Load);
            this.tConInventory.ResumeLayout(false);
            this.tabStockOUT.ResumeLayout(false);
            this.gboxPrice.ResumeLayout(false);
            this.gboxPrice.PerformLayout();
            this.gboxStock.ResumeLayout(false);
            this.gboxStock.PerformLayout();
            this.grbInventory.ResumeLayout(false);
            this.grbInventory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventory)).EndInit();
            this.tabStockIN.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.supplierBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.supplierDataSet)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgStockIn)).EndInit();
            this.tabStockCheck.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCheckStock)).EndInit();
            this.tabSuppliers.ResumeLayout(false);
            this.tabSuppliers.PerformLayout();
            this.grbSContact.ResumeLayout(false);
            this.grbSContact.PerformLayout();
            this.grbSAddress.ResumeLayout(false);
            this.grbSAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSuppliers)).EndInit();
            this.tabInventoryValue.ResumeLayout(false);
            this.tabInventoryValue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventoryValue)).EndInit();
            this.tabSupplierSummary.ResumeLayout(false);
            this.tabSupplierSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventorySummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSupplierSearch)).EndInit();
            this.tabOrderStock.ResumeLayout(false);
            this.tabOrderStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSupOrderHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrderInvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSupOrderList)).EndInit();
            this.tabOrderInventory.ResumeLayout(false);
            this.tabOrderInventory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgNewInvOrderDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInvOrderDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInvOrderHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tConInventory;
        private System.Windows.Forms.TabPage tabStockOUT;
        private System.Windows.Forms.DataGridView dtgInventory;
        private System.Windows.Forms.TabPage tabSuppliers;
        private System.Windows.Forms.TabPage tabInventoryValue;
        private System.Windows.Forms.TextBox txtInvTotalStock;
        private System.Windows.Forms.TextBox txtInvReLevel;
        private System.Windows.Forms.TextBox txtInvCat;
        private System.Windows.Forms.TextBox txtInvPrice;
        private System.Windows.Forms.TextBox txtInvStockOut;
        private System.Windows.Forms.TextBox txtInvDesc;
        private System.Windows.Forms.TextBox txtInvItem;
        private System.Windows.Forms.TextBox txtInvCode;
        private System.Windows.Forms.TextBox txtInvID;
        private System.Windows.Forms.Label lblIMarkup;
        private System.Windows.Forms.Label lblInvTotalStock;
        private System.Windows.Forms.Label lblIReLevel;
        private System.Windows.Forms.Label lblIPrice;
        private System.Windows.Forms.Label lblIStock;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblIDesc;
        private System.Windows.Forms.Label lblIItem;
        private System.Windows.Forms.Label lblICode;
        private System.Windows.Forms.Label lblIID;
        private System.Windows.Forms.ComboBox cmbInvMarkup;
        private System.Windows.Forms.TextBox txtSellPrice;
        private System.Windows.Forms.TextBox txtSEmail;
        private System.Windows.Forms.TextBox txtSProv;
        private System.Windows.Forms.TextBox txtSCity;
        private System.Windows.Forms.TextBox txtSAddress;
        private System.Windows.Forms.TextBox txtSCell;
        private System.Windows.Forms.TextBox txtSBusNr;
        private System.Windows.Forms.TextBox txtSREP;
        private System.Windows.Forms.TextBox txtSContactP;
        private System.Windows.Forms.TextBox txtSName;
        private System.Windows.Forms.TextBox txtSID;
        private System.Windows.Forms.DataGridView dtgSuppliers;
        private System.Windows.Forms.GroupBox grbSAddress;
        private System.Windows.Forms.GroupBox grbSContact;
        private System.Windows.Forms.Label lblSCell;
        private System.Windows.Forms.Label lblSREP;
        private System.Windows.Forms.Label lblSContactP;
        private System.Windows.Forms.Label lblSEmail;
        private System.Windows.Forms.GroupBox grbInventory;
        private System.Windows.Forms.Button btnStockUpdate;
        private System.Windows.Forms.Button btnDeleteSup;
        private System.Windows.Forms.Button btnSupINSERT;
        private System.Windows.Forms.Button btnSupUPDATE;
        private System.Windows.Forms.DataGridView dtgInventoryValue;
        private System.Windows.Forms.TextBox txtStockValue;
        private System.Windows.Forms.TabPage tabSupplierSummary;
        private System.Windows.Forms.DataGridView dtgSupplierSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblValueSummary;
        private System.Windows.Forms.TextBox txtSupSummaryValue;
        private System.Windows.Forms.DataGridView dtgInventorySummary;
        private System.Windows.Forms.TextBox txtSearchSCell;
        private System.Windows.Forms.TextBox txtSearchSProv;
        private System.Windows.Forms.TextBox txtSearchSCity;
        private System.Windows.Forms.TextBox txtSearchSAddress;
        private System.Windows.Forms.TextBox txtSearchSBus;
        private System.Windows.Forms.TextBox txtSearchSEmail;
        private System.Windows.Forms.TextBox txtSearchSREP;
        private System.Windows.Forms.TextBox txtSearchSContact;
        private System.Windows.Forms.TextBox txtSearchSName;
        private System.Windows.Forms.TextBox txtSearchSID;
        private System.Windows.Forms.Label lblSearchSup;
        private System.Windows.Forms.TextBox txtSearchSupplier;
        private System.Windows.Forms.Button btnSearchClear;
        private System.Windows.Forms.Button btnSearchGO;
        private System.Windows.Forms.Label lblInventoryValue;
        private System.Windows.Forms.TabPage tabOrderStock;
        private System.Windows.Forms.DataGridView dtgSupOrderHistory;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnRemoveSingle;
        private System.Windows.Forms.Button btnAddSingle;
        private System.Windows.Forms.DataGridView dtgOrderInvList;
        private System.Windows.Forms.DataGridView dtgSupOrderList;
        private System.Windows.Forms.TextBox txtOrderTotalIncl;
        private System.Windows.Forms.TextBox txtOrderVAT;
        private System.Windows.Forms.TextBox txtOrderTotalExcl;
        private System.Windows.Forms.Button btnOrderNextItem;
        private System.Windows.Forms.Button btnOrderPreview;
        private System.Windows.Forms.Button btnSaveOrder;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gboxPrice;
        private System.Windows.Forms.GroupBox gboxStock;
        private System.Windows.Forms.Label lblSellPrice;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSupReTime;
        private System.Windows.Forms.TextBox txtSupPrefix;
        private System.Windows.Forms.Label lblBusnr;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabOrderInventory;
        private System.Windows.Forms.TabPage tabStockCheck;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSCinvRelevel;
        private System.Windows.Forms.TextBox txtSCsysTotal;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtSCactualTotal;
        private System.Windows.Forms.Button btnSCsave;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClearSCinv;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnSearchSCinv;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtSCinvCat;
        private System.Windows.Forms.TextBox txtSCinvDesc;
        private System.Windows.Forms.TextBox txtSCinvItem;
        private System.Windows.Forms.TextBox txtSCinvCode;
        private System.Windows.Forms.TextBox txtSCinvID;
        private System.Windows.Forms.DataGridView dtgCheckStock;
        private System.Windows.Forms.Button btnSupSearch;
        private System.Windows.Forms.Button btnSupClear;
        private System.Windows.Forms.DateTimePicker dtpDateStockOUT;
        private System.Windows.Forms.Button btnPDFprint;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbOrderSupplier;
        private System.Windows.Forms.DataGridView dtgNewInvOrderDetails;
        private System.Windows.Forms.DataGridView dtgInvOrderDetails;
        private System.Windows.Forms.DataGridView dtgInvOrderHistory;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.ComboBox cmbSupPayTerm;
        private System.Windows.Forms.TabPage tabStockIN;
        private System.Windows.Forms.Button btnISIsave;
        private System.Windows.Forms.Button btnISIrecordNewInvoice;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ComboBox cmbISISupplier;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.TextBox txtISITotal;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmbISIinvMarkup;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtISIinvReLevel;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtISIstockTotal;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtISIstockReceived;
        private System.Windows.Forms.TextBox txtISIstockPrice;
        private System.Windows.Forms.Button btnISIpreviewCurrent;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnISIinvDeleteItem;
        private System.Windows.Forms.Button btnISInewItem;
        private System.Windows.Forms.Button btnISIclear;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Button btnISIdelItemInvoice;
        private System.Windows.Forms.Button btnISIsearchItem;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TextBox txtISIinvCategory;
        private System.Windows.Forms.TextBox txtISIinvDescription;
        private System.Windows.Forms.TextBox txtISIinvItem;
        private System.Windows.Forms.TextBox txtISIinvCode;
        private System.Windows.Forms.TextBox txtISIinvID;
        private System.Windows.Forms.DataGridView dtgStockIn;
        private SupplierDataSet supplierDataSet;
        private System.Windows.Forms.BindingSource supplierBindingSource;
        private SupplierDataSetTableAdapters.SupplierTableAdapter supplierTableAdapter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnOrderFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn invID;
        private System.Windows.Forms.DataGridViewTextBoxColumn invCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn invItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn invSupDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn invDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn invLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderPrice;
        private System.Windows.Forms.Button btnOrderSupGO;
        private System.Windows.Forms.ComboBox cbxOrderSup;
    }
}