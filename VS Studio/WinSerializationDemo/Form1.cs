using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//For Binary Serialization
using System.Runtime.Serialization.Formatters.Binary;
//For XML Serialization
using System.Xml.Serialization;
//For Soap Serialization
using System.Runtime.Serialization.Formatters.Soap;

namespace WinSerializationDemo
{
    
    public partial class Form1 : Form
    {

        public void ClearAllTextBoxes()
        {
            foreach (Control item in this.Controls)
            {
                if (item.GetType() == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)item;
                    textBox.Clear();
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// For Binary Serialization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void binSerialize_Click(object sender, EventArgs e)
        {
            Employee emp1 = new Employee();
            
            emp1.ID = Convert.ToInt32(txtEmployeeID.Text);
            emp1.Name = txtName.Text;
            emp1.Salary = Convert.ToInt32(txtSalary.Text);

            //Binary Serialization Code Below
            FileStream fs = new FileStream(@"D:\Capgemini\BinSerializable.bin",FileMode.OpenOrCreate,FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, emp1);

            ClearAllTextBoxes();

            fs.Close();
            MessageBox.Show("Recoed Added");

        }

        private void binDeSerialize_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"D:\Capgemini\BinSerializable.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();

            Employee emp1 = (Employee)bf.Deserialize(fs);

            txtEmployeeID.Text = emp1.ID.ToString();
            txtName.Text = emp1.Name;
            txtSalary.Text = emp1.Salary.ToString();

            fs.Close();
        }

        /// <summary>
        /// For XML Serialization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xmlSerialization_Click(object sender, EventArgs e)
        {
            Employee emp1 = new Employee();

            emp1.ID = Convert.ToInt32(txtEmployeeID.Text);
            emp1.Name = txtName.Text;
            emp1.Salary = Convert.ToInt32(txtSalary.Text);

            //XML Serialization Code Below
            FileStream fs = new FileStream(@"D:\Capgemini\XMLSerializable.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Employee));
            xs.Serialize(fs, emp1);

            ClearAllTextBoxes();

            fs.Close();
            MessageBox.Show("Recoed Added");
        }

        private void xmlDeSerialize_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"D:\Capgemini\XMLSerializable.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Employee));

            Employee emp1 = (Employee)xs.Deserialize(fs);

            txtEmployeeID.Text = emp1.ID.ToString();
            txtName.Text = emp1.Name;
            txtSalary.Text = emp1.Salary.ToString();

            fs.Close();
        }

        /// <summary>
        /// Soap Serialization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void soapSerialize_Click(object sender, EventArgs e)
        {
            Employee emp1 = new Employee();

            emp1.ID = Convert.ToInt32(txtEmployeeID.Text);
            emp1.Name = txtName.Text;
            emp1.Salary = Convert.ToInt32(txtSalary.Text);

            //Soap Serialization Code Below
            FileStream fs = new FileStream(@"D:\Capgemini\SoapSerializable.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            SoapFormatter sf = new SoapFormatter();
            sf.Serialize(fs, emp1);

            ClearAllTextBoxes();

            fs.Close();
            MessageBox.Show("Recoed Added");

        }

        private void soapDeSerialize_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"D:\Capgemini\SoapSerializable.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            SoapFormatter sf = new SoapFormatter();

            Employee emp1 = (Employee)sf.Deserialize(fs);

            txtEmployeeID.Text = emp1.ID.ToString();
            txtName.Text = emp1.Name;
            txtSalary.Text = emp1.Salary.ToString();

            fs.Close();

        }
    }
}
