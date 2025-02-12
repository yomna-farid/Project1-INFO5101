using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_INFO5101
{
    public class CityInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StateAbbrev { get; set; }
        public string State { get; set; }
        public string Capital { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Population { get; set; }
        public double Density { get; set; }
        public string TimeZone { get; set; }
        public List<string> Zips { get; set; }


        public CityInfo(int id, string name, string stateAbbrev, string state, string capital, double latitude, double longitude, int population, double density, string timeZone, List<string> zips)
        {
            Id = id;
            Name = name;
            StateAbbrev = stateAbbrev;
            State = state;
            Capital = capital;
            Latitude = latitude;
            Longitude = longitude;
            Population = population;
            Density = density;
            TimeZone = timeZone;
            Zips = zips;
        }
    }
}
