using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Alarms
{
    public class Alarms
    {
        // Creating txt file next to exe within bin folder.
        private static readonly string _srcPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static readonly string LogPath = Path.Combine(_srcPath, @"app_data\alarms.txt");

        // Dictionary used to store alarms.
        public Dictionary<string, Alarm> AlarmsDict = new Dictionary<string, Alarm>();

        // Default Constructor
        public Alarms() { }

        // Method used to create unique id for alarm
        public string GenerateTimeID(Alarm alarm)
        {
            return string.Format("{0} - {1}", alarm.AlarmTime.ToString("hh:mm:ss tt"), alarm.Enabled ? "Enabled" : "Disabled");
        }
    
        // Method used to add new alarms to the collection
        public void Add(Alarm alarm)
        {
            AlarmsDict.Add(GenerateTimeID(alarm), alarm);
        }

        // Method used to return the number of alarms in collection
        public int Count()
        {
            return AlarmsDict.Count;
        }

        // Method used to remove an alarm from the collection
        public void Delete(string alarmKey)
        {
            AlarmsDict.Remove(alarmKey);
        }

        // Method used to get alarm from collection
        public Alarm GetAlarm(string alarmKey)
        {
            Alarm alarm;
            return !AlarmsDict.TryGetValue(alarmKey, out alarm) ? null : alarm;
        }

        // Method used to return next alarm enabled for the day
        public Alarm GetNextDue()
        {
            DateTime currentTime = DateTime.Now;
            Alarm[] alarms = AlarmsDict.Values.OrderBy(t => t.AlarmTime).ToArray();
            return alarms.FirstOrDefault(alarm => alarm.AlarmTime > currentTime && alarm.Enabled);
        }

        // Loads alarms from text file saved on disk
        public void LoadAlarmsFromFile()
        {
            if (!File.Exists(LogPath)) return;
            
            using (StreamReader reader = new StreamReader(LogPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] logArray = line.Split(',');
                    DateTime alarmTime;
                    Boolean enabled;
                    string category = logArray[1];
                    

                    if (!DateTime.TryParse(logArray[0], out alarmTime) || !Boolean.TryParse(logArray[2], out enabled)) continue;

                    Add(new Alarm { AlarmCategory = category, AlarmTime = alarmTime, Enabled = enabled });
                }
            }
        }

        // Saves alarms to csv on disk
        public void SaveAlarmsToFile()
        {
            const string logInfo = "{0},{1},{2}";

            using (StreamWriter writer = new StreamWriter(LogPath))
            {
                Alarm[] alarms = AlarmsDict.Values.OrderBy(t => t.AlarmTime).ToArray();
                foreach (Alarm alarm in alarms)
                {
                    writer.WriteLine(logInfo, alarm.AlarmTime, alarm.AlarmCategory, alarm.Enabled ? "True" : "False");
                }
            }
        }
    }
}
