using Common.Interfaces;

namespace Common.Models
{
    public class GameConfiguration : IConfigurable
    {
        public string Type { get; set; }
        public int NextMoveDelay { get; set; }
    }
}
