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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        LoginMechanics LoginMe = new LoginMechanics();
        public LoginPage()
        {
            InitializeComponent();
        }
        private  void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(LoginMe.getUser(LoginTextBox.Text, PasswordTextBox.Password.ToString()))
            {
                NavigationService.Navigate(Pages.p5);
            }
        }
    }
    public class LoginMechanics
    {

        public bool getUser(string userName, string password)
        {
            string internalUserName = "Michael";
            string internalPassword = "Password";

            if (userName == internalUserName && password == internalPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
