using System.Collections.ObjectModel;
using System.Windows;
using lab4.Entity;
using lab4.Request;

namespace lab4.Pages
{
    public partial class SearchBusPage : Window
    {
        public ObservableCollection<Trip> Trips { get; set; }
        public ObservableCollection<Trip> TripsInit { get; set; }

        public SearchBusPage()
        {
            InitializeComponent();
            Trips = new ObservableCollection<Trip>();
            TripsInit = new ObservableCollection<Trip>();
            GetTrips();
        }

        private async void GetTrips()
        {
            var (successful, tripsList) = await TripsResponse.TripsAsync();
            if (successful)
            {
                foreach (var trip in tripsList)
                {
                    Trips.Add(trip);
                    TripsInit.Add(trip);
                }

                UpdateTripList();
            }
        }

        private void UpdateTripList()
        {
            TripsListBox.ItemsSource = Trips;
        }

        private void SearchByName(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var searchText = SearchTextBox.Text.ToLower();
            var filteredTrips = TripsInit.Where(t => t.TripName.ToLower().Contains(searchText)).ToList();
            Trips.Clear();
            foreach (var treap in filteredTrips)
            {
                Trips.Add(treap);
            }

            UpdateTripList();
        }

        private void SearchByData(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SearchDatePicker.SelectedDate.HasValue)
            {
                var selectedDate = SearchDatePicker.SelectedDate.Value;
        
         
                if (selectedDate.DayOfWeek == DayOfWeek.Saturday || selectedDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    MessageBox.Show("Маршрути не працюють у вихідні дні.");
                    return;
                }
                
           
                var holidays = new List<DateTime> { new DateTime(DateTime.Now.Year, 1, 1), new DateTime(DateTime.Now.Year, 12, 25) };
                if (holidays.Contains(selectedDate))
                {
                    MessageBox.Show("Маршрути не працюють у святкові дні.");
                    return;
                }

              
                if (selectedDate < DateTime.Today)
                {
                    MessageBox.Show("Не можна вибрати минулу дату.");
                    return;
                }

      
                if ((selectedDate - DateTime.Today).Days > 30)
                {
                    MessageBox.Show("Пошук маршрутів доступний лише на 30 днів наперед.");
                    return;
                }

                var selectedDateStr = selectedDate.ToString("yyyy-MM-dd");
                var filteredTrips = TripsInit.Where(t => t.StartDate.ToString("yyyy-MM-dd").Contains(selectedDateStr)).ToList();
                Trips.Clear();
                foreach (var treap in filteredTrips)
                {
                    Trips.Add(treap);
                }

                UpdateTripList();
            }
            else
            {
                Trips.Clear();
                foreach (var treap in TripsInit)
                {
                    Trips.Add(treap);
                }

                UpdateTripList();
            }
        }


        private void TripsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedTrip = (Trip)TripsListBox.SelectedItem;
            if (selectedTrip != null)
            {
                BusSeatsPage busSeatsPage = new BusSeatsPage(selectedTrip);
                busSeatsPage.ShowDialog();
            }
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}