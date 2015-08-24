using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Arena.Interfaces;

namespace Arena.Games.PickTheWinner
{
    public class PickTheWinner: IGameProvider
    {
        private IGame _game;

        public UserControl GetVisualisation()
        {
           return new PickTheWinnerControl();
        }

        public IGame CreateNewGame(IEnumerable<Arena.Models.Competitor> competitors)
        {
            _game = new PickTheWInnerGame();
            _game.Competitors = competitors.ToList();
            return _game;
        }
    }
}
