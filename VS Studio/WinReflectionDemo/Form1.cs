using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinReflectionDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void loadAssembly_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string FileName = openFileDialog1.FileName;
            
            Assembly assemblyObj = Assembly.LoadFrom(FileName);

            Type[] myType = assemblyObj.GetTypes();
            this.ReflectAll(myType[0]);
        }

        public void ReflectAll(Type typeObj)
        {
            //Getting All Methods
            MethodInfo[] methodList = typeObj.GetMethods();

            //Getting All Properties
            PropertyInfo[] propList = typeObj.GetProperties();

            //Load All Properties
            foreach (var item in propList)
            {
                listBox1.Items.Add(item);
            }

            foreach (var item in methodList)
            {
                listBox2.Items.Add(item);
            }

        }

        private void clearAll_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }
    }
}
