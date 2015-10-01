using System.Windows;
using Arena.ViewModels;
using Arena.Views;

namespace Arena
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow(new MainWindowViewModel());
            mainWindow.Show();
        }
    }
}
