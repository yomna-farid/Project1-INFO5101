using System.Xml.Linq;
using System.Globalization;
using CsvHelper;

namespace Project1_INFO5101
{
    internal class DataModeler
    {

      
            // Dictionary to store city data with city name as key
            public Dictionary<string, List<CityInfo>> CityDictionary { get; private set; } = new Dictionary<string, List<CityInfo>>();

            // Delegate for parsing methods
            public delegate void ParseDelegate(string fileName);

            public void ParseFile(string fileName, int fileType)
            {
                ParseDelegate parser;

                switch (fileType)
                {

                case 1:
                    parser = ParseXML;
                    break;

                case 2:
                    parser = ParseJSON;
                    break;
               
                case 3:
                    parser = ParseCSV;
                    break;


                default:
                    throw new ArgumentException("Unsupported file format.");
                }

                parser(fileName);
            }

        // Parse XML file
        private void ParseXML(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException("File not found.");

            XDocument doc = XDocument.Load(fileName);
            var cities = doc.Descendants("City");

            foreach (var city in cities)
            {
                CityInfo cityInfo = new CityInfo(
                    (int)city.Element("id")!,
                    (string)city.Element("name")!,
                    (string)city.Element("state_abbrev")!,
                    (string)city.Element("state")!,
                    (string)city.Element("capital")!,
                    (double)city.Element("lat")!,
                    (double)city.Element("lng")!,
                    (int)city.Element("population")!,
                    (int)city.Element("density")!,
                    (string)city.Element("timezone")!,
                    (string)city.Element("zips")!
                );
                AddToDictionary(cityInfo);
            }
        }



        // Parse JSON file
        private void ParseJSON(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException("File not found.");

            string jsonData = File.ReadAllText(fileName);
        }

        
        private void ParseCSV(string fileName)
        {
            string data = "";
            using (var reader = new StreamReader(fileName))

                if (!File.Exists(fileName)) throw new FileNotFoundException("File not found.");

            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<dynamic>();
                foreach (var record in records)
                {
                    CityInfo cityInfo = new CityInfo(
                        (int)record.Element("id")!,
                        (string)record.Element("name")!,
                        (string)record.Element("state_abbrev")!,
                        (string)record.Element("state")!,
                        (string)record.Element("capital")!,
                        (double)record.Element("lat")!,
                        (double)record.Element("lng")!,
                        (int)record.Element("population")!,
                        (int)record.Element("density")!,
                        (string)record.Element("timezone")!,
                        (string)record.Element("zips")!

                        );
                    AddToDictionary(cityInfo);

                }
            }
        }


        // Add city data to dictionary handling duplicates

        private void AddToDictionary(CityInfo city)
        {
            if (CityDictionary.ContainsKey(city.Name))
            {
                CityDictionary[city.Name].Add(city);
            }
            else
            {
                CityDictionary[city.Name] = new List<CityInfo> { city };
            }
        }

    }


}





    



            
       