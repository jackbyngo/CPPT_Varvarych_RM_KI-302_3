using lab4.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using lab4.Entity;
using lab4.Request;

namespace lab4.Pages;

public partial class RegisterPage : Window
{
    private RegisterEntity registerEntity { get; set; }
    
    public RegisterPage()
    {
        registerEntity = new RegisterEntity();
        DataContext = registerEntity;
        InitializeComponent();
    }

    private void GoToSignIn(object sender, RoutedEventArgs e)
    {
        var authPage = new AuthPage();
        authPage.Show();
        this.Close();
    }
    
    private async void RegisterClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (registerEntity.Password != registerEntity.ConfirmationPassword)
            {
                MessageBox.Show($"Виникла помилка: Пароль не співпадає з підтвердженням паролю", "Помилка");
                return;
            }
            else
            {
                var registerRequest = await RegisterRequest.RegisterAsync(registerEntity.Login, registerEntity.Password, registerEntity.ConfirmationPassword);
                if (registerRequest)
                {
                    var searchBusPage = new SearchBusPage();
                    searchBusPage.Show();
                    this.Close();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Виникла помилка: {ex.Message}", "Помилка");
            throw;
        }
    }
}