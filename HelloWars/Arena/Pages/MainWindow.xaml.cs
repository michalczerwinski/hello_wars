using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml.Serialization;
using Arena.Configuration;
using Arena.EliminationTypes.TournamentLadder.UserControls;
using Arena.Models;
using Arena.Pages.Controls;
using Arena.Serialization;
using Arena.ViewModels;
using BotClient;

namespace Arena.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = (MainWindowViewModel)DataContext;
            EliminationTypeGrid.Children.Add(_viewModel.EliminationTypeControl);
        }
    }
}
