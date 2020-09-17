using System;

namespace ContactsApp.Models
{
    public sealed class Alarm
    {
        public TimeSpan time;
        public string name;
        public double volume;

        public Alarm()
        {
            time = TimeSpan.Zero;
            name = string.Empty;
            volume = 0;
        }
    }
}
