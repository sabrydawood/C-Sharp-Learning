using Test.json;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppData appData = new();

            Console.WriteLine(appData.ToString());
            Console.WriteLine("Hello, World!");
            Console.ReadKey();
        }
    }
}
