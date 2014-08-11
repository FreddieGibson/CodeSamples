using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Alarms
{
    public partial class AlarmForm : Form
    {
        private static readonly string[] _categories = {"Personal", "Business"};
        private static Dictionary<string, Alarm> _existingAlarms;

        public AlarmForm(Dictionary<string, Alarm> dict)
        {
            InitializeComponent();
            _existingAlarms = dict;
            categoryComboBox.Items.AddRange(_categories);
            categoryComboBox.SelectedIndex = 0;
            warningLabel.Visible = false;
            warningLabel.Text = "Existing alarm";
        }

        public AlarmForm(Alarm alarm, Dictionary<string, Alarm> dict)
        {
            InitializeComponent();
            _existingAlarms = dict;
            categoryComboBox.Items.AddRange(_categories);
            categoryComboBox.SelectedIndex = (categoryComboBox.Items).IndexOf(alarm.AlarmCategory);
            warningLabel.Visible = false;
            warningLabel.Text = "Existing Alarm";

            hourNumericUpDown.Value = alarm.AlarmTime.Hour;
            minuteNumericUpDown.Value = alarm.AlarmTime.Minute;
            enabledCheckBox.Checked = alarm.Enabled;
        }

        public Alarm _alarm;
        public Alarm Alarm 
        {
            get { return _alarm; } 
            set { _alarm = value; }
        }

        public List<Alarm> _alarms;
        private List<Alarm> Alarms 
        {
            get { return _alarms; }
            set { _alarms = value; }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            int hourValue;
            int minuteValue;

            if (Int32.TryParse(hourNumericUpDown.Value.ToString(CultureInfo.InvariantCulture), out hourValue) &&
                Int32.TryParse(minuteNumericUpDown.Value.ToString(CultureInfo.InvariantCulture), out minuteValue))
            {
                DateTime date = DateTime.Today;
                Alarm = new Alarm
                {
                    AlarmTime = new DateTime(date.Year, date.Month, date.Day, hourValue, minuteValue, 0),
                    Enabled = enabledCheckBox.Checked,
                    AlarmCategory = categoryComboBox.SelectedItem.ToString()
                };
            }
            
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void AlarmForm_Shown(object sender, EventArgs e)
        {
            hourNumericUpDown.Minimum = minuteNumericUpDown.Minimum = 0;
            hourNumericUpDown.Maximum = 24;
            minuteNumericUpDown.Maximum = 59;
        }

        private void hourNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CheckForExistingAlarm();
        }

        private void minuteNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CheckForExistingAlarm();
        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckForExistingAlarm();
        }

        private void enabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckForExistingAlarm();
        }

        private void CheckForExistingAlarm()
        {
            int hourValue;
            int minuteValue;

            if (!Int32.TryParse(hourNumericUpDown.Value.ToString(CultureInfo.InvariantCulture), out hourValue) ||
                !Int32.TryParse(minuteNumericUpDown.Value.ToString(CultureInfo.InvariantCulture), out minuteValue))
            {
                warningLabel.Visible = false;
                return;
            }

            DateTime date = DateTime.Today;
            Alarm alarm = new Alarm
            {
                AlarmTime = new DateTime(date.Year, date.Month, date.Day, hourValue, minuteValue, 0),
                Enabled = enabledCheckBox.Checked,
                AlarmCategory = categoryComboBox.SelectedItem.ToString()
            };



            warningLabel.Visible = (_existingAlarms.Values.Any(value => value.Enabled == alarm.Enabled &&
                                                                        value.AlarmTime.Hour == alarm.AlarmTime.Hour &&
                                                                        value.AlarmTime.Minute == alarm.AlarmTime.Minute &&
                                                                        value.AlarmCategory == alarm.AlarmCategory));
        }
    }
}
