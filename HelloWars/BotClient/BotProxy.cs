using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BotClient
{
    public class BotProxy
    {
        public BotProxy(string url)
        {

        }

       


        public string TournamentRegistration()
        {
            var bot = new CompetitorConfiguration
            {
                Url = "http://",
                Name = "Zenek",
                TournamentId = 17,
                AvatarUrl = "http://",
                
            };

            Serialize(bot);

            return "";
        }

        //Begin Tournament 
        //zwraca  Id Tournamentu
        //zwraca 



        public void EndTrounament()
        {
        }


        public void Serialize(CompetitorConfiguration details)
        {
            var serializer = new XmlSerializer(typeof(CompetitorConfiguration));
            using (TextWriter writer = new StreamWriter(@"C:\Xml\Xml.xml"))
            {
                serializer.Serialize(writer, details);
            }
        }
    }
}
