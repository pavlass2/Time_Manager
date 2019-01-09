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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EventDirector eventDirector;
        private OptionDirector optionDirector;
        private TimeDirector timeDirector;

        /// <summary>
        /// Shows the MainWindow
        /// </summary>
        public MainWindow()
        {
            
            InitializeComponent();

            eventDirector = new EventDirector();
            optionDirector = new OptionDirector();
            timeDirector = new TimeDirector(eventDirector, optionDirector);
            DataContext = eventDirector;
        }

        /// <summary>
        /// Opens the OptionsWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsButton_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow(optionDirector, this);
            optionsWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the AddEventWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventButton_Click(object sender, RoutedEventArgs e)
        {
            AddEventWindow addEventWindow = new AddEventWindow(eventDirector);
            addEventWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the ActiveEventWindow and hides the MainWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startEventButton_Click(object sender, RoutedEventArgs e)
        {
            //Checks if there is any event to start
            try
            {
                ActiveEventWindow activeEventWindow = new ActiveEventWindow(timeDirector);
                activeEventWindow.Show();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }


    }
}
