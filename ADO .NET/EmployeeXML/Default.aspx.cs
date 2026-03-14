using System;
using System.Linq;
using System.Xml.Linq;

namespace EmployeeXML
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XDocument xdoc = XDocument.Parse(@"
<Employees>
<Employee>
<Name>Thomas</Name>
<Designation>Executive</Designation>
<Department>Accounts</Department>
<Salary>5000</Salary>
</Employee>

<Employee>
<Name>Wills</Name>
<Designation>Manager</Designation>
<Department>Accounts</Department>
<Salary>24000</Salary>
</Employee>

<Employee>
<Name>Brod</Name>
<Designation>Manager</Designation>
<Department>Finance</Department>
<Salary>28000</Salary>
</Employee>

<Employee>
<Name>Smith</Name>
<Designation>Analyst</Designation>
<Department>Finance</Department>
<Salary>21000</Salary>
</Employee>
</Employees>
");

            var res = from emp in xdoc.Root.Elements("Employee")
                      where emp.Element("Department").Value == "Finance"
                      && Convert.ToInt32(emp.Element("Salary").Value) > 25000
                      select new
                      {
                          EmployeeName = emp.Element("Name").Value,
                          Department = emp.Element("Department").Value,
                          Salary = emp.Element("Salary").Value
                      };

            GridView1.DataSource = res;
            GridView1.DataBind();
        }
    }
}