using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Alarms
{
    public partial class MainForm : Form
    {
        public Alarms _alarms = new Alarms();

        public MainForm()
        {
            InitializeComponent();

            if (!(Directory.Exists(Path.GetDirectoryName(Alarms.LogPath))))
                Directory.CreateDirectory(Path.GetDirectoryName(Alarms.LogPath));
        }

        private void alarmTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToString("hh:mm:ss tt");
            string key = timeLabel.Text + " - Enabled";

            if (!_alarms.AlarmsDict.ContainsKey(key)) return;

            Alarm alarm = _alarms.AlarmsDict[key];
            MessageBox.Show(string.Format("{0}\n{1}", alarm.AlarmTime, alarm.AlarmCategory), "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void alarmsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtons(alarmsListBox.SelectedIndex >= 0);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AlarmForm alarmForm = new AlarmForm(_alarms.AlarmsDict);
            DialogResult result = alarmForm.ShowDialog();

            if (result != DialogResult.OK) return;

            Alarm alarm = alarmForm.Alarm;
            _alarms.Add(alarm);
            RefreshAlarmList();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            string key = alarmsListBox.SelectedItem.ToString();
            Alarm alarm = _alarms.AlarmsDict[key];

            AlarmForm alarmForm = new AlarmForm(alarm, _alarms.AlarmsDict);
            DialogResult result = alarmForm.ShowDialog();

            if (result != DialogResult.OK) return;

            Alarm newAlarm = alarmForm.Alarm;
            _alarms.Add(newAlarm);
            _alarms.AlarmsDict.Remove(key);
            RefreshAlarmList();

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string key = alarmsListBox.SelectedItem.ToString();

            DialogResult result = MessageBox.Show("Delete " + key.Split('-')[0] + " alarm?", " Delete Alarm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result != DialogResult.OK) return;

            _alarms.AlarmsDict.Remove(key);
            RefreshAlarmList();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToString("hh:mm:ss tt");
            SetButtons(alarmsListBox.SelectedIndex >= 0);
        }

        private void RefreshAlarmList()
        {
            Alarm[] alarms = _alarms.AlarmsDict.Values.OrderBy(t => t.AlarmTime).ToArray();
            object[] alarmKeys = new object[alarms.Length];

            for (int i = 0; i < alarms.Count(); i++)
            {
                alarmKeys[i] = _alarms.GenerateTimeID(alarms[i]);
            }

            alarmsListBox.Items.Clear();
            alarmsListBox.Items.AddRange(alarmKeys);
        }

        void SetButtons(bool enabled = false)
        {
            editButton.Enabled = deleteButton.Enabled = enabled;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _alarms.SaveAlarmsToFile();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _alarms.LoadAlarmsFromFile();
            RefreshAlarmList();
        }
    }
}
