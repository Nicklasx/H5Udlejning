using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Baml2006;
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
    /// Interaction logic for ScanPage.xaml
    /// </summary>
    public partial class ScanPage : Page
    {
        
        List<AddDummyDataButton> ListofDummyBUttons = new List<AddDummyDataButton>();
        List<Kabler> kabler = new List<Kabler>();
        List<Speekers> speekers = new List<Speekers>();
        List<Lights> lights = new List<Lights>();

        public ScanPage()
        {
            InitializeComponent();
            adddata();
            DummyShopContent();
            

        }
       

       
        private void EditEvent_Click(object sender, RoutedEventArgs e)
        {
           
            Button button = sender as Button;

            


        }



        private void ScanBackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.p5);
        }
        public void adddata()
        {
            ListofDummyBUttons.Add(new AddDummyDataButton() { eventname = "Roskilde" });
            ListofDummyBUttons.Add(new AddDummyDataButton() { eventname = "Langeland" });
            ListofDummyBUttons.Add(new AddDummyDataButton() { eventname = "Parken" });
            ListofDummyBUttons.Add(new AddDummyDataButton() { eventname = "Thy Rock" });
            ListofDummyBUttons.Add(new AddDummyDataButton() { eventname = "Destortion" });
            ListofDummyBUttons.Add(new AddDummyDataButton() { eventname = "Copenhell" });
            ListofDummyBUttons.Add(new AddDummyDataButton() { eventname = "Morsø Festival" });

            foreach (var item in ListofDummyBUttons)
            {
                Button button = new Button() { Content = item.eventname };
                button.Click += EditEvent_Click; //Hooking up to event
                Eventlistforbuttons.Children.Add(button); //Adding to Stackpanel

                var bc = new BrushConverter();
                button.Background = (Brush)bc.ConvertFrom("#FF009B88");
            }



        }
        public void DummyShopContent()
        {
            kabler.Add(new Kabler() { Kabelname = "Pa", KabelLenght = 100, Volt = 2000, Antal = 50 });
            kabler.Add(new Kabler() { Kabelname = "Pa", KabelLenght = 50, Volt = 2000, Antal = 50 });
            kabler.Add(new Kabler() { Kabelname = "Pa", KabelLenght = 10, Volt = 2000, Antal = 50 });
            kabler.Add(new Kabler() { Kabelname = "Subwoffer", KabelLenght = 100, Volt = 1300, Antal = 50 });
            kabler.Add(new Kabler() { Kabelname = "Subwoffer", KabelLenght = 50, Volt = 1300, Antal = 50 });
            kabler.Add(new Kabler() { Kabelname = "Subwoffer", KabelLenght = 10, Volt = 1300, Antal = 50 });
            kabler.Add(new Kabler() { Kabelname = "Digital", KabelLenght = 100, Volt = 2000, Antal = 50 });
            kabler.Add(new Kabler() { Kabelname = "Digital", KabelLenght = 50, Volt = 2000, Antal = 50 });
            kabler.Add(new Kabler() { Kabelname = "Digital", KabelLenght = 10, Volt = 2000, Antal = 50 });
            kabler.Add(new Kabler() { Kabelname = "Digital", KabelLenght = 5, Volt = 2000, Antal = 50 });

            speekers.Add(new Speekers() { SpeekerName = "B&o", SpeekerSize = 10, Watt = 2000, Antal = 50 });
            speekers.Add(new Speekers() { SpeekerName = "B&o", SpeekerSize = 15, Watt = 2000, Antal = 50 });
            speekers.Add(new Speekers() { SpeekerName = "B&o", SpeekerSize = 18, Watt = 2000, Antal = 50 });
            speekers.Add(new Speekers() { SpeekerName = "Alpine", SpeekerSize = 10, Watt = 2500, Antal = 50 });
            speekers.Add(new Speekers() { SpeekerName = "Alpine", SpeekerSize = 15, Watt = 2500, Antal = 50 });
            speekers.Add(new Speekers() { SpeekerName = "Alpine", SpeekerSize = 18, Watt = 2500, Antal = 50 });
            speekers.Add(new Speekers() { SpeekerName = "Jbl", SpeekerSize = 10, Watt = 3500, Antal = 50 });
            speekers.Add(new Speekers() { SpeekerName = "Jbl", SpeekerSize = 15, Watt = 3500, Antal = 50 });
            speekers.Add(new Speekers() { SpeekerName = "Jbl", SpeekerSize = 18, Watt = 3500, Antal = 50 });

            lights.Add(new Lights() { LightName = "LED", LightLength = 100, NumberOfBolths = 200, Antal = 50 });
            lights.Add(new Lights() { LightName = "LED", LightLength = 150, NumberOfBolths = 200, Antal = 50 });
            lights.Add(new Lights() { LightName = "LED", LightLength = 180, NumberOfBolths = 200, Antal = 50 });
            lights.Add(new Lights() { LightName = "Spot", LightLength = 100, NumberOfBolths = 250, Antal = 50 });
            lights.Add(new Lights() { LightName = "Spot", LightLength = 150, NumberOfBolths = 250, Antal = 50 });
            lights.Add(new Lights() { LightName = "Spot", LightLength = 180, NumberOfBolths = 250, Antal = 50 });
            lights.Add(new Lights() { LightName = "El", LightLength = 100, NumberOfBolths = 350, Antal = 50 });
            lights.Add(new Lights() { LightName = "El", LightLength = 150, NumberOfBolths = 350, Antal = 50 });
            lights.Add(new Lights() { LightName = "El", LightLength = 180, NumberOfBolths = 350, Antal = 50 });


            KabelView.ItemsSource = kabler;
            SpeekerView.ItemsSource = speekers;
            LightsView.ItemsSource = lights;



        }

        private void Kabel_OnClick(object sender, RoutedEventArgs e)
        {
            SpeekerView.Visibility = Visibility.Collapsed;
            LightsView.Visibility = Visibility.Collapsed;
            KabelView.Visibility = Visibility.Visible;


        }
        private void Speeker_OnClick(object sender, RoutedEventArgs e)
        {
            KabelView.Visibility = Visibility.Collapsed;
            LightsView.Visibility = Visibility.Collapsed;
            SpeekerView.Visibility = Visibility.Visible;
        }
        private void Lights_OnClick(object sender, RoutedEventArgs e)
        {
            KabelView.Visibility = Visibility.Collapsed;
            SpeekerView.Visibility = Visibility.Collapsed;
            LightsView.Visibility = Visibility.Visible; 

        }

    }
    class AddDummyDataButton
    {
        private string _EventName;
        public string _KablerList;
        public string _SpeekersList;
        public string _LightsList; 

        public string eventname
        {
            get { return _EventName; }
            set { _EventName = value; }
        }
        
       
            
            
 
    }
    class Kabler
    {

        private string _Kabelname;
        private int    _KabelLenght;
        private int    _Volt;
        private int   _Antal;

        public string Kabelname
        {
            get { return _Kabelname; }
            set { _Kabelname = value; }
        }
        public int KabelLenght
        {
            get { return _KabelLenght; }
            set { _KabelLenght = value; }
        }
        public int Volt
        {
            get { return _Volt; }
            set { _Volt = value; }
        }
        public int Antal
        {
            get { return _Antal; }
            set { _Antal = value; }
        }

        

    }
    class Speekers
    {
        private string _SpeekerName;
        private int    _SpeekerSize;
        private int    _Watt;
        private int    _Antal;

        public string SpeekerName
        {
            get { return _SpeekerName; }
            set { _SpeekerName = value; }
        }
        public int SpeekerSize
        {
            get { return _SpeekerSize; }
            set { _SpeekerSize = value; }
        }
        public int Watt
        {
            get { return _Watt; }
            set { _Watt = value; }
        }
        public int Antal
        {
            get { return _Antal; }
            set { _Antal = value; }
        }
    }
    class Lights
    {
        private string _LightName;
        private int    _LightLength;
        private int    _NumberOfBolths;
        private int    _Antal;
        private Button _Add;

        public string LightName
        {
            get { return _LightName; }
            set { _LightName = value; }
        }

        public int LightLength
        {
            get { return _LightLength; }
            set { _LightLength = value; }
        }
        public int NumberOfBolths
        {
            get { return _NumberOfBolths; }
            set { _NumberOfBolths = value; }
        }
        public int Antal
        {
            get { return _Antal; }
            set { _Antal = value; }
        }
        public Button Add
        {
            get { return _Add; }
            set { _Add = value; }
        }



    }
    
}
