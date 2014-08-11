using System;

namespace Alarms
{
    public class Alarm
    {
        // Category for the alarm.
        public string AlarmCategory { get; set; }

        // Time for the alarm.
        public DateTime AlarmTime { get; set; }

        // Is the tool enabled?
        public bool Enabled { get; set; }
    }
}
