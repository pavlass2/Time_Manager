using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TimeManager
{
    public class TimeDirector: INotifyPropertyChanged
    {
        /// <summary>
        /// Countdown
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Storage for remaining time in minutes
        /// </summary>
        public int MinutesRemain { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Store the name of current event
        /// </summary>
        public string EventName { get; set; }
        
        private ActiveEventWindow activeEventWindow;
        private BreakWindow breakWindow;
        private EventDirector eventDirector;
        private OptionDirector optionDirector;

        /// <summary>
        /// Used to alternate between events and breaks
        /// </summary>
        private bool takeABreak;

        /// <summary>
        /// Constructor for TimeDirector
        /// </summary>
        /// <param name="eventDirector">Used EventDirector</param>
        /// <param name="optionDirector">Used OptionDirector</param>
        public TimeDirector(EventDirector eventDirector, OptionDirector optionDirector)
        {
            this.eventDirector = eventDirector;            
            this.optionDirector = optionDirector;
            
        }

        /// <summary>
        /// Starts the next event
        /// </summary>
        /// <param name="activeEventWindow"></param>
        public void StartEvent(ActiveEventWindow activeEventWindow)
        {
            this.activeEventWindow = activeEventWindow;
            MinutesRemain = optionDirector.option.EventLength; //set the length
            EventName = eventDirector.Events[0].Name; //set the name for binding
            takeABreak = false;
            Start();
        }
        
        /// <summary>
        /// Starts the break
        /// </summary>
        /// <param name="breakWindow"></param>
        public void StartBreak(BreakWindow breakWindow)
        {
            this.breakWindow = breakWindow;
            MinutesRemain = optionDirector.option.BreakLength; //set the length
            takeABreak = true;
            Start();
        }
        
        /// <summary>
        /// Starts the countdown
        /// </summary>
        private void Start()
        {
            MinutesRemain = MinutesRemain - 1;
            timer = new Timer(60000); //Nastavuji timer na 60 sekund
            timer.Elapsed += TimeIsUp; //Nastavuji udalosti Elapsed příjemce
            timer.AutoReset = true; //Automatic reset         
            timer.Start();
        }

        /// <summary>
        /// This method is called every 60 seconds
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void TimeIsUp(Object source, ElapsedEventArgs e)
        {
            
            if (MinutesRemain > 0)
            {
                MinutesRemain = MinutesRemain - 1;
            }
            else 
            {
                timer.Dispose();
                if (takeABreak == false) //If eventDirector exists, then delete elapsed event
                {
                    eventDirector.EventDone();
                    EndEvent();
                }
                else //If doesn't exists, then end elapsed break and start next event
                {
                    EndBreak();
                }
            }
            OnPropertyChanged("MinutesRemain");
        }

        /// <summary>
        /// Call when Property changed event is needed
        /// </summary>
        /// <param name="name">Name of the changed property</param>
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Ends event
        /// </summary>
        private void EndEvent()
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                activeEventWindow.Close();
                breakWindow = new BreakWindow(this);
                breakWindow.ShowDialog();
            });
            
        }

        /// <summary>
        /// Ends break
        /// </summary>
        private void EndBreak()
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                breakWindow.Close();
                activeEventWindow = new ActiveEventWindow(this);
                activeEventWindow.ShowDialog();
            });
        }

        public void ShutDown()
        {
            timer.Dispose();
        }

        
    }
}
