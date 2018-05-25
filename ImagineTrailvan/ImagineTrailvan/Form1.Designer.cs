namespace ImagineTrailvan
{
    partial class frmSwitchboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSwitchboard));
            this.button1 = new System.Windows.Forms.Button();
            this.linkStock = new System.Windows.Forms.LinkLabel();
            this.linkFibreglass = new System.Windows.Forms.LinkLabel();
            this.linkAdminDev = new System.Windows.Forms.LinkLabel();
            this.linkRepairService = new System.Windows.Forms.LinkLabel();
            this.linkQuoteInvoice = new System.Windows.Forms.LinkLabel();
            this.lblStockHandling = new System.Windows.Forms.Label();
            this.lblRepairService = new System.Windows.Forms.Label();
            this.lblQuoteInvoice = new System.Windows.Forms.Label();
            this.lblFibreglassHandling = new System.Windows.Forms.Label();
            this.lblAdminDev = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(876, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Log Out";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // linkStock
            // 
            this.linkStock.AutoSize = true;
            this.linkStock.BackColor = System.Drawing.Color.Transparent;
            this.linkStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkStock.Location = new System.Drawing.Point(12, 128);
            this.linkStock.Name = "linkStock";
            this.linkStock.Size = new System.Drawing.Size(221, 26);
            this.linkStock.TabIndex = 1;
            this.linkStock.TabStop = true;
            this.linkStock.Text = "STOCK HANDLING";
            this.linkStock.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkStock_LinkClicked);
            // 
            // linkFibreglass
            // 
            this.linkFibreglass.AutoSize = true;
            this.linkFibreglass.BackColor = System.Drawing.Color.Transparent;
            this.linkFibreglass.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkFibreglass.Location = new System.Drawing.Point(12, 323);
            this.linkFibreglass.Name = "linkFibreglass";
            this.linkFibreglass.Size = new System.Drawing.Size(290, 26);
            this.linkFibreglass.TabIndex = 2;
            this.linkFibreglass.TabStop = true;
            this.linkFibreglass.Text = "FIBREGLASS HANDLING";
            // 
            // linkAdminDev
            // 
            this.linkAdminDev.AutoSize = true;
            this.linkAdminDev.BackColor = System.Drawing.Color.Transparent;
            this.linkAdminDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkAdminDev.Location = new System.Drawing.Point(12, 388);
            this.linkAdminDev.Name = "linkAdminDev";
            this.linkAdminDev.Size = new System.Drawing.Size(453, 26);
            this.linkAdminDev.TabIndex = 3;
            this.linkAdminDev.TabStop = true;
            this.linkAdminDev.Text = "ADMINISTRATION AND DEVELOPMENT";
            // 
            // linkRepairService
            // 
            this.linkRepairService.AutoSize = true;
            this.linkRepairService.BackColor = System.Drawing.Color.Transparent;
            this.linkRepairService.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkRepairService.Location = new System.Drawing.Point(12, 193);
            this.linkRepairService.Name = "linkRepairService";
            this.linkRepairService.Size = new System.Drawing.Size(302, 26);
            this.linkRepairService.TabIndex = 4;
            this.linkRepairService.TabStop = true;
            this.linkRepairService.Text = "REPAIRS AND SERVICES";
            // 
            // linkQuoteInvoice
            // 
            this.linkQuoteInvoice.AutoSize = true;
            this.linkQuoteInvoice.BackColor = System.Drawing.Color.Transparent;
            this.linkQuoteInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkQuoteInvoice.Location = new System.Drawing.Point(12, 258);
            this.linkQuoteInvoice.Name = "linkQuoteInvoice";
            this.linkQuoteInvoice.Size = new System.Drawing.Size(343, 26);
            this.linkQuoteInvoice.TabIndex = 5;
            this.linkQuoteInvoice.TabStop = true;
            this.linkQuoteInvoice.Text = "QUOTATIONS AND INVOICES";
            // 
            // lblStockHandling
            // 
            this.lblStockHandling.AutoSize = true;
            this.lblStockHandling.BackColor = System.Drawing.Color.Transparent;
            this.lblStockHandling.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockHandling.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStockHandling.Location = new System.Drawing.Point(481, 128);
            this.lblStockHandling.Name = "lblStockHandling";
            this.lblStockHandling.Size = new System.Drawing.Size(435, 39);
            this.lblStockHandling.TabIndex = 6;
            this.lblStockHandling.Text = "Manage stock in and out, Display low stock, View inventory on hand value,\r\nRecord" +
    " invoices received, Create orders to suppliers for stock,\r\nView and update suppl" +
    "ier\'s details";
            // 
            // lblRepairService
            // 
            this.lblRepairService.AutoSize = true;
            this.lblRepairService.BackColor = System.Drawing.Color.Transparent;
            this.lblRepairService.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepairService.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRepairService.Location = new System.Drawing.Point(481, 193);
            this.lblRepairService.Name = "lblRepairService";
            this.lblRepairService.Size = new System.Drawing.Size(281, 13);
            this.lblRepairService.TabIndex = 7;
            this.lblRepairService.Text = "Manage and record repairs and services booked";
            // 
            // lblQuoteInvoice
            // 
            this.lblQuoteInvoice.AutoSize = true;
            this.lblQuoteInvoice.BackColor = System.Drawing.Color.Transparent;
            this.lblQuoteInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuoteInvoice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblQuoteInvoice.Location = new System.Drawing.Point(481, 258);
            this.lblQuoteInvoice.Name = "lblQuoteInvoice";
            this.lblQuoteInvoice.Size = new System.Drawing.Size(316, 39);
            this.lblQuoteInvoice.TabIndex = 8;
            this.lblQuoteInvoice.Text = "Manage and create invoices and quotations to clients,\r\nManage and update pricelis" +
    "ts,\r\nView and update delivery schedule";
            // 
            // lblFibreglassHandling
            // 
            this.lblFibreglassHandling.AutoSize = true;
            this.lblFibreglassHandling.BackColor = System.Drawing.Color.Transparent;
            this.lblFibreglassHandling.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFibreglassHandling.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFibreglassHandling.Location = new System.Drawing.Point(481, 323);
            this.lblFibreglassHandling.Name = "lblFibreglassHandling";
            this.lblFibreglassHandling.Size = new System.Drawing.Size(286, 52);
            this.lblFibreglassHandling.TabIndex = 9;
            this.lblFibreglassHandling.Text = "Manage fibreglass costing,\r\nCreate and record fibreglass orders and invoices,\r\nVi" +
    "ew and update fibreglass reconciliation,\r\nRecord and manage Atlin Chemicals\' inv" +
    "oices";
            // 
            // lblAdminDev
            // 
            this.lblAdminDev.AutoSize = true;
            this.lblAdminDev.BackColor = System.Drawing.Color.Transparent;
            this.lblAdminDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminDev.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAdminDev.Location = new System.Drawing.Point(481, 388);
            this.lblAdminDev.Name = "lblAdminDev";
            this.lblAdminDev.Size = new System.Drawing.Size(189, 52);
            this.lblAdminDev.TabIndex = 10;
            this.lblAdminDev.Text = "Manage permissions on users,\r\nManage development issues,\r\nDebug and resolve issue" +
    "s,\r\nEnhance user-based experience";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(17, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(383, 124);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // frmSwitchboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1043, 462);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblAdminDev);
            this.Controls.Add(this.lblFibreglassHandling);
            this.Controls.Add(this.lblQuoteInvoice);
            this.Controls.Add(this.lblRepairService);
            this.Controls.Add(this.lblStockHandling);
            this.Controls.Add(this.linkQuoteInvoice);
            this.Controls.Add(this.linkRepairService);
            this.Controls.Add(this.linkAdminDev);
            this.Controls.Add(this.linkFibreglass);
            this.Controls.Add(this.linkStock);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Name = "frmSwitchboard";
            this.Text = "Switchboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSwitchboard_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkStock;
        private System.Windows.Forms.LinkLabel linkFibreglass;
        private System.Windows.Forms.LinkLabel linkAdminDev;
        private System.Windows.Forms.LinkLabel linkRepairService;
        private System.Windows.Forms.LinkLabel linkQuoteInvoice;
        private System.Windows.Forms.Label lblStockHandling;
        private System.Windows.Forms.Label lblRepairService;
        private System.Windows.Forms.Label lblQuoteInvoice;
        private System.Windows.Forms.Label lblFibreglassHandling;
        private System.Windows.Forms.Label lblAdminDev;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

