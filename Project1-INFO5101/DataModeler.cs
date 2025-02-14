using System.Xml.Linq;
using System.Globalization;
using CsvHelper;
using Newtonsoft.Json;
using System.Runtime;
using System;

namespace Project1_INFO5101
{
    internal class DataModeler
    {

        // Dictionary to store city data with city name as key
        public Dictionary<string, List<CityInfo>> CityDictionary { get; private set; } = new Dictionary<string, List<CityInfo>>();


        /// <summary>
        /// Delegate for different file parsing methods (CSV, JSON, XML).
        /// </summary>
        /// <param name="fileName"></param>
        // Delegate for parsing methods
        public delegate void ParseDelegate(string fileName);

        /// <summary>
        /// Determines the file type and calls the appropriate parsing method.
        /// </summary>
        /// <param name="fileName">The name of the file to parse.</param>
        /// <param name="fileType">The file format (1 = CSV, 2 = JSON, 3 = XML).</param>
        public void ParseFile(string fileName, int fileType)
        {
            ParseDelegate parser;

            switch (fileType)
            {
                case 1:
                    parser = ParseCSV;

                    break;

                case 2:
                    parser = ParseJSON;
                    break;

                case 3:
                    parser = ParseXML;
                    break;

                default:
                    throw new ArgumentException("Unsupported file format.");
            }

            parser(fileName);
        }
        /// <summary>
        /// Parses an XML file and extracts city information.
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="FileNotFoundException"></exception>
        // Parse XML file
        private void ParseXML(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException("File not found.");

            XDocument doc = XDocument.Load(fileName);
            var cities = doc.Descendants("city");
            List<string> zips = new();
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
                   (double)city.Element("density")!,
                   (string)city.Element("timezone")!,
                   (string)city.Element("zips")!
                 //  zips!
               );
                AddToDictionary(cityInfo);
            }
        }


        /// <summary>
        /// Parses a JSON file and extracts city information.
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="FileNotFoundException"></exception>
        // Parse JSON file
        private void ParseJSON(string fileName)
        {
            string city = ""; //add all values here
            if (!File.Exists(fileName)) throw new FileNotFoundException("File not found.");

            string jsonData = File.ReadAllText(fileName);
           var cities = JsonConvert.DeserializeObject<List<CityInfo>>(jsonData);

            if (cities != null)
            {
                foreach (var c in cities)
                {

                    CityInfo cityInfo = new CityInfo(
                        c.Id,
                        c.Name,
                        c.StateAbbrev,
                        c.State,
                        c.Capital,
                        c.Latitude,
                        c.Longitude,
                        c.Population,
                        c.Density,
                        c.TimeZone,
                        c.Zips
                    );

                    if (!CityDictionary.ContainsKey(cityInfo.Name))
                    {
                        CityDictionary[cityInfo.Name] = new List<CityInfo>();
                    }
                    CityDictionary[cityInfo.Name].Add(cityInfo);
                }
            }
        }

        /// <summary>
        /// Parses a CSV file and extracts city information.
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="FileNotFoundException"></exception>
        private void ParseCSV(string fileName)
        {
            using (var reader = new StreamReader(fileName))

                if (!File.Exists(fileName)) throw new FileNotFoundException("File not found.");

            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<dynamic>();
                foreach (var record in records)
                {


                    int id = Convert.ToInt32(record.id); 
                    string city = Convert.ToString(record.city);
                  
                    string stateAbbrev = Convert.ToString(record.state_abbrev);
                    string state = Convert.ToString(record.state);
                    string capital = Convert.ToString(record.capital);

                    double lat = 0;
                    double lng = 0;
                    int population = 0;
                    double density = 0;

                    double.TryParse(Convert.ToString(record.lat), out lat);
                    double.TryParse(Convert.ToString(record.lng), out lng);
                    int.TryParse(Convert.ToString(record.population), out population);
                    double.TryParse(Convert.ToString(record.density), out density);

                    string timezone = Convert.ToString(record.timezone);
                    string zips= Convert.ToString(record.zips);
                    CityInfo cityInfo = new CityInfo(id, city, stateAbbrev, state, capital, lat, lng, population, density, timezone, zips);
                    AddToDictionary(cityInfo);
                }
            }
        }


        /// <summary>
        /// Adds a city to the dictionary, handling duplicates by appending to the existing list.
        /// </summary>
        /// <param name="city">The city object to be added.</param>

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












