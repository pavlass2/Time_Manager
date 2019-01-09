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

namespace TimeManager
{
    /// <summary>
    /// Interaction logic for AddEvent.xaml
    /// </summary>
    public partial class AddEventWindow : Window
    {
        private EventDirector eventDirector;

        public AddEventWindow(EventDirector eventDirector)
        {
            InitializeComponent();
            this.eventDirector = eventDirector;
        }

        /// <summary>
        /// Calls eventDirector method Add which adds this new event to the list Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventSaveButton_Click(object sender, RoutedEventArgs e)
        {
            //transform text from combobox to the bool
            bool repeat = false;
            if (eventTypeComboBox.Text == "Repeat")
            {
                repeat = true;
            }

            //adding
            eventDirector.Add(eventNameTextBox.Text, eventNotesTextBox.Text, repeat);
            Close();
        }
    }
}
