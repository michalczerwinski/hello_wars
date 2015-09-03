using System.IO;
using System.Reflection;
using System.Windows;
using Arena.Configuration;
using Arena.Serialization;
using Arena.ViewModels;
using Arena.Views;

namespace Arena
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        private ArenaConfiguration _arenaConfiguration;

        protected async override void OnStartup(StartupEventArgs e)
        {
            _arenaConfiguration = ReadConfigurationFromXML();

            var viewModel = new MainWindowViewModel();
            await viewModel.Init(_arenaConfiguration);
            var mainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            mainWindow.Show();
        }

        private ArenaConfiguration ReadConfigurationFromXML()
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var xmlStream = new StreamReader(currentPath + "/ArenaConfiguration.xml");
            var configurationFile = xmlStream.ReadToEnd();

            var serializer = new XmlSerializer<ArenaConfiguration>();

            return serializer.Deserialize(configurationFile);
        }
    }
}
