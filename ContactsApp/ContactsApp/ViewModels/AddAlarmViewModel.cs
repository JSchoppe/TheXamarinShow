using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using ContactsApp.Models;
using Xamarin.Forms.PlatformConfiguration;

namespace ContactsApp.ViewModels
{
    public sealed class AddAlarmViewModel : INotifyPropertyChanged
    {
        private string name;
        private bool isBusy;
        private string nextAlarmCountdown;

        public AddAlarmViewModel()
        {
            // Default alarm parameters.
            Volume = 0.5;
            AlarmTimeSpan = TimeSpan.FromHours(3);

            isBusy = false;

            SaveAlarmCommand = new Command(async () => await SaveAlarm(),
                                            () => !IsBusy);

            Device.StartTimer(TimeSpan.FromSeconds(1), Update);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public double Volume { get; set; }
        public TimeSpan AlarmTimeSpan { get; set; }

        public string NextAlarmCountdown
        {
            get { return nextAlarmCountdown; }
            set
            {
                nextAlarmCountdown = value;
                OnPropertyChanged();
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;

                if (name == "Miguel")
                    IsBusy = true;
                else
                    IsBusy = false;

                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;

                OnPropertyChanged();
                SaveAlarmCommand.ChangeCanExecute();
            }
        }

        public Command SaveAlarmCommand { get; }

        private async Task SaveAlarm()
        {
            IsBusy = true;

            Alarm alarm = new Alarm();
            alarm.name = name;
            alarm.volume = Volume;
            alarm.time = AlarmTimeSpan;
            // Alarm or it's constructor parameters
            // would be sent to the model here.
            await Task.Delay(2000);

            IsBusy = false;

            await Application.Current.MainPage.DisplayAlert("Save", "Alarm Saved", "OK");
        }

        private bool Update()
        {
            TimeSpan timeLeft = AlarmTimeSpan - DateTime.Now.TimeOfDay;
            if (timeLeft.TotalMilliseconds < 0)
                timeLeft += TimeSpan.FromHours(24);
            NextAlarmCountdown = string.Format("{0:00}:{1:00}:{2:00}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
            return true;
        }
    }
}
