using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
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
        public static List CyclistListMaker()
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
            List<string> ranking = new List<string>();
            List<string> time = new List<string>();

            foreach (var v in rank)
            {
                Console.WriteLine(v);
                if (v.result.Contains(":"))
                {
                    time.Add(v.result);
                }
                else
                {
                    ranking.Add(v.result);
                }
            }

            
        }
    }
}
