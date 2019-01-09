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
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        OptionDirector optionDirector;
        MainWindow mainWindow;

        public OptionsWindow(OptionDirector optionDirector, MainWindow mainWindow)
        {
            this.optionDirector = optionDirector;
            this.mainWindow = mainWindow;
            DataContext = optionDirector;
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            int eventLength = 30;
            int breakLength = 10;
            
            try
            {
                eventLength = Int32.Parse(eventLengtTextBox.Text);
                breakLength = Int32.Parse(breakLengtTextBox.Text);               
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Option option = new Option(eventLength, breakLength);

            optionDirector.Save(option);
            optionDirector.Load();
            Close();
        }
    }
}
