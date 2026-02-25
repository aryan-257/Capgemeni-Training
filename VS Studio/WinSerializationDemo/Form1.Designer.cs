namespace WinSerializationDemo
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
            this.id = new System.Windows.Forms.Label();
            this.txtEmployeeID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.Label();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.salary = new System.Windows.Forms.Label();
            this.binSerialize = new System.Windows.Forms.Button();
            this.binDeSerialize = new System.Windows.Forms.Button();
            this.xmlSerialization = new System.Windows.Forms.Button();
            this.xmlDeSerialize = new System.Windows.Forms.Button();
            this.soapSerialize = new System.Windows.Forms.Button();
            this.soapDeSerialize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // id
            // 
            this.id.AutoSize = true;
            this.id.Font = new System.Drawing.Font("Microsoft Uighur", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.id.Location = new System.Drawing.Point(99, 101);
            this.id.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(126, 34);
            this.id.TabIndex = 0;
            this.id.Text = "Employee ID";
            this.id.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtEmployeeID
            // 
            this.txtEmployeeID.Font = new System.Drawing.Font("Microsoft Uighur", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeID.Location = new System.Drawing.Point(232, 101);
            this.txtEmployeeID.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtEmployeeID.Name = "txtEmployeeID";
            this.txtEmployeeID.Size = new System.Drawing.Size(136, 37);
            this.txtEmployeeID.TabIndex = 1;
            this.txtEmployeeID.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Uighur", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(232, 150);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(136, 37);
            this.txtName.TabIndex = 3;
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Microsoft Uighur", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(99, 153);
            this.name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(65, 34);
            this.name.TabIndex = 2;
            this.name.Text = "Name";
            // 
            // txtSalary
            // 
            this.txtSalary.Font = new System.Drawing.Font("Microsoft Uighur", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalary.Location = new System.Drawing.Point(232, 199);
            this.txtSalary.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Size = new System.Drawing.Size(136, 37);
            this.txtSalary.TabIndex = 5;
            // 
            // salary
            // 
            this.salary.AutoSize = true;
            this.salary.Font = new System.Drawing.Font("Microsoft Uighur", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salary.Location = new System.Drawing.Point(99, 199);
            this.salary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.salary.Name = "salary";
            this.salary.Size = new System.Drawing.Size(68, 34);
            this.salary.TabIndex = 4;
            this.salary.Text = "Salary";
            // 
            // binSerialize
            // 
            this.binSerialize.Font = new System.Drawing.Font("Microsoft Uighur", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.binSerialize.Location = new System.Drawing.Point(124, 306);
            this.binSerialize.Name = "binSerialize";
            this.binSerialize.Size = new System.Drawing.Size(124, 46);
            this.binSerialize.TabIndex = 6;
            this.binSerialize.Text = "Bin Serialize";
            this.binSerialize.UseVisualStyleBackColor = true;
            this.binSerialize.Click += new System.EventHandler(this.binSerialize_Click);
            // 
            // binDeSerialize
            // 
            this.binDeSerialize.Font = new System.Drawing.Font("Microsoft Uighur", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.binDeSerialize.Location = new System.Drawing.Point(124, 381);
            this.binDeSerialize.Name = "binDeSerialize";
            this.binDeSerialize.Size = new System.Drawing.Size(124, 46);
            this.binDeSerialize.TabIndex = 7;
            this.binDeSerialize.Text = "Bin DeSerialize";
            this.binDeSerialize.UseVisualStyleBackColor = true;
            this.binDeSerialize.Click += new System.EventHandler(this.binDeSerialize_Click);
            // 
            // xmlSerialization
            // 
            this.xmlSerialization.Font = new System.Drawing.Font("Microsoft Uighur", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmlSerialization.Location = new System.Drawing.Point(253, 306);
            this.xmlSerialization.Name = "xmlSerialization";
            this.xmlSerialization.Size = new System.Drawing.Size(124, 46);
            this.xmlSerialization.TabIndex = 8;
            this.xmlSerialization.Text = "XML Serialize";
            this.xmlSerialization.UseVisualStyleBackColor = true;
            this.xmlSerialization.Click += new System.EventHandler(this.xmlSerialization_Click);
            // 
            // xmlDeSerialize
            // 
            this.xmlDeSerialize.Font = new System.Drawing.Font("Microsoft Uighur", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmlDeSerialize.Location = new System.Drawing.Point(253, 381);
            this.xmlDeSerialize.Name = "xmlDeSerialize";
            this.xmlDeSerialize.Size = new System.Drawing.Size(124, 46);
            this.xmlDeSerialize.TabIndex = 11;
            this.xmlDeSerialize.Text = "XML DeSerialize";
            this.xmlDeSerialize.UseVisualStyleBackColor = true;
            this.xmlDeSerialize.Click += new System.EventHandler(this.xmlDeSerialize_Click);
            // 
            // soapSerialize
            // 
            this.soapSerialize.Font = new System.Drawing.Font("Microsoft Uighur", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soapSerialize.Location = new System.Drawing.Point(383, 306);
            this.soapSerialize.Name = "soapSerialize";
            this.soapSerialize.Size = new System.Drawing.Size(124, 46);
            this.soapSerialize.TabIndex = 10;
            this.soapSerialize.Text = "Soap Serialize";
            this.soapSerialize.UseVisualStyleBackColor = true;
            this.soapSerialize.Click += new System.EventHandler(this.soapSerialize_Click);
            // 
            // soapDeSerialize
            // 
            this.soapDeSerialize.Font = new System.Drawing.Font("Microsoft Uighur", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soapDeSerialize.Location = new System.Drawing.Point(383, 381);
            this.soapDeSerialize.Name = "soapDeSerialize";
            this.soapDeSerialize.Size = new System.Drawing.Size(124, 46);
            this.soapDeSerialize.TabIndex = 9;
            this.soapDeSerialize.Text = "Soap Deserialize";
            this.soapDeSerialize.UseVisualStyleBackColor = true;
            this.soapDeSerialize.Click += new System.EventHandler(this.soapDeSerialize_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 34F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 956);
            this.Controls.Add(this.xmlDeSerialize);
            this.Controls.Add(this.soapSerialize);
            this.Controls.Add(this.soapDeSerialize);
            this.Controls.Add(this.xmlSerialization);
            this.Controls.Add(this.binDeSerialize);
            this.Controls.Add(this.binSerialize);
            this.Controls.Add(this.txtSalary);
            this.Controls.Add(this.salary);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.name);
            this.Controls.Add(this.txtEmployeeID);
            this.Controls.Add(this.id);
            this.Font = new System.Drawing.Font("Microsoft Uighur", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label id;
        private System.Windows.Forms.TextBox txtEmployeeID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.TextBox txtSalary;
        private System.Windows.Forms.Label salary;
        private System.Windows.Forms.Button binSerialize;
        private System.Windows.Forms.Button binDeSerialize;
        private System.Windows.Forms.Button xmlSerialization;
        private System.Windows.Forms.Button xmlDeSerialize;
        private System.Windows.Forms.Button soapSerialize;
        private System.Windows.Forms.Button soapDeSerialize;
    }
}

