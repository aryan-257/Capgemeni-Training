using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private IProductRepo productRepo;

        public Form1()
        {
            InitializeComponent();
            productRepo = new ProductUtility();
            
            // Wire up event handlers
            btnAddProduct.Click += btnAddProduct_Click;
            btnUpdateProduct.Click += btnUpdateProduct_Click;
            btnDeleteProduct.Click += btnDeleteProduct_Click;
            btnSearchByID.Click += btnSearchByID_Click;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllProducts();
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
        }

        private void LoadAllProducts()
        {
            try
            {
                var utility = productRepo as ProductUtility;
                if (utility != null)
                {
                    var products = utility.GetAllProducts();
                    dataGridView1.DataSource = products;
                }
            }
            catch (MyCustomException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    Product newProduct = new Product
                    {
                        ProdID = int.Parse(txtProdID.Text),
                        ProdName = txtProdName.Text,
                        Price = int.Parse(txtPrice.Text),
                        Description = txtDesc.Text
                    };

                    bool success = productRepo.AddData(newProduct);

                    if (success)
                    {
                        MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadAllProducts();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (MyCustomException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProdID.Text))
                {
                    MessageBox.Show("Please enter Product ID to update.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ValidateInputs())
                {
                    int prodID = int.Parse(txtProdID.Text);

                    Product updatedProduct = new Product
                    {
                        ProdID = prodID,
                        ProdName = txtProdName.Text,
                        Price = int.Parse(txtPrice.Text),
                        Description = txtDesc.Text
                    };

                    bool success = productRepo.UpdateData(prodID, updatedProduct);

                    if (success)
                    {
                        MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadAllProducts();
                    }
                    else
                    {
                        MessageBox.Show("Product not found or update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (MyCustomException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProdID.Text))
                {
                    MessageBox.Show("Please enter Product ID to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int prodID = int.Parse(txtProdID.Text);

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete product with ID {prodID}?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool success = productRepo.DeleteData(prodID);

                    if (success)
                    {
                        MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadAllProducts();
                    }
                    else
                    {
                        MessageBox.Show("Product not found or delete failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchByID_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProdID.Text))
                {
                    MessageBox.Show("Please enter Product ID to search.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int prodID = int.Parse(txtProdID.Text);
                Product product = productRepo.SearchByID(prodID);

                if (product != null)
                {
                    txtProdName.Text = product.ProdName;
                    txtPrice.Text = product.Price.ToString();
                    txtDesc.Text = product.Description;

                    MessageBox.Show("Product found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Product with ID {prodID} not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAllProduct_Click(object sender, EventArgs e)
        {
            LoadAllProducts();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    
                    txtProdID.Text = row.Cells["ProdID"].Value?.ToString() ?? "";
                    txtProdName.Text = row.Cells["ProdName"].Value?.ToString() ?? "";
                    txtPrice.Text = row.Cells["Price"].Value?.ToString() ?? "";
                    txtDesc.Text = row.Cells["Description"].Value?.ToString() ?? "";
                }
            }
            catch
            {
                // Silently handle selection errors
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtProdID.Text))
            {
                MessageBox.Show("Product ID is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProdID.Focus();
                return false;
            }

            if (!int.TryParse(txtProdID.Text, out int prodID))
            {
                MessageBox.Show("Product ID must be a number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProdID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProdName.Text))
            {
                MessageBox.Show("Product Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProdName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Price is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (!int.TryParse(txtPrice.Text, out int price))
            {
                MessageBox.Show("Price must be a number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            txtProdID.Clear();
            txtProdName.Clear();
            txtPrice.Clear();
            txtDesc.Clear();
            txtProdID.Focus();
        }

        // Keep existing empty event handlers for compatibility
        private void txtProdName_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}
