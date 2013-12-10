using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using csvReading.ViewModel;

namespace csvReading
{
    public partial class ListaComuni : PhoneApplicationPage
    {
        string prov;

        public ListaComuni()
        {
            InitializeComponent();
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
                App.ViewModel.Cancella();
        }

        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;
            
            //Prelevo dall'elemento selezionato l'oggetto comune per ricavarci l'id
            ComuneVM com = (ComuneVM)(MainListBox.SelectedItem);
            
            // Navigate to the new page
            NavigationService.Navigate(new Uri("/City.xaml?comune=" + com.LineTwo, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData(prov);
            }
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string provincia;
            // recupero il comune di cui cercare il tutto
            if (NavigationContext.QueryString.TryGetValue("prov", out provincia))
            {
                prov = provincia;
            }
            switch (prov)
            {
                case "ve": { Provincia.Text = "Venezia"; break; }
                case "pd": { Provincia.Text = "Padova"; break; }
                case "tv": { Provincia.Text = "Treviso"; break; }
                case "vi": { Provincia.Text = "Vicenza"; break; }
                case "ro": { Provincia.Text = "Rovigo"; break; }
                case "bl": { Provincia.Text = "Belluno"; break; }
                case "vr": { Provincia.Text = "Verona"; break; }
                default: { break; }

            }
        }


  

    }
}