using System.Security.Cryptography.X509Certificates;

namespace Project1_INFO5101
{
    public class Program
    {
        static void Main(string[] args)
        {
            ProgramTitle();
            DataSourceSelection();

            //Prompt user to select data source
            Console.Write("Enter you selection: ");



        }

        //ALL INCOMPLETE METHODS

        //Method for program title 
        public static void ProgramTitle()
        {
            Console.WriteLine("U.S Cities Information System v1.0\t\t Data Format: " + //Display the data format here
            "\n-------------------------------------------------------------------\n");

        }

        //Method for data source selection
        public static void DataSourceSelection()
        {
            //Data source selection file names
            //string csv = "usacities.csv";
            //string json = "usacities.json";
            //string xml = "usacities.xml";

            Console.WriteLine("Data source selection...\n\n" +
                "1. CSV  - usacities.csv\n" +
                "2. JSON - usacities.json\n" +
                "3. XML  - usacities.xml");

            Console.Write("Enter you selection: "); //maybe switch to method
            //convert user input to int



        }

        //Method for Main Options
        public static void MainOptions()
        {
            Console.WriteLine("Main Options...\n\n" +
                              "1. Query by City\n" +
                              "2. Query by State" +
                              "3. Change Data Source\n" +
                              "4. Exit the Program\n");

            //Prompt user to select an option
            Console.Write("Enter you selection: "); //maybe switch to method

        }

        //1. Method for Query by City 
        public static void QueryByCity()
        {
            Console.WriteLine("City Options...\n\n" +
                              "1. City Information\n" +
                              "2. Compare Population Density\n" +
                              "3. Distance Between Cities\n" +
                              "4. Distance from Capital\n" +
                              "5. Show City on Map\n" +
                              "6. Adjust Population\n" +
                              "7. Back to Main Options\n");
        }

        //All 6 Options for Query by City
        public static void CityInformation()
        {
            //loop until user enter valid city name 
            Console.WriteLine("Enter city name: ");

            Console.WriteLine("Number of matches: "); //Display number of matches here FIXX MEE

            //loop to Display all details of city or cities in numbered order


        }

        //Method for Query by 
        public static void QueryByState()
        {
            Console.WriteLine("");
        }

        //Method for Change Data Source

        //Method for Exit the Program


    }
}
