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
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Threading;

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
            Loaded += (new RoutedEventHandler(createUserBtn));
        }
        public async void createUserBtn(object sender, RoutedEventArgs e)
        {
            string[] svar = (string[])await selectUser("selectUsers");
            
            for(int i = 0; i < svar.Length; i++)
            {
                Button button = new Button()
                {
                    //Content = USerNameTextBox.Text, 
                    Content = svar[i]

                }; // Creating button


                button.Click += EditUser_Click; //Hooking up to User
                UserListStack.Children.Add(button); //Adding to Stackpanel

                var bc = new BrushConverter();
                button.Background = (Brush)bc.ConvertFrom("#FF009B88"); //BackGround color for NEw button

                string WithoutWhitespace = svar[i].Replace(" ", "");// remove Whitepace

                button.Name = WithoutWhitespace;
                
                //usertolist.Add(new UserToList() { EventName = users("selectOneUser", ), Name = ContacUSerNameTextBox.Text, Phone = ContacUserPhoneNrTextBox.Text });

                USerNameTextBox.Clear();
                ContacUSerNameTextBox.Clear();
                ContacUserPhoneNrTextBox.Clear();

                AddUserBorderBox.Visibility = Visibility.Collapsed;
                AddUserStackpanel.Visibility = Visibility.Collapsed;
                AdminChechBOx.IsChecked = false;
                UserCheckBox.IsChecked = false;
            }
            for (int j = 0; j < svar.Length; j++)
            {
                string username = svar[j];
                string name = (string)await selectInfo("selectInfo", username, "name");
                string password = (string)await selectInfo("selectInfo", username, "password");
                string phone = (string)await selectInfo("selectInfo", username, "phone");
                usertolist.Add(new UserToList() { EventName = username, Name = name, Phone = phone, Password = password });
            }

        }

        public class Info
        {
            private string _Function;
            private string _Username;
            private string _Info;

            public string function
            {
                get { return _Function; }
                set { _Function = value; }
            }
            public string username
            {
                get { return _Username; }
                set { _Username = value; }
            }
            public string info
            {
                get { return _Info; }
                set { _Info = value; }
            }
        }
        public async Task<object> selectInfo(string function, string username, string info)
        {
            HttpClient client = new HttpClient();
            string apiUrl = ConfigurationManager.AppSettings["Api"];
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Info getInfo = new Info();
            getInfo.function = function;
            getInfo.username = username;
            getInfo.info = info;

            var serializedProduct = JsonConvert.SerializeObject(getInfo);

            var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {

                var jsonString = await response.Content.ReadAsStringAsync();
                var json = (JObject)JsonConvert.DeserializeObject(jsonString);
                //MessageBox.Show(json.Value<string>("info"));
                return json.Value<string>("info");

            }
            else
            {

                return false;
            }
        }
        public class getUsers
        {
            private string _Function;

            public string function
            {
                get { return _Function; }
                set { _Function = value; }
            }
        }
        public async Task<object> selectUser(string function)
        {
            HttpClient client = new HttpClient();
            string apiUrl = ConfigurationManager.AppSettings["Api"];
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            getUsers users = new getUsers();
            users.function = function;

            var serializedProduct = JsonConvert.SerializeObject(users);

            var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {

                var jsonString = await response.Content.ReadAsStringAsync();
                dynamic json = (JObject)JsonConvert.DeserializeObject(jsonString);
                //var json = JObject.Parse(jsonString);
                int count = json.usernames.Count;
                
                string[] allNames = new string[count];
                int i = 0;
                while(i < count)
                {
                    string test = json.usernames[i];
                    allNames[i] = test;
                    i++;
                    //return usernames[i];
                }
                return allNames;

            }
            else
            {

                return false;
            }
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
                    //insert admin
                    insertAdmin("createUser", USerNameTextBox.Text, UserPassswordTextBox.Text, ContacUSerNameTextBox.Text, ContacUserPhoneNrTextBox.Text, (bool)AdminChechBOx.IsChecked);
                    MessageBox.Show("you created at admin user");
                    runRest();
                    
                }
                else if (AdminChechBOx.IsChecked == true && UserCheckBox.IsChecked == true)
                {
                    MessageBox.Show("User cant have 2 roles, please uncheck a role");
                }
               
                else
                {
                    //insert normal user

                    insertNormal("createUser", USerNameTextBox.Text, UserPassswordTextBox.Text, ContacUSerNameTextBox.Text, ContacUserPhoneNrTextBox.Text, (bool)UserCheckBox.IsChecked);
                    MessageBox.Show("you created a normal user");
                    runRest();
                }
            }
            else
            {
                MessageBox.Show("There needs to be assigned a Role to the user");
            }
            
            async void runRest()// run if checkbox  controle is in order
            {
                if (regexItem.IsMatch(WithoutspicialChars))// checkes there are no special chars in Username
                {
                    var svar = await users("selectOneUser", USerNameTextBox.Text);
                    
                        Button button = new Button() {
                        //Content = USerNameTextBox.Text, 
                        Content = svar.ToString()
                        
                    }; // Creating button

                    button.Click += EditUser_Click; //Hooking up to User
                    UserListStack.Children.Add(button); //Adding to Stackpanel

                    var bc = new BrushConverter();
                    button.Background = (Brush)bc.ConvertFrom("#FF009B88"); //BackGround color for NEw button

                    string WithoutWhitespace = WithoutspicialChars.Replace(" ", "");// remove Whitepace
                    
                    button.Name = WithoutWhitespace;
                        string username = svar.ToString();
                        string name = (string)await selectInfo("selectInfo", username, "name");
                        string password = (string)await selectInfo("selectInfo", username, "password");
                        string phone = (string)await selectInfo("selectInfo", username, "phone");
                        usertolist.Add(new UserToList() { EventName = username, Name = name, Phone = phone, Password = password });

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

        public class insertUser
        {
            private string _Function;
            private string _Username;
            private string _Password;
            private string _Name;
            private string _Phone;
            private string _Type;

            public string function
            {
                get { return _Function; }
                set { _Function = value; }
            }
            public string username
            {
                get { return _Username; }
                set { _Username = value; }
            }
            public string password
            {
                get { return _Password; }
                set { _Password = value; }
            }
            public string name
            {
                get { return _Name; }
                set { _Name = value; }
            }
            public string phone
            {
                get { return _Phone; }
                set { _Phone = value; }
            }
            public string type
            {
                get { return _Type; }
                set { _Type = value; }
            }
        }
        public async Task<object> insertAdmin(string function, string username, string password, string name, string phone, bool type)
        {
            HttpClient client = new HttpClient();
            string apiUrl = ConfigurationManager.AppSettings["Api"];
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            insertUser insert = new insertUser();
            insert.function = function;
            insert.username = username;
            insert.password = password;
            insert.name = name;
            insert.phone = phone;
            if(type == true)
            {
                insert.type = "1";
            }
            
            var serializedProduct = JsonConvert.SerializeObject(insert);

            var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {

                var jsonString = await response.Content.ReadAsStringAsync();
                var json = (JObject)JsonConvert.DeserializeObject(jsonString);
                //MessageBox.Show(json.Value<string>("status"));
                return json.Value<string>("status");

            }
            else
            {

                return false;
            }
        }
        public async Task<object> insertNormal(string function, string username, string password, string name, string phone, bool type)
        {
            HttpClient client = new HttpClient();
            string apiUrl = ConfigurationManager.AppSettings["Api"];
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            insertUser insert = new insertUser();
            insert.function = function;
            insert.username = username;
            insert.password = password;
            insert.name = name;
            insert.phone = phone;
            if (type == true)
            {
                insert.type = "0";
            }

            var serializedProduct = JsonConvert.SerializeObject(insert);

            var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {

                var jsonString = await response.Content.ReadAsStringAsync();
                var json = (JObject)JsonConvert.DeserializeObject(jsonString);
                //MessageBox.Show(json.Value<string>("status"));
                return json.Value<string>("status");

            }
            else
            {

                return false;
            }
        }

        public class getOneUser
        {
            private string _Function;
            private string _Username;

            public string function
            {
                get { return _Function; }
                set { _Function = value; }
            }
            public string username
            {
                get { return _Username; }
                set { _Username = value; }
            }
        }
        public async Task<object> users(string function, string username)
        {
            HttpClient client = new HttpClient();
            string apiUrl = ConfigurationManager.AppSettings["Api"];
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            getOneUser user = new getOneUser();
            user.function = function;
            user.username = username;

            var serializedProduct = JsonConvert.SerializeObject(user);

            var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {

                var jsonString = await response.Content.ReadAsStringAsync();
                var json = (JObject)JsonConvert.DeserializeObject(jsonString);
                //MessageBox.Show(json.Value<string>("username"));
                return json.Value<string>("username");

            }
            else
            {

                return false;
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserBorderBox.Visibility = Visibility.Visible;
            AddUserStackpanel.Visibility = Visibility.Visible;
            test.Visibility = Visibility.Visible;
            Button button = sender as Button;

            string arr = button.Name;
           

            foreach (var item in usertolist)
            {
                if (button.Name == item.EventName)
                {
                    USerNameTextBox.Text = item.EventName;
                    UserPassswordTextBox.Text = item.Password;
                    ContacUSerNameTextBox.Text = item.Name;
                    ContacUserPhoneNrTextBox.Text = item.Phone;

                }

            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserBorderBox.Visibility = Visibility.Visible;
            AddUserStackpanel.Visibility = Visibility.Visible;
            test.Visibility = Visibility.Hidden;
        }

        private async void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            string user = USerNameTextBox.Text;
            var slet = await delete("deleteUser", user);
            MessageBox.Show(slet.ToString());

        }
        public class deleteUser
        {
            private string _Function;
            private string _Username;

            public string function
            {
                get { return _Function; }
                set { _Function = value; }
            }

            public string username
            {
                get { return _Username; }
                set { _Username = value; }
            }
        }
        public async Task<object> delete(string function, string username)
        {
            HttpClient client = new HttpClient();
            string apiUrl = ConfigurationManager.AppSettings["Api"];
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            deleteUser delete = new deleteUser();
            delete.function = function;
            delete.username = username;

            var serializedProduct = JsonConvert.SerializeObject(delete);

            var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {

                var jsonString = await response.Content.ReadAsStringAsync();
                var json = (JObject)JsonConvert.DeserializeObject(jsonString);
                //MessageBox.Show(json.Value<string>("username"));
                return  USerNameTextBox.Text + " has been deleted";

            }
            else
            {

                return false;
            }
        }
    }
    class UserToList
    {
        private string _Username;
        private string _Password;
        private string _Name;
        private string _Phone;
        


        public string EventName
        {
            get { return _Username; }
            set { _Username = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
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

