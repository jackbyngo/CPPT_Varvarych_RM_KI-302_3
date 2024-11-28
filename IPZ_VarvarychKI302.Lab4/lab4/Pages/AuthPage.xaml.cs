using System.Windows;
using lab4.Entity;
using lab4.Request;

namespace lab4.Pages;


public partial class AuthPage : Window
{
    private AuthEntity authEntity { get; set; }
    
    public AuthPage()
    {
        authEntity = new AuthEntity();
        DataContext = authEntity;
        InitializeComponent();
    }

    private void GoToSignUp(object sender, RoutedEventArgs e)
    {
        var registerPage = new RegisterPage();
        registerPage.Show();
        this.Close();
    }
    
    private async void AuthClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var authRequest = await AuthRequest.AuthAsync(authEntity.Login, authEntity.Password);
            if (authRequest)
            {
                var searchBusPage = new SearchBusPage();
                searchBusPage.Show();
                this.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Виникла помилка: {ex.Message}", "Помилка");
            throw;
        }
    }
}