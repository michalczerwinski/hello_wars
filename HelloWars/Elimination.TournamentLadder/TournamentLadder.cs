using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Common.Interfaces;
using Elimination.TournamentLadder.UserControls;
using Elimination.TournamentLadder.ViewModels;

namespace Elimination.TournamentLadder
{
    public class TournamentLadder : IElimination
    {
        private TournamentLadderViewModel _tournamentLadderViewModel;
        public List<ICompetitor> Bots { get; set; }

        public UserControl GetVisualization(IConfigurable configuration)
        {
            if (Bots != null)
            {
                _tournamentLadderViewModel = new TournamentLadderViewModel(Bots);
                return new TournamentLadderControl(_tournamentLadderViewModel);
            }

            return null;
        }

        public IList<ICompetitor> GetNextCompetitors()
        {
            var result = new List<ICompetitor>();

            foreach (var stageList in _tournamentLadderViewModel.StageLists)
            {
                foreach (var bot in stageList)
                {
                    if (stageList.Count > 1)
                    {
                        var botViewModel = bot.ViewModel;
                        if (botViewModel.StilInGame)
                        {
                            var connectedBot = stageList.First(f => f.PairWithId == bot.Id);

                            if (connectedBot.ViewModel.StilInGame)
                            {
                                if (botViewModel.BotClient == null || !botViewModel.BotClient.IsVerified)
                                {
                                    UpdateTournamentLadder(bot, 0);
                                    UpdateTournamentLadder(connectedBot, 1);
                                    return GetNextCompetitors();
                                }
                                if (connectedBot.ViewModel.BotClient == null || !connectedBot.ViewModel.BotClient.IsVerified)
                                {
                                    UpdateTournamentLadder(bot, 1);
                                    UpdateTournamentLadder(connectedBot, 0);
                                    return GetNextCompetitors();
                                }

                                result.Add(bot.ViewModel.BotClient);
                                result.Add(connectedBot.ViewModel.BotClient);
                                return result;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public void SetLastDuelResult(IDictionary<ICompetitor, double> resultDictionary)
        {
            if (resultDictionary != null && resultDictionary.Any(pair => pair.Value > 0))
            {
                foreach (var singleResult in resultDictionary)
                {
                    var botControl = ReturnBotControl(singleResult.Key);

                    if (botControl != null)
                    {
                        UpdateTournamentLadder(botControl, singleResult.Value);
                    }
                }
            }
        }

        private void UpdateTournamentLadder(BotUserControl botControl, double score)
        {
            if ((int)score == 1)
            {
                if (_tournamentLadderViewModel.StageLists[botControl.ViewModel.CurrentStage].Count > 1)
                {
                    var nextStageControl = _tournamentLadderViewModel.StageLists[botControl.ViewModel.CurrentStage + 1].First(f => f.Id == botControl.NextStageTargetId);
                    botControl.ViewModel.CurrentStage++;
                    nextStageControl.ViewModel = botControl.ViewModel;
                }
            }
            if ((int)score == 0)
            {
                botControl.ViewModel.StilInGame = false;
            }
        }

        public string GetGameDescription()
        {
            var competitors = GetNextCompetitors();
            return string.Format("Duel: {0} vs {1} {2}", competitors[0].Name, competitors[1].Name, DateTime.Now);
        }

        private BotUserControl ReturnBotControl(ICompetitor botClient)
        {
            BotUserControl botUserControl = null;

            foreach (var stageList in _tournamentLadderViewModel.StageLists)
            {
                var tempViewControl = stageList.FirstOrDefault(f => f.ViewModel.BotClient != null && f.ViewModel.BotClient.Id == botClient.Id);
                if (tempViewControl != null) { botUserControl = tempViewControl; }
            }

            return botUserControl;
        }
    }
}
