using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project1_INFO5101
{

    public class PopulationChangeEvent
    {
        // Define the delegate for population changes
        public delegate void PopulationChangedHandler(string message);

        // Define an event based on the delegate
        public event PopulationChangedHandler? PopulationChanged;

        // Method to update city population 
        public void UpdatePopulation(string city, string fileName)
        {
            DataModeler dataModeler = new DataModeler();
            if (dataModeler.CityDictionary.ContainsKey(city))
            {
                List<CityInfo> list = dataModeler.CityDictionary[city];
                int updatedPop = 0;
                double updatedDen = 0;
                foreach (CityInfo cityInfo in list)
                {


                    Console.WriteLine($"Changing the popluation figure for {cityInfo.Name}, {cityInfo.StateAbbrev}...");

                    Console.WriteLine($"Current population: {cityInfo.Population} ");

                    Console.WriteLine("Revised Population:");
                    if (int.TryParse(Console.ReadLine(), out int newPopulation))
                    {
                        if (newPopulation > 0 && newPopulation != cityInfo.Population)
                        {

                            Console.WriteLine("Changing the population figure for" + cityInfo.Name + "," + cityInfo.StateAbbrev + "...");

                            int oldPopulation = cityInfo.Population;
                            cityInfo.Population = newPopulation;
                            updatedPop = cityInfo.Population;
                            // Recalculate Density
                            cityInfo.Density = cityInfo.Density * ((double)newPopulation / oldPopulation);
                            updatedDen = cityInfo.Density;

                            PopulationChanged?.Invoke($"Population of {cityInfo.Name}, {cityInfo.StateAbbrev} updated from {oldPopulation} to {newPopulation}. New density: {cityInfo.Density:F2}");
                        }

                    }


                    if (fileName.Equals("usacities.csv"))
                    {
                        IEnumerable<dynamic> records;
                        using (var reader = new StreamReader(fileName))

                            if (!File.Exists(fileName)) throw new FileNotFoundException("File not found.");

                        using (var reader = new StreamReader(fileName))
                        using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
                        {
                             records = csv.GetRecords<dynamic>();
                            foreach (var record in records)
                            {

                                if (record.Name == city)
                                {
                                    record.Population = updatedPop;
                                    record.Density = updatedDen;
                                }


                            }


                        }
                        using (var writer = new StreamWriter(fileName))
                        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                        {
                            csv.WriteRecords(records);
                        }

                    }

                    
                    if (fileName.Equals("usacities.xml"))
                    {

                        XDocument doc = XDocument.Load(fileName);
                        var cities = doc.Descendants("city");
                        if (cities != null)
                        {
                          foreach(var c in cities)  {
                                c.Element("population")?.SetValue(updatedPop);
                                c.Element("density")?.SetValue(updatedDen);
                            }
                        }

                        // Save changes back to file
                        doc.Save(fileName);
                    }

                    if (fileName.Equals("usacities.json"))
                    {

                        //XDocument doc = XDocument.Load(fileName);
                        //var cities = doc.Descendants("city");
                        //if (cities != null)
                        //{
                        //    foreach (var c in cities)
                        //    {
                        //        c.Element("population")?.SetValue(updatedPop);
                        //        c.Element("density")?.SetValue(updatedDen);
                        //    }
                        //}

                        // Save changes back to file
                       // doc.Save(fileName);
                    }


                    //Console.WriteLine("Revised Population:");
                    //if (int.TryParse(Console.ReadLine(), out int newPopulation))
                    //{
                    //    foreach (CityInfo cityInfo in list)
                    //    {

                    //        if (newPopulation > 0 && newPopulation != cityInfo.Population)
                    //        {

                    //            Console.WriteLine("Changing the population figure for" + cityInfo.Name + "," + cityInfo.StateAbbrev + "...");

                    //            int oldPopulation = cityInfo.Population;
                    //            cityInfo.Population = newPopulation;

                    //            // Recalculate Density
                    //            cityInfo.Density = cityInfo.Density * ((double)newPopulation / oldPopulation);


                    //            PopulationChanged?.Invoke($"Population of {cityInfo.Name}, {cityInfo.StateAbbrev} updated from {oldPopulation} to {newPopulation}. New density: {cityInfo.Density:F2}");
                    //        }
                    //    }
                    //}
                }
            }
        
        }


    }
}

    


