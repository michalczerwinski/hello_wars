using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Arena.Commands;
using Arena.Commands.MenuItemCommands;
using Arena.Configuration;
using Common;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Common.Serialization;
using Common.Utilities;

namespace Arena.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private List<ICompetitor> _competitors;
        private ObservableCollection<GameHistoryEntryViewModel> _gameHistory;
        private bool _isHistoryVisible;
        private bool _isOutputVisible;
        private int _selectedTabIndex;
        private string _outputText;
        private static readonly object _lock = new object();
        private bool _isFullScreenApplied;
        private bool _isGameInProgress;
        private bool _isGamePaused;
        private bool _isPlayButtonAvailable;
        private bool _isRestartAndStopButtonAvailable;
        private bool _isPauseButtonAvailable;
        private bool _isArenaMessageVisible;
        private GameSpeedMode _currentSpeedMode;
        private string _arenaMessage;

        private ICommand _autoPlayCommand;
        private ICommand _restartCommand;
        private ICommand _pauseCommand;
        private ICommand _onLoadedCommand;
        private ICommand _openCommand;
        private ICommand _openGameConfigCommand;
        private ICommand _closeCommand;
        private ICommand _verifyPlayersCommand;
        private ICommand _gameRulesCommand;
        private ICommand _aboutCommand;
        private ICommand _toggleHistoryCommand;
        private ICommand _playDuelCommand;
        private ICommand _showArenaInstructionCommand;
        private ICommand _fullScreenWindowCommand;
        private ICommand _presentPlayersCommand;
        private ICommand _setNormalSpeedCommand;
        private ICommand _setFastSpeedCommand;
        private ICommand _setVeryFastSeedCommand;
        private ICommand _stopDuelCommand;

        private WindowState _windowState;
        private WindowStyle _windowStyle;
        private Visibility _playerPresentationVisibility;

        public ArenaConfiguration ArenaConfiguration { get; set; }
        public IElimination Elimination { get; set; }
        public IGame Game { get; set; }
        public ScoreList ScoreList { get; set; }
        public bool ShouldRestartGame { get; set; }

        public string OutputText
        {
            get { return _outputText; }
            set { SetProperty(ref _outputText, value); }
        }

        public bool IsGameInProgress
        {
            get { return _isGameInProgress; }
            set
            {
                SetProperty(ref _isGameInProgress, value);
                IsPlayButtonAvailable = !value;
                CalculateButtonsAvailability();
            }
        }

        public bool IsGamePaused
        {
            get { return _isGamePaused; }
            set
            {
                _isGamePaused = value;
                CalculateButtonsAvailability();
            }
        }

        public bool IsPlayButtonAvailable
        {
            get { return _isPlayButtonAvailable; }
            set { SetProperty(ref _isPlayButtonAvailable, value); }
        }

        public bool IsRestartAndStopButtonAvailable
        {
            get { return _isRestartAndStopButtonAvailable; }
            set { SetProperty(ref _isRestartAndStopButtonAvailable, value); }
        }

        private void CalculateButtonsAvailability()
        {
            IsPauseButtonAvailable = IsGameInProgress && !IsArenaMessageVisible;
            IsRestartAndStopButtonAvailable = ( IsGameInProgress || IsGamePaused ) && !IsArenaMessageVisible;
        }

        public bool IsHistoryVisible
        {
            get { return _isHistoryVisible; }
            set { SetProperty(ref _isHistoryVisible, value); }
        }

        public bool IsOutputVisible
        {
            get { return _isOutputVisible; }
            set { SetProperty(ref _isOutputVisible, value); }
        }

        public bool IsPauseButtonAvailable
        {
            get { return _isPauseButtonAvailable; }
            set { SetProperty(ref _isPauseButtonAvailable, value); }
        }

        public GameSpeedMode CurrentSpeedMode
        {
            get { return _currentSpeedMode; }
            set { SetProperty(ref _currentSpeedMode, value);}
        }

        public Visibility PlayerPresentationVisibility
        {
            get { return _playerPresentationVisibility; }
            set { SetProperty(ref _playerPresentationVisibility, value); }
        }

        public bool IsArenaMessageVisible
        {
            get { return _isArenaMessageVisible; }
            set
            {
                SetProperty(ref _isArenaMessageVisible, value);
                CalculateButtonsAvailability();
            }
        }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { SetProperty(ref _selectedTabIndex, value); }
        }

        public ObservableCollection<GameHistoryEntryViewModel> GameHistory
        {
            get { return _gameHistory ?? (_gameHistory = new ObservableCollection<GameHistoryEntryViewModel>()); }
            set { SetProperty(ref _gameHistory, value); }
        }

        public UserControl GameTypeControl
        {
            get { return _gameTypeControl; }
            set { SetProperty(ref _gameTypeControl, value); }
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

        public ICommand OnLoadedCommand
        {
            get { return _onLoadedCommand ?? (_onLoadedCommand = new LoadGameAndEliminationUserControlsOnLoadedControl(this)); }
        }

        public WindowStyle WindowStyle
        {
            get { return _windowStyle; }
            set { SetProperty(ref _windowStyle, value); }
        }

        public WindowState WindowState
        {
            get { return _windowState; }
            set { SetProperty(ref _windowState, value); }
        }

        public string ArenaMessage
        {
            get { return _arenaMessage; }
            set { SetProperty(ref _arenaMessage, value); }
        }

        #region MenuItems

        public ICommand OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new OpenCommand(this)); }
        }

        public ICommand OpenGameConfigCommand
        {
            get { return _openGameConfigCommand ?? (_openGameConfigCommand = new OpenGameConfigCommand(this)); }
        }

        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new CloseCommand()); }
        }

        public ICommand PlayDuelCommand
        {
            get { return _playDuelCommand ?? (_playDuelCommand = new PlayDuelCommand(this)); }
        }

        public ICommand AutoPlayCommand
        {
            get { return _autoPlayCommand ?? (_autoPlayCommand = new AutoPlayCommand(this)); }
        }

        public ICommand RestartCommand
        {
            get { return _restartCommand ?? (_restartCommand = new RestartCommand(this)); }
        }

        public ICommand PauseCommand
        {
            get { return _pauseCommand ?? (_pauseCommand = new PauseCommand(this)); }
        }

        public ICommand VerifyPlayersCommand
        {
            get { return _verifyPlayersCommand ?? (_verifyPlayersCommand = new VerifyPlayersCommand(this)); }
        }

        public ICommand GameRulesCommand
        {
            get { return _gameRulesCommand ?? (_gameRulesCommand = new GameRulesCommand(this)); }
        }

        public ICommand FullScreenWindowCommand
        {
            get { return _fullScreenWindowCommand ?? (_fullScreenWindowCommand = new FullScreenWindowCommand(this)); }
        }

        public ICommand AboutCommand
        {
            get { return _aboutCommand ?? (_aboutCommand = new AboutCommand(this)); }
        }

        public ICommand ToggleHistoryCommand
        {
            get { return _toggleHistoryCommand ?? (_toggleHistoryCommand = new ToggleHistoryCommand(this)); }
        }

        public ICommand PresentPlayersCommand
        {
            get { return _presentPlayersCommand ?? (_presentPlayersCommand = new PresentPlayersCommand(this)); }
        }

        public ICommand ShowArenaInstructionCommand
        {
            get { return _showArenaInstructionCommand ?? (_showArenaInstructionCommand = new ShowArenaInstruction()); }
        }

        public ICommand SetNormalSpeedCommand
        {
            get { return _setNormalSpeedCommand ?? (_setNormalSpeedCommand = new GameSpeedChangeCommand(this, GameSpeedMode.Normal)); }
        }

        public ICommand SetFastSpeedCommand
        {
            get { return _setFastSpeedCommand ?? (_setFastSpeedCommand= new GameSpeedChangeCommand(this, GameSpeedMode.Fast)); }
        }

        public ICommand SetVeryFastSpeedCommand
        {
            get { return _setVeryFastSeedCommand ?? ( _setVeryFastSeedCommand = new GameSpeedChangeCommand(this, GameSpeedMode.VeryFast)); }
        }

        public ICommand StopDuelCommand
        {
            get { return _stopDuelCommand ?? (_stopDuelCommand = new StopDuelCommand(this)); }
        }

        public bool IsFullScreenApplied
        {
            get { return _isFullScreenApplied; }
            set
            {
                SetProperty(ref _isFullScreenApplied, value);
                FullScreenWindowCommand.Execute(null);
            }
        }

        #endregion

        public MainWindowViewModel()
        {
            ScoreList = new ScoreList();
            IsHistoryVisible = true;
            IsOutputVisible = true;
            IsFullScreenApplied = false;
            IsArenaMessageVisible = false;
            PlayerPresentationVisibility = Visibility.Collapsed;
            ApplyConfiguration(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Resources.DefaultArenaConfigurationName);
        }

        public void AskForCompetitors(string gameTypeName, List<ICompetitor> emptyCompetitors)
        {
            IsPlayButtonAvailable = false;

            OutputText += string.Format("Waiting for players ({0})\n", emptyCompetitors.Count);

            Task.Run(() =>
            {
                var competitorsTasks = emptyCompetitors.Select(async bot =>
                {
                    try
                    {
                        await bot.VerifyAsync(gameTypeName);

                        lock (_lock)
                        {
                            OutputText += string.Format("Bot \"{0}\" connected!\n", bot.Name);
                            Elimination.Bots.First(f => f.Id == bot.Id).Name = bot.Name;
                        }
                    }
                    catch (Exception e)
                    {
                        bot.Name = "Not connected";
                        OutputText += string.Format("ERROR: Url: {0} - couldn't verify bot!\nError message:\n{1}\n", bot.Url, e.Message);
                    }

                    return bot;

                }).ToList();

                Task.WhenAll(competitorsTasks).ContinueWith(task =>
                {
                    IsPlayButtonAvailable = true;
                    if (emptyCompetitors.All(competitor => competitor.IsVerified))
                    {
                        OutputText += "All players connected!\n";
                    }
                    else
                    {
                        OutputText += "WARNING: Not all players were succesfully verified.\nTry reconnecting or play tournament without them\n";
                    }
                });
            });
        }

        public void ApplyConfiguration(string configFilePath)
        {
            ArenaConfiguration = ReadConfigurationFromXml(configFilePath);
            InitiateManagedExtensibilityFramework();
            Competitors = ArenaConfiguration.BotUrls.Select(url => new Competitor()
            {
                Id = Guid.NewGuid(),
                Name = "Connecting...",
                Url = url
            } as ICompetitor).ToList();
        }

        public void ApplyGameCustomConfiguration(string configFilePath)
        {
            Game.ApplyConfiguration(ReadFile(configFilePath));
            GameTypeControl = Game.GetVisualisationUserControl(ArenaConfiguration.GameConfiguration);
        }

        public ArenaConfiguration ReadConfigurationFromXml(string path)
        {
            var configurationFile = ReadFile(path);
            var serializer = new XmlSerializer<ArenaConfiguration>();

            return serializer.Deserialize(configurationFile);
        }

        private string ReadFile(string path)
        {
            var xmlStream = new StreamReader(path);

            return xmlStream.ReadToEnd();
        }

        public void InitiateManagedExtensibilityFramework()
        {
            var catalog = new AggregateCatalog();
            var assembly = Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(assembly);
            var dictionary = new DirectoryCatalog(path);
            catalog.Catalogs.Add(dictionary);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(ArenaConfiguration);
        }

        public async Task PlayNextGameAsync()
        {
            var nextCompetitors = Elimination.GetNextCompetitors();
            
            if (nextCompetitors != null)
            {
                await SetArenaMessage(ArenaMessageHelper.GetInitialMessage(nextCompetitors));

                var gameHistoryEntry = new GameHistoryEntryViewModel()
                {
                    GameDescription = Elimination.GetGameDescription(),
                    History = new ObservableCollection<RoundPartialHistory>()
                };

                Game.SetupNewGame(nextCompetitors);
                GameHistory.Add(gameHistoryEntry);

                OutputText += "Game starting: " + gameHistoryEntry.GameDescription + "\n";

                await PlayGameAsync(gameHistoryEntry);
            }
        }

        public async Task ResumeGameAsync()
        {
            var gameHistoryEntry = GameHistory.Last();
            Game.SetPreview(gameHistoryEntry.History.Last().BoardState);
            await PlayGameAsync(gameHistoryEntry);
        }

        private async Task PlayGameAsync(GameHistoryEntryViewModel gameHistoryEntry)
        {
            RoundResult result;

            do
            {
                result = await Game.PerformNextRoundAsync();

                OutputText += result.OutputText;

                foreach (var roundPartialHistory in result.History)
                {
                    gameHistoryEntry.History.Add(roundPartialHistory);
                }

            } while (!result.IsFinished && IsGameInProgress && !IsGamePaused);

            if (result.IsFinished && IsGameInProgress)
            {
                await MakeEndGameConfiguration(result);
            }
        }

        public async Task MakeEndGameConfiguration(RoundResult result)
        {
            Elimination.SetLastDuelResult(result.FinalResult);
            ScoreList.SaveScore(result.FinalResult);
            await SetArenaMessage(ArenaMessageHelper.GetEndGameMessage(result.FinalResult));
        }

        private async Task SetArenaMessage(string message)
        {
            ArenaMessage = message;
            IsArenaMessageVisible = true;
            await DelayHelper.DelayAsync(ArenaConfiguration.ArenaMessageDuration);
            IsArenaMessageVisible = false;
        }

        public void RestartGame()
        {
            ShouldRestartGame = false;
            IsGamePaused = false;
            PlayDuelCommand.Execute(null);
        }
    }
}
