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
        public MainWindow() => InitializeComponent();

        private PersonContext db;

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

            db = new PersonContext();

            cboCountries.ItemsSource = db.Adresses.Select(x => x.Country).Distinct().OrderBy(x => x).ToList();

            // init selected radioboxes
            List<string> rboxes = new List<string>();
            panRadios.Children.OfType<RadioButton>().Where(x => x.IsChecked == true).ToList().ForEach(x => {
                lblCheckedRadio.Content = x.Content; // should be exact 1
            });

            // init selected checkboxes
            List<string> cboxes = new List<string>();
            panCheckboxes.Items.OfType<CheckBox>().Where(x => x.IsChecked == true).ToList().ForEach(x => {
                cboxes.Add(x.Content.ToString()); 
            });
            
            lblCheckedCheckboxes.Content = string.Join(", ", cboxes.OrderBy(x => x).ToArray());

            // init marker for text with less than min length red
            panTextboxes.Children.OfType<TextBox>().Where(x => x.Text.Length < sldMinLength.Value).ToList().ForEach(x => {
                x.Background = Brushes.Red;
            });

            // pan buttons
            db.Persons.Select(x => x.Adress.City).Distinct().OrderBy(x=>x).ToList().ForEach(x=>{

                Button btn = new Button()
                {
                    Content = x
                };
                btn.Click += Btn_Click;

            panButtons.Children.Add(btn);
            });

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            // clear children if needed ?
            panPersons.Children.Clear();

            Button btn = (Button) sender;
            db.Persons.Select(x => x).Where(x => x.Adress.City.Equals(btn.Content.ToString())).ToList().ForEach(x=> {
                panPersons.Children.Add(new Label()
                {
                    Content = $"{x.Lastname} {x.Firstname}"
                });
            });
        }

        private void CboCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstPersonsInCountry.ItemsSource = db.Persons.Select(x => x).Where(x => x.Adress.Country.Equals(cboCountries.SelectedItem)).OrderBy(x=>x.Lastname);
        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            lstPersonsFound.ItemsSource = db.Persons.Select(x => x).Where(x => x.Firstname.ToLower().Contains(txtSearch.Text.ToLower()) || x.Lastname.ToLower().Contains(txtSearch.Text.ToLower())).OrderBy(x => x.Lastname).ThenBy(x => x.Firstname);

        }

        private void SldMinLength_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            panTextboxes.Children.OfType<TextBox>().Where(x => x.Text.Length < sldMinLength.Value).ToList().ForEach(x => {
                x.Background = Brushes.Red;
            });
            panTextboxes.Children.OfType<TextBox>().Where(x => x.Text.Length > sldMinLength.Value).ToList().ForEach(x => {
                x.Background = Brushes.White;
            });
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if(rb is not null && lblCheckedRadio is not null)
            {
                lblCheckedRadio.Content = rb.Content;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            
            if(lblCheckedCheckboxes is not null)
            {
                string[] str = lblCheckedCheckboxes.Content.ToString().Split(", ");
                List<string> strlst = str.Select(x => x).ToList();
                strlst.Add(cb.Content.ToString());
                strlst.Remove(""); // remove empty string from initializing
                lblCheckedCheckboxes.Content = string.Join(", ", strlst.OrderBy(x=>x).ToArray());
            }
            
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (lblCheckedCheckboxes is not null)
            {
                string[] str = lblCheckedCheckboxes.Content.ToString().Split(", ");
                List<string> strlst = str.Select(x => x).ToList();
                strlst.Remove(cb.Content.ToString());
                lblCheckedCheckboxes.Content = string.Join(", ", strlst.ToArray());
            }
        }
    }
}
