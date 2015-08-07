using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BotClient
{
    public class BotProxy
    {
        // http://mccomputer/super-mc
        // http://mccomputer/super-mc/bot-info
        // http://mccomputer/super-mc/perform-next-move

        public BotProxy(string url)
        {

        }

        public string GetAvatarUrl()
        {
            return @"C:\Users\mariusz.iwanski\Documents\hello_wars\HelloWars\Arena\Assets\TempFoto.png";
        }

        public string GetName()
        {
            return "afsd";
        }

        public object PerformNextMove(object worldInfo)
        {
            return "";
        }
    }
}
