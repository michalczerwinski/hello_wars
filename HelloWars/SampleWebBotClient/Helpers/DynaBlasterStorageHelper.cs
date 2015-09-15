using System;
using System.Collections.Generic;
using System.Drawing;

namespace SampleWebBotClient.Helpers
{
    public static class DynaBlasterStorageHelper
    {
        private static readonly Dictionary<Guid, List<Point>> _locationHistory = new Dictionary<Guid, List<Point>>();

        public static List<Point> GetBotLocationHistory(Guid botId)
        {
            return _locationHistory.ContainsKey(botId) ? _locationHistory[botId] : (_locationHistory[botId] = new List<Point>());
        }
    }
}