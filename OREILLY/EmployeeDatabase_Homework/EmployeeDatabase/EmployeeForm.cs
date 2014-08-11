using System;
using System.Globalization;
using System.Windows.Forms;

namespace EmployeeDatabase
{
    public partial class EmployeeForm : Form
    {
        /* Create a private class variable named _employeeInfo in the EmployeeForm 
         * for the Employee object.  Do NOT instantiate the object; you will do that 
         * later when the user clicks OK.  Note that we can’t name the variable simply 
         * _employee as that would make the accessor Employee, and we already have a 
         * class named Employee. */
        private Employee _employeeInfo;

        /* Create a read-only accessor EmployeeInfo for _employeeInfo class variable. */
        public Employee EmployeeInfo
        {
            get { return _employeeInfo; }
        }

        public EmployeeForm()
        {
            InitializeComponent();
        }

        /* Add a Click event handler for the OK button that extracts the information 
         * from the EmployeeForm and, using object initializers, creates an instance 
         * of the Employee class and assign the new class instance to the _employeeInfo 
         * class variable. */
        private void okButton_Click(object sender, EventArgs e)
        {
            string lastName = lastNameTextBox.Text;
            string firstName = firstNameTextBox.Text;
            DateTime dateOfBirth;
            DateTime.TryParse(dobMaskedTextBox.Text, out dateOfBirth);
            string address1 = addr1TextBox.Text;
            string address2 = addr2TextBox.Text;
            string city = cityTextBox.Text;
            string state = stateTextBox.Text;
            int zipCode;
            Int32.TryParse(zipMaskedTextBox.Text, out zipCode);
            string phoneNumber = phoneMaskedTextBox.Text;
            //Int32.TryParse(phoneMaskedTextBox.Text, out phoneNumber);


            _employeeInfo = new Employee()
            {
                LastName = lastName,
                FirstName = firstName,
                DateOfBirth = dateOfBirth,
                Address1 = address1,
                Address2 = address2,
                City = city,
                State = state,
                Zip = zipCode,
                PhoneNumber = phoneNumber
            };

            DialogResult = DialogResult.OK;
        }
    }

    public class Employee
    {
        private int _id;
        private string _lastName;
        private string _firstName;
        private DateTime _dateOfBirth;
        private string _address1;
        private string _address2;
        private string _city;
        private string _state;
        private int _zip;
        private string _phoneNumber;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }

        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public int Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }
    }
}
