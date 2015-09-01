﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Arena.Eliminations.TournamentLadder.UserControls;
using Arena.Eliminations.TournamentLadder.ViewModels;
using Arena.Interfaces;
using Bot = BotClient.BotClient;

namespace Arena.Eliminations.TournamentLadder
{
    public class TournamentLadder : IElimination
    {
        private TournamentLadderViewModel _tournamentLadderViewModel;
        public List<Bot> Bots { get; set; }

        public UserControl GetVisualization()
        {
            if (Bots != null)
            {
                _tournamentLadderViewModel = new TournamentLadderViewModel(Bots);
                return new TournamentLadderControl(_tournamentLadderViewModel);
            }

            return null;
        }

        public IList<Bot> GetNextCompetitors()
        {
            var result = new List<Bot>();

            foreach (var stageList in _tournamentLadderViewModel.StageLists)
            {
                foreach (var bot in stageList)
                {
                    if (stageList.Count > 1)
                    {
                        var botViewModel = bot.ViewModel;
                        if (botViewModel.StilInGame)
                        {
                            var connectedBot = stageList.First(f => f.PairWithId == bot.Id).ViewModel;

                            if (connectedBot.StilInGame)
                            {
                                result.Add(bot.ViewModel.BotClient);
                                result.Add(connectedBot.BotClient);
                                return result;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public void SetLastDuelResult(IDictionary<Bot, double> resultDictionary)
        {
            if (resultDictionary != null)
            {
                foreach (var singleResult in resultDictionary)
                {
                    var botControl = ReturnBotControl(singleResult.Key);

                    if (botControl != null)
                    {
                        if ((int)singleResult.Value == 1)
                        {
                            if (_tournamentLadderViewModel.StageLists[botControl.ViewModel.CurrentStage].Count > 1)
                            {
                                var nextStageControl = _tournamentLadderViewModel.StageLists[botControl.ViewModel.CurrentStage + 1].First(f => f.Id == botControl.NextStageTargetId);
                                botControl.ViewModel.CurrentStage++;
                                nextStageControl.ViewModel = botControl.ViewModel;
                            }
                        }
                        if ((int)singleResult.Value == 0)
                        {
                            botControl.ViewModel.StilInGame = false;
                        }
                    }
                }
            }
        }

        private BotUserControl ReturnBotControl(Bot botClient)
        {
            BotUserControl botUserControl = null;

            foreach (var stageList in _tournamentLadderViewModel.StageLists)
            {
                var tempViewControl = stageList.FirstOrDefault(f => f.ViewModel.BotClient == botClient);
                if (tempViewControl != null) { botUserControl = tempViewControl; }
            }

            return botUserControl;
        }
    }
}
