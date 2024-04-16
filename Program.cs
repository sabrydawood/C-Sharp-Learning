using Server;
using System.Runtime.InteropServices.ComTypes;
using System.Text.Json;
MyDataObject dataObject = GetJsonData();
string[] prefixes = [];
dataObject.APP_PORTS.ToList().ForEach(port => prefixes = [.. prefixes, .. new string[] { $"{dataObject.APP_URL}:{port}/" }]);
SimpleHttpServer server = new(prefixes, dataObject.APP_NAME);
server.Start();
Console.WriteLine("Press any key to stop the server...");
Console.ReadLine();
server.Stop();

static MyDataObject GetJsonData()
{
    string jsonFilePath = "./json/data.json";
    string jsonString = File.ReadAllText(jsonFilePath);
    MyDataObject dataObject = JsonSerializer.Deserialize<MyDataObject>(jsonString);
    return dataObject;
}
class MyDataObject
{
    public required string APP_NAME { get; set; }
    public required string APP_DESCRIPTION { get; set; }
    public required string APP_VERSION { get; set; }
    public required string APP_AUTHOR { get; set; }
    public required string APP_URL { get; set; }
    public required int[] APP_PORTS { get; set; }
}
