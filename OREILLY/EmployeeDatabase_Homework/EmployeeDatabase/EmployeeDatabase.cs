using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EmployeeDatabase
{
    public partial class EmployeeDatabase : Form
    {
        private static List<Employee> _employees = new List<Employee>();
        private int _employeeID = 0;

        public EmployeeDatabase()
        {
            InitializeComponent();
            removeButton.Enabled = false;
        }

        /* Add a method named displayEmployees() that will loop through the _employees 
         * Employee list , displaying the first name, last name, and id for each employee 
         * I n the List in the employeesListBox, and updating the employeeCountValueLabel 
         * with the total number of employees. */
        private void displayEmployees()
        {
            string empInfo = "{0}. {1} {2}";
            employeesListBox.Items.Clear();

            foreach (var employee in _employees)
            {
                employeesListBox.Items.Add(string.Format(empInfo, employee.Id, employee.FirstName, employee.LastName));
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            /* Add code to the addButton Click event to instatiate and display the 
             * EmployeeForm as a modal dialog box. */
            EmployeeForm employeeForm = new EmployeeForm();
            DialogResult result = employeeForm.ShowDialog();

            /* Add code to the addButton Click event to process the DialogResult.OK 
             * return result, extracting the Employee information from the EmployeeForm, 
             * updating the employee ID in the Employee information, adding the Employee 
             * information to the _employees list, and calling the displayEmployees() 
             * method. */
            if (result == DialogResult.OK)
            {
                _employeeID++;
                Employee empInfo = employeeForm.EmployeeInfo;
                empInfo.Id = _employeeID;
                _employees.Add(empInfo);
                displayEmployees();
            }
        }


        /* Add a button control named removeButton to the EmployeeDatabase.cs Form that 
         * will only be enabled when an employee is selected in the ListBox. You use the 
         * Enabled property of a Button to enable or disable the Button control on the 
         * form, and can set this property to False initially.  To enable the Button, 
         * handle the ListBox SelectedIndexChanged event, setting the Enabled property 
         * to True or False depeding on whether the ListBox SelectedIndex is >= 0.  When 
         * the removeButton is clicked, display a Yes/No Message Control, confirming that 
         * the user wants to delete the employee.  If they confirm, remove the selected 
         * employee from the _employees List, and call the dipslayEmployees() method to 
         * update the ListBox display.  You can use the ListBox SelectedIndex property 
         * to determine which entry in the ListBox was selected, and delete that item 
         * from the _employees List using the List method RemoveAt and the ListBox 
         * SelectedIndex as the argurment.
         */
        private void removeButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deletion Confirmation", "Delete the employee?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _employees.RemoveAt(employeesListBox.SelectedIndex);
                displayEmployees();
            }
            removeButton.Enabled = false;
        }

        private void employeesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeButton.Enabled = employeesListBox.SelectedIndex >= 0;
        }
    }
}
