using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinDiscArchDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnShowAllProd_Click(object sender, EventArgs e)
        {
            ProductUtility prodObj = new ProductUtility();

            dataGridView1.DataSource = prodObj.ShowAll();
        }
    }
}
