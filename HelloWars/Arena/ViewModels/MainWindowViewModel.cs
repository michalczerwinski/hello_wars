﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Arena.Commands;
using Arena.Configuration;
using Arena.Interfaces;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Common.Utilities;

namespace Arena.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _headerText;
        private ArenaConfiguration _arenaConfiguration;
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private string _gameLog;
        private List<ICompetitor> _competitors;
        private ICommand _autoPlayCommand;
        private List<IGame> _gameHistory;
        private Type _gameType; 

        public IElimination Elimination { get; set; }
        public IGame CurrentGame { get; set; }
        public ScoreList ScoreList { get; set; }

        public event EventHandler GameControlChanged;

        protected virtual void OnGameControlChanged()
        {
            if (GameControlChanged != null)
            {
                GameControlChanged(this, EventArgs.Empty);
            }
        }

        public string HeaderText
        {
            get { return _headerText; }
            set { SetProperty(ref _headerText, value); }
        }

        public UserControl GameTypeControl
        {
            get { return _gameTypeControl; }
            set
            {
                SetProperty(ref _gameTypeControl, value);
                OnGameControlChanged();
            }
        }

        public string GameLog
        {
            get { return _gameLog; }
            set { SetProperty(ref _gameLog, value); }
        }

        public UserControl EliminationTypeControl
        {
            get { return _eliminationTypeControl; }
            set { SetProperty(ref _eliminationTypeControl, value); }
        }

        public List<ICompetitor> Competitors
        {
            get { return _competitors; }
            set { SetProperty(ref _competitors, value); }
        }

        public ICommand PlayDuelCommand
        {
            get { return new PlayDuelCommand(this); }
        }

        public ICommand AutoPlayCommand
        {
            get { return _autoPlayCommand ?? (_autoPlayCommand = new AutoPlayCommand(this)); }
        }

        public void Init(ArenaConfiguration arenaConfiguration)
        {
            ScoreList = new ScoreList();
            HeaderText = "Hello Wars();";
            _arenaConfiguration = arenaConfiguration;
            Elimination = arenaConfiguration.Elimination;
            _gameType = TypeHelper<IGame>.GetGameType(arenaConfiguration.GameType);
            _gameHistory = new List<IGame>();

            AskForCompetitors(arenaConfiguration.GameType);

            Elimination.Bots = Competitors;
            _eliminationTypeControl = Elimination.GetVisualization();
        }

        public void SetupNewGame()
        {
            CurrentGame = TypeHelper<IGame>.CreateInstance(_gameType);
            _gameHistory.Add(CurrentGame);
            GameTypeControl = CurrentGame.GetVisualisation();
        }

        private void AskForCompetitors(string gameTypeName)
        {
            Task.Run(async () =>
            {
                var loader = new CompetitorLoadService();

            var competitorsTasks = _arenaConfiguration.BotUrls.Select(botUrl => loader.LoadCompetitorAsync(botUrl, gameTypeName)).ToList();

            Competitors = (await Task.WhenAll(competitorsTasks)).Where(competitor => competitor != null).ToList();
            }).Wait();
        }
    }
}
