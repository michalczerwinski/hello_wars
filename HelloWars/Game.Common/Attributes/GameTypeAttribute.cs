using System;

namespace Common.Attributes
{
    public class GameTypeAttribute : Attribute
    {
        public string Type { get; set; }

        public GameTypeAttribute(string type)
        {
            Type = type;
        }
    }
}
