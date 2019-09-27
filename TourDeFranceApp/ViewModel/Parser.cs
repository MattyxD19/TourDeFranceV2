using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TourDeFranceApp.Model
{
    class Parser
    {
        public List<Cyclist> cyclists = new List<Cyclist>();

        public void Validator()
        {
            string fileName = "Cycling-Tour-De-France.xml";
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.XmlResolver = new XmlUrlResolver();

            settings.ValidationType = ValidationType.DTD;
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(ValidationCallBack);
            settings.IgnoreWhitespace = true;

            XmlReader reader = XmlReader.Create(fileName, settings);

            // Parse the file.
            while (reader.Read())
            {
                Console.WriteLine("{0}, {1}: {2} ", reader.NodeType, reader.Name, reader.Value);
            }

            var readName = XDocument.Parse("Cycling-Tour-De-France.xml").Descendants("event_participants")
                .Select(e => (string)e.Attribute("name")).ToList();

            var readCounty = XDocument.Parse("Cycling-Tour-De-France.xml").Descendants("event_participants")
                .Select(e => (string)e.Attribute("country")).ToList();
        }

        private static void ValidationCallBack(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            if (e.Severity == System.Xml.Schema.XmlSeverityType.Warning)
                Console.WriteLine("Warning: Matching schema not found.  No validation occurred." + e.Message);
            else // Igor
                Console.WriteLine("Validation error: " + e.Message);
        }
        
        // til at få det andet 
        public static List<Cyclist> CyclistListMaker()
        {
            string fileName = "Cycling-Tour-De-France.xml";
            XElement root = XElement.Load(fileName);

            var cyclistAtts = from cyclist in root.Descendants("participant") select new
            {
                name = cyclist.Attribute("name").Value,
                gender = cyclist.Attribute("gender").Value,
                country = cyclist.Attribute("countryFK").Value,         
            };

          List<Cyclist> cyclists = new List<Cyclist>();

        foreach (var x in cyclistAtts)
            {
                Cyclist cyclist = new Cyclist { Name = x.name, Gender = x.gender, Country = x.country};
                cyclists.Add(cyclist);
            }

        // til at få rank og tiden 

         var rank = from feed in root.Descendants("result")
                        select new
                        {
                           result = feed.Attribute("value").Value

                        };
            List<Cyclist> ranking = new List<Cyclist>();
            List<Cyclist> time = new List<Cyclist>();

            foreach(var v in rank)
            {

                if (v.result.Contains(":"))
                {
                    Cyclist cyclist = new Cyclist { ResultTime = v.result };
                    time.Add(cyclist);
                }
                else
                {
                    int x = Int32.Parse(v.result);
                    Cyclist cyclist = new Cyclist { EndPosition = x };
                    ranking.Add(cyclist);
                }
            }

            List<Cyclist> lastList = new List<Cyclist>();
            for (int i = 0; i < cyclists.Count(); i++)
            {
                Cyclist newCyclist = new Cyclist { Name = cyclists.ElementAt(i).Name, Gender = cyclists.ElementAt(i).Gender, Country = cyclists.ElementAt(i).Country, EndPosition = ranking.ElementAt(i).EndPosition, ResultTime = time.ElementAt(i).ResultTime };
                lastList.Add(newCyclist);
            }

            return lastList;

        }
        public static List<StageInfo> StageInfoMaker()
        {
            string fileName = "Cycling-Tour-De-France.xml";
            XElement root = XElement.Load(fileName);

            var sport = from sportName in root.Descendants("sport")
                        select new
                        {
                            sport = sportName.Attribute("name").Value,

                        };

            var stage = from stageInfo in root.Descendants("event")
                        select new
                        {
                            stageName = stageInfo.Attribute("name").Value,
                            date = stageInfo.Attribute("startdate").Value

                        };
            List<StageInfo> infos = new List<StageInfo>();

            for (int i = 0; i < stage.Count(); i++)
            {
                StageInfo stageInfo1 = new StageInfo { Date = stage.ElementAt(i).date, StageName = stage.ElementAt(i).stageName, Sport = sport.ElementAt(i).sport };
                infos.Add(stageInfo1);
            }



            return infos;

        }

        public static void XMLCreator(List<Cyclist> cyclists)
        {
            XDocument xdoc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            // This is the root of the document
            new XElement("cyclist",
            from rider in cyclists
            select
            new XElement("rider",
            new XElement("name"),
            new XElement("gender"),
            new XElement("Country"),
            new XElement("resulttime"),
            new XElement("rank"))));

            // Write the document to the file system. change to a file you have on your pc.             
            xdoc.Save(@"C:\Users\mathi\source\repos\TourDeFranceV2\tomTest.xml");
        }

    }
}
