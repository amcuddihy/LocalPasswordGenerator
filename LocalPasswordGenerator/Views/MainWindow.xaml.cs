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

        IUserPreferencesService preferencesService = new JsonUserPreferencesService();
        IPasswordGeneratorService passwordGeneratorService = new SimplePasswordGeneratorService();
        
        // Unfortunately this critical bit of code has to be hidden away here in the MainWindow.xaml.cs file.
        // I do not know of a way to set the DataContext to the dependency injected services from anywhere outside of this file.
        DataContext = new PasswordViewModel(preferencesService, passwordGeneratorService);
    }
}