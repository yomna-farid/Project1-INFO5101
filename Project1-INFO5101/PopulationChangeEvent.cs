using System;
using System.Collections.Generic;
using System.Linq;
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
            public void UpdatePopulation(CityInfo city, int newPopulation)
            {
                if (newPopulation > 0 && newPopulation != city.Population)
                {
                    int oldPopulation = city.Population;
                    city.Population = newPopulation;

                    // Recalculate Density
                    city.Density = city.Density * ((double)newPopulation / oldPopulation);

                    // print a message
                    //I think this goes in a s
                    PopulationChanged?.Invoke($"Population of {city.Name}, {city.StateAbbrev} updated from {oldPopulation} to {newPopulation}. New density: {city.Density:F2}");
                }
            }
        }

    }


