using PersonDBLib;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace LinqCsvDemo_Hue5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            var db = new PersonContext();
            lst_Names.ItemsSource = db.Persons
            //.Where(x => x.Lastname.Length < 5)
            .Where(x => x.Adress.Country=="China")
            .OrderBy(x => x.Lastname)
            .ToList();
            */
        }

        private void CboCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void SldMinLength_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

    }
}
