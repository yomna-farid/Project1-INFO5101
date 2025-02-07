namespace Project1_INFO5101
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataModeler dataModeler = new DataModeler();

            dataModeler.ParseFile("usacities.csv", 3);
        }
    }
}
