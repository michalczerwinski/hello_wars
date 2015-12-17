using Common.Interfaces;

namespace Common.Models
{
    public class EliminationConfiguration : IConfigurable
    {
        public string Type { get; set; }
        public int NextMoveDelay { get; set; }
    }
}
