using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleWebBotClient.Helpers
{
    public static class NameHelper
    {
        private static readonly Random _rand = new Random(DateTime.Now.Millisecond);

        private static readonly string[] Names = 
        {
            "Czesiek", "Wiesiek", "Michal", "Mateusz", "Robert", "Tomek", "Przemek",
            "Dominik", "Bartek", "Romek", "Asia", "Kasia", "Magda", "Grazyna", "Danuta",
            "Genowefa"
        };

        public static string GetRandomName()
        {
            return Names[_rand.Next(Names.Length)];
        }
    }
}