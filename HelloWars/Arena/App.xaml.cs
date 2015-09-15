using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using Arena.Configuration;
using Arena.ViewModels;
using Arena.Views;
using Common.Serialization;

namespace Arena
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow( new MainWindowViewModel());
            mainWindow.Show();
        }
    }
}
