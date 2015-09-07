﻿using System.IO;
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
        private ArenaConfiguration _arenaConfiguration;

        protected override void OnStartup(StartupEventArgs e)
        {
            _arenaConfiguration = ReadConfigurationFromXML();

            var viewModel = new MainWindowViewModel();
            viewModel.Init(_arenaConfiguration);
            var mainWindow = new MainWindow(viewModel);

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
