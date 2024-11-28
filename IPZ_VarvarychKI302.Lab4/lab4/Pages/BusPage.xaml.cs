using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Linq; // Для LINQ
using System.Collections.Generic;
using lab4.Entity;
using lab4.Request; // Для списків

namespace lab4.Pages
{
    public partial class BusSeatsPage : Window
    {
        private int _totalSeats { get; set; }
        private int? _selectedSeat = null;
        private Button _selectedButton = null;
        private int? _rating = 0;
        private Trip _tripEntity;

        public BusSeatsPage(Trip tripEntity)
        {
            InitializeComponent();
            _tripEntity = tripEntity;
            _totalSeats = (int)tripEntity.AmountSeats;
            GenerateSeatsGrid(_totalSeats);

          
            LoadBookedSeats(_tripEntity.Id); 
            Loaded += async (s, e) => await LoadBookedSeats(_tripEntity.Id);
        }

        private void GenerateSeatsGrid(int totalSeats)
        {
            int rows = 4; 
            int columns = (int)(totalSeats / rows) + (totalSeats % rows == 0 ? 0 : 1);
            int seatNumber = 1;

            // Додаємо колонки
            for (int i = 0; i < columns; i++)
            {
                SeatsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

          
            for (int i = 0; i < rows + 1; i++) 
            {
                if (i == 2)  
                {
                    SeatsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                }
                else
                {
                    SeatsGrid.RowDefinitions.Add(new RowDefinition());
                }
            }

            // Генеруємо місця
            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    if (seatNumber > totalSeats) break;

                    Button seatButton = new Button
                    {
                        Content = seatNumber.ToString(),
                        Width = 50,
                        Height = 50,
                        Margin = new Thickness(5),
                        Background = Brushes.LightGreen
                    };

                    seatButton.Click += SeatClick;

                    
                    int actualRow = row >= 2 ? row + 1 : row;

                    Grid.SetColumn(seatButton, col);
                    Grid.SetRow(seatButton, actualRow);

                    SeatsGrid.Children.Add(seatButton);
                    seatNumber++;
                }
            }
        }


        private void SeatClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int seatIndex = int.Parse(clickedButton.Content.ToString()) - 1;

            if (clickedButton.Background == Brushes.Gray)
            {
                MessageBox.Show("Це місце вже заброньовано.", "Помилка");
                return;
            }

            if (_selectedSeat == seatIndex)
            {
                clickedButton.Background = Brushes.LightGreen;
                _selectedSeat = null;
                _selectedButton = null;
            }
            else
            {
                if (_selectedButton != null)
                {
                    _selectedButton.Background = Brushes.LightGreen;
                }

                clickedButton.Background = Brushes.Red;
                _selectedSeat = seatIndex;
                _selectedButton = clickedButton;
            }

            UpdateBuyButtonVisibility();
        }

        private void UpdateBuyButtonVisibility()
        {
            BuyButton.Visibility = _selectedSeat.HasValue ? Visibility.Visible : Visibility.Collapsed;
        }

        private async Task LoadBookedSeats(int tripId)
        {
            try
            {
               
                List<int> bookedSeats = await SeatRequest.GetBookedSeatsAsync(tripId);

               
                Console.WriteLine("Booked seats: " + string.Join(", ", bookedSeats));

                foreach (var seatButton in SeatsGrid.Children.OfType<Button>())
                {
                    int seatNumber = int.Parse(seatButton.Content.ToString()); 
                    
                    if (bookedSeats.Contains(seatNumber))
                    {
                        seatButton.Background = Brushes.Gray;
                        seatButton.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося отримати заброньовані місця: {ex.Message}", "Помилка");
            }
        }


        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RatingTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void RatingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    int value = int.Parse(textBox.Text);
                    if (value < 1)
                    {
                        textBox.Text = "1";
                    }
                    else if (value > 5)
                    {
                        textBox.Text = "5";
                    }

                    _rating = int.Parse(textBox.Text);
                }
            }
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        private async void BuyTicketsClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_selectedSeat.HasValue)
                {
                    MessageBox.Show("Будь ласка, оберіть місце перед покупкою квитка.", "Помилка");
                    return;
                }

                if (_rating == null || _rating <= 0 || _rating > 5)
                {
                    MessageBox.Show("Оцініть обслуговування перед покупкою квитка (від 1 до 5).", "Помилка");
                    return;
                }

                var seatRequest = await SeatRequest.SeatAsync(AuthEntity.userId, _tripEntity.BusId, _selectedSeat);

                if (seatRequest)
                {
                    await LikeRequest.LikeAsync(_rating.Value, _tripEntity.Id);
                    MessageBox.Show($"Квиток на місце {_selectedSeat.Value + 1} успішно куплений", "Квиток");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Виникла помилка: {ex.Message}", "Помилка");
            }
        }
    }
}
