using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Arena.EliminationTypes.TournamentLadder.UserControls;
using Arena.Interfaces;
using Bot = BotClient.BotClient;

namespace Arena.EliminationTypes.TournamentLadder
{
    public class TournamentLadder : IElimination
    {
        private TournamentLadderViewModel _tournamentLadderViewModel;
        public List<Bot> Competitors { get; set; }


        public UserControl GetVisualization()
        {
            if (Competitors != null)
            {
                _tournamentLadderViewModel = new TournamentLadderViewModel(Competitors);
                return new TournamentLadderControl(_tournamentLadderViewModel);
            }

            return null;
        }

        public IList<Bot> GetNextCompetitors()
        {
            var result = new List<Bot>();

            foreach (var stageList in _tournamentLadderViewModel.StageLists)
            {
                foreach (var competitor in stageList)
                {
                    if (stageList.Count > 1)
                    {
                        var competitorViewModel = competitor.ViewModel;
                        if (competitorViewModel.StilInGame)
                        {
                            var connectedCompetitor = stageList.First(f => f.PairWithId == competitor.Id).ViewModel;

                            if (connectedCompetitor.StilInGame)
                            {
                                result.Add(competitor.ViewModel.BotClient);
                                result.Add(connectedCompetitor.BotClient);
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
                    var competitorControl = ReturnCompetitorControl(singleResult.Key);

                    if (competitorControl != null)
                    {
                        if (singleResult.Value == 1)
                        {
                            if (_tournamentLadderViewModel.StageLists[competitorControl.ViewModel.CurrentStage].Count > 1)
                            {
                                var nextStageControl = _tournamentLadderViewModel.StageLists[competitorControl.ViewModel.CurrentStage + 1].First(f => f.Id == competitorControl.NextStageTargetId);
                                competitorControl.ViewModel.CurrentStage++;
                                nextStageControl.ViewModel = competitorControl.ViewModel;
                            }
                        }
                        if (singleResult.Value == 0)
                        {
                            competitorControl.ViewModel.StilInGame = false;
                        }
                    }
                }
            }
        }

        private CompetitorViewControl ReturnCompetitorControl(Bot botClient)
        {
            CompetitorViewControl competitorViewControl = null;

            foreach (var stageList in _tournamentLadderViewModel.StageLists)
            {
                var tempViewControl = stageList.FirstOrDefault(f => f.ViewModel.BotClient == botClient);
                if (tempViewControl != null) { competitorViewControl = tempViewControl; }
            }

            return competitorViewControl;
        }
    }
}
