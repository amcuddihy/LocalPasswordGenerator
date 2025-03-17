using LocalPasswordGenerator.Services;
using LocalPasswordGenerator.ViewModels;
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

namespace LocalPasswordGenerator.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // This is where a different service can be dependency injected into the PasswordViewModel the 'new JsonUserPreferencesService();'
        // can be replaced by the constructor of any class that implements the IUSerPreferenceService interface and it will work. 
        IUserPreferencesService preferencesService = new JsonUserPreferencesService();
        DataContext = new PasswordViewModel(preferencesService);
    }
}