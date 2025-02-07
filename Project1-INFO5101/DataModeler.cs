using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Formats.Asn1;

namespace Project1_INFO5101
{
    internal class DataModeler
    {

      
            // Dictionary to store city data with city name as key
            public Dictionary<string, List<CityInfo>> CityDictionary { get; private set; } = new Dictionary<string, List<CityInfo>>();

            // Delegate for parsing methods
            public delegate void ParseDelegate(string fileName);

            public void ParseFile(string fileName, string fileType)
            {
                ParseDelegate parser;

                switch (fileType.ToLower())
                {

                case "xml":
                    parser = ParseXML;
                    break;

                case "json":
                        parser = ParseJSON;
                        break;
               
                case "csv":
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
                    (int)city.Element("Id")!,
                    (string)city.Element("Name")!,
                    (string)city.Element("StateAbbrev")!,
                    (string)city.Element("State")!,
                    (string)city.Element("Capital")!,
                    (double)city.Element("Latitude")!,
                    (double)city.Element("Longitude")!,
                    (int)city.Element("Population")!,
                    (int)city.Element("Density")!,
                    DateTime.Parse((string)city.Element("TimeZone")!),
                    (string)city.Element("Zips")!
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

        // Parse CSV file
        private void ParseCSV(string fileName)
        {
            using (var reader = new StreamReader(fileName))

                if (!File.Exists(fileName)) throw new FileNotFoundException("File not found.");

            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<dynamic>();
                
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





    



            
       