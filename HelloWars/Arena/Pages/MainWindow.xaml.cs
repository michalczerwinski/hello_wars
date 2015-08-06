using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Arena.Models;
using BotClient;

namespace Arena.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public DuelPair DuelPair { get; set; }
      //  public List<DuelPair> List { get; set; }
        public RoundList RoundList { get; set; }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var competitors = GenerateCompetitorList();
            DuelPair = new DuelPair();


         //   List = new List<DuelPair>();
            RoundList = new RoundList();

            for (int i = 0; i < competitors.Count; i++)
            {
                var pair = new DuelPair
                {
                    Competitor1 = competitors[i++],
                    Competitor2 = competitors[i]
                };

                RoundList.PairList.Add(pair);
            }

            RoundViewControl1.DataContext = RoundViewControl2.DataContext = RoundViewControl3.DataContext = RoundList;
        }

        private List<Competitor> GenerateCompetitorList()
        {
            var result = new List<Competitor>();

            result.Add(new Competitor
            {
                AvatarUrl = "/Assets/TempFoto.png",
                Name = "Paul",
            });

            result.Add(new Competitor
            {
                AvatarUrl = "/Assets/TempFoto.png",
                Name = "Vladimir",
            });

            result.Add(new Competitor
            {
                AvatarUrl = "/Assets/TempFoto.png",
                Name = "Helena",
            });

            result.Add(new Competitor
            {
                AvatarUrl = "/Assets/TempFoto.png",
                Name = "Duncan",
            });

            return result;
        }
    }
}
