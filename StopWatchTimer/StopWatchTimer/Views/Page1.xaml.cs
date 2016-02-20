using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace StopWatchTimer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page1 : Page  
    {

        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        public ViewModel DataModel { get; set; }
        
        public Page1()
        {
            this.InitializeComponent();
            DataModel = new ViewModel();
            DataModel.Hour = 1.ToString();
            DataModel.Minute = 20.ToString();
            DataModel.Second = 30.ToString();
            DataModel.MiliSecond = 49.ToString();
            this.DataContext = DataModel;
            dt.Tick += Dt_Tick;
            dt.Interval = new TimeSpan(0, 0, 0, 0, 10);
            stopWatch.Start();
          //  dt.Start();
        }

        private void Dt_Tick(object sender, object e)
        {
            InitializeStoryBoard();
            TimeSpan ts = stopWatch.Elapsed;
            //string currentTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0}",
            //    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            //DataModel.TimeValue = currentTime;
            DataModel.Hour = string.Format("{0:00}", ts.Hours);
            DataModel.Minute = string.Format("{0:00}", ts.Minutes);
            DataModel.Second = string.Format("{0:00}", ts.Seconds);
            DataModel.MiliSecond = string.Format("{0:00}", ts.Milliseconds/10);
        }

        private void InitializeStoryBoard()
        {
            
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (((Windows.UI.Xaml.Controls.SymbolIcon)this.PlayPauseButton.Icon).Symbol.ToString().Equals("Play"))
            {
                this.PlayPauseButton.Icon = new SymbolIcon(Symbol.Pause);
                this.PlayPauseButton.Label = "Pause";
                if(!this.dt.IsEnabled)
                    this.dt.Start();
            }
            else
            {
                this.PlayPauseButton.Icon = new SymbolIcon(Symbol.Play);
                this.PlayPauseButton.Label = "Play";
                this.dt.Stop();
            }


        }

      

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop();
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private string hour;
        public string Hour
        {
            get { return this.hour; }

            set
            {
                if (value != this.hour)
                {
                    this.hour = value;
                    NotifyPropertyChanged("Hour");
                }
            }
        }

        private string miliSecond;
        public string MiliSecond
        {
            get { return this.miliSecond; }

            set
            {
                if (value != this.miliSecond)
                {
                    this.miliSecond = value;
                    NotifyPropertyChanged("MiliSecond");
                }
            }
        }

        private string minute;
        public string Minute
        {
            get { return this.minute; }

            set
            {
                if (value != this.minute)
                {
                    this.minute = value;
                    NotifyPropertyChanged("Minute");
                }
            }
        }

        private string second;
        private string timeValue;

        public string Second
        {
            get { return this.second; }

            set
            {
                if (value != this.second)
                {
                    this.second = value;
                    NotifyPropertyChanged("Second");
                }
            }
        }

        public string TimeValue {
            get { return this.timeValue; }

            set
            {
                if (value != this.timeValue)
                {
                    this.timeValue = value;
                    NotifyPropertyChanged("TimeValue");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
