using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Media;

namespace TimeManager
{
    /// <summary>
    /// Interaction logic for BreakWindow.xaml
    /// </summary>
    public partial class BreakWindow : Window
    {
        /// <summary>
        /// Used for countdown and related 
        /// </summary>
        private TimeDirector timeDirector;
        private SoundPlayer soundplayer;

        /// <summary>
        /// Shows the window for break
        /// </summary>
        /// <param name="timeDirector">Used timeDirector</param>
        public BreakWindow(TimeDirector timeDirector)
        {
            (App.Current.MainWindow).Visibility = Visibility.Collapsed;  //Collapse mainWindow when breakWindow shows
            InitializeComponent();

            soundplayer = new SoundPlayer("sounds/eventEnd.wav"); //SoundPlayer for sound play
            soundplayer.Play();

            this.timeDirector = timeDirector;
            DataContext = timeDirector; //Sets DataContext for contdown and eventName binding
            timeDirector.StartBreak(this); //Starts the countwdown in timeDirector

            this.Closed += new EventHandler(BreakWindow_Closed);//Subscribe to Closed event
        }

        /// <summary>
        /// When breakWindow is closed, do this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BreakWindow_Closed(object sender, EventArgs e)
        {
            timeDirector.ShutDown();
            (App.Current.MainWindow).Visibility = Visibility.Visible; //Show the mainWindow
        }


    }
}
