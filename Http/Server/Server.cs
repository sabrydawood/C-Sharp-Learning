using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class HttpApp
{
    private readonly HttpListener _listener = new();
    public HttpApp(string[] prefixes)
    {
        if (!HttpListener.IsSupported)
        {
            throw new NotSupportedException(
                "HTTP Listener is not supported on this platform.");
        }

        // Set the port
        _listener.Prefixes.Clear();
        // Add prefixes
        foreach (string prefix in prefixes)
        {
            _listener.Prefixes.Add(prefix);
        }

    }

    public HttpListener Get_listener()
    {
        return _listener;
    }

    public void Start(HttpListener _listener)
    {
        _listener.Start();
        Console.WriteLine("Server started...");
        Console.WriteLine("Listening on prefixes: ");
        foreach (string prefix in _listener.Prefixes)
        {
            Console.WriteLine(prefix);
        }
        Task.Run(() => Listen());
    }

    public void Stop()
    {
        _listener.Stop();
        _listener.Close();
        Console.WriteLine("Server stopped...");
    }

    private async Task Listen()
    {
        while (_listener.IsListening)
        {
            try
            {
                HttpListenerContext context = await _listener.GetContextAsync();
                string method = context.Request.HttpMethod;
                string path = context.Request.Url?.AbsolutePath;
                switch (path)
                {
                    case "/":
                        HandleRequest(context);
                        break;
                    case "/json":
                        HandleJsonRequest(context);
                        break;
                    default:
                        HandleRequest(context);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    private void HandleRequest(HttpListenerContext context)
    {
        HttpListenerRequest request = context.Request;
        Console.WriteLine($"Request received: {request.HttpMethod} {request.Url}");

        string responseString = "<html><body><h1>Server is Up!" +
            "<br>Method: " + request.HttpMethod + "<br>Path: " + request.Url?.AbsolutePath + "<br>" +
            "</h1></body></html>";
        byte[] buffer = Encoding.UTF8.GetBytes(responseString);

        HttpListenerResponse response = context.Response;
        response.ContentLength64 = buffer.Length;
        response.ContentType = "text/html";
        response.OutputStream.Write(buffer, 0, buffer.Length);

        response.Close();
    }
    private void HandleJsonRequest(HttpListenerContext context)
    {
        HttpListenerRequest request = context.Request;
        Console.WriteLine($"Request received: {request.HttpMethod} {request.Url} ");
        Response<int> res = new()
        {
            Message = "Server is Up!",
            Error = null,
            Method = request.HttpMethod,
            Path = request.Url?.AbsolutePath,
            Date = DateTime.Now,
            Data = 6
        };

        byte[] buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res));

        HttpListenerResponse response = context.Response;
        response.ContentLength64 = buffer.Length;
        response.ContentType = "application/json";
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.Close();
    }
}