using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace TimeManager
{
    /// <summary>
    /// Interaction logic for active.xaml
    /// </summary>
    public partial class ActiveEventWindow : Window
    {
        /// <summary>
        /// Used for countdown and related 
        /// </summary>
        private TimeDirector timeDirector;
        private SoundPlayer soundplayer;

        /// <summary>
        /// Shows the window for working
        /// </summary>
        /// <param name="timeDirector">Used timeDirector</param>
        public ActiveEventWindow(TimeDirector timeDirector)
        {
            (App.Current.MainWindow).Visibility = Visibility.Collapsed; //Collapse mainWindow when activeEventWindow shows
            InitializeComponent();

            soundplayer = new SoundPlayer("sounds/eventStart.wav"); //SoundPlayer for sound play 
            soundplayer.Play();

            this.timeDirector = timeDirector;
            DataContext = timeDirector; //Sets DataContext for contdown and eventName binding
            timeDirector.StartEvent(this); //Starts the countwdown in timeDirector

            this.Closed += new EventHandler(ActiveEventWindow_Closed); //Subscribe to Closed event
        }

        /// <summary>
        /// When activeEventWindow is closed, do this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ActiveEventWindow_Closed(object sender, EventArgs e)
        {
            timeDirector.ShutDown();
            (App.Current.MainWindow).Visibility = Visibility.Visible; //Show the activeEventWindow
        }

        
    
    }
}