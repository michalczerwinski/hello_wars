using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;

namespace Common.Helpers
{
    public static class ArenaMessageHelper
    {
        public static string GetInitialMessage(IEnumerable<ICompetitor> competitors)
        {
            return string.Join("\nvs\n", competitors.Select(comp => comp.Name));
        }

        public static string GetEndGameMessage(Dictionary<ICompetitor, double> finalResult)
        {
            if (finalResult.Any(res => res.Value == 1.0))
            {
                return finalResult.Single(x => x.Value == 1.0).Key.Name + "\nWINS";
            }

            return "DRAW";
        }
    }
}
