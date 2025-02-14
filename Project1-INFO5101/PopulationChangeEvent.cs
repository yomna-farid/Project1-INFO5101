using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Project1_INFO5101
{

    public class PopulationChangeEvent
    {
        // Define the delegate for population changes
        public delegate void PopulationChangedHandler(string message);

        // Define an event based on the delegate
        public event PopulationChangedHandler? PopulationChanged;

        // Method to update city population 
        public void UpdatePopulation(string city, string fileFormat)
        {
            DataModeler dataModeler = new DataModeler();
            if (dataModeler.CityDictionary.ContainsKey(city))
            {
                List<CityInfo> list = dataModeler.CityDictionary[city];

                foreach (CityInfo cityInfo in list)
                {


                    Console.WriteLine($"Changing the popluation figure for {cityInfo.Name}, {cityInfo.StateAbbrev}...");

                    Console.WriteLine($"Current population: {cityInfo.Population} ");
                    break;
                }

                Console.WriteLine("Revised Population:");
                if (int.TryParse(Console.ReadLine(), out int newPopulation))
                {
                    foreach (CityInfo cityInfo in list)
                    {

                        if (newPopulation > 0 && newPopulation != cityInfo.Population)
                        {

                            Console.WriteLine("Changing the population figure for" + cityInfo.Name + "," + cityInfo.StateAbbrev + "...");

                            int oldPopulation = cityInfo.Population;
                            cityInfo.Population = newPopulation;

                            // Recalculate Density
                            cityInfo.Density = cityInfo.Density * ((double)newPopulation / oldPopulation);

                           
                            PopulationChanged?.Invoke($"Population of {cityInfo.Name}, {cityInfo.StateAbbrev} updated from {oldPopulation} to {newPopulation}. New density: {cityInfo.Density:F2}");
                        }
                    }
                }
            }
        }


    }
}

    


