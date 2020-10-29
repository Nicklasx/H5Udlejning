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
//api things\/
using Newtonsoft.Json;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace FELM
{
    
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        
        List<UserToList> usertolist = new List<UserToList>();


        public MainPage()
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

        private void SummitUser_Click(object sender, RoutedEventArgs e)
        {
           

            
            var regexItem = new Regex("^[a-zA-Z0-9]*$");//Check id chars in username is within the given specs

            string WithoutspicialChars = USerNameTextBox.Text;






            //controles if the checkbox for admin or user in null and witch one it is
            if (AdminChechBOx.IsChecked == true || UserCheckBox.IsChecked == true)
            {
                if (AdminChechBOx.IsChecked == true && UserCheckBox.IsChecked == false) 
                {
                    MessageBox.Show("congrats you are an admin");
                    runRest();
                    
                }
                else if (AdminChechBOx.IsChecked == true && UserCheckBox.IsChecked == true)
                {
                    MessageBox.Show("User cant have 2 roles, please uncheck a role");
                }
               
                else
                {
                    MessageBox.Show("sucked it you are a normal user");
                    runRest();
                }
            }
            else
            {
                MessageBox.Show("There needs to be assigned a Role to the user");
            }
            
            void runRest()// run if checkbox  controle is in order
            {
                if (regexItem.IsMatch(WithoutspicialChars))// checkes there are no special chars in Username
                {
                    Button button = new Button() { Content = USerNameTextBox.Text, }; // Creating button


                    button.Click += EditUser_Click; //Hooking up to User
                    UserListStack.Children.Add(button); //Adding to Stackpanel

                    var bc = new BrushConverter();
                    button.Background = (Brush)bc.ConvertFrom("#FF009B88"); //BackGround color for NEw button

                    string WithoutWhitespace = WithoutspicialChars.Replace(" ", "");// remove Whitepace

                    button.Name = WithoutWhitespace;

                    usertolist.Add(new UserToList() { EventName = USerNameTextBox.Text, Name = ContacUSerNameTextBox.Text, Phone = ContacUserPhoneNrTextBox.Text });

                    USerNameTextBox.Clear();
                    ContacUSerNameTextBox.Clear();
                    ContacUserPhoneNrTextBox.Clear();

                    AddUserBorderBox.Visibility = Visibility.Collapsed;
                    AddUserStackpanel.Visibility = Visibility.Collapsed;
                    AdminChechBOx.IsChecked = false;
                    UserCheckBox.IsChecked = false;
                }
                else
                {
                    MessageBox.Show("User Name cannot contain special Chareters or space");
                }
            }


            

           
           
        }
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserBorderBox.Visibility = Visibility.Visible;
            AddUserStackpanel.Visibility = Visibility.Visible;
            Button button = sender as Button;

            string arr = button.Name;
           

            foreach (var item in usertolist)
            {
                if (button.Name == item.EventName)
                {
                    USerNameTextBox.Text = item.EventName;
                    ContacUSerNameTextBox.Text = item.Name;
                    ContacUserPhoneNrTextBox.Text = item.Phone;

                }

            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserBorderBox.Visibility = Visibility.Visible;
            AddUserStackpanel.Visibility = Visibility.Visible;
        }

       
    }
    class UserToList
    {
        private string _Username;
        private string _Name;
        private string _Phone;
        


        public string EventName
        {
            get { return _Username; }
            set { _Username = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

    }




}

