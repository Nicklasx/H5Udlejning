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
using System.Web.Script.Serialization;
using System.Net;//
using System.IO; //

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;

namespace FELM
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        Api api = new Api();

        public LoginPage()
        {
            InitializeComponent();
   
        }
        private  void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text != null && PasswordTextBox.Password.ToString() != null)
            {
                var svar = api.apiCall("login", LoginTextBox.Text, PasswordTextBox.Password.ToString());
                if (svar.Result.ToString() == "True")
                {
                    //json.Text = svar.Result.ToString();
                    NavigationService.Navigate(Pages.p5); 
                }
            }
            else
            {
                MessageBox.Show("udfyld login info");
            }

        }
    }
    public class LoginMechanics
    {

        private string _Username;
        private string _Password;
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
    }
    public class Api
    {
        public class Login
        {
            private string _Function;
            private string _Username;
            private string _Password;

            public string function
            {
                get { return _Function; }
                set { _Function = value;}
            }
            public string username
            {
                get { return _Username; }
                set { _Username = value;}
            }
            public string password
            {
                get { return _Password; }
                set { _Password = value;}
            }
        }
        public async Task<object> apiCall(string function, string username, string password)
        {
            HttpClient client = new HttpClient();
            string apiUrl = ConfigurationManager.AppSettings["Api"];
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Login login = new Login();
            login.function = function;
            login.username = username;
            login.password = password;
            var serializedProduct = JsonConvert.SerializeObject(login);
            
            var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {
               
                var jsonString = await response.Content.ReadAsStringAsync();
                var json = (JObject)JsonConvert.DeserializeObject(jsonString);
               
                return json.Value<string>("status");

            }
            else
            {
                
                return false;
            }
        }
    }

}
