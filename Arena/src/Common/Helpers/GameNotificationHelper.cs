using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;

namespace Common.Helpers
{
    public static class GameNotificationHelper
    {
        public static readonly int NotificationDelay = 2000;
        public static string GetInitialNotification(IList<ICompetitor> competitors)
        {
            return competitors[0].Name + "\nvs\n" + competitors[1].Name;
        }

        public static string GetEndRoundNotification(Dictionary<ICompetitor, double> finalResult)
        {
            if (finalResult.Any(res => res.Value == 1.0))
            {
                return finalResult.SingleOrDefault(x => x.Value == 1.0).Key.Name + "\nWINS";
            }
            else
            {
                return "DRAW";
            }
        }
    }
}
