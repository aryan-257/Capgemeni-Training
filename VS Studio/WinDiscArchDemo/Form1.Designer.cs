namespace WinDiscArchDemo
{
    partial class Form1
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
            this.txtProdID = new System.Windows.Forms.TextBox();
            this.lblProdID = new System.Windows.Forms.Label();
            this.lblProdName = new System.Windows.Forms.Label();
            this.txtProdName = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.btnAddProd = new System.Windows.Forms.Button();
            this.lblDsc = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.btnUpdateProd = new System.Windows.Forms.Button();
            this.btnSearchByID = new System.Windows.Forms.Button();
            this.btnDeleteProd = new System.Windows.Forms.Button();
            this.btnShowAllProd = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtProdID
            // 
            this.txtProdID.Location = new System.Drawing.Point(242, 60);
            this.txtProdID.Name = "txtProdID";
            this.txtProdID.Size = new System.Drawing.Size(133, 22);
            this.txtProdID.TabIndex = 0;
            // 
            // lblProdID
            // 
            this.lblProdID.AutoSize = true;
            this.lblProdID.Location = new System.Drawing.Point(167, 63);
            this.lblProdID.Name = "lblProdID";
            this.lblProdID.Size = new System.Drawing.Size(69, 16);
            this.lblProdID.TabIndex = 1;
            this.lblProdID.Text = "Product ID";
            this.lblProdID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProdName
            // 
            this.lblProdName.AutoSize = true;
            this.lblProdName.Location = new System.Drawing.Point(143, 106);
            this.lblProdName.Name = "lblProdName";
            this.lblProdName.Size = new System.Drawing.Size(93, 16);
            this.lblProdName.TabIndex = 3;
            this.lblProdName.Text = "Product Name";
            // 
            // txtProdName
            // 
            this.txtProdName.Location = new System.Drawing.Point(242, 103);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Size = new System.Drawing.Size(133, 22);
            this.txtProdName.TabIndex = 2;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(198, 148);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(38, 16);
            this.lblPrice.TabIndex = 5;
            this.lblPrice.Text = "Price";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(242, 145);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(133, 22);
            this.txtPrice.TabIndex = 4;
            // 
            // btnAddProd
            // 
            this.btnAddProd.Location = new System.Drawing.Point(117, 301);
            this.btnAddProd.Name = "btnAddProd";
            this.btnAddProd.Size = new System.Drawing.Size(127, 42);
            this.btnAddProd.TabIndex = 6;
            this.btnAddProd.Text = "Add Product";
            this.btnAddProd.UseVisualStyleBackColor = true;
            // 
            // lblDsc
            // 
            this.lblDsc.AutoSize = true;
            this.lblDsc.Location = new System.Drawing.Point(161, 196);
            this.lblDsc.Name = "lblDsc";
            this.lblDsc.Size = new System.Drawing.Size(75, 16);
            this.lblDsc.TabIndex = 8;
            this.lblDsc.Text = "Description";
            this.lblDsc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(242, 193);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(144, 77);
            this.txtDesc.TabIndex = 7;
            // 
            // btnUpdateProd
            // 
            this.btnUpdateProd.Location = new System.Drawing.Point(248, 301);
            this.btnUpdateProd.Name = "btnUpdateProd";
            this.btnUpdateProd.Size = new System.Drawing.Size(127, 42);
            this.btnUpdateProd.TabIndex = 9;
            this.btnUpdateProd.Text = "Update Product";
            this.btnUpdateProd.UseVisualStyleBackColor = true;
            // 
            // btnSearchByID
            // 
            this.btnSearchByID.Location = new System.Drawing.Point(381, 301);
            this.btnSearchByID.Name = "btnSearchByID";
            this.btnSearchByID.Size = new System.Drawing.Size(127, 42);
            this.btnSearchByID.TabIndex = 10;
            this.btnSearchByID.Text = "Search By ID";
            this.btnSearchByID.UseVisualStyleBackColor = true;
            // 
            // btnDeleteProd
            // 
            this.btnDeleteProd.Location = new System.Drawing.Point(117, 374);
            this.btnDeleteProd.Name = "btnDeleteProd";
            this.btnDeleteProd.Size = new System.Drawing.Size(127, 42);
            this.btnDeleteProd.TabIndex = 11;
            this.btnDeleteProd.Text = "Delete Product";
            this.btnDeleteProd.UseVisualStyleBackColor = true;
            // 
            // btnShowAllProd
            // 
            this.btnShowAllProd.Location = new System.Drawing.Point(248, 374);
            this.btnShowAllProd.Name = "btnShowAllProd";
            this.btnShowAllProd.Size = new System.Drawing.Size(127, 42);
            this.btnShowAllProd.TabIndex = 12;
            this.btnShowAllProd.Text = "Show All Products";
            this.btnShowAllProd.UseVisualStyleBackColor = true;
            this.btnShowAllProd.Click += new System.EventHandler(this.btnShowAllProd_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(431, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(357, 231);
            this.dataGridView1.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnShowAllProd);
            this.Controls.Add(this.btnDeleteProd);
            this.Controls.Add(this.btnSearchByID);
            this.Controls.Add(this.btnUpdateProd);
            this.Controls.Add(this.lblDsc);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.btnAddProd);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblProdName);
            this.Controls.Add(this.txtProdName);
            this.Controls.Add(this.lblProdID);
            this.Controls.Add(this.txtProdID);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProdID;
        private System.Windows.Forms.Label lblProdID;
        private System.Windows.Forms.Label lblProdName;
        private System.Windows.Forms.TextBox txtProdName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Button btnAddProd;
        private System.Windows.Forms.Label lblDsc;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Button btnUpdateProd;
        private System.Windows.Forms.Button btnSearchByID;
        private System.Windows.Forms.Button btnDeleteProd;
        private System.Windows.Forms.Button btnShowAllProd;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

