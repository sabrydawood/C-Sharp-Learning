using System;

class Program
{
    static void Main(string[] args)
    {
        // Define prefixes for the HTTP server
        string[] prefixes = {
        "http://localhost:8080/", // Local
        "http://localhost:5000/",
        "http://localhost:3000/"
        };

        // Instantiate HttpApp object
        HttpApp httpApp = new(prefixes);

        try
        {
            // Start the HTTP server
            httpApp.Start(httpApp.Get_listener());

            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
        }
        finally
        {
            // Stop the server when done
            httpApp.Stop();
        }
    }
}
