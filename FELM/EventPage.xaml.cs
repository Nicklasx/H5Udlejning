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

namespace FELM
{
    /// <summary>
    /// Interaction logic for EventPage.xaml
    /// </summary>
    public partial class EventPage : Page
    {
        List<EventToList> eventtolist = new List<EventToList>();
        public EventPage()
        {
            InitializeComponent();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.p3);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.p4);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.p2);
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            AddEventBorderBox.Visibility = Visibility.Visible;
            AddEventStackpanel.Visibility = Visibility.Visible;


        }
        private void EditEvent_Click(object sender, RoutedEventArgs e)
        {
            AddEventBorderBox.Visibility = Visibility.Visible;
            AddEventStackpanel.Visibility = Visibility.Visible;
            Button button = sender as Button;

            string arr = button.Name;
            //int i = eventtolist.IndexOf();

            foreach (var item in eventtolist)
            {
                if (button.Name == item.EventName)
                {
                    EventNameTextBox.Text = item.EventName;
                    ContacNameTextBox.Text = item.Name;
                    ContacPhoneNrTextBox.Text = item.Phone;
                    ContacCityTextBox.Text = item.City;
                    ConcatStreetTextBox.Text = item.Street;
                    ContactNrTextBox.Text = item.Nr;
                    ContactPostalTextBox.Text = item.Postal;


                }


            }
        }


        
        private void SummitEvent_Click(object sender, RoutedEventArgs e)
        {




            AddEventBorderBox.Visibility = Visibility.Collapsed;
            AddEventStackpanel.Visibility = Visibility.Collapsed;


            Button button = new Button() { Content = EventNameTextBox.Text, }; // Creating button


            button.Click += EditEvent_Click; //Hooking up to event
            EventListStack.Children.Add(button); //Adding to Stackpanel

            var bc = new BrushConverter();
            button.Background = (Brush)bc.ConvertFrom("#FF009B88");

            string WithoutWhitespace = EventNameTextBox.Text;
            WithoutWhitespace = WithoutWhitespace.Replace(' ', '_');
            button.Name = WithoutWhitespace;

            eventtolist.Add(new EventToList() { EventName = EventNameTextBox.Text, Name = ContacNameTextBox.Text, Phone = ContacPhoneNrTextBox.Text, City = ContacCityTextBox.Text, Street = ConcatStreetTextBox.Text, Nr = ContactNrTextBox.Text, Postal = ContactPostalTextBox.Text});

            EventNameTextBox.Clear();
            ContacNameTextBox.Clear();
            ContacPhoneNrTextBox.Clear();
            ContacCityTextBox.Clear();
            ConcatStreetTextBox.Clear();
            ContactNrTextBox.Clear();
            ContactPostalTextBox.Clear();
           

        }
    }
}
