using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Project1_INFO5101
{
    public class PopulationChangeEvent
    {
        // Delegate for population changes
        public delegate void PopulationChangedHandler(string message);

        // Event based on the delegate
        public event PopulationChangedHandler? PopulationChanged;

        // Method to update city population 
        public bool UpdatePopulation(string city, string fileName)
        {
            DataModeler dataModeler = new ();
            dataModeler.ParseFile(fileName, GetFileType(fileName));
            //Validation for city input
            if (!dataModeler.CityDictionary.ContainsKey(city))
            {
                Console.WriteLine("Invalid city input.");
                return false;
            }

            List<CityInfo> list = dataModeler.CityDictionary[city];
            foreach (CityInfo cityInfo in list)
            {
                Console.WriteLine($"Changing the population figure for {cityInfo.Name}, {cityInfo.StateAbbrev}...");
                Console.WriteLine($"Current population: {cityInfo.Population}");

                //validation for population input
                bool validPopulation = false;
                int newPopulation;
                do
                {
                    Console.Write("Revised Population: ");
                    validPopulation = int.TryParse(Console.ReadLine(), out newPopulation) && newPopulation > 0 && newPopulation != cityInfo.Population;
                    if (!validPopulation)
                        Console.WriteLine("Invalid population input.\n");
                } while (!validPopulation);


                int oldPopulation = cityInfo.Population;
                cityInfo.Population = newPopulation;

                // Update density based on new population 
                cityInfo.Density = Math.Round(cityInfo.Density * (newPopulation / (double)oldPopulation),1);

                PopulationChanged?.Invoke($"\nPopulation of {cityInfo.Name}, {cityInfo.StateAbbrev} in {fileName} successfully changed from {oldPopulation} to {newPopulation}.");

                UpdateFile(fileName, cityInfo);
                break;
            }
            return true;
        }

        //Method to update the correct filetype
        private void UpdateFile(string fileName, CityInfo cityInfo)
        {
            int fileType = GetFileType(fileName);
            switch (fileType)
            {
                case 1:
                    UpdateCSV(fileName, cityInfo);
                    break;
                case 2:
                    UpdateJSON(fileName, cityInfo);
                    break;
                case 3:
                    UpdateXML(fileName, cityInfo);
                    break;
                default:
                    throw new ArgumentException("Unsupported file format.");
            }
        }

        //Method to get the correct file
        private int GetFileType(string fileName)
        {
            if (fileName.Equals("usacities.csv")) return 1;
            if (fileName.Equals("usacities.json")) return 2;
            if (fileName.Equals("usacities.xml")) return 3;
            return 0;
        }

        private void UpdateCSV(string fileName, CityInfo updatedCity)
        {
            //Processing files if header is formated different like case sensitive
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,  // Disable strict header validation
                MissingFieldFound = null // Ignore missing fields
            };

            List<dynamic> records;
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, config))
            {
                records = csv.GetRecords<dynamic>().ToList();
            }

            foreach (var record in records)
            {
                if (record.city == updatedCity.Name)
                {
                    record.population = updatedCity.Population;
                    record.density = updatedCity.Density;
                }
            }

            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(records);
            }
        }

        private void UpdateJSON(string fileName, CityInfo updatedCity)
        {
            var cities = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CityInfo>>(File.ReadAllText(fileName));
            if (cities != null)
            {
                var city = cities.FirstOrDefault(c => c.Name == updatedCity.Name);
                if (city != null)
                {
                    city.Population = updatedCity.Population;
                    city.Density = updatedCity.Density;
                    File.WriteAllText(fileName, Newtonsoft.Json.JsonConvert.SerializeObject(cities, Newtonsoft.Json.Formatting.Indented));
                }
            }
        }

        private void UpdateXML(string fileName, CityInfo updatedCity)
        {
            XDocument doc = XDocument.Load(fileName);
            var cityElement = doc.Descendants("city").FirstOrDefault(c => (string?)c.Element("name") == updatedCity.Name);
            if (cityElement != null)
            {
                cityElement.SetElementValue("population", updatedCity.Population);
                cityElement.SetElementValue("density", updatedCity.Density);
                doc.Save(fileName);
            }
        }
    }
}
