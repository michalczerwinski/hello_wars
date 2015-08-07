using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Arena.Configuration;
using Arena.Models;
using Arena.Pages;
using Arena.Serialization;
using Arena.ViewModels;

namespace Arena
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ArenaConfiguration ArenaConfiguration { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            ArenaConfiguration = ReadConfigurationFromXML();

            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(ArenaConfiguration)
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
