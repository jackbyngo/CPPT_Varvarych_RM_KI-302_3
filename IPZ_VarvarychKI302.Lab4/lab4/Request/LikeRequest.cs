using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace lab4.Request;

public class LikeRequest
{
    private const string ServerAddress = "localhost"; 
    private const int ServerPort = 5000;

    public static async Task<bool> LikeAsync(int number, int tripId)
    {
        var registerModel = new
        {
            command = "add_rating",
            number = number.ToString(),
            tripId = tripId.ToString()
        };

        var request = JsonSerializer.Serialize(registerModel);
        var bytesToSend = Encoding.UTF8.GetBytes(request);

        using (var client = new TcpClient(ServerAddress, ServerPort))
        {
            using (var stream = client.GetStream())
            {
                try
                {
                    await stream.WriteAsync(bytesToSend, 0, bytesToSend.Length);

                    using (var memoryStream = new MemoryStream())
                    {
                        var buffer = new byte[1024];
                        int bytesRead;

                        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await memoryStream.WriteAsync(buffer, 0, bytesRead);
                        }

                        string response = Encoding.UTF8.GetString(memoryStream.ToArray());

                        dynamic decodedResponse = JsonConvert.DeserializeObject(response);
                        Console.WriteLine(decodedResponse);

                        var responseObject = JsonSerializer.Deserialize<ResponseWrapper>(response);
                        if (responseObject.success)
                        {
                            return responseObject.success;
                        }
                        
                        MessageBox.Show(responseObject.message);
                        return responseObject.success;
                    }
                }
                catch (JsonException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return false;
                }
            }
        }
    }


    public class ResponseWrapper
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
}